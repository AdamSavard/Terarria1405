﻿// Decompiled with JetBrains decompiler
// Type: Terraria.Physics.BallPassThroughEvent
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

namespace Terraria.Physics
{
  public struct BallPassThroughEvent
  {
    public readonly Tile Tile;
    public readonly Entity Entity;
    public readonly BallPassThroughType Type;
    public readonly float TimeScale;

    public BallPassThroughEvent(
      float timeScale,
      Tile tile,
      Entity entity,
      BallPassThroughType type)
    {
      this.Tile = tile;
      this.Entity = entity;
      this.Type = type;
      this.TimeScale = timeScale;
    }
  }
}
