// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.VirtualCamera
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;

namespace Terraria.Graphics
{
  public struct VirtualCamera
  {
    public readonly Player Player;

    public VirtualCamera(Player player)
    {
      this.Player = player;
    }

    public Vector2 Position
    {
      get
      {
        return this.Center - this.Size * 0.5f;
      }
    }

    public Vector2 Size
    {
      get
      {
        return new Vector2((float) Main.maxScreenW, (float) Main.maxScreenH);
      }
    }

    public Vector2 Center
    {
      get
      {
        return this.Player.Center;
      }
    }
  }
}
