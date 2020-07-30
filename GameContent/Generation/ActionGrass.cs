﻿// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Generation.ActionGrass
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation
{
  public class ActionGrass : GenAction
  {
    public override bool Apply(Point origin, int x, int y, params object[] args)
    {
      if (GenBase._tiles[x, y].active() || GenBase._tiles[x, y - 1].active())
        return false;
      WorldGen.PlaceTile(x, y, (int) Utils.SelectRandom<ushort>(GenBase._random, (ushort) 3, (ushort) 73), true, false, -1, 0);
      return this.UnitApply(origin, x, y, args);
    }
  }
}
