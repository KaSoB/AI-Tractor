using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

namespace ServerService {
    public class Server {
        public bool Online { get; private set; }
        public IPAddress IP_Adress { get; private set; }
        public int Port { get; private set; }

        private TcpListener Listener;
        private Thread ListenerThread;

        const int MAX_CLIENTS = 5;
        public List<TcpClient> Clients { private set; get; }

        public Server(IPAddress IPAddress, int port, int maxClients = 1) {
            Online = false;
            Port = port;
            IP_Adress = IPAddress;
            Clients = new List<TcpClient>();
        }

        public void Start() {
            if (Online == false) {
                Listener = new TcpListener(IP_Adress, Port);
                ListenerThread = new Thread(new ThreadStart(ListenerUpdate));
                ListenerThread.Start();
                Debug.Log("Włączono serwer");
                Online = true;
            }
        }
        public void Stop() {
            if (Online == true) {
                Debug.Log("Wyłączanie serwera...");
                Listener.Stop();
                ListenerThread.Join();
                Clients.Clear();
                Online = false;
            }
        }

        private void ListenerUpdate() {
            Listener.Start();
            while (true) {
                try {
                    TcpClient c = Listener.AcceptTcpClient();
                    Debug.Log(string.Format("Nawiązano połączenie z {0} ", ((IPEndPoint) c.Client.RemoteEndPoint).Address));
                    if (Clients.Count < MAX_CLIENTS) {
                        HandleClient(c);
                    }
                } catch {
                    Debug.Log("Błąd serwera. Wątek odpowiedzialny za nasłuchiwanie połączeń wywołał błąd.");
                    break;
                }
            }
        }
        protected void HandleClient(TcpClient client) {
            try {
                SendText(client, "Hello");
                Debug.Log("Odebrano od klienta: " + LoadText(client)[0]);
                Clients.Add(client);
            } catch {
                Debug.Log("Nie uzyskano odpowiedzi od klienta lub utracono połączenie.");
            }
        }


        private string[] LoadText(TcpClient c) {
            byte[] buffer = new byte[c.ReceiveBufferSize];
            int bytesRead = c.GetStream().Read(buffer, 0, c.ReceiveBufferSize);
            string text = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            return text.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        }
        private void SendText(TcpClient c, string text) {
            text += '\n';  // Dodanie znaku końca linii
            byte[] buffer = Encoding.ASCII.GetBytes(text);
            c.GetStream().Write(buffer, 0, buffer.Length);
        }

    }
}