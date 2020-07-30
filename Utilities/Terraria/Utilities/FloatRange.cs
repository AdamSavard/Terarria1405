// Decompiled with JetBrains decompiler
// Type: Terraria.Utilities.Terraria.Utilities.FloatRange
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Newtonsoft.Json;

namespace Terraria.Utilities.Terraria.Utilities
{
  public struct FloatRange
  {
    [JsonProperty("Min")]
    public readonly float Minimum;
    [JsonProperty("Max")]
    public readonly float Maximum;

    public FloatRange(float minimum, float maximum)
    {
      this.Minimum = minimum;
      this.Maximum = maximum;
    }

    public static FloatRange operator *(FloatRange range, float scale)
    {
      return new FloatRange(range.Minimum * scale, range.Maximum * scale);
    }

    public static FloatRange operator *(float scale, FloatRange range)
    {
      return range * scale;
    }

    public static FloatRange operator /(FloatRange range, float scale)
    {
      return new FloatRange(range.Minimum / scale, range.Maximum / scale);
    }

    public static FloatRange operator /(float scale, FloatRange range)
    {
      return range / scale;
    }
  }
}
