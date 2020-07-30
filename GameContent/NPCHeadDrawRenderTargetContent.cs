// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.NPCHeadDrawRenderTargetContent
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent
{
  public class NPCHeadDrawRenderTargetContent : AnOutlinedDrawRenderTargetContent
  {
    private Texture2D _theTexture;

    public void SetTexture(Texture2D texture)
    {
      if (this._theTexture == texture)
        return;
      this._theTexture = texture;
      this._wasPrepared = false;
      this.width = texture.Width + 8;
      this.height = texture.Height + 8;
    }

    internal override void DrawTheContent(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(this._theTexture, new Vector2(4f, 4f), new Rectangle?(), Color.White, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
    }
  }
}
