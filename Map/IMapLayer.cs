﻿// Decompiled with JetBrains decompiler
// Type: Terraria.Map.IMapLayer
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

namespace Terraria.Map
{
  public interface IMapLayer
  {
    void Draw(ref MapOverlayDrawContext context, ref string text);
  }
}
