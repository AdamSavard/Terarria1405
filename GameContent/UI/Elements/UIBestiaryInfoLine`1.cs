// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UIBestiaryInfoLine`1
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class UIBestiaryInfoLine<T> : UIElement, IManuallyOrderedUIElement
  {
    private float _textScale = 1f;
    private Vector2 _textSize = Vector2.Zero;
    private Color _color = Color.White;
    private T _text;

    public int OrderInUIList { get; set; }

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

    public UIBestiaryInfoLine(T text, float textScale = 1f)
    {
      this.SetText(text, textScale);
    }

    public override void Recalculate()
    {
      this.SetText(this._text, this._textScale);
      base.Recalculate();
    }

    public void SetText(T text)
    {
      this.SetText(text, this._textScale);
    }

    public virtual void SetText(T text, float textScale)
    {
      Vector2 vector2 = new Vector2(FontAssets.MouseText.get_Value().MeasureString(text.ToString()).X, 16f) * textScale;
      this._text = text;
      this._textScale = textScale;
      this._textSize = vector2;
      this.MinWidth.Set(vector2.X + this.PaddingLeft + this.PaddingRight, 0.0f);
      this.MinHeight.Set(vector2.Y + this.PaddingTop + this.PaddingBottom, 0.0f);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      CalculatedStyle innerDimensions = this.GetInnerDimensions();
      Vector2 pos = innerDimensions.Position();
      pos.Y -= 2f * this._textScale;
      pos.X += (float) (((double) innerDimensions.Width - (double) this._textSize.X) * 0.5);
      Utils.DrawBorderString(spriteBatch, this.Text, pos, this._color, this._textScale, 0.0f, 0.0f, -1);
    }

    public override int CompareTo(object obj)
    {
      return obj is IManuallyOrderedUIElement orderedUiElement ? this.OrderInUIList.CompareTo(orderedUiElement.OrderInUIList) : base.CompareTo(obj);
    }
  }
}
