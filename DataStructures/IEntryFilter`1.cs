// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.IEntryFilter`1
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Terraria.UI;

namespace Terraria.DataStructures
{
  public interface IEntryFilter<T>
  {
    bool FitsFilter(T entry);

    string GetDisplayNameKey();

    UIElement GetImage();
  }
}
