// Decompiled with JetBrains decompiler
// Type: Terraria.Net.NetModule
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using System.IO;

namespace Terraria.Net
{
  public abstract class NetModule
  {
    public abstract bool Deserialize(BinaryReader reader, int userId);

    protected static NetPacket CreatePacket<T>(int maxSize) where T : NetModule
    {
      return new NetPacket(NetManager.Instance.GetId<T>(), maxSize);
    }
  }
}
