﻿// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Liquid.LiquidRenderer
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.Utilities;

namespace Terraria.GameContent.Liquid
{
  public class LiquidRenderer
  {
    private static readonly int[] WATERFALL_LENGTH = new int[3]
    {
      10,
      3,
      2
    };
    private static readonly float[] DEFAULT_OPACITY = new float[3]
    {
      0.6f,
      0.95f,
      0.95f
    };
    private static readonly byte[] WAVE_MASK_STRENGTH = new byte[5]
    {
      (byte) 0,
      (byte) 0,
      (byte) 0,
      byte.MaxValue,
      (byte) 0
    };
    private static readonly byte[] VISCOSITY_MASK = new byte[5]
    {
      (byte) 0,
      (byte) 200,
      (byte) 240,
      (byte) 0,
      (byte) 0
    };
    private readonly Asset<Texture2D>[] _liquidTextures = new Asset<Texture2D>[13];
    private LiquidRenderer.LiquidCache[] _cache = new LiquidRenderer.LiquidCache[1];
    private LiquidRenderer.LiquidDrawCache[] _drawCache = new LiquidRenderer.LiquidDrawCache[1];
    private Rectangle _drawArea = new Rectangle(0, 0, 1, 1);
    private readonly UnifiedRandom _random = new UnifiedRandom();
    private Color[] _waveMask = new Color[1];
    private const int ANIMATION_FRAME_COUNT = 16;
    private const int CACHE_PADDING = 2;
    private const int CACHE_PADDING_2 = 4;
    public const float MIN_LIQUID_SIZE = 0.25f;
    public static LiquidRenderer Instance;
    private int _animationFrame;
    private float _frameState;

    public event Action<Color[], Rectangle> WaveFilters;

    private static Tile[,] Tiles
    {
      get
      {
        return Main.tile;
      }
    }

    public static void LoadContent()
    {
      LiquidRenderer.Instance = new LiquidRenderer();
      LiquidRenderer.Instance.PrepareAssets();
    }

    private void PrepareAssets()
    {
      for (int index = 0; index < this._liquidTextures.Length; ++index)
        this._liquidTextures[index] = (Asset<Texture2D>) Main.Assets.Request<Texture2D>("Images/Misc/water_" + (object) index, (AssetRequestMode) 1);
    }

    private unsafe void InternalPrepareDraw(Rectangle drawArea)
    {
      Rectangle rectangle = new Rectangle(drawArea.X - 2, drawArea.Y - 2, drawArea.Width + 4, drawArea.Height + 4);
      this._drawArea = drawArea;
      if (this._cache.Length < rectangle.Width * rectangle.Height + 1)
        this._cache = new LiquidRenderer.LiquidCache[rectangle.Width * rectangle.Height + 1];
      if (this._drawCache.Length < drawArea.Width * drawArea.Height + 1)
        this._drawCache = new LiquidRenderer.LiquidDrawCache[drawArea.Width * drawArea.Height + 1];
      if (this._waveMask.Length < drawArea.Width * drawArea.Height)
        this._waveMask = new Color[drawArea.Width * drawArea.Height];
      fixed (LiquidRenderer.LiquidCache* liquidCachePtr1 = &this._cache[1])
      {
        int num1 = rectangle.Height * 2 + 2;
        LiquidRenderer.LiquidCache* liquidCachePtr2 = liquidCachePtr1;
        for (int x = rectangle.X; x < rectangle.X + rectangle.Width; ++x)
        {
          for (int y = rectangle.Y; y < rectangle.Y + rectangle.Height; ++y)
          {
            Tile tile = LiquidRenderer.Tiles[x, y] ?? new Tile();
            liquidCachePtr2->LiquidLevel = (float) tile.liquid / (float) byte.MaxValue;
            liquidCachePtr2->IsHalfBrick = tile.halfBrick() && liquidCachePtr2[-1].HasLiquid && !TileID.Sets.Platforms[(int) tile.type];
            liquidCachePtr2->IsSolid = WorldGen.SolidOrSlopedTile(tile);
            liquidCachePtr2->HasLiquid = tile.liquid > (byte) 0;
            liquidCachePtr2->VisibleLiquidLevel = 0.0f;
            liquidCachePtr2->HasWall = tile.wall > (ushort) 0;
            liquidCachePtr2->Type = tile.liquidType();
            if (liquidCachePtr2->IsHalfBrick && !liquidCachePtr2->HasLiquid)
              liquidCachePtr2->Type = liquidCachePtr2[-1].Type;
            ++liquidCachePtr2;
          }
        }
        LiquidRenderer.LiquidCache* liquidCachePtr3 = liquidCachePtr1 + num1;
        for (int index1 = 2; index1 < rectangle.Width - 2; ++index1)
        {
          for (int index2 = 2; index2 < rectangle.Height - 2; ++index2)
          {
            float val1 = 0.0f;
            float num2;
            if (liquidCachePtr3->IsHalfBrick && liquidCachePtr3[-1].HasLiquid)
              num2 = 1f;
            else if (!liquidCachePtr3->HasLiquid)
            {
              LiquidRenderer.LiquidCache liquidCache1 = liquidCachePtr3[-1];
              LiquidRenderer.LiquidCache liquidCache2 = liquidCachePtr3[1];
              LiquidRenderer.LiquidCache liquidCache3 = liquidCachePtr3[-rectangle.Height];
              LiquidRenderer.LiquidCache liquidCache4 = liquidCachePtr3[rectangle.Height];
              if (liquidCache1.HasLiquid && liquidCache2.HasLiquid && ((int) liquidCache1.Type == (int) liquidCache2.Type && !liquidCache1.IsSolid) && !liquidCache2.IsSolid)
              {
                val1 = liquidCache1.LiquidLevel + liquidCache2.LiquidLevel;
                liquidCachePtr3->Type = liquidCache1.Type;
              }
              if (liquidCache3.HasLiquid && liquidCache4.HasLiquid && ((int) liquidCache3.Type == (int) liquidCache4.Type && !liquidCache3.IsSolid) && !liquidCache4.IsSolid)
              {
                val1 = Math.Max(val1, liquidCache3.LiquidLevel + liquidCache4.LiquidLevel);
                liquidCachePtr3->Type = liquidCache3.Type;
              }
              num2 = val1 * 0.5f;
            }
            else
              num2 = liquidCachePtr3->LiquidLevel;
            liquidCachePtr3->VisibleLiquidLevel = num2;
            liquidCachePtr3->HasVisibleLiquid = (double) num2 != 0.0;
            ++liquidCachePtr3;
          }
          liquidCachePtr3 += 4;
        }
        LiquidRenderer.LiquidCache* liquidCachePtr4 = liquidCachePtr1;
        for (int index1 = 0; index1 < rectangle.Width; ++index1)
        {
          for (int index2 = 0; index2 < rectangle.Height - 10; ++index2)
          {
            if (liquidCachePtr4->HasVisibleLiquid && (!liquidCachePtr4->IsSolid || liquidCachePtr4->IsHalfBrick))
            {
              liquidCachePtr4->Opacity = 1f;
              liquidCachePtr4->VisibleType = liquidCachePtr4->Type;
              float num2 = 1f / (float) (LiquidRenderer.WATERFALL_LENGTH[(int) liquidCachePtr4->Type] + 1);
              float num3 = 1f;
              for (int index3 = 1; index3 <= LiquidRenderer.WATERFALL_LENGTH[(int) liquidCachePtr4->Type]; ++index3)
              {
                num3 -= num2;
                if (!liquidCachePtr4[index3].IsSolid)
                {
                  liquidCachePtr4[index3].VisibleLiquidLevel = Math.Max(liquidCachePtr4[index3].VisibleLiquidLevel, liquidCachePtr4->VisibleLiquidLevel * num3);
                  liquidCachePtr4[index3].Opacity = num3;
                  liquidCachePtr4[index3].VisibleType = liquidCachePtr4->Type;
                }
                else
                  break;
              }
            }
            if (liquidCachePtr4->IsSolid && !liquidCachePtr4->IsHalfBrick)
            {
              liquidCachePtr4->VisibleLiquidLevel = 1f;
              liquidCachePtr4->HasVisibleLiquid = false;
            }
            else
              liquidCachePtr4->HasVisibleLiquid = (double) liquidCachePtr4->VisibleLiquidLevel != 0.0;
            ++liquidCachePtr4;
          }
          liquidCachePtr4 += 10;
        }
        LiquidRenderer.LiquidCache* liquidCachePtr5 = liquidCachePtr1 + num1;
        for (int index1 = 2; index1 < rectangle.Width - 2; ++index1)
        {
          for (int index2 = 2; index2 < rectangle.Height - 2; ++index2)
          {
            if (!liquidCachePtr5->HasVisibleLiquid)
            {
              liquidCachePtr5->HasLeftEdge = false;
              liquidCachePtr5->HasTopEdge = false;
              liquidCachePtr5->HasRightEdge = false;
              liquidCachePtr5->HasBottomEdge = false;
            }
            else
            {
              LiquidRenderer.LiquidCache liquidCache1 = liquidCachePtr5[-1];
              LiquidRenderer.LiquidCache liquidCache2 = liquidCachePtr5[1];
              LiquidRenderer.LiquidCache liquidCache3 = liquidCachePtr5[-rectangle.Height];
              LiquidRenderer.LiquidCache liquidCache4 = liquidCachePtr5[rectangle.Height];
              float num2 = 0.0f;
              float num3 = 1f;
              float num4 = 0.0f;
              float num5 = 1f;
              float visibleLiquidLevel = liquidCachePtr5->VisibleLiquidLevel;
              if (!liquidCache1.HasVisibleLiquid)
                num4 += liquidCache2.VisibleLiquidLevel * (1f - visibleLiquidLevel);
              if (!liquidCache2.HasVisibleLiquid && !liquidCache2.IsSolid && !liquidCache2.IsHalfBrick)
                num5 -= liquidCache1.VisibleLiquidLevel * (1f - visibleLiquidLevel);
              if (!liquidCache3.HasVisibleLiquid && !liquidCache3.IsSolid && !liquidCache3.IsHalfBrick)
                num2 += liquidCache4.VisibleLiquidLevel * (1f - visibleLiquidLevel);
              if (!liquidCache4.HasVisibleLiquid && !liquidCache4.IsSolid && !liquidCache4.IsHalfBrick)
                num3 -= liquidCache3.VisibleLiquidLevel * (1f - visibleLiquidLevel);
              liquidCachePtr5->LeftWall = num2;
              liquidCachePtr5->RightWall = num3;
              liquidCachePtr5->BottomWall = num5;
              liquidCachePtr5->TopWall = num4;
              Point zero = Point.Zero;
              liquidCachePtr5->HasTopEdge = !liquidCache1.HasVisibleLiquid && !liquidCache1.IsSolid || (double) num4 != 0.0;
              liquidCachePtr5->HasBottomEdge = !liquidCache2.HasVisibleLiquid && !liquidCache2.IsSolid || (double) num5 != 1.0;
              liquidCachePtr5->HasLeftEdge = !liquidCache3.HasVisibleLiquid && !liquidCache3.IsSolid || (double) num2 != 0.0;
              liquidCachePtr5->HasRightEdge = !liquidCache4.HasVisibleLiquid && !liquidCache4.IsSolid || (double) num3 != 1.0;
              if (!liquidCachePtr5->HasLeftEdge)
              {
                if (liquidCachePtr5->HasRightEdge)
                  zero.X += 32;
                else
                  zero.X += 16;
              }
              if (liquidCachePtr5->HasLeftEdge && liquidCachePtr5->HasRightEdge)
              {
                zero.X = 16;
                zero.Y += 32;
                if (liquidCachePtr5->HasTopEdge)
                  zero.Y = 16;
              }
              else if (!liquidCachePtr5->HasTopEdge)
              {
                if (!liquidCachePtr5->HasLeftEdge && !liquidCachePtr5->HasRightEdge)
                  zero.Y += 48;
                else
                  zero.Y += 16;
              }
              if (zero.Y == 16 && liquidCachePtr5->HasLeftEdge ^ liquidCachePtr5->HasRightEdge && (index2 + rectangle.Y) % 2 == 0)
                zero.Y += 16;
              liquidCachePtr5->FrameOffset = zero;
            }
            ++liquidCachePtr5;
          }
          liquidCachePtr5 += 4;
        }
        LiquidRenderer.LiquidCache* liquidCachePtr6 = liquidCachePtr1 + num1;
        for (int index1 = 2; index1 < rectangle.Width - 2; ++index1)
        {
          for (int index2 = 2; index2 < rectangle.Height - 2; ++index2)
          {
            if (liquidCachePtr6->HasVisibleLiquid)
            {
              LiquidRenderer.LiquidCache liquidCache1 = liquidCachePtr6[-1];
              LiquidRenderer.LiquidCache liquidCache2 = liquidCachePtr6[1];
              LiquidRenderer.LiquidCache liquidCache3 = liquidCachePtr6[-rectangle.Height];
              LiquidRenderer.LiquidCache liquidCache4 = liquidCachePtr6[rectangle.Height];
              liquidCachePtr6->VisibleLeftWall = liquidCachePtr6->LeftWall;
              liquidCachePtr6->VisibleRightWall = liquidCachePtr6->RightWall;
              liquidCachePtr6->VisibleTopWall = liquidCachePtr6->TopWall;
              liquidCachePtr6->VisibleBottomWall = liquidCachePtr6->BottomWall;
              if (liquidCache1.HasVisibleLiquid && liquidCache2.HasVisibleLiquid)
              {
                if (liquidCachePtr6->HasLeftEdge)
                  liquidCachePtr6->VisibleLeftWall = (float) (((double) liquidCachePtr6->LeftWall * 2.0 + (double) liquidCache1.LeftWall + (double) liquidCache2.LeftWall) * 0.25);
                if (liquidCachePtr6->HasRightEdge)
                  liquidCachePtr6->VisibleRightWall = (float) (((double) liquidCachePtr6->RightWall * 2.0 + (double) liquidCache1.RightWall + (double) liquidCache2.RightWall) * 0.25);
              }
              if (liquidCache3.HasVisibleLiquid && liquidCache4.HasVisibleLiquid)
              {
                if (liquidCachePtr6->HasTopEdge)
                  liquidCachePtr6->VisibleTopWall = (float) (((double) liquidCachePtr6->TopWall * 2.0 + (double) liquidCache3.TopWall + (double) liquidCache4.TopWall) * 0.25);
                if (liquidCachePtr6->HasBottomEdge)
                  liquidCachePtr6->VisibleBottomWall = (float) (((double) liquidCachePtr6->BottomWall * 2.0 + (double) liquidCache3.BottomWall + (double) liquidCache4.BottomWall) * 0.25);
              }
            }
            ++liquidCachePtr6;
          }
          liquidCachePtr6 += 4;
        }
        LiquidRenderer.LiquidCache* liquidCachePtr7 = liquidCachePtr1 + num1;
        for (int index1 = 2; index1 < rectangle.Width - 2; ++index1)
        {
          for (int index2 = 2; index2 < rectangle.Height - 2; ++index2)
          {
            if (liquidCachePtr7->HasLiquid)
            {
              LiquidRenderer.LiquidCache liquidCache1 = liquidCachePtr7[-1];
              LiquidRenderer.LiquidCache liquidCache2 = liquidCachePtr7[1];
              LiquidRenderer.LiquidCache liquidCache3 = liquidCachePtr7[-rectangle.Height];
              LiquidRenderer.LiquidCache liquidCache4 = liquidCachePtr7[rectangle.Height];
              if (liquidCachePtr7->HasTopEdge && !liquidCachePtr7->HasBottomEdge && liquidCachePtr7->HasLeftEdge ^ liquidCachePtr7->HasRightEdge)
              {
                if (liquidCachePtr7->HasRightEdge)
                {
                  liquidCachePtr7->VisibleRightWall = liquidCache2.VisibleRightWall;
                  liquidCachePtr7->VisibleTopWall = liquidCache3.VisibleTopWall;
                }
                else
                {
                  liquidCachePtr7->VisibleLeftWall = liquidCache2.VisibleLeftWall;
                  liquidCachePtr7->VisibleTopWall = liquidCache4.VisibleTopWall;
                }
              }
              else if (liquidCache2.FrameOffset.X == 16 && liquidCache2.FrameOffset.Y == 32)
              {
                if ((double) liquidCachePtr7->VisibleLeftWall > 0.5)
                {
                  liquidCachePtr7->VisibleLeftWall = 0.0f;
                  liquidCachePtr7->FrameOffset = new Point(0, 0);
                }
                else if ((double) liquidCachePtr7->VisibleRightWall < 0.5)
                {
                  liquidCachePtr7->VisibleRightWall = 1f;
                  liquidCachePtr7->FrameOffset = new Point(32, 0);
                }
              }
            }
            ++liquidCachePtr7;
          }
          liquidCachePtr7 += 4;
        }
        LiquidRenderer.LiquidCache* liquidCachePtr8 = liquidCachePtr1 + num1;
        for (int index1 = 2; index1 < rectangle.Width - 2; ++index1)
        {
          for (int index2 = 2; index2 < rectangle.Height - 2; ++index2)
          {
            if (liquidCachePtr8->HasLiquid)
            {
              LiquidRenderer.LiquidCache liquidCache1 = liquidCachePtr8[-1];
              LiquidRenderer.LiquidCache liquidCache2 = liquidCachePtr8[1];
              LiquidRenderer.LiquidCache liquidCache3 = liquidCachePtr8[-rectangle.Height];
              LiquidRenderer.LiquidCache liquidCache4 = liquidCachePtr8[rectangle.Height];
              if (!liquidCachePtr8->HasBottomEdge && !liquidCachePtr8->HasLeftEdge && (!liquidCachePtr8->HasTopEdge && !liquidCachePtr8->HasRightEdge))
              {
                if (liquidCache3.HasTopEdge && liquidCache1.HasLeftEdge)
                {
                  liquidCachePtr8->FrameOffset.X = Math.Max(4, (int) (16.0 - (double) liquidCache1.VisibleLeftWall * 16.0)) - 4;
                  liquidCachePtr8->FrameOffset.Y = 48 + Math.Max(4, (int) (16.0 - (double) liquidCache3.VisibleTopWall * 16.0)) - 4;
                  liquidCachePtr8->VisibleLeftWall = 0.0f;
                  liquidCachePtr8->VisibleTopWall = 0.0f;
                  liquidCachePtr8->VisibleRightWall = 1f;
                  liquidCachePtr8->VisibleBottomWall = 1f;
                }
                else if (liquidCache4.HasTopEdge && liquidCache1.HasRightEdge)
                {
                  liquidCachePtr8->FrameOffset.X = 32 - Math.Min(16, (int) ((double) liquidCache1.VisibleRightWall * 16.0) - 4);
                  liquidCachePtr8->FrameOffset.Y = 48 + Math.Max(4, (int) (16.0 - (double) liquidCache4.VisibleTopWall * 16.0)) - 4;
                  liquidCachePtr8->VisibleLeftWall = 0.0f;
                  liquidCachePtr8->VisibleTopWall = 0.0f;
                  liquidCachePtr8->VisibleRightWall = 1f;
                  liquidCachePtr8->VisibleBottomWall = 1f;
                }
              }
            }
            ++liquidCachePtr8;
          }
          liquidCachePtr8 += 4;
        }
        LiquidRenderer.LiquidCache* liquidCachePtr9 = liquidCachePtr1 + num1;
        fixed (LiquidRenderer.LiquidDrawCache* liquidDrawCachePtr1 = &this._drawCache[0])
          fixed (Color* colorPtr1 = &this._waveMask[0])
          {
            LiquidRenderer.LiquidDrawCache* liquidDrawCachePtr2 = liquidDrawCachePtr1;
            Color* colorPtr2 = colorPtr1;
            for (int index1 = 2; index1 < rectangle.Width - 2; ++index1)
            {
              for (int index2 = 2; index2 < rectangle.Height - 2; ++index2)
              {
                if (liquidCachePtr9->HasVisibleLiquid)
                {
                  float num2 = Math.Min(0.75f, liquidCachePtr9->VisibleLeftWall);
                  float num3 = Math.Max(0.25f, liquidCachePtr9->VisibleRightWall);
                  float num4 = Math.Min(0.75f, liquidCachePtr9->VisibleTopWall);
                  float num5 = Math.Max(0.25f, liquidCachePtr9->VisibleBottomWall);
                  if (liquidCachePtr9->IsHalfBrick && liquidCachePtr9->IsSolid && (double) num5 > 0.5)
                    num5 = 0.5f;
                  liquidDrawCachePtr2->IsVisible = liquidCachePtr9->HasWall || (!liquidCachePtr9->IsHalfBrick || !liquidCachePtr9->HasLiquid || (double) liquidCachePtr9->LiquidLevel >= 1.0);
                  liquidDrawCachePtr2->SourceRectangle = new Rectangle((int) (16.0 - (double) num3 * 16.0) + liquidCachePtr9->FrameOffset.X, (int) (16.0 - (double) num5 * 16.0) + liquidCachePtr9->FrameOffset.Y, (int) Math.Ceiling(((double) num3 - (double) num2) * 16.0), (int) Math.Ceiling(((double) num5 - (double) num4) * 16.0));
                  liquidDrawCachePtr2->IsSurfaceLiquid = liquidCachePtr9->FrameOffset.X == 16 && liquidCachePtr9->FrameOffset.Y == 0 && (double) (index2 + rectangle.Y) > Main.worldSurface - 40.0;
                  liquidDrawCachePtr2->Opacity = liquidCachePtr9->Opacity;
                  liquidDrawCachePtr2->LiquidOffset = new Vector2((float) Math.Floor((double) num2 * 16.0), (float) Math.Floor((double) num4 * 16.0));
                  liquidDrawCachePtr2->Type = liquidCachePtr9->VisibleType;
                  liquidDrawCachePtr2->HasWall = liquidCachePtr9->HasWall;
                  byte num6 = LiquidRenderer.WAVE_MASK_STRENGTH[(int) liquidCachePtr9->VisibleType];
                  byte num7 = (byte) ((uint) num6 >> 1);
                  colorPtr2->R = num7;
                  colorPtr2->G = num7;
                  colorPtr2->B = LiquidRenderer.VISCOSITY_MASK[(int) liquidCachePtr9->VisibleType];
                  colorPtr2->A = num6;
                  LiquidRenderer.LiquidCache* liquidCachePtr10 = liquidCachePtr9 - 1;
                  if (index2 != 2 && !liquidCachePtr10->HasVisibleLiquid && (!liquidCachePtr10->IsSolid && !liquidCachePtr10->IsHalfBrick))
                    *(colorPtr2 - 1) = *colorPtr2;
                }
                else
                {
                  liquidDrawCachePtr2->IsVisible = false;
                  int index3 = liquidCachePtr9->IsSolid || liquidCachePtr9->IsHalfBrick ? 3 : 4;
                  byte num2 = LiquidRenderer.WAVE_MASK_STRENGTH[index3];
                  byte num3 = (byte) ((uint) num2 >> 1);
                  colorPtr2->R = num3;
                  colorPtr2->G = num3;
                  colorPtr2->B = LiquidRenderer.VISCOSITY_MASK[index3];
                  colorPtr2->A = num2;
                }
                ++liquidCachePtr9;
                ++liquidDrawCachePtr2;
                ++colorPtr2;
              }
              liquidCachePtr9 += 4;
            }
          }
        LiquidRenderer.LiquidCache* liquidCachePtr11 = liquidCachePtr1;
        for (int x = rectangle.X; x < rectangle.X + rectangle.Width; ++x)
        {
          for (int y = rectangle.Y; y < rectangle.Y + rectangle.Height; ++y)
          {
            if (liquidCachePtr11->VisibleType == (byte) 1 && liquidCachePtr11->HasVisibleLiquid && Dust.lavaBubbles < 200)
            {
              if (this._random.Next(700) == 0)
                Dust.NewDust(new Vector2((float) (x * 16), (float) (y * 16)), 16, 16, 35, 0.0f, 0.0f, 0, Color.White, 1f);
              if (this._random.Next(350) == 0)
              {
                int index = Dust.NewDust(new Vector2((float) (x * 16), (float) (y * 16)), 16, 8, 35, 0.0f, 0.0f, 50, Color.White, 1.5f);
                Main.dust[index].velocity *= 0.8f;
                Main.dust[index].velocity.X *= 2f;
                Main.dust[index].velocity.Y -= (float) this._random.Next(1, 7) * 0.1f;
                if (this._random.Next(10) == 0)
                  Main.dust[index].velocity.Y *= (float) this._random.Next(2, 5);
                Main.dust[index].noGravity = true;
              }
            }
            ++liquidCachePtr11;
          }
        }
      }
      if (this.WaveFilters == null)
        return;
      this.WaveFilters(this._waveMask, this.GetCachedDrawArea());
    }

    private unsafe void InternalDraw(
      SpriteBatch spriteBatch,
      Vector2 drawOffset,
      int waterStyle,
      float globalAlpha,
      bool isBackgroundDraw)
    {
      Rectangle drawArea = this._drawArea;
      Main.tileBatch.Begin();
      fixed (LiquidRenderer.LiquidDrawCache* liquidDrawCachePtr1 = &this._drawCache[0])
      {
        LiquidRenderer.LiquidDrawCache* liquidDrawCachePtr2 = liquidDrawCachePtr1;
        for (int x = drawArea.X; x < drawArea.X + drawArea.Width; ++x)
        {
          for (int y = drawArea.Y; y < drawArea.Y + drawArea.Height; ++y)
          {
            if (liquidDrawCachePtr2->IsVisible)
            {
              Rectangle sourceRectangle = liquidDrawCachePtr2->SourceRectangle;
              if (liquidDrawCachePtr2->IsSurfaceLiquid)
                sourceRectangle.Y = 1280;
              else
                sourceRectangle.Y += this._animationFrame * 80;
              Vector2 liquidOffset = liquidDrawCachePtr2->LiquidOffset;
              float val2 = liquidDrawCachePtr2->Opacity * (isBackgroundDraw ? 1f : LiquidRenderer.DEFAULT_OPACITY[(int) liquidDrawCachePtr2->Type]);
              int index = (int) liquidDrawCachePtr2->Type;
              switch (index)
              {
                case 0:
                  index = waterStyle;
                  val2 *= globalAlpha;
                  break;
                case 2:
                  index = 11;
                  break;
              }
              float num = Math.Min(1f, val2);
              VertexColors vertices;
              Lighting.GetCornerColors(x, y, out vertices, 1f);
              vertices.BottomLeftColor *= num;
              vertices.BottomRightColor *= num;
              vertices.TopLeftColor *= num;
              vertices.TopRightColor *= num;
              Main.DrawTileInWater(drawOffset, x, y);
              Main.tileBatch.Draw(this._liquidTextures[index].Value, new Vector2((float) (x << 4), (float) (y << 4)) + drawOffset + liquidOffset, new Rectangle?(sourceRectangle), vertices, Vector2.Zero, 1f, SpriteEffects.None);
            }
            ++liquidDrawCachePtr2;
          }
        }
      }
      Main.tileBatch.End();
    }

    public bool HasFullWater(int x, int y)
    {
      x -= this._drawArea.X;
      y -= this._drawArea.Y;
      int index = x * this._drawArea.Height + y;
      if (index < 0 || index >= this._drawCache.Length)
        return true;
      return this._drawCache[index].IsVisible && !this._drawCache[index].IsSurfaceLiquid;
    }

    public float GetVisibleLiquid(int x, int y)
    {
      x -= this._drawArea.X;
      y -= this._drawArea.Y;
      if (x < 0 || x >= this._drawArea.Width || (y < 0 || y >= this._drawArea.Height))
        return 0.0f;
      int index = (x + 2) * (this._drawArea.Height + 4) + y + 2;
      return !this._cache[index].HasVisibleLiquid ? 0.0f : this._cache[index].VisibleLiquidLevel;
    }

    public void Update(GameTime gameTime)
    {
      if (Main.gamePaused || !Main.hasFocus)
        return;
      float num = Main.windSpeedCurrent * 25f;
      this._frameState += ((double) num >= 0.0 ? num + 6f : num - 6f) * (float) gameTime.ElapsedGameTime.TotalSeconds;
      if ((double) this._frameState < 0.0)
        this._frameState += 16f;
      this._frameState %= 16f;
      this._animationFrame = (int) this._frameState;
    }

    public void PrepareDraw(Rectangle drawArea)
    {
      this.InternalPrepareDraw(drawArea);
    }

    public void SetWaveMaskData(ref Texture2D texture)
    {
      try
      {
        if (texture == null || texture.Width < this._drawArea.Height || texture.Height < this._drawArea.Width)
        {
          Console.WriteLine("WaveMaskData texture recreated. {0}x{1}", (object) this._drawArea.Height, (object) this._drawArea.Width);
          if (texture != null)
          {
            try
            {
              texture.Dispose();
            }
            catch
            {
            }
          }
          texture = new Texture2D(Main.instance.GraphicsDevice, this._drawArea.Height, this._drawArea.Width, false, SurfaceFormat.Color);
        }
        texture.SetData<Color>(0, new Rectangle?(new Rectangle(0, 0, this._drawArea.Height, this._drawArea.Width)), this._waveMask, 0, this._drawArea.Width * this._drawArea.Height);
      }
      catch
      {
        texture = new Texture2D(Main.instance.GraphicsDevice, this._drawArea.Height, this._drawArea.Width, false, SurfaceFormat.Color);
        texture.SetData<Color>(0, new Rectangle?(new Rectangle(0, 0, this._drawArea.Height, this._drawArea.Width)), this._waveMask, 0, this._drawArea.Width * this._drawArea.Height);
      }
    }

    public Rectangle GetCachedDrawArea()
    {
      return this._drawArea;
    }

    public void Draw(
      SpriteBatch spriteBatch,
      Vector2 drawOffset,
      int waterStyle,
      float alpha,
      bool isBackgroundDraw)
    {
      this.InternalDraw(spriteBatch, drawOffset, waterStyle, alpha, isBackgroundDraw);
    }

    private struct LiquidCache
    {
      public float LiquidLevel;
      public float VisibleLiquidLevel;
      public float Opacity;
      public bool IsSolid;
      public bool IsHalfBrick;
      public bool HasLiquid;
      public bool HasVisibleLiquid;
      public bool HasWall;
      public Point FrameOffset;
      public bool HasLeftEdge;
      public bool HasRightEdge;
      public bool HasTopEdge;
      public bool HasBottomEdge;
      public float LeftWall;
      public float RightWall;
      public float BottomWall;
      public float TopWall;
      public float VisibleLeftWall;
      public float VisibleRightWall;
      public float VisibleBottomWall;
      public float VisibleTopWall;
      public byte Type;
      public byte VisibleType;
    }

    private struct LiquidDrawCache
    {
      public Rectangle SourceRectangle;
      public Vector2 LiquidOffset;
      public bool IsVisible;
      public float Opacity;
      public byte Type;
      public bool IsSurfaceLiquid;
      public bool HasWall;
    }
  }
}
