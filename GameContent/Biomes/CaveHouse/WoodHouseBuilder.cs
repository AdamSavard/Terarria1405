// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Biomes.CaveHouse.WoodHouseBuilder
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes.CaveHouse
{
  public class WoodHouseBuilder : HouseBuilder
  {
    public WoodHouseBuilder(IEnumerable<Microsoft.Xna.Framework.Rectangle> rooms)
      : base(HouseType.Wood, rooms)
    {
      this.TileType = (ushort) 30;
      this.WallType = (ushort) 27;
      this.BeamType = (ushort) 124;
      this.PlatformStyle = 0;
      this.DoorStyle = 0;
      this.TableStyle = 0;
      this.WorkbenchStyle = 0;
      this.PianoStyle = 0;
      this.BookcaseStyle = 0;
      this.ChairStyle = 0;
      this.ChestStyle = 1;
    }

    protected override void AgeRoom(Microsoft.Xna.Framework.Rectangle room)
    {
      for (int index = 0; index < room.Width * room.Height / 16; ++index)
        WorldUtils.Gen(new Point(WorldGen.genRand.Next(1, room.Width - 1) + room.X, WorldGen.genRand.Next(1, room.Height - 1) + room.Y), (GenShape) new Shapes.Rectangle(2, 2), Actions.Chain((GenAction) new Modifiers.Dither(0.5), (GenAction) new Modifiers.Blotches(2, 2.0), (GenAction) new Modifiers.IsEmpty(), (GenAction) new Actions.SetTile((ushort) 51, true, true)));
      WorldUtils.Gen(new Point(room.X, room.Y), (GenShape) new Shapes.Rectangle(room.Width, room.Height), Actions.Chain((GenAction) new Modifiers.Dither(0.850000023841858), (GenAction) new Modifiers.Blotches(2, 0.3), (GenAction) new Modifiers.OnlyWalls(new ushort[1]
      {
        this.WallType
      }), (GenAction) new Modifiers.SkipTiles(this.SkipTilesDuringWallAging), (double) room.Y > Main.worldSurface ? (GenAction) new Actions.ClearWall(true) : (GenAction) new Actions.PlaceWall((ushort) 2, true)));
      WorldUtils.Gen(new Point(room.X, room.Y), (GenShape) new Shapes.Rectangle(room.Width, room.Height), Actions.Chain((GenAction) new Modifiers.Dither(0.949999988079071), (GenAction) new Modifiers.OnlyTiles(new ushort[3]
      {
        (ushort) 30,
        (ushort) 321,
        (ushort) 158
      }), (GenAction) new Actions.ClearTile(true)));
    }
  }
}
