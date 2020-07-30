// Decompiled with JetBrains decompiler
// Type: Terraria.Physics.PhysicsProperties
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

namespace Terraria.Physics
{
  public class PhysicsProperties
  {
    public readonly float Gravity;
    public readonly float Drag;

    public PhysicsProperties(float gravity, float drag)
    {
      this.Gravity = gravity;
      this.Drag = drag;
    }
  }
}
