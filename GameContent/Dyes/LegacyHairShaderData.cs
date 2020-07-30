﻿// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Dyes.LegacyHairShaderData
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Dyes
{
  public class LegacyHairShaderData : HairShaderData
  {
    private LegacyHairShaderData.ColorProcessingMethod _colorProcessor;

    public LegacyHairShaderData()
      : base((Ref<Effect>) null, (string) null)
    {
      this._shaderDisabled = true;
    }

    public override Color GetColor(Player player, Color lightColor)
    {
      bool lighting = true;
      Color color = this._colorProcessor(player, player.hairColor, ref lighting);
      return lighting ? new Color(color.ToVector4() * lightColor.ToVector4()) : color;
    }

    public LegacyHairShaderData UseLegacyMethod(
      LegacyHairShaderData.ColorProcessingMethod colorProcessor)
    {
      this._colorProcessor = colorProcessor;
      return this;
    }

    public delegate Color ColorProcessingMethod(Player player, Color color, ref bool lighting);
  }
}
