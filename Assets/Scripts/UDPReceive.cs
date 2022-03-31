using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using TMPro;
using UnityEngine.UI;

public class UDPReceive : MonoBehaviour
{
    // receiving Thread
    Thread receiveThread;

    // udpclient object
    private UdpClient client;

    // public
    public string IP = "192.168.43.1";
    public int port= 41235;

    // infos
    public string lastReceivedUDPPacket = "";
    public string allReceivedUDPPackets = "";

    //UI
    public Text feedbackConnection;
    public Image icono;
    //variables
    public UDPSend udpSender;

    void Start()
    {
        DontDestroyOnLoad(this);
        init();
    }

    // init
    private void init()
    {
        feedbackConnection.text = "IP:port >>>" + IP.ToString() + ":" + port.ToString();
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();

    }

    // receive thread
    private void ReceiveData()
    {
        while (true)
        {
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);
            feedbackConnection.text = "Conectando";
            byte[] data = client.Receive(ref localEndPoint);
            feedbackConnection.text = "Recibiendo datos";
            if (data != null && data.Length > 0)
            {
                icono.color = Color.green;
                string text = Encoding.ASCII.GetString(data);
                // latest UDPpacket
                //lastReceivedUDPPacket = text;
                //allReceivedUDPPackets = allReceivedUDPPackets + text;
                //Debug.Log("<>>>>" + text);
                //if (text == "OK;")
                //{
                //    udpSender.isConnected = true;

                //}
                //else
                //{
                    feedbackConnection.text = text;
                //}
            }
            else
            {
                feedbackConnection.text = "No data";
            }
        }

    }

    private void OnDisable()
    {
        if (receiveThread.IsAlive)
            receiveThread.Abort();
    }

    public string getLatestUDPPacket()
    {
        allReceivedUDPPackets = "";
        return lastReceivedUDPPacket;
    }
}



