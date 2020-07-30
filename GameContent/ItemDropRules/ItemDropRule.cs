// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.ItemDropRules.ItemDropRule
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

namespace Terraria.GameContent.ItemDropRules
{
  public class ItemDropRule
  {
    public static IItemDropRule Common(
      int itemId,
      int dropsOutOfX = 1,
      int minimumDropped = 1,
      int maximumDropped = 1)
    {
      return (IItemDropRule) new CommonDrop(itemId, dropsOutOfX, minimumDropped, maximumDropped, 1);
    }

    public static IItemDropRule BossBag(int itemId)
    {
      return (IItemDropRule) new DropBasedOnExpertMode(ItemDropRule.DropNothing(), (IItemDropRule) new DropLocalPerClientAndResetsNPCMoneyTo0(itemId, 1, 1, 1, (IItemDropRuleCondition) null));
    }

    public static IItemDropRule BossBagByCondition(
      IItemDropRuleCondition condition,
      int itemId)
    {
      return (IItemDropRule) new DropBasedOnExpertMode(ItemDropRule.DropNothing(), (IItemDropRule) new DropLocalPerClientAndResetsNPCMoneyTo0(itemId, 1, 1, 1, condition));
    }

    public static IItemDropRule ExpertGetsRerolls(
      int itemId,
      int dropsOutOfX,
      int expertRerolls)
    {
      return (IItemDropRule) new DropBasedOnExpertMode(ItemDropRule.WithRerolls(itemId, 0, dropsOutOfX, 1, 1), ItemDropRule.WithRerolls(itemId, expertRerolls, dropsOutOfX, 1, 1));
    }

    public static IItemDropRule MasterModeCommonDrop(int itemId)
    {
      return ItemDropRule.ByCondition((IItemDropRuleCondition) new Conditions.IsMasterMode(), itemId, 1, 1, 1, 1);
    }

    public static IItemDropRule MasterModeDropOnAllPlayers(
      int itemId,
      int dropsAtXOutOfY_TheY = 1)
    {
      return (IItemDropRule) new DropBasedOnMasterMode(ItemDropRule.DropNothing(), (IItemDropRule) new DropPerPlayerOnThePlayer(itemId, dropsAtXOutOfY_TheY, 1, 1, (IItemDropRuleCondition) new Conditions.IsMasterMode()));
    }

    public static IItemDropRule WithRerolls(
      int itemId,
      int rerolls,
      int dropsOutOfX = 1,
      int minimumDropped = 1,
      int maximumDropped = 1)
    {
      return (IItemDropRule) new CommonDropWithRerolls(itemId, dropsOutOfX, minimumDropped, maximumDropped, rerolls);
    }

    public static IItemDropRule ByCondition(
      IItemDropRuleCondition condition,
      int itemId,
      int dropsOutOfX = 1,
      int minimumDropped = 1,
      int maximumDropped = 1,
      int dropsXOutOfY = 1)
    {
      return (IItemDropRule) new ItemDropWithConditionRule(itemId, dropsOutOfX, minimumDropped, maximumDropped, condition, dropsXOutOfY);
    }

    public static IItemDropRule NotScalingWithLuck(
      int itemId,
      int dropsOutOfX = 1,
      int minimumDropped = 1,
      int maximumDropped = 1)
    {
      return (IItemDropRule) new CommonDrop(itemId, dropsOutOfX, minimumDropped, maximumDropped, 1);
    }

    public static IItemDropRule OneFromOptionsNotScalingWithLuck(
      int dropsOutOfX,
      params int[] options)
    {
      return (IItemDropRule) new OneFromOptionsNotScaledWithLuckDropRule(dropsOutOfX, 1, options);
    }

    public static IItemDropRule OneFromOptionsNotScalingWithLuckWithX(
      int dropsOutOfY,
      int xOutOfY,
      params int[] options)
    {
      return (IItemDropRule) new OneFromOptionsNotScaledWithLuckDropRule(dropsOutOfY, xOutOfY, options);
    }

    public static IItemDropRule OneFromOptions(int dropsOutOfX, params int[] options)
    {
      return (IItemDropRule) new OneFromOptionsDropRule(dropsOutOfX, 1, options);
    }

    public static IItemDropRule OneFromOptionsWithX(
      int dropsOutOfY,
      int xOutOfY,
      params int[] options)
    {
      return (IItemDropRule) new OneFromOptionsDropRule(dropsOutOfY, xOutOfY, options);
    }

    public static IItemDropRule DropNothing()
    {
      return (IItemDropRule) new Terraria.GameContent.ItemDropRules.DropNothing();
    }

    public static IItemDropRule NormalvsExpert(
      int itemId,
      int oncePerXInNormal,
      int oncePerXInExpert)
    {
      return (IItemDropRule) new DropBasedOnExpertMode(ItemDropRule.Common(itemId, oncePerXInNormal, 1, 1), ItemDropRule.Common(itemId, oncePerXInExpert, 1, 1));
    }

    public static IItemDropRule NormalvsExpertNotScalingWithLuck(
      int itemId,
      int oncePerXInNormal,
      int oncePerXInExpert)
    {
      return (IItemDropRule) new DropBasedOnExpertMode(ItemDropRule.NotScalingWithLuck(itemId, oncePerXInNormal, 1, 1), ItemDropRule.NotScalingWithLuck(itemId, oncePerXInExpert, 1, 1));
    }

    public static IItemDropRule NormalvsExpertOneFromOptionsNotScalingWithLuck(
      int dropsOutOfXNormalMode,
      int dropsOutOfXExpertMode,
      params int[] options)
    {
      return (IItemDropRule) new DropBasedOnExpertMode(ItemDropRule.OneFromOptionsNotScalingWithLuck(dropsOutOfXNormalMode, options), ItemDropRule.OneFromOptionsNotScalingWithLuck(dropsOutOfXExpertMode, options));
    }

    public static IItemDropRule NormalvsExpertOneFromOptions(
      int dropsOutOfXNormalMode,
      int dropsOutOfXExpertMode,
      params int[] options)
    {
      return (IItemDropRule) new DropBasedOnExpertMode(ItemDropRule.OneFromOptions(dropsOutOfXNormalMode, options), ItemDropRule.OneFromOptions(dropsOutOfXExpertMode, options));
    }

    public static IItemDropRule Food(
      int itemId,
      int dropsOutOfX,
      int minimumDropped = 1,
      int maximumDropped = 1)
    {
      return (IItemDropRule) new ItemDropWithConditionRule(itemId, dropsOutOfX, minimumDropped, maximumDropped, (IItemDropRuleCondition) new Conditions.NotFromStatue(), 1);
    }

    public static IItemDropRule StatusImmunityItem(int itemId, int dropsOutOfX)
    {
      return ItemDropRule.ExpertGetsRerolls(itemId, dropsOutOfX, 1);
    }
  }
}
