﻿// Decompiled with JetBrains decompiler
// Type: Terraria.UI.StyleDimension
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

namespace Terraria.UI
{
  public struct StyleDimension
  {
    public static StyleDimension Fill = new StyleDimension(0.0f, 1f);
    public static StyleDimension Empty = new StyleDimension(0.0f, 0.0f);
    public float Pixels;
    public float Precent;

    public StyleDimension(float pixels, float precent)
    {
      this.Pixels = pixels;
      this.Precent = precent;
    }

    public void Set(float pixels, float precent)
    {
      this.Pixels = pixels;
      this.Precent = precent;
    }

    public float GetValue(float containerSize)
    {
      return this.Pixels + this.Precent * containerSize;
    }

    public static StyleDimension FromPixels(float pixels)
    {
      return new StyleDimension(pixels, 0.0f);
    }

    public static StyleDimension FromPercent(float percent)
    {
      return new StyleDimension(0.0f, percent);
    }

    public static StyleDimension FromPixelsAndPercent(float pixels, float percent)
    {
      return new StyleDimension(pixels, percent);
    }
  }
}
