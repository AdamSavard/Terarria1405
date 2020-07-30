// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.ItemDropRules.DropBasedOnMasterMode
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using System.Collections.Generic;

namespace Terraria.GameContent.ItemDropRules
{
  public class DropBasedOnMasterMode : IItemDropRule, INestedItemDropRule
  {
    private IItemDropRule _ruleForDefault;
    private IItemDropRule _ruleForMasterMode;

    public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

    public DropBasedOnMasterMode(IItemDropRule ruleForDefault, IItemDropRule ruleForMasterMode)
    {
      this._ruleForDefault = ruleForDefault;
      this._ruleForMasterMode = ruleForMasterMode;
      this.ChainedRules = new List<IItemDropRuleChainAttempt>();
    }

    public bool CanDrop(DropAttemptInfo info)
    {
      return info.IsMasterMode ? this._ruleForMasterMode.CanDrop(info) : this._ruleForDefault.CanDrop(info);
    }

    public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
    {
      return new ItemDropAttemptResult()
      {
        State = ItemDropAttemptResultState.DidNotRunCode
      };
    }

    public ItemDropAttemptResult TryDroppingItem(
      DropAttemptInfo info,
      ItemDropRuleResolveAction resolveAction)
    {
      return info.IsMasterMode ? resolveAction(this._ruleForMasterMode, info) : resolveAction(this._ruleForDefault, info);
    }

    public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
    {
      DropRateInfoChainFeed ratesInfo1 = ratesInfo.With(1f);
      ratesInfo1.AddCondition((IItemDropRuleCondition) new Conditions.IsMasterMode());
      this._ruleForMasterMode.ReportDroprates(drops, ratesInfo1);
      DropRateInfoChainFeed ratesInfo2 = ratesInfo.With(1f);
      ratesInfo2.AddCondition((IItemDropRuleCondition) new Conditions.NotMasterMode());
      this._ruleForDefault.ReportDroprates(drops, ratesInfo2);
      Chains.ReportDroprates(this.ChainedRules, 1f, drops, ratesInfo);
    }
  }
}
