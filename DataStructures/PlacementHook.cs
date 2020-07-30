// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.PlacementHook
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using System;

namespace Terraria.DataStructures
{
  public struct PlacementHook
  {
    public static PlacementHook Empty = new PlacementHook((Func<int, int, int, int, int, int, int>) null, 0, 0, false);
    public Func<int, int, int, int, int, int, int> hook;
    public int badReturn;
    public int badResponse;
    public bool processedCoordinates;
    public const int Response_AllInvalid = 0;

    public PlacementHook(
      Func<int, int, int, int, int, int, int> hook,
      int badReturn,
      int badResponse,
      bool processedCoordinates)
    {
      this.hook = hook;
      this.badResponse = badResponse;
      this.badReturn = badReturn;
      this.processedCoordinates = processedCoordinates;
    }

    public static bool operator ==(PlacementHook first, PlacementHook second)
    {
      return first.hook == second.hook && first.badResponse == second.badResponse && first.badReturn == second.badReturn && first.processedCoordinates == second.processedCoordinates;
    }

    public static bool operator !=(PlacementHook first, PlacementHook second)
    {
      return first.hook != second.hook || first.badResponse != second.badResponse || first.badReturn != second.badReturn || first.processedCoordinates != second.processedCoordinates;
    }

    public override bool Equals(object obj)
    {
      return obj is PlacementHook placementHook && this == placementHook;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
