// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Bestiary.MoonLordPortraitBackgroundProviderBestiaryInfoElement
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
  public class MoonLordPortraitBackgroundProviderBestiaryInfoElement : IBestiaryInfoElement, IBestiaryBackgroundImagePathAndColorProvider
  {
    public Asset<Texture2D> GetBackgroundImage()
    {
      return (Asset<Texture2D>) Main.Assets.Request<Texture2D>("Images/MapBG1", (AssetRequestMode) 1);
    }

    public Color? GetBackgroundColor()
    {
      return new Color?(Color.Black);
    }

    public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
    {
      return (UIElement) null;
    }
  }
}
