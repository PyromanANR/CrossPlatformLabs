using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProxyServer_WiFi_GSM_Bluetooth
{
    public class WebSocketListener
    {
        private readonly HttpListener _httpListener;

        public WebSocketListener(int port)
        {
            _httpListener = new HttpListener();
            _httpListener.Prefixes.Add($"http://127.0.0.2:{port}/");
        }

        public void Start()
        {
            _httpListener.Start();
        }

        public async Task<WebSocket> AcceptWebSocketAsync()
        {
            var context = await _httpListener.GetContextAsync();
            if (context.Request.IsWebSocketRequest)
            {
                var webSocketContext = await context.AcceptWebSocketAsync(null);
                return webSocketContext.WebSocket;
            }
            else
            {
                context.Response.StatusCode = 400;
                context.Response.Close();
                throw new Exception("Not a WebSocket request.");
            }
        }
    }
}
