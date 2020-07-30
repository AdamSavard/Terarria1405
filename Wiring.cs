﻿// Decompiled with JetBrains decompiler
// Type: Terraria.Wiring
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Events;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria
{
  public static class Wiring
  {
    private static int CurrentUser = (int) byte.MaxValue;
    public static bool blockPlayerTeleportationForOneIteration;
    public static bool running;
    private static Dictionary<Point16, bool> _wireSkip;
    private static DoubleStack<Point16> _wireList;
    private static DoubleStack<byte> _wireDirectionList;
    private static Dictionary<Point16, byte> _toProcess;
    private static Queue<Point16> _GatesCurrent;
    private static Queue<Point16> _LampsToCheck;
    private static Queue<Point16> _GatesNext;
    private static Dictionary<Point16, bool> _GatesDone;
    private static Dictionary<Point16, byte> _PixelBoxTriggers;
    private static Vector2[] _teleport;
    private const int MaxPump = 20;
    private static int[] _inPumpX;
    private static int[] _inPumpY;
    private static int _numInPump;
    private static int[] _outPumpX;
    private static int[] _outPumpY;
    private static int _numOutPump;
    private const int MaxMech = 1000;
    private static int[] _mechX;
    private static int[] _mechY;
    private static int _numMechs;
    private static int[] _mechTime;
    private static int _currentWireColor;

    public static void SetCurrentUser(int plr = -1)
    {
      if (plr < 0 || plr > (int) byte.MaxValue)
        plr = (int) byte.MaxValue;
      if (Main.netMode == 0)
        plr = Main.myPlayer;
      Wiring.CurrentUser = plr;
    }

    public static void Initialize()
    {
      Wiring._wireSkip = new Dictionary<Point16, bool>();
      Wiring._wireList = new DoubleStack<Point16>(1024, 0);
      Wiring._wireDirectionList = new DoubleStack<byte>(1024, 0);
      Wiring._toProcess = new Dictionary<Point16, byte>();
      Wiring._GatesCurrent = new Queue<Point16>();
      Wiring._GatesNext = new Queue<Point16>();
      Wiring._GatesDone = new Dictionary<Point16, bool>();
      Wiring._LampsToCheck = new Queue<Point16>();
      Wiring._PixelBoxTriggers = new Dictionary<Point16, byte>();
      Wiring._inPumpX = new int[20];
      Wiring._inPumpY = new int[20];
      Wiring._outPumpX = new int[20];
      Wiring._outPumpY = new int[20];
      Wiring._teleport = new Vector2[2];
      Wiring._mechX = new int[1000];
      Wiring._mechY = new int[1000];
      Wiring._mechTime = new int[1000];
    }

    public static void SkipWire(int x, int y)
    {
      Wiring._wireSkip[new Point16(x, y)] = true;
    }

    public static void SkipWire(Point16 point)
    {
      Wiring._wireSkip[point] = true;
    }

    public static void UpdateMech()
    {
      Wiring.SetCurrentUser(-1);
      for (int index1 = Wiring._numMechs - 1; index1 >= 0; --index1)
      {
        --Wiring._mechTime[index1];
        if (Main.tile[Wiring._mechX[index1], Wiring._mechY[index1]].active() && Main.tile[Wiring._mechX[index1], Wiring._mechY[index1]].type == (ushort) 144)
        {
          if (Main.tile[Wiring._mechX[index1], Wiring._mechY[index1]].frameY == (short) 0)
          {
            Wiring._mechTime[index1] = 0;
          }
          else
          {
            int num = (int) Main.tile[Wiring._mechX[index1], Wiring._mechY[index1]].frameX / 18;
            switch (num)
            {
              case 0:
                num = 60;
                break;
              case 1:
                num = 180;
                break;
              case 2:
                num = 300;
                break;
              case 3:
                num = 30;
                break;
              case 4:
                num = 15;
                break;
            }
            if (Math.IEEERemainder((double) Wiring._mechTime[index1], (double) num) == 0.0)
            {
              Wiring._mechTime[index1] = 18000;
              Wiring.TripWire(Wiring._mechX[index1], Wiring._mechY[index1], 1, 1);
            }
          }
        }
        if (Wiring._mechTime[index1] <= 0)
        {
          if (Main.tile[Wiring._mechX[index1], Wiring._mechY[index1]].active() && Main.tile[Wiring._mechX[index1], Wiring._mechY[index1]].type == (ushort) 144)
          {
            Main.tile[Wiring._mechX[index1], Wiring._mechY[index1]].frameY = (short) 0;
            NetMessage.SendTileSquare(-1, Wiring._mechX[index1], Wiring._mechY[index1], 1, TileChangeType.None);
          }
          if (Main.tile[Wiring._mechX[index1], Wiring._mechY[index1]].active() && Main.tile[Wiring._mechX[index1], Wiring._mechY[index1]].type == (ushort) 411)
          {
            Tile tile = Main.tile[Wiring._mechX[index1], Wiring._mechY[index1]];
            int num1 = (int) tile.frameX % 36 / 18;
            int num2 = (int) tile.frameY % 36 / 18;
            int tileX = Wiring._mechX[index1] - num1;
            int tileY = Wiring._mechY[index1] - num2;
            int num3 = 36;
            if (Main.tile[tileX, tileY].frameX >= (short) 36)
              num3 = -36;
            for (int index2 = tileX; index2 < tileX + 2; ++index2)
            {
              for (int index3 = tileY; index3 < tileY + 2; ++index3)
                Main.tile[index2, index3].frameX += (short) num3;
            }
            NetMessage.SendTileSquare(-1, tileX, tileY, 2, TileChangeType.None);
          }
          for (int index2 = index1; index2 < Wiring._numMechs; ++index2)
          {
            Wiring._mechX[index2] = Wiring._mechX[index2 + 1];
            Wiring._mechY[index2] = Wiring._mechY[index2 + 1];
            Wiring._mechTime[index2] = Wiring._mechTime[index2 + 1];
          }
          --Wiring._numMechs;
        }
      }
    }

    public static void HitSwitch(int i, int j)
    {
      if (!WorldGen.InWorld(i, j, 0) || Main.tile[i, j] == null)
        return;
      if (Main.tile[i, j].type == (ushort) 135 || Main.tile[i, j].type == (ushort) 314 || (Main.tile[i, j].type == (ushort) 423 || Main.tile[i, j].type == (ushort) 428) || (Main.tile[i, j].type == (ushort) 442 || Main.tile[i, j].type == (ushort) 476))
      {
        SoundEngine.PlaySound(28, i * 16, j * 16, 0, 1f, 0.0f);
        Wiring.TripWire(i, j, 1, 1);
      }
      else if (Main.tile[i, j].type == (ushort) 440)
      {
        SoundEngine.PlaySound(28, i * 16 + 16, j * 16 + 16, 0, 1f, 0.0f);
        Wiring.TripWire(i, j, 3, 3);
      }
      else if (Main.tile[i, j].type == (ushort) 136)
      {
        Main.tile[i, j].frameY = Main.tile[i, j].frameY != (short) 0 ? (short) 0 : (short) 18;
        SoundEngine.PlaySound(28, i * 16, j * 16, 0, 1f, 0.0f);
        Wiring.TripWire(i, j, 1, 1);
      }
      else if (Main.tile[i, j].type == (ushort) 443)
        Wiring.GeyserTrap(i, j);
      else if (Main.tile[i, j].type == (ushort) 144)
      {
        if (Main.tile[i, j].frameY == (short) 0)
        {
          Main.tile[i, j].frameY = (short) 18;
          if (Main.netMode != 1)
            Wiring.CheckMech(i, j, 18000);
        }
        else
          Main.tile[i, j].frameY = (short) 0;
        SoundEngine.PlaySound(28, i * 16, j * 16, 0, 1f, 0.0f);
      }
      else if (Main.tile[i, j].type == (ushort) 441 || Main.tile[i, j].type == (ushort) 468)
      {
        int num1 = (int) Main.tile[i, j].frameX / 18 * -1;
        int num2 = (int) Main.tile[i, j].frameY / 18 * -1;
        int num3 = num1 % 4;
        if (num3 < -1)
          num3 += 2;
        int left = num3 + i;
        int top = num2 + j;
        SoundEngine.PlaySound(28, i * 16, j * 16, 0, 1f, 0.0f);
        Wiring.TripWire(left, top, 2, 2);
      }
      else if (Main.tile[i, j].type == (ushort) 467)
      {
        if ((int) Main.tile[i, j].frameX / 36 != 4)
          return;
        int num1 = (int) Main.tile[i, j].frameX / 18 * -1;
        int num2 = (int) Main.tile[i, j].frameY / 18 * -1;
        int num3 = num1 % 4;
        if (num3 < -1)
          num3 += 2;
        int left = num3 + i;
        int top = num2 + j;
        SoundEngine.PlaySound(28, i * 16, j * 16, 0, 1f, 0.0f);
        Wiring.TripWire(left, top, 2, 2);
      }
      else
      {
        if (Main.tile[i, j].type != (ushort) 132 && Main.tile[i, j].type != (ushort) 411)
          return;
        short num1 = 36;
        int num2 = (int) Main.tile[i, j].frameX / 18 * -1;
        int num3 = (int) Main.tile[i, j].frameY / 18 * -1;
        int num4 = num2 % 4;
        if (num4 < -1)
        {
          num4 += 2;
          num1 = (short) -36;
        }
        int index1 = num4 + i;
        int index2 = num3 + j;
        if (Main.netMode != 1 && Main.tile[index1, index2].type == (ushort) 411)
          Wiring.CheckMech(index1, index2, 60);
        for (int index3 = index1; index3 < index1 + 2; ++index3)
        {
          for (int index4 = index2; index4 < index2 + 2; ++index4)
          {
            if (Main.tile[index3, index4].type == (ushort) 132 || Main.tile[index3, index4].type == (ushort) 411)
              Main.tile[index3, index4].frameX += num1;
          }
        }
        WorldGen.TileFrame(index1, index2, false, false);
        SoundEngine.PlaySound(28, i * 16, j * 16, 0, 1f, 0.0f);
        Wiring.TripWire(index1, index2, 2, 2);
      }
    }

    public static void PokeLogicGate(int lampX, int lampY)
    {
      if (Main.netMode == 1)
        return;
      Wiring._LampsToCheck.Enqueue(new Point16(lampX, lampY));
      Wiring.LogicGatePass();
    }

    public static bool Actuate(int i, int j)
    {
      Tile tile = Main.tile[i, j];
      if (!tile.actuator())
        return false;
      if (tile.inActive())
        Wiring.ReActive(i, j);
      else
        Wiring.DeActive(i, j);
      return true;
    }

    public static void ActuateForced(int i, int j)
    {
      if (Main.tile[i, j].inActive())
        Wiring.ReActive(i, j);
      else
        Wiring.DeActive(i, j);
    }

    public static void MassWireOperation(Point ps, Point pe, Player master)
    {
      int wireCount = 0;
      int actuatorCount = 0;
      for (int index = 0; index < 58; ++index)
      {
        if (master.inventory[index].type == 530)
          wireCount += master.inventory[index].stack;
        if (master.inventory[index].type == 849)
          actuatorCount += master.inventory[index].stack;
      }
      int num1 = wireCount;
      int num2 = actuatorCount;
      Wiring.MassWireOperationInner(ps, pe, master.Center, master.direction == 1, ref wireCount, ref actuatorCount);
      int num3 = wireCount;
      int num4 = num1 - num3;
      int num5 = num2 - actuatorCount;
      if (Main.netMode == 2)
      {
        NetMessage.SendData(110, master.whoAmI, -1, (NetworkText) null, 530, (float) num4, (float) master.whoAmI, 0.0f, 0, 0, 0);
        NetMessage.SendData(110, master.whoAmI, -1, (NetworkText) null, 849, (float) num5, (float) master.whoAmI, 0.0f, 0, 0, 0);
      }
      else
      {
        for (int index = 0; index < num4; ++index)
          master.ConsumeItem(530, false);
        for (int index = 0; index < num5; ++index)
          master.ConsumeItem(849, false);
      }
    }

    private static bool CheckMech(int i, int j, int time)
    {
      for (int index = 0; index < Wiring._numMechs; ++index)
      {
        if (Wiring._mechX[index] == i && Wiring._mechY[index] == j)
          return false;
      }
      if (Wiring._numMechs >= 999)
        return false;
      Wiring._mechX[Wiring._numMechs] = i;
      Wiring._mechY[Wiring._numMechs] = j;
      Wiring._mechTime[Wiring._numMechs] = time;
      ++Wiring._numMechs;
      return true;
    }

    private static void XferWater()
    {
      for (int index1 = 0; index1 < Wiring._numInPump; ++index1)
      {
        int i1 = Wiring._inPumpX[index1];
        int j1 = Wiring._inPumpY[index1];
        int liquid1 = (int) Main.tile[i1, j1].liquid;
        if (liquid1 > 0)
        {
          bool lava = Main.tile[i1, j1].lava();
          bool honey = Main.tile[i1, j1].honey();
          for (int index2 = 0; index2 < Wiring._numOutPump; ++index2)
          {
            int i2 = Wiring._outPumpX[index2];
            int j2 = Wiring._outPumpY[index2];
            int liquid2 = (int) Main.tile[i2, j2].liquid;
            if (liquid2 < (int) byte.MaxValue)
            {
              bool flag1 = Main.tile[i2, j2].lava();
              bool flag2 = Main.tile[i2, j2].honey();
              if (liquid2 == 0)
              {
                flag1 = lava;
                flag2 = honey;
              }
              if (lava == flag1 && honey == flag2)
              {
                int num = liquid1;
                if (num + liquid2 > (int) byte.MaxValue)
                  num = (int) byte.MaxValue - liquid2;
                Main.tile[i2, j2].liquid += (byte) num;
                Main.tile[i1, j1].liquid -= (byte) num;
                liquid1 = (int) Main.tile[i1, j1].liquid;
                Main.tile[i2, j2].lava(lava);
                Main.tile[i2, j2].honey(honey);
                WorldGen.SquareTileFrame(i2, j2, true);
                if (Main.tile[i1, j1].liquid == (byte) 0)
                {
                  Main.tile[i1, j1].lava(false);
                  WorldGen.SquareTileFrame(i1, j1, true);
                  break;
                }
              }
            }
          }
          WorldGen.SquareTileFrame(i1, j1, true);
        }
      }
    }

    private static void TripWire(int left, int top, int width, int height)
    {
      if (Main.netMode == 1)
        return;
      Wiring.running = true;
      if (Wiring._wireList.Count != 0)
        Wiring._wireList.Clear(true);
      if (Wiring._wireDirectionList.Count != 0)
        Wiring._wireDirectionList.Clear(true);
      Vector2[] vector2Array1 = new Vector2[8];
      int num1 = 0;
      Point16 back;
      for (int X = left; X < left + width; ++X)
      {
        for (int Y = top; Y < top + height; ++Y)
        {
          back = new Point16(X, Y);
          Tile tile = Main.tile[X, Y];
          if (tile != null && tile.wire())
            Wiring._wireList.PushBack(back);
        }
      }
      Wiring._teleport[0].X = -1f;
      Wiring._teleport[0].Y = -1f;
      Wiring._teleport[1].X = -1f;
      Wiring._teleport[1].Y = -1f;
      if (Wiring._wireList.Count > 0)
      {
        Wiring._numInPump = 0;
        Wiring._numOutPump = 0;
        Wiring.HitWire(Wiring._wireList, 1);
        if (Wiring._numInPump > 0 && Wiring._numOutPump > 0)
          Wiring.XferWater();
      }
      Vector2[] vector2Array2 = vector2Array1;
      int index1 = num1;
      int num2 = index1 + 1;
      Vector2 vector2_1 = Wiring._teleport[0];
      vector2Array2[index1] = vector2_1;
      Vector2[] vector2Array3 = vector2Array1;
      int index2 = num2;
      int num3 = index2 + 1;
      Vector2 vector2_2 = Wiring._teleport[1];
      vector2Array3[index2] = vector2_2;
      for (int X = left; X < left + width; ++X)
      {
        for (int Y = top; Y < top + height; ++Y)
        {
          back = new Point16(X, Y);
          Tile tile = Main.tile[X, Y];
          if (tile != null && tile.wire2())
            Wiring._wireList.PushBack(back);
        }
      }
      Wiring._teleport[0].X = -1f;
      Wiring._teleport[0].Y = -1f;
      Wiring._teleport[1].X = -1f;
      Wiring._teleport[1].Y = -1f;
      if (Wiring._wireList.Count > 0)
      {
        Wiring._numInPump = 0;
        Wiring._numOutPump = 0;
        Wiring.HitWire(Wiring._wireList, 2);
        if (Wiring._numInPump > 0 && Wiring._numOutPump > 0)
          Wiring.XferWater();
      }
      Vector2[] vector2Array4 = vector2Array1;
      int index3 = num3;
      int num4 = index3 + 1;
      Vector2 vector2_3 = Wiring._teleport[0];
      vector2Array4[index3] = vector2_3;
      Vector2[] vector2Array5 = vector2Array1;
      int index4 = num4;
      int num5 = index4 + 1;
      Vector2 vector2_4 = Wiring._teleport[1];
      vector2Array5[index4] = vector2_4;
      Wiring._teleport[0].X = -1f;
      Wiring._teleport[0].Y = -1f;
      Wiring._teleport[1].X = -1f;
      Wiring._teleport[1].Y = -1f;
      for (int X = left; X < left + width; ++X)
      {
        for (int Y = top; Y < top + height; ++Y)
        {
          back = new Point16(X, Y);
          Tile tile = Main.tile[X, Y];
          if (tile != null && tile.wire3())
            Wiring._wireList.PushBack(back);
        }
      }
      if (Wiring._wireList.Count > 0)
      {
        Wiring._numInPump = 0;
        Wiring._numOutPump = 0;
        Wiring.HitWire(Wiring._wireList, 3);
        if (Wiring._numInPump > 0 && Wiring._numOutPump > 0)
          Wiring.XferWater();
      }
      Vector2[] vector2Array6 = vector2Array1;
      int index5 = num5;
      int num6 = index5 + 1;
      Vector2 vector2_5 = Wiring._teleport[0];
      vector2Array6[index5] = vector2_5;
      Vector2[] vector2Array7 = vector2Array1;
      int index6 = num6;
      int num7 = index6 + 1;
      Vector2 vector2_6 = Wiring._teleport[1];
      vector2Array7[index6] = vector2_6;
      Wiring._teleport[0].X = -1f;
      Wiring._teleport[0].Y = -1f;
      Wiring._teleport[1].X = -1f;
      Wiring._teleport[1].Y = -1f;
      for (int X = left; X < left + width; ++X)
      {
        for (int Y = top; Y < top + height; ++Y)
        {
          back = new Point16(X, Y);
          Tile tile = Main.tile[X, Y];
          if (tile != null && tile.wire4())
            Wiring._wireList.PushBack(back);
        }
      }
      if (Wiring._wireList.Count > 0)
      {
        Wiring._numInPump = 0;
        Wiring._numOutPump = 0;
        Wiring.HitWire(Wiring._wireList, 4);
        if (Wiring._numInPump > 0 && Wiring._numOutPump > 0)
          Wiring.XferWater();
      }
      Vector2[] vector2Array8 = vector2Array1;
      int index7 = num7;
      int num8 = index7 + 1;
      Vector2 vector2_7 = Wiring._teleport[0];
      vector2Array8[index7] = vector2_7;
      Vector2[] vector2Array9 = vector2Array1;
      int index8 = num8;
      int num9 = index8 + 1;
      Vector2 vector2_8 = Wiring._teleport[1];
      vector2Array9[index8] = vector2_8;
      Wiring.running = false;
      for (int index9 = 0; index9 < 8; index9 += 2)
      {
        Wiring._teleport[0] = vector2Array1[index9];
        Wiring._teleport[1] = vector2Array1[index9 + 1];
        if ((double) Wiring._teleport[0].X >= 0.0 && (double) Wiring._teleport[1].X >= 0.0)
          Wiring.Teleport();
      }
      Wiring.PixelBoxPass();
      Wiring.LogicGatePass();
    }

    private static void PixelBoxPass()
    {
      foreach (KeyValuePair<Point16, byte> pixelBoxTrigger in Wiring._PixelBoxTriggers)
      {
        if (pixelBoxTrigger.Value != (byte) 2)
        {
          if (pixelBoxTrigger.Value == (byte) 1)
          {
            if (Main.tile[(int) pixelBoxTrigger.Key.X, (int) pixelBoxTrigger.Key.Y].frameX != (short) 0)
            {
              Main.tile[(int) pixelBoxTrigger.Key.X, (int) pixelBoxTrigger.Key.Y].frameX = (short) 0;
              NetMessage.SendTileSquare(-1, (int) pixelBoxTrigger.Key.X, (int) pixelBoxTrigger.Key.Y, 1, TileChangeType.None);
            }
          }
          else if (pixelBoxTrigger.Value == (byte) 3 && Main.tile[(int) pixelBoxTrigger.Key.X, (int) pixelBoxTrigger.Key.Y].frameX != (short) 18)
          {
            Main.tile[(int) pixelBoxTrigger.Key.X, (int) pixelBoxTrigger.Key.Y].frameX = (short) 18;
            NetMessage.SendTileSquare(-1, (int) pixelBoxTrigger.Key.X, (int) pixelBoxTrigger.Key.Y, 1, TileChangeType.None);
          }
        }
      }
      Wiring._PixelBoxTriggers.Clear();
    }

    private static void LogicGatePass()
    {
      if (Wiring._GatesCurrent.Count != 0)
        return;
      Wiring._GatesDone.Clear();
      while (Wiring._LampsToCheck.Count > 0)
      {
        while (Wiring._LampsToCheck.Count > 0)
        {
          Point16 point16 = Wiring._LampsToCheck.Dequeue();
          Wiring.CheckLogicGate((int) point16.X, (int) point16.Y);
        }
        while (Wiring._GatesNext.Count > 0)
        {
          Utils.Swap<Queue<Point16>>(ref Wiring._GatesCurrent, ref Wiring._GatesNext);
          while (Wiring._GatesCurrent.Count > 0)
          {
            Point16 key = Wiring._GatesCurrent.Peek();
            bool flag;
            if (Wiring._GatesDone.TryGetValue(key, out flag) && flag)
            {
              Wiring._GatesCurrent.Dequeue();
            }
            else
            {
              Wiring._GatesDone.Add(key, true);
              Wiring.TripWire((int) key.X, (int) key.Y, 1, 1);
              Wiring._GatesCurrent.Dequeue();
            }
          }
        }
      }
      Wiring._GatesDone.Clear();
      if (!Wiring.blockPlayerTeleportationForOneIteration)
        return;
      Wiring.blockPlayerTeleportationForOneIteration = false;
    }

    private static void CheckLogicGate(int lampX, int lampY)
    {
      if (!WorldGen.InWorld(lampX, lampY, 1))
        return;
      for (int index1 = lampY; index1 < Main.maxTilesY; ++index1)
      {
        Tile tile1 = Main.tile[lampX, index1];
        if (!tile1.active())
          break;
        if (tile1.type == (ushort) 420)
        {
          bool flag1;
          Wiring._GatesDone.TryGetValue(new Point16(lampX, index1), out flag1);
          int num1 = (int) tile1.frameY / 18;
          bool flag2 = tile1.frameX == (short) 18;
          bool flag3 = tile1.frameX == (short) 36;
          if (num1 < 0)
            break;
          int num2 = 0;
          int num3 = 0;
          bool flag4 = false;
          for (int index2 = index1 - 1; index2 > 0; --index2)
          {
            Tile tile2 = Main.tile[lampX, index2];
            if (tile2.active() && tile2.type == (ushort) 419)
            {
              if (tile2.frameX == (short) 36)
              {
                flag4 = true;
                break;
              }
              ++num2;
              num3 += (tile2.frameX == (short) 18).ToInt();
            }
            else
              break;
          }
          bool flag5;
          switch (num1)
          {
            case 0:
              flag5 = num2 == num3;
              break;
            case 1:
              flag5 = num3 > 0;
              break;
            case 2:
              flag5 = num2 != num3;
              break;
            case 3:
              flag5 = num3 == 0;
              break;
            case 4:
              flag5 = num3 == 1;
              break;
            case 5:
              flag5 = num3 != 1;
              break;
            default:
              return;
          }
          bool flag6 = !flag4 & flag3;
          bool flag7 = false;
          if (flag4 && Framing.GetTileSafely(lampX, lampY).frameX == (short) 36)
            flag7 = true;
          if (!(flag5 != flag2 | flag6 | flag7))
            break;
          int num4 = (int) tile1.frameX % 18 / 18;
          tile1.frameX = (short) (18 * flag5.ToInt());
          if (flag4)
            tile1.frameX = (short) 36;
          Wiring.SkipWire(lampX, index1);
          WorldGen.SquareTileFrame(lampX, index1, true);
          NetMessage.SendTileSquare(-1, lampX, index1, 1, TileChangeType.None);
          bool flag8 = !flag4 | flag7;
          if (flag7)
          {
            if (num3 == 0 || num2 == 0)
              ;
            flag8 = (double) Main.rand.NextFloat() < (double) num3 / (double) num2;
          }
          if (flag6)
            flag8 = false;
          if (!flag8)
            break;
          if (!flag1)
          {
            Wiring._GatesNext.Enqueue(new Point16(lampX, index1));
            break;
          }
          Vector2 position = new Vector2((float) lampX, (float) index1) * 16f - new Vector2(10f);
          Utils.PoofOfSmoke(position);
          NetMessage.SendData(106, -1, -1, (NetworkText) null, (int) position.X, position.Y, 0.0f, 0.0f, 0, 0, 0);
          break;
        }
        if (tile1.type != (ushort) 419)
          break;
      }
    }

    private static void HitWire(DoubleStack<Point16> next, int wireType)
    {
      Wiring._wireDirectionList.Clear(true);
      for (int index = 0; index < next.Count; ++index)
      {
        Point16 point16 = next.PopFront();
        Wiring.SkipWire(point16);
        Wiring._toProcess.Add(point16, (byte) 4);
        next.PushBack(point16);
        Wiring._wireDirectionList.PushBack((byte) 0);
      }
      Wiring._currentWireColor = wireType;
      while (next.Count > 0)
      {
        Point16 key = next.PopFront();
        int num1 = (int) Wiring._wireDirectionList.PopFront();
        int x = (int) key.X;
        int y = (int) key.Y;
        if (!Wiring._wireSkip.ContainsKey(key))
          Wiring.HitWireSingle(x, y);
        for (int index1 = 0; index1 < 4; ++index1)
        {
          int X;
          int Y;
          switch (index1)
          {
            case 0:
              X = x;
              Y = y + 1;
              break;
            case 1:
              X = x;
              Y = y - 1;
              break;
            case 2:
              X = x + 1;
              Y = y;
              break;
            case 3:
              X = x - 1;
              Y = y;
              break;
            default:
              X = x;
              Y = y + 1;
              break;
          }
          if (X >= 2 && X < Main.maxTilesX - 2 && (Y >= 2 && Y < Main.maxTilesY - 2))
          {
            Tile tile1 = Main.tile[X, Y];
            if (tile1 != null)
            {
              Tile tile2 = Main.tile[x, y];
              if (tile2 != null)
              {
                byte num2 = 3;
                if (tile1.type == (ushort) 424 || tile1.type == (ushort) 445)
                  num2 = (byte) 0;
                if (tile2.type == (ushort) 424)
                {
                  switch ((int) tile2.frameX / 18)
                  {
                    case 0:
                      if (index1 == num1)
                        break;
                      continue;
                    case 1:
                      if (num1 == 0 && index1 == 3 || num1 == 3 && index1 == 0 || (num1 == 1 && index1 == 2 || num1 == 2 && index1 == 1))
                        break;
                      continue;
                    case 2:
                      if (num1 == 0 && index1 == 2 || num1 == 2 && index1 == 0 || (num1 == 1 && index1 == 3 || num1 == 3 && index1 == 1))
                        break;
                      continue;
                  }
                }
                if (tile2.type == (ushort) 445)
                {
                  if (index1 == num1)
                  {
                    if (Wiring._PixelBoxTriggers.ContainsKey(key))
                      Wiring._PixelBoxTriggers[key] |= index1 == 0 | index1 == 1 ? (byte) 2 : (byte) 1;
                    else
                      Wiring._PixelBoxTriggers[key] = index1 == 0 | index1 == 1 ? (byte) 2 : (byte) 1;
                  }
                  else
                    continue;
                }
                bool flag;
                switch (wireType)
                {
                  case 1:
                    flag = tile1.wire();
                    break;
                  case 2:
                    flag = tile1.wire2();
                    break;
                  case 3:
                    flag = tile1.wire3();
                    break;
                  case 4:
                    flag = tile1.wire4();
                    break;
                  default:
                    flag = false;
                    break;
                }
                if (flag)
                {
                  Point16 index2 = new Point16(X, Y);
                  byte num3;
                  if (Wiring._toProcess.TryGetValue(index2, out num3))
                  {
                    --num3;
                    if (num3 == (byte) 0)
                      Wiring._toProcess.Remove(index2);
                    else
                      Wiring._toProcess[index2] = num3;
                  }
                  else
                  {
                    next.PushBack(index2);
                    Wiring._wireDirectionList.PushBack((byte) index1);
                    if (num2 > (byte) 0)
                      Wiring._toProcess.Add(index2, num2);
                  }
                }
              }
            }
          }
        }
      }
      Wiring._wireSkip.Clear();
      Wiring._toProcess.Clear();
    }

    private static void HitWireSingle(int i, int j)
    {
      Tile tile1 = Main.tile[i, j];
      int type = (int) tile1.type;
      if (tile1.actuator())
        Wiring.ActuateForced(i, j);
      if (!tile1.active())
        return;
      switch (type)
      {
        case 144:
          Wiring.HitSwitch(i, j);
          WorldGen.SquareTileFrame(i, j, true);
          NetMessage.SendTileSquare(-1, i, j, 1, TileChangeType.None);
          break;
        case 421:
          if (!tile1.actuator())
          {
            tile1.type = (ushort) 422;
            WorldGen.SquareTileFrame(i, j, true);
            NetMessage.SendTileSquare(-1, i, j, 1, TileChangeType.None);
            break;
          }
          break;
        default:
          if (type == 422 && !tile1.actuator())
          {
            tile1.type = (ushort) 421;
            WorldGen.SquareTileFrame(i, j, true);
            NetMessage.SendTileSquare(-1, i, j, 1, TileChangeType.None);
            break;
          }
          break;
      }
      if (type >= (int) byte.MaxValue && type <= 268)
      {
        if (tile1.actuator())
          return;
        if (type >= 262)
          tile1.type -= (ushort) 7;
        else
          tile1.type += (ushort) 7;
        WorldGen.SquareTileFrame(i, j, true);
        NetMessage.SendTileSquare(-1, i, j, 1, TileChangeType.None);
      }
      else
      {
        switch (type)
        {
          case 130:
            if (Main.tile[i, j - 1] != null && Main.tile[i, j - 1].active() && (TileID.Sets.BasicChest[(int) Main.tile[i, j - 1].type] || TileID.Sets.BasicChestFake[(int) Main.tile[i, j - 1].type] || Main.tile[i, j - 1].type == (ushort) 88))
              break;
            tile1.type = (ushort) 131;
            WorldGen.SquareTileFrame(i, j, true);
            NetMessage.SendTileSquare(-1, i, j, 1, TileChangeType.None);
            break;
          case 131:
            tile1.type = (ushort) 130;
            WorldGen.SquareTileFrame(i, j, true);
            NetMessage.SendTileSquare(-1, i, j, 1, TileChangeType.None);
            break;
          case 209:
            int num1 = (int) tile1.frameX % 72 / 18;
            int num2 = (int) tile1.frameY % 54 / 18;
            int num3 = i - num1;
            int num4 = j - num2;
            int angle = (int) tile1.frameY / 54;
            int num5 = (int) tile1.frameX / 72;
            int num6 = -1;
            if (num1 == 1 || num1 == 2)
              num6 = num2;
            int num7 = 0;
            if (num1 == 3)
              num7 = -54;
            if (num1 == 0)
              num7 = 54;
            if (angle >= 8 && num7 > 0)
              num7 = 0;
            if (angle == 0 && num7 < 0)
              num7 = 0;
            bool flag1 = false;
            if (num7 != 0)
            {
              for (int x = num3; x < num3 + 4; ++x)
              {
                for (int y = num4; y < num4 + 3; ++y)
                {
                  Wiring.SkipWire(x, y);
                  Main.tile[x, y].frameY += (short) num7;
                }
              }
              flag1 = true;
            }
            if ((num5 == 3 || num5 == 4) && (num6 == 0 || num6 == 1))
            {
              int num8 = num5 == 3 ? 72 : -72;
              for (int x = num3; x < num3 + 4; ++x)
              {
                for (int y = num4; y < num4 + 3; ++y)
                {
                  Wiring.SkipWire(x, y);
                  Main.tile[x, y].frameX += (short) num8;
                }
              }
              flag1 = true;
            }
            if (flag1)
              NetMessage.SendTileSquare(-1, num3 + 1, num4 + 1, 4, TileChangeType.None);
            if (num6 == -1)
              break;
            bool flag2 = true;
            if ((num5 == 3 || num5 == 4) && num6 < 2)
              flag2 = false;
            if (!(Wiring.CheckMech(num3, num4, 30) & flag2))
              break;
            WorldGen.ShootFromCannon(num3, num4, angle, num5 + 1, 0, 0.0f, Wiring.CurrentUser);
            break;
          case 212:
            int num9 = (int) tile1.frameX % 54 / 18;
            int num10 = (int) tile1.frameY % 54 / 18;
            int i1 = i - num9;
            int j1 = j - num10;
            int num11 = (int) tile1.frameX / 54;
            int num12 = -1;
            if (num9 == 1)
              num12 = num10;
            int num13 = 0;
            if (num9 == 0)
              num13 = -54;
            if (num9 == 2)
              num13 = 54;
            if (num11 >= 1 && num13 > 0)
              num13 = 0;
            if (num11 == 0 && num13 < 0)
              num13 = 0;
            bool flag3 = false;
            if (num13 != 0)
            {
              for (int x = i1; x < i1 + 3; ++x)
              {
                for (int y = j1; y < j1 + 3; ++y)
                {
                  Wiring.SkipWire(x, y);
                  Main.tile[x, y].frameX += (short) num13;
                }
              }
              flag3 = true;
            }
            if (flag3)
              NetMessage.SendTileSquare(-1, i1 + 1, j1 + 1, 4, TileChangeType.None);
            if (num12 == -1 || !Wiring.CheckMech(i1, j1, 10))
              break;
            double num14 = 12.0 + (double) Main.rand.Next(450) * 0.00999999977648258;
            float num15 = (float) Main.rand.Next(85, 105);
            double num16 = (double) Main.rand.Next(-35, 11);
            int Type1 = 166;
            int Damage1 = 0;
            float KnockBack1 = 0.0f;
            Vector2 vector2_1 = new Vector2((float) ((i1 + 2) * 16 - 8), (float) ((j1 + 2) * 16 - 8));
            if ((int) tile1.frameX / 54 == 0)
            {
              num15 *= -1f;
              vector2_1.X -= 12f;
            }
            else
              vector2_1.X += 12f;
            float num17 = num15;
            float num18 = (float) num16;
            double num19 = Math.Sqrt((double) num17 * (double) num17 + (double) num18 * (double) num18);
            float num20 = (float) (num14 / num19);
            float SpeedX1 = num17 * num20;
            float SpeedY1 = num18 * num20;
            Projectile.NewProjectile(vector2_1.X, vector2_1.Y, SpeedX1, SpeedY1, Type1, Damage1, KnockBack1, Wiring.CurrentUser, 0.0f, 0.0f);
            break;
          case 215:
            int num21 = (int) tile1.frameX % 54 / 18;
            int num22 = (int) tile1.frameY % 36 / 18;
            int index1 = i - num21;
            int index2 = j - num22;
            int num23 = 36;
            if (Main.tile[index1, index2].frameY >= (short) 36)
              num23 = -36;
            for (int x = index1; x < index1 + 3; ++x)
            {
              for (int y = index2; y < index2 + 2; ++y)
              {
                Wiring.SkipWire(x, y);
                Main.tile[x, y].frameY += (short) num23;
              }
            }
            NetMessage.SendTileSquare(-1, index1 + 1, index2 + 1, 3, TileChangeType.None);
            break;
          case 405:
            int num24 = (int) tile1.frameX % 54 / 18;
            int num25 = (int) tile1.frameY % 36 / 18;
            int index3 = i - num24;
            int index4 = j - num25;
            int num26 = 54;
            if (Main.tile[index3, index4].frameX >= (short) 54)
              num26 = -54;
            for (int x = index3; x < index3 + 3; ++x)
            {
              for (int y = index4; y < index4 + 2; ++y)
              {
                Wiring.SkipWire(x, y);
                Main.tile[x, y].frameX += (short) num26;
              }
            }
            NetMessage.SendTileSquare(-1, index3 + 1, index4 + 1, 3, TileChangeType.None);
            break;
          case 406:
            int num27 = (int) tile1.frameX % 54 / 18;
            int num28 = (int) tile1.frameY % 54 / 18;
            int index5 = i - num27;
            int index6 = j - num28;
            int num29 = 54;
            if (Main.tile[index5, index6].frameY >= (short) 108)
              num29 = -108;
            for (int x = index5; x < index5 + 3; ++x)
            {
              for (int y = index6; y < index6 + 3; ++y)
              {
                Wiring.SkipWire(x, y);
                Main.tile[x, y].frameY += (short) num29;
              }
            }
            NetMessage.SendTileSquare(-1, index5 + 1, index6 + 1, 3, TileChangeType.None);
            break;
          case 411:
            int num30 = (int) tile1.frameX % 36 / 18;
            int num31 = (int) tile1.frameY % 36 / 18;
            int tileX = i - num30;
            int tileY = j - num31;
            int num32 = 36;
            if (Main.tile[tileX, tileY].frameX >= (short) 36)
              num32 = -36;
            for (int x = tileX; x < tileX + 2; ++x)
            {
              for (int y = tileY; y < tileY + 2; ++y)
              {
                Wiring.SkipWire(x, y);
                Main.tile[x, y].frameX += (short) num32;
              }
            }
            NetMessage.SendTileSquare(-1, tileX, tileY, 2, TileChangeType.None);
            break;
          case 419:
            int num33 = 18;
            if ((int) tile1.frameX >= num33)
              num33 = -num33;
            if (tile1.frameX == (short) 36)
              num33 = 0;
            Wiring.SkipWire(i, j);
            tile1.frameX += (short) num33;
            WorldGen.SquareTileFrame(i, j, true);
            NetMessage.SendTileSquare(-1, i, j, 1, TileChangeType.None);
            Wiring._LampsToCheck.Enqueue(new Point16(i, j));
            break;
          case 425:
            int num34 = (int) tile1.frameX % 36 / 18;
            int num35 = (int) tile1.frameY % 36 / 18;
            int i2 = i - num34;
            int j2 = j - num35;
            for (int x = i2; x < i2 + 2; ++x)
            {
              for (int y = j2; y < j2 + 2; ++y)
                Wiring.SkipWire(x, y);
            }
            if (Main.AnnouncementBoxDisabled)
              break;
            Color pink = Color.Pink;
            int index7 = Sign.ReadSign(i2, j2, false);
            if (index7 == -1 || Main.sign[index7] == null || string.IsNullOrWhiteSpace(Main.sign[index7].text))
              break;
            if (Main.AnnouncementBoxRange == -1)
            {
              if (Main.netMode == 0)
              {
                Main.NewTextMultiline(Main.sign[index7].text, false, pink, 460);
                break;
              }
              if (Main.netMode != 2)
                break;
              NetMessage.SendData(107, -1, -1, NetworkText.FromLiteral(Main.sign[index7].text), (int) byte.MaxValue, (float) pink.R, (float) pink.G, (float) pink.B, 460, 0, 0);
              break;
            }
            switch (Main.netMode)
            {
              case 0:
                if ((double) Main.player[Main.myPlayer].Distance(new Vector2((float) (i2 * 16 + 16), (float) (j2 * 16 + 16))) > (double) Main.AnnouncementBoxRange)
                  return;
                Main.NewTextMultiline(Main.sign[index7].text, false, pink, 460);
                return;
              case 2:
                for (int remoteClient = 0; remoteClient < (int) byte.MaxValue; ++remoteClient)
                {
                  if (Main.player[remoteClient].active && (double) Main.player[remoteClient].Distance(new Vector2((float) (i2 * 16 + 16), (float) (j2 * 16 + 16))) <= (double) Main.AnnouncementBoxRange)
                    NetMessage.SendData(107, remoteClient, -1, NetworkText.FromLiteral(Main.sign[index7].text), (int) byte.MaxValue, (float) pink.R, (float) pink.G, (float) pink.B, 460, 0, 0);
                }
                return;
              default:
                return;
            }
          case 452:
            int num36 = (int) tile1.frameX % 54 / 18;
            int num37 = (int) tile1.frameY % 54 / 18;
            int index8 = i - num36;
            int index9 = j - num37;
            int num38 = 54;
            if (Main.tile[index8, index9].frameX >= (short) 54)
              num38 = -54;
            for (int x = index8; x < index8 + 3; ++x)
            {
              for (int y = index9; y < index9 + 3; ++y)
              {
                Wiring.SkipWire(x, y);
                Main.tile[x, y].frameX += (short) num38;
              }
            }
            NetMessage.SendTileSquare(-1, index8 + 1, index9 + 1, 3, TileChangeType.None);
            break;
          default:
            if (type == 387 || type == 386)
            {
              bool flag4 = type == 387;
              int num8 = WorldGen.ShiftTrapdoor(i, j, true, -1).ToInt();
              if (num8 == 0)
                num8 = -WorldGen.ShiftTrapdoor(i, j, false, -1).ToInt();
              if (num8 == 0)
                break;
              NetMessage.SendData(19, -1, -1, (NetworkText) null, 3 - flag4.ToInt(), (float) i, (float) j, (float) num8, 0, 0, 0);
              break;
            }
            if (type == 389 || type == 388)
            {
              bool closing = type == 389;
              WorldGen.ShiftTallGate(i, j, closing, false);
              NetMessage.SendData(19, -1, -1, (NetworkText) null, 4 + closing.ToInt(), (float) i, (float) j, 0.0f, 0, 0, 0);
              break;
            }
            switch (type)
            {
              case 10:
                int direction = 1;
                if (Main.rand.Next(2) == 0)
                  direction = -1;
                if (!WorldGen.OpenDoor(i, j, direction))
                {
                  if (!WorldGen.OpenDoor(i, j, -direction))
                    return;
                  NetMessage.SendData(19, -1, -1, (NetworkText) null, 0, (float) i, (float) j, (float) -direction, 0, 0, 0);
                  return;
                }
                NetMessage.SendData(19, -1, -1, (NetworkText) null, 0, (float) i, (float) j, (float) direction, 0, 0, 0);
                return;
              case 11:
                if (!WorldGen.CloseDoor(i, j, true))
                  return;
                NetMessage.SendData(19, -1, -1, (NetworkText) null, 1, (float) i, (float) j, 0.0f, 0, 0, 0);
                return;
              case 216:
                WorldGen.LaunchRocket(i, j);
                Wiring.SkipWire(i, j);
                return;
              default:
                if (type == 497 || type == 15 && (int) tile1.frameY / 40 == 1 || type == 15 && (int) tile1.frameY / 40 == 20)
                {
                  int num8 = j - (int) tile1.frameY % 40 / 18;
                  int num39 = i;
                  Wiring.SkipWire(num39, num8);
                  Wiring.SkipWire(num39, num8 + 1);
                  if (!Wiring.CheckMech(num39, num8, 60))
                    return;
                  Projectile.NewProjectile((float) (num39 * 16 + 8), (float) (num8 * 16 + 12), 0.0f, 0.0f, 733, 0, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                  return;
                }
                switch (type)
                {
                  case 4:
                    if (tile1.frameX < (short) 66)
                      tile1.frameX += (short) 66;
                    else
                      tile1.frameX -= (short) 66;
                    NetMessage.SendTileSquare(-1, i, j, 1, TileChangeType.None);
                    return;
                  case 42:
                    int num40 = (int) tile1.frameY / 18;
                    while (num40 >= 2)
                      num40 -= 2;
                    int y1 = j - num40;
                    short num41 = 18;
                    if (tile1.frameX > (short) 0)
                      num41 = (short) -18;
                    Main.tile[i, y1].frameX += num41;
                    Main.tile[i, y1 + 1].frameX += num41;
                    Wiring.SkipWire(i, y1);
                    Wiring.SkipWire(i, y1 + 1);
                    NetMessage.SendTileSquare(-1, i, j, 3, TileChangeType.None);
                    return;
                  case 93:
                    int num42 = (int) tile1.frameY / 18;
                    while (num42 >= 3)
                      num42 -= 3;
                    int y2 = j - num42;
                    short num43 = 18;
                    if (tile1.frameX > (short) 0)
                      num43 = (short) -18;
                    Main.tile[i, y2].frameX += num43;
                    Main.tile[i, y2 + 1].frameX += num43;
                    Main.tile[i, y2 + 2].frameX += num43;
                    Wiring.SkipWire(i, y2);
                    Wiring.SkipWire(i, y2 + 1);
                    Wiring.SkipWire(i, y2 + 2);
                    NetMessage.SendTileSquare(-1, i, y2 + 1, 3, TileChangeType.None);
                    return;
                  case 149:
                    if (tile1.frameX < (short) 54)
                      tile1.frameX += (short) 54;
                    else
                      tile1.frameX -= (short) 54;
                    NetMessage.SendTileSquare(-1, i, j, 1, TileChangeType.None);
                    return;
                  case 235:
                    int num44 = i - (int) tile1.frameX / 18;
                    if (tile1.wall == (ushort) 87 && (double) j > Main.worldSurface && !NPC.downedPlantBoss)
                      return;
                    if ((double) Wiring._teleport[0].X == -1.0)
                    {
                      Wiring._teleport[0].X = (float) num44;
                      Wiring._teleport[0].Y = (float) j;
                      if (!tile1.halfBrick())
                        return;
                      Wiring._teleport[0].Y += 0.5f;
                      return;
                    }
                    if ((double) Wiring._teleport[0].X == (double) num44 && (double) Wiring._teleport[0].Y == (double) j)
                      return;
                    Wiring._teleport[1].X = (float) num44;
                    Wiring._teleport[1].Y = (float) j;
                    if (!tile1.halfBrick())
                      return;
                    Wiring._teleport[1].Y += 0.5f;
                    return;
                  case 244:
                    int num45 = (int) tile1.frameX / 18;
                    while (num45 >= 3)
                      num45 -= 3;
                    int num46 = (int) tile1.frameY / 18;
                    while (num46 >= 3)
                      num46 -= 3;
                    int index10 = i - num45;
                    int index11 = j - num46;
                    int num47 = 54;
                    if (Main.tile[index10, index11].frameX >= (short) 54)
                      num47 = -54;
                    for (int x = index10; x < index10 + 3; ++x)
                    {
                      for (int y3 = index11; y3 < index11 + 2; ++y3)
                      {
                        Wiring.SkipWire(x, y3);
                        Main.tile[x, y3].frameX += (short) num47;
                      }
                    }
                    NetMessage.SendTileSquare(-1, index10 + 1, index11 + 1, 3, TileChangeType.None);
                    return;
                  case 335:
                    int num48 = j - (int) tile1.frameY / 18;
                    int num49 = i - (int) tile1.frameX / 18;
                    Wiring.SkipWire(num49, num48);
                    Wiring.SkipWire(num49, num48 + 1);
                    Wiring.SkipWire(num49 + 1, num48);
                    Wiring.SkipWire(num49 + 1, num48 + 1);
                    if (!Wiring.CheckMech(num49, num48, 30))
                      return;
                    WorldGen.LaunchRocketSmall(num49, num48);
                    return;
                  case 338:
                    int num50 = j - (int) tile1.frameY / 18;
                    int num51 = i - (int) tile1.frameX / 18;
                    Wiring.SkipWire(num51, num50);
                    Wiring.SkipWire(num51, num50 + 1);
                    if (!Wiring.CheckMech(num51, num50, 30))
                      return;
                    bool flag5 = false;
                    for (int index12 = 0; index12 < 1000; ++index12)
                    {
                      if (Main.projectile[index12].active && Main.projectile[index12].aiStyle == 73 && ((double) Main.projectile[index12].ai[0] == (double) num51 && (double) Main.projectile[index12].ai[1] == (double) num50))
                      {
                        flag5 = true;
                        break;
                      }
                    }
                    if (flag5)
                      return;
                    Projectile.NewProjectile((float) (num51 * 16 + 8), (float) (num50 * 16 + 2), 0.0f, 0.0f, 419 + Main.rand.Next(4), 0, 0.0f, Main.myPlayer, (float) num51, (float) num50);
                    return;
                  case 429:
                    int num52 = (int) Main.tile[i, j].frameX / 18;
                    bool flag6 = num52 % 2 >= 1;
                    bool flag7 = num52 % 4 >= 2;
                    bool flag8 = num52 % 8 >= 4;
                    bool flag9 = num52 % 16 >= 8;
                    bool flag10 = false;
                    short num53 = 0;
                    switch (Wiring._currentWireColor)
                    {
                      case 1:
                        num53 = (short) 18;
                        flag10 = !flag6;
                        break;
                      case 2:
                        num53 = (short) 72;
                        flag10 = !flag8;
                        break;
                      case 3:
                        num53 = (short) 36;
                        flag10 = !flag7;
                        break;
                      case 4:
                        num53 = (short) 144;
                        flag10 = !flag9;
                        break;
                    }
                    if (flag10)
                      tile1.frameX += num53;
                    else
                      tile1.frameX -= num53;
                    NetMessage.SendTileSquare(-1, i, j, 1, TileChangeType.None);
                    return;
                  case 565:
                    int num54 = (int) tile1.frameX / 18;
                    while (num54 >= 2)
                      num54 -= 2;
                    int num55 = (int) tile1.frameY / 18;
                    while (num55 >= 2)
                      num55 -= 2;
                    int index13 = i - num54;
                    int index14 = j - num55;
                    int num56 = 36;
                    if (Main.tile[index13, index14].frameX >= (short) 36)
                      num56 = -36;
                    for (int x = index13; x < index13 + 2; ++x)
                    {
                      for (int y3 = index14; y3 < index14 + 2; ++y3)
                      {
                        Wiring.SkipWire(x, y3);
                        Main.tile[x, y3].frameX += (short) num56;
                      }
                    }
                    NetMessage.SendTileSquare(-1, index13 + 1, index14 + 1, 3, TileChangeType.None);
                    return;
                  default:
                    if (type == 126 || type == 95 || (type == 100 || type == 173) || type == 564)
                    {
                      int num8 = (int) tile1.frameY / 18;
                      while (num8 >= 2)
                        num8 -= 2;
                      int index12 = j - num8;
                      int num39 = (int) tile1.frameX / 18;
                      if (num39 > 1)
                        num39 -= 2;
                      int index15 = i - num39;
                      short num57 = 36;
                      if (Main.tile[index15, index12].frameX > (short) 0)
                        num57 = (short) -36;
                      Main.tile[index15, index12].frameX += num57;
                      Main.tile[index15, index12 + 1].frameX += num57;
                      Main.tile[index15 + 1, index12].frameX += num57;
                      Main.tile[index15 + 1, index12 + 1].frameX += num57;
                      Wiring.SkipWire(index15, index12);
                      Wiring.SkipWire(index15 + 1, index12);
                      Wiring.SkipWire(index15, index12 + 1);
                      Wiring.SkipWire(index15 + 1, index12 + 1);
                      NetMessage.SendTileSquare(-1, index15, index12, 3, TileChangeType.None);
                      return;
                    }
                    switch (type)
                    {
                      case 34:
                        int num58 = (int) tile1.frameY / 18;
                        while (num58 >= 3)
                          num58 -= 3;
                        int index16 = j - num58;
                        int num59 = (int) tile1.frameX % 108 / 18;
                        if (num59 > 2)
                          num59 -= 3;
                        int index17 = i - num59;
                        short num60 = 54;
                        if ((int) Main.tile[index17, index16].frameX % 108 > 0)
                          num60 = (short) -54;
                        for (int x = index17; x < index17 + 3; ++x)
                        {
                          for (int y3 = index16; y3 < index16 + 3; ++y3)
                          {
                            Main.tile[x, y3].frameX += num60;
                            Wiring.SkipWire(x, y3);
                          }
                        }
                        NetMessage.SendTileSquare(-1, index17 + 1, index16 + 1, 3, TileChangeType.None);
                        return;
                      case 314:
                        if (!Wiring.CheckMech(i, j, 5))
                          return;
                        Minecart.FlipSwitchTrack(i, j);
                        return;
                      case 593:
                        int index18 = i;
                        int index19 = j;
                        Wiring.SkipWire(index18, index19);
                        short num61 = Main.tile[index18, index19].frameX != (short) 0 ? (short) -18 : (short) 18;
                        Main.tile[index18, index19].frameX += num61;
                        if (Main.netMode == 2)
                          NetMessage.SendTileRange(-1, index18, index19, 1, 1, TileChangeType.None);
                        int num62 = num61 > (short) 0 ? 4 : 3;
                        Animation.NewTemporaryAnimation(num62, (ushort) 593, index18, index19);
                        NetMessage.SendTemporaryAnimation(-1, num62, 593, index18, index19);
                        return;
                      case 594:
                        int num63 = (int) tile1.frameY / 18;
                        while (num63 >= 2)
                          num63 -= 2;
                        int index20 = j - num63;
                        int num64 = (int) tile1.frameX / 18;
                        if (num64 > 1)
                          num64 -= 2;
                        int index21 = i - num64;
                        Wiring.SkipWire(index21, index20);
                        Wiring.SkipWire(index21, index20 + 1);
                        Wiring.SkipWire(index21 + 1, index20);
                        Wiring.SkipWire(index21 + 1, index20 + 1);
                        short num65 = Main.tile[index21, index20].frameX != (short) 0 ? (short) -36 : (short) 36;
                        for (int index12 = 0; index12 < 2; ++index12)
                        {
                          for (int index15 = 0; index15 < 2; ++index15)
                            Main.tile[index21 + index12, index20 + index15].frameX += num65;
                        }
                        if (Main.netMode == 2)
                          NetMessage.SendTileRange(-1, index21, index20, 2, 2, TileChangeType.None);
                        int num66 = num65 > (short) 0 ? 4 : 3;
                        Animation.NewTemporaryAnimation(num66, (ushort) 594, index21, index20);
                        NetMessage.SendTemporaryAnimation(-1, num66, 594, index21, index20);
                        return;
                      default:
                        if (type == 33 || type == 174 || (type == 49 || type == 372))
                        {
                          short num8 = 18;
                          if (tile1.frameX > (short) 0)
                            num8 = (short) -18;
                          tile1.frameX += num8;
                          NetMessage.SendTileSquare(-1, i, j, 3, TileChangeType.None);
                          return;
                        }
                        switch (type)
                        {
                          case 92:
                            int num67 = j - (int) tile1.frameY / 18;
                            short num68 = 18;
                            if (tile1.frameX > (short) 0)
                              num68 = (short) -18;
                            for (int y3 = num67; y3 < num67 + 6; ++y3)
                            {
                              Main.tile[i, y3].frameX += num68;
                              Wiring.SkipWire(i, y3);
                            }
                            NetMessage.SendTileSquare(-1, i, num67 + 3, 7, TileChangeType.None);
                            return;
                          case 137:
                            int num69 = (int) tile1.frameY / 18;
                            Vector2 vector2_2 = Vector2.Zero;
                            float SpeedX2 = 0.0f;
                            float SpeedY2 = 0.0f;
                            int Type2 = 0;
                            int Damage2 = 0;
                            switch (num69)
                            {
                              case 0:
                              case 1:
                              case 2:
                                if (Wiring.CheckMech(i, j, 200))
                                {
                                  int num8 = tile1.frameX == (short) 0 ? -1 : (tile1.frameX == (short) 18 ? 1 : 0);
                                  int num39 = tile1.frameX < (short) 36 ? 0 : (tile1.frameX < (short) 72 ? -1 : 1);
                                  vector2_2 = new Vector2((float) (i * 16 + 8 + 10 * num8), (float) (j * 16 + 8 + 10 * num39));
                                  float num57 = 3f;
                                  if (num69 == 0)
                                  {
                                    Type2 = 98;
                                    Damage2 = 20;
                                    num57 = 12f;
                                  }
                                  if (num69 == 1)
                                  {
                                    Type2 = 184;
                                    Damage2 = 40;
                                    num57 = 12f;
                                  }
                                  if (num69 == 2)
                                  {
                                    Type2 = 187;
                                    Damage2 = 40;
                                    num57 = 5f;
                                  }
                                  SpeedX2 = (float) num8 * num57;
                                  SpeedY2 = (float) num39 * num57;
                                  break;
                                }
                                break;
                              case 3:
                                if (Wiring.CheckMech(i, j, 300))
                                {
                                  int num8 = 200;
                                  for (int index12 = 0; index12 < 1000; ++index12)
                                  {
                                    if (Main.projectile[index12].active && Main.projectile[index12].type == Type2)
                                    {
                                      float num39 = (new Vector2((float) (i * 16 + 8), (float) (j * 18 + 8)) - Main.projectile[index12].Center).Length();
                                      if ((double) num39 < 50.0)
                                        num8 -= 50;
                                      else if ((double) num39 < 100.0)
                                        num8 -= 15;
                                      else if ((double) num39 < 200.0)
                                        num8 -= 10;
                                      else if ((double) num39 < 300.0)
                                        num8 -= 8;
                                      else if ((double) num39 < 400.0)
                                        num8 -= 6;
                                      else if ((double) num39 < 500.0)
                                        num8 -= 5;
                                      else if ((double) num39 < 700.0)
                                        num8 -= 4;
                                      else if ((double) num39 < 900.0)
                                        num8 -= 3;
                                      else if ((double) num39 < 1200.0)
                                        num8 -= 2;
                                      else
                                        --num8;
                                    }
                                  }
                                  if (num8 > 0)
                                  {
                                    Type2 = 185;
                                    Damage2 = 40;
                                    int num39 = 0;
                                    int num57 = 0;
                                    switch ((int) tile1.frameX / 18)
                                    {
                                      case 0:
                                      case 1:
                                        num39 = 0;
                                        num57 = 1;
                                        break;
                                      case 2:
                                        num39 = 0;
                                        num57 = -1;
                                        break;
                                      case 3:
                                        num39 = -1;
                                        num57 = 0;
                                        break;
                                      case 4:
                                        num39 = 1;
                                        num57 = 0;
                                        break;
                                    }
                                    SpeedX2 = (float) (4 * num39) + (float) Main.rand.Next((num39 == 1 ? 20 : 0) - 20, 21 - (num39 == -1 ? 20 : 0)) * 0.05f;
                                    SpeedY2 = (float) (4 * num57) + (float) Main.rand.Next((num57 == 1 ? 20 : 0) - 20, 21 - (num57 == -1 ? 20 : 0)) * 0.05f;
                                    vector2_2 = new Vector2((float) (i * 16 + 8 + 14 * num39), (float) (j * 16 + 8 + 14 * num57));
                                    break;
                                  }
                                  break;
                                }
                                break;
                              case 4:
                                if (Wiring.CheckMech(i, j, 90))
                                {
                                  int num8 = 0;
                                  int num39 = 0;
                                  switch ((int) tile1.frameX / 18)
                                  {
                                    case 0:
                                    case 1:
                                      num8 = 0;
                                      num39 = 1;
                                      break;
                                    case 2:
                                      num8 = 0;
                                      num39 = -1;
                                      break;
                                    case 3:
                                      num8 = -1;
                                      num39 = 0;
                                      break;
                                    case 4:
                                      num8 = 1;
                                      num39 = 0;
                                      break;
                                  }
                                  SpeedX2 = (float) (8 * num8);
                                  SpeedY2 = (float) (8 * num39);
                                  Damage2 = 60;
                                  Type2 = 186;
                                  vector2_2 = new Vector2((float) (i * 16 + 8 + 18 * num8), (float) (j * 16 + 8 + 18 * num39));
                                  break;
                                }
                                break;
                            }
                            switch (num69 + 10)
                            {
                              case 0:
                                if (Wiring.CheckMech(i, j, 200))
                                {
                                  int num8 = -1;
                                  if (tile1.frameX != (short) 0)
                                    num8 = 1;
                                  SpeedX2 = (float) (12 * num8);
                                  Damage2 = 20;
                                  Type2 = 98;
                                  vector2_2 = new Vector2((float) (i * 16 + 8), (float) (j * 16 + 7));
                                  vector2_2.X += (float) (10 * num8);
                                  vector2_2.Y += 2f;
                                  break;
                                }
                                break;
                              case 1:
                                if (Wiring.CheckMech(i, j, 200))
                                {
                                  int num8 = -1;
                                  if (tile1.frameX != (short) 0)
                                    num8 = 1;
                                  SpeedX2 = (float) (12 * num8);
                                  Damage2 = 40;
                                  Type2 = 184;
                                  vector2_2 = new Vector2((float) (i * 16 + 8), (float) (j * 16 + 7));
                                  vector2_2.X += (float) (10 * num8);
                                  vector2_2.Y += 2f;
                                  break;
                                }
                                break;
                              case 2:
                                if (Wiring.CheckMech(i, j, 200))
                                {
                                  int num8 = -1;
                                  if (tile1.frameX != (short) 0)
                                    num8 = 1;
                                  SpeedX2 = (float) (5 * num8);
                                  Damage2 = 40;
                                  Type2 = 187;
                                  vector2_2 = new Vector2((float) (i * 16 + 8), (float) (j * 16 + 7));
                                  vector2_2.X += (float) (10 * num8);
                                  vector2_2.Y += 2f;
                                  break;
                                }
                                break;
                              case 3:
                                if (Wiring.CheckMech(i, j, 300))
                                {
                                  Type2 = 185;
                                  int num8 = 200;
                                  for (int index12 = 0; index12 < 1000; ++index12)
                                  {
                                    if (Main.projectile[index12].active && Main.projectile[index12].type == Type2)
                                    {
                                      float num39 = (new Vector2((float) (i * 16 + 8), (float) (j * 18 + 8)) - Main.projectile[index12].Center).Length();
                                      if ((double) num39 < 50.0)
                                        num8 -= 50;
                                      else if ((double) num39 < 100.0)
                                        num8 -= 15;
                                      else if ((double) num39 < 200.0)
                                        num8 -= 10;
                                      else if ((double) num39 < 300.0)
                                        num8 -= 8;
                                      else if ((double) num39 < 400.0)
                                        num8 -= 6;
                                      else if ((double) num39 < 500.0)
                                        num8 -= 5;
                                      else if ((double) num39 < 700.0)
                                        num8 -= 4;
                                      else if ((double) num39 < 900.0)
                                        num8 -= 3;
                                      else if ((double) num39 < 1200.0)
                                        num8 -= 2;
                                      else
                                        --num8;
                                    }
                                  }
                                  if (num8 > 0)
                                  {
                                    SpeedX2 = (float) Main.rand.Next(-20, 21) * 0.05f;
                                    SpeedY2 = (float) (4.0 + (double) Main.rand.Next(0, 21) * 0.0500000007450581);
                                    Damage2 = 40;
                                    vector2_2 = new Vector2((float) (i * 16 + 8), (float) (j * 16 + 16));
                                    vector2_2.Y += 6f;
                                    Projectile.NewProjectile((float) (int) vector2_2.X, (float) (int) vector2_2.Y, SpeedX2, SpeedY2, Type2, Damage2, 2f, Main.myPlayer, 0.0f, 0.0f);
                                    break;
                                  }
                                  break;
                                }
                                break;
                              case 4:
                                if (Wiring.CheckMech(i, j, 90))
                                {
                                  SpeedX2 = 0.0f;
                                  SpeedY2 = 8f;
                                  Damage2 = 60;
                                  Type2 = 186;
                                  vector2_2 = new Vector2((float) (i * 16 + 8), (float) (j * 16 + 16));
                                  vector2_2.Y += 10f;
                                  break;
                                }
                                break;
                            }
                            if (Type2 == 0)
                              return;
                            Projectile.NewProjectile((float) (int) vector2_2.X, (float) (int) vector2_2.Y, SpeedX2, SpeedY2, Type2, Damage2, 2f, Main.myPlayer, 0.0f, 0.0f);
                            return;
                          case 443:
                            Wiring.GeyserTrap(i, j);
                            return;
                          case 531:
                            int num70 = (int) tile1.frameX / 36;
                            int num71 = (int) tile1.frameY / 54;
                            int i3 = i - ((int) tile1.frameX - num70 * 36) / 18;
                            int j3 = j - ((int) tile1.frameY - num71 * 54) / 18;
                            if (!Wiring.CheckMech(i3, j3, 900))
                              return;
                            Vector2 vector2_3 = new Vector2((float) (i3 + 1), (float) j3) * 16f;
                            vector2_3.Y += 28f;
                            int Type3 = 99;
                            int Damage3 = 70;
                            float KnockBack2 = 10f;
                            if (Type3 == 0)
                              return;
                            Projectile.NewProjectile((float) (int) vector2_3.X, (float) (int) vector2_3.Y, 0.0f, 0.0f, Type3, Damage3, KnockBack2, Main.myPlayer, 0.0f, 0.0f);
                            return;
                          default:
                            if (type == 139 || type == 35)
                            {
                              WorldGen.SwitchMB(i, j);
                              return;
                            }
                            if (type == 207)
                            {
                              WorldGen.SwitchFountain(i, j);
                              return;
                            }
                            if (type == 410 || type == 480 || type == 509)
                            {
                              WorldGen.SwitchMonolith(i, j);
                              return;
                            }
                            switch (type)
                            {
                              case 141:
                                WorldGen.KillTile(i, j, false, false, true);
                                NetMessage.SendTileSquare(-1, i, j, 1, TileChangeType.None);
                                Projectile.NewProjectile((float) (i * 16 + 8), (float) (j * 16 + 8), 0.0f, 0.0f, 108, 500, 10f, Main.myPlayer, 0.0f, 0.0f);
                                return;
                              case 210:
                                WorldGen.ExplodeMine(i, j);
                                return;
                              case 455:
                                BirthdayParty.ToggleManualParty();
                                return;
                              default:
                                if (type == 142 || type == 143)
                                {
                                  int y3 = j - (int) tile1.frameY / 18;
                                  int num8 = (int) tile1.frameX / 18;
                                  if (num8 > 1)
                                    num8 -= 2;
                                  int x = i - num8;
                                  Wiring.SkipWire(x, y3);
                                  Wiring.SkipWire(x, y3 + 1);
                                  Wiring.SkipWire(x + 1, y3);
                                  Wiring.SkipWire(x + 1, y3 + 1);
                                  if (type == 142)
                                  {
                                    for (int index12 = 0; index12 < 4 && Wiring._numInPump < 19; ++index12)
                                    {
                                      int num39;
                                      int num57;
                                      switch (index12)
                                      {
                                        case 0:
                                          num39 = x;
                                          num57 = y3 + 1;
                                          break;
                                        case 1:
                                          num39 = x + 1;
                                          num57 = y3 + 1;
                                          break;
                                        case 2:
                                          num39 = x;
                                          num57 = y3;
                                          break;
                                        default:
                                          num39 = x + 1;
                                          num57 = y3;
                                          break;
                                      }
                                      Wiring._inPumpX[Wiring._numInPump] = num39;
                                      Wiring._inPumpY[Wiring._numInPump] = num57;
                                      ++Wiring._numInPump;
                                    }
                                    return;
                                  }
                                  for (int index12 = 0; index12 < 4 && Wiring._numOutPump < 19; ++index12)
                                  {
                                    int num39;
                                    int num57;
                                    switch (index12)
                                    {
                                      case 0:
                                        num39 = x;
                                        num57 = y3 + 1;
                                        break;
                                      case 1:
                                        num39 = x + 1;
                                        num57 = y3 + 1;
                                        break;
                                      case 2:
                                        num39 = x;
                                        num57 = y3;
                                        break;
                                      default:
                                        num39 = x + 1;
                                        num57 = y3;
                                        break;
                                    }
                                    Wiring._outPumpX[Wiring._numOutPump] = num39;
                                    Wiring._outPumpY[Wiring._numOutPump] = num57;
                                    ++Wiring._numOutPump;
                                  }
                                  return;
                                }
                                switch (type)
                                {
                                  case 105:
                                    int num72 = j - (int) tile1.frameY / 18;
                                    int num73 = (int) tile1.frameX / 18;
                                    int num74 = 0;
                                    while (num73 >= 2)
                                    {
                                      num73 -= 2;
                                      ++num74;
                                    }
                                    int num75 = i - num73;
                                    int num76 = i - (int) tile1.frameX % 36 / 18;
                                    int num77 = j - (int) tile1.frameY % 54 / 18;
                                    int num78 = (int) tile1.frameY / 54 % 3;
                                    int num79 = (int) tile1.frameX / 36 + num78 * 55;
                                    Wiring.SkipWire(num76, num77);
                                    Wiring.SkipWire(num76, num77 + 1);
                                    Wiring.SkipWire(num76, num77 + 2);
                                    Wiring.SkipWire(num76 + 1, num77);
                                    Wiring.SkipWire(num76 + 1, num77 + 1);
                                    Wiring.SkipWire(num76 + 1, num77 + 2);
                                    int X = num76 * 16 + 16;
                                    int Y = (num77 + 3) * 16;
                                    int index22 = -1;
                                    int num80 = -1;
                                    bool flag11 = true;
                                    bool flag12 = false;
                                    switch (num79)
                                    {
                                      case 5:
                                        num80 = 73;
                                        break;
                                      case 13:
                                        num80 = 24;
                                        break;
                                      case 30:
                                        num80 = 6;
                                        break;
                                      case 35:
                                        num80 = 2;
                                        break;
                                      case 51:
                                        num80 = (int) Utils.SelectRandom<short>(Main.rand, (short) 299, (short) 538);
                                        break;
                                      case 52:
                                        num80 = 356;
                                        break;
                                      case 53:
                                        num80 = 357;
                                        break;
                                      case 54:
                                        num80 = (int) Utils.SelectRandom<short>(Main.rand, (short) 355, (short) 358);
                                        break;
                                      case 55:
                                        num80 = (int) Utils.SelectRandom<short>(Main.rand, (short) 367, (short) 366);
                                        break;
                                      case 56:
                                        num80 = (int) Utils.SelectRandom<short>(Main.rand, (short) 359, (short) 359, (short) 359, (short) 359, (short) 360);
                                        break;
                                      case 57:
                                        num80 = 377;
                                        break;
                                      case 58:
                                        num80 = 300;
                                        break;
                                      case 59:
                                        num80 = (int) Utils.SelectRandom<short>(Main.rand, (short) 364, (short) 362);
                                        break;
                                      case 60:
                                        num80 = 148;
                                        break;
                                      case 61:
                                        num80 = 361;
                                        break;
                                      case 62:
                                        num80 = (int) Utils.SelectRandom<short>(Main.rand, (short) 487, (short) 486, (short) 485);
                                        break;
                                      case 63:
                                        num80 = 164;
                                        flag11 &= NPC.MechSpawn((float) X, (float) Y, 165);
                                        break;
                                      case 64:
                                        num80 = 86;
                                        flag12 = true;
                                        break;
                                      case 65:
                                        num80 = 490;
                                        break;
                                      case 66:
                                        num80 = 82;
                                        break;
                                      case 67:
                                        num80 = 449;
                                        break;
                                      case 68:
                                        num80 = 167;
                                        break;
                                      case 69:
                                        num80 = 480;
                                        break;
                                      case 70:
                                        num80 = 48;
                                        break;
                                      case 71:
                                        num80 = (int) Utils.SelectRandom<short>(Main.rand, (short) 170, (short) 180, (short) 171);
                                        flag12 = true;
                                        break;
                                      case 72:
                                        num80 = 481;
                                        break;
                                      case 73:
                                        num80 = 482;
                                        break;
                                      case 74:
                                        num80 = 430;
                                        break;
                                      case 75:
                                        num80 = 489;
                                        break;
                                      case 76:
                                        num80 = 611;
                                        break;
                                      case 77:
                                        num80 = 602;
                                        break;
                                      case 78:
                                        num80 = (int) Utils.SelectRandom<short>(Main.rand, (short) 595, (short) 596, (short) 599, (short) 597, (short) 600, (short) 598);
                                        break;
                                      case 79:
                                        num80 = (int) Utils.SelectRandom<short>(Main.rand, (short) 616, (short) 617);
                                        break;
                                    }
                                    if (((num80 == -1 || !Wiring.CheckMech(num76, num77, 30) ? 0 : (NPC.MechSpawn((float) X, (float) Y, num80) ? 1 : 0)) & (flag11 ? 1 : 0)) != 0)
                                    {
                                      if (!flag12 || !Collision.SolidTiles(num76 - 2, num76 + 3, num77, num77 + 2))
                                      {
                                        index22 = NPC.NewNPC(X, Y, num80, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
                                      }
                                      else
                                      {
                                        Vector2 position = new Vector2((float) (X - 4), (float) (Y - 22)) - new Vector2(10f);
                                        Utils.PoofOfSmoke(position);
                                        NetMessage.SendData(106, -1, -1, (NetworkText) null, (int) position.X, position.Y, 0.0f, 0.0f, 0, 0, 0);
                                      }
                                    }
                                    if (index22 <= -1)
                                    {
                                      switch (num79)
                                      {
                                        case 2:
                                          if (Wiring.CheckMech(num76, num77, 600) && Item.MechSpawn((float) X, (float) Y, 184) && (Item.MechSpawn((float) X, (float) Y, 1735) && Item.MechSpawn((float) X, (float) Y, 1868)))
                                          {
                                            Item.NewItem(X, Y - 16, 0, 0, 184, 1, false, 0, false, false);
                                            break;
                                          }
                                          break;
                                        case 4:
                                          if (Wiring.CheckMech(num76, num77, 30) && NPC.MechSpawn((float) X, (float) Y, 1))
                                          {
                                            index22 = NPC.NewNPC(X, Y - 12, 1, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
                                            break;
                                          }
                                          break;
                                        case 7:
                                          if (Wiring.CheckMech(num76, num77, 30) && NPC.MechSpawn((float) X, (float) Y, 49))
                                          {
                                            index22 = NPC.NewNPC(X - 4, Y - 6, 49, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
                                            break;
                                          }
                                          break;
                                        case 8:
                                          if (Wiring.CheckMech(num76, num77, 30) && NPC.MechSpawn((float) X, (float) Y, 55))
                                          {
                                            index22 = NPC.NewNPC(X, Y - 12, 55, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
                                            break;
                                          }
                                          break;
                                        case 9:
                                          int num81 = 46;
                                          if (BirthdayParty.PartyIsUp)
                                            num81 = 540;
                                          if (Wiring.CheckMech(num76, num77, 30) && NPC.MechSpawn((float) X, (float) Y, num81))
                                          {
                                            index22 = NPC.NewNPC(X, Y - 12, num81, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
                                            break;
                                          }
                                          break;
                                        case 10:
                                          if (Wiring.CheckMech(num76, num77, 30) && NPC.MechSpawn((float) X, (float) Y, 21))
                                          {
                                            index22 = NPC.NewNPC(X, Y, 21, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
                                            break;
                                          }
                                          break;
                                        case 16:
                                          if (Wiring.CheckMech(num76, num77, 30) && NPC.MechSpawn((float) X, (float) Y, 42))
                                          {
                                            if (!Collision.SolidTiles(num76 - 1, num76 + 1, num77, num77 + 1))
                                            {
                                              index22 = NPC.NewNPC(X, Y - 12, 42, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
                                              break;
                                            }
                                            Vector2 position = new Vector2((float) (X - 4), (float) (Y - 22)) - new Vector2(10f);
                                            Utils.PoofOfSmoke(position);
                                            NetMessage.SendData(106, -1, -1, (NetworkText) null, (int) position.X, position.Y, 0.0f, 0.0f, 0, 0, 0);
                                            break;
                                          }
                                          break;
                                        case 17:
                                          if (Wiring.CheckMech(num76, num77, 600) && Item.MechSpawn((float) X, (float) Y, 166))
                                          {
                                            Item.NewItem(X, Y - 20, 0, 0, 166, 1, false, 0, false, false);
                                            break;
                                          }
                                          break;
                                        case 18:
                                          if (Wiring.CheckMech(num76, num77, 30) && NPC.MechSpawn((float) X, (float) Y, 67))
                                          {
                                            index22 = NPC.NewNPC(X, Y - 12, 67, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
                                            break;
                                          }
                                          break;
                                        case 23:
                                          if (Wiring.CheckMech(num76, num77, 30) && NPC.MechSpawn((float) X, (float) Y, 63))
                                          {
                                            index22 = NPC.NewNPC(X, Y - 12, 63, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
                                            break;
                                          }
                                          break;
                                        case 27:
                                          if (Wiring.CheckMech(num76, num77, 30) && NPC.MechSpawn((float) X, (float) Y, 85))
                                          {
                                            index22 = NPC.NewNPC(X - 9, Y, 85, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
                                            break;
                                          }
                                          break;
                                        case 28:
                                          if (Wiring.CheckMech(num76, num77, 30) && NPC.MechSpawn((float) X, (float) Y, 74))
                                          {
                                            index22 = NPC.NewNPC(X, Y - 12, (int) Utils.SelectRandom<short>(Main.rand, (short) 74, (short) 297, (short) 298), 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
                                            break;
                                          }
                                          break;
                                        case 34:
                                          for (int index12 = 0; index12 < 2; ++index12)
                                          {
                                            for (int index15 = 0; index15 < 3; ++index15)
                                            {
                                              Tile tile2 = Main.tile[num76 + index12, num77 + index15];
                                              tile2.type = (ushort) 349;
                                              tile2.frameX = (short) (index12 * 18 + 216);
                                              tile2.frameY = (short) (index15 * 18);
                                            }
                                          }
                                          Animation.NewTemporaryAnimation(0, (ushort) 349, num76, num77);
                                          if (Main.netMode == 2)
                                          {
                                            NetMessage.SendTileRange(-1, num76, num77, 2, 3, TileChangeType.None);
                                            break;
                                          }
                                          break;
                                        case 37:
                                          if (Wiring.CheckMech(num76, num77, 600) && Item.MechSpawn((float) X, (float) Y, 58) && (Item.MechSpawn((float) X, (float) Y, 1734) && Item.MechSpawn((float) X, (float) Y, 1867)))
                                          {
                                            Item.NewItem(X, Y - 16, 0, 0, 58, 1, false, 0, false, false);
                                            break;
                                          }
                                          break;
                                        case 40:
                                          if (Wiring.CheckMech(num76, num77, 300))
                                          {
                                            int length = 50;
                                            int[] numArray = new int[length];
                                            int maxValue = 0;
                                            for (int index12 = 0; index12 < 200; ++index12)
                                            {
                                              if (Main.npc[index12].active && (Main.npc[index12].type == 17 || Main.npc[index12].type == 19 || (Main.npc[index12].type == 22 || Main.npc[index12].type == 38) || (Main.npc[index12].type == 54 || Main.npc[index12].type == 107 || (Main.npc[index12].type == 108 || Main.npc[index12].type == 142)) || (Main.npc[index12].type == 160 || Main.npc[index12].type == 207 || (Main.npc[index12].type == 209 || Main.npc[index12].type == 227) || (Main.npc[index12].type == 228 || Main.npc[index12].type == 229 || (Main.npc[index12].type == 368 || Main.npc[index12].type == 369))) || (Main.npc[index12].type == 550 || Main.npc[index12].type == 441 || Main.npc[index12].type == 588)))
                                              {
                                                numArray[maxValue] = index12;
                                                ++maxValue;
                                                if (maxValue >= length)
                                                  break;
                                              }
                                            }
                                            if (maxValue > 0)
                                            {
                                              int number = numArray[Main.rand.Next(maxValue)];
                                              Main.npc[number].position.X = (float) (X - Main.npc[number].width / 2);
                                              Main.npc[number].position.Y = (float) (Y - Main.npc[number].height - 1);
                                              NetMessage.SendData(23, -1, -1, (NetworkText) null, number, 0.0f, 0.0f, 0.0f, 0, 0, 0);
                                              break;
                                            }
                                            break;
                                          }
                                          break;
                                        case 41:
                                          if (Wiring.CheckMech(num76, num77, 300))
                                          {
                                            int length = 50;
                                            int[] numArray = new int[length];
                                            int maxValue = 0;
                                            for (int index12 = 0; index12 < 200; ++index12)
                                            {
                                              if (Main.npc[index12].active && (Main.npc[index12].type == 18 || Main.npc[index12].type == 20 || (Main.npc[index12].type == 124 || Main.npc[index12].type == 178) || (Main.npc[index12].type == 208 || Main.npc[index12].type == 353 || Main.npc[index12].type == 633)))
                                              {
                                                numArray[maxValue] = index12;
                                                ++maxValue;
                                                if (maxValue >= length)
                                                  break;
                                              }
                                            }
                                            if (maxValue > 0)
                                            {
                                              int number = numArray[Main.rand.Next(maxValue)];
                                              Main.npc[number].position.X = (float) (X - Main.npc[number].width / 2);
                                              Main.npc[number].position.Y = (float) (Y - Main.npc[number].height - 1);
                                              NetMessage.SendData(23, -1, -1, (NetworkText) null, number, 0.0f, 0.0f, 0.0f, 0, 0, 0);
                                              break;
                                            }
                                            break;
                                          }
                                          break;
                                        case 42:
                                          if (Wiring.CheckMech(num76, num77, 30) && NPC.MechSpawn((float) X, (float) Y, 58))
                                          {
                                            index22 = NPC.NewNPC(X, Y - 12, 58, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
                                            break;
                                          }
                                          break;
                                        case 50:
                                          if (Wiring.CheckMech(num76, num77, 30) && NPC.MechSpawn((float) X, (float) Y, 65))
                                          {
                                            if (!Collision.SolidTiles(num76 - 2, num76 + 3, num77, num77 + 2))
                                            {
                                              index22 = NPC.NewNPC(X, Y - 12, 65, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
                                              break;
                                            }
                                            Vector2 position = new Vector2((float) (X - 4), (float) (Y - 22)) - new Vector2(10f);
                                            Utils.PoofOfSmoke(position);
                                            NetMessage.SendData(106, -1, -1, (NetworkText) null, (int) position.X, position.Y, 0.0f, 0.0f, 0, 0, 0);
                                            break;
                                          }
                                          break;
                                      }
                                    }
                                    if (index22 < 0)
                                      return;
                                    Main.npc[index22].value = 0.0f;
                                    Main.npc[index22].npcSlots = 0.0f;
                                    Main.npc[index22].SpawnedFromStatue = true;
                                    return;
                                  case 349:
                                    int num82 = (int) tile1.frameY / 18 % 3;
                                    int index23 = j - num82;
                                    int num83 = (int) tile1.frameX / 18;
                                    while (num83 >= 2)
                                      num83 -= 2;
                                    int index24 = i - num83;
                                    Wiring.SkipWire(index24, index23);
                                    Wiring.SkipWire(index24, index23 + 1);
                                    Wiring.SkipWire(index24, index23 + 2);
                                    Wiring.SkipWire(index24 + 1, index23);
                                    Wiring.SkipWire(index24 + 1, index23 + 1);
                                    Wiring.SkipWire(index24 + 1, index23 + 2);
                                    short num84 = Main.tile[index24, index23].frameX != (short) 0 ? (short) -216 : (short) 216;
                                    for (int index12 = 0; index12 < 2; ++index12)
                                    {
                                      for (int index15 = 0; index15 < 3; ++index15)
                                        Main.tile[index24 + index12, index23 + index15].frameX += num84;
                                    }
                                    if (Main.netMode == 2)
                                      NetMessage.SendTileRange(-1, index24, index23, 2, 3, TileChangeType.None);
                                    Animation.NewTemporaryAnimation(num84 > (short) 0 ? 0 : 1, (ushort) 349, index24, index23);
                                    return;
                                  case 506:
                                    int num85 = (int) tile1.frameY / 18 % 3;
                                    int index25 = j - num85;
                                    int num86 = (int) tile1.frameX / 18;
                                    while (num86 >= 2)
                                      num86 -= 2;
                                    int index26 = i - num86;
                                    Wiring.SkipWire(index26, index25);
                                    Wiring.SkipWire(index26, index25 + 1);
                                    Wiring.SkipWire(index26, index25 + 2);
                                    Wiring.SkipWire(index26 + 1, index25);
                                    Wiring.SkipWire(index26 + 1, index25 + 1);
                                    Wiring.SkipWire(index26 + 1, index25 + 2);
                                    short num87 = Main.tile[index26, index25].frameX >= (short) 72 ? (short) -72 : (short) 72;
                                    for (int index12 = 0; index12 < 2; ++index12)
                                    {
                                      for (int index15 = 0; index15 < 3; ++index15)
                                        Main.tile[index26 + index12, index25 + index15].frameX += num87;
                                    }
                                    if (Main.netMode != 2)
                                      return;
                                    NetMessage.SendTileRange(-1, index26, index25, 2, 3, TileChangeType.None);
                                    return;
                                  case 546:
                                    tile1.type = (ushort) 557;
                                    WorldGen.SquareTileFrame(i, j, true);
                                    NetMessage.SendTileSquare(-1, i, j, 1, TileChangeType.None);
                                    return;
                                  case 557:
                                    tile1.type = (ushort) 546;
                                    WorldGen.SquareTileFrame(i, j, true);
                                    NetMessage.SendTileSquare(-1, i, j, 1, TileChangeType.None);
                                    return;
                                  default:
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }
      }
    }

    private static void GeyserTrap(int i, int j)
    {
      Tile tile = Main.tile[i, j];
      if (tile.type != (ushort) 443)
        return;
      int num = (int) tile.frameX / 36;
      int i1 = i - ((int) tile.frameX - num * 36) / 18;
      int j1 = j;
      if (!Wiring.CheckMech(i1, j1, 200))
        return;
      Vector2 zero = Vector2.Zero;
      Vector2 vector2_1 = Vector2.Zero;
      int Type = 654;
      int Damage = 20;
      Vector2 vector2_2;
      if (num < 2)
      {
        vector2_2 = new Vector2((float) (i1 + 1), (float) j1) * 16f;
        vector2_1 = new Vector2(0.0f, -8f);
      }
      else
      {
        vector2_2 = new Vector2((float) (i1 + 1), (float) (j1 + 1)) * 16f;
        vector2_1 = new Vector2(0.0f, 8f);
      }
      if (Type == 0)
        return;
      Projectile.NewProjectile((float) (int) vector2_2.X, (float) (int) vector2_2.Y, vector2_1.X, vector2_1.Y, Type, Damage, 2f, Main.myPlayer, 0.0f, 0.0f);
    }

    private static void Teleport()
    {
      if ((double) Wiring._teleport[0].X < (double) Wiring._teleport[1].X + 3.0 && (double) Wiring._teleport[0].X > (double) Wiring._teleport[1].X - 3.0 && ((double) Wiring._teleport[0].Y > (double) Wiring._teleport[1].Y - 3.0 && (double) Wiring._teleport[0].Y < (double) Wiring._teleport[1].Y))
        return;
      Rectangle[] rectangleArray = new Rectangle[2];
      rectangleArray[0].X = (int) ((double) Wiring._teleport[0].X * 16.0);
      rectangleArray[0].Width = 48;
      rectangleArray[0].Height = 48;
      rectangleArray[0].Y = (int) ((double) Wiring._teleport[0].Y * 16.0 - (double) rectangleArray[0].Height);
      rectangleArray[1].X = (int) ((double) Wiring._teleport[1].X * 16.0);
      rectangleArray[1].Width = 48;
      rectangleArray[1].Height = 48;
      rectangleArray[1].Y = (int) ((double) Wiring._teleport[1].Y * 16.0 - (double) rectangleArray[1].Height);
      for (int index1 = 0; index1 < 2; ++index1)
      {
        Vector2 vector2_1 = new Vector2((float) (rectangleArray[1].X - rectangleArray[0].X), (float) (rectangleArray[1].Y - rectangleArray[0].Y));
        if (index1 == 1)
          vector2_1 = new Vector2((float) (rectangleArray[0].X - rectangleArray[1].X), (float) (rectangleArray[0].Y - rectangleArray[1].Y));
        if (!Wiring.blockPlayerTeleportationForOneIteration)
        {
          for (int playerIndex = 0; playerIndex < (int) byte.MaxValue; ++playerIndex)
          {
            if (Main.player[playerIndex].active && !Main.player[playerIndex].dead && (!Main.player[playerIndex].teleporting && Wiring.TeleporterHitboxIntersects(rectangleArray[index1], Main.player[playerIndex].Hitbox)))
            {
              Vector2 vector2_2 = Main.player[playerIndex].position + vector2_1;
              Main.player[playerIndex].teleporting = true;
              if (Main.netMode == 2)
                RemoteClient.CheckSection(playerIndex, vector2_2, 1);
              Main.player[playerIndex].Teleport(vector2_2, 0, 0);
              if (Main.netMode == 2)
                NetMessage.SendData(65, -1, -1, (NetworkText) null, 0, (float) playerIndex, vector2_2.X, vector2_2.Y, 0, 0, 0);
            }
          }
        }
        for (int index2 = 0; index2 < 200; ++index2)
        {
          if (Main.npc[index2].active && !Main.npc[index2].teleporting && (Main.npc[index2].lifeMax > 5 && !Main.npc[index2].boss) && !Main.npc[index2].noTileCollide)
          {
            int type = Main.npc[index2].type;
            if (!NPCID.Sets.TeleportationImmune[type] && Wiring.TeleporterHitboxIntersects(rectangleArray[index1], Main.npc[index2].Hitbox))
            {
              Main.npc[index2].teleporting = true;
              Main.npc[index2].Teleport(Main.npc[index2].position + vector2_1, 0, 0);
            }
          }
        }
      }
      for (int index = 0; index < (int) byte.MaxValue; ++index)
        Main.player[index].teleporting = false;
      for (int index = 0; index < 200; ++index)
        Main.npc[index].teleporting = false;
    }

    private static bool TeleporterHitboxIntersects(Rectangle teleporter, Rectangle entity)
    {
      Rectangle rectangle = Rectangle.Union(teleporter, entity);
      return rectangle.Width <= teleporter.Width + entity.Width && rectangle.Height <= teleporter.Height + entity.Height;
    }

    private static void DeActive(int i, int j)
    {
      if (!Main.tile[i, j].active() || Main.tile[i, j].type == (ushort) 226 && (double) j > Main.worldSurface && !NPC.downedPlantBoss)
        return;
      bool flag = Main.tileSolid[(int) Main.tile[i, j].type] && !TileID.Sets.NotReallySolid[(int) Main.tile[i, j].type];
      switch (Main.tile[i, j].type)
      {
        case 314:
        case 386:
        case 387:
        case 388:
        case 389:
        case 476:
          flag = false;
          break;
      }
      if (!flag || Main.tile[i, j - 1].active() && (TileID.Sets.BasicChest[(int) Main.tile[i, j - 1].type] || Main.tile[i, j - 1].type == (ushort) 26 || (Main.tile[i, j - 1].type == (ushort) 77 || Main.tile[i, j - 1].type == (ushort) 88) || (Main.tile[i, j - 1].type == (ushort) 470 || Main.tile[i, j - 1].type == (ushort) 475 || (Main.tile[i, j - 1].type == (ushort) 237 || Main.tile[i, j - 1].type == (ushort) 597)) || !WorldGen.CanKillTile(i, j - 1)))
        return;
      Main.tile[i, j].inActive(true);
      WorldGen.SquareTileFrame(i, j, false);
      if (Main.netMode == 1)
        return;
      NetMessage.SendTileSquare(-1, i, j, 1, TileChangeType.None);
    }

    private static void ReActive(int i, int j)
    {
      Main.tile[i, j].inActive(false);
      WorldGen.SquareTileFrame(i, j, false);
      if (Main.netMode == 1)
        return;
      NetMessage.SendTileSquare(-1, i, j, 1, TileChangeType.None);
    }

    private static void MassWireOperationInner(
      Point ps,
      Point pe,
      Vector2 dropPoint,
      bool dir,
      ref int wireCount,
      ref int actuatorCount)
    {
      Math.Abs(ps.X - pe.X);
      Math.Abs(ps.Y - pe.Y);
      int num1 = Math.Sign(pe.X - ps.X);
      int num2 = Math.Sign(pe.Y - ps.Y);
      WiresUI.Settings.MultiToolMode toolMode = WiresUI.Settings.ToolMode;
      Point pt = new Point();
      bool flag1 = false;
      Item.StartCachingType(530);
      Item.StartCachingType(849);
      bool flag2 = dir;
      int num3;
      int num4;
      int num5;
      if (flag2)
      {
        pt.X = ps.X;
        num3 = ps.Y;
        num4 = pe.Y;
        num5 = num2;
      }
      else
      {
        pt.Y = ps.Y;
        num3 = ps.X;
        num4 = pe.X;
        num5 = num1;
      }
      for (int index = num3; index != num4 && !flag1; index += num5)
      {
        if (flag2)
          pt.Y = index;
        else
          pt.X = index;
        bool? nullable = Wiring.MassWireOperationStep(pt, toolMode, ref wireCount, ref actuatorCount);
        if (nullable.HasValue && !nullable.Value)
        {
          flag1 = true;
          break;
        }
      }
      int num6;
      int num7;
      int num8;
      if (flag2)
      {
        pt.Y = pe.Y;
        num6 = ps.X;
        num7 = pe.X;
        num8 = num1;
      }
      else
      {
        pt.X = pe.X;
        num6 = ps.Y;
        num7 = pe.Y;
        num8 = num2;
      }
      for (int index = num6; index != num7 && !flag1; index += num8)
      {
        if (!flag2)
          pt.Y = index;
        else
          pt.X = index;
        bool? nullable = Wiring.MassWireOperationStep(pt, toolMode, ref wireCount, ref actuatorCount);
        if (nullable.HasValue && !nullable.Value)
        {
          flag1 = true;
          break;
        }
      }
      if (!flag1)
        Wiring.MassWireOperationStep(pe, toolMode, ref wireCount, ref actuatorCount);
      Item.DropCache(dropPoint, Vector2.Zero, 530, true);
      Item.DropCache(dropPoint, Vector2.Zero, 849, true);
    }

    private static bool? MassWireOperationStep(
      Point pt,
      WiresUI.Settings.MultiToolMode mode,
      ref int wiresLeftToConsume,
      ref int actuatorsLeftToConstume)
    {
      if (!WorldGen.InWorld(pt.X, pt.Y, 1))
        return new bool?();
      Tile tile = Main.tile[pt.X, pt.Y];
      if (tile == null)
        return new bool?();
      if (!mode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Cutter))
      {
        if (mode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Red) && !tile.wire())
        {
          if (wiresLeftToConsume <= 0)
            return new bool?(false);
          --wiresLeftToConsume;
          WorldGen.PlaceWire(pt.X, pt.Y);
          NetMessage.SendData(17, -1, -1, (NetworkText) null, 5, (float) pt.X, (float) pt.Y, 0.0f, 0, 0, 0);
        }
        if (mode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Green) && !tile.wire3())
        {
          if (wiresLeftToConsume <= 0)
            return new bool?(false);
          --wiresLeftToConsume;
          WorldGen.PlaceWire3(pt.X, pt.Y);
          NetMessage.SendData(17, -1, -1, (NetworkText) null, 12, (float) pt.X, (float) pt.Y, 0.0f, 0, 0, 0);
        }
        if (mode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Blue) && !tile.wire2())
        {
          if (wiresLeftToConsume <= 0)
            return new bool?(false);
          --wiresLeftToConsume;
          WorldGen.PlaceWire2(pt.X, pt.Y);
          NetMessage.SendData(17, -1, -1, (NetworkText) null, 10, (float) pt.X, (float) pt.Y, 0.0f, 0, 0, 0);
        }
        if (mode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Yellow) && !tile.wire4())
        {
          if (wiresLeftToConsume <= 0)
            return new bool?(false);
          --wiresLeftToConsume;
          WorldGen.PlaceWire4(pt.X, pt.Y);
          NetMessage.SendData(17, -1, -1, (NetworkText) null, 16, (float) pt.X, (float) pt.Y, 0.0f, 0, 0, 0);
        }
        if (mode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Actuator) && !tile.actuator())
        {
          if (actuatorsLeftToConstume <= 0)
            return new bool?(false);
          --actuatorsLeftToConstume;
          WorldGen.PlaceActuator(pt.X, pt.Y);
          NetMessage.SendData(17, -1, -1, (NetworkText) null, 8, (float) pt.X, (float) pt.Y, 0.0f, 0, 0, 0);
        }
      }
      if (mode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Cutter))
      {
        if (mode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Red) && tile.wire() && WorldGen.KillWire(pt.X, pt.Y))
          NetMessage.SendData(17, -1, -1, (NetworkText) null, 6, (float) pt.X, (float) pt.Y, 0.0f, 0, 0, 0);
        if (mode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Green) && tile.wire3() && WorldGen.KillWire3(pt.X, pt.Y))
          NetMessage.SendData(17, -1, -1, (NetworkText) null, 13, (float) pt.X, (float) pt.Y, 0.0f, 0, 0, 0);
        if (mode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Blue) && tile.wire2() && WorldGen.KillWire2(pt.X, pt.Y))
          NetMessage.SendData(17, -1, -1, (NetworkText) null, 11, (float) pt.X, (float) pt.Y, 0.0f, 0, 0, 0);
        if (mode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Yellow) && tile.wire4() && WorldGen.KillWire4(pt.X, pt.Y))
          NetMessage.SendData(17, -1, -1, (NetworkText) null, 17, (float) pt.X, (float) pt.Y, 0.0f, 0, 0, 0);
        if (mode.HasFlag((Enum) WiresUI.Settings.MultiToolMode.Actuator) && tile.actuator() && WorldGen.KillActuator(pt.X, pt.Y))
          NetMessage.SendData(17, -1, -1, (NetworkText) null, 9, (float) pt.X, (float) pt.Y, 0.0f, 0, 0, 0);
      }
      return new bool?(true);
    }
  }
}
