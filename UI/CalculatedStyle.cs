﻿// Decompiled with JetBrains decompiler
// Type: Terraria.UI.CalculatedStyle
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;

namespace Terraria.UI
{
  public struct CalculatedStyle
  {
    public float X;
    public float Y;
    public float Width;
    public float Height;

    public CalculatedStyle(float x, float y, float width, float height)
    {
      this.X = x;
      this.Y = y;
      this.Width = width;
      this.Height = height;
    }

    public Rectangle ToRectangle()
    {
      return new Rectangle((int) this.X, (int) this.Y, (int) this.Width, (int) this.Height);
    }

    public Vector2 Position()
    {
      return new Vector2(this.X, this.Y);
    }

    public Vector2 Center()
    {
      return new Vector2(this.X + this.Width * 0.5f, this.Y + this.Height * 0.5f);
    }
  }
}
