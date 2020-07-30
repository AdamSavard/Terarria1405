// Decompiled with JetBrains decompiler
// Type: Terraria.ShoppingSettings
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

namespace Terraria
{
  public struct ShoppingSettings
  {
    public double PriceAdjustment;
    public string HappinessReport;

    public static ShoppingSettings NotInShop
    {
      get
      {
        return new ShoppingSettings()
        {
          PriceAdjustment = 1.0,
          HappinessReport = ""
        };
      }
    }
  }
}
