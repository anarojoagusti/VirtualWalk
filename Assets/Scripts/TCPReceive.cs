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
	public int once = 0;
	public bool long_pierna_get = false;
	public string serverMessage = null;
    
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

							// Convert byte array to string message					
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
		Debug.Log("message arrived: " + serverMessage);
		string[] msg_split = serverMessage.Split('|');
		if (msg_split.Length < 3)
			return;
		else
		{
			if (once == 0)
			{
				icono.color = Color.green;
				StepController.instance.ActivateSteps();
				once ++;
			}
			
			if (long_pierna_get)
			{
				//EMGParams data = JsonUtility.FromJson<EMGParams>("{" + msg_start[1]);
				//t_data_received.text = data.ShowParams();
				StepController.instance.UpdateStride(float.Parse(msg_split[1]), float.Parse(msg_split[0]));
			}
			else
			{

				//EMGParams data = JsonUtility.FromJson<EMGParams>("{" + msg_start[1]);
				StepController.instance.longitud_pierna = float.Parse(msg_split[2]);
				//long_pierna_get = true;
				StepController.instance.SetReferences();
			}
		}
    }

	// Invoked when a line of data is received from the serial device.
	public void OnMessageArrived(string msg)
	{

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

