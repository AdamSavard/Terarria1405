﻿// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.RGB.UndergroundHallowShader
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;
using System;

namespace Terraria.GameContent.RGB
{
  public class UndergroundHallowShader : ChromaShader
  {
    private readonly Vector4 _baseColor;
    private readonly Vector4 _pinkCrystalColor;
    private readonly Vector4 _blueCrystalColor;

    [RgbProcessor]
    private void ProcessLowDetail(
      RgbDevice device,
      Fragment fragment,
      EffectDetailLevel quality,
      float time)
    {
      for (int index = 0; index < fragment.Count; ++index)
      {
        Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(index);
        Vector4 vector4 = Vector4.Lerp(this._pinkCrystalColor, this._blueCrystalColor, (float) (Math.Sin((double) time * 2.0 + (double) canvasPositionOfIndex.X) * 0.5 + 0.5));
        fragment.SetColor(index, vector4);
      }
    }

    [RgbProcessor]
    private void ProcessHighDetail(
      RgbDevice device,
      Fragment fragment,
      EffectDetailLevel quality,
      float time)
    {
      for (int index = 0; index < fragment.Count; ++index)
      {
        fragment.GetGridPositionOfIndex(index);
        Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(index);
        Vector4 baseColor = this._baseColor;
        float amount1 = Math.Max(0.0f, (float) (1.0 - 2.5 * (double) NoiseHelper.GetDynamicNoise(canvasPositionOfIndex * 0.4f, time * 0.05f)));
        float amount2 = Math.Max(0.0f, (float) (1.0 - 2.5 * (double) NoiseHelper.GetDynamicNoise(canvasPositionOfIndex * 0.4f + new Vector2(0.05f, 0.0f), time * 0.05f)));
        Vector4 vector4 = (double) amount1 <= (double) amount2 ? Vector4.Lerp(baseColor, this._blueCrystalColor, amount2) : Vector4.Lerp(baseColor, this._pinkCrystalColor, amount1);
        fragment.SetColor(index, vector4);
      }
    }

    public UndergroundHallowShader()
    {
      base.\u002Ector();
    }
  }
}
