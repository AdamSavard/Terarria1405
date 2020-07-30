// Decompiled with JetBrains decompiler
// Type: Terraria.Chest
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Achievements;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ObjectData;

namespace Terraria
{
  public class Chest
  {
    public static int[] chestTypeToIcon = new int[52];
    public static int[] chestItemSpawn = new int[52];
    public static int[] chestTypeToIcon2 = new int[14];
    public static int[] chestItemSpawn2 = new int[14];
    public static int[] dresserTypeToIcon = new int[40];
    public static int[] dresserItemSpawn = new int[40];
    private static HashSet<int> _chestInUse = new HashSet<int>();
    public const float chestStackRange = 250f;
    public const int maxChestTypes = 52;
    public const int maxChestTypes2 = 14;
    public const int maxDresserTypes = 40;
    public const int maxItems = 40;
    public const int MaxNameLength = 20;
    public Item[] item;
    public int x;
    public int y;
    public bool bankChest;
    public string name;
    public int frameCounter;
    public int frame;

    public Chest(bool bank = false)
    {
      this.item = new Item[40];
      this.bankChest = bank;
      this.name = string.Empty;
    }

    public override string ToString()
    {
      int num = 0;
      for (int index = 0; index < this.item.Length; ++index)
      {
        if (this.item[index].stack > 0)
          ++num;
      }
      return string.Format("{{X: {0}, Y: {1}, Count: {2}}}", (object) this.x, (object) this.y, (object) num);
    }

    public static void Initialize()
    {
      int[] chestItemSpawn = Chest.chestItemSpawn;
      int[] chestTypeToIcon = Chest.chestTypeToIcon;
      chestTypeToIcon[0] = chestItemSpawn[0] = 48;
      chestTypeToIcon[1] = chestItemSpawn[1] = 306;
      chestTypeToIcon[2] = 327;
      chestItemSpawn[2] = 306;
      chestTypeToIcon[3] = chestItemSpawn[3] = 328;
      chestTypeToIcon[4] = 329;
      chestItemSpawn[4] = 328;
      chestTypeToIcon[5] = chestItemSpawn[5] = 343;
      chestTypeToIcon[6] = chestItemSpawn[6] = 348;
      chestTypeToIcon[7] = chestItemSpawn[7] = 625;
      chestTypeToIcon[8] = chestItemSpawn[8] = 626;
      chestTypeToIcon[9] = chestItemSpawn[9] = 627;
      chestTypeToIcon[10] = chestItemSpawn[10] = 680;
      chestTypeToIcon[11] = chestItemSpawn[11] = 681;
      chestTypeToIcon[12] = chestItemSpawn[12] = 831;
      chestTypeToIcon[13] = chestItemSpawn[13] = 838;
      chestTypeToIcon[14] = chestItemSpawn[14] = 914;
      chestTypeToIcon[15] = chestItemSpawn[15] = 952;
      chestTypeToIcon[16] = chestItemSpawn[16] = 1142;
      chestTypeToIcon[17] = chestItemSpawn[17] = 1298;
      chestTypeToIcon[18] = chestItemSpawn[18] = 1528;
      chestTypeToIcon[19] = chestItemSpawn[19] = 1529;
      chestTypeToIcon[20] = chestItemSpawn[20] = 1530;
      chestTypeToIcon[21] = chestItemSpawn[21] = 1531;
      chestTypeToIcon[22] = chestItemSpawn[22] = 1532;
      chestTypeToIcon[23] = 1533;
      chestItemSpawn[23] = 1528;
      chestTypeToIcon[24] = 1534;
      chestItemSpawn[24] = 1529;
      chestTypeToIcon[25] = 1535;
      chestItemSpawn[25] = 1530;
      chestTypeToIcon[26] = 1536;
      chestItemSpawn[26] = 1531;
      chestTypeToIcon[27] = 1537;
      chestItemSpawn[27] = 1532;
      chestTypeToIcon[28] = chestItemSpawn[28] = 2230;
      chestTypeToIcon[29] = chestItemSpawn[29] = 2249;
      chestTypeToIcon[30] = chestItemSpawn[30] = 2250;
      chestTypeToIcon[31] = chestItemSpawn[31] = 2526;
      chestTypeToIcon[32] = chestItemSpawn[32] = 2544;
      chestTypeToIcon[33] = chestItemSpawn[33] = 2559;
      chestTypeToIcon[34] = chestItemSpawn[34] = 2574;
      chestTypeToIcon[35] = chestItemSpawn[35] = 2612;
      chestTypeToIcon[36] = 327;
      chestItemSpawn[36] = 2612;
      chestTypeToIcon[37] = chestItemSpawn[37] = 2613;
      chestTypeToIcon[38] = 327;
      chestItemSpawn[38] = 2613;
      chestTypeToIcon[39] = chestItemSpawn[39] = 2614;
      chestTypeToIcon[40] = 327;
      chestItemSpawn[40] = 2614;
      chestTypeToIcon[41] = chestItemSpawn[41] = 2615;
      chestTypeToIcon[42] = chestItemSpawn[42] = 2616;
      chestTypeToIcon[43] = chestItemSpawn[43] = 2617;
      chestTypeToIcon[44] = chestItemSpawn[44] = 2618;
      chestTypeToIcon[45] = chestItemSpawn[45] = 2619;
      chestTypeToIcon[46] = chestItemSpawn[46] = 2620;
      chestTypeToIcon[47] = chestItemSpawn[47] = 2748;
      chestTypeToIcon[48] = chestItemSpawn[48] = 2814;
      chestTypeToIcon[49] = chestItemSpawn[49] = 3180;
      chestTypeToIcon[50] = chestItemSpawn[50] = 3125;
      chestTypeToIcon[51] = chestItemSpawn[51] = 3181;
      int[] chestItemSpawn2 = Chest.chestItemSpawn2;
      int[] chestTypeToIcon2 = Chest.chestTypeToIcon2;
      chestTypeToIcon2[0] = chestItemSpawn2[0] = 3884;
      chestTypeToIcon2[1] = chestItemSpawn2[1] = 3885;
      chestTypeToIcon2[2] = chestItemSpawn2[2] = 3939;
      chestTypeToIcon2[3] = chestItemSpawn2[3] = 3965;
      chestTypeToIcon2[4] = chestItemSpawn2[4] = 3988;
      chestTypeToIcon2[5] = chestItemSpawn2[5] = 4153;
      chestTypeToIcon2[6] = chestItemSpawn2[6] = 4174;
      chestTypeToIcon2[7] = chestItemSpawn2[7] = 4195;
      chestTypeToIcon2[8] = chestItemSpawn2[8] = 4216;
      chestTypeToIcon2[9] = chestItemSpawn2[9] = 4265;
      chestTypeToIcon2[10] = chestItemSpawn2[10] = 4267;
      chestTypeToIcon2[11] = chestItemSpawn2[11] = 4574;
      chestTypeToIcon2[12] = chestItemSpawn2[12] = 4712;
      chestTypeToIcon2[13] = 4714;
      chestItemSpawn2[13] = 4712;
      Chest.dresserTypeToIcon[0] = Chest.dresserItemSpawn[0] = 334;
      Chest.dresserTypeToIcon[1] = Chest.dresserItemSpawn[1] = 647;
      Chest.dresserTypeToIcon[2] = Chest.dresserItemSpawn[2] = 648;
      Chest.dresserTypeToIcon[3] = Chest.dresserItemSpawn[3] = 649;
      Chest.dresserTypeToIcon[4] = Chest.dresserItemSpawn[4] = 918;
      Chest.dresserTypeToIcon[5] = Chest.dresserItemSpawn[5] = 2386;
      Chest.dresserTypeToIcon[6] = Chest.dresserItemSpawn[6] = 2387;
      Chest.dresserTypeToIcon[7] = Chest.dresserItemSpawn[7] = 2388;
      Chest.dresserTypeToIcon[8] = Chest.dresserItemSpawn[8] = 2389;
      Chest.dresserTypeToIcon[9] = Chest.dresserItemSpawn[9] = 2390;
      Chest.dresserTypeToIcon[10] = Chest.dresserItemSpawn[10] = 2391;
      Chest.dresserTypeToIcon[11] = Chest.dresserItemSpawn[11] = 2392;
      Chest.dresserTypeToIcon[12] = Chest.dresserItemSpawn[12] = 2393;
      Chest.dresserTypeToIcon[13] = Chest.dresserItemSpawn[13] = 2394;
      Chest.dresserTypeToIcon[14] = Chest.dresserItemSpawn[14] = 2395;
      Chest.dresserTypeToIcon[15] = Chest.dresserItemSpawn[15] = 2396;
      Chest.dresserTypeToIcon[16] = Chest.dresserItemSpawn[16] = 2529;
      Chest.dresserTypeToIcon[17] = Chest.dresserItemSpawn[17] = 2545;
      Chest.dresserTypeToIcon[18] = Chest.dresserItemSpawn[18] = 2562;
      Chest.dresserTypeToIcon[19] = Chest.dresserItemSpawn[19] = 2577;
      Chest.dresserTypeToIcon[20] = Chest.dresserItemSpawn[20] = 2637;
      Chest.dresserTypeToIcon[21] = Chest.dresserItemSpawn[21] = 2638;
      Chest.dresserTypeToIcon[22] = Chest.dresserItemSpawn[22] = 2639;
      Chest.dresserTypeToIcon[23] = Chest.dresserItemSpawn[23] = 2640;
      Chest.dresserTypeToIcon[24] = Chest.dresserItemSpawn[24] = 2816;
      Chest.dresserTypeToIcon[25] = Chest.dresserItemSpawn[25] = 3132;
      Chest.dresserTypeToIcon[26] = Chest.dresserItemSpawn[26] = 3134;
      Chest.dresserTypeToIcon[27] = Chest.dresserItemSpawn[27] = 3133;
      Chest.dresserTypeToIcon[28] = Chest.dresserItemSpawn[28] = 3911;
      Chest.dresserTypeToIcon[29] = Chest.dresserItemSpawn[29] = 3912;
      Chest.dresserTypeToIcon[30] = Chest.dresserItemSpawn[30] = 3913;
      Chest.dresserTypeToIcon[31] = Chest.dresserItemSpawn[31] = 3914;
      Chest.dresserTypeToIcon[32] = Chest.dresserItemSpawn[32] = 3934;
      Chest.dresserTypeToIcon[33] = Chest.dresserItemSpawn[33] = 3968;
      Chest.dresserTypeToIcon[34] = Chest.dresserItemSpawn[34] = 4148;
      Chest.dresserTypeToIcon[35] = Chest.dresserItemSpawn[35] = 4169;
      Chest.dresserTypeToIcon[36] = Chest.dresserItemSpawn[36] = 4190;
      Chest.dresserTypeToIcon[37] = Chest.dresserItemSpawn[37] = 4211;
      Chest.dresserTypeToIcon[38] = Chest.dresserItemSpawn[38] = 4301;
      Chest.dresserTypeToIcon[39] = Chest.dresserItemSpawn[39] = 4569;
    }

    private static bool IsPlayerInChest(int i)
    {
      for (int index = 0; index < (int) byte.MaxValue; ++index)
      {
        if (Main.player[index].chest == i)
          return true;
      }
      return false;
    }

    public static List<int> GetCurrentlyOpenChests()
    {
      List<int> intList = new List<int>();
      for (int index = 0; index < (int) byte.MaxValue; ++index)
      {
        if (Main.player[index].chest > -1)
          intList.Add(Main.player[index].chest);
      }
      return intList;
    }

    public static bool IsLocked(int x, int y)
    {
      return Chest.IsLocked(Main.tile[x, y]);
    }

    public static bool IsLocked(Tile t)
    {
      if (t == null || t.type == (ushort) 21 && (t.frameX >= (short) 72 && t.frameX <= (short) 106 || t.frameX >= (short) 144 && t.frameX <= (short) 178 || (t.frameX >= (short) 828 && t.frameX <= (short) 1006 || t.frameX >= (short) 1296 && t.frameX <= (short) 1330) || (t.frameX >= (short) 1368 && t.frameX <= (short) 1402 || t.frameX >= (short) 1440 && t.frameX <= (short) 1474)))
        return true;
      return t.type == (ushort) 467 && (int) t.frameX / 36 == 13;
    }

    public static void ServerPlaceItem(int plr, int slot)
    {
      Main.player[plr].inventory[slot] = Chest.PutItemInNearbyChest(Main.player[plr].inventory[slot], Main.player[plr].Center);
      NetMessage.SendData(5, -1, -1, (NetworkText) null, plr, (float) slot, (float) Main.player[plr].inventory[slot].prefix, 0.0f, 0, 0, 0);
    }

    public static Item PutItemInNearbyChest(Item item, Vector2 position)
    {
      if (Main.netMode == 1)
        return item;
      for (int i = 0; i < 8000; ++i)
      {
        bool flag1 = false;
        bool flag2 = false;
        if (Main.chest[i] != null && !Chest.IsPlayerInChest(i) && !Chest.IsLocked(Main.chest[i].x, Main.chest[i].y) && (double) (new Vector2((float) (Main.chest[i].x * 16 + 16), (float) (Main.chest[i].y * 16 + 16)) - position).Length() < 250.0)
        {
          for (int index = 0; index < Main.chest[i].item.Length; ++index)
          {
            if (Main.chest[i].item[index].type > 0 && Main.chest[i].item[index].stack > 0)
            {
              if (item.IsTheSameAs(Main.chest[i].item[index]))
              {
                flag1 = true;
                int num = Main.chest[i].item[index].maxStack - Main.chest[i].item[index].stack;
                if (num > 0)
                {
                  if (num > item.stack)
                    num = item.stack;
                  item.stack -= num;
                  Main.chest[i].item[index].stack += num;
                  if (item.stack <= 0)
                  {
                    item.SetDefaults(0);
                    return item;
                  }
                }
              }
            }
            else
              flag2 = true;
          }
          if (flag1 & flag2 && item.stack > 0)
          {
            for (int index = 0; index < Main.chest[i].item.Length; ++index)
            {
              if (Main.chest[i].item[index].type == 0 || Main.chest[i].item[index].stack == 0)
              {
                Main.chest[i].item[index] = item.Clone();
                item.SetDefaults(0);
                return item;
              }
            }
          }
        }
      }
      return item;
    }

    public object Clone()
    {
      return this.MemberwiseClone();
    }

    public static bool Unlock(int X, int Y)
    {
      if (Main.tile[X, Y] == null || Main.tile[X + 1, Y] == null || (Main.tile[X, Y + 1] == null || Main.tile[X + 1, Y + 1] == null))
        return false;
      short num1 = 0;
      int Type = 0;
      Tile tileSafely1 = Framing.GetTileSafely(X, Y);
      int type = (int) tileSafely1.type;
      int num2 = (int) tileSafely1.frameX / 36;
      switch (type)
      {
        case 21:
          switch (num2)
          {
            case 2:
              num1 = (short) 36;
              Type = 11;
              AchievementsHelper.NotifyProgressionEvent(19);
              break;
            case 4:
              num1 = (short) 36;
              Type = 11;
              break;
            case 23:
            case 24:
            case 25:
            case 26:
            case 27:
              if (!NPC.downedPlantBoss)
                return false;
              num1 = (short) 180;
              Type = 11;
              AchievementsHelper.NotifyProgressionEvent(20);
              break;
            case 36:
            case 38:
            case 40:
              num1 = (short) 36;
              Type = 11;
              break;
            default:
              return false;
          }
          break;
        case 467:
          if (num2 != 13 || !NPC.downedPlantBoss)
            return false;
          num1 = (short) 36;
          Type = 11;
          AchievementsHelper.NotifyProgressionEvent(20);
          break;
      }
      SoundEngine.PlaySound(22, X * 16, Y * 16, 1, 1f, 0.0f);
      for (int i = X; i <= X + 1; ++i)
      {
        for (int j = Y; j <= Y + 1; ++j)
        {
          Tile tileSafely2 = Framing.GetTileSafely(i, j);
          if ((int) tileSafely2.type == type)
          {
            tileSafely2.frameX -= num1;
            Main.tile[i, j] = tileSafely2;
            for (int index = 0; index < 4; ++index)
              Dust.NewDust(new Vector2((float) (i * 16), (float) (j * 16)), 16, 16, Type, 0.0f, 0.0f, 0, new Color(), 1f);
          }
        }
      }
      return true;
    }

    public static int UsingChest(int i)
    {
      if (Main.chest[i] != null)
      {
        for (int index = 0; index < (int) byte.MaxValue; ++index)
        {
          if (Main.player[index].active && Main.player[index].chest == i)
            return index;
        }
      }
      return -1;
    }

    public static int FindChest(int X, int Y)
    {
      for (int index = 0; index < 8000; ++index)
      {
        if (Main.chest[index] != null && Main.chest[index].x == X && Main.chest[index].y == Y)
          return index;
      }
      return -1;
    }

    public static int FindChestByGuessing(int X, int Y)
    {
      for (int index = 0; index < 8000; ++index)
      {
        if (Main.chest[index] != null && Main.chest[index].x >= X && (Main.chest[index].x < X + 2 && Main.chest[index].y >= Y) && Main.chest[index].y < Y + 2)
          return index;
      }
      return -1;
    }

    public static int FindEmptyChest(
      int x,
      int y,
      int type = 21,
      int style = 0,
      int direction = 1,
      int alternate = 0)
    {
      int num = -1;
      for (int index = 0; index < 8000; ++index)
      {
        Chest chest = Main.chest[index];
        if (chest != null)
        {
          if (chest.x == x && chest.y == y)
            return -1;
        }
        else if (num == -1)
          num = index;
      }
      return num;
    }

    public static bool NearOtherChests(int x, int y)
    {
      for (int i = x - 25; i < x + 25; ++i)
      {
        for (int j = y - 8; j < y + 8; ++j)
        {
          Tile tileSafely = Framing.GetTileSafely(i, j);
          if (tileSafely.active() && TileID.Sets.BasicChest[(int) tileSafely.type])
            return true;
        }
      }
      return false;
    }

    public static int AfterPlacement_Hook(
      int x,
      int y,
      int type = 21,
      int style = 0,
      int direction = 1,
      int alternate = 0)
    {
      Point16 baseCoords = new Point16(x, y);
      TileObjectData.OriginToTopLeft(type, style, ref baseCoords);
      int emptyChest = Chest.FindEmptyChest((int) baseCoords.X, (int) baseCoords.Y, 21, 0, 1, 0);
      if (emptyChest == -1)
        return -1;
      if (Main.netMode != 1)
      {
        Chest chest = new Chest(false);
        chest.x = (int) baseCoords.X;
        chest.y = (int) baseCoords.Y;
        for (int index = 0; index < 40; ++index)
          chest.item[index] = new Item();
        Main.chest[emptyChest] = chest;
      }
      else
      {
        switch (type)
        {
          case 21:
            NetMessage.SendData(34, -1, -1, (NetworkText) null, 0, (float) x, (float) y, (float) style, 0, 0, 0);
            break;
          case 467:
            NetMessage.SendData(34, -1, -1, (NetworkText) null, 4, (float) x, (float) y, (float) style, 0, 0, 0);
            break;
          default:
            NetMessage.SendData(34, -1, -1, (NetworkText) null, 2, (float) x, (float) y, (float) style, 0, 0, 0);
            break;
        }
      }
      return emptyChest;
    }

    public static int CreateChest(int X, int Y, int id = -1)
    {
      int index1 = id;
      if (index1 == -1)
      {
        index1 = Chest.FindEmptyChest(X, Y, 21, 0, 1, 0);
        if (index1 == -1)
          return -1;
        if (Main.netMode == 1)
          return index1;
      }
      Main.chest[index1] = new Chest(false);
      Main.chest[index1].x = X;
      Main.chest[index1].y = Y;
      for (int index2 = 0; index2 < 40; ++index2)
        Main.chest[index1].item[index2] = new Item();
      return index1;
    }

    public static bool CanDestroyChest(int X, int Y)
    {
      for (int index1 = 0; index1 < 8000; ++index1)
      {
        Chest chest = Main.chest[index1];
        if (chest != null && chest.x == X && chest.y == Y)
        {
          for (int index2 = 0; index2 < 40; ++index2)
          {
            if (chest.item[index2] != null && chest.item[index2].type > 0 && chest.item[index2].stack > 0)
              return false;
          }
          return true;
        }
      }
      return true;
    }

    public static bool DestroyChest(int X, int Y)
    {
      for (int index1 = 0; index1 < 8000; ++index1)
      {
        Chest chest = Main.chest[index1];
        if (chest != null && chest.x == X && chest.y == Y)
        {
          for (int index2 = 0; index2 < 40; ++index2)
          {
            if (chest.item[index2] != null && chest.item[index2].type > 0 && chest.item[index2].stack > 0)
              return false;
          }
          Main.chest[index1] = (Chest) null;
          if (Main.player[Main.myPlayer].chest == index1)
            Main.player[Main.myPlayer].chest = -1;
          Recipe.FindRecipes(false);
          return true;
        }
      }
      return true;
    }

    public static void DestroyChestDirect(int X, int Y, int id)
    {
      if (id < 0)
        return;
      if (id >= Main.chest.Length)
        return;
      try
      {
        Chest chest = Main.chest[id];
        if (chest == null || chest.x != X || chest.y != Y)
          return;
        Main.chest[id] = (Chest) null;
        if (Main.player[Main.myPlayer].chest == id)
          Main.player[Main.myPlayer].chest = -1;
        Recipe.FindRecipes(false);
      }
      catch
      {
      }
    }

    public void AddItemToShop(Item newItem)
    {
      int num1 = Main.shopSellbackHelper.Remove(newItem);
      if (num1 >= newItem.stack)
        return;
      for (int index = 0; index < 39; ++index)
      {
        if (this.item[index] == null || this.item[index].type == 0)
        {
          this.item[index] = newItem.Clone();
          this.item[index].favorited = false;
          this.item[index].buyOnce = true;
          this.item[index].stack -= num1;
          int num2 = this.item[index].value;
          break;
        }
      }
    }

    public static void SetupTravelShop()
    {
      for (int index = 0; index < 40; ++index)
        Main.travelShop[index] = 0;
      Player player1 = (Player) null;
      for (int index = 0; index < (int) byte.MaxValue; ++index)
      {
        Player player2 = Main.player[index];
        if (player2.active && (player1 == null || (double) player1.luck < (double) player2.luck))
          player1 = player2;
      }
      if (player1 == null)
        player1 = new Player();
      int num1 = Main.rand.Next(4, 7);
      if (player1.RollLuck(4) == 0)
        ++num1;
      if (player1.RollLuck(8) == 0)
        ++num1;
      if (player1.RollLuck(16) == 0)
        ++num1;
      if (player1.RollLuck(32) == 0)
        ++num1;
      if (Main.expertMode && player1.RollLuck(2) == 0)
        ++num1;
      int index1 = 0;
      int num2 = 0;
      int[] numArray = new int[6]
      {
        100,
        200,
        300,
        400,
        500,
        600
      };
      while (num2 < num1)
      {
        int num3 = 0;
        if (player1.RollLuck(numArray[4]) == 0)
          num3 = 3309;
        if (player1.RollLuck(numArray[3]) == 0)
          num3 = 3314;
        if (player1.RollLuck(numArray[5]) == 0)
          num3 = 1987;
        if (player1.RollLuck(numArray[4]) == 0 && Main.hardMode)
          num3 = 2270;
        if (player1.RollLuck(numArray[4]) == 0 && Main.hardMode)
          num3 = 4760;
        if (player1.RollLuck(numArray[4]) == 0)
          num3 = 2278;
        if (player1.RollLuck(numArray[4]) == 0)
          num3 = 2271;
        if (player1.RollLuck(numArray[4]) == 0 && Main.hardMode && NPC.downedMechBossAny)
          num3 = 4060;
        if (player1.RollLuck(numArray[4]) == 0 && (NPC.downedBoss1 || NPC.downedBoss2 || (NPC.downedBoss3 || NPC.downedQueenBee) || Main.hardMode))
        {
          num3 = 4347;
          if (Main.hardMode)
            num3 = 4348;
        }
        if (player1.RollLuck(numArray[3]) == 0 && Main.hardMode && NPC.downedPlantBoss)
          num3 = 2223;
        if (player1.RollLuck(numArray[3]) == 0)
          num3 = 2272;
        if (player1.RollLuck(numArray[3]) == 0)
          num3 = 2219;
        if (player1.RollLuck(numArray[3]) == 0)
          num3 = 2276;
        if (player1.RollLuck(numArray[3]) == 0)
          num3 = 2284;
        if (player1.RollLuck(numArray[3]) == 0)
          num3 = 2285;
        if (player1.RollLuck(numArray[3]) == 0)
          num3 = 2286;
        if (player1.RollLuck(numArray[3]) == 0)
          num3 = 2287;
        if (player1.RollLuck(numArray[3]) == 0)
          num3 = 4744;
        if (player1.RollLuck(numArray[3]) == 0)
          num3 = 2296;
        if (player1.RollLuck(numArray[3]) == 0)
          num3 = 3628;
        if (player1.RollLuck(numArray[3]) == 0 && Main.hardMode)
          num3 = 4091;
        if (player1.RollLuck(numArray[3]) == 0)
          num3 = 4603;
        if (player1.RollLuck(numArray[3]) == 0)
          num3 = 4604;
        if (player1.RollLuck(numArray[3]) == 0)
          num3 = 4605;
        if (player1.RollLuck(numArray[3]) == 0)
          num3 = 4550;
        if (player1.RollLuck(numArray[2]) == 0 && WorldGen.shadowOrbSmashed)
          num3 = 2269;
        if (player1.RollLuck(numArray[2]) == 0)
          num3 = 2177;
        if (player1.RollLuck(numArray[2]) == 0)
          num3 = 1988;
        if (player1.RollLuck(numArray[2]) == 0)
          num3 = 2275;
        if (player1.RollLuck(numArray[2]) == 0)
          num3 = 2279;
        if (player1.RollLuck(numArray[2]) == 0)
          num3 = 2277;
        if (player1.RollLuck(numArray[2]) == 0)
          num3 = 4555;
        if (player1.RollLuck(numArray[2]) == 0)
          num3 = 4321;
        if (player1.RollLuck(numArray[2]) == 0)
          num3 = 4323;
        if (player1.RollLuck(numArray[2]) == 0)
          num3 = 4549;
        if (player1.RollLuck(numArray[2]) == 0)
          num3 = 4561;
        if (player1.RollLuck(numArray[2]) == 0)
          num3 = 4774;
        if (player1.RollLuck(numArray[2]) == 0)
          num3 = 4562;
        if (player1.RollLuck(numArray[2]) == 0)
          num3 = 4558;
        if (player1.RollLuck(numArray[2]) == 0)
          num3 = 4559;
        if (player1.RollLuck(numArray[2]) == 0)
          num3 = 4563;
        if (player1.RollLuck(numArray[2]) == 0)
          num3 = 4666;
        if (player1.RollLuck(numArray[2]) == 0 && NPC.downedBoss1)
          num3 = 3262;
        if (player1.RollLuck(numArray[2]) == 0 && NPC.downedMechBossAny)
          num3 = 3284;
        if (player1.RollLuck(numArray[2]) == 0 && Main.hardMode && NPC.downedMoonlord)
          num3 = 3596;
        if (player1.RollLuck(numArray[2]) == 0 && Main.hardMode && NPC.downedMartians)
          num3 = 2865;
        if (player1.RollLuck(numArray[2]) == 0 && Main.hardMode && NPC.downedMartians)
          num3 = 2866;
        if (player1.RollLuck(numArray[2]) == 0 && Main.hardMode && NPC.downedMartians)
          num3 = 2867;
        if (player1.RollLuck(numArray[2]) == 0 && Main.xMas)
          num3 = 3055;
        if (player1.RollLuck(numArray[2]) == 0 && Main.xMas)
          num3 = 3056;
        if (player1.RollLuck(numArray[2]) == 0 && Main.xMas)
          num3 = 3057;
        if (player1.RollLuck(numArray[2]) == 0 && Main.xMas)
          num3 = 3058;
        if (player1.RollLuck(numArray[2]) == 0 && Main.xMas)
          num3 = 3059;
        if (player1.RollLuck(numArray[1]) == 0)
          num3 = 2214;
        if (player1.RollLuck(numArray[1]) == 0)
          num3 = 2215;
        if (player1.RollLuck(numArray[1]) == 0)
          num3 = 2216;
        if (player1.RollLuck(numArray[1]) == 0)
          num3 = 2217;
        if (player1.RollLuck(numArray[1]) == 0)
          num3 = 3624;
        if (player1.RollLuck(numArray[1]) == 0)
          num3 = 2273;
        if (player1.RollLuck(numArray[1]) == 0)
          num3 = 2274;
        if (player1.RollLuck(numArray[0]) == 0)
          num3 = 2266;
        if (player1.RollLuck(numArray[0]) == 0)
          num3 = 2267;
        if (player1.RollLuck(numArray[0]) == 0)
          num3 = 2268;
        if (player1.RollLuck(numArray[0]) == 0)
          num3 = 2281 + Main.rand.Next(3);
        if (player1.RollLuck(numArray[0]) == 0)
          num3 = 2258;
        if (player1.RollLuck(numArray[0]) == 0)
          num3 = 2242;
        if (player1.RollLuck(numArray[0]) == 0)
          num3 = 2260;
        if (player1.RollLuck(numArray[0]) == 0)
          num3 = 3637;
        if (player1.RollLuck(numArray[0]) == 0)
          num3 = 4420;
        if (player1.RollLuck(numArray[0]) == 0)
          num3 = 3119;
        if (player1.RollLuck(numArray[0]) == 0)
          num3 = 3118;
        if (player1.RollLuck(numArray[0]) == 0)
          num3 = 3099;
        if (num3 != 0)
        {
          for (int index2 = 0; index2 < 40; ++index2)
          {
            if (Main.travelShop[index2] == num3)
            {
              num3 = 0;
              break;
            }
            if (num3 == 3637)
            {
              switch (Main.travelShop[index2])
              {
                case 3621:
                case 3622:
                case 3633:
                case 3634:
                case 3635:
                case 3636:
                case 3637:
                case 3638:
                case 3639:
                case 3640:
                case 3641:
                case 3642:
                  num3 = 0;
                  break;
              }
              if (num3 == 0)
                break;
            }
          }
        }
        if (num3 != 0)
        {
          ++num2;
          Main.travelShop[index1] = num3;
          ++index1;
          if (num3 == 2260)
          {
            Main.travelShop[index1] = 2261;
            int index2 = index1 + 1;
            Main.travelShop[index2] = 2262;
            index1 = index2 + 1;
          }
          if (num3 == 4555)
          {
            Main.travelShop[index1] = 4556;
            int index2 = index1 + 1;
            Main.travelShop[index2] = 4557;
            index1 = index2 + 1;
          }
          if (num3 == 4321)
          {
            Main.travelShop[index1] = 4322;
            ++index1;
          }
          if (num3 == 4323)
          {
            Main.travelShop[index1] = 4324;
            int index2 = index1 + 1;
            Main.travelShop[index2] = 4365;
            index1 = index2 + 1;
          }
          if (num3 == 4666)
          {
            Main.travelShop[index1] = 4664;
            int index2 = index1 + 1;
            Main.travelShop[index2] = 4665;
            index1 = index2 + 1;
          }
          if (num3 == 3637)
          {
            --index1;
            switch (Main.rand.Next(6))
            {
              case 0:
                int[] travelShop1 = Main.travelShop;
                int index2 = index1;
                int num4 = index2 + 1;
                travelShop1[index2] = 3637;
                int[] travelShop2 = Main.travelShop;
                int index3 = num4;
                index1 = index3 + 1;
                travelShop2[index3] = 3642;
                continue;
              case 1:
                int[] travelShop3 = Main.travelShop;
                int index4 = index1;
                int num5 = index4 + 1;
                travelShop3[index4] = 3621;
                int[] travelShop4 = Main.travelShop;
                int index5 = num5;
                index1 = index5 + 1;
                travelShop4[index5] = 3622;
                continue;
              case 2:
                int[] travelShop5 = Main.travelShop;
                int index6 = index1;
                int num6 = index6 + 1;
                travelShop5[index6] = 3634;
                int[] travelShop6 = Main.travelShop;
                int index7 = num6;
                index1 = index7 + 1;
                travelShop6[index7] = 3639;
                continue;
              case 3:
                int[] travelShop7 = Main.travelShop;
                int index8 = index1;
                int num7 = index8 + 1;
                travelShop7[index8] = 3633;
                int[] travelShop8 = Main.travelShop;
                int index9 = num7;
                index1 = index9 + 1;
                travelShop8[index9] = 3638;
                continue;
              case 4:
                int[] travelShop9 = Main.travelShop;
                int index10 = index1;
                int num8 = index10 + 1;
                travelShop9[index10] = 3635;
                int[] travelShop10 = Main.travelShop;
                int index11 = num8;
                index1 = index11 + 1;
                travelShop10[index11] = 3640;
                continue;
              case 5:
                int[] travelShop11 = Main.travelShop;
                int index12 = index1;
                int num9 = index12 + 1;
                travelShop11[index12] = 3636;
                int[] travelShop12 = Main.travelShop;
                int index13 = num9;
                index1 = index13 + 1;
                travelShop12[index13] = 3641;
                continue;
              default:
                continue;
            }
          }
        }
      }
    }

    public void SetupShop(int type)
    {
      bool flag1 = Main.LocalPlayer.currentShoppingSettings.PriceAdjustment <= 0.850000023841858;
      Item[] objArray1 = this.item;
      for (int index = 0; index < 40; ++index)
        objArray1[index] = new Item();
      int index1 = 0;
      switch (type)
      {
        case 1:
          objArray1[index1].SetDefaults(88);
          int index2 = index1 + 1;
          objArray1[index2].SetDefaults(87);
          int index3 = index2 + 1;
          objArray1[index3].SetDefaults(35);
          int index4 = index3 + 1;
          objArray1[index4].SetDefaults(1991);
          int index5 = index4 + 1;
          objArray1[index5].SetDefaults(3509);
          int index6 = index5 + 1;
          objArray1[index6].SetDefaults(3506);
          int index7 = index6 + 1;
          objArray1[index7].SetDefaults(8);
          int index8 = index7 + 1;
          objArray1[index8].SetDefaults(28);
          int index9 = index8 + 1;
          objArray1[index9].SetDefaults(110);
          int index10 = index9 + 1;
          objArray1[index10].SetDefaults(40);
          int index11 = index10 + 1;
          objArray1[index11].SetDefaults(42);
          int index12 = index11 + 1;
          objArray1[index12].SetDefaults(965);
          int index13 = index12 + 1;
          if (Main.player[Main.myPlayer].ZoneSnow)
          {
            objArray1[index13].SetDefaults(967);
            ++index13;
          }
          if (Main.player[Main.myPlayer].ZoneJungle)
          {
            objArray1[index13].SetDefaults(33);
            ++index13;
          }
          if (Main.dayTime && Main.IsItAHappyWindyDay)
            objArray1[index13++].SetDefaults(4074);
          if (Main.bloodMoon)
          {
            objArray1[index13].SetDefaults(279);
            ++index13;
          }
          if (!Main.dayTime)
          {
            objArray1[index13].SetDefaults(282);
            ++index13;
          }
          if (NPC.downedBoss3)
          {
            objArray1[index13].SetDefaults(346);
            ++index13;
          }
          if (Main.hardMode)
          {
            objArray1[index13].SetDefaults(488);
            ++index13;
          }
          for (int index14 = 0; index14 < 58; ++index14)
          {
            if (Main.player[Main.myPlayer].inventory[index14].type == 930)
            {
              objArray1[index13].SetDefaults(931);
              int index15 = index13 + 1;
              objArray1[index15].SetDefaults(1614);
              index13 = index15 + 1;
              break;
            }
          }
          objArray1[index13].SetDefaults(1786);
          index1 = index13 + 1;
          if (Main.hardMode)
          {
            objArray1[index1].SetDefaults(1348);
            ++index1;
          }
          if (NPC.downedBoss2 || NPC.downedBoss3 || Main.hardMode)
          {
            Item[] objArray2 = objArray1;
            int index14 = index1;
            int num = index14 + 1;
            objArray2[index14].SetDefaults(4063);
            Item[] objArray3 = objArray1;
            int index15 = num;
            index1 = index15 + 1;
            objArray3[index15].SetDefaults(4673);
          }
          if (Main.player[Main.myPlayer].HasItem(3107))
          {
            objArray1[index1].SetDefaults(3108);
            ++index1;
            break;
          }
          break;
        case 2:
          objArray1[index1].SetDefaults(97);
          int index16 = index1 + 1;
          if (Main.bloodMoon || Main.hardMode)
          {
            if (WorldGen.SavedOreTiers.Silver == 168)
            {
              objArray1[index16].SetDefaults(4915);
              ++index16;
            }
            else
            {
              objArray1[index16].SetDefaults(278);
              ++index16;
            }
          }
          if (NPC.downedBoss2 && !Main.dayTime || Main.hardMode)
          {
            objArray1[index16].SetDefaults(47);
            ++index16;
          }
          objArray1[index16].SetDefaults(95);
          int index17 = index16 + 1;
          objArray1[index17].SetDefaults(98);
          index1 = index17 + 1;
          if (Main.player[Main.myPlayer].ZoneGraveyard)
            objArray1[index1++].SetDefaults(4703);
          if (!Main.dayTime)
          {
            objArray1[index1].SetDefaults(324);
            ++index1;
          }
          if (Main.hardMode)
          {
            objArray1[index1].SetDefaults(534);
            ++index1;
          }
          if (Main.hardMode)
          {
            objArray1[index1].SetDefaults(1432);
            ++index1;
          }
          if (Main.player[Main.myPlayer].HasItem(1258))
          {
            objArray1[index1].SetDefaults(1261);
            ++index1;
          }
          if (Main.player[Main.myPlayer].HasItem(1835))
          {
            objArray1[index1].SetDefaults(1836);
            ++index1;
          }
          if (Main.player[Main.myPlayer].HasItem(3107))
          {
            objArray1[index1].SetDefaults(3108);
            ++index1;
          }
          if (Main.player[Main.myPlayer].HasItem(1782))
          {
            objArray1[index1].SetDefaults(1783);
            ++index1;
          }
          if (Main.player[Main.myPlayer].HasItem(1784))
          {
            objArray1[index1].SetDefaults(1785);
            ++index1;
          }
          if (Main.halloween)
          {
            objArray1[index1].SetDefaults(1736);
            int index14 = index1 + 1;
            objArray1[index14].SetDefaults(1737);
            int index15 = index14 + 1;
            objArray1[index15].SetDefaults(1738);
            index1 = index15 + 1;
            break;
          }
          break;
        case 3:
          int index18;
          if (Main.bloodMoon)
          {
            if (WorldGen.crimson)
            {
              objArray1[index1].SetDefaults(2886);
              int index14 = index1 + 1;
              objArray1[index14].SetDefaults(2171);
              int index15 = index14 + 1;
              objArray1[index15].SetDefaults(4508);
              index18 = index15 + 1;
            }
            else
            {
              objArray1[index1].SetDefaults(67);
              int index14 = index1 + 1;
              objArray1[index14].SetDefaults(59);
              int index15 = index14 + 1;
              objArray1[index15].SetDefaults(4504);
              index18 = index15 + 1;
            }
          }
          else
          {
            objArray1[index1].SetDefaults(66);
            int index14 = index1 + 1;
            objArray1[index14].SetDefaults(62);
            int index15 = index14 + 1;
            objArray1[index15].SetDefaults(63);
            int index19 = index15 + 1;
            objArray1[index19].SetDefaults(745);
            index18 = index19 + 1;
          }
          if (Main.hardMode && Main.player[Main.myPlayer].ZoneGraveyard)
          {
            if (WorldGen.crimson)
              objArray1[index18].SetDefaults(59);
            else
              objArray1[index18].SetDefaults(2171);
            ++index18;
          }
          objArray1[index18].SetDefaults(27);
          int index20 = index18 + 1;
          objArray1[index20].SetDefaults(114);
          int index21 = index20 + 1;
          objArray1[index21].SetDefaults(1828);
          int index22 = index21 + 1;
          objArray1[index22].SetDefaults(747);
          int index23 = index22 + 1;
          if (Main.hardMode)
          {
            objArray1[index23].SetDefaults(746);
            ++index23;
          }
          if (Main.hardMode)
          {
            objArray1[index23].SetDefaults(369);
            ++index23;
          }
          if (Main.hardMode)
          {
            objArray1[index23].SetDefaults(4505);
            ++index23;
          }
          if (Main.player[Main.myPlayer].ZoneGlowshroom)
          {
            objArray1[index23].SetDefaults(194);
            ++index23;
          }
          if (Main.halloween)
          {
            objArray1[index23].SetDefaults(1853);
            int index14 = index23 + 1;
            objArray1[index14].SetDefaults(1854);
            index23 = index14 + 1;
          }
          if (NPC.downedSlimeKing)
          {
            objArray1[index23].SetDefaults(3215);
            ++index23;
          }
          if (NPC.downedQueenBee)
          {
            objArray1[index23].SetDefaults(3216);
            ++index23;
          }
          if (NPC.downedBoss1)
          {
            objArray1[index23].SetDefaults(3219);
            ++index23;
          }
          if (NPC.downedBoss2)
          {
            if (WorldGen.crimson)
            {
              objArray1[index23].SetDefaults(3218);
              ++index23;
            }
            else
            {
              objArray1[index23].SetDefaults(3217);
              ++index23;
            }
          }
          if (NPC.downedBoss3)
          {
            objArray1[index23].SetDefaults(3220);
            int index14 = index23 + 1;
            objArray1[index14].SetDefaults(3221);
            index23 = index14 + 1;
          }
          if (Main.hardMode)
          {
            objArray1[index23].SetDefaults(3222);
            ++index23;
          }
          Item[] objArray4 = objArray1;
          int index24 = index23;
          int num1 = index24 + 1;
          objArray4[index24].SetDefaults(4047);
          Item[] objArray5 = objArray1;
          int index25 = num1;
          int num2 = index25 + 1;
          objArray5[index25].SetDefaults(4045);
          Item[] objArray6 = objArray1;
          int index26 = num2;
          int num3 = index26 + 1;
          objArray6[index26].SetDefaults(4044);
          Item[] objArray7 = objArray1;
          int index27 = num3;
          int num4 = index27 + 1;
          objArray7[index27].SetDefaults(4043);
          Item[] objArray8 = objArray1;
          int index28 = num4;
          int num5 = index28 + 1;
          objArray8[index28].SetDefaults(4042);
          Item[] objArray9 = objArray1;
          int index29 = num5;
          int num6 = index29 + 1;
          objArray9[index29].SetDefaults(4046);
          Item[] objArray10 = objArray1;
          int index30 = num6;
          int num7 = index30 + 1;
          objArray10[index30].SetDefaults(4041);
          Item[] objArray11 = objArray1;
          int index31 = num7;
          int num8 = index31 + 1;
          objArray11[index31].SetDefaults(4241);
          Item[] objArray12 = objArray1;
          int index32 = num8;
          index1 = index32 + 1;
          objArray12[index32].SetDefaults(4048);
          if (Main.hardMode)
          {
            switch (Main.moonPhase / 2)
            {
              case 0:
                Item[] objArray13 = objArray1;
                int index33 = index1;
                int num9 = index33 + 1;
                objArray13[index33].SetDefaults(4430);
                Item[] objArray14 = objArray1;
                int index34 = num9;
                int num10 = index34 + 1;
                objArray14[index34].SetDefaults(4431);
                Item[] objArray15 = objArray1;
                int index35 = num10;
                index1 = index35 + 1;
                objArray15[index35].SetDefaults(4432);
                break;
              case 1:
                Item[] objArray16 = objArray1;
                int index36 = index1;
                int num11 = index36 + 1;
                objArray16[index36].SetDefaults(4433);
                Item[] objArray17 = objArray1;
                int index37 = num11;
                int num12 = index37 + 1;
                objArray17[index37].SetDefaults(4434);
                Item[] objArray18 = objArray1;
                int index38 = num12;
                index1 = index38 + 1;
                objArray18[index38].SetDefaults(4435);
                break;
              case 2:
                Item[] objArray19 = objArray1;
                int index39 = index1;
                int num13 = index39 + 1;
                objArray19[index39].SetDefaults(4436);
                Item[] objArray20 = objArray1;
                int index40 = num13;
                int num14 = index40 + 1;
                objArray20[index40].SetDefaults(4437);
                Item[] objArray21 = objArray1;
                int index41 = num14;
                index1 = index41 + 1;
                objArray21[index41].SetDefaults(4438);
                break;
              default:
                Item[] objArray22 = objArray1;
                int index42 = index1;
                int num15 = index42 + 1;
                objArray22[index42].SetDefaults(4439);
                Item[] objArray23 = objArray1;
                int index43 = num15;
                int num16 = index43 + 1;
                objArray23[index43].SetDefaults(4440);
                Item[] objArray24 = objArray1;
                int index44 = num16;
                index1 = index44 + 1;
                objArray24[index44].SetDefaults(4441);
                break;
            }
          }
          else
            break;
          break;
        case 4:
          objArray1[index1].SetDefaults(168);
          int index45 = index1 + 1;
          objArray1[index45].SetDefaults(166);
          int index46 = index45 + 1;
          objArray1[index46].SetDefaults(167);
          index1 = index46 + 1;
          if (Main.hardMode)
          {
            objArray1[index1].SetDefaults(265);
            ++index1;
          }
          if (Main.hardMode && NPC.downedPlantBoss && NPC.downedPirates)
          {
            objArray1[index1].SetDefaults(937);
            ++index1;
          }
          if (Main.hardMode)
          {
            objArray1[index1].SetDefaults(1347);
            ++index1;
          }
          for (int index14 = 0; index14 < 58; ++index14)
          {
            if (Main.player[Main.myPlayer].inventory[index14].type == 4827)
            {
              objArray1[index1].SetDefaults(4827);
              ++index1;
              break;
            }
          }
          for (int index14 = 0; index14 < 58; ++index14)
          {
            if (Main.player[Main.myPlayer].inventory[index14].type == 4824)
            {
              objArray1[index1].SetDefaults(4824);
              ++index1;
              break;
            }
          }
          for (int index14 = 0; index14 < 58; ++index14)
          {
            if (Main.player[Main.myPlayer].inventory[index14].type == 4825)
            {
              objArray1[index1].SetDefaults(4825);
              ++index1;
              break;
            }
          }
          for (int index14 = 0; index14 < 58; ++index14)
          {
            if (Main.player[Main.myPlayer].inventory[index14].type == 4826)
            {
              objArray1[index1].SetDefaults(4826);
              ++index1;
              break;
            }
          }
          break;
        case 5:
          objArray1[index1].SetDefaults(254);
          int index47 = index1 + 1;
          objArray1[index47].SetDefaults(981);
          int index48 = index47 + 1;
          if (Main.dayTime)
          {
            objArray1[index48].SetDefaults(242);
            ++index48;
          }
          switch (Main.moonPhase)
          {
            case 0:
              objArray1[index48].SetDefaults(245);
              int index49 = index48 + 1;
              objArray1[index49].SetDefaults(246);
              index48 = index49 + 1;
              if (!Main.dayTime)
              {
                Item[] objArray2 = objArray1;
                int index14 = index48;
                int num17 = index14 + 1;
                objArray2[index14].SetDefaults(1288);
                Item[] objArray3 = objArray1;
                int index15 = num17;
                index48 = index15 + 1;
                objArray3[index15].SetDefaults(1289);
                break;
              }
              break;
            case 1:
              objArray1[index48].SetDefaults(325);
              int index50 = index48 + 1;
              objArray1[index50].SetDefaults(326);
              index48 = index50 + 1;
              break;
          }
          objArray1[index48].SetDefaults(269);
          int index51 = index48 + 1;
          objArray1[index51].SetDefaults(270);
          int index52 = index51 + 1;
          objArray1[index52].SetDefaults(271);
          index1 = index52 + 1;
          if (NPC.downedClown)
          {
            objArray1[index1].SetDefaults(503);
            int index14 = index1 + 1;
            objArray1[index14].SetDefaults(504);
            int index15 = index14 + 1;
            objArray1[index15].SetDefaults(505);
            index1 = index15 + 1;
          }
          if (Main.bloodMoon)
          {
            objArray1[index1].SetDefaults(322);
            ++index1;
            if (!Main.dayTime)
            {
              Item[] objArray2 = objArray1;
              int index14 = index1;
              int num17 = index14 + 1;
              objArray2[index14].SetDefaults(3362);
              Item[] objArray3 = objArray1;
              int index15 = num17;
              index1 = index15 + 1;
              objArray3[index15].SetDefaults(3363);
            }
          }
          if (NPC.downedAncientCultist)
          {
            if (Main.dayTime)
            {
              Item[] objArray2 = objArray1;
              int index14 = index1;
              int num17 = index14 + 1;
              objArray2[index14].SetDefaults(2856);
              Item[] objArray3 = objArray1;
              int index15 = num17;
              index1 = index15 + 1;
              objArray3[index15].SetDefaults(2858);
            }
            else
            {
              Item[] objArray2 = objArray1;
              int index14 = index1;
              int num17 = index14 + 1;
              objArray2[index14].SetDefaults(2857);
              Item[] objArray3 = objArray1;
              int index15 = num17;
              index1 = index15 + 1;
              objArray3[index15].SetDefaults(2859);
            }
          }
          if (NPC.AnyNPCs(441))
          {
            Item[] objArray2 = objArray1;
            int index14 = index1;
            int num17 = index14 + 1;
            objArray2[index14].SetDefaults(3242);
            Item[] objArray3 = objArray1;
            int index15 = num17;
            int num18 = index15 + 1;
            objArray3[index15].SetDefaults(3243);
            Item[] objArray25 = objArray1;
            int index19 = num18;
            index1 = index19 + 1;
            objArray25[index19].SetDefaults(3244);
          }
          if (Main.player[Main.myPlayer].ZoneGraveyard)
          {
            Item[] objArray2 = objArray1;
            int index14 = index1;
            int num17 = index14 + 1;
            objArray2[index14].SetDefaults(4685);
            Item[] objArray3 = objArray1;
            int index15 = num17;
            int num18 = index15 + 1;
            objArray3[index15].SetDefaults(4686);
            Item[] objArray25 = objArray1;
            int index19 = num18;
            int num19 = index19 + 1;
            objArray25[index19].SetDefaults(4704);
            Item[] objArray26 = objArray1;
            int index53 = num19;
            int num20 = index53 + 1;
            objArray26[index53].SetDefaults(4705);
            Item[] objArray27 = objArray1;
            int index54 = num20;
            int num21 = index54 + 1;
            objArray27[index54].SetDefaults(4706);
            Item[] objArray28 = objArray1;
            int index55 = num21;
            int num22 = index55 + 1;
            objArray28[index55].SetDefaults(4707);
            Item[] objArray29 = objArray1;
            int index56 = num22;
            int num23 = index56 + 1;
            objArray29[index56].SetDefaults(4708);
            Item[] objArray30 = objArray1;
            int index57 = num23;
            index1 = index57 + 1;
            objArray30[index57].SetDefaults(4709);
          }
          if (Main.player[Main.myPlayer].ZoneSnow)
          {
            objArray1[index1].SetDefaults(1429);
            ++index1;
          }
          if (Main.halloween)
          {
            objArray1[index1].SetDefaults(1740);
            ++index1;
          }
          if (Main.hardMode)
          {
            if (Main.moonPhase == 2)
            {
              objArray1[index1].SetDefaults(869);
              ++index1;
            }
            if (Main.moonPhase == 3)
            {
              objArray1[index1].SetDefaults(4994);
              int index14 = index1 + 1;
              objArray1[index14].SetDefaults(4997);
              index1 = index14 + 1;
            }
            if (Main.moonPhase == 4)
            {
              objArray1[index1].SetDefaults(864);
              int index14 = index1 + 1;
              objArray1[index14].SetDefaults(865);
              index1 = index14 + 1;
            }
            if (Main.moonPhase == 5)
            {
              objArray1[index1].SetDefaults(4995);
              int index14 = index1 + 1;
              objArray1[index14].SetDefaults(4998);
              index1 = index14 + 1;
            }
            if (Main.moonPhase == 6)
            {
              objArray1[index1].SetDefaults(873);
              int index14 = index1 + 1;
              objArray1[index14].SetDefaults(874);
              int index15 = index14 + 1;
              objArray1[index15].SetDefaults(875);
              index1 = index15 + 1;
            }
            if (Main.moonPhase == 7)
            {
              objArray1[index1].SetDefaults(4996);
              int index14 = index1 + 1;
              objArray1[index14].SetDefaults(4999);
              index1 = index14 + 1;
            }
          }
          if (NPC.downedFrost)
          {
            objArray1[index1].SetDefaults(1275);
            int index14 = index1 + 1;
            objArray1[index14].SetDefaults(1276);
            index1 = index14 + 1;
          }
          if (Main.halloween)
          {
            Item[] objArray2 = objArray1;
            int index14 = index1;
            int num17 = index14 + 1;
            objArray2[index14].SetDefaults(3246);
            Item[] objArray3 = objArray1;
            int index15 = num17;
            index1 = index15 + 1;
            objArray3[index15].SetDefaults(3247);
          }
          if (BirthdayParty.PartyIsUp)
          {
            Item[] objArray2 = objArray1;
            int index14 = index1;
            int num17 = index14 + 1;
            objArray2[index14].SetDefaults(3730);
            Item[] objArray3 = objArray1;
            int index15 = num17;
            int num18 = index15 + 1;
            objArray3[index15].SetDefaults(3731);
            Item[] objArray25 = objArray1;
            int index19 = num18;
            int num19 = index19 + 1;
            objArray25[index19].SetDefaults(3733);
            Item[] objArray26 = objArray1;
            int index53 = num19;
            int num20 = index53 + 1;
            objArray26[index53].SetDefaults(3734);
            Item[] objArray27 = objArray1;
            int index54 = num20;
            index1 = index54 + 1;
            objArray27[index54].SetDefaults(3735);
          }
          int scoreAccumulated1 = Main.LocalPlayer.golferScoreAccumulated;
          if (index1 < 38 && scoreAccumulated1 >= 2000)
          {
            objArray1[index1].SetDefaults(4744);
            ++index1;
            break;
          }
          break;
        case 6:
          objArray1[index1].SetDefaults(128);
          int index58 = index1 + 1;
          objArray1[index58].SetDefaults(486);
          int index59 = index58 + 1;
          objArray1[index59].SetDefaults(398);
          int index60 = index59 + 1;
          objArray1[index60].SetDefaults(84);
          int index61 = index60 + 1;
          objArray1[index61].SetDefaults(407);
          int index62 = index61 + 1;
          objArray1[index62].SetDefaults(161);
          index1 = index62 + 1;
          break;
        case 7:
          objArray1[index1].SetDefaults(487);
          int index63 = index1 + 1;
          objArray1[index63].SetDefaults(496);
          int index64 = index63 + 1;
          objArray1[index64].SetDefaults(500);
          int index65 = index64 + 1;
          objArray1[index65].SetDefaults(507);
          int index66 = index65 + 1;
          objArray1[index66].SetDefaults(508);
          int index67 = index66 + 1;
          objArray1[index67].SetDefaults(531);
          int index68 = index67 + 1;
          objArray1[index68].SetDefaults(576);
          int index69 = index68 + 1;
          objArray1[index69].SetDefaults(3186);
          index1 = index69 + 1;
          if (Main.halloween)
          {
            objArray1[index1].SetDefaults(1739);
            ++index1;
            break;
          }
          break;
        case 8:
          objArray1[index1].SetDefaults(509);
          int index70 = index1 + 1;
          objArray1[index70].SetDefaults(850);
          int index71 = index70 + 1;
          objArray1[index71].SetDefaults(851);
          int index72 = index71 + 1;
          objArray1[index72].SetDefaults(3612);
          int index73 = index72 + 1;
          objArray1[index73].SetDefaults(510);
          int index74 = index73 + 1;
          objArray1[index74].SetDefaults(530);
          int index75 = index74 + 1;
          objArray1[index75].SetDefaults(513);
          int index76 = index75 + 1;
          objArray1[index76].SetDefaults(538);
          int index77 = index76 + 1;
          objArray1[index77].SetDefaults(529);
          int index78 = index77 + 1;
          objArray1[index78].SetDefaults(541);
          int index79 = index78 + 1;
          objArray1[index79].SetDefaults(542);
          int index80 = index79 + 1;
          objArray1[index80].SetDefaults(543);
          int index81 = index80 + 1;
          objArray1[index81].SetDefaults(852);
          int index82 = index81 + 1;
          objArray1[index82].SetDefaults(853);
          int num24 = index82 + 1;
          Item[] objArray31 = objArray1;
          int index83 = num24;
          int num25 = index83 + 1;
          objArray31[index83].SetDefaults(4261);
          Item[] objArray32 = objArray1;
          int index84 = num25;
          int index85 = index84 + 1;
          objArray32[index84].SetDefaults(3707);
          objArray1[index85].SetDefaults(2739);
          int index86 = index85 + 1;
          objArray1[index86].SetDefaults(849);
          int num26 = index86 + 1;
          Item[] objArray33 = objArray1;
          int index87 = num26;
          int num27 = index87 + 1;
          objArray33[index87].SetDefaults(3616);
          Item[] objArray34 = objArray1;
          int index88 = num27;
          int num28 = index88 + 1;
          objArray34[index88].SetDefaults(2799);
          Item[] objArray35 = objArray1;
          int index89 = num28;
          int num29 = index89 + 1;
          objArray35[index89].SetDefaults(3619);
          Item[] objArray36 = objArray1;
          int index90 = num29;
          int num30 = index90 + 1;
          objArray36[index90].SetDefaults(3627);
          Item[] objArray37 = objArray1;
          int index91 = num30;
          int num31 = index91 + 1;
          objArray37[index91].SetDefaults(3629);
          Item[] objArray38 = objArray1;
          int index92 = num31;
          int num32 = index92 + 1;
          objArray38[index92].SetDefaults(4484);
          Item[] objArray39 = objArray1;
          int index93 = num32;
          index1 = index93 + 1;
          objArray39[index93].SetDefaults(4485);
          if (NPC.AnyNPCs(369) && Main.hardMode && Main.moonPhase == 3)
          {
            objArray1[index1].SetDefaults(2295);
            ++index1;
            break;
          }
          break;
        case 9:
          objArray1[index1].SetDefaults(588);
          int index94 = index1 + 1;
          objArray1[index94].SetDefaults(589);
          int index95 = index94 + 1;
          objArray1[index95].SetDefaults(590);
          int index96 = index95 + 1;
          objArray1[index96].SetDefaults(597);
          int index97 = index96 + 1;
          objArray1[index97].SetDefaults(598);
          int index98 = index97 + 1;
          objArray1[index98].SetDefaults(596);
          index1 = index98 + 1;
          for (int Type = 1873; Type < 1906; ++Type)
          {
            objArray1[index1].SetDefaults(Type);
            ++index1;
          }
          break;
        case 10:
          if (NPC.downedMechBossAny)
          {
            objArray1[index1].SetDefaults(756);
            int index14 = index1 + 1;
            objArray1[index14].SetDefaults(787);
            index1 = index14 + 1;
          }
          objArray1[index1].SetDefaults(868);
          int index99 = index1 + 1;
          if (NPC.downedPlantBoss)
          {
            objArray1[index99].SetDefaults(1551);
            ++index99;
          }
          objArray1[index99].SetDefaults(1181);
          int index100 = index99 + 1;
          objArray1[index100].SetDefaults(783);
          index1 = index100 + 1;
          break;
        case 11:
          objArray1[index1].SetDefaults(779);
          int index101 = index1 + 1;
          int index102;
          if (Main.moonPhase >= 4)
          {
            objArray1[index101].SetDefaults(748);
            index102 = index101 + 1;
          }
          else
          {
            objArray1[index101].SetDefaults(839);
            int index14 = index101 + 1;
            objArray1[index14].SetDefaults(840);
            int index15 = index14 + 1;
            objArray1[index15].SetDefaults(841);
            index102 = index15 + 1;
          }
          if (NPC.downedGolemBoss)
          {
            objArray1[index102].SetDefaults(948);
            ++index102;
          }
          Item[] objArray40 = objArray1;
          int index103 = index102;
          int num33 = index103 + 1;
          objArray40[index103].SetDefaults(3623);
          Item[] objArray41 = objArray1;
          int index104 = num33;
          int num34 = index104 + 1;
          objArray41[index104].SetDefaults(3603);
          Item[] objArray42 = objArray1;
          int index105 = num34;
          int num35 = index105 + 1;
          objArray42[index105].SetDefaults(3604);
          Item[] objArray43 = objArray1;
          int index106 = num35;
          int num36 = index106 + 1;
          objArray43[index106].SetDefaults(3607);
          Item[] objArray44 = objArray1;
          int index107 = num36;
          int num37 = index107 + 1;
          objArray44[index107].SetDefaults(3605);
          Item[] objArray45 = objArray1;
          int index108 = num37;
          int num38 = index108 + 1;
          objArray45[index108].SetDefaults(3606);
          Item[] objArray46 = objArray1;
          int index109 = num38;
          int num39 = index109 + 1;
          objArray46[index109].SetDefaults(3608);
          Item[] objArray47 = objArray1;
          int index110 = num39;
          int num40 = index110 + 1;
          objArray47[index110].SetDefaults(3618);
          Item[] objArray48 = objArray1;
          int index111 = num40;
          int num41 = index111 + 1;
          objArray48[index111].SetDefaults(3602);
          Item[] objArray49 = objArray1;
          int index112 = num41;
          int num42 = index112 + 1;
          objArray49[index112].SetDefaults(3663);
          Item[] objArray50 = objArray1;
          int index113 = num42;
          int num43 = index113 + 1;
          objArray50[index113].SetDefaults(3609);
          Item[] objArray51 = objArray1;
          int index114 = num43;
          int index115 = index114 + 1;
          objArray51[index114].SetDefaults(3610);
          objArray1[index115].SetDefaults(995);
          int index116 = index115 + 1;
          if (NPC.downedBoss1 && NPC.downedBoss2 && NPC.downedBoss3)
          {
            objArray1[index116].SetDefaults(2203);
            ++index116;
          }
          if (WorldGen.crimson)
          {
            objArray1[index116].SetDefaults(2193);
            ++index116;
          }
          if (!WorldGen.crimson)
          {
            objArray1[index116].SetDefaults(4142);
            ++index116;
          }
          objArray1[index116].SetDefaults(1263);
          int index117 = index116 + 1;
          if (Main.eclipse || Main.bloodMoon)
          {
            if (WorldGen.crimson)
            {
              objArray1[index117].SetDefaults(784);
              index1 = index117 + 1;
            }
            else
            {
              objArray1[index117].SetDefaults(782);
              index1 = index117 + 1;
            }
          }
          else if (Main.player[Main.myPlayer].ZoneHallow)
          {
            objArray1[index117].SetDefaults(781);
            index1 = index117 + 1;
          }
          else
          {
            objArray1[index117].SetDefaults(780);
            index1 = index117 + 1;
          }
          if (Main.hardMode)
          {
            objArray1[index1].SetDefaults(1344);
            int index14 = index1 + 1;
            objArray1[index14].SetDefaults(4472);
            index1 = index14 + 1;
          }
          if (Main.halloween)
          {
            objArray1[index1].SetDefaults(1742);
            ++index1;
            break;
          }
          break;
        case 12:
          objArray1[index1].SetDefaults(1037);
          int index118 = index1 + 1;
          objArray1[index118].SetDefaults(2874);
          int index119 = index118 + 1;
          objArray1[index119].SetDefaults(1120);
          index1 = index119 + 1;
          if (Main.netMode == 1)
          {
            objArray1[index1].SetDefaults(1969);
            ++index1;
          }
          if (Main.halloween)
          {
            objArray1[index1].SetDefaults(3248);
            int index14 = index1 + 1;
            objArray1[index14].SetDefaults(1741);
            index1 = index14 + 1;
          }
          if (Main.moonPhase == 0)
          {
            objArray1[index1].SetDefaults(2871);
            int index14 = index1 + 1;
            objArray1[index14].SetDefaults(2872);
            index1 = index14 + 1;
          }
          if (!Main.dayTime && Main.bloodMoon)
          {
            objArray1[index1].SetDefaults(4663);
            ++index1;
          }
          if (Main.player[Main.myPlayer].ZoneGraveyard)
          {
            objArray1[index1].SetDefaults(4662);
            ++index1;
            break;
          }
          break;
        case 13:
          objArray1[index1].SetDefaults(859);
          int index120 = index1 + 1;
          if (Main.LocalPlayer.golferScoreAccumulated > 500)
            objArray1[index120++].SetDefaults(4743);
          objArray1[index120].SetDefaults(1000);
          int index121 = index120 + 1;
          objArray1[index121].SetDefaults(1168);
          int index122 = index121 + 1;
          int index123;
          if (Main.dayTime)
          {
            objArray1[index122].SetDefaults(1449);
            index123 = index122 + 1;
          }
          else
          {
            objArray1[index122].SetDefaults(4552);
            index123 = index122 + 1;
          }
          objArray1[index123].SetDefaults(1345);
          int index124 = index123 + 1;
          objArray1[index124].SetDefaults(1450);
          int num44 = index124 + 1;
          Item[] objArray52 = objArray1;
          int index125 = num44;
          int num45 = index125 + 1;
          objArray52[index125].SetDefaults(3253);
          Item[] objArray53 = objArray1;
          int index126 = num45;
          int num46 = index126 + 1;
          objArray53[index126].SetDefaults(4553);
          Item[] objArray54 = objArray1;
          int index127 = num46;
          int num47 = index127 + 1;
          objArray54[index127].SetDefaults(2700);
          Item[] objArray55 = objArray1;
          int index128 = num47;
          int num48 = index128 + 1;
          objArray55[index128].SetDefaults(2738);
          Item[] objArray56 = objArray1;
          int index129 = num48;
          int num49 = index129 + 1;
          objArray56[index129].SetDefaults(4470);
          Item[] objArray57 = objArray1;
          int index130 = num49;
          int index131 = index130 + 1;
          objArray57[index130].SetDefaults(4681);
          if (Main.player[Main.myPlayer].ZoneGraveyard)
            objArray1[index131++].SetDefaults(4682);
          if (LanternNight.LanternsUp)
            objArray1[index131++].SetDefaults(4702);
          if (Main.player[Main.myPlayer].HasItem(3548))
          {
            objArray1[index131].SetDefaults(3548);
            ++index131;
          }
          if (NPC.AnyNPCs(229))
            objArray1[index131++].SetDefaults(3369);
          if (NPC.downedGolemBoss)
            objArray1[index131++].SetDefaults(3546);
          if (Main.hardMode)
          {
            objArray1[index131].SetDefaults(3214);
            int index14 = index131 + 1;
            objArray1[index14].SetDefaults(2868);
            int index15 = index14 + 1;
            objArray1[index15].SetDefaults(970);
            int index19 = index15 + 1;
            objArray1[index19].SetDefaults(971);
            int index53 = index19 + 1;
            objArray1[index53].SetDefaults(972);
            int index54 = index53 + 1;
            objArray1[index54].SetDefaults(973);
            index131 = index54 + 1;
          }
          Item[] objArray58 = objArray1;
          int index132 = index131;
          int num50 = index132 + 1;
          objArray58[index132].SetDefaults(4791);
          Item[] objArray59 = objArray1;
          int index133 = num50;
          int num51 = index133 + 1;
          objArray59[index133].SetDefaults(3747);
          Item[] objArray60 = objArray1;
          int index134 = num51;
          int num52 = index134 + 1;
          objArray60[index134].SetDefaults(3732);
          Item[] objArray61 = objArray1;
          int index135 = num52;
          index1 = index135 + 1;
          objArray61[index135].SetDefaults(3742);
          if (BirthdayParty.PartyIsUp)
          {
            Item[] objArray2 = objArray1;
            int index14 = index1;
            int num17 = index14 + 1;
            objArray2[index14].SetDefaults(3749);
            Item[] objArray3 = objArray1;
            int index15 = num17;
            int num18 = index15 + 1;
            objArray3[index15].SetDefaults(3746);
            Item[] objArray25 = objArray1;
            int index19 = num18;
            int num19 = index19 + 1;
            objArray25[index19].SetDefaults(3739);
            Item[] objArray26 = objArray1;
            int index53 = num19;
            int num20 = index53 + 1;
            objArray26[index53].SetDefaults(3740);
            Item[] objArray27 = objArray1;
            int index54 = num20;
            int num21 = index54 + 1;
            objArray27[index54].SetDefaults(3741);
            Item[] objArray28 = objArray1;
            int index55 = num21;
            int num22 = index55 + 1;
            objArray28[index55].SetDefaults(3737);
            Item[] objArray29 = objArray1;
            int index56 = num22;
            int num23 = index56 + 1;
            objArray29[index56].SetDefaults(3738);
            Item[] objArray30 = objArray1;
            int index57 = num23;
            int num53 = index57 + 1;
            objArray30[index57].SetDefaults(3736);
            Item[] objArray62 = objArray1;
            int index136 = num53;
            int num54 = index136 + 1;
            objArray62[index136].SetDefaults(3745);
            Item[] objArray63 = objArray1;
            int index137 = num54;
            int num55 = index137 + 1;
            objArray63[index137].SetDefaults(3744);
            Item[] objArray64 = objArray1;
            int index138 = num55;
            index1 = index138 + 1;
            objArray64[index138].SetDefaults(3743);
            break;
          }
          break;
        case 14:
          objArray1[index1].SetDefaults(771);
          ++index1;
          if (Main.bloodMoon)
          {
            objArray1[index1].SetDefaults(772);
            ++index1;
          }
          if (!Main.dayTime || Main.eclipse)
          {
            objArray1[index1].SetDefaults(773);
            ++index1;
          }
          if (Main.eclipse)
          {
            objArray1[index1].SetDefaults(774);
            ++index1;
          }
          if (NPC.downedMartians)
          {
            objArray1[index1++].SetDefaults(4445);
            if (Main.bloodMoon || Main.eclipse)
              objArray1[index1++].SetDefaults(4446);
          }
          if (Main.hardMode)
          {
            objArray1[index1].SetDefaults(4459);
            ++index1;
          }
          if (Main.hardMode)
          {
            objArray1[index1].SetDefaults(760);
            ++index1;
          }
          if (Main.hardMode)
          {
            objArray1[index1].SetDefaults(1346);
            ++index1;
          }
          if (Main.player[Main.myPlayer].ZoneGraveyard)
          {
            objArray1[index1].SetDefaults(4409);
            ++index1;
          }
          if (Main.player[Main.myPlayer].ZoneGraveyard)
          {
            objArray1[index1].SetDefaults(4392);
            ++index1;
          }
          if (Main.halloween)
          {
            objArray1[index1].SetDefaults(1743);
            int index14 = index1 + 1;
            objArray1[index14].SetDefaults(1744);
            int index15 = index14 + 1;
            objArray1[index15].SetDefaults(1745);
            index1 = index15 + 1;
          }
          if (NPC.downedMartians)
          {
            Item[] objArray2 = objArray1;
            int index14 = index1;
            int num17 = index14 + 1;
            objArray2[index14].SetDefaults(2862);
            Item[] objArray3 = objArray1;
            int index15 = num17;
            index1 = index15 + 1;
            objArray3[index15].SetDefaults(3109);
          }
          if (Main.player[Main.myPlayer].HasItem(3384) || Main.player[Main.myPlayer].HasItem(3664))
          {
            objArray1[index1].SetDefaults(3664);
            ++index1;
            break;
          }
          break;
        case 15:
          objArray1[index1].SetDefaults(1071);
          int index139 = index1 + 1;
          objArray1[index139].SetDefaults(1072);
          int index140 = index139 + 1;
          objArray1[index140].SetDefaults(1100);
          int index141 = index140 + 1;
          for (int Type = 1073; Type <= 1084; ++Type)
          {
            objArray1[index141].SetDefaults(Type);
            ++index141;
          }
          objArray1[index141].SetDefaults(1097);
          int index142 = index141 + 1;
          objArray1[index142].SetDefaults(1099);
          int index143 = index142 + 1;
          objArray1[index143].SetDefaults(1098);
          int index144 = index143 + 1;
          objArray1[index144].SetDefaults(1966);
          index1 = index144 + 1;
          if (Main.player[Main.myPlayer].ZoneGraveyard)
          {
            objArray1[index1].SetDefaults(4668);
            ++index1;
          }
          if (Main.hardMode)
          {
            objArray1[index1].SetDefaults(1967);
            int index14 = index1 + 1;
            objArray1[index14].SetDefaults(1968);
            index1 = index14 + 1;
          }
          if (!Main.player[Main.myPlayer].ZoneGraveyard)
          {
            objArray1[index1].SetDefaults(1490);
            int index14 = index1 + 1;
            if (Main.moonPhase <= 1)
            {
              objArray1[index14].SetDefaults(1481);
              index1 = index14 + 1;
            }
            else if (Main.moonPhase <= 3)
            {
              objArray1[index14].SetDefaults(1482);
              index1 = index14 + 1;
            }
            else if (Main.moonPhase <= 5)
            {
              objArray1[index14].SetDefaults(1483);
              index1 = index14 + 1;
            }
            else
            {
              objArray1[index14].SetDefaults(1484);
              index1 = index14 + 1;
            }
          }
          if (Main.player[Main.myPlayer].ZoneCrimson)
          {
            objArray1[index1].SetDefaults(1492);
            ++index1;
          }
          if (Main.player[Main.myPlayer].ZoneCorrupt)
          {
            objArray1[index1].SetDefaults(1488);
            ++index1;
          }
          if (Main.player[Main.myPlayer].ZoneHallow)
          {
            objArray1[index1].SetDefaults(1489);
            ++index1;
          }
          if (Main.player[Main.myPlayer].ZoneJungle)
          {
            objArray1[index1].SetDefaults(1486);
            ++index1;
          }
          if (Main.player[Main.myPlayer].ZoneSnow)
          {
            objArray1[index1].SetDefaults(1487);
            ++index1;
          }
          if (Main.player[Main.myPlayer].ZoneDesert)
          {
            objArray1[index1].SetDefaults(1491);
            ++index1;
          }
          if (Main.bloodMoon)
          {
            objArray1[index1].SetDefaults(1493);
            ++index1;
          }
          if (!Main.player[Main.myPlayer].ZoneGraveyard)
          {
            if ((double) Main.player[Main.myPlayer].position.Y / 16.0 < Main.worldSurface * 0.349999994039536)
            {
              objArray1[index1].SetDefaults(1485);
              ++index1;
            }
            if ((double) Main.player[Main.myPlayer].position.Y / 16.0 < Main.worldSurface * 0.349999994039536 && Main.hardMode)
            {
              objArray1[index1].SetDefaults(1494);
              ++index1;
            }
          }
          if (Main.player[Main.myPlayer].ZoneGraveyard)
          {
            objArray1[index1].SetDefaults(4723);
            int index14 = index1 + 1;
            objArray1[index14].SetDefaults(4724);
            int index15 = index14 + 1;
            objArray1[index15].SetDefaults(4725);
            int index19 = index15 + 1;
            objArray1[index19].SetDefaults(4726);
            int index53 = index19 + 1;
            objArray1[index53].SetDefaults(4727);
            int index54 = index53 + 1;
            objArray1[index54].SetDefaults(4728);
            int index55 = index54 + 1;
            objArray1[index55].SetDefaults(4729);
            index1 = index55 + 1;
          }
          if (Main.xMas)
          {
            for (int Type = 1948; Type <= 1957; ++Type)
            {
              objArray1[index1].SetDefaults(Type);
              ++index1;
            }
          }
          for (int Type = 2158; Type <= 2160; ++Type)
          {
            if (index1 < 39)
              objArray1[index1].SetDefaults(Type);
            ++index1;
          }
          for (int Type = 2008; Type <= 2014; ++Type)
          {
            if (index1 < 39)
              objArray1[index1].SetDefaults(Type);
            ++index1;
          }
          break;
        case 16:
          Item[] objArray65 = objArray1;
          int index145 = index1;
          int num56 = index145 + 1;
          objArray65[index145].SetDefaults(1430);
          Item[] objArray66 = objArray1;
          int index146 = num56;
          int num57 = index146 + 1;
          objArray66[index146].SetDefaults(986);
          if (NPC.AnyNPCs(108))
            objArray1[num57++].SetDefaults(2999);
          if (Main.hardMode && NPC.downedPlantBoss)
          {
            Item[] objArray2 = objArray1;
            int index14 = num57;
            int num17 = index14 + 1;
            objArray2[index14].SetDefaults(1159);
            Item[] objArray3 = objArray1;
            int index15 = num17;
            int num18 = index15 + 1;
            objArray3[index15].SetDefaults(1160);
            Item[] objArray25 = objArray1;
            int index19 = num18;
            int num19 = index19 + 1;
            objArray25[index19].SetDefaults(1161);
            if (!Main.dayTime)
              objArray1[num19++].SetDefaults(1158);
            if (Main.player[Main.myPlayer].ZoneJungle)
              objArray1[num19++].SetDefaults(1167);
            Item[] objArray26 = objArray1;
            int index53 = num19;
            num57 = index53 + 1;
            objArray26[index53].SetDefaults(1339);
          }
          if (Main.hardMode && Main.player[Main.myPlayer].ZoneJungle)
          {
            objArray1[num57++].SetDefaults(1171);
            if (!Main.dayTime)
              objArray1[num57++].SetDefaults(1162);
          }
          Item[] objArray67 = objArray1;
          int index147 = num57;
          int num58 = index147 + 1;
          objArray67[index147].SetDefaults(909);
          Item[] objArray68 = objArray1;
          int index148 = num58;
          int num59 = index148 + 1;
          objArray68[index148].SetDefaults(910);
          Item[] objArray69 = objArray1;
          int index149 = num59;
          int num60 = index149 + 1;
          objArray69[index149].SetDefaults(940);
          Item[] objArray70 = objArray1;
          int index150 = num60;
          int num61 = index150 + 1;
          objArray70[index150].SetDefaults(941);
          Item[] objArray71 = objArray1;
          int index151 = num61;
          int num62 = index151 + 1;
          objArray71[index151].SetDefaults(942);
          Item[] objArray72 = objArray1;
          int index152 = num62;
          int num63 = index152 + 1;
          objArray72[index152].SetDefaults(943);
          Item[] objArray73 = objArray1;
          int index153 = num63;
          int num64 = index153 + 1;
          objArray73[index153].SetDefaults(944);
          Item[] objArray74 = objArray1;
          int index154 = num64;
          int num65 = index154 + 1;
          objArray74[index154].SetDefaults(945);
          Item[] objArray75 = objArray1;
          int index155 = num65;
          int num66 = index155 + 1;
          objArray75[index155].SetDefaults(4922);
          Item[] objArray76 = objArray1;
          int index156 = num66;
          index1 = index156 + 1;
          objArray76[index156].SetDefaults(4417);
          if (Main.player[Main.myPlayer].HasItem(1835))
            objArray1[index1++].SetDefaults(1836);
          if (Main.player[Main.myPlayer].HasItem(1258))
            objArray1[index1++].SetDefaults(1261);
          if (Main.halloween)
          {
            objArray1[index1++].SetDefaults(1791);
            break;
          }
          break;
        case 17:
          objArray1[index1].SetDefaults(928);
          int index157 = index1 + 1;
          objArray1[index157].SetDefaults(929);
          int index158 = index157 + 1;
          objArray1[index158].SetDefaults(876);
          int index159 = index158 + 1;
          objArray1[index159].SetDefaults(877);
          int index160 = index159 + 1;
          objArray1[index160].SetDefaults(878);
          int index161 = index160 + 1;
          objArray1[index161].SetDefaults(2434);
          index1 = index161 + 1;
          int num67 = (int) (((double) Main.screenPosition.X + (double) (Main.screenWidth / 2)) / 16.0);
          if ((double) Main.screenPosition.Y / 16.0 < Main.worldSurface + 10.0 && (num67 < 380 || num67 > Main.maxTilesX - 380))
          {
            objArray1[index1].SetDefaults(1180);
            ++index1;
          }
          if (Main.hardMode && NPC.downedMechBossAny && NPC.AnyNPCs(208))
          {
            objArray1[index1].SetDefaults(1337);
            ++index1;
            break;
          }
          break;
        case 18:
          objArray1[index1].SetDefaults(1990);
          int index162 = index1 + 1;
          objArray1[index162].SetDefaults(1979);
          index1 = index162 + 1;
          if (Main.player[Main.myPlayer].statLifeMax >= 400)
          {
            objArray1[index1].SetDefaults(1977);
            ++index1;
          }
          if (Main.player[Main.myPlayer].statManaMax >= 200)
          {
            objArray1[index1].SetDefaults(1978);
            ++index1;
          }
          long num68 = 0;
          for (int index14 = 0; index14 < 54; ++index14)
          {
            if (Main.player[Main.myPlayer].inventory[index14].type == 71)
              num68 += (long) Main.player[Main.myPlayer].inventory[index14].stack;
            if (Main.player[Main.myPlayer].inventory[index14].type == 72)
              num68 += (long) (Main.player[Main.myPlayer].inventory[index14].stack * 100);
            if (Main.player[Main.myPlayer].inventory[index14].type == 73)
              num68 += (long) (Main.player[Main.myPlayer].inventory[index14].stack * 10000);
            if (Main.player[Main.myPlayer].inventory[index14].type == 74)
              num68 += (long) (Main.player[Main.myPlayer].inventory[index14].stack * 1000000);
          }
          if (num68 >= 1000000L)
          {
            objArray1[index1].SetDefaults(1980);
            ++index1;
          }
          if (Main.moonPhase % 2 == 0 && Main.dayTime || Main.moonPhase % 2 == 1 && !Main.dayTime)
          {
            objArray1[index1].SetDefaults(1981);
            ++index1;
          }
          if (Main.player[Main.myPlayer].team != 0)
          {
            objArray1[index1].SetDefaults(1982);
            ++index1;
          }
          if (Main.hardMode)
          {
            objArray1[index1].SetDefaults(1983);
            ++index1;
          }
          if (NPC.AnyNPCs(208))
          {
            objArray1[index1].SetDefaults(1984);
            ++index1;
          }
          if (Main.hardMode && NPC.downedMechBoss1 && (NPC.downedMechBoss2 && NPC.downedMechBoss3))
          {
            objArray1[index1].SetDefaults(1985);
            ++index1;
          }
          if (Main.hardMode && NPC.downedMechBossAny)
          {
            objArray1[index1].SetDefaults(1986);
            ++index1;
          }
          if (Main.hardMode && NPC.downedMartians)
          {
            objArray1[index1].SetDefaults(2863);
            int index14 = index1 + 1;
            objArray1[index14].SetDefaults(3259);
            index1 = index14 + 1;
            break;
          }
          break;
        case 19:
          for (int index14 = 0; index14 < 40; ++index14)
          {
            if (Main.travelShop[index14] != 0)
            {
              objArray1[index1].netDefaults(Main.travelShop[index14]);
              ++index1;
            }
          }
          break;
        case 20:
          if (Main.moonPhase % 2 == 0)
            objArray1[index1].SetDefaults(3001);
          else
            objArray1[index1].SetDefaults(28);
          int index163 = index1 + 1;
          if (!Main.dayTime || Main.moonPhase == 0)
            objArray1[index163].SetDefaults(3002);
          else
            objArray1[index163].SetDefaults(282);
          int index164 = index163 + 1;
          if (Main.time % 60.0 * 60.0 * 6.0 <= 10800.0)
            objArray1[index164].SetDefaults(3004);
          else
            objArray1[index164].SetDefaults(8);
          int index165 = index164 + 1;
          if (Main.moonPhase == 0 || Main.moonPhase == 1 || (Main.moonPhase == 4 || Main.moonPhase == 5))
            objArray1[index165].SetDefaults(3003);
          else
            objArray1[index165].SetDefaults(40);
          int index166 = index165 + 1;
          if (Main.moonPhase % 4 == 0)
            objArray1[index166].SetDefaults(3310);
          else if (Main.moonPhase % 4 == 1)
            objArray1[index166].SetDefaults(3313);
          else if (Main.moonPhase % 4 == 2)
            objArray1[index166].SetDefaults(3312);
          else
            objArray1[index166].SetDefaults(3311);
          int index167 = index166 + 1;
          objArray1[index167].SetDefaults(166);
          int index168 = index167 + 1;
          objArray1[index168].SetDefaults(965);
          index1 = index168 + 1;
          if (Main.hardMode)
          {
            if (Main.moonPhase < 4)
              objArray1[index1].SetDefaults(3316);
            else
              objArray1[index1].SetDefaults(3315);
            int index14 = index1 + 1;
            objArray1[index14].SetDefaults(3334);
            index1 = index14 + 1;
            if (Main.bloodMoon)
            {
              objArray1[index1].SetDefaults(3258);
              ++index1;
            }
          }
          if (Main.moonPhase == 0 && !Main.dayTime)
          {
            objArray1[index1].SetDefaults(3043);
            ++index1;
            break;
          }
          break;
        case 21:
          bool flag2 = Main.hardMode && NPC.downedMechBossAny;
          int num69 = !Main.hardMode ? 0 : (NPC.downedGolemBoss ? 1 : 0);
          objArray1[index1].SetDefaults(353);
          int index169 = index1 + 1;
          objArray1[index169].SetDefaults(3828);
          objArray1[index169].shopCustomPrice = num69 == 0 ? (!flag2 ? new int?(Item.buyPrice(0, 0, 25, 0)) : new int?(Item.buyPrice(0, 1, 0, 0))) : new int?(Item.buyPrice(0, 4, 0, 0));
          int index170 = index169 + 1;
          objArray1[index170].SetDefaults(3816);
          int index171 = index170 + 1;
          objArray1[index171].SetDefaults(3813);
          objArray1[index171].shopCustomPrice = new int?(75);
          objArray1[index171].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
          int num70 = index171 + 1;
          int index172 = 10;
          objArray1[index172].SetDefaults(3818);
          objArray1[index172].shopCustomPrice = new int?(5);
          objArray1[index172].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
          int index173 = index172 + 1;
          objArray1[index173].SetDefaults(3824);
          objArray1[index173].shopCustomPrice = new int?(5);
          objArray1[index173].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
          int index174 = index173 + 1;
          objArray1[index174].SetDefaults(3832);
          objArray1[index174].shopCustomPrice = new int?(5);
          objArray1[index174].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
          int index175 = index174 + 1;
          objArray1[index175].SetDefaults(3829);
          objArray1[index175].shopCustomPrice = new int?(5);
          objArray1[index175].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
          if (flag2)
          {
            int index14 = 20;
            objArray1[index14].SetDefaults(3819);
            objArray1[index14].shopCustomPrice = new int?(25);
            objArray1[index14].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index15 = index14 + 1;
            objArray1[index15].SetDefaults(3825);
            objArray1[index15].shopCustomPrice = new int?(25);
            objArray1[index15].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index19 = index15 + 1;
            objArray1[index19].SetDefaults(3833);
            objArray1[index19].shopCustomPrice = new int?(25);
            objArray1[index19].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index53 = index19 + 1;
            objArray1[index53].SetDefaults(3830);
            objArray1[index53].shopCustomPrice = new int?(25);
            objArray1[index53].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
          }
          if (num69 != 0)
          {
            int index14 = 30;
            objArray1[index14].SetDefaults(3820);
            objArray1[index14].shopCustomPrice = new int?(100);
            objArray1[index14].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index15 = index14 + 1;
            objArray1[index15].SetDefaults(3826);
            objArray1[index15].shopCustomPrice = new int?(100);
            objArray1[index15].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index19 = index15 + 1;
            objArray1[index19].SetDefaults(3834);
            objArray1[index19].shopCustomPrice = new int?(100);
            objArray1[index19].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index53 = index19 + 1;
            objArray1[index53].SetDefaults(3831);
            objArray1[index53].shopCustomPrice = new int?(100);
            objArray1[index53].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
          }
          if (flag2)
          {
            int index14 = 4;
            objArray1[index14].SetDefaults(3800);
            objArray1[index14].shopCustomPrice = new int?(25);
            objArray1[index14].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index15 = index14 + 1;
            objArray1[index15].SetDefaults(3801);
            objArray1[index15].shopCustomPrice = new int?(25);
            objArray1[index15].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index19 = index15 + 1;
            objArray1[index19].SetDefaults(3802);
            objArray1[index19].shopCustomPrice = new int?(25);
            objArray1[index19].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            num70 = index19 + 1;
            int index53 = 14;
            objArray1[index53].SetDefaults(3797);
            objArray1[index53].shopCustomPrice = new int?(25);
            objArray1[index53].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index54 = index53 + 1;
            objArray1[index54].SetDefaults(3798);
            objArray1[index54].shopCustomPrice = new int?(25);
            objArray1[index54].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index55 = index54 + 1;
            objArray1[index55].SetDefaults(3799);
            objArray1[index55].shopCustomPrice = new int?(25);
            objArray1[index55].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            num70 = index55 + 1;
            int index56 = 24;
            objArray1[index56].SetDefaults(3803);
            objArray1[index56].shopCustomPrice = new int?(25);
            objArray1[index56].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index57 = index56 + 1;
            objArray1[index57].SetDefaults(3804);
            objArray1[index57].shopCustomPrice = new int?(25);
            objArray1[index57].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index136 = index57 + 1;
            objArray1[index136].SetDefaults(3805);
            objArray1[index136].shopCustomPrice = new int?(25);
            objArray1[index136].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            num70 = index136 + 1;
            int index137 = 34;
            objArray1[index137].SetDefaults(3806);
            objArray1[index137].shopCustomPrice = new int?(25);
            objArray1[index137].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index138 = index137 + 1;
            objArray1[index138].SetDefaults(3807);
            objArray1[index138].shopCustomPrice = new int?(25);
            objArray1[index138].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index176 = index138 + 1;
            objArray1[index176].SetDefaults(3808);
            objArray1[index176].shopCustomPrice = new int?(25);
            objArray1[index176].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            num70 = index176 + 1;
          }
          if (num69 != 0)
          {
            int index14 = 7;
            objArray1[index14].SetDefaults(3871);
            objArray1[index14].shopCustomPrice = new int?(75);
            objArray1[index14].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index15 = index14 + 1;
            objArray1[index15].SetDefaults(3872);
            objArray1[index15].shopCustomPrice = new int?(75);
            objArray1[index15].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index19 = index15 + 1;
            objArray1[index19].SetDefaults(3873);
            objArray1[index19].shopCustomPrice = new int?(75);
            objArray1[index19].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            num70 = index19 + 1;
            int index53 = 17;
            objArray1[index53].SetDefaults(3874);
            objArray1[index53].shopCustomPrice = new int?(75);
            objArray1[index53].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index54 = index53 + 1;
            objArray1[index54].SetDefaults(3875);
            objArray1[index54].shopCustomPrice = new int?(75);
            objArray1[index54].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index55 = index54 + 1;
            objArray1[index55].SetDefaults(3876);
            objArray1[index55].shopCustomPrice = new int?(75);
            objArray1[index55].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            num70 = index55 + 1;
            int index56 = 27;
            objArray1[index56].SetDefaults(3877);
            objArray1[index56].shopCustomPrice = new int?(75);
            objArray1[index56].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index57 = index56 + 1;
            objArray1[index57].SetDefaults(3878);
            objArray1[index57].shopCustomPrice = new int?(75);
            objArray1[index57].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index136 = index57 + 1;
            objArray1[index136].SetDefaults(3879);
            objArray1[index136].shopCustomPrice = new int?(75);
            objArray1[index136].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            num70 = index136 + 1;
            int index137 = 37;
            objArray1[index137].SetDefaults(3880);
            objArray1[index137].shopCustomPrice = new int?(75);
            objArray1[index137].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index138 = index137 + 1;
            objArray1[index138].SetDefaults(3881);
            objArray1[index138].shopCustomPrice = new int?(75);
            objArray1[index138].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index176 = index138 + 1;
            objArray1[index176].SetDefaults(3882);
            objArray1[index176].shopCustomPrice = new int?(75);
            objArray1[index176].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            num70 = index176 + 1;
          }
          index1 = num69 == 0 ? (!flag2 ? 4 : 30) : 39;
          break;
        case 22:
          Item[] objArray77 = objArray1;
          int index177 = index1;
          int num71 = index177 + 1;
          objArray77[index177].SetDefaults(4587);
          Item[] objArray78 = objArray1;
          int index178 = num71;
          int num72 = index178 + 1;
          objArray78[index178].SetDefaults(4590);
          Item[] objArray79 = objArray1;
          int index179 = num72;
          int num73 = index179 + 1;
          objArray79[index179].SetDefaults(4589);
          Item[] objArray80 = objArray1;
          int index180 = num73;
          int num74 = index180 + 1;
          objArray80[index180].SetDefaults(4588);
          Item[] objArray81 = objArray1;
          int index181 = num74;
          int num75 = index181 + 1;
          objArray81[index181].SetDefaults(4083);
          Item[] objArray82 = objArray1;
          int index182 = num75;
          int num76 = index182 + 1;
          objArray82[index182].SetDefaults(4084);
          Item[] objArray83 = objArray1;
          int index183 = num76;
          int num77 = index183 + 1;
          objArray83[index183].SetDefaults(4085);
          Item[] objArray84 = objArray1;
          int index184 = num77;
          int num78 = index184 + 1;
          objArray84[index184].SetDefaults(4086);
          Item[] objArray85 = objArray1;
          int index185 = num78;
          int num79 = index185 + 1;
          objArray85[index185].SetDefaults(4087);
          Item[] objArray86 = objArray1;
          int index186 = num79;
          int index187 = index186 + 1;
          objArray86[index186].SetDefaults(4088);
          int scoreAccumulated2 = Main.LocalPlayer.golferScoreAccumulated;
          if (scoreAccumulated2 > 500)
          {
            objArray1[index187].SetDefaults(4039);
            int index14 = index187 + 1;
            objArray1[index14].SetDefaults(4094);
            int index15 = index14 + 1;
            objArray1[index15].SetDefaults(4093);
            int index19 = index15 + 1;
            objArray1[index19].SetDefaults(4092);
            index187 = index19 + 1;
          }
          Item[] objArray87 = objArray1;
          int index188 = index187;
          int num80 = index188 + 1;
          objArray87[index188].SetDefaults(4089);
          Item[] objArray88 = objArray1;
          int index189 = num80;
          int num81 = index189 + 1;
          objArray88[index189].SetDefaults(3989);
          Item[] objArray89 = objArray1;
          int index190 = num81;
          int num82 = index190 + 1;
          objArray89[index190].SetDefaults(4095);
          Item[] objArray90 = objArray1;
          int index191 = num82;
          int num83 = index191 + 1;
          objArray90[index191].SetDefaults(4040);
          Item[] objArray91 = objArray1;
          int index192 = num83;
          int num84 = index192 + 1;
          objArray91[index192].SetDefaults(4319);
          Item[] objArray92 = objArray1;
          int index193 = num84;
          int index194 = index193 + 1;
          objArray92[index193].SetDefaults(4320);
          if (scoreAccumulated2 > 1000)
          {
            objArray1[index194].SetDefaults(4591);
            int index14 = index194 + 1;
            objArray1[index14].SetDefaults(4594);
            int index15 = index14 + 1;
            objArray1[index15].SetDefaults(4593);
            int index19 = index15 + 1;
            objArray1[index19].SetDefaults(4592);
            index194 = index19 + 1;
          }
          Item[] objArray93 = objArray1;
          int index195 = index194;
          int num85 = index195 + 1;
          objArray93[index195].SetDefaults(4135);
          Item[] objArray94 = objArray1;
          int index196 = num85;
          int num86 = index196 + 1;
          objArray94[index196].SetDefaults(4138);
          Item[] objArray95 = objArray1;
          int index197 = num86;
          int num87 = index197 + 1;
          objArray95[index197].SetDefaults(4136);
          Item[] objArray96 = objArray1;
          int index198 = num87;
          int num88 = index198 + 1;
          objArray96[index198].SetDefaults(4137);
          Item[] objArray97 = objArray1;
          int index199 = num88;
          index1 = index199 + 1;
          objArray97[index199].SetDefaults(4049);
          if (scoreAccumulated2 > 500)
          {
            objArray1[index1].SetDefaults(4265);
            ++index1;
          }
          if (scoreAccumulated2 > 2000)
          {
            objArray1[index1].SetDefaults(4595);
            int index14 = index1 + 1;
            objArray1[index14].SetDefaults(4598);
            int index15 = index14 + 1;
            objArray1[index15].SetDefaults(4597);
            int index19 = index15 + 1;
            objArray1[index19].SetDefaults(4596);
            index1 = index19 + 1;
            if (NPC.downedBoss3)
            {
              objArray1[index1].SetDefaults(4264);
              ++index1;
            }
          }
          if (scoreAccumulated2 > 500)
          {
            objArray1[index1].SetDefaults(4599);
            ++index1;
          }
          if (scoreAccumulated2 >= 1000)
          {
            objArray1[index1].SetDefaults(4600);
            ++index1;
          }
          if (scoreAccumulated2 >= 2000)
          {
            objArray1[index1].SetDefaults(4601);
            ++index1;
          }
          if (scoreAccumulated2 >= 2000)
          {
            switch (Main.moonPhase)
            {
              case 0:
              case 1:
                objArray1[index1].SetDefaults(4658);
                ++index1;
                break;
              case 2:
              case 3:
                objArray1[index1].SetDefaults(4659);
                ++index1;
                break;
              case 4:
              case 5:
                objArray1[index1].SetDefaults(4660);
                ++index1;
                break;
              case 6:
              case 7:
                objArray1[index1].SetDefaults(4661);
                ++index1;
                break;
            }
          }
          else
            break;
          break;
        case 23:
          BestiaryUnlockProgressReport bestiaryProgressReport = Main.GetBestiaryProgressReport();
          if (Chest.BestiaryGirl_IsFairyTorchAvailable())
            objArray1[index1++].SetDefaults(4776);
          Item[] objArray98 = objArray1;
          int index200 = index1;
          int num89 = index200 + 1;
          objArray98[index200].SetDefaults(4767);
          Item[] objArray99 = objArray1;
          int index201 = num89;
          index1 = index201 + 1;
          objArray99[index201].SetDefaults(4759);
          if ((double) bestiaryProgressReport.CompletionPercent >= 0.150000005960464)
            objArray1[index1++].SetDefaults(4672);
          if (!NPC.boughtCat)
            objArray1[index1++].SetDefaults(4829);
          if (!NPC.boughtDog && (double) bestiaryProgressReport.CompletionPercent >= 0.25)
            objArray1[index1++].SetDefaults(4830);
          if (!NPC.boughtBunny && (double) bestiaryProgressReport.CompletionPercent >= 0.449999988079071)
            objArray1[index1++].SetDefaults(4910);
          if ((double) bestiaryProgressReport.CompletionPercent >= 0.300000011920929)
            objArray1[index1++].SetDefaults(4871);
          if ((double) bestiaryProgressReport.CompletionPercent >= 0.300000011920929)
            objArray1[index1++].SetDefaults(4907);
          if (NPC.downedTowerSolar)
            objArray1[index1++].SetDefaults(4677);
          if ((double) bestiaryProgressReport.CompletionPercent >= 0.100000001490116)
            objArray1[index1++].SetDefaults(4676);
          if ((double) bestiaryProgressReport.CompletionPercent >= 0.300000011920929)
            objArray1[index1++].SetDefaults(4762);
          if ((double) bestiaryProgressReport.CompletionPercent >= 0.25)
            objArray1[index1++].SetDefaults(4716);
          if ((double) bestiaryProgressReport.CompletionPercent >= 0.300000011920929)
            objArray1[index1++].SetDefaults(4785);
          if ((double) bestiaryProgressReport.CompletionPercent >= 0.300000011920929)
            objArray1[index1++].SetDefaults(4786);
          if ((double) bestiaryProgressReport.CompletionPercent >= 0.300000011920929)
            objArray1[index1++].SetDefaults(4787);
          if ((double) bestiaryProgressReport.CompletionPercent >= 0.300000011920929 && Main.hardMode)
            objArray1[index1++].SetDefaults(4788);
          if ((double) bestiaryProgressReport.CompletionPercent >= 0.400000005960464)
            objArray1[index1++].SetDefaults(4955);
          if (Main.hardMode && Main.bloodMoon)
            objArray1[index1++].SetDefaults(4736);
          if (NPC.downedPlantBoss)
            objArray1[index1++].SetDefaults(4701);
          if ((double) bestiaryProgressReport.CompletionPercent >= 0.5)
            objArray1[index1++].SetDefaults(4765);
          if ((double) bestiaryProgressReport.CompletionPercent >= 0.5)
            objArray1[index1++].SetDefaults(4766);
          if ((double) bestiaryProgressReport.CompletionPercent >= 0.5)
            objArray1[index1++].SetDefaults(4777);
          if ((double) bestiaryProgressReport.CompletionPercent >= 0.600000023841858)
            objArray1[index1++].SetDefaults(4763);
          if ((double) bestiaryProgressReport.CompletionPercent >= 0.699999988079071)
            objArray1[index1++].SetDefaults(4735);
          if ((double) bestiaryProgressReport.CompletionPercent >= 1.0)
            objArray1[index1++].SetDefaults(4951);
          switch (Main.moonPhase)
          {
            case 0:
            case 1:
              Item[] objArray100 = objArray1;
              int index202 = index1;
              int num90 = index202 + 1;
              objArray100[index202].SetDefaults(4768);
              Item[] objArray101 = objArray1;
              int index203 = num90;
              index1 = index203 + 1;
              objArray101[index203].SetDefaults(4769);
              break;
            case 2:
            case 3:
              Item[] objArray102 = objArray1;
              int index204 = index1;
              int num91 = index204 + 1;
              objArray102[index204].SetDefaults(4770);
              Item[] objArray103 = objArray1;
              int index205 = num91;
              index1 = index205 + 1;
              objArray103[index205].SetDefaults(4771);
              break;
            case 4:
            case 5:
              Item[] objArray104 = objArray1;
              int index206 = index1;
              int num92 = index206 + 1;
              objArray104[index206].SetDefaults(4772);
              Item[] objArray105 = objArray1;
              int index207 = num92;
              index1 = index207 + 1;
              objArray105[index207].SetDefaults(4773);
              break;
            case 6:
            case 7:
              Item[] objArray106 = objArray1;
              int index208 = index1;
              int num93 = index208 + 1;
              objArray106[index208].SetDefaults(4560);
              Item[] objArray107 = objArray1;
              int index209 = num93;
              index1 = index209 + 1;
              objArray107[index209].SetDefaults(4775);
              break;
          }
          break;
      }
      if (((type == 19 ? 0 : (type != 20 ? 1 : 0)) & (flag1 ? 1 : 0)) != 0 && !Main.player[Main.myPlayer].ZoneCorrupt && !Main.player[Main.myPlayer].ZoneCrimson)
      {
        if (!Main.player[Main.myPlayer].ZoneSnow && !Main.player[Main.myPlayer].ZoneDesert && (!Main.player[Main.myPlayer].ZoneBeach && !Main.player[Main.myPlayer].ZoneJungle) && (!Main.player[Main.myPlayer].ZoneHallow && !Main.player[Main.myPlayer].ZoneGlowshroom && ((double) Main.player[Main.myPlayer].Center.Y / 16.0 < Main.worldSurface && index1 < 39)))
          objArray1[index1++].SetDefaults(4876);
        if (Main.player[Main.myPlayer].ZoneSnow && index1 < 39)
          objArray1[index1++].SetDefaults(4920);
        if (Main.player[Main.myPlayer].ZoneDesert && index1 < 39)
          objArray1[index1++].SetDefaults(4919);
        if (!Main.player[Main.myPlayer].ZoneSnow && !Main.player[Main.myPlayer].ZoneDesert && (!Main.player[Main.myPlayer].ZoneBeach && !Main.player[Main.myPlayer].ZoneJungle) && (!Main.player[Main.myPlayer].ZoneHallow && !Main.player[Main.myPlayer].ZoneGlowshroom && ((double) Main.player[Main.myPlayer].Center.Y / 16.0 >= Main.worldSurface && index1 < 39)))
          objArray1[index1++].SetDefaults(4917);
        if (Main.player[Main.myPlayer].ZoneBeach && (double) Main.player[Main.myPlayer].position.Y < Main.worldSurface * 16.0 && index1 < 39)
          objArray1[index1++].SetDefaults(4918);
        if (Main.player[Main.myPlayer].ZoneJungle && index1 < 39)
          objArray1[index1++].SetDefaults(4875);
        if (Main.player[Main.myPlayer].ZoneHallow && index1 < 39)
          objArray1[index1++].SetDefaults(4916);
        if (Main.player[Main.myPlayer].ZoneGlowshroom && index1 < 39)
          objArray1[index1++].SetDefaults(4921);
      }
      for (int index14 = 0; index14 < index1; ++index14)
        objArray1[index14].isAShopItem = true;
    }

    private static bool BestiaryGirl_IsFairyTorchAvailable()
    {
      return Chest.DidDiscoverBestiaryEntry(585) && Chest.DidDiscoverBestiaryEntry(584) && Chest.DidDiscoverBestiaryEntry(583);
    }

    private static bool DidDiscoverBestiaryEntry(int npcId)
    {
      return Main.BestiaryDB.FindEntryByNPCID(npcId).UIInfoProvider.GetEntryUICollectionInfo().UnlockState > BestiaryEntryUnlockState.NotKnownAtAll_0;
    }

    public static void UpdateChestFrames()
    {
      int num = 8000;
      Chest._chestInUse.Clear();
      for (int index = 0; index < (int) byte.MaxValue; ++index)
      {
        if (Main.player[index].active && Main.player[index].chest >= 0 && Main.player[index].chest < num)
          Chest._chestInUse.Add(Main.player[index].chest);
      }
      for (int index = 0; index < num; ++index)
      {
        Chest chest = Main.chest[index];
        if (chest != null)
        {
          if (Chest._chestInUse.Contains(index))
            ++chest.frameCounter;
          else
            --chest.frameCounter;
          if (chest.frameCounter < 0)
            chest.frameCounter = 0;
          if (chest.frameCounter > 10)
            chest.frameCounter = 10;
          chest.frame = chest.frameCounter != 0 ? (chest.frameCounter != 10 ? 1 : 2) : 0;
        }
      }
    }
  }
}
