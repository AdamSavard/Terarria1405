// Decompiled with JetBrains decompiler
// Type: Terraria.Net.TcpAddress
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using System.Net;

namespace Terraria.Net
{
  public class TcpAddress : RemoteAddress
  {
    public IPAddress Address;
    public int Port;

    public TcpAddress(IPAddress address, int port)
    {
      this.Type = AddressType.Tcp;
      this.Address = address;
      this.Port = port;
    }

    public override string GetIdentifier()
    {
      return this.Address.ToString();
    }

    public override bool IsLocalHost()
    {
      return this.Address.Equals((object) IPAddress.Loopback);
    }

    public override string ToString()
    {
      return new IPEndPoint(this.Address, this.Port).ToString();
    }

    public override string GetFriendlyName()
    {
      return this.ToString();
    }
  }
}
