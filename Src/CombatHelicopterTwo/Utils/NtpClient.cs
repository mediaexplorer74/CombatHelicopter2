// Modified by MediaExplorer (2026)
// Type: Helicopter.Utils.NtpClient
// Assembly: Combat Helicopter 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2424C8FD-D17D-4821-8CD9-AC9139939D33
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Combat Helicopter 2.dll

using System;
using System.Net;
using System.Net.Sockets;

#nullable disable
namespace Helicopter.Utils
{
  public class NtpClient
  {
    private readonly string _serverName;
    public string[] NtpServerList = new string[7]
    {
      "pool.ntp.org ",
      "asia.pool.ntp.org",
      "europe.pool.ntp.org",
      "north-america.pool.ntp.org",
      "oceania.pool.ntp.org",
      "south-america.pool.ntp.org",
      "time-a.nist.gov"
    };
    private Socket _socket;

    public event EventHandler<NtpClient.TimeReceivedEventArgs> TimeReceived;

    public event EventHandler<EventArgs> Error;

    public NtpClient(string serverName) => this._serverName = serverName;

    public NtpClient()
      : this("time-a.nist.gov")
    {
    }

    protected void OnTimeReceived(DateTime time)
    {
      if (this.TimeReceived == null)
        return;
      this.TimeReceived((object) this, new NtpClient.TimeReceivedEventArgs()
      {
        CurrentTime = time
      });
    }

    protected void OnError()
    {
      if (this.Error == null)
        return;
      this.Error((object) this, EventArgs.Empty);
    }

    public void RequestTime()
    {
      byte[] buffer = new byte[48];
      buffer[0] = (byte) 27;
      for (int index = 1; index < buffer.Length; ++index)
        buffer[index] = (byte) 0;
      DnsEndPoint endPoint = new DnsEndPoint(this._serverName, 123);
      this._socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
      SocketAsyncEventArgs e1 = new SocketAsyncEventArgs()
      {
        RemoteEndPoint = (EndPoint) endPoint
      };
      e1.Completed += (EventHandler<SocketAsyncEventArgs>) ((o, e) =>
      {
        if (e.SocketError == SocketError.Success)
        {
          SocketAsyncEventArgs e2 = new SocketAsyncEventArgs()
          {
            RemoteEndPoint = (EndPoint) endPoint
          };
          e2.Completed += new EventHandler<SocketAsyncEventArgs>(this.sArgs_Completed);
          e2.SetBuffer(buffer, 0, buffer.Length);
          e2.UserToken = (object) buffer;
          this._socket.SendAsync(e2);
        }
        else
          this.OnError();
      });
      this._socket.ConnectAsync(e1);
    }

    private void sArgs_Completed(object sender, SocketAsyncEventArgs e)
    {
      if (e.SocketError == SocketError.Success)
      {
        byte[] buffer = e.Buffer;
        SocketAsyncEventArgs e1 = new SocketAsyncEventArgs();
        e1.RemoteEndPoint = e.RemoteEndPoint;
        e1.SetBuffer(buffer, 0, buffer.Length);
        e1.Completed += (EventHandler<SocketAsyncEventArgs>) ((o, a) =>
        {
          if (a.SocketError != SocketError.Success)
            return;
          ulong num1 = 0;
          ulong num2 = 0;
          for (int index = 40; index <= 43; ++index)
            num1 = num1 << 8 | (ulong) buffer[index];
          for (int index = 44; index <= 47; ++index)
            num2 = num2 << 8 | (ulong) buffer[index];
          this.OnTimeReceived(new DateTime(1900, 1, 1) + TimeSpan.FromTicks((long) (num1 * 1000UL + num2 * 1000UL / 4294967296UL) * 10000L));
        });
        this._socket.ReceiveAsync(e1);
      }
      else
        this.OnError();
    }

    public class TimeReceivedEventArgs : EventArgs
    {
      public DateTime CurrentTime { get; internal set; }
    }
  }
}
