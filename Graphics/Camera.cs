// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Camera
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics
{
  public class Camera
  {
    public Vector2 UnscaledPosition
    {
      get
      {
        return Main.screenPosition;
      }
    }

    public Vector2 UnscaledSize
    {
      get
      {
        return new Vector2((float) Main.screenWidth, (float) Main.screenHeight);
      }
    }

    public Vector2 ScaledPosition
    {
      get
      {
        return this.UnscaledPosition + this.GameViewMatrix.Translation;
      }
    }

    public Vector2 ScaledSize
    {
      get
      {
        return this.UnscaledSize - this.GameViewMatrix.Translation * 2f;
      }
    }

    public RasterizerState Rasterizer
    {
      get
      {
        return Main.Rasterizer;
      }
    }

    public SamplerState Sampler
    {
      get
      {
        return Main.DefaultSamplerState;
      }
    }

    public SpriteViewMatrix GameViewMatrix
    {
      get
      {
        return Main.GameViewMatrix;
      }
    }

    public SpriteBatch SpriteBatch
    {
      get
      {
        return Main.spriteBatch;
      }
    }

    public Vector2 Center
    {
      get
      {
        return this.UnscaledPosition + this.UnscaledSize * 0.5f;
      }
    }
  }
}
