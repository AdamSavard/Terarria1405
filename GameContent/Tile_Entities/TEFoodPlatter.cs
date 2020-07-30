// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Tile_Entities.TEFoodPlatter
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using System.IO;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.GameContent.Tile_Entities
{
  public class TEFoodPlatter : TileEntity
  {
    private static byte _myEntityID;
    public Item item;

    public override void RegisterTileEntityID(int assignedID)
    {
      TEFoodPlatter._myEntityID = (byte) assignedID;
    }

    public override void NetPlaceEntityAttempt(int x, int y)
    {
      TEFoodPlatter.NetPlaceEntity(x, y);
    }

    public static void NetPlaceEntity(int x, int y)
    {
      NetMessage.SendData(86, -1, -1, (NetworkText) null, TEFoodPlatter.Place(x, y), (float) x, (float) y, 0.0f, 0, 0, 0);
    }

    public override TileEntity GenerateInstance()
    {
      return (TileEntity) new TEFoodPlatter();
    }

    public TEFoodPlatter()
    {
      this.item = new Item();
    }

    public static int Place(int x, int y)
    {
      TEFoodPlatter teFoodPlatter = new TEFoodPlatter();
      teFoodPlatter.Position = new Point16(x, y);
      teFoodPlatter.ID = TileEntity.AssignNewID();
      teFoodPlatter.type = TEFoodPlatter._myEntityID;
      TileEntity.ByID[teFoodPlatter.ID] = (TileEntity) teFoodPlatter;
      TileEntity.ByPosition[teFoodPlatter.Position] = (TileEntity) teFoodPlatter;
      return teFoodPlatter.ID;
    }

    public override bool IsTileValidForEntity(int x, int y)
    {
      return TEFoodPlatter.ValidTile(x, y);
    }

    public static int Hook_AfterPlacement(
      int x,
      int y,
      int type = 520,
      int style = 0,
      int direction = 1,
      int alternate = 0)
    {
      if (Main.netMode != 1)
        return TEFoodPlatter.Place(x, y);
      NetMessage.SendTileSquare(Main.myPlayer, x, y, 1, TileChangeType.None);
      NetMessage.SendData(87, -1, -1, (NetworkText) null, x, (float) y, (float) TEFoodPlatter._myEntityID, 0.0f, 0, 0, 0);
      return -1;
    }

    public static void Kill(int x, int y)
    {
      TileEntity tileEntity;
      if (!TileEntity.ByPosition.TryGetValue(new Point16(x, y), out tileEntity) || (int) tileEntity.type != (int) TEFoodPlatter._myEntityID)
        return;
      TileEntity.ByID.Remove(tileEntity.ID);
      TileEntity.ByPosition.Remove(new Point16(x, y));
    }

    public static int Find(int x, int y)
    {
      TileEntity tileEntity;
      return TileEntity.ByPosition.TryGetValue(new Point16(x, y), out tileEntity) && (int) tileEntity.type == (int) TEFoodPlatter._myEntityID ? tileEntity.ID : -1;
    }

    public static bool ValidTile(int x, int y)
    {
      return Main.tile[x, y].active() && Main.tile[x, y].type == (ushort) 520 && Main.tile[x, y].frameY == (short) 0;
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

    public void DropItem()
    {
      if (Main.netMode != 1)
        Item.NewItem((int) this.Position.X * 16, (int) this.Position.Y * 16, 16, 16, this.item.netID, 1, false, (int) this.item.prefix, false, false);
      this.item = new Item();
    }

    public static void TryPlacing(int x, int y, int netid, int prefix, int stack)
    {
      WorldGen.RangeFrame(x, y, x + 1, y + 1);
      int index = TEFoodPlatter.Find(x, y);
      if (index == -1)
      {
        int number = Item.NewItem(x * 16, y * 16, 16, 16, 1, 1, false, 0, false, false);
        Main.item[number].netDefaults(netid);
        Main.item[number].Prefix(prefix);
        Main.item[number].stack = stack;
        NetMessage.SendData(21, -1, -1, (NetworkText) null, number, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      }
      else
      {
        TEFoodPlatter teFoodPlatter = (TEFoodPlatter) TileEntity.ByID[index];
        if (teFoodPlatter.item.stack > 0)
          teFoodPlatter.DropItem();
        teFoodPlatter.item = new Item();
        teFoodPlatter.item.netDefaults(netid);
        teFoodPlatter.item.Prefix(prefix);
        teFoodPlatter.item.stack = stack;
        NetMessage.SendData(86, -1, -1, (NetworkText) null, teFoodPlatter.ID, (float) x, (float) y, 0.0f, 0, 0, 0);
      }
    }

    public static void OnPlayerInteraction(Player player, int clickX, int clickY)
    {
      if (TEFoodPlatter.FitsFoodPlatter(player.inventory[player.selectedItem]) && !player.inventory[player.selectedItem].favorited)
      {
        player.GamepadEnableGrappleCooldown();
        TEFoodPlatter.PlaceItemInFrame(player, clickX, clickY);
        Recipe.FindRecipes(false);
      }
      else
      {
        int x = clickX;
        int y = clickY;
        int index = TEFoodPlatter.Find(x, y);
        if (index == -1 || ((TEFoodPlatter) TileEntity.ByID[index]).item.stack <= 0)
          return;
        player.GamepadEnableGrappleCooldown();
        WorldGen.KillTile(clickX, clickY, true, false, false);
        if (Main.netMode != 1)
          return;
        NetMessage.SendData(17, -1, -1, (NetworkText) null, 0, (float) x, (float) y, 1f, 0, 0, 0);
      }
    }

    public static bool FitsFoodPlatter(Item i)
    {
      return i.stack > 0 && ItemID.Sets.IsFood[i.type];
    }

    public static void PlaceItemInFrame(Player player, int x, int y)
    {
      int index = TEFoodPlatter.Find(x, y);
      if (index == -1)
        return;
      if (((TEFoodPlatter) TileEntity.ByID[index]).item.stack > 0)
      {
        WorldGen.KillTile(x, y, true, false, false);
        if (Main.netMode == 1)
          NetMessage.SendData(17, -1, -1, (NetworkText) null, 0, (float) Player.tileTargetX, (float) y, 1f, 0, 0, 0);
      }
      if (Main.netMode == 1)
        NetMessage.SendData(133, -1, -1, (NetworkText) null, x, (float) y, (float) player.selectedItem, (float) player.whoAmI, 1, 0, 0);
      else
        TEFoodPlatter.TryPlacing(x, y, player.inventory[player.selectedItem].netID, (int) player.inventory[player.selectedItem].prefix, 1);
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
      WorldGen.RangeFrame(x, y, x + 1, y + 1);
    }
  }
}
