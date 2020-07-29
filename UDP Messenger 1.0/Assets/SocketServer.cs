using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class SocketServer : MonoBehaviour
{
    public string ipStr = "127.0.0.1";
    public int port = 6000;
    string currentStr;
    Thread thread;
    Thread r_thread;
    // Use this for initialization
    public void StartServer()
    {
        bt_connnect_Click();
    }

    // Update is called once per frame
    void Update()
    {
        //ttttt.text = currentStr;
    }

    private void bt_connnect_Click()
    {
        try
        {
            int _port = port;
            string _ip = ipStr;

            //点击开始监听时 在服务端创建一个负责监听IP和端口号的Socket
            Socket socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(_ip);
            //创建对象端口
            IPEndPoint point = new IPEndPoint(ip, _port);

            socketWatch.Bind(point);//绑定端口号
            Debug.Log("监听成功!");
            socketWatch.Listen(10);//设置监听，最大同时连接10台

            //创建监听线程
            thread = new Thread(Listen);
            thread.IsBackground = true;
            thread.Start(socketWatch);
        }
        catch { }

    }

    /// <summary>
    /// 等待客户端的连接 并且创建与之通信的Socket
    /// </summary>
    Socket socketSend;
    Socket socketWatch;
    void Listen(object o)
    {
        try
        {
            socketWatch = o as Socket;
            while (true)
            {
                socketSend = socketWatch.Accept();//等待接收客户端连接
                Debug.Log(socketSend.RemoteEndPoint.ToString() + ":" + "连接成功!");
                //开启一个新线程，执行接收消息方法
                r_thread = new Thread(Received);
                r_thread.IsBackground = true;
                r_thread.Start(socketSend);
            }
        }
        catch { }
    }

    /// <summary>
    /// 服务器端不停的接收客户端发来的消息
    /// </summary>
    /// <param name="o"></param>
    void Received(object o)
    {
        try
        {
            Socket socketSend = o as Socket;
            while (true)
            {
                //客户端连接服务器成功后，服务器接收客户端发送的消息
                byte[] buffer = new byte[1024 * 1024 * 3];
                //实际接收到的有效字节数
                int len = socketSend.Receive(buffer);
                if (len == 0)
                {
                    break;
                }
                string str = Encoding.UTF8.GetString(buffer, 0, len);
                currentStr += str + "\n";
                Debug.Log("服务器打印：" + socketSend.RemoteEndPoint + ":" + str);
                Send("我收到了");
            }
        }
        catch { }
    }

    /// <summary>
    /// 服务器向客户端发送消息
    /// </summary>
    /// <param name="str"></param>
    void Send(string str)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(str);
        socketSend.Send(buffer);
    }

    public void OnDisable()
    {
        if (thread != null)
        {
            thread.Abort();
            thread = null;
        }
        if (r_thread != null)
        {
            r_thread.Abort();
            r_thread = null;
        }
        
        if(socketSend != null)
            socketSend.Close();
        if(socketWatch != null)
            socketWatch.Close();
    }
}