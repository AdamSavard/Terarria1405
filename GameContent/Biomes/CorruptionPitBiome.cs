﻿// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Biomes.CorruptionPitBiome
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes
{
  public class CorruptionPitBiome : MicroBiome
  {
    public static bool[] ValidTiles = TileID.Sets.Factory.CreateBoolSet(true, 21, 31, 26);

    public override bool Place(Point origin, StructureMap structures)
    {
      if (WorldGen.SolidTile(origin.X, origin.Y, false) && GenBase._tiles[origin.X, origin.Y].wall == (ushort) 3)
        return false;
      if (!WorldUtils.Find(origin, Searches.Chain((GenSearch) new Searches.Down(100), (GenCondition) new Conditions.IsSolid()), out origin))
        return false;
      if (!WorldUtils.Find(new Point(origin.X - 4, origin.Y), Searches.Chain((GenSearch) new Searches.Down(5), new Conditions.IsTile(new ushort[1]
      {
        (ushort) 25
      }).AreaAnd(8, 1)), out Point _))
        return false;
      ShapeData data1 = new ShapeData();
      ShapeData shapeData1 = new ShapeData();
      ShapeData shapeData2 = new ShapeData();
      for (int index = 0; index < 6; ++index)
        WorldUtils.Gen(origin, (GenShape) new Shapes.Circle(GenBase._random.Next(10, 12) + index), Actions.Chain((GenAction) new Modifiers.Offset(0, 5 * index + 5), new Modifiers.Blotches(3, 0.3).Output(data1)));
      for (int index = 0; index < 6; ++index)
        WorldUtils.Gen(origin, (GenShape) new Shapes.Circle(GenBase._random.Next(5, 7) + index), Actions.Chain((GenAction) new Modifiers.Offset(0, 2 * index + 18), new Modifiers.Blotches(3, 0.3).Output(shapeData1)));
      for (int index = 0; index < 6; ++index)
        WorldUtils.Gen(origin, (GenShape) new Shapes.Circle(GenBase._random.Next(4, 6) + index / 2), Actions.Chain((GenAction) new Modifiers.Offset(0, (int) (7.5 * (double) index) - 10), new Modifiers.Blotches(3, 0.3).Output(shapeData2)));
      ShapeData data2 = new ShapeData(shapeData1);
      shapeData1.Subtract(shapeData2, origin, origin);
      data2.Subtract(shapeData1, origin, origin);
      Microsoft.Xna.Framework.Rectangle bounds = ShapeData.GetBounds(origin, data1, shapeData2);
      if (!structures.CanPlace(bounds, CorruptionPitBiome.ValidTiles, 2))
        return false;
      WorldUtils.Gen(origin, (GenShape) new ModShapes.All(data1), Actions.Chain((GenAction) new Actions.SetTile((ushort) 25, true, true), (GenAction) new Actions.PlaceWall((ushort) 3, true)));
      WorldUtils.Gen(origin, (GenShape) new ModShapes.All(shapeData1), (GenAction) new Actions.SetTile((ushort) 0, true, true));
      WorldUtils.Gen(origin, (GenShape) new ModShapes.All(shapeData2), (GenAction) new Actions.ClearTile(true));
      WorldUtils.Gen(origin, (GenShape) new ModShapes.All(shapeData1), Actions.Chain((GenAction) new Modifiers.IsTouchingAir(true), (GenAction) new Modifiers.NotTouching(false, new ushort[1]
      {
        (ushort) 25
      }), (GenAction) new Actions.SetTile((ushort) 23, true, true)));
      WorldUtils.Gen(origin, (GenShape) new ModShapes.All(data2), (GenAction) new Actions.PlaceWall((ushort) 69, true));
      structures.AddProtectedStructure(bounds, 2);
      return true;
    }
  }
}
