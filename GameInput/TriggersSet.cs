// Decompiled with JetBrains decompiler
// Type: Terraria.GameInput.TriggersSet
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Terraria.GameInput
{
  public class TriggersSet
  {
    public Dictionary<string, bool> KeyStatus = new Dictionary<string, bool>();
    public bool UsedMovementKey = true;
    public int HotbarScrollCD;
    public int HotbarHoldTime;

    public bool MouseLeft
    {
      get
      {
        return this.KeyStatus[nameof (MouseLeft)];
      }
      set
      {
        this.KeyStatus[nameof (MouseLeft)] = value;
      }
    }

    public bool MouseRight
    {
      get
      {
        return this.KeyStatus[nameof (MouseRight)];
      }
      set
      {
        this.KeyStatus[nameof (MouseRight)] = value;
      }
    }

    public bool Up
    {
      get
      {
        return this.KeyStatus[nameof (Up)];
      }
      set
      {
        this.KeyStatus[nameof (Up)] = value;
      }
    }

    public bool Down
    {
      get
      {
        return this.KeyStatus[nameof (Down)];
      }
      set
      {
        this.KeyStatus[nameof (Down)] = value;
      }
    }

    public bool Left
    {
      get
      {
        return this.KeyStatus[nameof (Left)];
      }
      set
      {
        this.KeyStatus[nameof (Left)] = value;
      }
    }

    public bool Right
    {
      get
      {
        return this.KeyStatus[nameof (Right)];
      }
      set
      {
        this.KeyStatus[nameof (Right)] = value;
      }
    }

    public bool Jump
    {
      get
      {
        return this.KeyStatus[nameof (Jump)];
      }
      set
      {
        this.KeyStatus[nameof (Jump)] = value;
      }
    }

    public bool Throw
    {
      get
      {
        return this.KeyStatus[nameof (Throw)];
      }
      set
      {
        this.KeyStatus[nameof (Throw)] = value;
      }
    }

    public bool Inventory
    {
      get
      {
        return this.KeyStatus[nameof (Inventory)];
      }
      set
      {
        this.KeyStatus[nameof (Inventory)] = value;
      }
    }

    public bool Grapple
    {
      get
      {
        return this.KeyStatus[nameof (Grapple)];
      }
      set
      {
        this.KeyStatus[nameof (Grapple)] = value;
      }
    }

    public bool SmartSelect
    {
      get
      {
        return this.KeyStatus[nameof (SmartSelect)];
      }
      set
      {
        this.KeyStatus[nameof (SmartSelect)] = value;
      }
    }

    public bool SmartCursor
    {
      get
      {
        return this.KeyStatus[nameof (SmartCursor)];
      }
      set
      {
        this.KeyStatus[nameof (SmartCursor)] = value;
      }
    }

    public bool QuickMount
    {
      get
      {
        return this.KeyStatus[nameof (QuickMount)];
      }
      set
      {
        this.KeyStatus[nameof (QuickMount)] = value;
      }
    }

    public bool QuickHeal
    {
      get
      {
        return this.KeyStatus[nameof (QuickHeal)];
      }
      set
      {
        this.KeyStatus[nameof (QuickHeal)] = value;
      }
    }

    public bool QuickMana
    {
      get
      {
        return this.KeyStatus[nameof (QuickMana)];
      }
      set
      {
        this.KeyStatus[nameof (QuickMana)] = value;
      }
    }

    public bool QuickBuff
    {
      get
      {
        return this.KeyStatus[nameof (QuickBuff)];
      }
      set
      {
        this.KeyStatus[nameof (QuickBuff)] = value;
      }
    }

    public bool MapZoomIn
    {
      get
      {
        return this.KeyStatus[nameof (MapZoomIn)];
      }
      set
      {
        this.KeyStatus[nameof (MapZoomIn)] = value;
      }
    }

    public bool MapZoomOut
    {
      get
      {
        return this.KeyStatus[nameof (MapZoomOut)];
      }
      set
      {
        this.KeyStatus[nameof (MapZoomOut)] = value;
      }
    }

    public bool MapAlphaUp
    {
      get
      {
        return this.KeyStatus[nameof (MapAlphaUp)];
      }
      set
      {
        this.KeyStatus[nameof (MapAlphaUp)] = value;
      }
    }

    public bool MapAlphaDown
    {
      get
      {
        return this.KeyStatus[nameof (MapAlphaDown)];
      }
      set
      {
        this.KeyStatus[nameof (MapAlphaDown)] = value;
      }
    }

    public bool MapFull
    {
      get
      {
        return this.KeyStatus[nameof (MapFull)];
      }
      set
      {
        this.KeyStatus[nameof (MapFull)] = value;
      }
    }

    public bool MapStyle
    {
      get
      {
        return this.KeyStatus[nameof (MapStyle)];
      }
      set
      {
        this.KeyStatus[nameof (MapStyle)] = value;
      }
    }

    public bool Hotbar1
    {
      get
      {
        return this.KeyStatus[nameof (Hotbar1)];
      }
      set
      {
        this.KeyStatus[nameof (Hotbar1)] = value;
      }
    }

    public bool Hotbar2
    {
      get
      {
        return this.KeyStatus[nameof (Hotbar2)];
      }
      set
      {
        this.KeyStatus[nameof (Hotbar2)] = value;
      }
    }

    public bool Hotbar3
    {
      get
      {
        return this.KeyStatus[nameof (Hotbar3)];
      }
      set
      {
        this.KeyStatus[nameof (Hotbar3)] = value;
      }
    }

    public bool Hotbar4
    {
      get
      {
        return this.KeyStatus[nameof (Hotbar4)];
      }
      set
      {
        this.KeyStatus[nameof (Hotbar4)] = value;
      }
    }

    public bool Hotbar5
    {
      get
      {
        return this.KeyStatus[nameof (Hotbar5)];
      }
      set
      {
        this.KeyStatus[nameof (Hotbar5)] = value;
      }
    }

    public bool Hotbar6
    {
      get
      {
        return this.KeyStatus[nameof (Hotbar6)];
      }
      set
      {
        this.KeyStatus[nameof (Hotbar6)] = value;
      }
    }

    public bool Hotbar7
    {
      get
      {
        return this.KeyStatus[nameof (Hotbar7)];
      }
      set
      {
        this.KeyStatus[nameof (Hotbar7)] = value;
      }
    }

    public bool Hotbar8
    {
      get
      {
        return this.KeyStatus[nameof (Hotbar8)];
      }
      set
      {
        this.KeyStatus[nameof (Hotbar8)] = value;
      }
    }

    public bool Hotbar9
    {
      get
      {
        return this.KeyStatus[nameof (Hotbar9)];
      }
      set
      {
        this.KeyStatus[nameof (Hotbar9)] = value;
      }
    }

    public bool Hotbar10
    {
      get
      {
        return this.KeyStatus[nameof (Hotbar10)];
      }
      set
      {
        this.KeyStatus[nameof (Hotbar10)] = value;
      }
    }

    public bool HotbarMinus
    {
      get
      {
        return this.KeyStatus[nameof (HotbarMinus)];
      }
      set
      {
        this.KeyStatus[nameof (HotbarMinus)] = value;
      }
    }

    public bool HotbarPlus
    {
      get
      {
        return this.KeyStatus[nameof (HotbarPlus)];
      }
      set
      {
        this.KeyStatus[nameof (HotbarPlus)] = value;
      }
    }

    public bool DpadRadial1
    {
      get
      {
        return this.KeyStatus[nameof (DpadRadial1)];
      }
      set
      {
        this.KeyStatus[nameof (DpadRadial1)] = value;
      }
    }

    public bool DpadRadial2
    {
      get
      {
        return this.KeyStatus[nameof (DpadRadial2)];
      }
      set
      {
        this.KeyStatus[nameof (DpadRadial2)] = value;
      }
    }

    public bool DpadRadial3
    {
      get
      {
        return this.KeyStatus[nameof (DpadRadial3)];
      }
      set
      {
        this.KeyStatus[nameof (DpadRadial3)] = value;
      }
    }

    public bool DpadRadial4
    {
      get
      {
        return this.KeyStatus[nameof (DpadRadial4)];
      }
      set
      {
        this.KeyStatus[nameof (DpadRadial4)] = value;
      }
    }

    public bool RadialHotbar
    {
      get
      {
        return this.KeyStatus[nameof (RadialHotbar)];
      }
      set
      {
        this.KeyStatus[nameof (RadialHotbar)] = value;
      }
    }

    public bool RadialQuickbar
    {
      get
      {
        return this.KeyStatus[nameof (RadialQuickbar)];
      }
      set
      {
        this.KeyStatus[nameof (RadialQuickbar)] = value;
      }
    }

    public bool DpadMouseSnap1
    {
      get
      {
        return this.KeyStatus["DpadSnap1"];
      }
      set
      {
        this.KeyStatus["DpadSnap1"] = value;
      }
    }

    public bool DpadMouseSnap2
    {
      get
      {
        return this.KeyStatus["DpadSnap2"];
      }
      set
      {
        this.KeyStatus["DpadSnap2"] = value;
      }
    }

    public bool DpadMouseSnap3
    {
      get
      {
        return this.KeyStatus["DpadSnap3"];
      }
      set
      {
        this.KeyStatus["DpadSnap3"] = value;
      }
    }

    public bool DpadMouseSnap4
    {
      get
      {
        return this.KeyStatus["DpadSnap4"];
      }
      set
      {
        this.KeyStatus["DpadSnap4"] = value;
      }
    }

    public bool MenuUp
    {
      get
      {
        return this.KeyStatus[nameof (MenuUp)];
      }
      set
      {
        this.KeyStatus[nameof (MenuUp)] = value;
      }
    }

    public bool MenuDown
    {
      get
      {
        return this.KeyStatus[nameof (MenuDown)];
      }
      set
      {
        this.KeyStatus[nameof (MenuDown)] = value;
      }
    }

    public bool MenuLeft
    {
      get
      {
        return this.KeyStatus[nameof (MenuLeft)];
      }
      set
      {
        this.KeyStatus[nameof (MenuLeft)] = value;
      }
    }

    public bool MenuRight
    {
      get
      {
        return this.KeyStatus[nameof (MenuRight)];
      }
      set
      {
        this.KeyStatus[nameof (MenuRight)] = value;
      }
    }

    public bool LockOn
    {
      get
      {
        return this.KeyStatus[nameof (LockOn)];
      }
      set
      {
        this.KeyStatus[nameof (LockOn)] = value;
      }
    }

    public bool ViewZoomIn
    {
      get
      {
        return this.KeyStatus[nameof (ViewZoomIn)];
      }
      set
      {
        this.KeyStatus[nameof (ViewZoomIn)] = value;
      }
    }

    public bool ViewZoomOut
    {
      get
      {
        return this.KeyStatus[nameof (ViewZoomOut)];
      }
      set
      {
        this.KeyStatus[nameof (ViewZoomOut)] = value;
      }
    }

    public bool OpenCreativePowersMenu
    {
      get
      {
        return this.KeyStatus["ToggleCreativeMenu"];
      }
      set
      {
        this.KeyStatus["ToggleCreativeMenu"] = value;
      }
    }

    public void Reset()
    {
      foreach (string index in this.KeyStatus.Keys.ToArray<string>())
        this.KeyStatus[index] = false;
    }

    public TriggersSet Clone()
    {
      TriggersSet triggersSet = new TriggersSet();
      foreach (string key in this.KeyStatus.Keys)
        triggersSet.KeyStatus.Add(key, this.KeyStatus[key]);
      triggersSet.UsedMovementKey = this.UsedMovementKey;
      triggersSet.HotbarScrollCD = this.HotbarScrollCD;
      triggersSet.HotbarHoldTime = this.HotbarHoldTime;
      return triggersSet;
    }

    public void SetupKeys()
    {
      this.KeyStatus.Clear();
      foreach (string knownTrigger in PlayerInput.KnownTriggers)
        this.KeyStatus.Add(knownTrigger, false);
    }

    public Vector2 DirectionsRaw
    {
      get
      {
        return new Vector2((float) (this.Right.ToInt() - this.Left.ToInt()), (float) (this.Down.ToInt() - this.Up.ToInt()));
      }
    }

    public Vector2 GetNavigatorDirections()
    {
      bool flag1 = Main.gameMenu || Main.ingameOptionsWindow || (Main.editChest || Main.editSign) || (Main.playerInventory || Main.LocalPlayer.talkNPC != -1) && PlayerInput.CurrentProfile.UsingDpadMovekeys();
      bool flag2 = this.Up || flag1 && this.MenuUp;
      int num = this.Right ? 1 : (!flag1 ? 0 : (this.MenuRight ? 1 : 0));
      bool flag3 = this.Down || flag1 && this.MenuDown;
      bool flag4 = this.Left || flag1 && this.MenuLeft;
      return new Vector2((float) ((num != 0).ToInt() - flag4.ToInt()), (float) (flag3.ToInt() - flag2.ToInt()));
    }

    public void CopyInto(Player p)
    {
      if (PlayerInput.CurrentInputMode != InputMode.XBoxGamepadUI && !PlayerInput.CursorIsBusy)
      {
        p.controlUp = this.Up;
        p.controlDown = this.Down;
        p.controlLeft = this.Left;
        p.controlRight = this.Right;
        p.controlJump = this.Jump;
        p.controlHook = this.Grapple;
        p.controlTorch = this.SmartSelect;
        p.controlSmart = this.SmartCursor;
        p.controlMount = this.QuickMount;
        p.controlQuickHeal = this.QuickHeal;
        p.controlQuickMana = this.QuickMana;
        p.controlCreativeMenu = this.OpenCreativePowersMenu;
        if (this.QuickBuff)
          p.QuickBuff();
      }
      p.controlInv = this.Inventory;
      p.controlThrow = this.Throw;
      p.mapZoomIn = this.MapZoomIn;
      p.mapZoomOut = this.MapZoomOut;
      p.mapAlphaUp = this.MapAlphaUp;
      p.mapAlphaDown = this.MapAlphaDown;
      p.mapFullScreen = this.MapFull;
      p.mapStyle = this.MapStyle;
      if (this.MouseLeft)
      {
        if (!Main.blockMouse && !p.mouseInterface)
          p.controlUseItem = true;
      }
      else
        Main.blockMouse = false;
      if (!this.MouseRight && !Main.playerInventory)
        PlayerInput.LockGamepadTileUseButton = false;
      if (this.MouseRight && !p.mouseInterface && (!Main.blockMouse && !this.ShouldLockTileUsage()) && !PlayerInput.InBuildingMode)
        p.controlUseTile = true;
      if (PlayerInput.InBuildingMode && this.MouseRight)
        p.controlInv = true;
      bool flag = PlayerInput.Triggers.Current.HotbarPlus || PlayerInput.Triggers.Current.HotbarMinus;
      if (flag)
        ++this.HotbarHoldTime;
      else
        this.HotbarHoldTime = 0;
      if (this.HotbarScrollCD <= 0 || this.HotbarScrollCD == 1 & flag && PlayerInput.CurrentProfile.HotbarRadialHoldTimeRequired > 0)
        return;
      --this.HotbarScrollCD;
    }

    public void CopyIntoDuringChat(Player p)
    {
      if (this.MouseLeft)
      {
        if (!Main.blockMouse && !p.mouseInterface)
          p.controlUseItem = true;
      }
      else
        Main.blockMouse = false;
      if (!this.MouseRight && !Main.playerInventory)
        PlayerInput.LockGamepadTileUseButton = false;
      if (this.MouseRight && !p.mouseInterface && (!Main.blockMouse && !this.ShouldLockTileUsage()) && !PlayerInput.InBuildingMode)
        p.controlUseTile = true;
      bool flag = PlayerInput.Triggers.Current.HotbarPlus || PlayerInput.Triggers.Current.HotbarMinus;
      if (flag)
        ++this.HotbarHoldTime;
      else
        this.HotbarHoldTime = 0;
      if (this.HotbarScrollCD <= 0 || this.HotbarScrollCD == 1 & flag && PlayerInput.CurrentProfile.HotbarRadialHoldTimeRequired > 0)
        return;
      --this.HotbarScrollCD;
    }

    private bool ShouldLockTileUsage()
    {
      return PlayerInput.LockGamepadTileUseButton && PlayerInput.UsingGamepad;
    }
  }
}
