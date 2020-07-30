// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.States.UIEmotesMenu
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.Audio;
using Terraria.GameContent.Events;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
  public class UIEmotesMenu : UIState
  {
    private UIElement _outerContainer;
    private UIElement _backPanel;
    private UIElement _container;
    private UIList _list;
    private UIScrollbar _scrollBar;
    private bool _isScrollbarAttached;

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
    }

    public void InitializePage()
    {
      this.RemoveAllChildren();
      UIElement element = new UIElement();
      element.Width.Set(590f, 0.0f);
      element.Top.Set(220f, 0.0f);
      element.Height.Set(-220f, 1f);
      element.HAlign = 0.5f;
      this._outerContainer = element;
      this.Append(element);
      UIPanel uiPanel = new UIPanel();
      uiPanel.Width.Set(0.0f, 1f);
      uiPanel.Height.Set(-110f, 1f);
      uiPanel.BackgroundColor = new Color(33, 43, 79) * 0.8f;
      uiPanel.PaddingTop = 0.0f;
      element.Append((UIElement) uiPanel);
      this._container = (UIElement) uiPanel;
      UIList uiList = new UIList();
      uiList.Width.Set(-25f, 1f);
      uiList.Height.Set(-50f, 1f);
      uiList.Top.Set(50f, 0.0f);
      uiList.HAlign = 0.5f;
      uiList.ListPadding = 14f;
      uiPanel.Append((UIElement) uiList);
      this._list = uiList;
      UIScrollbar scrollbar = new UIScrollbar();
      scrollbar.SetView(100f, 1000f);
      scrollbar.Height.Set(-20f, 1f);
      scrollbar.HAlign = 1f;
      scrollbar.VAlign = 1f;
      scrollbar.Top = StyleDimension.FromPixels(-5f);
      uiList.SetScrollbar(scrollbar);
      this._scrollBar = scrollbar;
      UITextPanel<LocalizedText> uiTextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true);
      uiTextPanel.Width.Set(-10f, 0.5f);
      uiTextPanel.Height.Set(50f, 0.0f);
      uiTextPanel.VAlign = 1f;
      uiTextPanel.HAlign = 0.5f;
      uiTextPanel.Top.Set(-45f, 0.0f);
      uiTextPanel.OnMouseOver += new UIElement.MouseEvent(this.FadedMouseOver);
      uiTextPanel.OnMouseOut += new UIElement.MouseEvent(this.FadedMouseOut);
      uiTextPanel.OnClick += new UIElement.MouseEvent(this.GoBackClick);
      uiTextPanel.SetSnapPoint("Back", 0, new Vector2?(), new Vector2?());
      element.Append((UIElement) uiTextPanel);
      this._backPanel = (UIElement) uiTextPanel;
      int currentGroupIndex = 0;
      this.TryAddingList(Language.GetText("UI.EmoteCategoryGeneral"), ref currentGroupIndex, 10, this.GetEmotesGeneral());
      this.TryAddingList(Language.GetText("UI.EmoteCategoryRPS"), ref currentGroupIndex, 10, this.GetEmotesRPS());
      this.TryAddingList(Language.GetText("UI.EmoteCategoryItems"), ref currentGroupIndex, 11, this.GetEmotesItems());
      this.TryAddingList(Language.GetText("UI.EmoteCategoryBiomesAndEvents"), ref currentGroupIndex, 8, this.GetEmotesBiomesAndEvents());
      this.TryAddingList(Language.GetText("UI.EmoteCategoryTownNPCs"), ref currentGroupIndex, 9, this.GetEmotesTownNPCs());
      this.TryAddingList(Language.GetText("UI.EmoteCategoryCritters"), ref currentGroupIndex, 7, this.GetEmotesCritters());
      this.TryAddingList(Language.GetText("UI.EmoteCategoryBosses"), ref currentGroupIndex, 8, this.GetEmotesBosses());
    }

    private void TryAddingList(
      LocalizedText title,
      ref int currentGroupIndex,
      int maxEmotesPerRow,
      List<int> emoteIds)
    {
      if (emoteIds == null || emoteIds.Count == 0)
        return;
      this._list.Add((UIElement) new EmotesGroupListItem(title, currentGroupIndex++, maxEmotesPerRow, emoteIds.ToArray()));
    }

    private List<int> GetEmotesGeneral()
    {
      return new List<int>()
      {
        0,
        1,
        2,
        3,
        15,
        136,
        94,
        16,
        135,
        134,
        137,
        138,
        139,
        17,
        87,
        88,
        89,
        91,
        92,
        93,
        8,
        9,
        10,
        11,
        14,
        100
      };
    }

    private List<int> GetEmotesRPS()
    {
      return new List<int>() { 36, 37, 38, 33, 34, 35 };
    }

    private List<int> GetEmotesItems()
    {
      return new List<int>()
      {
        7,
        73,
        74,
        75,
        76,
        131,
        78,
        79,
        80,
        81,
        82,
        83,
        84,
        85,
        86,
        90,
        132,
        126,
        (int) sbyte.MaxValue,
        128,
        129
      };
    }

    private List<int> GetEmotesBiomesAndEvents()
    {
      return new List<int>()
      {
        22,
        23,
        24,
        25,
        26,
        27,
        28,
        29,
        30,
        31,
        32,
        18,
        19,
        20,
        21,
        99,
        4,
        5,
        6,
        95,
        96,
        97,
        98
      };
    }

    private List<int> GetEmotesTownNPCs()
    {
      return new List<int>()
      {
        101,
        102,
        103,
        104,
        105,
        106,
        107,
        108,
        109,
        110,
        111,
        112,
        113,
        114,
        115,
        116,
        117,
        118,
        119,
        120,
        121,
        122,
        123,
        124,
        125,
        130,
        140,
        141,
        142
      };
    }

    private List<int> GetEmotesCritters()
    {
      List<int> intList = new List<int>();
      intList.AddRange((IEnumerable<int>) new int[5]
      {
        12,
        13,
        61,
        62,
        63
      });
      intList.AddRange((IEnumerable<int>) new int[4]
      {
        67,
        68,
        69,
        70
      });
      intList.Add(72);
      if (NPC.downedGoblins)
        intList.Add(64);
      if (NPC.downedFrost)
        intList.Add(66);
      if (NPC.downedPirates)
        intList.Add(65);
      if (NPC.downedMartians)
        intList.Add(71);
      return intList;
    }

    private List<int> GetEmotesBosses()
    {
      List<int> intList = new List<int>();
      if (NPC.downedBoss1)
        intList.Add(39);
      if (NPC.downedBoss2)
      {
        intList.Add(40);
        intList.Add(41);
      }
      if (NPC.downedSlimeKing)
        intList.Add(51);
      if (NPC.downedQueenBee)
        intList.Add(42);
      if (NPC.downedBoss3)
        intList.Add(43);
      if (Main.hardMode)
        intList.Add(44);
      if (NPC.downedQueenSlime)
        intList.Add(144);
      if (NPC.downedMechBoss1)
        intList.Add(45);
      if (NPC.downedMechBoss3)
        intList.Add(46);
      if (NPC.downedMechBoss2)
        intList.Add(47);
      if (NPC.downedPlantBoss)
        intList.Add(48);
      if (NPC.downedGolemBoss)
        intList.Add(49);
      if (NPC.downedFishron)
        intList.Add(50);
      if (NPC.downedEmpressOfLight)
        intList.Add(143);
      if (NPC.downedAncientCultist)
        intList.Add(52);
      if (NPC.downedMoonlord)
        intList.Add(53);
      if (NPC.downedHalloweenTree)
        intList.Add(54);
      if (NPC.downedHalloweenKing)
        intList.Add(55);
      if (NPC.downedChristmasTree)
        intList.Add(56);
      if (NPC.downedChristmasIceQueen)
        intList.Add(57);
      if (NPC.downedChristmasSantank)
        intList.Add(58);
      if (NPC.downedPirates)
        intList.Add(59);
      if (NPC.downedMartians)
        intList.Add(60);
      if (DD2Event.DownedInvasionAnyDifficulty)
        intList.Add(133);
      return intList;
    }

    public override void Recalculate()
    {
      if (this._scrollBar != null)
      {
        if (this._isScrollbarAttached && !this._scrollBar.CanScroll)
        {
          this._container.RemoveChild((UIElement) this._scrollBar);
          this._isScrollbarAttached = false;
          this._list.Width.Set(0.0f, 1f);
        }
        else if (!this._isScrollbarAttached && this._scrollBar.CanScroll)
        {
          this._container.Append((UIElement) this._scrollBar);
          this._isScrollbarAttached = true;
          this._list.Width.Set(-25f, 1f);
        }
      }
      base.Recalculate();
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

    public override void Draw(SpriteBatch spriteBatch)
    {
      base.Draw(spriteBatch);
      this.SetupGamepadPoints2(spriteBatch);
    }

    private void SetupGamepadPoints2(SpriteBatch spriteBatch)
    {
      int num1 = 7;
      UILinkPointNavigator.Shortcuts.BackButtonCommand = 1;
      int ID1;
      int index1 = ID1 = 3001;
      List<SnapPoint> snapPoints = this.GetSnapPoints();
      this.RemoveSnapPointsOutOfScreen(spriteBatch, snapPoints);
      Vector2 vector2 = this._backPanel.GetInnerDimensions().ToRectangle().Center.ToVector2();
      UILinkPointNavigator.SetPosition(ID1, vector2);
      UILinkPoint point1 = UILinkPointNavigator.Points[index1];
      point1.Unlink();
      point1.Up = index1 + 1;
      UILinkPoint uiLinkPoint = point1;
      int ID2 = index1 + 1;
      int length = 0;
      List<List<SnapPoint>> snapPointListList = new List<List<SnapPoint>>();
      for (int groupIndex = 0; groupIndex < num1; ++groupIndex)
      {
        List<SnapPoint> emoteGroup = this.GetEmoteGroup(snapPoints, groupIndex);
        if (emoteGroup.Count > 0)
          snapPointListList.Add(emoteGroup);
        length += (int) Math.Ceiling((double) emoteGroup.Count / 14.0);
      }
      SnapPoint[,] snapPointArray = new SnapPoint[14, length];
      int num2 = 0;
      for (int index2 = 0; index2 < snapPointListList.Count; ++index2)
      {
        List<SnapPoint> snapPointList = snapPointListList[index2];
        for (int index3 = 0; index3 < snapPointList.Count; ++index3)
        {
          int index4 = num2 + index3 / 14;
          int index5 = index3 % 14;
          snapPointArray[index5, index4] = snapPointList[index3];
        }
        num2 += (int) Math.Ceiling((double) snapPointList.Count / 14.0);
      }
      int[,] grid = new int[14, length];
      int num3 = 0;
      for (int index2 = 0; index2 < snapPointArray.GetLength(1); ++index2)
      {
        for (int index3 = 0; index3 < snapPointArray.GetLength(0); ++index3)
        {
          SnapPoint snapPoint = snapPointArray[index3, index2];
          if (snapPoint != null)
          {
            UILinkPointNavigator.Points[ID2].Unlink();
            UILinkPointNavigator.SetPosition(ID2, snapPoint.Position);
            grid[index3, index2] = ID2;
            if (index3 == 0)
              num3 = ID2;
            ++ID2;
          }
        }
      }
      uiLinkPoint.Up = num3;
      for (int y1 = 0; y1 < snapPointArray.GetLength(1); ++y1)
      {
        for (int x1 = 0; x1 < snapPointArray.GetLength(0); ++x1)
        {
          int index2 = grid[x1, y1];
          if (index2 != 0)
          {
            UILinkPoint point2 = UILinkPointNavigator.Points[index2];
            if (this.TryGetPointOnGrid(grid, x1, y1, -1, 0))
            {
              point2.Left = grid[x1 - 1, y1];
            }
            else
            {
              point2.Left = point2.ID;
              for (int x2 = x1; x2 < snapPointArray.GetLength(0); ++x2)
              {
                if (this.TryGetPointOnGrid(grid, x2, y1, 0, 0))
                  point2.Left = grid[x2, y1];
              }
            }
            if (this.TryGetPointOnGrid(grid, x1, y1, 1, 0))
            {
              point2.Right = grid[x1 + 1, y1];
            }
            else
            {
              point2.Right = point2.ID;
              for (int x2 = x1; x2 >= 0; --x2)
              {
                if (this.TryGetPointOnGrid(grid, x2, y1, 0, 0))
                  point2.Right = grid[x2, y1];
              }
            }
            if (this.TryGetPointOnGrid(grid, x1, y1, 0, -1))
            {
              point2.Up = grid[x1, y1 - 1];
            }
            else
            {
              point2.Up = point2.ID;
              for (int y2 = y1 - 1; y2 >= 0; --y2)
              {
                if (this.TryGetPointOnGrid(grid, x1, y2, 0, 0))
                {
                  point2.Up = grid[x1, y2];
                  break;
                }
              }
            }
            if (this.TryGetPointOnGrid(grid, x1, y1, 0, 1))
            {
              point2.Down = grid[x1, y1 + 1];
            }
            else
            {
              point2.Down = point2.ID;
              for (int y2 = y1 + 1; y2 < snapPointArray.GetLength(1); ++y2)
              {
                if (this.TryGetPointOnGrid(grid, x1, y2, 0, 0))
                {
                  point2.Down = grid[x1, y2];
                  break;
                }
              }
              if (point2.Down == point2.ID)
                point2.Down = uiLinkPoint.ID;
            }
          }
        }
      }
    }

    private bool TryGetPointOnGrid(int[,] grid, int x, int y, int offsetX, int offsetY)
    {
      return x + offsetX >= 0 && x + offsetX < grid.GetLength(0) && (y + offsetY >= 0 && y + offsetY < grid.GetLength(1)) && grid[x + offsetX, y + offsetY] != 0;
    }

    private void RemoveSnapPointsOutOfScreen(SpriteBatch spriteBatch, List<SnapPoint> pts)
    {
      float num = 1f / Main.UIScale;
      Rectangle clippingRectangle = this._container.GetClippingRectangle(spriteBatch);
      Vector2 minimum = clippingRectangle.TopLeft() * num;
      Vector2 maximum = clippingRectangle.BottomRight() * num;
      for (int index = 0; index < pts.Count; ++index)
      {
        if (!pts[index].Position.Between(minimum, maximum))
        {
          pts.Remove(pts[index]);
          --index;
        }
      }
    }

    private void SetupGamepadPoints(SpriteBatch spriteBatch)
    {
      UILinkPointNavigator.Shortcuts.BackButtonCommand = 1;
      int ID1 = 3001;
      int index1 = ID1;
      List<SnapPoint> snapPoints = this.GetSnapPoints();
      UILinkPointNavigator.SetPosition(ID1, this._backPanel.GetInnerDimensions().ToRectangle().Center.ToVector2());
      UILinkPoint point1 = UILinkPointNavigator.Points[index1];
      point1.Unlink();
      point1.Up = index1 + 1;
      UILinkPoint uiLinkPoint = point1;
      int ID2 = index1 + 1;
      float num1 = 1f / Main.UIScale;
      Rectangle clippingRectangle = this._container.GetClippingRectangle(spriteBatch);
      Vector2 minimum = clippingRectangle.TopLeft() * num1;
      Vector2 maximum = clippingRectangle.BottomRight() * num1;
      for (int index2 = 0; index2 < snapPoints.Count; ++index2)
      {
        if (!snapPoints[index2].Position.Between(minimum, maximum))
        {
          snapPoints.Remove(snapPoints[index2]);
          --index2;
        }
      }
      int num2 = 0;
      int num3 = 7;
      List<List<SnapPoint>> snapPointListList = new List<List<SnapPoint>>();
      for (int groupIndex = 0; groupIndex < num3; ++groupIndex)
      {
        List<SnapPoint> emoteGroup = this.GetEmoteGroup(snapPoints, groupIndex);
        if (emoteGroup.Count > 0)
          snapPointListList.Add(emoteGroup);
      }
      List<SnapPoint>[] array = snapPointListList.ToArray();
      for (int index2 = 0; index2 < array.Length; ++index2)
      {
        List<SnapPoint> snapPointList = array[index2];
        int num4 = snapPointList.Count / 14;
        if (snapPointList.Count % 14 > 0)
          ++num4;
        int num5 = 14;
        if (snapPointList.Count % 14 != 0)
          num5 = snapPointList.Count % 14;
        for (int index3 = 0; index3 < snapPointList.Count; ++index3)
        {
          UILinkPoint point2 = UILinkPointNavigator.Points[ID2];
          point2.Unlink();
          UILinkPointNavigator.SetPosition(ID2, snapPointList[index3].Position);
          int num6 = 14;
          if (index3 / 14 == num4 - 1 && snapPointList.Count % 14 != 0)
            num6 = snapPointList.Count % 14;
          int num7 = index3 % 14;
          point2.Left = ID2 - 1;
          point2.Right = ID2 + 1;
          point2.Up = ID2 - 14;
          point2.Down = ID2 + 14;
          if (num7 == num6 - 1)
            point2.Right = ID2 - num6 + 1;
          if (num7 == 0)
            point2.Left = ID2 + num6 - 1;
          if (num7 == 0)
            uiLinkPoint.Up = ID2;
          if (index3 < 14)
          {
            if (num2 == 0)
            {
              point2.Up = -1;
            }
            else
            {
              point2.Up = ID2 - 14;
              if (num7 >= num2)
                point2.Up -= 14;
              for (int index4 = index2 - 1; index4 > 0 && array[index4].Count <= num7; --index4)
                point2.Up -= 14;
            }
          }
          int num8 = ID1;
          if (index2 == array.Length - 1)
          {
            if (index3 / 14 < num4 - 1 && num7 >= snapPointList.Count % 14)
              point2.Down = num8;
            if (index3 / 14 == num4 - 1)
              point2.Down = num8;
          }
          else if (index3 / 14 == num4 - 1)
          {
            point2.Down = ID2 + 14;
            for (int index4 = index2 + 1; index4 < array.Length && array[index4].Count <= num7; ++index4)
              point2.Down += 14;
            if (index2 == array.Length - 1)
              point2.Down = num8;
          }
          else if (num7 >= num5)
          {
            point2.Down = ID2 + 14 + 14;
            for (int index4 = index2 + 1; index4 < array.Length && array[index4].Count <= num7; ++index4)
              point2.Down += 14;
          }
          ++ID2;
        }
        num2 = num5;
        int num9 = 14 - num2;
        ID2 += num9;
      }
    }

    private List<SnapPoint> GetEmoteGroup(List<SnapPoint> ptsOnPage, int groupIndex)
    {
      string groupName = "Group " + (object) groupIndex;
      List<SnapPoint> list = ptsOnPage.Where<SnapPoint>((Func<SnapPoint, bool>) (a => a.Name == groupName)).ToList<SnapPoint>();
      list.Sort(new Comparison<SnapPoint>(this.SortPoints));
      return list;
    }

    private int SortPoints(SnapPoint a, SnapPoint b)
    {
      return a.Id.CompareTo(b.Id);
    }
  }
}
