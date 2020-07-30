// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Tile_Entities.TEWeaponsRack
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using System.IO;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.GameContent.Tile_Entities
{
  public class TEWeaponsRack : TileEntity
  {
    private static byte _myEntityID;
    public Item item;
    private const int MyTileID = 471;

    public TEWeaponsRack()
    {
      this.item = new Item();
    }

    public override void RegisterTileEntityID(int assignedID)
    {
      TEWeaponsRack._myEntityID = (byte) assignedID;
    }

    public override TileEntity GenerateInstance()
    {
      return (TileEntity) new TEWeaponsRack();
    }

    public override void NetPlaceEntityAttempt(int x, int y)
    {
      TEWeaponsRack.NetPlaceEntity(x, y);
    }

    public static void NetPlaceEntity(int x, int y)
    {
      NetMessage.SendData(86, -1, -1, (NetworkText) null, TEWeaponsRack.Place(x, y), (float) x, (float) y, 0.0f, 0, 0, 0);
    }

    public override bool IsTileValidForEntity(int x, int y)
    {
      return TEWeaponsRack.ValidTile(x, y);
    }

    public static bool ValidTile(int x, int y)
    {
      return Main.tile[x, y].active() && Main.tile[x, y].type == (ushort) 471 && (Main.tile[x, y].frameY == (short) 0 && (int) Main.tile[x, y].frameX % 54 == 0);
    }

    public static int Place(int x, int y)
    {
      TEWeaponsRack teWeaponsRack = new TEWeaponsRack();
      teWeaponsRack.Position = new Point16(x, y);
      teWeaponsRack.ID = TileEntity.AssignNewID();
      teWeaponsRack.type = TEWeaponsRack._myEntityID;
      TileEntity.ByID[teWeaponsRack.ID] = (TileEntity) teWeaponsRack;
      TileEntity.ByPosition[teWeaponsRack.Position] = (TileEntity) teWeaponsRack;
      return teWeaponsRack.ID;
    }

    public static int Hook_AfterPlacement(
      int x,
      int y,
      int type = 471,
      int style = 0,
      int direction = 1,
      int alternate = 0)
    {
      if (Main.netMode != 1)
        return TEWeaponsRack.Place(x, y);
      NetMessage.SendTileSquare(Main.myPlayer, x, y, 5, TileChangeType.None);
      NetMessage.SendData(87, -1, -1, (NetworkText) null, x, (float) y, (float) TEWeaponsRack._myEntityID, 0.0f, 0, 0, 0);
      return -1;
    }

    public static void Kill(int x, int y)
    {
      TileEntity tileEntity;
      if (!TileEntity.ByPosition.TryGetValue(new Point16(x, y), out tileEntity) || (int) tileEntity.type != (int) TEWeaponsRack._myEntityID)
        return;
      TileEntity.ByID.Remove(tileEntity.ID);
      TileEntity.ByPosition.Remove(new Point16(x, y));
    }

    public static int Find(int x, int y)
    {
      TileEntity tileEntity;
      return TileEntity.ByPosition.TryGetValue(new Point16(x, y), out tileEntity) && (int) tileEntity.type == (int) TEWeaponsRack._myEntityID ? tileEntity.ID : -1;
    }

    public override void WriteExtraData(BinaryWriter writer, bool networkSend)
    {
      writer.Write((short) this.item.netID);
      writer.Write(this.item.prefix);
      writer.Write((short) this.item.stack);
    }

    public override void ReadExtraData(BinaryReader reader, bool networkSend)
    {
      this.item = new Item();
      this.item.netDefaults((int) reader.ReadInt16());
      this.item.Prefix((int) reader.ReadByte());
      this.item.stack = (int) reader.ReadInt16();
    }

    public override string ToString()
    {
      return this.Position.X.ToString() + "x  " + (object) this.Position.Y + "y item: " + (object) this.item;
    }

    public static void Framing_CheckTile(int callX, int callY)
    {
      int num1 = 3;
      int num2 = 3;
      if (WorldGen.destroyObject)
        return;
      int num3 = callX;
      int num4 = callY;
      Tile tileSafely = Framing.GetTileSafely(callX, callY);
      int x = num3 - (int) tileSafely.frameX / 18 % num1;
      int y = num4 - (int) tileSafely.frameY / 18 % num2;
      bool flag = false;
      for (int index1 = x; index1 < x + num1; ++index1)
      {
        for (int index2 = y; index2 < y + num2; ++index2)
        {
          Tile tile = Main.tile[index1, index2];
          if (!tile.active() || tile.type != (ushort) 471 || tile.wall == (ushort) 0)
            flag = true;
        }
      }
      if (!flag)
        return;
      Item.NewItem(x * 16, y * 16, 48, 48, 2699, 1, false, 0, false, false);
      WorldGen.destroyObject = true;
      int index = TEWeaponsRack.Find(x, y);
      if (index != -1)
      {
        TEWeaponsRack teWeaponsRack = (TEWeaponsRack) TileEntity.ByID[index];
        if (!teWeaponsRack.item.IsAir)
          teWeaponsRack.DropItem();
      }
      for (int i = x; i < x + num1; ++i)
      {
        for (int j = y; j < y + num2; ++j)
        {
          if (Main.tile[i, j].active() && Main.tile[i, j].type == (ushort) 471)
            WorldGen.KillTile(i, j, false, false, false);
        }
      }
      WorldGen.destroyObject = false;
    }

    public void DropItem()
    {
      if (Main.netMode != 1)
        Item.NewItem((int) this.Position.X * 16, (int) this.Position.Y * 16, 32, 32, this.item.netID, 1, false, (int) this.item.prefix, false, false);
      this.item = new Item();
    }

    public static void TryPlacing(int x, int y, int netid, int prefix, int stack)
    {
      WorldGen.RangeFrame(x, y, x + 3, y + 3);
      int index = TEWeaponsRack.Find(x, y);
      if (index == -1)
      {
        int number = Item.NewItem(x * 16, y * 16, 32, 32, 1, 1, false, 0, false, false);
        Main.item[number].netDefaults(netid);
        Main.item[number].Prefix(prefix);
        Main.item[number].stack = stack;
        NetMessage.SendData(21, -1, -1, (NetworkText) null, number, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      }
      else
      {
        TEWeaponsRack teWeaponsRack = (TEWeaponsRack) TileEntity.ByID[index];
        if (teWeaponsRack.item.stack > 0)
          teWeaponsRack.DropItem();
        teWeaponsRack.item = new Item();
        teWeaponsRack.item.netDefaults(netid);
        teWeaponsRack.item.Prefix(prefix);
        teWeaponsRack.item.stack = stack;
        NetMessage.SendData(86, -1, -1, (NetworkText) null, teWeaponsRack.ID, (float) x, (float) y, 0.0f, 0, 0, 0);
      }
    }

    public static void OnPlayerInteraction(Player player, int clickX, int clickY)
    {
      if (TEWeaponsRack.FitsWeaponFrame(player.inventory[player.selectedItem]) && !player.inventory[player.selectedItem].favorited)
      {
        player.GamepadEnableGrappleCooldown();
        TEWeaponsRack.PlaceItemInFrame(player, clickX, clickY);
        Recipe.FindRecipes(false);
      }
      else
      {
        int index1 = clickX;
        int index2 = clickY;
        int index3 = index1 - (int) Main.tile[index1, index2].frameX % 54 / 18;
        int num = index2 - (int) Main.tile[index3, index2].frameY % 54 / 18;
        int index4 = TEWeaponsRack.Find(index3, num);
        if (index4 == -1 || ((TEWeaponsRack) TileEntity.ByID[index4]).item.stack <= 0)
          return;
        player.GamepadEnableGrappleCooldown();
        WorldGen.KillTile(index3, num, true, false, false);
        if (Main.netMode != 1)
          return;
        NetMessage.SendData(17, -1, -1, (NetworkText) null, 0, (float) index3, (float) num, 1f, 0, 0, 0);
      }
    }

    public static bool FitsWeaponFrame(Item i)
    {
      if (!i.IsAir && (i.fishingPole > 0 || ItemID.Sets.CanBePlacedOnWeaponRacks[i.type]))
        return true;
      return i.damage > 0 && i.useStyle != 0 && i.stack > 0;
    }

    private static void PlaceItemInFrame(Player player, int x, int y)
    {
      x -= (int) Main.tile[x, y].frameX % 54 / 18;
      y -= (int) Main.tile[x, y].frameY % 54 / 18;
      int index = TEWeaponsRack.Find(x, y);
      if (index == -1)
        return;
      if (((TEWeaponsRack) TileEntity.ByID[index]).item.stack > 0)
      {
        WorldGen.KillTile(x, y, true, false, false);
        if (Main.netMode == 1)
          NetMessage.SendData(17, -1, -1, (NetworkText) null, 0, (float) Player.tileTargetX, (float) y, 1f, 0, 0, 0);
      }
      if (Main.netMode == 1)
        NetMessage.SendData(123, -1, -1, (NetworkText) null, x, (float) y, (float) player.selectedItem, (float) player.whoAmI, 1, 0, 0);
      else
        TEWeaponsRack.TryPlacing(x, y, player.inventory[player.selectedItem].netID, (int) player.inventory[player.selectedItem].prefix, 1);
      --player.inventory[player.selectedItem].stack;
      if (player.inventory[player.selectedItem].stack <= 0)
      {
        player.inventory[player.selectedItem].SetDefaults(0);
        Main.mouseItem.SetDefaults(0);
      }
      if (player.selectedItem == 58)
        Main.mouseItem = player.inventory[player.selectedItem].Clone();
      player.releaseUseItem = false;
      player.mouseInterface = true;
      WorldGen.RangeFrame(x, y, x + 3, y + 3);
    }

    public static bool KillTileDropItem(Tile tileCache, int i, int j)
    {
      int index = TEWeaponsRack.Find(i - (int) tileCache.frameX % 54 / 18, j - (int) tileCache.frameY % 54 / 18);
      if (index == -1 || ((TEWeaponsRack) TileEntity.ByID[index]).item.stack <= 0)
        return false;
      ((TEWeaponsRack) TileEntity.ByID[index]).DropItem();
      if (Main.netMode != 2)
        Main.LocalPlayer.InterruptItemUsageIfOverTile(471);
      return true;
    }
  }
}
