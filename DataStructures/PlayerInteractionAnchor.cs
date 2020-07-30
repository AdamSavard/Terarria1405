// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.PlayerInteractionAnchor
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

namespace Terraria.DataStructures
{
  public struct PlayerInteractionAnchor
  {
    public int interactEntityID;
    public int X;
    public int Y;

    public PlayerInteractionAnchor(int entityID, int x = -1, int y = -1)
    {
      this.interactEntityID = entityID;
      this.X = x;
      this.Y = y;
    }

    public bool InUse
    {
      get
      {
        return this.interactEntityID != -1;
      }
    }

    public void Clear()
    {
      this.interactEntityID = -1;
      this.X = -1;
      this.Y = -1;
    }

    public void Set(int entityID, int x, int y)
    {
      this.interactEntityID = entityID;
      this.X = x;
      this.Y = y;
    }

    public bool IsInValidUseTileEntity()
    {
      return this.InUse && TileEntity.ByID.ContainsKey(this.interactEntityID);
    }

    public TileEntity GetTileEntity()
    {
      return !this.IsInValidUseTileEntity() ? (TileEntity) null : TileEntity.ByID[this.interactEntityID];
    }
  }
}
