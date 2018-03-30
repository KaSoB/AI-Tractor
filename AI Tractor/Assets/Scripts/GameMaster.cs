using ServerService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using UnityEngine;


public class GameMaster : MonoBehaviour {
    private Server server;
    private AI agent;

    [SerializeField]
    private string serverIP = "127.0.0.1";
    [SerializeField]
    private int port = 8888;
    [SerializeField]
    private int maxClients = 5;

    private bool serverOnline;
    public bool ServerOnline {
        get { return serverOnline; }
        set {
            serverOnline = value;
            if (serverOnline) {
                server.Start();
            } else {
                server.Stop();
            }
        }
    }

    private float timeUpdate = 2F;
    private float tmpTime;

    private void Start() {
        agent = GameObject.FindGameObjectWithTag("AI").GetComponent<AI>();
        server = new Server(IPAddress.Parse(serverIP), port, maxClients);
    }
    
    private void Update() {
        tmpTime += Time.deltaTime;
        if (tmpTime < timeUpdate) {
            return;
        }
        tmpTime = 0;

        ServerUpdate();

    }

    private void ServerUpdate() { // TODO
        foreach (var client in server.Clients.Where(y => y.GetStream().DataAvailable)) {
            foreach (var message in server.LoadText(client)) {
                Debug.Log(message);
                var param = message.Split(' ');
                int x = int.Parse(param[0]);
                int y = int.Parse(param[1]);
                int z = int.Parse(param[2]);
                var vect = new Vector3(x, y, z);
                agent.GoTo(vect);
            }

        }
    }
}
