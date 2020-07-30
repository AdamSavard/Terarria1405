// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.TileEntitiesManager
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using System.Collections.Generic;
using Terraria.GameContent.Tile_Entities;

namespace Terraria.DataStructures
{
  public class TileEntitiesManager
  {
    private Dictionary<int, TileEntity> _types = new Dictionary<int, TileEntity>();
    private int _nextEntityID;

    private int AssignNewID()
    {
      return this._nextEntityID++;
    }

    private bool InvalidEntityID(int id)
    {
      return id < 0 || id >= this._nextEntityID;
    }

    public void RegisterAll()
    {
      this.Register((TileEntity) new TETrainingDummy());
      this.Register((TileEntity) new TEItemFrame());
      this.Register((TileEntity) new TELogicSensor());
      this.Register((TileEntity) new TEDisplayDoll());
      this.Register((TileEntity) new TEWeaponsRack());
      this.Register((TileEntity) new TEHatRack());
      this.Register((TileEntity) new TEFoodPlatter());
      this.Register((TileEntity) new TETeleportationPylon());
    }

    public void Register(TileEntity entity)
    {
      int assignedID = this.AssignNewID();
      this._types[assignedID] = entity;
      entity.RegisterTileEntityID(assignedID);
    }

    public bool CheckValidTile(int id, int x, int y)
    {
      return !this.InvalidEntityID(id) && this._types[id].IsTileValidForEntity(x, y);
    }

    public void NetPlaceEntity(int id, int x, int y)
    {
      if (this.InvalidEntityID(id) || !this._types[id].IsTileValidForEntity(x, y))
        return;
      this._types[id].NetPlaceEntityAttempt(x, y);
    }

    public TileEntity GenerateInstance(int id)
    {
      return this.InvalidEntityID(id) ? (TileEntity) null : this._types[id].GenerateInstance();
    }
  }
}
