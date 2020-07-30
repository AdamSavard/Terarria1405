// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Effects.CustomSky
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Effects
{
  public abstract class CustomSky : GameEffect
  {
    public abstract void Update(GameTime gameTime);

    public abstract void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth);

    public abstract bool IsActive();

    public abstract void Reset();

    public virtual Color OnTileColor(Color inColor)
    {
      return inColor;
    }

    public virtual float GetCloudAlpha()
    {
      return 1f;
    }

    public override bool IsVisible()
    {
      return true;
    }
  }
}
