﻿// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.GameNotificationType
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using System;

namespace Terraria.GameContent
{
  [Flags]
  public enum GameNotificationType
  {
    None = 0,
    Damage = 1,
    SpawnOrDeath = 2,
    WorldGen = 4,
    All = WorldGen | SpawnOrDeath | Damage, // 0x00000007
  }
}
