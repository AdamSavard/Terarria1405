﻿// Decompiled with JetBrains decompiler
// Type: Terraria.GameInput.SmartSelectGamepadPointer
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System;

namespace Terraria.GameInput
{
  public class SmartSelectGamepadPointer
  {
    private Vector2 _distUniform = new Vector2(80f, 64f);
    private Vector2 _size;
    private Vector2 _center;

    public bool ShouldBeUsed()
    {
      return PlayerInput.UsingGamepad && Main.LocalPlayer.controlTorch && Main.SmartCursorEnabled;
    }

    public void SmartSelectLookup_GetTargetTile(Player player, out int tX, out int tY)
    {
      tX = (int) (((double) Main.mouseX + (double) Main.screenPosition.X) / 16.0);
      tY = (int) (((double) Main.mouseY + (double) Main.screenPosition.Y) / 16.0);
      if ((double) player.gravDir == -1.0)
        tY = (int) (((double) Main.screenPosition.Y + (double) Main.screenHeight - (double) Main.mouseY) / 16.0);
      if (!this.ShouldBeUsed())
        return;
      Point point = this.GetPointerPosition().ToPoint();
      tX = (int) (((double) point.X + (double) Main.screenPosition.X) / 16.0);
      tY = (int) (((double) point.Y + (double) Main.screenPosition.Y) / 16.0);
      if ((double) player.gravDir != -1.0)
        return;
      tY = (int) (((double) Main.screenPosition.Y + (double) Main.screenHeight - (double) point.Y) / 16.0);
    }

    public void UpdateSize(Vector2 size)
    {
      this._size = size;
    }

    public void UpdateCenter(Vector2 center)
    {
      this._center = center;
    }

    public Vector2 GetPointerPosition()
    {
      Vector2 vector2 = (new Vector2((float) Main.mouseX, (float) Main.mouseY) - this._center) / this._size;
      float num = Math.Abs(vector2.X);
      if ((double) num < (double) Math.Abs(vector2.Y))
        num = Math.Abs(vector2.Y);
      if ((double) num > 1.0)
        vector2 /= num;
      return vector2 * Main.GameViewMatrix.Zoom.X * this._distUniform + this._center;
    }
  }
}
