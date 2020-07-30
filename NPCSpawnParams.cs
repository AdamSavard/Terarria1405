// Decompiled with JetBrains decompiler
// Type: Terraria.NPCSpawnParams
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Terraria.DataStructures;

namespace Terraria
{
  public struct NPCSpawnParams
  {
    public float? sizeScaleOverride;
    public int? playerCountForMultiplayerDifficultyOverride;
    public GameModeData gameModeData;
    public float? strengthMultiplierOverride;

    public NPCSpawnParams WithScale(float scaleOverride)
    {
      return new NPCSpawnParams()
      {
        sizeScaleOverride = new float?(scaleOverride),
        playerCountForMultiplayerDifficultyOverride = this.playerCountForMultiplayerDifficultyOverride,
        gameModeData = this.gameModeData,
        strengthMultiplierOverride = this.strengthMultiplierOverride
      };
    }
  }
}
