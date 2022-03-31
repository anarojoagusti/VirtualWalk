using System.Collections;
using TMPro;
using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;


public class UDPSend : MonoBehaviour
{
    public string IP="192.168.43.1";  // define in init
    public int port = 41234;

    public TMP_Text sendtext;
    public bool isConnected;

    IPEndPoint remoteEndPoint;
    UdpClient client;
    Thread sendThread;

    private void Start()
    {
        isConnected = false;
        DontDestroyOnLoad(this);
        init();
    }

    // init
    public void init()
    {
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);
        client = new UdpClient();
        sendThread = new Thread(
                        new ThreadStart(sendOK));
        sendThread.IsBackground = true;
        sendThread.Start();
    }

    // inputFromConsole
    private void inputFromConsole()
    {
        try
        {
            string text;
            do
            {
                text = Console.ReadLine();
                if (text != "")
                {
                    byte[] data = Encoding.UTF8.GetBytes(text);
                    client.Send(data, data.Length, remoteEndPoint);
                }
            } while (text != "");
        }
        catch (Exception err)
        {
            print(err.ToString());
        }

    }

    public void sendOK()
    {
        while (!isConnected)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes("#ready");
                client.Send(data, data.Length, remoteEndPoint);
                sendtext.text = ">>>" + IP.ToString() + ":" + port.ToString() + " message: #ready;";
            }
            catch (Exception err)
            {
                print(err.ToString());
            }
        }
    }
    private void OnDisable()
    {
        if (sendThread.IsAlive)
            sendThread.Abort();
    }

    // sendData
    public void sendString(string message)
    {
        try
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            client.Send(data, data.Length, remoteEndPoint);
        }
        catch (Exception err)
        {
            print(err.ToString());
        }
    }

    //private void sendEndless(string testStr)
    //{
    //    do
    //    {
    //        sendString(testStr);
    //    }
    //    while (true);
    //}

}
