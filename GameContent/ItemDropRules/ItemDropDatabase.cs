﻿// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.ItemDropRules.ItemDropDatabase
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.ID;

namespace Terraria.GameContent.ItemDropRules
{
  public class ItemDropDatabase
  {
    private List<IItemDropRule> _globalEntries = new List<IItemDropRule>();
    private Dictionary<int, List<IItemDropRule>> _entriesByNpcNetId = new Dictionary<int, List<IItemDropRule>>();
    private Dictionary<int, List<int>> _npcNetIdsByType = new Dictionary<int, List<int>>();
    private int _masterModeDropRng = 4;

    public void PrepareNPCNetIDsByTypeDictionary()
    {
      this._npcNetIdsByType.Clear();
      foreach (KeyValuePair<int, NPC> keyValuePair in ContentSamples.NpcsByNetId.Where<KeyValuePair<int, NPC>>((Func<KeyValuePair<int, NPC>, bool>) (x => x.Key < 0)))
      {
        if (!this._npcNetIdsByType.ContainsKey(keyValuePair.Value.type))
          this._npcNetIdsByType[keyValuePair.Value.type] = new List<int>();
        this._npcNetIdsByType[keyValuePair.Value.type].Add(keyValuePair.Value.netID);
      }
    }

    public void TrimDuplicateRulesForNegativeIDs()
    {
      for (int key = -65; key < 0; ++key)
      {
        List<IItemDropRule> source;
        if (this._entriesByNpcNetId.TryGetValue(key, out source))
          this._entriesByNpcNetId[key] = source.Distinct<IItemDropRule>().ToList<IItemDropRule>();
      }
    }

    public List<IItemDropRule> GetRulesForNPCID(
      int npcNetId,
      bool includeGlobalDrops = true)
    {
      List<IItemDropRule> itemDropRuleList1 = new List<IItemDropRule>();
      if (includeGlobalDrops)
        itemDropRuleList1.AddRange((IEnumerable<IItemDropRule>) this._globalEntries);
      List<IItemDropRule> itemDropRuleList2;
      if (this._entriesByNpcNetId.TryGetValue(npcNetId, out itemDropRuleList2))
        itemDropRuleList1.AddRange((IEnumerable<IItemDropRule>) itemDropRuleList2);
      return itemDropRuleList1;
    }

    public IItemDropRule RegisterToGlobal(IItemDropRule entry)
    {
      this._globalEntries.Add(entry);
      return entry;
    }

    public IItemDropRule RegisterToNPC(int type, IItemDropRule entry)
    {
      this.RegisterToNPCNetId(type, entry);
      List<int> intList;
      if (type > 0 && this._npcNetIdsByType.TryGetValue(type, out intList))
      {
        for (int index = 0; index < intList.Count; ++index)
          this.RegisterToNPCNetId(intList[index], entry);
      }
      return entry;
    }

    private void RegisterToNPCNetId(int npcNetId, IItemDropRule entry)
    {
      if (!this._entriesByNpcNetId.ContainsKey(npcNetId))
        this._entriesByNpcNetId[npcNetId] = new List<IItemDropRule>();
      this._entriesByNpcNetId[npcNetId].Add(entry);
    }

    public IItemDropRule RegisterToMultipleNPCs(
      IItemDropRule entry,
      params int[] npcNetIds)
    {
      for (int index = 0; index < npcNetIds.Length; ++index)
        this.RegisterToNPC(npcNetIds[index], entry);
      return entry;
    }

    private void RemoveFromNPCNetId(int npcNetId, IItemDropRule entry)
    {
      if (!this._entriesByNpcNetId.ContainsKey(npcNetId))
        return;
      this._entriesByNpcNetId[npcNetId].Remove(entry);
    }

    public IItemDropRule RemoveFromNPC(int type, IItemDropRule entry)
    {
      this.RemoveFromNPCNetId(type, entry);
      List<int> intList;
      if (type > 0 && this._npcNetIdsByType.TryGetValue(type, out intList))
      {
        for (int index = 0; index < intList.Count; ++index)
          this.RemoveFromNPCNetId(intList[index], entry);
      }
      return entry;
    }

    public IItemDropRule RemoveFromMultipleNPCs(
      IItemDropRule entry,
      params int[] npcNetIds)
    {
      for (int index = 0; index < npcNetIds.Length; ++index)
        this.RemoveFromNPC(npcNetIds[index], entry);
      return entry;
    }

    public void Populate()
    {
      this.PrepareNPCNetIDsByTypeDictionary();
      this.RegisterGlobalRules();
      this.RegisterFoodDrops();
      this.RegisterWeirdRules();
      this.RegisterTownNPCDrops();
      this.RegisterDD2EventDrops();
      this.RegisterMiscDrops();
      this.RegisterHardmodeFeathers();
      this.RegisterYoyos();
      this.RegisterStatusImmunityItems();
      this.RegisterPirateDrops();
      this.RegisterBloodMoonFishingEnemies();
      this.RegisterMartianDrops();
      this.RegisterBossTrophies();
      this.RegisterBosses();
      this.RegisterHardmodeDungeonDrops();
      this.RegisterMimic();
      this.RegisterEclipse();
      this.RegisterBloodMoonFishing();
      this.TrimDuplicateRulesForNegativeIDs();
    }

    private void RegisterBloodMoonFishing()
    {
      this.RegisterToMultipleNPCs(ItemDropRule.Common(4608, 2, 4, 6), 587, 586);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(4608, 2, 7, 10), 620, 621, 618);
      this.RegisterToMultipleNPCs(ItemDropRule.OneFromOptions(8, 4273, 4381, 4325), 587, 586);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(3213, 15, 1, 1), 587, 586);
      this.RegisterToNPC(620, ItemDropRule.Common(4270, 8, 1, 1));
      this.RegisterToNPC(620, ItemDropRule.Common(4317, 8, 1, 1));
      this.RegisterToNPC(621, ItemDropRule.Common(4272, 8, 1, 1));
      this.RegisterToNPC(621, ItemDropRule.Common(4317, 8, 1, 1));
      this.RegisterToNPC(618, ItemDropRule.Common(4269, 5, 1, 1));
      this.RegisterToNPC(618, ItemDropRule.Common(4054, 10, 1, 1));
      Conditions.IsBloodMoonAndNotFromStatue andNotFromStatue = new Conditions.IsBloodMoonAndNotFromStatue();
      this.RegisterToMultipleNPCs(ItemDropRule.ByCondition((IItemDropRuleCondition) andNotFromStatue, 4271, 200, 1, 1, 1), 587, 586, 489, 490, 109, 621, 620);
      this.RegisterToMultipleNPCs(ItemDropRule.ByCondition((IItemDropRuleCondition) andNotFromStatue, 4271, 9, 1, 1, 1), 53, 536, 618);
    }

    private void RegisterEclipse()
    {
      this.RegisterToNPC(461, ItemDropRule.ExpertGetsRerolls(497, 50, 1));
      this.RegisterToMultipleNPCs(ItemDropRule.ExpertGetsRerolls(900, 35, 1), 159, 158);
      this.RegisterToNPC(251, ItemDropRule.ExpertGetsRerolls(1311, 15, 1));
      Conditions.DownedAllMechBosses downedAllMechBosses = new Conditions.DownedAllMechBosses();
      Conditions.DownedPlantera downedPlantera = new Conditions.DownedPlantera();
      IItemDropRule npc = this.RegisterToNPC(477, (IItemDropRule) new LeadingConditionRule((IItemDropRuleCondition) downedAllMechBosses));
      IItemDropRule rule = npc.OnSuccess((IItemDropRule) new LeadingConditionRule((IItemDropRuleCondition) downedPlantera), false);
      npc.OnSuccess(ItemDropRule.ExpertGetsRerolls(1570, 4, 1), false);
      rule.OnSuccess(ItemDropRule.ExpertGetsRerolls(2770, 20, 1), false);
      rule.OnSuccess(ItemDropRule.ExpertGetsRerolls(3292, 3, 1), false);
      this.RegisterToNPC(253, (IItemDropRule) new LeadingConditionRule((IItemDropRuleCondition) downedAllMechBosses)).OnSuccess(ItemDropRule.ExpertGetsRerolls(1327, 40, 1), false);
      this.RegisterToNPC(460, (IItemDropRule) new LeadingConditionRule((IItemDropRuleCondition) downedPlantera)).OnSuccess(ItemDropRule.ExpertGetsRerolls(3098, 40, 1), false);
      this.RegisterToNPC(460, ItemDropRule.ExpertGetsRerolls(4740, 50, 1));
      this.RegisterToNPC(460, ItemDropRule.ExpertGetsRerolls(4741, 50, 1));
      this.RegisterToNPC(460, ItemDropRule.ExpertGetsRerolls(4742, 50, 1));
      this.RegisterToNPC(468, (IItemDropRule) new LeadingConditionRule((IItemDropRuleCondition) downedPlantera)).OnSuccess(ItemDropRule.ExpertGetsRerolls(3105, 40, 1), false);
      this.RegisterToNPC(468, ItemDropRule.ExpertGetsRerolls(4738, 50, 1));
      this.RegisterToNPC(468, ItemDropRule.ExpertGetsRerolls(4739, 50, 1));
      this.RegisterToNPC(466, (IItemDropRule) new LeadingConditionRule((IItemDropRuleCondition) downedPlantera)).OnSuccess(ItemDropRule.ExpertGetsRerolls(3106, 40, 1), false);
      this.RegisterToNPC(467, (IItemDropRule) new LeadingConditionRule((IItemDropRuleCondition) downedPlantera)).OnSuccess(ItemDropRule.ExpertGetsRerolls(3249, 40, 1), false);
      IItemDropRule itemDropRule1 = ItemDropRule.Common(3107, 25, 1, 1);
      IItemDropRule itemDropRule2 = ItemDropRule.WithRerolls(3107, 1, 25, 1, 1);
      itemDropRule1.OnSuccess(ItemDropRule.Common(3108, 1, 100, 200), true);
      itemDropRule2.OnSuccess(ItemDropRule.Common(3108, 1, 100, 200), true);
      this.RegisterToNPC(463, (IItemDropRule) new LeadingConditionRule((IItemDropRuleCondition) downedPlantera)).OnSuccess((IItemDropRule) new DropBasedOnExpertMode(itemDropRule1, itemDropRule2), false);
    }

    private void RegisterMimic()
    {
      this.RegisterToNPC(85, ItemDropRule.OneFromOptions(1, 437, 517, 535, 536, 532, 554));
      IItemDropRule itemDropRule = ItemDropRule.Common(1312, 20, 1, 1);
      itemDropRule.OnFailedRoll(ItemDropRule.OneFromOptions(1, 676, 725, 1264), false);
      this.RegisterToNPC(629, itemDropRule);
    }

    private void RegisterHardmodeDungeonDrops()
    {
      int[] numArray = new int[12]
      {
        269,
        270,
        271,
        272,
        273,
        274,
        275,
        276,
        277,
        278,
        279,
        280
      };
      this.RegisterToNPC(290, ItemDropRule.ExpertGetsRerolls(1513, 15, 1));
      this.RegisterToNPC(290, ItemDropRule.ExpertGetsRerolls(938, 10, 1));
      this.RegisterToNPC(287, ItemDropRule.ExpertGetsRerolls(977, 12, 1));
      this.RegisterToNPC(287, ItemDropRule.ExpertGetsRerolls(963, 12, 1));
      this.RegisterToNPC(291, ItemDropRule.ExpertGetsRerolls(1300, 12, 1));
      this.RegisterToNPC(291, ItemDropRule.ExpertGetsRerolls(1254, 12, 1));
      this.RegisterToNPC(292, ItemDropRule.ExpertGetsRerolls(1514, 12, 1));
      this.RegisterToNPC(292, ItemDropRule.ExpertGetsRerolls(679, 12, 1));
      this.RegisterToNPC(293, ItemDropRule.ExpertGetsRerolls(759, 18, 1));
      this.RegisterToNPC(289, ItemDropRule.ExpertGetsRerolls(4789, 25, 1));
      this.RegisterToMultipleNPCs(ItemDropRule.ExpertGetsRerolls(1446, 20, 1), 281, 282);
      this.RegisterToMultipleNPCs(ItemDropRule.ExpertGetsRerolls(1444, 20, 1), 283, 284);
      this.RegisterToMultipleNPCs(ItemDropRule.ExpertGetsRerolls(1445, 20, 1), 285, 286);
      this.RegisterToMultipleNPCs(ItemDropRule.ExpertGetsRerolls(1183, 400, 1), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.ExpertGetsRerolls(1266, 300, 1), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.ExpertGetsRerolls(671, 200, 1), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.ExpertGetsRerolls(4679, 200, 1), numArray);
      this.RegisterToNPC(288, ItemDropRule.Common(1508, 1, 1, 2));
    }

    private void RegisterBosses()
    {
      this.RegisterBoss_EOC();
      this.RegisterBoss_BOC();
      this.RegisterBoss_EOW();
      this.RegisterBoss_QueenBee();
      this.RegisterBoss_Skeletron();
      this.RegisterBoss_WOF();
      this.RegisterBoss_AncientCultist();
      this.RegisterBoss_MoonLord();
      this.RegisterBoss_LunarTowers();
      this.RegisterBoss_Betsy();
      this.RegisterBoss_Golem();
      this.RegisterBoss_DukeFishron();
      this.RegisterBoss_SkeletronPrime();
      this.RegisterBoss_TheDestroyer();
      this.RegisterBoss_Twins();
      this.RegisterBoss_Plantera();
      this.RegisterBoss_KingSlime();
      this.RegisterBoss_FrostMoon();
      this.RegisterBoss_PumpkinMoon();
      this.RegisterBoss_HallowBoss();
      this.RegisterBoss_QueenSlime();
    }

    private void RegisterBoss_QueenSlime()
    {
      short num = 657;
      this.RegisterToNPC((int) num, ItemDropRule.BossBag(4957));
      this.RegisterToNPC((int) num, ItemDropRule.MasterModeCommonDrop(4950));
      this.RegisterToNPC((int) num, ItemDropRule.MasterModeDropOnAllPlayers(4960, this._masterModeDropRng));
      LeadingConditionRule rule = new LeadingConditionRule((IItemDropRuleCondition) new Conditions.NotExpert());
      this.RegisterToNPC((int) num, (IItemDropRule) rule);
      rule.OnSuccess(ItemDropRule.Common(4986, 1, 25, 75), false);
      rule.OnSuccess(ItemDropRule.Common(4959, 7, 1, 1), false);
      rule.OnSuccess(ItemDropRule.OneFromOptions(1, 4982, 4983, 4984), false);
      rule.OnSuccess(ItemDropRule.Common(4981, 4, 1, 1), false);
      rule.OnSuccess(ItemDropRule.NotScalingWithLuck(4980, 3, 1, 1), false);
    }

    private void RegisterBoss_HallowBoss()
    {
      short num = 636;
      this.RegisterToNPC((int) num, ItemDropRule.BossBag(4782));
      this.RegisterToNPC((int) num, ItemDropRule.MasterModeCommonDrop(4949));
      this.RegisterToNPC((int) num, ItemDropRule.MasterModeDropOnAllPlayers(4811, this._masterModeDropRng));
      LeadingConditionRule rule = new LeadingConditionRule((IItemDropRuleCondition) new Conditions.NotExpert());
      this.RegisterToNPC((int) num, (IItemDropRule) rule).OnSuccess(ItemDropRule.OneFromOptions(1, 4923, 4952, 4953, 4914), false);
      rule.OnSuccess(ItemDropRule.Common(4823, 15, 1, 1), false);
      rule.OnSuccess(ItemDropRule.Common(4778, 4, 1, 1), false);
      rule.OnSuccess(ItemDropRule.Common(4715, 50, 1, 1), false);
      rule.OnSuccess(ItemDropRule.Common(4784, 7, 1, 1), false);
      LeadingConditionRule leadingConditionRule = new LeadingConditionRule((IItemDropRuleCondition) new Conditions.EmpressOfLightIsGenuinelyEnraged());
      this.RegisterToNPC((int) num, (IItemDropRule) leadingConditionRule).OnSuccess(ItemDropRule.Common(5005, 1, 1, 1), false);
    }

    private void RegisterBoss_PumpkinMoon()
    {
      Conditions.PumpkinMoonDropGatingChance dropGatingChance = new Conditions.PumpkinMoonDropGatingChance();
      Conditions.IsPumpkinMoon isPumpkinMoon = new Conditions.IsPumpkinMoon();
      Conditions.FromCertainWaveAndAbove certainWaveAndAbove = new Conditions.FromCertainWaveAndAbove(15);
      this.RegisterToNPC(327, (IItemDropRule) new LeadingConditionRule((IItemDropRuleCondition) isPumpkinMoon)).OnSuccess((IItemDropRule) new LeadingConditionRule((IItemDropRuleCondition) certainWaveAndAbove), false).OnSuccess(ItemDropRule.Common(1856, 1, 1, 1), false);
      this.RegisterToNPC(325, (IItemDropRule) new LeadingConditionRule((IItemDropRuleCondition) isPumpkinMoon)).OnSuccess((IItemDropRule) new LeadingConditionRule((IItemDropRuleCondition) certainWaveAndAbove), false).OnSuccess(ItemDropRule.Common(1855, 1, 1, 1), false);
      this.RegisterToNPC(315, ItemDropRule.ByCondition((IItemDropRuleCondition) dropGatingChance, 1857, 20, 1, 1, 1));
      int[] numArray = new int[10]
      {
        305,
        306,
        307,
        308,
        309,
        310,
        311,
        312,
        313,
        314
      };
      this.RegisterToMultipleNPCs((IItemDropRule) new LeadingConditionRule((IItemDropRuleCondition) dropGatingChance), numArray).OnSuccess(ItemDropRule.OneFromOptions(10, 1788, 1789, 1790), false);
      IItemDropRule npc1 = this.RegisterToNPC(325, (IItemDropRule) new LeadingConditionRule((IItemDropRuleCondition) dropGatingChance));
      IItemDropRule rule1 = ItemDropRule.Common(1835, 1, 1, 1);
      rule1.OnSuccess(ItemDropRule.Common(1836, 1, 30, 60), true);
      npc1.OnSuccess((IItemDropRule) new OneFromRulesRule(1, new IItemDropRule[5]
      {
        ItemDropRule.Common(1829, 1, 1, 1),
        ItemDropRule.Common(1831, 1, 1, 1),
        rule1,
        ItemDropRule.Common(1837, 1, 1, 1),
        ItemDropRule.Common(1845, 1, 1, 1)
      }), false);
      npc1.OnSuccess(ItemDropRule.ByCondition((IItemDropRuleCondition) new Conditions.IsExpert(), 4444, 5, 1, 1, 1), false);
      npc1.OnSuccess(ItemDropRule.MasterModeCommonDrop(4941), false);
      npc1.OnSuccess(ItemDropRule.MasterModeDropOnAllPlayers(4793, this._masterModeDropRng), false);
      IItemDropRule rule2 = ItemDropRule.Common(1782, 1, 1, 1);
      rule2.OnSuccess(ItemDropRule.Common(1783, 1, 50, 100), true);
      IItemDropRule rule3 = ItemDropRule.Common(1784, 1, 1, 1);
      rule3.OnSuccess(ItemDropRule.Common(1785, 1, 25, 50), true);
      IItemDropRule npc2 = this.RegisterToNPC(327, (IItemDropRule) new LeadingConditionRule((IItemDropRuleCondition) dropGatingChance));
      npc2.OnSuccess((IItemDropRule) new OneFromRulesRule(1, new IItemDropRule[8]
      {
        rule2,
        rule3,
        ItemDropRule.Common(1811, 1, 1, 1),
        ItemDropRule.Common(1826, 1, 1, 1),
        ItemDropRule.Common(1801, 1, 1, 1),
        ItemDropRule.Common(1802, 1, 1, 1),
        ItemDropRule.Common(4680, 1, 1, 1),
        ItemDropRule.Common(1798, 1, 1, 1)
      }), false);
      npc2.OnSuccess(ItemDropRule.MasterModeCommonDrop(4942), false);
      npc2.OnSuccess(ItemDropRule.MasterModeDropOnAllPlayers(4812, this._masterModeDropRng), false);
      this.RegisterToNPC(325, ItemDropRule.ByCondition((IItemDropRuleCondition) isPumpkinMoon, 1729, 1, 30, 50, 1));
      this.RegisterToNPC(326, ItemDropRule.ByCondition((IItemDropRuleCondition) isPumpkinMoon, 1729, 1, 1, 4, 1));
    }

    private void RegisterBoss_FrostMoon()
    {
      Conditions.FrostMoonDropGatingChance dropGatingChance = new Conditions.FrostMoonDropGatingChance();
      Conditions.FrostMoonDropGateForTrophies dropGateForTrophies = new Conditions.FrostMoonDropGateForTrophies();
      Conditions.FromCertainWaveAndAbove certainWaveAndAbove = new Conditions.FromCertainWaveAndAbove(15);
      IItemDropRule npc1 = this.RegisterToNPC(344, (IItemDropRule) new LeadingConditionRule((IItemDropRuleCondition) dropGatingChance));
      npc1.OnSuccess(ItemDropRule.ByCondition((IItemDropRuleCondition) dropGateForTrophies, 1962, 1, 1, 1, 1), false);
      npc1.OnSuccess(ItemDropRule.Common(1871, 15, 1, 1), false).OnFailedRoll(ItemDropRule.OneFromOptions(1, 1916, 1928, 1930), false);
      npc1.OnSuccess(ItemDropRule.MasterModeCommonDrop(4944), false);
      npc1.OnSuccess(ItemDropRule.MasterModeDropOnAllPlayers(4813, this._masterModeDropRng), false);
      IItemDropRule npc2 = this.RegisterToNPC(345, (IItemDropRule) new LeadingConditionRule((IItemDropRuleCondition) dropGatingChance));
      npc2.OnSuccess(ItemDropRule.ByCondition((IItemDropRuleCondition) dropGateForTrophies, 1960, 1, 1, 1, 1), false);
      npc2.OnSuccess(ItemDropRule.ByCondition((IItemDropRuleCondition) certainWaveAndAbove, 1914, 30, 1, 1, 1), false);
      npc2.OnSuccess(ItemDropRule.Common(1959, 15, 1, 1), false).OnFailedRoll(ItemDropRule.OneFromOptions(1, 1931, 1946, 1947), false);
      npc2.OnSuccess(ItemDropRule.MasterModeCommonDrop(4943), false);
      npc2.OnSuccess(ItemDropRule.MasterModeDropOnAllPlayers(4814, this._masterModeDropRng), false);
      IItemDropRule npc3 = this.RegisterToNPC(346, (IItemDropRule) new LeadingConditionRule((IItemDropRuleCondition) dropGatingChance));
      npc3.OnSuccess(ItemDropRule.ByCondition((IItemDropRuleCondition) dropGateForTrophies, 1961, 1, 1, 1, 1), false);
      npc3.OnSuccess(ItemDropRule.OneFromOptions(1, 1910, 1929), false);
      npc3.OnSuccess(ItemDropRule.MasterModeCommonDrop(4945), false);
      npc3.OnSuccess(ItemDropRule.MasterModeDropOnAllPlayers(4794, this._masterModeDropRng), false);
      this.RegisterToMultipleNPCs(ItemDropRule.OneFromOptions(200, 1943, 1944, 1945), 338, 339, 340);
      this.RegisterToNPC(341, ItemDropRule.ByCondition((IItemDropRuleCondition) new Conditions.IsChristmas(), 1869, 1, 1, 1, 1));
    }

    private void RegisterBoss_KingSlime()
    {
      short num = 50;
      this.RegisterToNPC((int) num, ItemDropRule.BossBag(3318));
      this.RegisterToNPC((int) num, ItemDropRule.MasterModeCommonDrop(4929));
      this.RegisterToNPC((int) num, ItemDropRule.MasterModeDropOnAllPlayers(4797, this._masterModeDropRng));
      LeadingConditionRule rule = new LeadingConditionRule((IItemDropRuleCondition) new Conditions.NotExpert());
      this.RegisterToNPC((int) num, (IItemDropRule) rule);
      rule.OnSuccess(ItemDropRule.Common(2430, 4, 1, 1), false);
      rule.OnSuccess(ItemDropRule.Common(2493, 7, 1, 1), false);
      rule.OnSuccess(ItemDropRule.OneFromOptions(1, 256, 257, 258), false);
      rule.OnSuccess(ItemDropRule.NotScalingWithLuck(2585, 3, 1, 1), false).OnFailedRoll(ItemDropRule.Common(2610, 1, 1, 1), false);
      rule.OnSuccess(ItemDropRule.Common(998, 1, 1, 1), false);
    }

    private void RegisterBoss_Plantera()
    {
      short num = 262;
      this.RegisterToNPC((int) num, ItemDropRule.BossBag(3328));
      this.RegisterToNPC((int) num, ItemDropRule.MasterModeCommonDrop(4934));
      this.RegisterToNPC((int) num, ItemDropRule.MasterModeDropOnAllPlayers(4806, this._masterModeDropRng));
      LeadingConditionRule rule1 = new LeadingConditionRule((IItemDropRuleCondition) new Conditions.NotExpert());
      this.RegisterToNPC((int) num, (IItemDropRule) rule1);
      LeadingConditionRule rule2 = new LeadingConditionRule((IItemDropRuleCondition) new Conditions.FirstTimeKillingPlantera());
      rule1.OnSuccess((IItemDropRule) rule2, false);
      rule1.OnSuccess(ItemDropRule.Common(2109, 7, 1, 1), false);
      rule1.OnSuccess(ItemDropRule.Common(1141, 1, 1, 1), false);
      rule1.OnSuccess(ItemDropRule.Common(1182, 20, 1, 1), false);
      rule1.OnSuccess(ItemDropRule.Common(1305, 50, 1, 1), false);
      rule1.OnSuccess(ItemDropRule.Common(1157, 4, 1, 1), false);
      rule1.OnSuccess(ItemDropRule.Common(3021, 10, 1, 1), false);
      IItemDropRule itemDropRule = ItemDropRule.Common(758, 1, 1, 1);
      itemDropRule.OnSuccess(ItemDropRule.Common(771, 1, 50, 150), true);
      rule2.OnSuccess(itemDropRule, true);
      rule2.OnFailedConditions((IItemDropRule) new OneFromRulesRule(1, new IItemDropRule[7]
      {
        itemDropRule,
        ItemDropRule.Common(1255, 1, 1, 1),
        ItemDropRule.Common(788, 1, 1, 1),
        ItemDropRule.Common(1178, 1, 1, 1),
        ItemDropRule.Common(1259, 1, 1, 1),
        ItemDropRule.Common(1155, 1, 1, 1),
        ItemDropRule.Common(3018, 1, 1, 1)
      }), false);
    }

    private void RegisterBoss_SkeletronPrime()
    {
      Conditions.NotExpert notExpert = new Conditions.NotExpert();
      short maxValue = (short) sbyte.MaxValue;
      this.RegisterToNPC((int) maxValue, ItemDropRule.BossBag(3327));
      this.RegisterToNPC((int) maxValue, ItemDropRule.MasterModeCommonDrop(4933));
      this.RegisterToNPC((int) maxValue, ItemDropRule.MasterModeDropOnAllPlayers(4805, this._masterModeDropRng));
      this.RegisterToNPC((int) maxValue, ItemDropRule.ByCondition((IItemDropRuleCondition) notExpert, 2107, 7, 1, 1, 1));
      this.RegisterToNPC((int) maxValue, ItemDropRule.ByCondition((IItemDropRuleCondition) notExpert, 1225, 1, 15, 30, 1));
      this.RegisterToNPC((int) maxValue, ItemDropRule.ByCondition((IItemDropRuleCondition) notExpert, 547, 1, 25, 40, 1));
    }

    private void RegisterBoss_TheDestroyer()
    {
      Conditions.NotExpert notExpert = new Conditions.NotExpert();
      short num = 134;
      this.RegisterToNPC((int) num, ItemDropRule.BossBag(3325));
      this.RegisterToNPC((int) num, ItemDropRule.MasterModeCommonDrop(4932));
      this.RegisterToNPC((int) num, ItemDropRule.MasterModeDropOnAllPlayers(4803, this._masterModeDropRng));
      this.RegisterToNPC((int) num, ItemDropRule.ByCondition((IItemDropRuleCondition) notExpert, 2113, 7, 1, 1, 1));
      this.RegisterToNPC((int) num, ItemDropRule.ByCondition((IItemDropRuleCondition) notExpert, 1225, 1, 15, 30, 1));
      this.RegisterToNPC((int) num, ItemDropRule.ByCondition((IItemDropRuleCondition) notExpert, 548, 1, 25, 40, 1));
    }

    private void RegisterBoss_Twins()
    {
      LeadingConditionRule rule1 = new LeadingConditionRule((IItemDropRuleCondition) new Conditions.MissingTwin());
      LeadingConditionRule rule2 = new LeadingConditionRule((IItemDropRuleCondition) new Conditions.NotExpert());
      rule1.OnSuccess(ItemDropRule.BossBag(3326), false);
      rule1.OnSuccess((IItemDropRule) rule2, false);
      rule2.OnSuccess(ItemDropRule.Common(2106, 7, 1, 1), false);
      rule2.OnSuccess(ItemDropRule.Common(1225, 1, 15, 30), false);
      rule2.OnSuccess(ItemDropRule.Common(549, 1, 25, 40), false);
      rule1.OnSuccess(ItemDropRule.MasterModeCommonDrop(4931), false);
      rule1.OnSuccess(ItemDropRule.MasterModeDropOnAllPlayers(4804, this._masterModeDropRng), false);
      this.RegisterToMultipleNPCs((IItemDropRule) rule1, 126, 125);
    }

    private void RegisterBoss_EOC()
    {
      Conditions.NotExpert notExpert = new Conditions.NotExpert();
      Conditions.IsCrimsonAndNotExpert crimsonAndNotExpert = new Conditions.IsCrimsonAndNotExpert();
      Conditions.IsCorruptionAndNotExpert corruptionAndNotExpert = new Conditions.IsCorruptionAndNotExpert();
      short num = 4;
      this.RegisterToNPC((int) num, ItemDropRule.BossBag(3319));
      this.RegisterToNPC((int) num, ItemDropRule.MasterModeCommonDrop(4924));
      this.RegisterToNPC((int) num, ItemDropRule.MasterModeCommonDrop(3763));
      this.RegisterToNPC((int) num, ItemDropRule.MasterModeDropOnAllPlayers(4798, this._masterModeDropRng));
      this.RegisterToNPC((int) num, ItemDropRule.ByCondition((IItemDropRuleCondition) notExpert, 2112, 7, 1, 1, 1));
      this.RegisterToNPC((int) num, ItemDropRule.ByCondition((IItemDropRuleCondition) notExpert, 1299, 40, 1, 1, 1));
      this.RegisterToNPC((int) num, ItemDropRule.ByCondition((IItemDropRuleCondition) crimsonAndNotExpert, 880, 1, 30, 90, 1));
      this.RegisterToNPC((int) num, ItemDropRule.ByCondition((IItemDropRuleCondition) crimsonAndNotExpert, 2171, 1, 1, 3, 1));
      this.RegisterToNPC((int) num, ItemDropRule.ByCondition((IItemDropRuleCondition) corruptionAndNotExpert, 47, 1, 20, 50, 1));
      this.RegisterToNPC((int) num, ItemDropRule.ByCondition((IItemDropRuleCondition) corruptionAndNotExpert, 56, 1, 30, 90, 1));
      this.RegisterToNPC((int) num, ItemDropRule.ByCondition((IItemDropRuleCondition) corruptionAndNotExpert, 59, 1, 1, 3, 1));
    }

    private void RegisterBoss_BOC()
    {
      Conditions.NotExpert notExpert = new Conditions.NotExpert();
      short num1 = 266;
      this.RegisterToNPC((int) num1, ItemDropRule.BossBag(3321));
      this.RegisterToNPC((int) num1, ItemDropRule.MasterModeCommonDrop(4926));
      this.RegisterToNPC((int) num1, ItemDropRule.MasterModeDropOnAllPlayers(4800, this._masterModeDropRng));
      this.RegisterToNPC((int) num1, ItemDropRule.ByCondition((IItemDropRuleCondition) notExpert, 880, 1, 40, 90, 1));
      this.RegisterToNPC((int) num1, ItemDropRule.ByCondition((IItemDropRuleCondition) notExpert, 2104, 7, 1, 1, 1));
      this.RegisterToNPC((int) num1, ItemDropRule.ByCondition((IItemDropRuleCondition) notExpert, 3060, 20, 1, 1, 1));
      short num2 = 267;
      this.RegisterToNPC((int) num2, (IItemDropRule) new DropBasedOnExpertMode((IItemDropRule) new CommonDrop(1329, 3, 2, 5, 2), (IItemDropRule) new CommonDrop(1329, 3, 4, 10, 2)));
      this.RegisterToNPC((int) num2, (IItemDropRule) new DropBasedOnExpertMode((IItemDropRule) new CommonDrop(880, 3, 5, 12, 2), (IItemDropRule) new CommonDrop(880, 3, 11, 25, 2)));
    }

    private void RegisterBoss_EOW()
    {
      Conditions.LegacyHack_IsBossAndExpert hackIsBossAndExpert = new Conditions.LegacyHack_IsBossAndExpert();
      Conditions.LegacyHack_IsBossAndNotExpert bossAndNotExpert = new Conditions.LegacyHack_IsBossAndNotExpert();
      int[] numArray = new int[3]{ 13, 14, 15 };
      this.RegisterToMultipleNPCs((IItemDropRule) new DropBasedOnExpertMode(ItemDropRule.Common(86, 2, 1, 2), ItemDropRule.Common(86, 2, 2, 3)), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(56, 2, 2, 5), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.BossBagByCondition((IItemDropRuleCondition) hackIsBossAndExpert, 3320), numArray);
      IItemDropRule multipleNpCs = this.RegisterToMultipleNPCs((IItemDropRule) new LeadingConditionRule((IItemDropRuleCondition) new Conditions.LegacyHack_IsABoss()), numArray);
      multipleNpCs.OnSuccess(ItemDropRule.MasterModeCommonDrop(4925), false);
      multipleNpCs.OnSuccess(ItemDropRule.MasterModeDropOnAllPlayers(4799, this._masterModeDropRng), false);
      this.RegisterToMultipleNPCs(ItemDropRule.ByCondition((IItemDropRuleCondition) bossAndNotExpert, 56, 1, 20, 60, 1), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.ByCondition((IItemDropRuleCondition) bossAndNotExpert, 994, 20, 1, 1, 1), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.ByCondition((IItemDropRuleCondition) bossAndNotExpert, 2111, 7, 1, 1, 1), numArray);
    }

    private void RegisterBoss_QueenBee()
    {
      Conditions.NotExpert notExpert = new Conditions.NotExpert();
      short num = 222;
      this.RegisterToNPC((int) num, ItemDropRule.BossBag(3322));
      this.RegisterToNPC((int) num, ItemDropRule.MasterModeCommonDrop(4928));
      this.RegisterToNPC((int) num, ItemDropRule.MasterModeDropOnAllPlayers(4802, this._masterModeDropRng));
      this.RegisterToNPC((int) num, ItemDropRule.ByCondition((IItemDropRuleCondition) notExpert, 2108, 7, 1, 1, 1));
      this.RegisterToNPC((int) num, (IItemDropRule) new DropBasedOnExpertMode(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, 1121, 1123, 2888), ItemDropRule.DropNothing()));
      this.RegisterToNPC((int) num, ItemDropRule.ByCondition((IItemDropRuleCondition) notExpert, 1132, 3, 1, 1, 1));
      this.RegisterToNPC((int) num, ItemDropRule.ByCondition((IItemDropRuleCondition) notExpert, 1170, 15, 1, 1, 1));
      this.RegisterToNPC((int) num, ItemDropRule.ByCondition((IItemDropRuleCondition) notExpert, 2502, 20, 1, 1, 1));
      this.RegisterToNPC((int) num, ItemDropRule.ByCondition((IItemDropRuleCondition) notExpert, 1129, 3, 1, 1, 1)).OnFailedRoll(ItemDropRule.OneFromOptionsNotScalingWithLuck(2, 842, 843, 844), false);
      this.RegisterToNPC((int) num, ItemDropRule.ByCondition((IItemDropRuleCondition) notExpert, 1130, 4, 10, 30, 3));
      this.RegisterToNPC((int) num, ItemDropRule.ByCondition((IItemDropRuleCondition) notExpert, 2431, 1, 16, 26, 1));
    }

    private void RegisterBoss_Skeletron()
    {
      Conditions.NotExpert notExpert = new Conditions.NotExpert();
      short num = 35;
      this.RegisterToNPC((int) num, ItemDropRule.BossBag(3323));
      this.RegisterToNPC((int) num, ItemDropRule.MasterModeCommonDrop(4927));
      this.RegisterToNPC((int) num, ItemDropRule.MasterModeDropOnAllPlayers(4801, this._masterModeDropRng));
      this.RegisterToNPC((int) num, ItemDropRule.ByCondition((IItemDropRuleCondition) notExpert, 1281, 7, 1, 1, 1)).OnFailedRoll(ItemDropRule.Common(1273, 7, 1, 1), false).OnFailedRoll(ItemDropRule.Common(1313, 7, 1, 1), false);
      this.RegisterToNPC((int) num, ItemDropRule.Common(4993, 7, 1, 1));
    }

    private void RegisterBoss_WOF()
    {
      Conditions.NotExpert notExpert = new Conditions.NotExpert();
      short num = 113;
      this.RegisterToNPC((int) num, ItemDropRule.BossBag(3324));
      this.RegisterToNPC((int) num, ItemDropRule.MasterModeCommonDrop(4930));
      this.RegisterToNPC((int) num, ItemDropRule.MasterModeDropOnAllPlayers(4795, this._masterModeDropRng));
      this.RegisterToNPC((int) num, ItemDropRule.ByCondition((IItemDropRuleCondition) notExpert, 2105, 7, 1, 1, 1));
      this.RegisterToNPC((int) num, ItemDropRule.ByCondition((IItemDropRuleCondition) notExpert, 367, 1, 1, 1, 1));
      this.RegisterToNPC((int) num, (IItemDropRule) new LeadingConditionRule((IItemDropRuleCondition) notExpert)).OnSuccess((IItemDropRule) new OneFromRulesRule(1, new IItemDropRule[2]
      {
        ItemDropRule.OneFromOptionsNotScalingWithLuck(1, 490, 491, 489, 2998),
        ItemDropRule.OneFromOptionsNotScalingWithLuck(1, 426, 434, 514, 4912)
      }), false);
    }

    private void RegisterBoss_AncientCultist()
    {
      short num = 439;
      this.RegisterToNPC((int) num, ItemDropRule.MasterModeCommonDrop(4937));
      this.RegisterToNPC((int) num, ItemDropRule.MasterModeDropOnAllPlayers(4809, this._masterModeDropRng));
      this.RegisterToNPC((int) num, ItemDropRule.Common(3372, 7, 1, 1));
      this.RegisterToNPC((int) num, ItemDropRule.Common(3549, 1, 1, 1));
    }

    private void RegisterBoss_MoonLord()
    {
      Conditions.NotExpert notExpert = new Conditions.NotExpert();
      short num = 398;
      this.RegisterToNPC((int) num, ItemDropRule.BossBag(3332));
      this.RegisterToNPC((int) num, ItemDropRule.MasterModeCommonDrop(4938));
      this.RegisterToNPC((int) num, ItemDropRule.MasterModeDropOnAllPlayers(4810, this._masterModeDropRng));
      this.RegisterToNPC((int) num, ItemDropRule.ByCondition((IItemDropRuleCondition) notExpert, 3373, 7, 1, 1, 1));
      this.RegisterToNPC((int) num, ItemDropRule.ByCondition((IItemDropRuleCondition) notExpert, 4469, 10, 1, 1, 1));
      this.RegisterToNPC((int) num, ItemDropRule.ByCondition((IItemDropRuleCondition) notExpert, 3384, 1, 1, 1, 1));
      this.RegisterToNPC((int) num, ItemDropRule.ByCondition((IItemDropRuleCondition) notExpert, 3460, 1, 70, 90, 1));
      this.RegisterToNPC((int) num, (IItemDropRule) new LeadingConditionRule((IItemDropRuleCondition) notExpert)).OnSuccess(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, 3063, 3389, 3065, 1553, 3930, 3541, 3570, 3571, 3569), false);
    }

    private void RegisterBoss_LunarTowers()
    {
      DropOneByOne.Parameters parameters1 = new DropOneByOne.Parameters()
      {
        MinimumItemDropsCount = 12,
        MaximumItemDropsCount = 20,
        DropsXOutOfYTimes_TheX = 1,
        DropsXOutOfYTimes_TheY = 1,
        MinimumStackPerChunkBase = 1,
        MaximumStackPerChunkBase = 3,
        BonusMinDropsPerChunkPerPlayer = 0,
        BonusMaxDropsPerChunkPerPlayer = 0
      };
      DropOneByOne.Parameters parameters2 = parameters1;
      parameters2.BonusMinDropsPerChunkPerPlayer = 1;
      parameters2.BonusMaxDropsPerChunkPerPlayer = 1;
      parameters2.MinimumStackPerChunkBase = (int) ((double) parameters1.MinimumStackPerChunkBase * 1.5);
      parameters2.MaximumStackPerChunkBase = (int) ((double) parameters1.MaximumStackPerChunkBase * 1.5);
      this.RegisterToNPC(517, (IItemDropRule) new DropBasedOnExpertMode((IItemDropRule) new DropOneByOne(3458, parameters1), (IItemDropRule) new DropOneByOne(3458, parameters2)));
      this.RegisterToNPC(422, (IItemDropRule) new DropBasedOnExpertMode((IItemDropRule) new DropOneByOne(3456, parameters1), (IItemDropRule) new DropOneByOne(3456, parameters2)));
      this.RegisterToNPC(507, (IItemDropRule) new DropBasedOnExpertMode((IItemDropRule) new DropOneByOne(3457, parameters1), (IItemDropRule) new DropOneByOne(3457, parameters2)));
      this.RegisterToNPC(493, (IItemDropRule) new DropBasedOnExpertMode((IItemDropRule) new DropOneByOne(3459, parameters1), (IItemDropRule) new DropOneByOne(3459, parameters2)));
    }

    private void RegisterBoss_Betsy()
    {
      Conditions.NotExpert notExpert = new Conditions.NotExpert();
      short num = 551;
      this.RegisterToNPC((int) num, ItemDropRule.BossBag(3860));
      this.RegisterToNPC((int) num, ItemDropRule.MasterModeCommonDrop(4948));
      this.RegisterToNPC((int) num, ItemDropRule.MasterModeDropOnAllPlayers(4817, this._masterModeDropRng));
      this.RegisterToNPC((int) num, ItemDropRule.ByCondition((IItemDropRuleCondition) notExpert, 3863, 7, 1, 1, 1));
      this.RegisterToNPC((int) num, ItemDropRule.ByCondition((IItemDropRuleCondition) notExpert, 3883, 4, 1, 1, 1));
      this.RegisterToNPC((int) num, (IItemDropRule) new LeadingConditionRule((IItemDropRuleCondition) notExpert)).OnSuccess(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, 3827, 3859, 3870, 3858), false);
    }

    private void RegisterBoss_Golem()
    {
      Conditions.NotExpert notExpert = new Conditions.NotExpert();
      short num = 245;
      this.RegisterToNPC((int) num, ItemDropRule.BossBag(3329));
      this.RegisterToNPC((int) num, ItemDropRule.MasterModeCommonDrop(4935));
      this.RegisterToNPC((int) num, ItemDropRule.MasterModeDropOnAllPlayers(4807, this._masterModeDropRng));
      this.RegisterToNPC((int) num, ItemDropRule.ByCondition((IItemDropRuleCondition) notExpert, 2110, 7, 1, 1, 1));
      this.RegisterToNPC((int) num, ItemDropRule.ByCondition((IItemDropRuleCondition) notExpert, 1294, 4, 1, 1, 1));
      IItemDropRule rule = ItemDropRule.Common(1258, 1, 1, 1);
      rule.OnSuccess(ItemDropRule.Common(1261, 1, 60, 180), true);
      this.RegisterToNPC((int) num, (IItemDropRule) new LeadingConditionRule((IItemDropRuleCondition) notExpert)).OnSuccess((IItemDropRule) new OneFromRulesRule(1, new IItemDropRule[7]
      {
        rule,
        ItemDropRule.Common(1122, 1, 1, 1),
        ItemDropRule.Common(899, 1, 1, 1),
        ItemDropRule.Common(1248, 1, 1, 1),
        ItemDropRule.Common(1295, 1, 1, 1),
        ItemDropRule.Common(1296, 1, 1, 1),
        ItemDropRule.Common(1297, 1, 1, 1)
      }), false);
      this.RegisterToNPC((int) num, ItemDropRule.ByCondition((IItemDropRuleCondition) notExpert, 2218, 1, 4, 8, 1));
    }

    private void RegisterBoss_DukeFishron()
    {
      Conditions.NotExpert notExpert = new Conditions.NotExpert();
      short num = 370;
      this.RegisterToNPC((int) num, ItemDropRule.BossBag(3330));
      this.RegisterToNPC((int) num, ItemDropRule.MasterModeCommonDrop(4936));
      this.RegisterToNPC((int) num, ItemDropRule.MasterModeDropOnAllPlayers(4808, this._masterModeDropRng));
      this.RegisterToNPC((int) num, ItemDropRule.ByCondition((IItemDropRuleCondition) notExpert, 2588, 7, 1, 1, 1));
      this.RegisterToNPC((int) num, ItemDropRule.ByCondition((IItemDropRuleCondition) notExpert, 2609, 15, 1, 1, 1));
      this.RegisterToNPC((int) num, (IItemDropRule) new LeadingConditionRule((IItemDropRuleCondition) notExpert)).OnSuccess(ItemDropRule.OneFromOptions(1, 2611, 2624, 2622, 2621, 2623), false);
    }

    private void RegisterWeirdRules()
    {
      this.RegisterToMultipleNPCs(ItemDropRule.NormalvsExpert(3260, 40, 30), 86);
    }

    private void RegisterGlobalRules()
    {
      this.RegisterToGlobal((IItemDropRule) new MechBossSpawnersDropRule());
      this.RegisterToGlobal((IItemDropRule) new SlimeBodyItemDropRule());
      this.RegisterToGlobal(ItemDropRule.ByCondition((IItemDropRuleCondition) new Conditions.HalloweenWeapons(), 1825, 2000, 1, 1, 1)).OnFailedRoll(ItemDropRule.Common(1827, 2000, 1, 1), false);
      this.RegisterToGlobal((IItemDropRule) new ItemDropWithConditionRule(1533, 2500, 1, 1, (IItemDropRuleCondition) new Conditions.JungleKeyCondition(), 1));
      this.RegisterToGlobal((IItemDropRule) new ItemDropWithConditionRule(1534, 2500, 1, 1, (IItemDropRuleCondition) new Conditions.CorruptKeyCondition(), 1));
      this.RegisterToGlobal((IItemDropRule) new ItemDropWithConditionRule(1535, 2500, 1, 1, (IItemDropRuleCondition) new Conditions.CrimsonKeyCondition(), 1));
      this.RegisterToGlobal((IItemDropRule) new ItemDropWithConditionRule(1536, 2500, 1, 1, (IItemDropRuleCondition) new Conditions.HallowKeyCondition(), 1));
      this.RegisterToGlobal((IItemDropRule) new ItemDropWithConditionRule(1537, 2500, 1, 1, (IItemDropRuleCondition) new Conditions.FrozenKeyCondition(), 1));
      this.RegisterToGlobal((IItemDropRule) new ItemDropWithConditionRule(4714, 2500, 1, 1, (IItemDropRuleCondition) new Conditions.DesertKeyCondition(), 1));
      this.RegisterToGlobal((IItemDropRule) new ItemDropWithConditionRule(1774, 80, 1, 1, (IItemDropRuleCondition) new Conditions.HalloweenGoodieBagDrop(), 1));
      this.RegisterToGlobal((IItemDropRule) new ItemDropWithConditionRule(1869, 13, 1, 1, (IItemDropRuleCondition) new Conditions.XmasPresentDrop(), 1));
      this.RegisterToGlobal((IItemDropRule) new ItemDropWithConditionRule(2701, 50, 20, 50, (IItemDropRuleCondition) new Conditions.LivingFlames(), 1));
      this.RegisterToGlobal((IItemDropRule) new ItemDropWithConditionRule(1314, 1000, 1, 1, (IItemDropRuleCondition) new Conditions.KOCannon(), 1));
      this.RegisterToGlobal((IItemDropRule) new ItemDropWithConditionRule(520, 5, 1, 1, (IItemDropRuleCondition) new Conditions.SoulOfLight(), 1));
      this.RegisterToGlobal((IItemDropRule) new ItemDropWithConditionRule(521, 5, 1, 1, (IItemDropRuleCondition) new Conditions.SoulOfNight(), 1));
      this.RegisterToGlobal(ItemDropRule.ByCondition((IItemDropRuleCondition) new Conditions.PirateMap(), 1315, 100, 1, 1, 1));
    }

    private void RegisterFoodDrops()
    {
      this.RegisterToNPC(48, ItemDropRule.Food(4016, 50, 1, 1));
      this.RegisterToNPC(224, ItemDropRule.Food(4021, 50, 1, 1));
      this.RegisterToNPC(44, ItemDropRule.Food(4037, 10, 1, 1));
      this.RegisterToNPC(469, ItemDropRule.Food(4037, 100, 1, 1));
      this.RegisterToMultipleNPCs(ItemDropRule.Food(4020, 30, 1, 1), 163, 238, 164, 165, 530, 531);
      this.RegisterToMultipleNPCs(ItemDropRule.Food(4029, 50, 1, 1), 480, 481);
      this.RegisterToMultipleNPCs(ItemDropRule.Food(4030, 75, 1, 1), 498, 499, 500, 501, 502, 503, 504, 505, 506, 496, 497, 494, 495);
      this.RegisterToMultipleNPCs(ItemDropRule.Food(4036, 50, 1, 1), 482, 483);
      this.RegisterToMultipleNPCs(ItemDropRule.Food(4015, 100, 1, 1), 6, 173);
      this.RegisterToMultipleNPCs(ItemDropRule.Food(4026, 150, 1, 1), 150, 147, 184);
      this.RegisterToMultipleNPCs(ItemDropRule.Food(4027, 75, 1, 1), 154, 206);
      this.RegisterToMultipleNPCs(ItemDropRule.Food(3532, 15, 1, 1), 170, 180, 171);
      this.RegisterToNPC(289, ItemDropRule.Food(4018, 35, 1, 1));
      this.RegisterToNPC(34, ItemDropRule.Food(4018, 70, 1, 1));
      this.RegisterToMultipleNPCs(ItemDropRule.Food(4013, 21, 1, 1), 293, 291, 292);
      this.RegisterToMultipleNPCs(ItemDropRule.Food(5042, 30, 1, 1), 43, 175, 56);
      this.RegisterToNPC(287, ItemDropRule.Food(5042, 10, 1, 1));
      this.RegisterToMultipleNPCs(ItemDropRule.Food(5041, 150, 1, 1), 21, 201, 202, 203, 322, 323, 324, 635, 449, 450, 451, 452);
      this.RegisterToNPC(290, ItemDropRule.Food(4013, 7, 1, 1));
      this.RegisterToMultipleNPCs(ItemDropRule.Food(4025, 30, 1, 1), 39, 156);
      this.RegisterToMultipleNPCs(ItemDropRule.Food(4023, 40, 1, 1), 177, 152);
      this.RegisterToMultipleNPCs(ItemDropRule.Food(4012, 50, 1, 1), 581, 509, 580, 508, 69);
      this.RegisterToMultipleNPCs(ItemDropRule.Food(4028, 30, 1, 1), 546, 542, 544, 543, 545);
      this.RegisterToMultipleNPCs(ItemDropRule.Food(4035, 50, 1, 1), 67, 65);
      this.RegisterToMultipleNPCs(ItemDropRule.Food(4011, 150, 1, 1), 120, 137, 138);
      this.RegisterToNPC(122, ItemDropRule.Food(4017, 75, 1, 1));
    }

    private void RegisterTownNPCDrops()
    {
      this.RegisterToNPC(22, (IItemDropRule) new ItemDropWithConditionRule(867, 1, 1, 1, (IItemDropRuleCondition) new Conditions.NamedNPC("Andrew"), 1));
      this.RegisterToNPC(178, (IItemDropRule) new ItemDropWithConditionRule(4372, 1, 1, 1, (IItemDropRuleCondition) new Conditions.NamedNPC("Whitney"), 1));
      this.RegisterToNPC(353, ItemDropRule.Common(3352, 8, 1, 1));
      this.RegisterToNPC(441, ItemDropRule.Common(3351, 8, 1, 1));
      this.RegisterToNPC(227, ItemDropRule.Common(3350, 10, 1, 1));
      this.RegisterToNPC(550, ItemDropRule.Common(3821, 6, 1, 1));
      this.RegisterToNPC(208, ItemDropRule.Common(3548, 4, 30, 60));
      this.RegisterToNPC(207, ItemDropRule.Common(3349, 8, 1, 1));
      this.RegisterToNPC(124, ItemDropRule.Common(4818, 8, 1, 1));
      this.RegisterToNPC(54, ItemDropRule.Common(260, 1, 1, 1));
      this.RegisterToNPC(368, ItemDropRule.Common(2222, 1, 1, 1));
    }

    private void RegisterDD2EventDrops()
    {
      this.RegisterToNPC(576, (IItemDropRule) new CommonDropNotScalingWithLuck(3865, 7, 1, 1));
      this.RegisterToNPC(576, ItemDropRule.NormalvsExpertOneFromOptionsNotScalingWithLuck(3, 2, 3809, 3811, 3810, 3812));
      this.RegisterToNPC(576, ItemDropRule.NormalvsExpertOneFromOptionsNotScalingWithLuck(3, 2, 3852, 3854, 3823, 3835, 3836));
      this.RegisterToNPC(576, ItemDropRule.NormalvsExpertNotScalingWithLuck(3856, 5, 4));
      this.RegisterToNPC(577, (IItemDropRule) new CommonDropNotScalingWithLuck(3865, 14, 1, 1));
      this.RegisterToNPC(577, ItemDropRule.MasterModeCommonDrop(4947));
      this.RegisterToNPC(577, ItemDropRule.MasterModeDropOnAllPlayers(4816, this._masterModeDropRng));
      this.RegisterToNPC(577, ItemDropRule.OneFromOptionsNotScalingWithLuck(6, 3809, 3811, 3810, 3812));
      this.RegisterToNPC(577, ItemDropRule.OneFromOptionsNotScalingWithLuck(6, 3852, 3854, 3823, 3835, 3836));
      this.RegisterToNPC(577, ItemDropRule.Common(3856, 10, 1, 1));
      this.RegisterToNPC(564, ItemDropRule.Common(3864, 7, 1, 1));
      this.RegisterToNPC(564, ItemDropRule.MasterModeDropOnAllPlayers(4796, this._masterModeDropRng));
      this.RegisterToNPC(564, (IItemDropRule) new OneFromRulesRule(5, new IItemDropRule[2]
      {
        ItemDropRule.NotScalingWithLuck(3814, 1, 1, 1),
        ItemDropRule.NotScalingWithLuck(3815, 1, 4, 4)
      }));
      this.RegisterToNPC(564, ItemDropRule.NormalvsExpertOneFromOptionsNotScalingWithLuck(3, 2, 3857, 3855));
      this.RegisterToNPC(565, ItemDropRule.Common(3864, 14, 1, 1));
      this.RegisterToNPC(565, ItemDropRule.MasterModeCommonDrop(4946));
      this.RegisterToNPC(565, ItemDropRule.MasterModeDropOnAllPlayers(4796, this._masterModeDropRng));
      this.RegisterToNPC(565, (IItemDropRule) new OneFromRulesRule(10, new IItemDropRule[2]
      {
        ItemDropRule.NotScalingWithLuck(3814, 1, 1, 1),
        ItemDropRule.NotScalingWithLuck(3815, 1, 4, 4)
      }));
      this.RegisterToNPC(565, ItemDropRule.OneFromOptionsNotScalingWithLuck(6, 3857, 3855));
    }

    private void RegisterHardmodeFeathers()
    {
      this.RegisterToNPC(156, ItemDropRule.Common(1518, 75, 1, 1));
      this.RegisterToNPC(243, ItemDropRule.Common(1519, 3, 1, 1));
      this.RegisterToMultipleNPCs(ItemDropRule.Common(1517, 450, 1, 1), 269, 270, 271, 272, 273, 274, 275, 276, 277, 278, 279, 280);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(1520, 40, 1, 1), 159, 158);
      this.RegisterToNPC(48, ItemDropRule.Common(1516, 200, 1, 1));
      this.RegisterToNPC(176, (IItemDropRule) new ItemDropWithConditionRule(1521, 150, 1, 1, (IItemDropRuleCondition) new Conditions.BeatAnyMechBoss(), 1));
      this.RegisterToNPC(205, (IItemDropRule) new ItemDropWithConditionRule(1611, 2, 1, 1, (IItemDropRuleCondition) new Conditions.BeatAnyMechBoss(), 1));
    }

    private void RegisterYoyos()
    {
      this.RegisterToGlobal((IItemDropRule) new ItemDropWithConditionRule(3282, 400, 1, 1, (IItemDropRuleCondition) new Conditions.YoyoCascade(), 1));
      this.RegisterToGlobal((IItemDropRule) new ItemDropWithConditionRule(3289, 300, 1, 1, (IItemDropRuleCondition) new Conditions.YoyosAmarok(), 1));
      this.RegisterToGlobal((IItemDropRule) new ItemDropWithConditionRule(3286, 200, 1, 1, (IItemDropRuleCondition) new Conditions.YoyosYelets(), 1));
      this.RegisterToGlobal((IItemDropRule) new ItemDropWithConditionRule(3291, 400, 1, 1, (IItemDropRuleCondition) new Conditions.YoyosKraken(), 1));
      this.RegisterToGlobal((IItemDropRule) new ItemDropWithConditionRule(3290, 400, 1, 1, (IItemDropRuleCondition) new Conditions.YoyosHelFire(), 1));
    }

    private void RegisterStatusImmunityItems()
    {
      this.RegisterToMultipleNPCs(ItemDropRule.StatusImmunityItem(3781, 100), 480);
      this.RegisterToMultipleNPCs(ItemDropRule.StatusImmunityItem(885, 100), 104, 102, 269, 270, 271, 272);
      this.RegisterToMultipleNPCs(ItemDropRule.StatusImmunityItem(886, 100), 77, 273, 274, 275, 276);
      this.RegisterToMultipleNPCs(ItemDropRule.StatusImmunityItem(887, 100), 141, 176, 42, 231, 232, 233, 234, 235);
      this.RegisterToMultipleNPCs(ItemDropRule.StatusImmunityItem(888, 100), 81, 79, 183, 630);
      this.RegisterToMultipleNPCs(ItemDropRule.StatusImmunityItem(889, 100), 78, 82, 75);
      this.RegisterToMultipleNPCs(ItemDropRule.StatusImmunityItem(890, 100), 103, 75, 79, 630);
      this.RegisterToMultipleNPCs(ItemDropRule.StatusImmunityItem(891, 100), 34, 83, 84, 179, 289);
      this.RegisterToMultipleNPCs(ItemDropRule.StatusImmunityItem(892, 100), 94, 182);
      this.RegisterToMultipleNPCs(ItemDropRule.StatusImmunityItem(893, 100), 93, 109, 80);
    }

    private void RegisterPirateDrops()
    {
      int[] numArray = new int[4]{ 212, 213, 214, 215 };
      this.RegisterToMultipleNPCs(ItemDropRule.Common(905, 8000, 1, 1), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(855, 4000, 1, 1), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(854, 2000, 1, 1), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(2584, 2000, 1, 1), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(3033, 1000, 1, 1), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(672, 200, 1, 1), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(1277, 500, 1, 1), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(1278, 500, 1, 1), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(1279, 500, 1, 1), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(1280, 500, 1, 1), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(1704, 300, 1, 1), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(1705, 300, 1, 1), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(1710, 300, 1, 1), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(1716, 300, 1, 1), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(1720, 300, 1, 1), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(2379, 300, 1, 1), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(2389, 300, 1, 1), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(2405, 300, 1, 1), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(2843, 300, 1, 1), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(3885, 300, 1, 1), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(2663, 300, 1, 1), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(3904, 150, 30, 50), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(3910, 300, 1, 1), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(2238, 300, 1, 1), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(2133, 300, 1, 1), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(2137, 300, 1, 1), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(2143, 300, 1, 1), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(2147, 300, 1, 1), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(2151, 300, 1, 1), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(2155, 300, 1, 1), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(3263, 500, 1, 1), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(3264, 500, 1, 1), numArray);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(3265, 500, 1, 1), numArray);
      this.RegisterToNPC(216, ItemDropRule.Common(905, 2000, 1, 1));
      this.RegisterToNPC(216, ItemDropRule.Common(855, 1000, 1, 1));
      this.RegisterToNPC(216, ItemDropRule.Common(854, 500, 1, 1));
      this.RegisterToNPC(216, ItemDropRule.Common(2584, 500, 1, 1));
      this.RegisterToNPC(216, ItemDropRule.Common(3033, 250, 1, 1));
      this.RegisterToNPC(216, ItemDropRule.Common(672, 50, 1, 1));
      this.RegisterToNPC(491, ItemDropRule.Common(905, 400, 1, 1));
      this.RegisterToNPC(491, ItemDropRule.Common(855, 200, 1, 1));
      this.RegisterToNPC(491, ItemDropRule.Common(854, 100, 1, 1));
      this.RegisterToNPC(491, ItemDropRule.Common(2584, 100, 1, 1));
      this.RegisterToNPC(491, ItemDropRule.Common(3033, 50, 1, 1));
      this.RegisterToNPC(491, ItemDropRule.Common(4471, 20, 1, 1));
      this.RegisterToNPC(491, ItemDropRule.Common(672, 10, 1, 1));
      this.RegisterToNPC(491, ItemDropRule.MasterModeCommonDrop(4940));
      this.RegisterToNPC(491, ItemDropRule.MasterModeDropOnAllPlayers(4792, this._masterModeDropRng));
    }

    private void RegisterBloodMoonFishingEnemies()
    {
    }

    private void RegisterBossTrophies()
    {
      Conditions.LegacyHack_IsABoss legacyHackIsAboss = new Conditions.LegacyHack_IsABoss();
      this.RegisterToNPC(4, ItemDropRule.ByCondition((IItemDropRuleCondition) legacyHackIsAboss, 1360, 10, 1, 1, 1));
      this.RegisterToNPC(13, ItemDropRule.ByCondition((IItemDropRuleCondition) legacyHackIsAboss, 1361, 10, 1, 1, 1));
      this.RegisterToNPC(14, ItemDropRule.ByCondition((IItemDropRuleCondition) legacyHackIsAboss, 1361, 10, 1, 1, 1));
      this.RegisterToNPC(15, ItemDropRule.ByCondition((IItemDropRuleCondition) legacyHackIsAboss, 1361, 10, 1, 1, 1));
      this.RegisterToNPC(266, ItemDropRule.ByCondition((IItemDropRuleCondition) legacyHackIsAboss, 1362, 10, 1, 1, 1));
      this.RegisterToNPC(35, ItemDropRule.ByCondition((IItemDropRuleCondition) legacyHackIsAboss, 1363, 10, 1, 1, 1));
      this.RegisterToNPC(222, ItemDropRule.ByCondition((IItemDropRuleCondition) legacyHackIsAboss, 1364, 10, 1, 1, 1));
      this.RegisterToNPC(113, ItemDropRule.ByCondition((IItemDropRuleCondition) legacyHackIsAboss, 1365, 10, 1, 1, 1));
      this.RegisterToNPC(134, ItemDropRule.ByCondition((IItemDropRuleCondition) legacyHackIsAboss, 1366, 10, 1, 1, 1));
      this.RegisterToNPC((int) sbyte.MaxValue, ItemDropRule.ByCondition((IItemDropRuleCondition) legacyHackIsAboss, 1367, 10, 1, 1, 1));
      this.RegisterToNPC(262, ItemDropRule.ByCondition((IItemDropRuleCondition) legacyHackIsAboss, 1370, 10, 1, 1, 1));
      this.RegisterToNPC(245, ItemDropRule.ByCondition((IItemDropRuleCondition) legacyHackIsAboss, 1371, 10, 1, 1, 1));
      this.RegisterToNPC(50, ItemDropRule.ByCondition((IItemDropRuleCondition) legacyHackIsAboss, 2489, 10, 1, 1, 1));
      this.RegisterToNPC(370, ItemDropRule.ByCondition((IItemDropRuleCondition) legacyHackIsAboss, 2589, 10, 1, 1, 1));
      this.RegisterToNPC(439, ItemDropRule.ByCondition((IItemDropRuleCondition) legacyHackIsAboss, 3357, 10, 1, 1, 1));
      this.RegisterToNPC(395, ItemDropRule.ByCondition((IItemDropRuleCondition) legacyHackIsAboss, 3358, 10, 1, 1, 1));
      this.RegisterToNPC(398, ItemDropRule.ByCondition((IItemDropRuleCondition) legacyHackIsAboss, 3595, 10, 1, 1, 1));
      this.RegisterToNPC(636, ItemDropRule.ByCondition((IItemDropRuleCondition) legacyHackIsAboss, 4783, 10, 1, 1, 1));
      this.RegisterToNPC(657, ItemDropRule.ByCondition((IItemDropRuleCondition) legacyHackIsAboss, 4958, 10, 1, 1, 1));
      this.RegisterToNPC(125, ItemDropRule.Common(1368, 10, 1, 1));
      this.RegisterToNPC(126, ItemDropRule.Common(1369, 10, 1, 1));
      this.RegisterToNPC(491, ItemDropRule.Common(3359, 10, 1, 1));
      this.RegisterToNPC(551, ItemDropRule.Common(3866, 10, 1, 1));
      this.RegisterToNPC(564, ItemDropRule.Common(3867, 10, 1, 1));
      this.RegisterToNPC(565, ItemDropRule.Common(3867, 10, 1, 1));
      this.RegisterToNPC(576, ItemDropRule.Common(3868, 10, 1, 1));
      this.RegisterToNPC(577, ItemDropRule.Common(3868, 10, 1, 1));
    }

    private void RegisterMartianDrops()
    {
      this.RegisterToMultipleNPCs(ItemDropRule.Common(2860, 8, 8, 20), 520, 383, 389, 385, 382, 381, 390, 386);
      int[] numArray1 = new int[8]
      {
        520,
        383,
        389,
        385,
        382,
        381,
        390,
        386
      };
      this.RegisterToMultipleNPCs(ItemDropRule.Common(2798, 800, 1, 1), numArray1);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(2800, 800, 1, 1), numArray1);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(2882, 800, 1, 1), numArray1);
      int[] numArray2 = new int[3]{ 383, 389, 386 };
      this.RegisterToMultipleNPCs(ItemDropRule.Common(2806, 200, 1, 1), numArray2);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(2807, 200, 1, 1), numArray2);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(2808, 200, 1, 1), numArray2);
      int[] numArray3 = new int[4]{ 385, 382, 381, 390 };
      this.RegisterToMultipleNPCs(ItemDropRule.Common(2803, 200, 1, 1), numArray3);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(2804, 200, 1, 1), numArray3);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(2805, 200, 1, 1), numArray3);
      this.RegisterToNPC(395, ItemDropRule.OneFromOptionsNotScalingWithLuck(1, 2797, 2749, 2795, 2796, 2880, 2769));
      this.RegisterToNPC(395, ItemDropRule.MasterModeCommonDrop(4939));
      this.RegisterToNPC(395, ItemDropRule.MasterModeDropOnAllPlayers(4815, this._masterModeDropRng));
      this.RegisterToNPC(390, ItemDropRule.Common(2771, 100, 1, 1));
    }

    private void RegisterMiscDrops()
    {
      this.RegisterToNPC(68, ItemDropRule.Common(1169, 1, 1, 1));
      this.RegisterToMultipleNPCs(ItemDropRule.Common(3086, 1, 5, 10), 483, 482);
      this.RegisterToNPC(77, ItemDropRule.Common(723, 150, 1, 1));
      this.RegisterToNPC(156, ItemDropRule.Common(683, 30, 1, 1));
      this.RegisterToMultipleNPCs(ItemDropRule.NormalvsExpert(3102, 2, 1), 195, 196);
      this.RegisterToNPC(471, ItemDropRule.NormalvsExpertOneFromOptions(2, 1, 3052, 3053, 3054));
      this.RegisterToNPC(153, ItemDropRule.Common(1328, 17, 1, 1));
      this.RegisterToNPC(120, ItemDropRule.NormalvsExpert(1326, 500, 400));
      this.RegisterToNPC(84, ItemDropRule.Common(4758, 35, 1, 1));
      this.RegisterToNPC(49, ItemDropRule.Common(1325, 250, 1, 1));
      this.RegisterToNPC(634, ItemDropRule.Common(4764, 100, 1, 1));
      this.RegisterToNPC(185, ItemDropRule.Common(951, 150, 1, 1));
      this.RegisterToNPC(44, ItemDropRule.Common(1320, 50, 1, 1));
      this.RegisterToNPC(44, ItemDropRule.Common(88, 20, 1, 1));
      this.RegisterToNPC(60, ItemDropRule.Common(1322, 150, 1, 1));
      this.RegisterToNPC(151, ItemDropRule.Common(1322, 50, 1, 1));
      this.RegisterToNPC(24, ItemDropRule.Common(1323, 50, 1, 1));
      this.RegisterToNPC(109, ItemDropRule.Common(1324, 30, 1, 4));
      int[] numArray1 = new int[2]{ 163, 238 };
      this.RegisterToMultipleNPCs(ItemDropRule.Common(1308, 40, 1, 1), numArray1);
      this.RegisterToMultipleNPCs((IItemDropRule) new DropBasedOnExpertMode(ItemDropRule.Common(2607, 2, 1, 3), (IItemDropRule) new CommonDrop(2607, 10, 1, 3, 9)), numArray1);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(1306, 180, 1, 1), 197, 206, 169, 154);
      this.RegisterToNPC(244, ItemDropRule.Common(23, 1, 1, 20));
      this.RegisterToNPC(244, ItemDropRule.Common(662, 1, 30, 60));
      this.RegisterToNPC(250, ItemDropRule.Common(1244, 15, 1, 1));
      this.RegisterToNPC(172, ItemDropRule.Common(754, 1, 1, 1));
      this.RegisterToNPC(172, ItemDropRule.Common(755, 1, 1, 1));
      this.RegisterToNPC(110, ItemDropRule.Common(682, 200, 1, 1));
      this.RegisterToNPC(110, ItemDropRule.Common(1321, 80, 1, 1));
      this.RegisterToMultipleNPCs(ItemDropRule.Common(4428, 100, 1, 1), 170, 180, 171);
      this.RegisterToMultipleNPCs((IItemDropRule) new ItemDropWithConditionRule(4613, 25, 1, 1, (IItemDropRuleCondition) new Conditions.WindyEnoughForKiteDrops(), 1), 170, 180, 171);
      this.RegisterToNPC(154, ItemDropRule.Common(1253, 100, 1, 1));
      this.RegisterToMultipleNPCs(ItemDropRule.Common(726, 50, 1, 1), 169, 206);
      this.RegisterToNPC(243, ItemDropRule.Common(2161, 1, 1, 1));
      this.RegisterToNPC(480, ItemDropRule.Common(3269, 50, 1, 1));
      int[] numArray2 = new int[3]{ 198, 199, 226 };
      this.RegisterToMultipleNPCs(ItemDropRule.Common(1172, 1000, 1, 1), numArray2);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(1293, 50, 1, 1), numArray2);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(2766, 7, 1, 2), numArray2);
      int[] numArray3 = new int[4]{ 78, 79, 80, 630 };
      this.RegisterToMultipleNPCs(ItemDropRule.Common(870, 75, 1, 1), numArray3);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(871, 75, 1, 1), numArray3);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(872, 75, 1, 1), numArray3);
      this.RegisterToNPC(473, ItemDropRule.OneFromOptions(1, 3008, 3014, 3012, 3015, 3023));
      this.RegisterToNPC(474, ItemDropRule.OneFromOptions(1, 3006, 3007, 3013, 3016, 3020));
      this.RegisterToNPC(475, ItemDropRule.OneFromOptions(1, 3029, 3030, 3051, 3022));
      int[] numArray4 = new int[3]{ 473, 474, 475 };
      this.RegisterToMultipleNPCs(ItemDropRule.Common(499, 1, 5, 10), numArray4);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(500, 1, 5, 15), numArray4);
      this.RegisterToNPC(87, (IItemDropRule) new ItemDropWithConditionRule(4379, 25, 1, 1, (IItemDropRuleCondition) new Conditions.WindyEnoughForKiteDrops(), 1));
      this.RegisterToNPC(87, (IItemDropRule) new DropBasedOnExpertMode(ItemDropRule.Common(575, 1, 5, 10), ItemDropRule.Common(575, 1, 10, 20)));
      this.RegisterToMultipleNPCs(ItemDropRule.OneFromOptions(50, 803, 804, 805), 161, 431);
      this.RegisterToNPC(217, ItemDropRule.Common(1115, 1, 1, 1));
      this.RegisterToNPC(218, ItemDropRule.Common(1116, 1, 1, 1));
      this.RegisterToNPC(219, ItemDropRule.Common(1117, 1, 1, 1));
      this.RegisterToNPC(220, ItemDropRule.Common(1118, 1, 1, 1));
      this.RegisterToNPC(221, ItemDropRule.Common(1119, 1, 1, 1));
      this.RegisterToNPC(167, ItemDropRule.Common(879, 50, 1, 1));
      this.RegisterToNPC(628, ItemDropRule.Common(313, 2, 1, 2));
      this.RegisterToMultipleNPCs(ItemDropRule.Common(593, 1, 5, 10), 143, 144, 145);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(527, 10, 1, 1), 79, 630);
      this.RegisterToNPC(80, ItemDropRule.Common(528, 10, 1, 1));
      this.RegisterToNPC(524, ItemDropRule.Common(3794, 10, 1, 3));
      this.RegisterToNPC(525, ItemDropRule.Common(3794, 10, 1, 1));
      this.RegisterToNPC(525, ItemDropRule.Common(522, 3, 1, 3));
      this.RegisterToNPC(525, ItemDropRule.Common(527, 15, 1, 1));
      this.RegisterToNPC(526, ItemDropRule.Common(3794, 10, 1, 1));
      this.RegisterToNPC(526, ItemDropRule.Common(1332, 3, 1, 3));
      this.RegisterToNPC(526, ItemDropRule.Common(527, 15, 1, 1));
      this.RegisterToNPC(527, ItemDropRule.Common(3794, 10, 1, 1));
      this.RegisterToNPC(527, ItemDropRule.Common(528, 15, 1, 1));
      this.RegisterToNPC(532, ItemDropRule.Common(3380, 3, 1, 1));
      this.RegisterToNPC(532, ItemDropRule.Common(3771, 50, 1, 1));
      this.RegisterToNPC(528, ItemDropRule.Common(2802, 25, 1, 1));
      this.RegisterToNPC(528, ItemDropRule.OneFromOptions(60, 3786, 3785, 3784));
      this.RegisterToNPC(529, ItemDropRule.Common(2801, 25, 1, 1));
      this.RegisterToNPC(529, ItemDropRule.OneFromOptions(40, 3786, 3785, 3784));
      this.RegisterToMultipleNPCs(ItemDropRule.Common(18, 100, 1, 1), 49, 51, 150, 93, 634);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(393, 50, 1, 1), 16, 185, 167, 197);
      this.RegisterToNPC(58, ItemDropRule.Common(393, 75, 1, 1));
      this.RegisterToMultipleNPCs(ItemDropRule.Common(18, 80, 1, 1), 494, 495, 496, 497, 498, 499, 500, 501, 502, 503, 504, 505, 506).OnFailedRoll(ItemDropRule.Common(393, 80, 1, 1), false).OnFailedRoll(ItemDropRule.Common(3285, 25, 1, 1), false);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(954, 100, 1, 1), 21, 201, 202, 203, 322, 323, 324, 635, 449, 450, 451, 452).OnFailedRoll(ItemDropRule.Common(955, 200, 1, 1), false).OnFailedRoll(ItemDropRule.Common(1166, 200, 1, 1), false).OnFailedRoll(ItemDropRule.Common(1274, 500, 1, 1), false);
      this.RegisterToNPC(6, ItemDropRule.OneFromOptions(175, 956, 957, 958));
      this.RegisterToMultipleNPCs(ItemDropRule.OneFromOptions(100, 960, 961, 962), 42, 43, 231, 232, 233, 234, 235);
      int[] numArray5 = new int[5]{ 31, 32, 294, 295, 296 };
      this.RegisterToMultipleNPCs(ItemDropRule.Common(959, 450, 1, 1), numArray5);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(1307, 300, 1, 1), numArray5);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(996, 200, 1, 1), 174, 179, 182, 183, 98, 83, 94, 81, 101);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(522, 1, 2, 5), 101, 98);
      this.RegisterToNPC(98, ItemDropRule.ByCondition((IItemDropRuleCondition) new Conditions.WindyEnoughForKiteDrops(), 4611, 25, 1, 1, 1));
      this.RegisterToNPC(86, ItemDropRule.Common(526, 1, 1, 1));
      this.RegisterToNPC(86, ItemDropRule.Common(856, 100, 1, 1));
      this.RegisterToNPC(86, ItemDropRule.ByCondition((IItemDropRuleCondition) new Conditions.WindyEnoughForKiteDrops(), 4684, 25, 1, 1, 1));
      this.RegisterToNPC(224, ItemDropRule.Common(4057, 100, 1, 1));
      this.RegisterToMultipleNPCs(ItemDropRule.Common(40, 1, 1, 9), 186, 432);
      this.RegisterToNPC(225, ItemDropRule.Common(1243, 45, 1, 1)).OnFailedRoll(ItemDropRule.Common(23, 1, 2, 6), false);
      this.RegisterToNPC(537, ItemDropRule.Common(23, 1, 2, 3));
      this.RegisterToNPC(537, ItemDropRule.NormalvsExpert(1309, 8000, 5600));
      this.RegisterToMultipleNPCs(ItemDropRule.Common(1906, 20, 1, 1), 335, 336, 333, 334);
      this.RegisterToNPC(-4, ItemDropRule.Common(3111, 1, 25, 50));
      this.RegisterToNPC(-4, ItemDropRule.NormalvsExpert(1309, 100, 70));
      int[] numArray6 = new int[15]
      {
        1,
        16,
        138,
        141,
        147,
        184,
        187,
        433,
        204,
        302,
        333,
        334,
        335,
        336,
        535
      };
      int[] numArray7 = new int[4]{ -6, -7, -8, -9 };
      int[] numArray8 = new int[5]{ -6, -7, -8, -9, -4 };
      this.RemoveFromMultipleNPCs(this.RegisterToMultipleNPCs(ItemDropRule.Common(23, 1, 1, 2), numArray6), numArray8);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(23, 1, 2, 5), numArray7);
      this.RemoveFromMultipleNPCs(this.RegisterToMultipleNPCs(ItemDropRule.NormalvsExpert(1309, 10000, 7000), numArray6), numArray8);
      this.RegisterToMultipleNPCs(ItemDropRule.NormalvsExpert(1309, 10000, 7000), numArray7);
      this.RegisterToNPC(75, ItemDropRule.Common(501, 1, 1, 3));
      this.RegisterToMultipleNPCs(ItemDropRule.Common(23, 1, 2, 4), 81, 183);
      this.RegisterToNPC(122, ItemDropRule.Common(23, 1, 5, 10));
      this.RegisterToNPC(71, ItemDropRule.Common(327, 1, 1, 1));
      this.RegisterToMultipleNPCs(ItemDropRule.Common(236, 100, 1, 1), 2, 317, 318, 190, 191, 192, 193, 194, 133).OnFailedRoll(ItemDropRule.Common(38, 3, 1, 1), false);
      this.RegisterToNPC(133, ItemDropRule.ByCondition((IItemDropRuleCondition) new Conditions.WindyEnoughForKiteDrops(), 4683, 25, 1, 1, 1));
      this.RegisterToNPC(104, ItemDropRule.Common(485, 60, 1, 1));
      this.RegisterToNPC(58, ItemDropRule.Common(263, 250, 1, 1)).OnFailedRoll(ItemDropRule.Common(118, 30, 1, 1), false);
      this.RegisterToNPC(102, ItemDropRule.Common(263, 250, 1, 1));
      int[] numArray9 = new int[23]
      {
        3,
        591,
        590,
        331,
        332,
        132,
        161,
        186,
        187,
        188,
        189,
        200,
        223,
        319,
        320,
        321,
        430,
        431,
        432,
        433,
        434,
        435,
        436
      };
      this.RegisterToMultipleNPCs(ItemDropRule.Common(216, 50, 1, 1), numArray9);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(1304, 250, 1, 1), numArray9);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(8, 1, 5, 20), 590, 591);
      this.RegisterToMultipleNPCs(ItemDropRule.NormalvsExpert(3212, 150, 75), 489, 490);
      this.RegisterToMultipleNPCs(ItemDropRule.NormalvsExpert(3213, 200, 100), 489, 490);
      this.RegisterToNPC(223, ItemDropRule.OneFromOptions(20, 1135, 1136));
      this.RegisterToNPC(66, ItemDropRule.Common(267, 1, 1, 1));
      this.RegisterToMultipleNPCs(ItemDropRule.Common(272, 35, 1, 1), 62, 66);
      this.RegisterToNPC(52, ItemDropRule.Common(251, 1, 1, 1));
      this.RegisterToNPC(53, ItemDropRule.Common(239, 1, 1, 1));
      this.RegisterToNPC(536, ItemDropRule.Common(3478, 1, 1, 1));
      this.RegisterToNPC(536, ItemDropRule.Common(3479, 1, 1, 1));
      this.RegisterToMultipleNPCs(ItemDropRule.Common(323, 3, 1, 2), 69, 581, 580, 508, 509);
      this.RegisterToNPC(582, ItemDropRule.Common(323, 6, 1, 1));
      this.RegisterToMultipleNPCs(ItemDropRule.Common(3772, 50, 1, 1), 581, 580, 508, 509);
      this.RegisterToNPC(73, ItemDropRule.Common(362, 1, 1, 2));
      int[] numArray10 = new int[2]{ 483, 482 };
      this.RegisterToMultipleNPCs(ItemDropRule.Common(3109, 30, 1, 1), numArray10);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(4400, 20, 1, 1), numArray10);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(68, 3, 1, 1), 6, 94);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(1330, 3, 1, 1), 181, 173, 239, 182, 240);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(68, 3, 1, 2), 7, 8, 9);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(69, 1, 3, 8), 7, 8, 9);
      this.RegisterToMultipleNPCs((IItemDropRule) new DropBasedOnExpertMode(ItemDropRule.Common(215, 50, 1, 1), ItemDropRule.WithRerolls(215, 1, 50, 1, 1)), 10, 11, 12, 95, 96, 97);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(243, 75, 1, 1), 47, 464);
      this.RegisterToMultipleNPCs(ItemDropRule.OneFromOptions(50, 3757, 3758, 3759), 168, 470);
      this.RegisterToNPC(533, ItemDropRule.Common(3795, 40, 1, 1)).OnFailedRoll(ItemDropRule.Common(3770, 30, 1, 1), false);
      int[] numArray11 = new int[3]{ 63, 103, 64 };
      this.RegisterToMultipleNPCs(ItemDropRule.Common(1303, 100, 1, 1), numArray11);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(282, 1, 1, 4), numArray11);
      this.RegisterToNPC(63, ItemDropRule.ByCondition((IItemDropRuleCondition) new Conditions.WindyEnoughForKiteDrops(), 4649, 50, 1, 1, 1));
      this.RegisterToNPC(64, ItemDropRule.ByCondition((IItemDropRuleCondition) new Conditions.WindyEnoughForKiteDrops(), 4650, 50, 1, 1, 1));
      this.RegisterToNPC(481, ItemDropRule.Common(3094, 2, 40, 80));
      this.RegisterToNPC(481, ItemDropRule.OneFromOptions(20, 3187, 3188, 3189));
      this.RegisterToNPC(481, ItemDropRule.Common(4463, 40, 1, 1));
      this.RegisterToMultipleNPCs(ItemDropRule.Common(118, 25, 1, 1), 21, 167, 201, 202, 481, 203, 322, 323, 324, 449, 450, 451, 452);
      this.RegisterToNPC(44, ItemDropRule.Common(118, 25, 1, 1)).OnFailedRoll(ItemDropRule.OneFromOptions(20, 410, 411), false).OnFailedRoll(ItemDropRule.Common(166, 1, 1, 3), false);
      this.RegisterToNPC(45, ItemDropRule.Common(238, 1, 1, 1));
      this.RegisterToNPC(23, ItemDropRule.Common(116, 50, 1, 1));
      this.RegisterToNPC(24, ItemDropRule.Common(244, 250, 1, 1));
      int[] numArray12 = new int[6]
      {
        31,
        32,
        34,
        294,
        295,
        296
      };
      this.RegisterToMultipleNPCs(ItemDropRule.Common(932, 250, 1, 1), numArray12).OnFailedRoll(ItemDropRule.Common(3095, 100, 1, 1), false).OnFailedRoll(ItemDropRule.Common(327, 65, 1, 1), false).OnFailedRoll(ItemDropRule.ByCondition((IItemDropRuleCondition) new Conditions.NotExpert(), 154, 1, 1, 3, 1), false);
      this.RegisterToMultipleNPCs(ItemDropRule.ByCondition((IItemDropRuleCondition) new Conditions.IsExpert(), 154, 1, 2, 6, 1), numArray12);
      this.RegisterToMultipleNPCs(ItemDropRule.Common(160, 200, 1, 1), 26, 27, 28, 29, 111).OnFailedRoll(ItemDropRule.Common(161, 2, 1, 5), false);
      this.RegisterToNPC(175, ItemDropRule.Common(1265, 100, 1, 1));
      this.RegisterToNPC(175, ItemDropRule.ByCondition((IItemDropRuleCondition) new Conditions.WindyEnoughForKiteDrops(), 4675, 25, 1, 1, 1));
      this.RegisterToMultipleNPCs((IItemDropRule) new DropBasedOnExpertMode((IItemDropRule) new CommonDrop(209, 3, 1, 1, 2), ItemDropRule.Common(209, 1, 1, 1)), 42, 231, 232, 233, 234, 235);
      this.RegisterToNPC(204, ItemDropRule.NormalvsExpert(209, 2, 1));
      this.RegisterToNPC(43, ItemDropRule.NormalvsExpert(210, 2, 1));
      this.RegisterToNPC(43, ItemDropRule.ByCondition((IItemDropRuleCondition) new Conditions.WindyEnoughForKiteDrops(), 4648, 25, 1, 1, 1));
      this.RegisterToNPC(39, ItemDropRule.ByCondition((IItemDropRuleCondition) new Conditions.WindyEnoughForKiteDrops(), 4610, 25, 1, 1, 1));
      this.RegisterToNPC(65, ItemDropRule.ByCondition((IItemDropRuleCondition) new Conditions.WindyEnoughForKiteDrops(), 4651, 25, 1, 1, 1));
      this.RegisterToNPC(65, ItemDropRule.Common(268, 50, 1, 1)).OnFailedRoll(ItemDropRule.Common(319, 1, 1, 1), false);
      this.RegisterToNPC(48, ItemDropRule.NotScalingWithLuck(320, 2, 1, 1));
      this.RegisterToNPC(541, ItemDropRule.Common(3783, 1, 1, 1));
      this.RegisterToMultipleNPCs(ItemDropRule.Common(319, 8, 1, 1), 542, 543, 544, 545);
      this.RegisterToMultipleNPCs(ItemDropRule.ByCondition((IItemDropRuleCondition) new Conditions.WindyEnoughForKiteDrops(), 4669, 25, 1, 1, 1), 542, 543, 544, 545);
      this.RegisterToNPC(543, ItemDropRule.Common(527, 25, 1, 1));
      this.RegisterToNPC(544, ItemDropRule.Common(527, 25, 1, 1));
      this.RegisterToNPC(545, ItemDropRule.Common(528, 25, 1, 1));
      this.RegisterToNPC(47, ItemDropRule.ByCondition((IItemDropRuleCondition) new Conditions.WindyEnoughForKiteDrops(), 4670, 25, 1, 1, 1));
      this.RegisterToNPC(464, ItemDropRule.ByCondition((IItemDropRuleCondition) new Conditions.WindyEnoughForKiteDrops(), 4671, 25, 1, 1, 1));
      this.RegisterToNPC(268, ItemDropRule.Common(1332, 1, 2, 5));
      this.RegisterToNPC(631, ItemDropRule.Common(3, 1, 10, 20));
      this.RegisterToNPC(631, ItemDropRule.Common(4761, 20, 1, 1));
      int[] numArray13 = new int[1]{ 594 };
      LeadingConditionRule rule1 = new LeadingConditionRule((IItemDropRuleCondition) new Conditions.NeverTrue());
      int[] numArray14 = new int[0];
      IItemDropRule rule2 = rule1.OnSuccess(ItemDropRule.OneFromOptions(20, numArray14), false);
      int dropsOutOfY = 13;
      rule2.OnSuccess((IItemDropRule) new CommonDrop(4367, dropsOutOfY, 1, 1, 1), false);
      rule2.OnSuccess((IItemDropRule) new CommonDrop(4368, dropsOutOfY, 1, 1, 1), false);
      rule2.OnSuccess((IItemDropRule) new CommonDrop(4369, dropsOutOfY, 1, 1, 1), false);
      rule2.OnSuccess((IItemDropRule) new CommonDrop(4370, dropsOutOfY, 1, 1, 1), false);
      rule2.OnSuccess((IItemDropRule) new CommonDrop(4371, dropsOutOfY, 1, 1, 1), false);
      rule2.OnSuccess((IItemDropRule) new CommonDrop(4612, dropsOutOfY, 1, 1, 1), false);
      rule2.OnSuccess((IItemDropRule) new CommonDrop(4674, dropsOutOfY, 1, 1, 1), false);
      rule2.OnSuccess((IItemDropRule) new CommonDrop(4343, dropsOutOfY, 1, 1, 3), false);
      rule2.OnSuccess((IItemDropRule) new CommonDrop(4344, dropsOutOfY, 1, 1, 3), false);
      this.RegisterToMultipleNPCs((IItemDropRule) rule1, numArray13);
    }
  }
}
