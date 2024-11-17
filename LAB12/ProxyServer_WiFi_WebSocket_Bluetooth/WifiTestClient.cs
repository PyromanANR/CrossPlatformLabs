using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ProxyServer_WiFi_WebSocket_Bluetooth
{
    public class WifiTestClient
    {
        private const string ServerAddress = "127.0.0.1"; 
        private const int ServerPort = 12345; 

        public void Send()
        {
            try
            {
                using (TcpClient client = new TcpClient(ServerAddress, ServerPort))
                using (NetworkStream stream = client.GetStream())
                {
                    string message = "Test message from Wi-Fi client!";
                    byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                    stream.Write(messageBytes, 0, messageBytes.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Wi-Fi client: {ex.Message}");
            }
        }
    }
}
