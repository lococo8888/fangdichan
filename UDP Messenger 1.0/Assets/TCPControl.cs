using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCPControl : MonoBehaviour {
    public bool isClient = false;
    public string ipStr = "127.0.0.1";
    public int port = 6000;
    public SocketServer server;
    public SocketClient client;
	// Use this for initialization
	void Start () {
        if (isClient)
        {
            client.ipStr = ipStr;
            client.port = port;
            client.StartClient();
        }
        else
        {
            server.ipStr = ipStr;
            server.port = port;
            server.StartServer();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
