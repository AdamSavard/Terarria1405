// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.NetModules.NetCreativePowersModule
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using System.IO;
using Terraria.GameContent.Creative;
using Terraria.Net;

namespace Terraria.GameContent.NetModules
{
  public class NetCreativePowersModule : NetModule
  {
    public static NetPacket PreparePacket(
      ushort powerId,
      int specificInfoBytesInPacketCount)
    {
      NetPacket packet = NetModule.CreatePacket<NetCreativePowersModule>(specificInfoBytesInPacketCount + 2);
      packet.Writer.Write(powerId);
      return packet;
    }

    public override bool Deserialize(BinaryReader reader, int userId)
    {
      ushort id = reader.ReadUInt16();
      ICreativePower power;
      if (!CreativePowerManager.Instance.TryGetPower(id, out power))
        return false;
      power.DeserializeNetMessage(reader, userId);
      return true;
    }
  }
}
