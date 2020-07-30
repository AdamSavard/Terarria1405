// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.PlayerQueenSlimeMountTextureContent
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent
{
  public class PlayerQueenSlimeMountTextureContent : ARenderTargetContentByRequest
  {
    protected override void HandleUseReqest(GraphicsDevice device, SpriteBatch spriteBatch)
    {
      Asset<Texture2D> asset = TextureAssets.Extra[204];
      this.PrepareARenderTarget_AndListenToEvents(ref this._target, device, asset.Width(), asset.Height(), RenderTargetUsage.PreserveContents);
      device.SetRenderTarget(this._target);
      device.Clear(Color.Transparent);
      DrawData drawData = new DrawData(asset.get_Value(), Vector2.Zero, Color.White);
      spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
      GameShaders.Misc["QueenSlime"].Apply(new DrawData?(drawData));
      drawData.Draw(spriteBatch);
      spriteBatch.End();
      device.SetRenderTarget((RenderTarget2D) null);
      this._wasPrepared = true;
    }
  }
}
