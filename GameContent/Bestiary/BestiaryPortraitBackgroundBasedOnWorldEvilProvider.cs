// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Bestiary.BestiaryPortraitBackgroundBasedOnWorldEvilProviderPreferenceInfoElement
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
  public class BestiaryPortraitBackgroundBasedOnWorldEvilProviderPreferenceInfoElement : IPreferenceProviderElement, IBestiaryInfoElement
  {
    private IBestiaryBackgroundImagePathAndColorProvider _preferredProviderCorrupt;
    private IBestiaryBackgroundImagePathAndColorProvider _preferredProviderCrimson;

    public BestiaryPortraitBackgroundBasedOnWorldEvilProviderPreferenceInfoElement(
      IBestiaryBackgroundImagePathAndColorProvider preferredProviderCorrupt,
      IBestiaryBackgroundImagePathAndColorProvider preferredProviderCrimson)
    {
      this._preferredProviderCorrupt = preferredProviderCorrupt;
      this._preferredProviderCrimson = preferredProviderCrimson;
    }

    public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
    {
      return (UIElement) null;
    }

    public bool Matches(
      IBestiaryBackgroundImagePathAndColorProvider provider)
    {
      return Main.ActiveWorldFileData == null || !WorldGen.crimson ? provider == this._preferredProviderCorrupt : provider == this._preferredProviderCrimson;
    }

    public IBestiaryBackgroundImagePathAndColorProvider GetPreferredProvider()
    {
      return Main.ActiveWorldFileData == null || !WorldGen.crimson ? this._preferredProviderCorrupt : this._preferredProviderCrimson;
    }
  }
}
