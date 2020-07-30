// Decompiled with JetBrains decompiler
// Type: Terraria.Enums.AnchorType
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using System;

namespace Terraria.Enums
{
  [Flags]
  public enum AnchorType
  {
    None = 0,
    SolidTile = 1,
    SolidWithTop = 2,
    Table = 4,
    SolidSide = 8,
    Tree = 16, // 0x00000010
    AlternateTile = 32, // 0x00000020
    EmptyTile = 64, // 0x00000040
    SolidBottom = 128, // 0x00000080
  }
}
