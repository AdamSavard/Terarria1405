// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.PortableStoolUsage
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

namespace Terraria.DataStructures
{
  public struct PortableStoolUsage
  {
    public bool HasAStool;
    public bool IsInUse;
    public int HeightBoost;
    public int VisualYOffset;
    public int MapYOffset;

    public void Reset()
    {
      this.HasAStool = false;
      this.IsInUse = false;
      this.HeightBoost = 0;
      this.VisualYOffset = 0;
      this.MapYOffset = 0;
    }

    public void SetStats(int heightBoost, int visualYOffset, int mapYOffset)
    {
      this.HasAStool = true;
      this.HeightBoost = heightBoost;
      this.VisualYOffset = visualYOffset;
      this.MapYOffset = mapYOffset;
    }
  }
}
