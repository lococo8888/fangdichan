using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class SocketClient : MonoBehaviour
{
    public string ipStr = "127.0.0.1";
    public int port = 6000;
    Thread c_thread;
    // Use this for initialization
    public void StartClient()
    {
        bt_connect_Click();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //public void SendMsg()
    //{
    //    bt_send_Click(input.text);
    //}

    Socket socketSend;
    private void bt_connect_Click()
    {
        try
        {
            int _port = port;
            string _ip = ipStr;

            //创建客户端Socket，获得远程ip和端口号
            socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(_ip);
            IPEndPoint point = new IPEndPoint(ip, _port);

            socketSend.Connect(point);
            Debug.Log("连接成功!");
            //开启新的线程，不停的接收服务器发来的消息
            c_thread = new Thread(Received);
            c_thread.IsBackground = true;
            c_thread.Start();
        }
        catch (Exception)
        {
            Debug.Log("IP或者端口号错误...");
        }

    }

    /// <summary>
    /// 接收服务端返回的消息
    /// </summary>
    void Received()
    {
        while (true)
        {
            try
            {
                byte[] buffer = new byte[1024 * 1024 * 3];
                //实际接收到的有效字节数
                int len = socketSend.Receive(buffer);
                if (len == 0)
                {
                    break;
                }
                string str = Encoding.UTF8.GetString(buffer, 0, len);
                Debug.Log("客户端打印：" + socketSend.RemoteEndPoint + ":" + str);
            }
            catch { }
        }
    }

    /// <summary>
    /// 向服务器发送消息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void bt_send_Click(string str)
    {
        try
        {
            string msg = str;
            byte[] buffer = new byte[1024 * 1024 * 3];
            buffer = Encoding.UTF8.GetBytes(msg);
            socketSend.Send(buffer);
        }
        catch { }
    }


    public void OnDisable()
    {
        if (c_thread != null)
        {
            c_thread.Abort();
            c_thread = null;
        }
        if(socketSend != null)
            socketSend.Close();
    }
}