// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UIResourcePackInfoButton`1
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.IO;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class UIResourcePackInfoButton<T> : UITextPanel<T>
  {
    private readonly Asset<Texture2D> _BasePanelTexture;
    private readonly Asset<Texture2D> _hoveredBorderTexture;
    private ResourcePack _resourcePack;

    public ResourcePack ResourcePack
    {
      get
      {
        return this._resourcePack;
      }
      set
      {
        this._resourcePack = value;
      }
    }

    public UIResourcePackInfoButton(T text, float textScale = 1f, bool large = false)
      : base(text, textScale, large)
    {
      this._BasePanelTexture = (Asset<Texture2D>) Main.Assets.Request<Texture2D>("Images/UI/CharCreation/PanelGrayscale", (AssetRequestMode) 1);
      this._hoveredBorderTexture = (Asset<Texture2D>) Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelBorder", (AssetRequestMode) 1);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      if (this._drawPanel)
      {
        CalculatedStyle dimensions = this.GetDimensions();
        int num1 = 10;
        int num2 = 10;
        Utils.DrawSplicedPanel(spriteBatch, this._BasePanelTexture.get_Value(), (int) dimensions.X, (int) dimensions.Y, (int) dimensions.Width, (int) dimensions.Height, num1, num1, num2, num2, Color.Lerp(Color.Black, this._color, 0.8f) * 0.5f);
        if (this.IsMouseHovering)
          Utils.DrawSplicedPanel(spriteBatch, this._hoveredBorderTexture.get_Value(), (int) dimensions.X, (int) dimensions.Y, (int) dimensions.Width, (int) dimensions.Height, num1, num1, num2, num2, Color.White);
      }
      this.DrawText(spriteBatch);
    }
  }
}
