﻿// Decompiled with JetBrains decompiler
// Type: Terraria.IO.GameConfiguration
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Newtonsoft.Json.Linq;

namespace Terraria.IO
{
  public class GameConfiguration
  {
    private readonly JObject _root;

    public GameConfiguration(JObject configurationRoot)
    {
      this._root = configurationRoot;
    }

    public T Get<T>(string entry)
    {
      return this._root.Value<T>(entry);
    }
  }
}
