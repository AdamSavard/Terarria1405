// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.ItemDropRules.DropNothing
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
  public class DropNothing : IItemDropRule
  {
    public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

    public DropNothing()
    {
      this.ChainedRules = new List<IItemDropRuleChainAttempt>();
    }

    public bool CanDrop(DropAttemptInfo info)
    {
      return false;
    }

    public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
    {
      return new ItemDropAttemptResult()
      {
        State = ItemDropAttemptResultState.DoesntFillConditions
      };
    }

    public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
    {
      Chains.ReportDroprates(this.ChainedRules, 1f, drops, ratesInfo);
    }
  }
}
