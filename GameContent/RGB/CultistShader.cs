﻿// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.RGB.CultistShader
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;
using System;

namespace Terraria.GameContent.RGB
{
  public class CultistShader : ChromaShader
  {
    private readonly Vector4 _lightningDarkColor;
    private readonly Vector4 _lightningBrightColor;
    private readonly Vector4 _fireDarkColor;
    private readonly Vector4 _fireBrightColor;
    private readonly Vector4 _iceDarkColor;
    private readonly Vector4 _iceBrightColor;
    private readonly Vector4 _backgroundColor;

    [RgbProcessor]
    private void ProcessHighDetail(
      RgbDevice device,
      Fragment fragment,
      EffectDetailLevel quality,
      float time)
    {
      time *= 2f;
      for (int index = 0; index < fragment.Count; ++index)
      {
        Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(index);
        Vector4 backgroundColor = this._backgroundColor;
        float num1 = time * 0.5f + canvasPositionOfIndex.X + canvasPositionOfIndex.Y;
        float amount = MathHelper.Clamp((float) (Math.Cos((double) num1) * 2.0 + 2.0), 0.0f, 1f);
        float num2 = (float) (((double) num1 + 3.14159274101257) % 18.8495559692383);
        Vector4 vector4_1;
        if ((double) num2 < 6.28318548202515)
        {
          float staticNoise = NoiseHelper.GetStaticNoise(canvasPositionOfIndex * 0.3f + new Vector2(12.5f, time * 0.2f));
          vector4_1 = Vector4.Lerp(this._fireDarkColor, this._fireBrightColor, MathHelper.Clamp(Math.Max(0.0f, (float) (1.0 - (double) staticNoise * (double) staticNoise * 4.0 * (double) staticNoise)), 0.0f, 1f));
        }
        else
          vector4_1 = (double) num2 >= 12.5663709640503 ? Vector4.Lerp(this._lightningDarkColor, this._lightningBrightColor, Math.Max(0.0f, (float) (1.0 - 5.0 * (Math.Sin((double) NoiseHelper.GetDynamicNoise(canvasPositionOfIndex * 0.15f, time * 0.05f) * 15.0) * 0.5 + 0.5)))) : Vector4.Lerp(this._iceDarkColor, this._iceBrightColor, Math.Max(0.0f, (float) (1.0 - (double) NoiseHelper.GetDynamicNoise(new Vector2((float) (((double) canvasPositionOfIndex.X + (double) canvasPositionOfIndex.Y) * 0.200000002980232), 0.0f), time / 5f) * 1.5)));
        Vector4 vector4_2 = Vector4.Lerp(backgroundColor, vector4_1, amount);
        fragment.SetColor(index, vector4_2);
      }
    }

    public CultistShader()
    {
      base.\u002Ector();
    }
  }
}
