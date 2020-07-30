// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.LineSegment
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
  public struct LineSegment
  {
    public Vector2 Start;
    public Vector2 End;

    public LineSegment(Vector2 start, Vector2 end)
    {
      this.Start = start;
      this.End = end;
    }
  }
}
