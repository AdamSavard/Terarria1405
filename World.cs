// Decompiled with JetBrains decompiler
// Type: Terraria.World
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

namespace Terraria
{
  public class World
  {
    public Tile[,] Tiles
    {
      get
      {
        return Main.tile;
      }
    }

    public int TileColumns
    {
      get
      {
        return Main.maxTilesX;
      }
    }

    public int TileRows
    {
      get
      {
        return Main.maxTilesY;
      }
    }

    public Player[] Players
    {
      get
      {
        return Main.player;
      }
    }
  }
}
