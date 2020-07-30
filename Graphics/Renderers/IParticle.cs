// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Renderers.IParticle
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Renderers
{
  public interface IParticle
  {
    bool ShouldBeRemovedFromRenderer { get; }

    void Update(ref ParticleRendererSettings settings);

    void Draw(ref ParticleRendererSettings settings, SpriteBatch spritebatch);
  }
}
