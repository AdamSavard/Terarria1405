// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Renderers.IPlayerRenderer
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Terraria.Graphics.Renderers
{
  public interface IPlayerRenderer
  {
    void DrawPlayers(Camera camera, IEnumerable<Player> players);

    void DrawPlayerHead(
      Camera camera,
      Player drawPlayer,
      Vector2 position,
      float alpha = 1f,
      float scale = 1f,
      Color borderColor = default (Color));

    void DrawPlayer(
      Camera camera,
      Player drawPlayer,
      Vector2 position,
      float rotation,
      Vector2 rotationOrigin,
      float shadow = 0.0f,
      float scale = 1f);
  }
}
