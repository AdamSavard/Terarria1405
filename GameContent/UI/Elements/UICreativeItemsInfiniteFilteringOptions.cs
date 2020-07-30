// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UICreativeItemsInfiniteFilteringOptions
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class UICreativeItemsInfiniteFilteringOptions : UIElement
  {
    private Dictionary<UIImageFramed, IItemEntryFilter> _filtersByButtons = new Dictionary<UIImageFramed, IItemEntryFilter>();
    private Dictionary<UIImageFramed, UIElement> _iconsByButtons = new Dictionary<UIImageFramed, UIElement>();
    private EntryFilterer<Item, IItemEntryFilter> _filterer;
    private const int barFramesX = 2;
    private const int barFramesY = 4;

    public event Action OnClickingOption;

    public UICreativeItemsInfiniteFilteringOptions(
      EntryFilterer<Item, IItemEntryFilter> filterer,
      string snapPointsName)
    {
      this._filterer = filterer;
      int num1 = 40;
      int count = this._filterer.AvailableFilters.Count;
      int num2 = num1 * count;
      this.Height = new StyleDimension((float) num1, 0.0f);
      this.Width = new StyleDimension((float) num2, 0.0f);
      this.Top = new StyleDimension(4f, 0.0f);
      this.SetPadding(0.0f);
      Asset<Texture2D> asset = (Asset<Texture2D>) Main.Assets.Request<Texture2D>("Images/UI/Creative/Infinite_Tabs_B", (AssetRequestMode) 1);
      for (int index = 0; index < this._filterer.AvailableFilters.Count; ++index)
      {
        IItemEntryFilter availableFilter = this._filterer.AvailableFilters[index];
        asset.Frame(2, 4, 0, 0, 0, 0).OffsetSize(-2, -2);
        UIImageFramed button = new UIImageFramed(asset, asset.Frame(2, 4, 0, 0, 0, 0).OffsetSize(-2, -2));
        button.Left.Set((float) (num1 * index), 0.0f);
        button.OnClick += new UIElement.MouseEvent(this.singleFilterButtonClick);
        button.OnMouseOver += new UIElement.MouseEvent(this.button_OnMouseOver);
        button.SetPadding(0.0f);
        button.SetSnapPoint(snapPointsName, index, new Vector2?(), new Vector2?());
        this.AddOnHover(availableFilter, (UIElement) button, index);
        UIElement image = availableFilter.GetImage();
        image.IgnoresMouseInteraction = true;
        image.Left = new StyleDimension(6f, 0.0f);
        image.HAlign = 0.0f;
        button.Append(image);
        this._filtersByButtons[button] = availableFilter;
        this._iconsByButtons[button] = image;
        this.Append((UIElement) button);
        this.UpdateVisuals(button, index);
      }
    }

    private void button_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
    {
      SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0.0f);
    }

    private void singleFilterButtonClick(UIMouseEvent evt, UIElement listeningElement)
    {
      IItemEntryFilter itemEntryFilter;
      if (!(evt.Target is UIImageFramed target) || !this._filtersByButtons.TryGetValue(target, out itemEntryFilter))
        return;
      int num = this._filterer.AvailableFilters.IndexOf(itemEntryFilter);
      if (num == -1)
        return;
      if (!this._filterer.ActiveFilters.Contains(itemEntryFilter))
        this._filterer.ActiveFilters.Clear();
      this._filterer.ToggleFilter(num);
      this.UpdateVisuals(target, num);
      if (this.OnClickingOption == null)
        return;
      this.OnClickingOption();
    }

    private void UpdateVisuals(UIImageFramed button, int indexOfFilter)
    {
      bool flag = this._filterer.IsFilterActive(indexOfFilter);
      bool isMouseHovering = button.IsMouseHovering;
      int frameX = flag.ToInt();
      int frameY = flag.ToInt() * 2 + isMouseHovering.ToInt();
      button.SetFrame(2, 4, frameX, frameY, -2, -2);
      if (!(this._iconsByButtons[button] is IColorable iconsByButton))
        return;
      Color color = flag ? Color.White : Color.White * 0.5f;
      iconsByButton.Color = color;
    }

    private void AddOnHover(IItemEntryFilter filter, UIElement button, int indexOfFilter)
    {
      button.OnUpdate += (UIElement.ElementEvent) (element => this.ShowButtonName(element, filter, indexOfFilter));
      button.OnUpdate += (UIElement.ElementEvent) (element => this.UpdateVisuals(button as UIImageFramed, indexOfFilter));
    }

    private void ShowButtonName(UIElement element, IItemEntryFilter number, int indexOfFilter)
    {
      if (!element.IsMouseHovering)
        return;
      string textValue = Language.GetTextValue(number.GetDisplayNameKey());
      Main.instance.MouseText(textValue, 0, (byte) 0, -1, -1, -1, -1, 0);
    }
  }
}
