using ServerService;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;

public class NetworkController : MonoBehaviour {
    private Server server;

    [SerializeField]
    private string serverIP = "127.0.0.1";
    [SerializeField]
    private int port = 8888;

    private float serverTimeUpdate = 0.1F;

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

    private void Start() {
        server = new Server(IPAddress.Parse(serverIP), port);
        InvokeRepeating("ServerUpdate", 1F, serverTimeUpdate);
    }

    private void ServerUpdate() {
        // for each client who has sent data to server
        foreach (var client in server.Clients.Where(y => y.Value.GetStream().DataAvailable)) {
            // and each his command...
            foreach (var command in server.LoadText(client.Value)) {
                Debug.Log(command);

                // todo: not static parameter
                server.MessageBuffer[1].Add(command);
            }
        }
    }
    public void SendMessage(int id, string text) {
        server.SendText(id, text);
    }
    public List<string> GetAllMessages(int id) {
        var tmp = new List<string>();
        if (server.MessageBuffer.ContainsKey(id)) {
            tmp = new List<string>(server.MessageBuffer[id]);
            server.MessageBuffer[id].Clear();
        }
        return tmp;

    }
}

