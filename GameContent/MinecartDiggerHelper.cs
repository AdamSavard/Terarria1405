// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.MinecartDiggerHelper
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.GameContent
{
  public class MinecartDiggerHelper
  {
    public static MinecartDiggerHelper Instance = new MinecartDiggerHelper();

    public void TryDigging(
      Player player,
      Vector2 trackWorldPosition,
      int digDirectionX,
      int digDirectionY)
    {
      digDirectionY = 0;
      Point tileCoordinates = trackWorldPosition.ToTileCoordinates();
      if (Framing.GetTileSafely(tileCoordinates).type != (ushort) 314 || (double) tileCoordinates.Y < Main.worldSurface)
        return;
      Point point1 = tileCoordinates;
      point1.X += digDirectionX;
      point1.Y += digDirectionY;
      if (this.AlreadyLeadsIntoWantedTrack(tileCoordinates, point1) || digDirectionY == 0 && (this.AlreadyLeadsIntoWantedTrack(tileCoordinates, new Point(point1.X, point1.Y - 1)) || this.AlreadyLeadsIntoWantedTrack(tileCoordinates, new Point(point1.X, point1.Y + 1))))
        return;
      int num = 5;
      if (digDirectionY != 0)
        num = 5;
      Point point2 = point1;
      Point point3 = point2;
      point3.Y -= num - 1;
      int x1 = point3.X;
      for (int y = point3.Y; y <= point2.Y; ++y)
      {
        if (!this.CanGetPastTile(x1, y) || !this.HasPickPower(player, x1, y))
          return;
      }
      if (!this.CanConsumeATrackItem(player))
        return;
      int x2 = point3.X;
      for (int y = point3.Y; y <= point2.Y; ++y)
        this.MineTheTileIfNecessary(x2, y);
      this.ConsumeATrackItem(player);
      this.PlaceATrack(point1.X, point1.Y);
      player.velocity.X = MathHelper.Clamp(player.velocity.X, -1f, 1f);
      if (this.DoTheTracksConnectProperly(tileCoordinates, point1))
        return;
      this.CorrectTrackConnections(tileCoordinates, point1);
    }

    private bool CanConsumeATrackItem(Player player)
    {
      return this.FindMinecartTrackItem(player) != null;
    }

    private void ConsumeATrackItem(Player player)
    {
      Item minecartTrackItem = this.FindMinecartTrackItem(player);
      --minecartTrackItem.stack;
      if (minecartTrackItem.stack != 0)
        return;
      minecartTrackItem.TurnToAir();
    }

    private Item FindMinecartTrackItem(Player player)
    {
      Item obj1 = (Item) null;
      for (int index = 0; index < 58; ++index)
      {
        if (player.selectedItem != index || player.itemAnimation <= 0 && player.reuseDelay <= 0 && player.itemTime <= 0)
        {
          Item obj2 = player.inventory[index];
          if (obj2.type == 2340 && obj2.stack > 0)
          {
            obj1 = obj2;
            break;
          }
        }
      }
      return obj1;
    }

    private void PoundTrack(Point spot)
    {
      if (Main.tile[spot.X, spot.Y].type != (ushort) 314 || !Minecart.FrameTrack(spot.X, spot.Y, true, false) || Main.netMode != 1)
        return;
      NetMessage.SendData(17, -1, -1, (NetworkText) null, 15, (float) spot.X, (float) spot.Y, 1f, 0, 0, 0);
    }

    private bool AlreadyLeadsIntoWantedTrack(
      Point tileCoordsOfFrontWheel,
      Point tileCoordsWeWantToReach)
    {
      Tile tileSafely1 = Framing.GetTileSafely(tileCoordsOfFrontWheel);
      Tile tileSafely2 = Framing.GetTileSafely(tileCoordsWeWantToReach);
      if (!tileSafely1.active() || tileSafely1.type != (ushort) 314 || (!tileSafely2.active() || tileSafely2.type != (ushort) 314))
        return false;
      int? expectedStartLeft;
      int? expectedStartRight;
      int? expectedEndLeft;
      int? expectedEndRight;
      MinecartDiggerHelper.GetExpectedDirections(tileCoordsOfFrontWheel, tileCoordsWeWantToReach, out expectedStartLeft, out expectedStartRight, out expectedEndLeft, out expectedEndRight);
      return Minecart.GetAreExpectationsForSidesMet(tileCoordsOfFrontWheel, expectedStartLeft, expectedStartRight) && Minecart.GetAreExpectationsForSidesMet(tileCoordsWeWantToReach, expectedEndLeft, expectedEndRight);
    }

    private static void GetExpectedDirections(
      Point startCoords,
      Point endCoords,
      out int? expectedStartLeft,
      out int? expectedStartRight,
      out int? expectedEndLeft,
      out int? expectedEndRight)
    {
      int num1 = endCoords.Y - startCoords.Y;
      int num2 = endCoords.X - startCoords.X;
      expectedStartLeft = new int?();
      expectedStartRight = new int?();
      expectedEndLeft = new int?();
      expectedEndRight = new int?();
      if (num2 == -1)
      {
        expectedStartLeft = new int?(num1);
        expectedEndRight = new int?(-num1);
      }
      if (num2 != 1)
        return;
      expectedStartRight = new int?(num1);
      expectedEndLeft = new int?(-num1);
    }

    private bool DoTheTracksConnectProperly(
      Point tileCoordsOfFrontWheel,
      Point tileCoordsWeWantToReach)
    {
      return this.AlreadyLeadsIntoWantedTrack(tileCoordsOfFrontWheel, tileCoordsWeWantToReach);
    }

    private void CorrectTrackConnections(Point startCoords, Point endCoords)
    {
      int? expectedStartLeft;
      int? expectedStartRight;
      int? expectedEndLeft;
      int? expectedEndRight;
      MinecartDiggerHelper.GetExpectedDirections(startCoords, endCoords, out expectedStartLeft, out expectedStartRight, out expectedEndLeft, out expectedEndRight);
      Tile tileSafely1 = Framing.GetTileSafely(startCoords);
      Tile tileSafely2 = Framing.GetTileSafely(endCoords);
      if (tileSafely1.active() && tileSafely1.type == (ushort) 314)
        Minecart.TryFittingTileOrientation(startCoords, expectedStartLeft, expectedStartRight);
      if (!tileSafely2.active() || tileSafely2.type != (ushort) 314)
        return;
      Minecart.TryFittingTileOrientation(endCoords, expectedEndLeft, expectedEndRight);
    }

    private bool HasPickPower(Player player, int x, int y)
    {
      return player.HasEnoughPickPowerToHurtTile(x, y);
    }

    private bool CanGetPastTile(int x, int y)
    {
      if (WorldGen.CheckTileBreakability(x, y) != 0)
        return false;
      Tile tile = Main.tile[x, y];
      return (!tile.active() || !TileID.Sets.Falling[(int) tile.type]) && (!tile.active() || WorldGen.CanKillTile(x, y));
    }

    private void PlaceATrack(int x, int y)
    {
      int Type = 314;
      int num = 0;
      if (!WorldGen.PlaceTile(x, y, Type, false, false, Main.myPlayer, num))
        return;
      NetMessage.SendData(17, -1, -1, (NetworkText) null, 1, (float) x, (float) y, (float) Type, num, 0, 0);
    }

    private void MineTheTileIfNecessary(int x, int y)
    {
      AchievementsHelper.CurrentlyMining = true;
      if (Main.tile[x, y].active())
      {
        WorldGen.KillTile(x, y, false, false, false);
        NetMessage.SendData(17, -1, -1, (NetworkText) null, 0, (float) x, (float) y, 0.0f, 0, 0, 0);
      }
      AchievementsHelper.CurrentlyMining = false;
    }
  }
}
