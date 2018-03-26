using ServerService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;


public class GameMaster : MonoBehaviour {
    private Server server;


    [SerializeField]
    private string serverIP = "127.0.0.1";

    [SerializeField]
    private int port = 8888;

    [SerializeField]
    private int maxClients = 5;

    private void Start() {
        server = new Server(IPAddress.Parse(serverIP), port, maxClients);
        server.Start();
    }



}
