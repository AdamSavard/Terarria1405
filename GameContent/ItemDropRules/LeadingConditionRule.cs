// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.ItemDropRules.LeadingConditionRule
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
  public class LeadingConditionRule : IItemDropRule
  {
    private IItemDropRuleCondition _condition;

    public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

    public LeadingConditionRule(IItemDropRuleCondition condition)
    {
      this._condition = condition;
      this.ChainedRules = new List<IItemDropRuleChainAttempt>();
    }

    public bool CanDrop(DropAttemptInfo info)
    {
      return this._condition.CanDrop(info);
    }

    public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
    {
      ratesInfo.AddCondition(this._condition);
      Chains.ReportDroprates(this.ChainedRules, 1f, drops, ratesInfo);
    }

    public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
    {
      return new ItemDropAttemptResult()
      {
        State = ItemDropAttemptResultState.Success
      };
    }
  }
}
