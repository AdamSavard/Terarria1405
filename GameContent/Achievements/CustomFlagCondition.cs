// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Achievements.CustomFlagCondition
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Terraria.Achievements;

namespace Terraria.GameContent.Achievements
{
  public class CustomFlagCondition : AchievementCondition
  {
    private CustomFlagCondition(string name)
      : base(name)
    {
    }

    public static AchievementCondition Create(string name)
    {
      return (AchievementCondition) new CustomFlagCondition(name);
    }
  }
}
