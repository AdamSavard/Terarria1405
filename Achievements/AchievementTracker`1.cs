﻿// Decompiled with JetBrains decompiler
// Type: Terraria.Achievements.AchievementTracker`1
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Terraria.Social;

namespace Terraria.Achievements
{
  public abstract class AchievementTracker<T> : IAchievementTracker
  {
    protected T _value;
    protected T _maxValue;
    protected string _name;
    private TrackerType _type;

    public T Value
    {
      get
      {
        return this._value;
      }
    }

    public T MaxValue
    {
      get
      {
        return this._maxValue;
      }
    }

    protected AchievementTracker(TrackerType type)
    {
      this._type = type;
    }

    void IAchievementTracker.ReportAs(string name)
    {
      this._name = name;
    }

    TrackerType IAchievementTracker.GetTrackerType()
    {
      return this._type;
    }

    void IAchievementTracker.Clear()
    {
      this.SetValue(default (T), true);
    }

    public void SetValue(T newValue, bool reportUpdate = true)
    {
      if (newValue.Equals((object) this._value))
        return;
      this._value = newValue;
      if (!reportUpdate)
        return;
      this.ReportUpdate();
      if (!this._value.Equals((object) this._maxValue))
        return;
      this.OnComplete();
    }

    public abstract void ReportUpdate();

    protected abstract void Load();

    void IAchievementTracker.Load()
    {
      this.Load();
    }

    protected void OnComplete()
    {
      if (SocialAPI.Achievements == null)
        return;
      SocialAPI.Achievements.StoreStats();
    }
  }
}
