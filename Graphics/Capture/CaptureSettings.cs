// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Capture.CaptureSettings
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System;

namespace Terraria.Graphics.Capture
{
  public class CaptureSettings
  {
    public bool UseScaling = true;
    public bool CaptureEntities = true;
    public CaptureBiome Biome = CaptureBiome.DefaultPurity;
    public Rectangle Area;
    public string OutputName;
    public bool CaptureMech;
    public bool CaptureBackground;

    public CaptureSettings()
    {
      DateTime localTime = DateTime.Now.ToLocalTime();
      this.OutputName = "Capture " + localTime.Year.ToString("D4") + "-" + localTime.Month.ToString("D2") + "-" + localTime.Day.ToString("D2") + " " + localTime.Hour.ToString("D2") + "_" + localTime.Minute.ToString("D2") + "_" + localTime.Second.ToString("D2");
    }
  }
}
