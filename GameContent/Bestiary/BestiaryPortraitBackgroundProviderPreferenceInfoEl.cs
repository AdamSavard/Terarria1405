// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Bestiary.BestiaryPortraitBackgroundProviderPreferenceInfoElement
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
  public class BestiaryPortraitBackgroundProviderPreferenceInfoElement : IPreferenceProviderElement, IBestiaryInfoElement
  {
    private IBestiaryBackgroundImagePathAndColorProvider _preferredProvider;

    public BestiaryPortraitBackgroundProviderPreferenceInfoElement(
      IBestiaryBackgroundImagePathAndColorProvider preferredProvider)
    {
      this._preferredProvider = preferredProvider;
    }

    public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
    {
      return (UIElement) null;
    }

    public bool Matches(
      IBestiaryBackgroundImagePathAndColorProvider provider)
    {
      return provider == this._preferredProvider;
    }

    public IBestiaryBackgroundImagePathAndColorProvider GetPreferredProvider()
    {
      return this._preferredProvider;
    }
  }
}
