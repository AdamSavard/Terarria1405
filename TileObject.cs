// Decompiled with JetBrains decompiler
// Type: Terraria.TileObject
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ObjectData;

namespace Terraria
{
  public struct TileObject
  {
    public static TileObject Empty = new TileObject();
    public static TileObjectPreviewData objectPreview = new TileObjectPreviewData();
    public int xCoord;
    public int yCoord;
    public int type;
    public int style;
    public int alternate;
    public int random;

    public static bool Place(TileObject toBePlaced)
    {
      TileObjectData tileData = TileObjectData.GetTileData(toBePlaced.type, toBePlaced.style, toBePlaced.alternate);
      if (tileData == null)
        return false;
      if (tileData.HookPlaceOverride.hook != null)
      {
        int num1;
        int num2;
        if (tileData.HookPlaceOverride.processedCoordinates)
        {
          num1 = toBePlaced.xCoord;
          num2 = toBePlaced.yCoord;
        }
        else
        {
          num1 = toBePlaced.xCoord + (int) tileData.Origin.X;
          num2 = toBePlaced.yCoord + (int) tileData.Origin.Y;
        }
        if (tileData.HookPlaceOverride.hook(num1, num2, toBePlaced.type, toBePlaced.style, 1, toBePlaced.alternate) == tileData.HookPlaceOverride.badReturn)
          return false;
      }
      else
      {
        ushort type = (ushort) toBePlaced.type;
        int placementStyle = tileData.CalculatePlacementStyle(toBePlaced.style, toBePlaced.alternate, toBePlaced.random);
        int num1 = 0;
        if (tileData.StyleWrapLimit > 0)
        {
          num1 = placementStyle / tileData.StyleWrapLimit * tileData.StyleLineSkip;
          placementStyle %= tileData.StyleWrapLimit;
        }
        int num2;
        int num3;
        if (tileData.StyleHorizontal)
        {
          num2 = tileData.CoordinateFullWidth * placementStyle;
          num3 = tileData.CoordinateFullHeight * num1;
        }
        else
        {
          num2 = tileData.CoordinateFullWidth * num1;
          num3 = tileData.CoordinateFullHeight * placementStyle;
        }
        int xCoord = toBePlaced.xCoord;
        int yCoord = toBePlaced.yCoord;
        for (int index1 = 0; index1 < tileData.Width; ++index1)
        {
          for (int index2 = 0; index2 < tileData.Height; ++index2)
          {
            Tile tileSafely = Framing.GetTileSafely(xCoord + index1, yCoord + index2);
            if (tileSafely.active() && tileSafely.type != (ushort) 484 && (Main.tileCut[(int) tileSafely.type] || TileID.Sets.BreakableWhenPlacing[(int) tileSafely.type]))
            {
              WorldGen.KillTile(xCoord + index1, yCoord + index2, false, false, false);
              if (!Main.tile[xCoord + index1, yCoord + index2].active() && Main.netMode == 1)
                NetMessage.SendData(17, -1, -1, (NetworkText) null, 0, (float) (yCoord + index1), (float) (yCoord + index2), 0.0f, 0, 0, 0);
            }
          }
        }
        for (int index1 = 0; index1 < tileData.Width; ++index1)
        {
          int num4 = num2 + index1 * (tileData.CoordinateWidth + tileData.CoordinatePadding);
          int num5 = num3;
          for (int index2 = 0; index2 < tileData.Height; ++index2)
          {
            Tile tileSafely = Framing.GetTileSafely(xCoord + index1, yCoord + index2);
            if (!tileSafely.active())
            {
              tileSafely.active(true);
              tileSafely.frameX = (short) num4;
              tileSafely.frameY = (short) num5;
              tileSafely.type = type;
            }
            num5 += tileData.CoordinateHeights[index2] + tileData.CoordinatePadding;
          }
        }
      }
      if (tileData.FlattenAnchors)
      {
        AnchorData anchorBottom = tileData.AnchorBottom;
        if (anchorBottom.tileCount != 0 && (anchorBottom.type & AnchorType.SolidTile) == AnchorType.SolidTile)
        {
          int num = toBePlaced.xCoord + anchorBottom.checkStart;
          int j = toBePlaced.yCoord + tileData.Height;
          for (int index = 0; index < anchorBottom.tileCount; ++index)
          {
            Tile tileSafely = Framing.GetTileSafely(num + index, j);
            if (Main.tileSolid[(int) tileSafely.type] && !Main.tileSolidTop[(int) tileSafely.type] && tileSafely.blockType() != 0)
              WorldGen.SlopeTile(num + index, j, 0, false);
          }
        }
        AnchorData anchorTop = tileData.AnchorTop;
        if (anchorTop.tileCount != 0 && (anchorTop.type & AnchorType.SolidTile) == AnchorType.SolidTile)
        {
          int num = toBePlaced.xCoord + anchorTop.checkStart;
          int j = toBePlaced.yCoord - 1;
          for (int index = 0; index < anchorTop.tileCount; ++index)
          {
            Tile tileSafely = Framing.GetTileSafely(num + index, j);
            if (Main.tileSolid[(int) tileSafely.type] && !Main.tileSolidTop[(int) tileSafely.type] && tileSafely.blockType() != 0)
              WorldGen.SlopeTile(num + index, j, 0, false);
          }
        }
        AnchorData anchorRight = tileData.AnchorRight;
        if (anchorRight.tileCount != 0 && (anchorRight.type & AnchorType.SolidTile) == AnchorType.SolidTile)
        {
          int i = toBePlaced.xCoord + tileData.Width;
          int num = toBePlaced.yCoord + anchorRight.checkStart;
          for (int index = 0; index < anchorRight.tileCount; ++index)
          {
            Tile tileSafely = Framing.GetTileSafely(i, num + index);
            if (Main.tileSolid[(int) tileSafely.type] && !Main.tileSolidTop[(int) tileSafely.type] && tileSafely.blockType() != 0)
              WorldGen.SlopeTile(i, num + index, 0, false);
          }
        }
        AnchorData anchorLeft = tileData.AnchorLeft;
        if (anchorLeft.tileCount != 0 && (anchorLeft.type & AnchorType.SolidTile) == AnchorType.SolidTile)
        {
          int i = toBePlaced.xCoord - 1;
          int num = toBePlaced.yCoord + anchorLeft.checkStart;
          for (int index = 0; index < anchorLeft.tileCount; ++index)
          {
            Tile tileSafely = Framing.GetTileSafely(i, num + index);
            if (Main.tileSolid[(int) tileSafely.type] && !Main.tileSolidTop[(int) tileSafely.type] && tileSafely.blockType() != 0)
              WorldGen.SlopeTile(i, num + index, 0, false);
          }
        }
      }
      return true;
    }

    public static bool CanPlace(
      int x,
      int y,
      int type,
      int style,
      int dir,
      out TileObject objectData,
      bool onlyCheck = false)
    {
      TileObjectData tileData1 = TileObjectData.GetTileData(type, style, 0);
      objectData = TileObject.Empty;
      if (tileData1 == null)
        return false;
      int num1 = x - (int) tileData1.Origin.X;
      int num2 = y - (int) tileData1.Origin.Y;
      if (num1 < 0 || num1 + tileData1.Width >= Main.maxTilesX || (num2 < 0 || num2 + tileData1.Height >= Main.maxTilesY))
        return false;
      bool flag1 = tileData1.RandomStyleRange > 0;
      if (TileObjectPreviewData.placementCache == null)
        TileObjectPreviewData.placementCache = new TileObjectPreviewData();
      TileObjectPreviewData.placementCache.Reset();
      int num3 = 0;
      if (tileData1.AlternatesCount != 0)
        num3 = tileData1.AlternatesCount;
      float num4 = -1f;
      float num5 = -1f;
      int num6 = 0;
      TileObjectData tileObjectData = (TileObjectData) null;
      int alternate = 0 - 1;
      while (alternate < num3)
      {
        ++alternate;
        TileObjectData tileData2 = TileObjectData.GetTileData(type, style, alternate);
        if (tileData2.Direction == TileObjectDirection.None || (tileData2.Direction != TileObjectDirection.PlaceLeft || dir != 1) && (tileData2.Direction != TileObjectDirection.PlaceRight || dir != -1))
        {
          int num7 = x - (int) tileData2.Origin.X;
          int num8 = y - (int) tileData2.Origin.Y;
          if (num7 < 5 || num7 + tileData2.Width > Main.maxTilesX - 5 || (num8 < 5 || num8 + tileData2.Height > Main.maxTilesY - 5))
            return false;
          Rectangle rectangle = new Rectangle(0, 0, tileData2.Width, tileData2.Height);
          int X = 0;
          int Y = 0;
          if (tileData2.AnchorTop.tileCount != 0)
          {
            if (rectangle.Y == 0)
            {
              rectangle.Y = -1;
              ++rectangle.Height;
              ++Y;
            }
            int checkStart = tileData2.AnchorTop.checkStart;
            if (checkStart < rectangle.X)
            {
              rectangle.Width += rectangle.X - checkStart;
              X += rectangle.X - checkStart;
              rectangle.X = checkStart;
            }
            int num9 = checkStart + tileData2.AnchorTop.tileCount - 1;
            int num10 = rectangle.X + rectangle.Width - 1;
            if (num9 > num10)
              rectangle.Width += num9 - num10;
          }
          if (tileData2.AnchorBottom.tileCount != 0)
          {
            if (rectangle.Y + rectangle.Height == tileData2.Height)
              ++rectangle.Height;
            int checkStart = tileData2.AnchorBottom.checkStart;
            if (checkStart < rectangle.X)
            {
              rectangle.Width += rectangle.X - checkStart;
              X += rectangle.X - checkStart;
              rectangle.X = checkStart;
            }
            int num9 = checkStart + tileData2.AnchorBottom.tileCount - 1;
            int num10 = rectangle.X + rectangle.Width - 1;
            if (num9 > num10)
              rectangle.Width += num9 - num10;
          }
          if (tileData2.AnchorLeft.tileCount != 0)
          {
            if (rectangle.X == 0)
            {
              rectangle.X = -1;
              ++rectangle.Width;
              ++X;
            }
            int checkStart = tileData2.AnchorLeft.checkStart;
            if ((tileData2.AnchorLeft.type & AnchorType.Tree) == AnchorType.Tree)
              --checkStart;
            if (checkStart < rectangle.Y)
            {
              rectangle.Width += rectangle.Y - checkStart;
              Y += rectangle.Y - checkStart;
              rectangle.Y = checkStart;
            }
            int num9 = checkStart + tileData2.AnchorLeft.tileCount - 1;
            if ((tileData2.AnchorLeft.type & AnchorType.Tree) == AnchorType.Tree)
              num9 += 2;
            int num10 = rectangle.Y + rectangle.Height - 1;
            if (num9 > num10)
              rectangle.Height += num9 - num10;
          }
          if (tileData2.AnchorRight.tileCount != 0)
          {
            if (rectangle.X + rectangle.Width == tileData2.Width)
              ++rectangle.Width;
            int checkStart = tileData2.AnchorLeft.checkStart;
            if ((tileData2.AnchorRight.type & AnchorType.Tree) == AnchorType.Tree)
              --checkStart;
            if (checkStart < rectangle.Y)
            {
              rectangle.Width += rectangle.Y - checkStart;
              Y += rectangle.Y - checkStart;
              rectangle.Y = checkStart;
            }
            int num9 = checkStart + tileData2.AnchorRight.tileCount - 1;
            if ((tileData2.AnchorRight.type & AnchorType.Tree) == AnchorType.Tree)
              num9 += 2;
            int num10 = rectangle.Y + rectangle.Height - 1;
            if (num9 > num10)
              rectangle.Height += num9 - num10;
          }
          if (onlyCheck)
          {
            TileObject.objectPreview.Reset();
            TileObject.objectPreview.Active = true;
            TileObject.objectPreview.Type = (ushort) type;
            TileObject.objectPreview.Style = (short) style;
            TileObject.objectPreview.Alternate = alternate;
            TileObject.objectPreview.Size = new Point16(rectangle.Width, rectangle.Height);
            TileObject.objectPreview.ObjectStart = new Point16(X, Y);
            TileObject.objectPreview.Coordinates = new Point16(num7 - X, num8 - Y);
          }
          float num11 = 0.0f;
          float num12 = (float) (tileData2.Width * tileData2.Height);
          float num13 = 0.0f;
          float num14 = 0.0f;
          for (int index1 = 0; index1 < tileData2.Width; ++index1)
          {
            for (int index2 = 0; index2 < tileData2.Height; ++index2)
            {
              Tile tileSafely = Framing.GetTileSafely(num7 + index1, num8 + index2);
              bool flag2 = !tileData2.LiquidPlace(tileSafely);
              bool flag3 = false;
              if (tileData2.AnchorWall)
              {
                ++num14;
                if (!tileData2.isValidWallAnchor((int) tileSafely.wall))
                  flag3 = true;
                else
                  ++num13;
              }
              bool flag4 = false;
              if (tileSafely.active() && (!Main.tileCut[(int) tileSafely.type] || tileSafely.type == (ushort) 484) && !TileID.Sets.BreakableWhenPlacing[(int) tileSafely.type])
                flag4 = true;
              if (flag4 | flag2 | flag3)
              {
                if (onlyCheck)
                  TileObject.objectPreview[index1 + X, index2 + Y] = 2;
              }
              else
              {
                if (onlyCheck)
                  TileObject.objectPreview[index1 + X, index2 + Y] = 1;
                ++num11;
              }
            }
          }
          AnchorData anchorBottom = tileData2.AnchorBottom;
          if (anchorBottom.tileCount != 0)
          {
            num14 += (float) anchorBottom.tileCount;
            int height = tileData2.Height;
            for (int index = 0; index < anchorBottom.tileCount; ++index)
            {
              int num9 = anchorBottom.checkStart + index;
              Tile tileSafely = Framing.GetTileSafely(num7 + num9, num8 + height);
              bool flag2 = false;
              if (tileSafely.nactive())
              {
                if ((anchorBottom.type & AnchorType.SolidTile) == AnchorType.SolidTile && Main.tileSolid[(int) tileSafely.type] && (!Main.tileSolidTop[(int) tileSafely.type] && !Main.tileNoAttach[(int) tileSafely.type]) && (tileData2.FlattenAnchors || tileSafely.blockType() == 0))
                  flag2 = tileData2.isValidTileAnchor((int) tileSafely.type);
                if (!flag2 && ((anchorBottom.type & AnchorType.SolidWithTop) == AnchorType.SolidWithTop || (anchorBottom.type & AnchorType.Table) == AnchorType.Table))
                {
                  if (TileID.Sets.Platforms[(int) tileSafely.type])
                  {
                    int num10 = (int) tileSafely.frameX / TileObjectData.PlatformFrameWidth();
                    if (!tileSafely.halfBrick() && WorldGen.PlatformProperTopFrame(tileSafely.frameX))
                      flag2 = true;
                  }
                  else if (Main.tileSolid[(int) tileSafely.type] && Main.tileSolidTop[(int) tileSafely.type])
                    flag2 = true;
                }
                if (!flag2 && (anchorBottom.type & AnchorType.Table) == AnchorType.Table && (!TileID.Sets.Platforms[(int) tileSafely.type] && Main.tileTable[(int) tileSafely.type]) && tileSafely.blockType() == 0)
                  flag2 = true;
                if (!flag2 && (anchorBottom.type & AnchorType.SolidSide) == AnchorType.SolidSide && (Main.tileSolid[(int) tileSafely.type] && !Main.tileSolidTop[(int) tileSafely.type]))
                {
                  switch (tileSafely.blockType())
                  {
                    case 4:
                    case 5:
                      flag2 = tileData2.isValidTileAnchor((int) tileSafely.type);
                      break;
                  }
                }
                if (!flag2 && (anchorBottom.type & AnchorType.AlternateTile) == AnchorType.AlternateTile && tileData2.isValidAlternateAnchor((int) tileSafely.type))
                  flag2 = true;
              }
              else if (!flag2 && (anchorBottom.type & AnchorType.EmptyTile) == AnchorType.EmptyTile)
                flag2 = true;
              if (!flag2)
              {
                if (onlyCheck)
                  TileObject.objectPreview[num9 + X, height + Y] = 2;
              }
              else
              {
                if (onlyCheck)
                  TileObject.objectPreview[num9 + X, height + Y] = 1;
                ++num13;
              }
            }
          }
          AnchorData anchorTop = tileData2.AnchorTop;
          if (anchorTop.tileCount != 0)
          {
            num14 += (float) anchorTop.tileCount;
            int num9 = -1;
            for (int index = 0; index < anchorTop.tileCount; ++index)
            {
              int num10 = anchorTop.checkStart + index;
              Tile tileSafely = Framing.GetTileSafely(num7 + num10, num8 + num9);
              bool flag2 = false;
              if (tileSafely.nactive())
              {
                if (Main.tileSolid[(int) tileSafely.type] && !Main.tileSolidTop[(int) tileSafely.type] && !Main.tileNoAttach[(int) tileSafely.type] && (tileData2.FlattenAnchors || tileSafely.blockType() == 0))
                  flag2 = tileData2.isValidTileAnchor((int) tileSafely.type);
                if (!flag2 && (anchorTop.type & AnchorType.SolidBottom) == AnchorType.SolidBottom && (Main.tileSolid[(int) tileSafely.type] && (!Main.tileSolidTop[(int) tileSafely.type] || TileID.Sets.Platforms[(int) tileSafely.type] && (tileSafely.halfBrick() || tileSafely.topSlope())) || (tileSafely.halfBrick() || tileSafely.topSlope())) && (!TileID.Sets.NotReallySolid[(int) tileSafely.type] && !tileSafely.bottomSlope()))
                  flag2 = tileData2.isValidTileAnchor((int) tileSafely.type);
                if (!flag2 && (anchorTop.type & AnchorType.SolidSide) == AnchorType.SolidSide && (Main.tileSolid[(int) tileSafely.type] && !Main.tileSolidTop[(int) tileSafely.type]))
                {
                  switch (tileSafely.blockType())
                  {
                    case 2:
                    case 3:
                      flag2 = tileData2.isValidTileAnchor((int) tileSafely.type);
                      break;
                  }
                }
                if (!flag2 && (anchorTop.type & AnchorType.AlternateTile) == AnchorType.AlternateTile && tileData2.isValidAlternateAnchor((int) tileSafely.type))
                  flag2 = true;
              }
              else if (!flag2 && (anchorTop.type & AnchorType.EmptyTile) == AnchorType.EmptyTile)
                flag2 = true;
              if (!flag2)
              {
                if (onlyCheck)
                  TileObject.objectPreview[num10 + X, num9 + Y] = 2;
              }
              else
              {
                if (onlyCheck)
                  TileObject.objectPreview[num10 + X, num9 + Y] = 1;
                ++num13;
              }
            }
          }
          AnchorData anchorRight = tileData2.AnchorRight;
          if (anchorRight.tileCount != 0)
          {
            num14 += (float) anchorRight.tileCount;
            int width = tileData2.Width;
            for (int index = 0; index < anchorRight.tileCount; ++index)
            {
              int num9 = anchorRight.checkStart + index;
              Tile tileSafely1 = Framing.GetTileSafely(num7 + width, num8 + num9);
              bool flag2 = false;
              if (tileSafely1.nactive())
              {
                if (Main.tileSolid[(int) tileSafely1.type] && !Main.tileSolidTop[(int) tileSafely1.type] && !Main.tileNoAttach[(int) tileSafely1.type] && (tileData2.FlattenAnchors || tileSafely1.blockType() == 0))
                  flag2 = tileData2.isValidTileAnchor((int) tileSafely1.type);
                if (!flag2 && (anchorRight.type & AnchorType.SolidSide) == AnchorType.SolidSide && (Main.tileSolid[(int) tileSafely1.type] && !Main.tileSolidTop[(int) tileSafely1.type]))
                {
                  switch (tileSafely1.blockType())
                  {
                    case 2:
                    case 4:
                      flag2 = tileData2.isValidTileAnchor((int) tileSafely1.type);
                      break;
                  }
                }
                if (!flag2 && (anchorRight.type & AnchorType.Tree) == AnchorType.Tree && TileID.Sets.IsATreeTrunk[(int) tileSafely1.type])
                {
                  flag2 = true;
                  if (index == 0)
                  {
                    ++num14;
                    Tile tileSafely2 = Framing.GetTileSafely(num7 + width, num8 + num9 - 1);
                    if (tileSafely2.nactive() && TileID.Sets.IsATreeTrunk[(int) tileSafely2.type])
                    {
                      ++num13;
                      if (onlyCheck)
                        TileObject.objectPreview[width + X, num9 + Y - 1] = 1;
                    }
                    else if (onlyCheck)
                      TileObject.objectPreview[width + X, num9 + Y - 1] = 2;
                  }
                  if (index == anchorRight.tileCount - 1)
                  {
                    ++num14;
                    Tile tileSafely2 = Framing.GetTileSafely(num7 + width, num8 + num9 + 1);
                    if (tileSafely2.nactive() && TileID.Sets.IsATreeTrunk[(int) tileSafely2.type])
                    {
                      ++num13;
                      if (onlyCheck)
                        TileObject.objectPreview[width + X, num9 + Y + 1] = 1;
                    }
                    else if (onlyCheck)
                      TileObject.objectPreview[width + X, num9 + Y + 1] = 2;
                  }
                }
                if (!flag2 && (anchorRight.type & AnchorType.AlternateTile) == AnchorType.AlternateTile && tileData2.isValidAlternateAnchor((int) tileSafely1.type))
                  flag2 = true;
              }
              else if (!flag2 && (anchorRight.type & AnchorType.EmptyTile) == AnchorType.EmptyTile)
                flag2 = true;
              if (!flag2)
              {
                if (onlyCheck)
                  TileObject.objectPreview[width + X, num9 + Y] = 2;
              }
              else
              {
                if (onlyCheck)
                  TileObject.objectPreview[width + X, num9 + Y] = 1;
                ++num13;
              }
            }
          }
          AnchorData anchorLeft = tileData2.AnchorLeft;
          if (anchorLeft.tileCount != 0)
          {
            num14 += (float) anchorLeft.tileCount;
            int num9 = -1;
            for (int index = 0; index < anchorLeft.tileCount; ++index)
            {
              int num10 = anchorLeft.checkStart + index;
              Tile tileSafely1 = Framing.GetTileSafely(num7 + num9, num8 + num10);
              bool flag2 = false;
              if (tileSafely1.nactive())
              {
                if (Main.tileSolid[(int) tileSafely1.type] && !Main.tileSolidTop[(int) tileSafely1.type] && !Main.tileNoAttach[(int) tileSafely1.type] && (tileData2.FlattenAnchors || tileSafely1.blockType() == 0))
                  flag2 = tileData2.isValidTileAnchor((int) tileSafely1.type);
                if (!flag2 && (anchorLeft.type & AnchorType.SolidSide) == AnchorType.SolidSide && (Main.tileSolid[(int) tileSafely1.type] && !Main.tileSolidTop[(int) tileSafely1.type]))
                {
                  switch (tileSafely1.blockType())
                  {
                    case 3:
                    case 5:
                      flag2 = tileData2.isValidTileAnchor((int) tileSafely1.type);
                      break;
                  }
                }
                if (!flag2 && (anchorLeft.type & AnchorType.Tree) == AnchorType.Tree && TileID.Sets.IsATreeTrunk[(int) tileSafely1.type])
                {
                  flag2 = true;
                  if (index == 0)
                  {
                    ++num14;
                    Tile tileSafely2 = Framing.GetTileSafely(num7 + num9, num8 + num10 - 1);
                    if (tileSafely2.nactive() && TileID.Sets.IsATreeTrunk[(int) tileSafely2.type])
                    {
                      ++num13;
                      if (onlyCheck)
                        TileObject.objectPreview[num9 + X, num10 + Y - 1] = 1;
                    }
                    else if (onlyCheck)
                      TileObject.objectPreview[num9 + X, num10 + Y - 1] = 2;
                  }
                  if (index == anchorLeft.tileCount - 1)
                  {
                    ++num14;
                    Tile tileSafely2 = Framing.GetTileSafely(num7 + num9, num8 + num10 + 1);
                    if (tileSafely2.nactive() && TileID.Sets.IsATreeTrunk[(int) tileSafely2.type])
                    {
                      ++num13;
                      if (onlyCheck)
                        TileObject.objectPreview[num9 + X, num10 + Y + 1] = 1;
                    }
                    else if (onlyCheck)
                      TileObject.objectPreview[num9 + X, num10 + Y + 1] = 2;
                  }
                }
                if (!flag2 && (anchorLeft.type & AnchorType.AlternateTile) == AnchorType.AlternateTile && tileData2.isValidAlternateAnchor((int) tileSafely1.type))
                  flag2 = true;
              }
              else if (!flag2 && (anchorLeft.type & AnchorType.EmptyTile) == AnchorType.EmptyTile)
                flag2 = true;
              if (!flag2)
              {
                if (onlyCheck)
                  TileObject.objectPreview[num9 + X, num10 + Y] = 2;
              }
              else
              {
                if (onlyCheck)
                  TileObject.objectPreview[num9 + X, num10 + Y] = 1;
                ++num13;
              }
            }
          }
          if (tileData2.HookCheckIfCanPlace.hook != null)
          {
            if (tileData2.HookCheckIfCanPlace.processedCoordinates)
            {
              Point16 origin1 = tileData2.Origin;
              Point16 origin2 = tileData2.Origin;
            }
            if (tileData2.HookCheckIfCanPlace.hook(x, y, type, style, dir, alternate) == tileData2.HookCheckIfCanPlace.badReturn && tileData2.HookCheckIfCanPlace.badResponse == 0)
            {
              num13 = 0.0f;
              num11 = 0.0f;
              TileObject.objectPreview.AllInvalid();
            }
          }
          float num15 = num13 / num14;
          float num16 = num11 / num12;
          if ((double) num16 == 1.0 && (double) num14 == 0.0)
          {
            num15 = 1f;
            num16 = 1f;
          }
          if ((double) num15 == 1.0 && (double) num16 == 1.0)
          {
            num4 = 1f;
            num5 = 1f;
            num6 = alternate;
            tileObjectData = tileData2;
            break;
          }
          if ((double) num15 > (double) num4 || (double) num15 == (double) num4 && (double) num16 > (double) num5)
          {
            TileObjectPreviewData.placementCache.CopyFrom(TileObject.objectPreview);
            num4 = num15;
            num5 = num16;
            tileObjectData = tileData2;
            num6 = alternate;
          }
        }
      }
      int num17 = -1;
      if (flag1)
      {
        if (TileObjectPreviewData.randomCache == null)
          TileObjectPreviewData.randomCache = new TileObjectPreviewData();
        bool flag2 = false;
        if ((int) TileObjectPreviewData.randomCache.Type == type)
        {
          Point16 coordinates = TileObjectPreviewData.randomCache.Coordinates;
          Point16 objectStart = TileObjectPreviewData.randomCache.ObjectStart;
          int num7 = (int) coordinates.X + (int) objectStart.X;
          int num8 = (int) coordinates.Y + (int) objectStart.Y;
          int num9 = x - (int) tileData1.Origin.X;
          int num10 = y - (int) tileData1.Origin.Y;
          if (num7 != num9 || num8 != num10)
            flag2 = true;
        }
        else
          flag2 = true;
        num17 = !flag2 ? TileObjectPreviewData.randomCache.Random : Main.rand.Next(tileData1.RandomStyleRange);
      }
      if (onlyCheck)
      {
        if ((double) num4 != 1.0 || (double) num5 != 1.0)
        {
          TileObject.objectPreview.CopyFrom(TileObjectPreviewData.placementCache);
          alternate = num6;
        }
        TileObject.objectPreview.Random = num17;
        if (tileData1.RandomStyleRange > 0)
          TileObjectPreviewData.randomCache.CopyFrom(TileObject.objectPreview);
      }
      if (!onlyCheck)
      {
        objectData.xCoord = x - (int) tileObjectData.Origin.X;
        objectData.yCoord = y - (int) tileObjectData.Origin.Y;
        objectData.type = type;
        objectData.style = style;
        objectData.alternate = alternate;
        objectData.random = num17;
      }
      return (double) num4 == 1.0 && (double) num5 == 1.0;
    }

    public static void DrawPreview(SpriteBatch sb, TileObjectPreviewData op, Vector2 position)
    {
      Point16 coordinates = op.Coordinates;
      Texture2D texture = TextureAssets.Tile[(int) op.Type].Value;
      TileObjectData tileData = TileObjectData.GetTileData((int) op.Type, (int) op.Style, op.Alternate);
      int placementStyle = tileData.CalculatePlacementStyle((int) op.Style, op.Alternate, op.Random);
      int num1 = 0;
      int drawYoffset = tileData.DrawYOffset;
      int drawXoffset = tileData.DrawXOffset;
      int num2 = placementStyle + tileData.DrawStyleOffset;
      int styleWrapLimit = tileData.StyleWrapLimit;
      int styleLineSkip = tileData.StyleLineSkip;
      int? nullable;
      if (tileData.StyleWrapLimitVisualOverride.HasValue)
      {
        nullable = tileData.StyleWrapLimitVisualOverride;
        styleWrapLimit = nullable.Value;
      }
      nullable = tileData.styleLineSkipVisualOverride;
      if (nullable.HasValue)
      {
        nullable = tileData.styleLineSkipVisualOverride;
        styleLineSkip = nullable.Value;
      }
      if (styleWrapLimit > 0)
      {
        num1 = num2 / styleWrapLimit * styleLineSkip;
        num2 %= styleWrapLimit;
      }
      int num3;
      int num4;
      if (tileData.StyleHorizontal)
      {
        num3 = tileData.CoordinateFullWidth * num2;
        num4 = tileData.CoordinateFullHeight * num1;
      }
      else
      {
        num3 = tileData.CoordinateFullWidth * num1;
        num4 = tileData.CoordinateFullHeight * num2;
      }
      for (int index1 = 0; index1 < (int) op.Size.X; ++index1)
      {
        int x = num3 + (index1 - (int) op.ObjectStart.X) * (tileData.CoordinateWidth + tileData.CoordinatePadding);
        int y = num4;
        for (int index2 = 0; index2 < (int) op.Size.Y; ++index2)
        {
          int i = (int) coordinates.X + index1;
          int num5 = (int) coordinates.Y + index2;
          if (index2 == 0 && tileData.DrawStepDown != 0 && WorldGen.SolidTile(Framing.GetTileSafely(i, num5 - 1)))
            drawYoffset += tileData.DrawStepDown;
          Color color1;
          switch (op[index1, index2])
          {
            case 1:
              color1 = Color.White;
              break;
            case 2:
              color1 = Color.Red * 0.7f;
              break;
            default:
              continue;
          }
          Color color2 = color1 * 0.5f;
          if (index1 >= (int) op.ObjectStart.X && index1 < (int) op.ObjectStart.X + tileData.Width && (index2 >= (int) op.ObjectStart.Y && index2 < (int) op.ObjectStart.Y + tileData.Height))
          {
            SpriteEffects effects = SpriteEffects.None;
            if (tileData.DrawFlipHorizontal && i % 2 == 0)
              effects |= SpriteEffects.FlipHorizontally;
            if (tileData.DrawFlipVertical && num5 % 2 == 0)
              effects |= SpriteEffects.FlipVertically;
            Rectangle rectangle = new Rectangle(x, y, tileData.CoordinateWidth, tileData.CoordinateHeights[index2 - (int) op.ObjectStart.Y]);
            sb.Draw(texture, new Vector2((float) (i * 16 - (int) ((double) position.X + (double) (tileData.CoordinateWidth - 16) / 2.0) + drawXoffset), (float) (num5 * 16 - (int) position.Y + drawYoffset)), new Rectangle?(rectangle), color2, 0.0f, Vector2.Zero, 1f, effects, 0.0f);
            y += tileData.CoordinateHeights[index2 - (int) op.ObjectStart.Y] + tileData.CoordinatePadding;
          }
        }
      }
    }
  }
}
