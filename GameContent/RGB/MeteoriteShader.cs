// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.RGB.MeteoriteShader
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;
using System;

namespace Terraria.GameContent.RGB
{
  public class MeteoriteShader : ChromaShader
  {
    private readonly Vector4 _baseColor;
    private readonly Vector4 _secondaryColor;
    private readonly Vector4 _glowColor;

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
        Vector4 vector4 = Vector4.Lerp(this._baseColor, this._secondaryColor, (float) (Math.Sin((double) time + (double) canvasPositionOfIndex.X) * 0.5 + 0.5));
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
        Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(index);
        Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(index);
        Vector4 baseColor = this._baseColor;
        float dynamicNoise = NoiseHelper.GetDynamicNoise(gridPositionOfIndex.X, gridPositionOfIndex.Y, time / 10f);
        Vector4 vector4 = Vector4.Lerp(Vector4.Lerp(baseColor, this._secondaryColor, dynamicNoise * dynamicNoise), this._glowColor, (float) Math.Sqrt((double) Math.Max(0.0f, (float) (1.0 - (double) NoiseHelper.GetDynamicNoise(canvasPositionOfIndex * 0.5f + new Vector2(0.0f, time * 0.05f), time / 20f) * 2.0))) * 0.75f);
        fragment.SetColor(index, vector4);
      }
    }

    public MeteoriteShader() : base()
{
    }
  }
}
