using ServerService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public interface INetworkIdentity {
    string GetTextRaport();
}
public interface INetworkController : INetworkIdentity { // todo name
    TaskManager TaskManager { get; }
}

public class NetworkController : MonoBehaviour {
    private Server server;
    private List<INetworkIdentity> farmFields = new List<INetworkIdentity>();
    private List<INetworkController> agents = new List<INetworkController>();

    [SerializeField]
    private string serverIP = "127.0.0.1";
    [SerializeField]
    private int port = 8888;

    private float serverTimeUpdate = 2F;

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
        farmFields = GameObject.FindGameObjectsWithTag("FarmField").Select(y => y.GetComponent<FarmField>()).ToList().ConvertAll(y => (INetworkIdentity) y).ToList();
        agents = GameObject.FindGameObjectsWithTag("AI").Select(y => y.GetComponent<Agent>()).ToList().ConvertAll(y => (INetworkController) y).ToList();
        server = new Server(IPAddress.Parse(serverIP), port);
        InvokeRepeating("ServerUpdate", 1F, serverTimeUpdate);
    }

    private void ServerUpdate() {
        // for each client who has sent data to server
        foreach (var client in server.Clients.Where(y => y.GetStream().DataAvailable)) {
            // and each his command...
            foreach (var command in server.LoadText(client)) {
                Debug.Log(command);
                ProcessTheCommand(client, command);
            }
        }
    }

    private void ProcessTheCommand(TcpClient client, string command) {
        var words = command.Split(' ');
        var commandType = words[0];
        var parameters = words.Skip(1).ToArray();

        switch (commandType) {
            case "GET":
                GetRequest(client, parameters);
                break;
            case "POST":
                PostRequest(client, parameters);
                break;
            default:
                Debug.Log("Unknown requests from client...");
                break;
        }
    }

    // TODO
    /// <summary>
    /// Requests data from a specified resource
    /// </summary>
    private void GetRequest(TcpClient client, string[] parameters) {
        // GET ID-polecenia
        switch (parameters[0]) {
            case "0":
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var item in farmFields) {
                    stringBuilder.Append(item.GetTextRaport()).Append(' ');
                }
                server.SendText(client, stringBuilder.ToString());
                break;
            case "1":
                StringBuilder stringBuilder2 = new StringBuilder();
                foreach (var item in agents) {
                    stringBuilder2.Append(item.GetTextRaport()).Append(' ');
                }
                Debug.Log(stringBuilder2.ToString());
                server.SendText(client, stringBuilder2.ToString());
                break;
            default:
                break;
        }
    }

    // TODO
    /// <summary>
    /// Submits data to be processed to a specified resource
    /// </summary>
    private void PostRequest(TcpClient client, string[] parameters) {
        // POST ID-tractora komenda parametr1 parametr2
        switch (parameters[1]) {
            case "0":
                int x = int.Parse(parameters[1]);
                int y = int.Parse(parameters[2]);
                var vect = new Vector3(x, 0, y);
              //  agents.ForEach(c => c.TaskManager.AddTask(new TaskGoTo(, vect)));
               // agents[int.Parse(parameters[0])].TaskManager.AddTask(new TaskGoTo((GameObject)))
                break;
            default:
                break;
        }
    }
}

