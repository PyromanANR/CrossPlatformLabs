using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace ProxyServer_WiFi_WebSocket_Bluetooth
{
    public class WebSocketTestClient
    {
        private const string ServerAddress = "ws://127.0.0.2:8181"; // WebSocket URL

        public async void Send()
        {
            using (ClientWebSocket webSocket = new ClientWebSocket())
            {
                try
                {
                    Uri serverUri = new Uri(ServerAddress);
                    await webSocket.ConnectAsync(serverUri, CancellationToken.None);
                    Console.WriteLine("\nConnected to WebSocket server.");

                    string message = "Test message from WebSocket client!";
                    byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                    ArraySegment<byte> segment = new ArraySegment<byte>(messageBytes);

                    await webSocket.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None);

                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client closing", CancellationToken.None);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in WebSocket client: {ex.Message}");
                }
            }
        }
    }
}
