using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using InTheHand.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ProxyServer_WiFi_WebSocket_Bluetooth
{
    public class BluetoothTestClient
    {
        private static readonly BluetoothAddress ServerAddress = BluetoothAddress.Parse("70:5f:a3:da:3f:5c");

        public void Send()
        {
            try
            {
                BluetoothClient bluetoothClient = new BluetoothClient();
                bluetoothClient.Connect(ServerAddress, BluetoothService.SerialPort);

                using (NetworkStream stream = bluetoothClient.GetStream())
                {
                    string message = "Test message from Bluetooth client!";
                    byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                    stream.Write(messageBytes, 0, messageBytes.Length);
                }

                bluetoothClient.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Bluetooth client: {ex.Message}");
            }
        }
    }
}
