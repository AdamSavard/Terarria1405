// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.RGB.JungleShader
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;
using System;

namespace Terraria.GameContent.RGB
{
  public class JungleShader : ChromaShader
  {
    private readonly Vector4 _backgroundColor;
    private readonly Vector4 _sporeColor;
    private readonly Vector4[] _flowerColors;

    [RgbProcessor]
    private void ProcessLowDetail(
      RgbDevice device,
      Fragment fragment,
      EffectDetailLevel quality,
      float time)
    {
      for (int index = 0; index < fragment.Count; ++index)
      {
        Vector4 vector4 = Vector4.Lerp(this._backgroundColor, this._sporeColor, NoiseHelper.GetDynamicNoise(fragment.GetCanvasPositionOfIndex(index) * 0.3f, time / 5f));
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
      bool flag = device.Type == null || device.Type == 6;
      for (int index = 0; index < fragment.Count; ++index)
      {
        Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(index);
        Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(index);
        Vector4 vector4 = Vector4.Lerp(this._backgroundColor, this._sporeColor, Math.Max(0.0f, (float) (1.0 - (double) NoiseHelper.GetDynamicNoise(canvasPositionOfIndex * 0.3f, time / 5f) * 2.5)));
        if (flag)
        {
          float amount = Math.Max(0.0f, (float) (1.0 - (double) NoiseHelper.GetDynamicNoise(gridPositionOfIndex.X, gridPositionOfIndex.Y, time / 100f) * 20.0));
          vector4 = Vector4.Lerp(vector4, this._flowerColors[((gridPositionOfIndex.Y * 47 + gridPositionOfIndex.X) % 5 + 5) % 5], amount);
        }
        fragment.SetColor(index, vector4);
      }
    }

    public JungleShader() : base()
{
    }
  }
}
