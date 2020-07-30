// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.TeleportPylonInfo
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using System;
using Terraria.DataStructures;

namespace Terraria.GameContent
{
  public struct TeleportPylonInfo : IEquatable<TeleportPylonInfo>
  {
    public Point16 PositionInTiles;
    public TeleportPylonType TypeOfPylon;

    public bool Equals(TeleportPylonInfo other)
    {
      return this.PositionInTiles == other.PositionInTiles && this.TypeOfPylon == other.TypeOfPylon;
    }
  }
}
