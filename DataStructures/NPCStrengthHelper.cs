// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.NPCStrengthHelper
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

namespace Terraria.DataStructures
{
  public struct NPCStrengthHelper
  {
    private float _strength;
    private GameModeData _gameModeData;

    public bool IsExpertMode
    {
      get
      {
        return (double) this._strength >= 2.0 || this._gameModeData.IsExpertMode;
      }
    }

    public bool IsMasterMode
    {
      get
      {
        return (double) this._strength >= 3.0 || this._gameModeData.IsMasterMode;
      }
    }

    public NPCStrengthHelper(GameModeData data, float strength)
    {
      this._strength = strength;
      this._gameModeData = data;
    }
  }
}
