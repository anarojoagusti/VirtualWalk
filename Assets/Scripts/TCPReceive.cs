using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Net.Sockets;
using System.Threading;

public class TCPReceive : MonoBehaviour
{
	#region private members 	
	private TcpClient socketConnection;
	private Thread clientReceiveThread;
	#endregion

	#region public memebers
	public string IP = "192.168.43.1";  // define in init
	public string showMessage;
	public Text ip_message;
	public Image icono;
	public SampleMessageListener sml;
	public string serverMessage = null;
    //public Text t_panel_id;
    //public GameObject panel;
    //public int currentTimestamp;
    //public int lastTimeStamp;
    #endregion


    private void Start()
    {
		//SendMessage();
		init();
	}

	/// <summary> 	
	/// Setup socket connection. 	
	/// </summary> 	
	private void init()
	{
		//lastTimeStamp = 0;
		try
		{
			clientReceiveThread = new Thread(new ThreadStart(ListenForData));
			clientReceiveThread.IsBackground = true;
			clientReceiveThread.Start();
		}
		catch (Exception e)
		{
			//Debug.Log("On client connect exception " + e);
			ip_message.text = "On client connect exception  " + e;
		}
	}
	/// <summary> 	
	/// Runs in background clientReceiveThread; Listens for incomming data. 	
	/// </summary>     
	private void ListenForData()
	{
		try
		{
			socketConnection = new TcpClient(IP, 41235);
			Byte[] bytes = new Byte[1024];
			SendMessage();
			while (true)
			{
				// Get a stream object for reading 				
				using (NetworkStream stream = socketConnection.GetStream())
				{
					int length;
					
					// Read incomming stream into byte arrary. 					
					while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
					{
						try
						{
							
							var incommingData = new byte[length];
							Array.Copy(bytes, 0, incommingData, 0, length);

							// Convert byte array to string message. 						
							serverMessage = Encoding.ASCII.GetString(incommingData);
							
							//sml.OnMessageArrived(serverMessage);
                        }
                        catch (SocketException socketException)
						{
							Debug.LogError("Socket exception 1: " + socketException);
						}
                    }
				}
			}
		}
		catch (SocketException socketException)
		{
			//Debug.Log("Socket exception 0: " + socketException);
			Debug.LogError("Socket exception 0: " + socketException);
		}
	}
	/// <summary> 	
	/// Send message to server using socket connection. 	
	/// </summary> 	
	public void SendMessage()
	{
		if (socketConnection == null)
		{
			return;
		}
		try
		{
			// Get a stream object for writing. 			
			NetworkStream stream = socketConnection.GetStream();
			if (stream.CanWrite)
			{
				string clientMessage = "#ready";
				// Convert string message to byte array.                 
				byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(clientMessage);
				// Write byte array to socketConnection stream.                 
				stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
			}
		}
		catch (SocketException socketException)
		{
			Debug.Log("Socket exception: " + socketException);
		}
	}

	void Update()
    {
		Debug.Log(serverMessage);
		if (serverMessage != null)
		{
			icono.color = Color.green;
			StepController.instance.ActivateSteps();
			sml.OnMessageArrived(serverMessage);
		}
    }

	//public void OKButton()
	//{
	//	IP = t_panel_id.text;
	//	init();
	//	panel.SetActive(false);
	//}
	//public void Button(string x)
	//{
	//	IP += x.ToString();
	//	t_panel_id.text = IP;
	//}
	
	//public void Button_remove()
	//{
	//	string id_aux = IP.Substring(0, IP.Length - 1);
	//	IP = id_aux;
	//	t_panel_id.text = IP;
	//}
}

