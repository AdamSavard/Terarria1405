// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.ItemDropRules.DropAttemptInfo
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Terraria.Utilities;

namespace Terraria.GameContent.ItemDropRules
{
  public struct DropAttemptInfo
  {
    public NPC npc;
    public Player player;
    public UnifiedRandom rng;
    public bool IsInSimulation;
    public bool IsExpertMode;
    public bool IsMasterMode;
  }
}
