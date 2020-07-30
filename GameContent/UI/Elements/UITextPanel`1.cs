// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UITextPanel`1
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class UITextPanel<T> : UIPanel
  {
    protected float _textScale = 1f;
    protected Vector2 _textSize = Vector2.Zero;
    protected Color _color = Color.White;
    protected bool _drawPanel = true;
    public float TextHAlign = 0.5f;
    protected T _text;
    protected bool _isLarge;

    public bool IsLarge
    {
      get
      {
        return this._isLarge;
      }
    }

    public bool DrawPanel
    {
      get
      {
        return this._drawPanel;
      }
      set
      {
        this._drawPanel = value;
      }
    }

    public float TextScale
    {
      get
      {
        return this._textScale;
      }
      set
      {
        this._textScale = value;
      }
    }

    public Vector2 TextSize
    {
      get
      {
        return this._textSize;
      }
    }

    public string Text
    {
      get
      {
        return (object) this._text != null ? this._text.ToString() : "";
      }
    }

    public Color TextColor
    {
      get
      {
        return this._color;
      }
      set
      {
        this._color = value;
      }
    }

    public UITextPanel(T text, float textScale = 1f, bool large = false)
    {
      this.SetText(text, textScale, large);
    }

    public override void Recalculate()
    {
      this.SetText(this._text, this._textScale, this._isLarge);
      base.Recalculate();
    }

    public void SetText(T text)
    {
      this.SetText(text, this._textScale, this._isLarge);
    }

    public virtual void SetText(T text, float textScale, bool large)
    {
      Vector2 vector2 = new Vector2((large ? FontAssets.DeathText.get_Value() : FontAssets.MouseText.get_Value()).MeasureString(text.ToString()).X, large ? 32f : 16f) * textScale;
      this._text = text;
      this._textScale = textScale;
      this._textSize = vector2;
      this._isLarge = large;
      this.MinWidth.Set(vector2.X + this.PaddingLeft + this.PaddingRight, 0.0f);
      this.MinHeight.Set(vector2.Y + this.PaddingTop + this.PaddingBottom, 0.0f);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      if (this._drawPanel)
        base.DrawSelf(spriteBatch);
      this.DrawText(spriteBatch);
    }

    protected void DrawText(SpriteBatch spriteBatch)
    {
      CalculatedStyle innerDimensions = this.GetInnerDimensions();
      Vector2 pos = innerDimensions.Position();
      if (this._isLarge)
        pos.Y -= 10f * this._textScale * this._textScale;
      else
        pos.Y -= 2f * this._textScale;
      pos.X += (innerDimensions.Width - this._textSize.X) * this.TextHAlign;
      if (this._isLarge)
        Utils.DrawBorderStringBig(spriteBatch, this.Text, pos, this._color, this._textScale, 0.0f, 0.0f, -1);
      else
        Utils.DrawBorderString(spriteBatch, this.Text, pos, this._color, this._textScale, 0.0f, 0.0f, -1);
    }
  }
}
