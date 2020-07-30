// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.AnchorData
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Terraria.Enums;

namespace Terraria.DataStructures
{
  public struct AnchorData
  {
    public AnchorType type;
    public int tileCount;
    public int checkStart;
    public static AnchorData Empty;

    public AnchorData(AnchorType type, int count, int start)
    {
      this.type = type;
      this.tileCount = count;
      this.checkStart = start;
    }

    public static bool operator ==(AnchorData data1, AnchorData data2)
    {
      return data1.type == data2.type && data1.tileCount == data2.tileCount && data1.checkStart == data2.checkStart;
    }

    public static bool operator !=(AnchorData data1, AnchorData data2)
    {
      return data1.type != data2.type || data1.tileCount != data2.tileCount || data1.checkStart != data2.checkStart;
    }

    public override bool Equals(object obj)
    {
      return obj is AnchorData anchorData && this.type == anchorData.type && this.tileCount == ((AnchorData) obj).tileCount && this.checkStart == ((AnchorData) obj).checkStart;
    }

    public override int GetHashCode()
    {
      return (int) (ushort) this.type << 16 | (int) (byte) this.tileCount << 8 | (int) (byte) this.checkStart;
    }
  }
}
