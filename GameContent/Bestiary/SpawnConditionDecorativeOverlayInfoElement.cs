// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Bestiary.SpawnConditionDecorativeOverlayInfoElement
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
  public class SpawnConditionDecorativeOverlayInfoElement : IBestiaryInfoElement, IBestiaryBackgroundOverlayAndColorProvider
  {
    private string _overlayImagePath;
    private Color? _overlayColor;

    public float DisplayPriority { get; set; }

    public SpawnConditionDecorativeOverlayInfoElement(string overlayImagePath = null, Color? overlayColor = null)
    {
      this._overlayImagePath = overlayImagePath;
      this._overlayColor = overlayColor;
    }

    public Asset<Texture2D> GetBackgroundOverlayImage()
    {
      return this._overlayImagePath == null ? (Asset<Texture2D>) null : (Asset<Texture2D>) Main.Assets.Request<Texture2D>(this._overlayImagePath, (AssetRequestMode) 1);
    }

    public Color? GetBackgroundOverlayColor()
    {
      return this._overlayColor;
    }

    public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
    {
      return (UIElement) null;
    }
  }
}
