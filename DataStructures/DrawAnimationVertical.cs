// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.DrawAnimationVertical
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.DataStructures
{
  public class DrawAnimationVertical : DrawAnimation
  {
    public bool PingPong;
    public bool NotActuallyAnimating;

    public DrawAnimationVertical(int ticksperframe, int frameCount, bool pingPong = false)
    {
      this.Frame = 0;
      this.FrameCounter = 0;
      this.FrameCount = frameCount;
      this.TicksPerFrame = ticksperframe;
      this.PingPong = pingPong;
    }

    public override void Update()
    {
      if (this.NotActuallyAnimating)
        return;
      if (++this.FrameCounter < this.TicksPerFrame)
        return;
      this.FrameCounter = 0;
      if (this.PingPong)
      {
        if (++this.Frame < this.FrameCount * 2 - 2)
          return;
        this.Frame = 0;
      }
      else
      {
        if (++this.Frame < this.FrameCount)
          return;
        this.Frame = 0;
      }
    }

    public override Rectangle GetFrame(Texture2D texture, int frameCounterOverride = -1)
    {
      if (frameCounterOverride != -1)
      {
        int num1 = frameCounterOverride / this.TicksPerFrame;
        int num2 = this.FrameCount;
        if (this.PingPong)
          num2 = num2 * 2 - 1;
        int num3 = num2;
        int frameY = num1 % num3;
        if (this.PingPong && frameY >= this.FrameCount)
          frameY = this.FrameCount * 2 - 2 - frameY;
        Rectangle rectangle = texture.Frame(1, this.FrameCount, 0, frameY, 0, 0);
        rectangle.Height -= 2;
        return rectangle;
      }
      int frameY1 = this.Frame;
      if (this.PingPong && this.Frame >= this.FrameCount)
        frameY1 = this.FrameCount * 2 - 2 - this.Frame;
      Rectangle rectangle1 = texture.Frame(1, this.FrameCount, 0, frameY1, 0, 0);
      rectangle1.Height -= 2;
      return rectangle1;
    }
  }
}
