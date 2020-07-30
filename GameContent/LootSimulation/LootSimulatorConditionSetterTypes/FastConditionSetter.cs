// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.LootSimulation.LootSimulatorConditionSetterTypes.FastConditionSetter
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using System;

namespace Terraria.GameContent.LootSimulation.LootSimulatorConditionSetterTypes
{
  public class FastConditionSetter : ISimulationConditionSetter
  {
    private Action<SimulatorInfo> _setup;
    private Action<SimulatorInfo> _tearDown;

    public FastConditionSetter(Action<SimulatorInfo> setup, Action<SimulatorInfo> tearDown)
    {
      this._setup = setup;
      this._tearDown = tearDown;
    }

    public void Setup(SimulatorInfo info)
    {
      if (this._setup == null)
        return;
      this._setup(info);
    }

    public void TearDown(SimulatorInfo info)
    {
      if (this._tearDown == null)
        return;
      this._tearDown(info);
    }

    public int GetTimesToRunMultiplier(SimulatorInfo info)
    {
      return 1;
    }
  }
}
