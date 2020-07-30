// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.RGB.BlizzardShader
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
  public class BlizzardShader : ChromaShader
  {
    private readonly Vector4 _backColor;
    private readonly Vector4 _frontColor;

    [RgbProcessor]
    private void ProcessHighDetail(
      RgbDevice device,
      Fragment fragment,
      EffectDetailLevel quality,
      float time)
    {
      if (quality == null)
        time *= 0.25f;
      for (int index = 0; index < fragment.Count; ++index)
      {
        float staticNoise = NoiseHelper.GetStaticNoise(fragment.GetCanvasPositionOfIndex(index) * new Vector2(0.2f, 0.4f) + new Vector2(time * 0.35f, time * -0.35f));
        Vector4 vector4 = Vector4.Lerp(this._backColor, this._frontColor, staticNoise * staticNoise);
        fragment.SetColor(index, vector4);
      }
    }

    public BlizzardShader()
    {
      base.\u002Ector();
    }
  }
}
