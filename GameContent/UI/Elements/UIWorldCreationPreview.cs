// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UIWorldCreationPreview
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class UIWorldCreationPreview : UIElement
  {
    private readonly Asset<Texture2D> _BorderTexture;
    private readonly Asset<Texture2D> _BackgroundExpertTexture;
    private readonly Asset<Texture2D> _BackgroundNormalTexture;
    private readonly Asset<Texture2D> _BackgroundMasterTexture;
    private readonly Asset<Texture2D> _BunnyExpertTexture;
    private readonly Asset<Texture2D> _BunnyNormalTexture;
    private readonly Asset<Texture2D> _BunnyCreativeTexture;
    private readonly Asset<Texture2D> _BunnyMasterTexture;
    private readonly Asset<Texture2D> _EvilRandomTexture;
    private readonly Asset<Texture2D> _EvilCorruptionTexture;
    private readonly Asset<Texture2D> _EvilCrimsonTexture;
    private readonly Asset<Texture2D> _SizeSmallTexture;
    private readonly Asset<Texture2D> _SizeMediumTexture;
    private readonly Asset<Texture2D> _SizeLargeTexture;
    private byte _difficulty;
    private byte _evil;
    private byte _size;

    public UIWorldCreationPreview()
    {
      this._BorderTexture = (Asset<Texture2D>) Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewBorder", (AssetRequestMode) 1);
      this._BackgroundNormalTexture = (Asset<Texture2D>) Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewDifficultyNormal1", (AssetRequestMode) 1);
      this._BackgroundExpertTexture = (Asset<Texture2D>) Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewDifficultyExpert1", (AssetRequestMode) 1);
      this._BackgroundMasterTexture = (Asset<Texture2D>) Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewDifficultyMaster1", (AssetRequestMode) 1);
      this._BunnyNormalTexture = (Asset<Texture2D>) Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewDifficultyNormal2", (AssetRequestMode) 1);
      this._BunnyExpertTexture = (Asset<Texture2D>) Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewDifficultyExpert2", (AssetRequestMode) 1);
      this._BunnyCreativeTexture = (Asset<Texture2D>) Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewDifficultyCreative2", (AssetRequestMode) 1);
      this._BunnyMasterTexture = (Asset<Texture2D>) Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewDifficultyMaster2", (AssetRequestMode) 1);
      this._EvilRandomTexture = (Asset<Texture2D>) Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewEvilRandom", (AssetRequestMode) 1);
      this._EvilCorruptionTexture = (Asset<Texture2D>) Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewEvilCorruption", (AssetRequestMode) 1);
      this._EvilCrimsonTexture = (Asset<Texture2D>) Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewEvilCrimson", (AssetRequestMode) 1);
      this._SizeSmallTexture = (Asset<Texture2D>) Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewSizeSmall", (AssetRequestMode) 1);
      this._SizeMediumTexture = (Asset<Texture2D>) Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewSizeMedium", (AssetRequestMode) 1);
      this._SizeLargeTexture = (Asset<Texture2D>) Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewSizeLarge", (AssetRequestMode) 1);
      this.Width.Set((float) this._BackgroundExpertTexture.Width(), 0.0f);
      this.Height.Set((float) this._BackgroundExpertTexture.Height(), 0.0f);
    }

    public void UpdateOption(byte difficulty, byte evil, byte size)
    {
      this._difficulty = difficulty;
      this._evil = evil;
      this._size = size;
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      CalculatedStyle dimensions = this.GetDimensions();
      Vector2 position = new Vector2(dimensions.X + 4f, dimensions.Y + 4f);
      Color color = Color.White;
      switch (this._difficulty)
      {
        case 0:
        case 3:
          spriteBatch.Draw(this._BackgroundNormalTexture.get_Value(), position, Color.White);
          color = Color.White;
          break;
        case 1:
          spriteBatch.Draw(this._BackgroundExpertTexture.get_Value(), position, Color.White);
          color = Color.DarkGray;
          break;
        case 2:
          spriteBatch.Draw(this._BackgroundMasterTexture.get_Value(), position, Color.White);
          color = Color.DarkGray;
          break;
      }
      switch (this._size)
      {
        case 0:
          spriteBatch.Draw(this._SizeSmallTexture.get_Value(), position, color);
          break;
        case 1:
          spriteBatch.Draw(this._SizeMediumTexture.get_Value(), position, color);
          break;
        case 2:
          spriteBatch.Draw(this._SizeLargeTexture.get_Value(), position, color);
          break;
      }
      switch (this._evil)
      {
        case 0:
          spriteBatch.Draw(this._EvilRandomTexture.get_Value(), position, color);
          break;
        case 1:
          spriteBatch.Draw(this._EvilCorruptionTexture.get_Value(), position, color);
          break;
        case 2:
          spriteBatch.Draw(this._EvilCrimsonTexture.get_Value(), position, color);
          break;
      }
      switch (this._difficulty)
      {
        case 0:
          spriteBatch.Draw(this._BunnyNormalTexture.get_Value(), position, color);
          break;
        case 1:
          spriteBatch.Draw(this._BunnyExpertTexture.get_Value(), position, color);
          break;
        case 2:
          spriteBatch.Draw(this._BunnyMasterTexture.get_Value(), position, color * 1.2f);
          break;
        case 3:
          spriteBatch.Draw(this._BunnyCreativeTexture.get_Value(), position, color);
          break;
      }
      spriteBatch.Draw(this._BorderTexture.get_Value(), new Vector2(dimensions.X, dimensions.Y), Color.White);
    }
  }
}
