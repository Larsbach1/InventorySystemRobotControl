// Robot.cs
using System.Globalization;
using System.Net.Sockets;
using System.Text;

namespace InventorySystemRobotControl;

public sealed class Robot
{
    private readonly string _host;
    private const int DashboardPort = 29999; // brake release, etc.
    private const int UrScriptPort  = 30002; // script socket

    public Robot(string host = "localhost")
    {
        _host = host;
    }

    // ✅ Ny signatur som din Program.cs kalder
    public void SendProgram(string program, uint item_id)
    {
        // (item_id kan bruges til logging/mapping – vi nøjes med at logge det)
        Console.WriteLine($"[Robot] Sending script for item_id={item_id}");

        // Sørg for '.' som decimalseparator
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

        void Send(string host, int port, string msg)
        {
            using var client = new TcpClient(host, port);
            using var stream = client.GetStream();
            var bytes = Encoding.ASCII.GetBytes(msg);
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
        }

        // Slip bremser og send script
        Send(_host, DashboardPort, "brake release\n");
        Send(_host, UrScriptPort, program);
        Console.WriteLine("✅ Program sent to robot.");
    }

    // ✅ Behold evt. den gamle signatur – forwarder til den nye
    public void SendProgram(string program) => SendProgram(program, 0);
}