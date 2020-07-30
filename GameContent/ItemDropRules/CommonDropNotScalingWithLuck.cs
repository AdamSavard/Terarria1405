// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.ItemDropRules.CommonDropNotScalingWithLuck
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

namespace Terraria.GameContent.ItemDropRules
{
  public class CommonDropNotScalingWithLuck : CommonDrop
  {
    public CommonDropNotScalingWithLuck(
      int itemId,
      int dropsOutOfY,
      int amountDroppedMinimum,
      int amountDroppedMaximum)
      : base(itemId, dropsOutOfY, amountDroppedMinimum, amountDroppedMaximum, 1)
    {
    }

    public override ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
    {
      if (info.rng.Next(this._dropsOutOfY) < this._dropsXoutOfY)
      {
        CommonCode.DropItemFromNPC(info.npc, this._itemId, info.rng.Next(this._amtDroppedMinimum, this._amtDroppedMaximum + 1), false);
        return new ItemDropAttemptResult()
        {
          State = ItemDropAttemptResultState.Success
        };
      }
      return new ItemDropAttemptResult()
      {
        State = ItemDropAttemptResultState.FailedRandomRoll
      };
    }
  }
}
