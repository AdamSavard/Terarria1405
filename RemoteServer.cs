// Decompiled with JetBrains decompiler
// Type: Terraria.RemoteServer
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using System;
using System.IO;
using Terraria.Localization;
using Terraria.Net.Sockets;

namespace Terraria
{
  public class RemoteServer
  {
    public ISocket Socket = (ISocket) new TcpSocket();
    public bool IsActive;
    public int State;
    public int TimeOutTimer;
    public bool IsReading;
    public byte[] ReadBuffer;
    public string StatusText;
    public int StatusCount;
    public int StatusMax;
    public BitsByte StatusTextFlags;

    public bool HideStatusTextPercent
    {
      get
      {
        return this.StatusTextFlags[0];
      }
    }

    public bool StatusTextHasShadows
    {
      get
      {
        return this.StatusTextFlags[1];
      }
    }

    public void ClientWriteCallBack(object state)
    {
      --NetMessage.buffer[256].spamCount;
    }

    public void ClientReadCallBack(object state, int length)
    {
      try
      {
        if (!Netplay.Disconnect)
        {
          int streamLength = length;
          if (streamLength == 0)
          {
            Netplay.Disconnect = true;
            Main.statusText = Language.GetTextValue("Net.LostConnection");
          }
          else if (Main.ignoreErrors)
          {
            try
            {
              NetMessage.ReceiveBytes(this.ReadBuffer, streamLength, 256);
            }
            catch
            {
            }
          }
          else
            NetMessage.ReceiveBytes(this.ReadBuffer, streamLength, 256);
        }
        this.IsReading = false;
      }
      catch (Exception ex)
      {
        try
        {
          using (StreamWriter streamWriter = new StreamWriter("client-crashlog.txt", true))
          {
            streamWriter.WriteLine((object) DateTime.Now);
            streamWriter.WriteLine((object) ex);
            streamWriter.WriteLine("");
          }
        }
        catch
        {
        }
        Netplay.Disconnect = true;
      }
    }
  }
}
