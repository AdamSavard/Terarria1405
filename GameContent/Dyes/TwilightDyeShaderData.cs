// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Dyes.TwilightDyeShaderData
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Dyes
{
  public class TwilightDyeShaderData : ArmorShaderData
  {
    public TwilightDyeShaderData(Ref<Effect> shader, string passName)
      : base(shader, passName)
    {
    }

    public override void Apply(Entity entity, DrawData? drawData)
    {
      if (drawData.HasValue)
      {
        switch (entity)
        {
          case Player player when !player.isDisplayDollOrInanimate && !player.isHatRackDoll:
            this.UseTargetPosition(Main.screenPosition + drawData.Value.position);
            break;
          case Projectile _:
            this.UseTargetPosition(Main.screenPosition + drawData.Value.position);
            break;
          default:
            this.UseTargetPosition(drawData.Value.position);
            break;
        }
      }
      base.Apply(entity, drawData);
    }
  }
}
