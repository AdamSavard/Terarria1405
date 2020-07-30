﻿// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Bestiary.SpawnConditionBestiaryInfoElement
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.GameContent.Bestiary
{
  public class SpawnConditionBestiaryInfoElement : FilterProviderInfoElement, IBestiaryBackgroundImagePathAndColorProvider
  {
    private string _backgroundImagePath;
    private Color? _backgroundColor;

    public SpawnConditionBestiaryInfoElement(
      string nameLanguageKey,
      int filterIconFrame,
      string backgroundImagePath = null,
      Color? backgroundColor = null)
      : base(nameLanguageKey, filterIconFrame)
    {
      this._backgroundImagePath = backgroundImagePath;
      this._backgroundColor = backgroundColor;
    }

    public Asset<Texture2D> GetBackgroundImage()
    {
      return this._backgroundImagePath == null ? (Asset<Texture2D>) null : (Asset<Texture2D>) Main.Assets.Request<Texture2D>(this._backgroundImagePath, (AssetRequestMode) 1);
    }

    public Color? GetBackgroundColor()
    {
      return this._backgroundColor;
    }
  }
}
