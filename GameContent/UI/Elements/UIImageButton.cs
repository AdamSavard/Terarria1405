// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UIImageButton
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
  public class UIImageButton : UIElement
  {
    private float _visibilityActive = 1f;
    private float _visibilityInactive = 0.4f;
    private Asset<Texture2D> _texture;
    private Asset<Texture2D> _borderTexture;

    public UIImageButton(Asset<Texture2D> texture)
    {
      this._texture = texture;
      this.Width.Set((float) this._texture.Width(), 0.0f);
      this.Height.Set((float) this._texture.Height(), 0.0f);
    }

    public void SetHoverImage(Asset<Texture2D> texture)
    {
      this._borderTexture = texture;
    }

    public void SetImage(Asset<Texture2D> texture)
    {
      this._texture = texture;
      this.Width.Set((float) this._texture.Width(), 0.0f);
      this.Height.Set((float) this._texture.Height(), 0.0f);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      CalculatedStyle dimensions = this.GetDimensions();
      spriteBatch.Draw(this._texture.Value, dimensions.Position(), Color.White * (this.IsMouseHovering ? this._visibilityActive : this._visibilityInactive));
      if (this._borderTexture == null || !this.IsMouseHovering)
        return;
      spriteBatch.Draw(this._borderTexture.Value, dimensions.Position(), Color.White);
    }

    public override void MouseOver(UIMouseEvent evt)
    {
      base.MouseOver(evt);
      SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0.0f);
    }

    public override void MouseOut(UIMouseEvent evt)
    {
      base.MouseOut(evt);
    }

    public void SetVisibility(float whenActive, float whenInactive)
    {
      this._visibilityActive = MathHelper.Clamp(whenActive, 0.0f, 1f);
      this._visibilityInactive = MathHelper.Clamp(whenInactive, 0.0f, 1f);
    }
  }
}
