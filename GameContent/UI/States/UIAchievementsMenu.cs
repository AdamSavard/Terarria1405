﻿// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.States.UIAchievementsMenu
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria.Achievements;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
  public class UIAchievementsMenu : UIState
  {
    private List<UIAchievementListItem> _achievementElements = new List<UIAchievementListItem>();
    private List<UIToggleImage> _categoryButtons = new List<UIToggleImage>();
    private UIList _achievementsList;
    private UIElement _backpanel;
    private UIElement _outerContainer;

    public void InitializePage()
    {
      this.RemoveAllChildren();
      this._categoryButtons.Clear();
      this._achievementElements.Clear();
      this._achievementsList = (UIList) null;
      bool largeForOtherLanguages = true;
      int num = largeForOtherLanguages.ToInt() * 100;
      UIElement element1 = new UIElement();
      element1.Width.Set(0.0f, 0.8f);
      element1.MaxWidth.Set(800f + (float) num, 0.0f);
      element1.MinWidth.Set(600f + (float) num, 0.0f);
      element1.Top.Set(220f, 0.0f);
      element1.Height.Set(-220f, 1f);
      element1.HAlign = 0.5f;
      this._outerContainer = element1;
      this.Append(element1);
      UIPanel uiPanel = new UIPanel();
      uiPanel.Width.Set(0.0f, 1f);
      uiPanel.Height.Set(-110f, 1f);
      uiPanel.BackgroundColor = new Color(33, 43, 79) * 0.8f;
      uiPanel.PaddingTop = 0.0f;
      element1.Append((UIElement) uiPanel);
      this._achievementsList = new UIList();
      this._achievementsList.Width.Set(-25f, 1f);
      this._achievementsList.Height.Set(-50f, 1f);
      this._achievementsList.Top.Set(50f, 0.0f);
      this._achievementsList.ListPadding = 5f;
      uiPanel.Append((UIElement) this._achievementsList);
      UITextPanel<LocalizedText> uiTextPanel1 = new UITextPanel<LocalizedText>(Language.GetText("UI.Achievements"), 1f, true);
      uiTextPanel1.HAlign = 0.5f;
      uiTextPanel1.Top.Set(-33f, 0.0f);
      uiTextPanel1.SetPadding(13f);
      uiTextPanel1.BackgroundColor = new Color(73, 94, 171);
      element1.Append((UIElement) uiTextPanel1);
      UITextPanel<LocalizedText> uiTextPanel2 = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true);
      uiTextPanel2.Width.Set(-10f, 0.5f);
      uiTextPanel2.Height.Set(50f, 0.0f);
      uiTextPanel2.VAlign = 1f;
      uiTextPanel2.HAlign = 0.5f;
      uiTextPanel2.Top.Set(-45f, 0.0f);
      uiTextPanel2.OnMouseOver += new UIElement.MouseEvent(this.FadedMouseOver);
      uiTextPanel2.OnMouseOut += new UIElement.MouseEvent(this.FadedMouseOut);
      uiTextPanel2.OnClick += new UIElement.MouseEvent(this.GoBackClick);
      element1.Append((UIElement) uiTextPanel2);
      this._backpanel = (UIElement) uiTextPanel2;
      List<Achievement> achievementsList = Main.Achievements.CreateAchievementsList();
      for (int index = 0; index < achievementsList.Count; ++index)
      {
        UIAchievementListItem achievementListItem = new UIAchievementListItem(achievementsList[index], largeForOtherLanguages);
        this._achievementsList.Add((UIElement) achievementListItem);
        this._achievementElements.Add(achievementListItem);
      }
      UIScrollbar scrollbar = new UIScrollbar();
      scrollbar.SetView(100f, 1000f);
      scrollbar.Height.Set(-50f, 1f);
      scrollbar.Top.Set(50f, 0.0f);
      scrollbar.HAlign = 1f;
      uiPanel.Append((UIElement) scrollbar);
      this._achievementsList.SetScrollbar(scrollbar);
      UIElement element2 = new UIElement();
      element2.Width.Set(0.0f, 1f);
      element2.Height.Set(32f, 0.0f);
      element2.Top.Set(10f, 0.0f);
      Asset<Texture2D> texture = (Asset<Texture2D>) Main.Assets.Request<Texture2D>("Images/UI/Achievement_Categories", (AssetRequestMode) 1);
      for (int index = 0; index < 4; ++index)
      {
        UIToggleImage uiToggleImage = new UIToggleImage(texture, 32, 32, new Point(34 * index, 0), new Point(34 * index, 34));
        uiToggleImage.Left.Set((float) (index * 36 + 8), 0.0f);
        uiToggleImage.SetState(true);
        uiToggleImage.OnClick += new UIElement.MouseEvent(this.FilterList);
        this._categoryButtons.Add(uiToggleImage);
        element2.Append((UIElement) uiToggleImage);
      }
      uiPanel.Append(element2);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      base.Draw(spriteBatch);
      for (int index = 0; index < this._categoryButtons.Count; ++index)
      {
        if (this._categoryButtons[index].IsMouseHovering)
        {
          string textValue;
          switch (index)
          {
            case -1:
              textValue = Language.GetTextValue("Achievements.NoCategory");
              break;
            case 0:
              textValue = Language.GetTextValue("Achievements.SlayerCategory");
              break;
            case 1:
              textValue = Language.GetTextValue("Achievements.CollectorCategory");
              break;
            case 2:
              textValue = Language.GetTextValue("Achievements.ExplorerCategory");
              break;
            case 3:
              textValue = Language.GetTextValue("Achievements.ChallengerCategory");
              break;
            default:
              textValue = Language.GetTextValue("Achievements.NoCategory");
              break;
          }
          float x = FontAssets.MouseText.Value.MeasureString(textValue).X;
          Vector2 vector2 = new Vector2((float) Main.mouseX, (float) Main.mouseY) + new Vector2(16f);
          if ((double) vector2.Y > (double) (Main.screenHeight - 30))
            vector2.Y = (float) (Main.screenHeight - 30);
          if ((double) vector2.X > (double) Main.screenWidth - (double) x)
            vector2.X = (float) (Main.screenWidth - 460);
          Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, textValue, vector2.X, vector2.Y, new Color((int) Main.mouseTextColor, (int) Main.mouseTextColor, (int) Main.mouseTextColor, (int) Main.mouseTextColor), Color.Black, Vector2.Zero, 1f);
          break;
        }
      }
      this.SetupGamepadPoints(spriteBatch);
    }

    public void GotoAchievement(Achievement achievement)
    {
      this._achievementsList.Goto((UIList.ElementSearchMethod) (element => element is UIAchievementListItem achievementListItem && achievementListItem.GetAchievement() == achievement));
    }

    private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
    {
      Main.menuMode = 0;
      IngameFancyUI.Close();
    }

    private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
    {
      SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0.0f);
      ((UIPanel) evt.Target).BackgroundColor = new Color(73, 94, 171);
      ((UIPanel) evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
    }

    private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
    {
      ((UIPanel) evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
      ((UIPanel) evt.Target).BorderColor = Color.Black;
    }

    private void FilterList(UIMouseEvent evt, UIElement listeningElement)
    {
      SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0.0f);
      this._achievementsList.Clear();
      foreach (UIAchievementListItem achievementElement in this._achievementElements)
      {
        if (this._categoryButtons[(int) achievementElement.GetAchievement().Category].IsOn)
          this._achievementsList.Add((UIElement) achievementElement);
      }
      this.Recalculate();
    }

    public override void OnActivate()
    {
      this.InitializePage();
      if (Main.gameMenu)
      {
        this._outerContainer.Top.Set(220f, 0.0f);
        this._outerContainer.Height.Set(-220f, 1f);
      }
      else
      {
        this._outerContainer.Top.Set(120f, 0.0f);
        this._outerContainer.Height.Set(-120f, 1f);
      }
      this._achievementsList.UpdateOrder();
      if (!PlayerInput.UsingGamepadUI)
        return;
      UILinkPointNavigator.ChangePoint(3002);
    }

    private void SetupGamepadPoints(SpriteBatch spriteBatch)
    {
      UILinkPointNavigator.Shortcuts.BackButtonCommand = 3;
      int ID1 = 3000;
      UILinkPointNavigator.SetPosition(ID1, this._backpanel.GetInnerDimensions().ToRectangle().Center.ToVector2());
      UILinkPointNavigator.SetPosition(ID1 + 1, this._outerContainer.GetInnerDimensions().ToRectangle().Center.ToVector2());
      int index1 = ID1;
      UILinkPoint point1 = UILinkPointNavigator.Points[index1];
      point1.Unlink();
      point1.Up = index1 + 1;
      int ID2 = index1 + 1;
      UILinkPoint point2 = UILinkPointNavigator.Points[ID2];
      point2.Unlink();
      point2.Up = ID2 + 1;
      point2.Down = ID2 - 1;
      for (int index2 = 0; index2 < this._categoryButtons.Count; ++index2)
      {
        ++ID2;
        UILinkPointNavigator.Shortcuts.FANCYUI_HIGHEST_INDEX = ID2;
        UILinkPointNavigator.SetPosition(ID2, this._categoryButtons[index2].GetInnerDimensions().ToRectangle().Center.ToVector2());
        UILinkPoint point3 = UILinkPointNavigator.Points[ID2];
        point3.Unlink();
        point3.Left = index2 == 0 ? -3 : ID2 - 1;
        point3.Right = index2 == this._categoryButtons.Count - 1 ? -4 : ID2 + 1;
        point3.Down = ID1;
      }
    }
  }
}
