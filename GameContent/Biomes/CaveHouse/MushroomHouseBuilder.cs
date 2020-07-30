// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Biomes.CaveHouse.MushroomHouseBuilder
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes.CaveHouse
{
  public class MushroomHouseBuilder : HouseBuilder
  {
    public MushroomHouseBuilder(IEnumerable<Microsoft.Xna.Framework.Rectangle> rooms)
      : base(HouseType.Mushroom, rooms)
    {
      this.TileType = (ushort) 190;
      this.WallType = (ushort) 74;
      this.BeamType = (ushort) 578;
      this.PlatformStyle = 18;
      this.DoorStyle = 6;
      this.TableStyle = 27;
      this.WorkbenchStyle = 7;
      this.PianoStyle = 22;
      this.BookcaseStyle = 24;
      this.ChairStyle = 9;
      this.ChestStyle = 32;
    }

    protected override void AgeRoom(Microsoft.Xna.Framework.Rectangle room)
    {
      WorldUtils.Gen(new Point(room.X, room.Y), (GenShape) new Shapes.Rectangle(room.Width, room.Height), Actions.Chain((GenAction) new Modifiers.Dither(0.699999988079071), (GenAction) new Modifiers.Blotches(2, 0.5), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        this.TileType
      }), (GenAction) new Actions.SetTileKeepWall((ushort) 70, true, true)));
      WorldUtils.Gen(new Point(room.X + 1, room.Y), (GenShape) new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain((GenAction) new Modifiers.Dither(0.600000023841858), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        (ushort) 70
      }), (GenAction) new Modifiers.Offset(0, -1), (GenAction) new Modifiers.IsEmpty(), (GenAction) new Actions.SetTile((ushort) 71, false, true)));
      WorldUtils.Gen(new Point(room.X + 1, room.Y + room.Height - 1), (GenShape) new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain((GenAction) new Modifiers.Dither(0.600000023841858), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        (ushort) 70
      }), (GenAction) new Modifiers.Offset(0, -1), (GenAction) new Modifiers.IsEmpty(), (GenAction) new Actions.SetTile((ushort) 71, false, true)));
      WorldUtils.Gen(new Point(room.X, room.Y), (GenShape) new Shapes.Rectangle(room.Width, room.Height), Actions.Chain((GenAction) new Modifiers.Dither(0.850000023841858), (GenAction) new Modifiers.Blotches(2, 0.3), (GenAction) new Actions.ClearWall(false)));
    }
  }
}
