using System;
using System.Net;
using System.Net.Sockets;
using System.IO.Ports;
using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.WebSockets;
using System.Threading;
using ProxyServer_WiFi_WebSocket_Bluetooth;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace ProxyServer_WiFi_GSM_Bluetooth;

public class ProxyServer
{
    private const string ServerUrl = "https://192.168.56.10:5074/api/LAB";
    private const string ApiKey = "51e0ec98-ef1c-40be-b73c-7eb22de45a93";
    private TcpListener _wifiListener;
    private WebSocketListener _webSocketListener; 
    private BluetoothListener _bluetoothListener;
    private readonly HttpClient _httpClient;

    public ProxyServer(int wifiPort, string webSocketPort)
    {
        _wifiListener = new TcpListener(IPAddress.Any, wifiPort);

        _webSocketListener = new WebSocketListener(Convert.ToInt32(webSocketPort));

        _bluetoothListener = new BluetoothListener(BluetoothService.SerialPort);

        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
        };

        _httpClient = new HttpClient(handler);

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("API-KEY", ApiKey);
        _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "ProxyServer-Client");

    }

    public async Task Start()
    {
        _wifiListener.Start();
        Console.WriteLine("Wi-Fi server started...");

        _webSocketListener.Start();
        Console.WriteLine("WebSocket server started...");

        _bluetoothListener.Start();
        Console.WriteLine("Bluetooth server started...");

        while (true)
        {
            var wifiClient = await _wifiListener.AcceptTcpClientAsync();
            Task.Run(() => HandleWifiClient(wifiClient));

            BluetoothClient bluetoothClient = _bluetoothListener.AcceptBluetoothClient();
            Task.Run(() => HandleBluetoothClient(bluetoothClient));

            WebSocket webSocketClient = await _webSocketListener.AcceptWebSocketAsync();
            Task.Run(() => HandleWebSocketClient(webSocketClient));
        }
    }

    private async Task HandleWifiClient(TcpClient client)
    {
        var stream = client.GetStream();
        byte[] buffer = new byte[256];
        int bytesRead;

        while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
        {
            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"\nReceived data from Wi-Fi: {message}");
            await SendDataToServer(message);
        }
    }

    private async Task HandleWebSocketClient(WebSocket client)
    {
        byte[] buffer = new byte[256];
        WebSocketReceiveResult result;

        while ((result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None)).MessageType != WebSocketMessageType.Close)
        {
            string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
            Console.WriteLine($"Received data from WebSocket: {message}");
            await SendDataToServer(message);
        }
    }

    private async Task HandleBluetoothClient(BluetoothClient client)
    {
        var stream = client.GetStream();
        byte[] buffer = new byte[256];
        int bytesRead;

        while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
        {
            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"\nReceived data from Bluetooth: {message}");
            await SendDataToServer(message);
        }
    }

    private async Task SendDataToServer(string data)
    {

        var jsonData = new
        {
            data = data
        };

        var json = JsonConvert.SerializeObject(jsonData);

        try
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("API-KEY", "51e0ec98-ef1c-40be-b73c-7eb22de45a93");

            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
            var response = await _httpClient.PostAsync(ServerUrl, content, cts.Token);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Data sent to the server.");
            }
            else
            {
                Console.WriteLine($"Error sending to server: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending to server: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
        }
    }


    public static void Main(string[] args)
    {
        var proxyServer = new ProxyServer(12345, "8181");
        Task.Run(async () => await proxyServer.Start());
        Task.Run(async () =>
        {
            WifiTestClient wifiTestClient = new WifiTestClient();
            wifiTestClient.Send();

            BluetoothTestClient bluetoothTestClient = new BluetoothTestClient();
            bluetoothTestClient.Send();

            WebSocketTestClient webSocketTestClient = new WebSocketTestClient();
            webSocketTestClient.Send();

        });


        Console.ReadLine();
    }
}
