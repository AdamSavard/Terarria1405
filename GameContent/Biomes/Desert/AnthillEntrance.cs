// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Biomes.Desert.AnthillEntrance
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes.Desert
{
  public static class AnthillEntrance
  {
    public static void Place(DesertDescription description)
    {
      int num = WorldGen.genRand.Next(2, 4);
      for (int index = 0; index < num; ++index)
      {
        int holeRadius = WorldGen.genRand.Next(15, 18);
        int x = (int) ((double) (index + 1) / (double) (num + 1) * (double) description.Surface.Width) + description.Desert.Left;
        int y = (int) description.Surface[x];
        AnthillEntrance.PlaceAt(description, new Point(x, y), holeRadius);
      }
    }

    private static void PlaceAt(DesertDescription description, Point position, int holeRadius)
    {
      ShapeData data = new ShapeData();
      Point origin = new Point(position.X, position.Y + 6);
      WorldUtils.Gen(origin, (GenShape) new Shapes.Tail((float) (holeRadius * 2), new Vector2(0.0f, (float) -holeRadius * 1.5f)), Actions.Chain(new Actions.SetTile((ushort) 53, false, true).Output(data)));
      GenShapeActionPair pair1 = new GenShapeActionPair((GenShape) new Shapes.Rectangle(1, 1), Actions.Chain((GenAction) new Modifiers.Blotches(2, 0.3), (GenAction) new Modifiers.IsSolid(), (GenAction) new Actions.Clear(), (GenAction) new Actions.PlaceWall((ushort) 187, true)));
      GenShapeActionPair genShapeActionPair = new GenShapeActionPair((GenShape) new Shapes.Rectangle(1, 1), Actions.Chain((GenAction) new Modifiers.IsSolid(), (GenAction) new Actions.Clear(), (GenAction) new Actions.PlaceWall((ushort) 187, true)));
      GenShapeActionPair pair2 = new GenShapeActionPair((GenShape) new Shapes.Circle(2, 3), Actions.Chain((GenAction) new Modifiers.IsSolid(), (GenAction) new Actions.SetTile((ushort) 397, false, true), (GenAction) new Actions.PlaceWall((ushort) 187, true)));
      GenShapeActionPair pair3 = new GenShapeActionPair((GenShape) new Shapes.Circle(holeRadius, 3), Actions.Chain((GenAction) new Modifiers.SkipWalls(new ushort[1]
      {
        (ushort) 187
      }), (GenAction) new Actions.SetTile((ushort) 53, false, true)));
      GenShapeActionPair pair4 = new GenShapeActionPair((GenShape) new Shapes.Circle(holeRadius - 2, 3), Actions.Chain((GenAction) new Actions.PlaceWall((ushort) 187, true)));
      int x = position.X;
      int y1 = position.Y - holeRadius - 3;
      while (true)
      {
        int num1 = y1;
        Microsoft.Xna.Framework.Rectangle rectangle = description.Hive;
        int top1 = rectangle.Top;
        int y2 = position.Y;
        rectangle = description.Desert;
        int top2 = rectangle.Top;
        int num2 = (y2 - top2) * 2;
        int num3 = top1 + num2 + 12;
        if (num1 < num3)
        {
          WorldUtils.Gen(new Point(x, y1), y1 < position.Y ? genShapeActionPair : pair1);
          WorldUtils.Gen(new Point(x, y1), pair2);
          if (y1 % 3 == 0 && y1 >= position.Y)
          {
            x += WorldGen.genRand.Next(-1, 2);
            WorldUtils.Gen(new Point(x, y1), pair1);
            if (y1 >= position.Y + 5)
            {
              WorldUtils.Gen(new Point(x, y1), pair3);
              WorldUtils.Gen(new Point(x, y1), pair4);
            }
            WorldUtils.Gen(new Point(x, y1), pair2);
          }
          ++y1;
        }
        else
          break;
      }
      WorldUtils.Gen(new Point(origin.X, origin.Y - (int) ((double) holeRadius * 1.5) + 3), (GenShape) new Shapes.Circle(holeRadius / 2, holeRadius / 3), Actions.Chain(Actions.Chain((GenAction) new Actions.ClearTile(false), (GenAction) new Modifiers.Expand(1), (GenAction) new Actions.PlaceWall((ushort) 0, true))));
      WorldUtils.Gen(origin, (GenShape) new ModShapes.All(data), (GenAction) new Actions.Smooth(false));
    }
  }
}
