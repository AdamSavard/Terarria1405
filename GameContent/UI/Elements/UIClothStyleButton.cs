// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UIClothStyleButton
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class UIClothStyleButton : UIElement
  {
    private readonly Player _player;
    public readonly int ClothStyleId;
    private readonly Asset<Texture2D> _BasePanelTexture;
    private readonly Asset<Texture2D> _selectedBorderTexture;
    private readonly Asset<Texture2D> _hoveredBorderTexture;
    private readonly UICharacter _char;
    private bool _hovered;
    private bool _soundedHover;
    private int _realSkinVariant;

    public UIClothStyleButton(Player player, int clothStyleId)
    {
      this._player = player;
      this.ClothStyleId = clothStyleId;
      this.Width = StyleDimension.FromPixels(44f);
      this.Height = StyleDimension.FromPixels(80f);
      this._BasePanelTexture = (Asset<Texture2D>) Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanel", (AssetRequestMode) 1);
      this._selectedBorderTexture = (Asset<Texture2D>) Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight", (AssetRequestMode) 1);
      this._hoveredBorderTexture = (Asset<Texture2D>) Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelBorder", (AssetRequestMode) 1);
      UICharacter uiCharacter = new UICharacter(this._player, false, false, 1f);
      uiCharacter.HAlign = 0.5f;
      uiCharacter.VAlign = 0.5f;
      this._char = uiCharacter;
      this.Append((UIElement) this._char);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      this._realSkinVariant = this._player.skinVariant;
      this._player.skinVariant = this.ClothStyleId;
      base.Draw(spriteBatch);
      this._player.skinVariant = this._realSkinVariant;
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      if (this._hovered)
      {
        if (!this._soundedHover)
          SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0.0f);
        this._soundedHover = true;
      }
      else
        this._soundedHover = false;
      CalculatedStyle dimensions = this.GetDimensions();
      Utils.DrawSplicedPanel(spriteBatch, this._BasePanelTexture.get_Value(), (int) dimensions.X, (int) dimensions.Y, (int) dimensions.Width, (int) dimensions.Height, 10, 10, 10, 10, Color.White * 0.5f);
      if (this._realSkinVariant == this.ClothStyleId)
        Utils.DrawSplicedPanel(spriteBatch, this._selectedBorderTexture.get_Value(), (int) dimensions.X + 3, (int) dimensions.Y + 3, (int) dimensions.Width - 6, (int) dimensions.Height - 6, 10, 10, 10, 10, Color.White);
      if (!this._hovered)
        return;
      Utils.DrawSplicedPanel(spriteBatch, this._hoveredBorderTexture.get_Value(), (int) dimensions.X, (int) dimensions.Y, (int) dimensions.Width, (int) dimensions.Height, 10, 10, 10, 10, Color.White);
    }

    public override void MouseDown(UIMouseEvent evt)
    {
      this._player.skinVariant = this.ClothStyleId;
      SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0.0f);
      base.MouseDown(evt);
    }

    public override void MouseOver(UIMouseEvent evt)
    {
      base.MouseOver(evt);
      this._hovered = true;
      this._char.SetAnimated(true);
    }

    public override void MouseOut(UIMouseEvent evt)
    {
      base.MouseOut(evt);
      this._hovered = false;
      this._char.SetAnimated(false);
    }
  }
}
