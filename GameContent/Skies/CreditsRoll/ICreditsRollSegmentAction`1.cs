// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Skies.CreditsRoll.ICreditsRollSegmentAction`1
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

namespace Terraria.GameContent.Skies.CreditsRoll
{
  public interface ICreditsRollSegmentAction<T>
  {
    void BindTo(T obj);

    int ExpectedLengthOfActionInFrames { get; }

    void ApplyTo(T obj, float localTimeForObj);

    void SetDelay(float delay);
  }
}
