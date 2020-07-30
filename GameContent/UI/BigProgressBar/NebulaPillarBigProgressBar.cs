// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.BigProgressBar.NebulaPillarBigProgressBar
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

namespace Terraria.GameContent.UI.BigProgressBar
{
  public class NebulaPillarBigProgressBar : LunarPillarBigProgessBar
  {
    internal override float GetCurrentShieldValue()
    {
      return (float) NPC.ShieldStrengthTowerNebula;
    }

    internal override float GetMaxShieldValue()
    {
      return (float) NPC.ShieldStrengthTowerMax;
    }

    internal override bool IsPlayerInCombatArea()
    {
      return Main.LocalPlayer.ZoneTowerNebula;
    }
  }
}
