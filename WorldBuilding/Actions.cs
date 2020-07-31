﻿// Decompiled with JetBrains decompiler
// Type: Terraria.WorldBuilding.Actions
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.GameContent;

namespace Terraria.WorldBuilding
{
  public static class Actions
  {
    public static GenAction Chain(params GenAction[] actions)
    {
      for (int index = 0; index < actions.Length - 1; ++index)
        actions[index].NextAction = actions[index + 1];
      return actions[0];
    }

    public static GenAction Continue(GenAction action)
    {
      return (GenAction) new Actions.ContinueWrapper(action);
    }

    public class ContinueWrapper : GenAction
    {
      private GenAction _action;

      public ContinueWrapper(GenAction action)
      {
        this._action = action;
      }

      public override bool Apply(Point origin, int x, int y, params object[] args)
      {
        this._action.Apply(origin, x, y, args);
        return this.UnitApply(origin, x, y, args);
      }
    }

    public class Count : GenAction
    {
      private Ref<int> _count;

      public Count(Ref<int> count)
      {
        this._count = count;
      }

      public override bool Apply(Point origin, int x, int y, params object[] args)
      {
        ++this._count.Value;
        return this.UnitApply(origin, x, y, args);
      }
    }

    public class Scanner : GenAction
    {
      private Ref<int> _count;

      public Scanner(Ref<int> count)
      {
        this._count = count;
      }

      public override bool Apply(Point origin, int x, int y, params object[] args)
      {
        ++this._count.Value;
        return this.UnitApply(origin, x, y, args);
      }
    }

    public class TileScanner : GenAction
    {
      private ushort[] _tileIds;
      private Dictionary<ushort, int> _tileCounts;

      public TileScanner(params ushort[] tiles)
      {
        this._tileIds = tiles;
        this._tileCounts = new Dictionary<ushort, int>();
        for (int index = 0; index < tiles.Length; ++index)
          this._tileCounts[this._tileIds[index]] = 0;
      }

      public override bool Apply(Point origin, int x, int y, params object[] args)
      {
        Tile tile = GenBase._tiles[x, y];
        if (tile.active() && this._tileCounts.ContainsKey(tile.type))
          this._tileCounts[tile.type]++;
        return this.UnitApply(origin, x, y, args);
      }

      public Actions.TileScanner Output(Dictionary<ushort, int> resultsOutput)
      {
        this._tileCounts = resultsOutput;
        for (int index = 0; index < this._tileIds.Length; ++index)
        {
          if (!this._tileCounts.ContainsKey(this._tileIds[index]))
            this._tileCounts[this._tileIds[index]] = 0;
        }
        return this;
      }

      public Dictionary<ushort, int> GetResults()
      {
        return this._tileCounts;
      }

      public int GetCount(ushort tileId)
      {
        return !this._tileCounts.ContainsKey(tileId) ? -1 : this._tileCounts[tileId];
      }
    }

    public class Blank : GenAction
    {
      public override bool Apply(Point origin, int x, int y, params object[] args)
      {
        return this.UnitApply(origin, x, y, args);
      }
    }

    public class Custom : GenAction
    {
      private GenBase.CustomPerUnitAction _perUnit;

      public Custom(GenBase.CustomPerUnitAction perUnit)
      {
        this._perUnit = perUnit;
      }

      public override bool Apply(Point origin, int x, int y, params object[] args)
      {
        return this._perUnit(x, y, args) | this.UnitApply(origin, x, y, args);
      }
    }

    public class ClearMetadata : GenAction
    {
      public override bool Apply(Point origin, int x, int y, params object[] args)
      {
        GenBase._tiles[x, y].ClearMetadata();
        return this.UnitApply(origin, x, y, args);
      }
    }

    public class Clear : GenAction
    {
      public override bool Apply(Point origin, int x, int y, params object[] args)
      {
        GenBase._tiles[x, y].ClearEverything();
        return this.UnitApply(origin, x, y, args);
      }
    }

    public class ClearTile : GenAction
    {
      private bool _frameNeighbors;

      public ClearTile(bool frameNeighbors = false)
      {
        this._frameNeighbors = frameNeighbors;
      }

      public override bool Apply(Point origin, int x, int y, params object[] args)
      {
        WorldUtils.ClearTile(x, y, this._frameNeighbors);
        return this.UnitApply(origin, x, y, args);
      }
    }

    public class ClearWall : GenAction
    {
      private bool _frameNeighbors;

      public ClearWall(bool frameNeighbors = false)
      {
        this._frameNeighbors = frameNeighbors;
      }

      public override bool Apply(Point origin, int x, int y, params object[] args)
      {
        WorldUtils.ClearWall(x, y, this._frameNeighbors);
        return this.UnitApply(origin, x, y, args);
      }
    }

    public class HalfBlock : GenAction
    {
      private bool _value;

      public HalfBlock(bool value = true)
      {
        this._value = value;
      }

      public override bool Apply(Point origin, int x, int y, params object[] args)
      {
        GenBase._tiles[x, y].halfBrick(this._value);
        return this.UnitApply(origin, x, y, args);
      }
    }

    public class SetTile : GenAction
    {
      private ushort _type;
      private bool _doFraming;
      private bool _doNeighborFraming;

      public SetTile(ushort type, bool setSelfFrames = false, bool setNeighborFrames = true)
      {
        this._type = type;
        this._doFraming = setSelfFrames;
        this._doNeighborFraming = setNeighborFrames;
      }

      public override bool Apply(Point origin, int x, int y, params object[] args)
      {
        GenBase._tiles[x, y].Clear(~(TileDataType.Wiring | TileDataType.Actuator));
        GenBase._tiles[x, y].type = this._type;
        GenBase._tiles[x, y].active(true);
        if (this._doFraming)
          WorldUtils.TileFrame(x, y, this._doNeighborFraming);
        return this.UnitApply(origin, x, y, args);
      }
    }

    public class SetTileKeepWall : GenAction
    {
      private ushort _type;
      private bool _doFraming;
      private bool _doNeighborFraming;

      public SetTileKeepWall(ushort type, bool setSelfFrames = false, bool setNeighborFrames = true)
      {
        this._type = type;
        this._doFraming = setSelfFrames;
        this._doNeighborFraming = setNeighborFrames;
      }

      public override bool Apply(Point origin, int x, int y, params object[] args)
      {
        ushort wall = GenBase._tiles[x, y].wall;
        int wallFrameX = GenBase._tiles[x, y].wallFrameX();
        int wallFrameY = GenBase._tiles[x, y].wallFrameY();
        GenBase._tiles[x, y].Clear(~(TileDataType.Wiring | TileDataType.Actuator));
        GenBase._tiles[x, y].type = this._type;
        GenBase._tiles[x, y].active(true);
        if (wall > (ushort) 0)
        {
          GenBase._tiles[x, y].wall = wall;
          GenBase._tiles[x, y].wallFrameX(wallFrameX);
          GenBase._tiles[x, y].wallFrameY(wallFrameY);
        }
        if (this._doFraming)
          WorldUtils.TileFrame(x, y, this._doNeighborFraming);
        return this.UnitApply(origin, x, y, args);
      }
    }

    public class DebugDraw : GenAction
    {
      private Color _color;
      private SpriteBatch _spriteBatch;

      public DebugDraw(SpriteBatch spriteBatch, Color color = default (Color))
      {
        this._spriteBatch = spriteBatch;
        this._color = color;
      }

      public override bool Apply(Point origin, int x, int y, params object[] args)
      {
        this._spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Microsoft.Xna.Framework.Rectangle((x << 4) - (int) Main.screenPosition.X, (y << 4) - (int) Main.screenPosition.Y, 16, 16), this._color);
        return this.UnitApply(origin, x, y, args);
      }
    }

    public class SetSlope : GenAction
    {
      private int _slope;

      public SetSlope(int slope)
      {
        this._slope = slope;
      }

      public override bool Apply(Point origin, int x, int y, params object[] args)
      {
        WorldGen.SlopeTile(x, y, this._slope, false);
        return this.UnitApply(origin, x, y, args);
      }
    }

    public class SetHalfTile : GenAction
    {
      private bool _halfTile;

      public SetHalfTile(bool halfTile)
      {
        this._halfTile = halfTile;
      }

      public override bool Apply(Point origin, int x, int y, params object[] args)
      {
        GenBase._tiles[x, y].halfBrick(this._halfTile);
        return this.UnitApply(origin, x, y, args);
      }
    }

    public class PlaceTile : GenAction
    {
      private ushort _type;
      private int _style;

      public PlaceTile(ushort type, int style = 0)
      {
        this._type = type;
        this._style = style;
      }

      public override bool Apply(Point origin, int x, int y, params object[] args)
      {
        WorldGen.PlaceTile(x, y, (int) this._type, true, false, -1, this._style);
        return this.UnitApply(origin, x, y, args);
      }
    }

    public class RemoveWall : GenAction
    {
      public override bool Apply(Point origin, int x, int y, params object[] args)
      {
        GenBase._tiles[x, y].wall = (ushort) 0;
        return this.UnitApply(origin, x, y, args);
      }
    }

    public class PlaceWall : GenAction
    {
      private ushort _type;
      private bool _neighbors;

      public PlaceWall(ushort type, bool neighbors = true)
      {
        this._type = type;
        this._neighbors = neighbors;
      }

      public override bool Apply(Point origin, int x, int y, params object[] args)
      {
        GenBase._tiles[x, y].wall = this._type;
        WorldGen.SquareWallFrame(x, y, true);
        if (this._neighbors)
        {
          WorldGen.SquareWallFrame(x + 1, y, true);
          WorldGen.SquareWallFrame(x - 1, y, true);
          WorldGen.SquareWallFrame(x, y - 1, true);
          WorldGen.SquareWallFrame(x, y + 1, true);
        }
        return this.UnitApply(origin, x, y, args);
      }
    }

    public class SetLiquid : GenAction
    {
      private int _type;
      private byte _value;

      public SetLiquid(int type = 0, byte value = 255)
      {
        this._value = value;
        this._type = type;
      }

      public override bool Apply(Point origin, int x, int y, params object[] args)
      {
        GenBase._tiles[x, y].liquidType(this._type);
        GenBase._tiles[x, y].liquid = this._value;
        return this.UnitApply(origin, x, y, args);
      }
    }

    public class SwapSolidTile : GenAction
    {
      private ushort _type;

      public SwapSolidTile(ushort type)
      {
        this._type = type;
      }

      public override bool Apply(Point origin, int x, int y, params object[] args)
      {
        Tile tile = GenBase._tiles[x, y];
        if (!WorldGen.SolidTile(tile))
          return this.Fail();
        tile.ResetToType(this._type);
        return this.UnitApply(origin, x, y, args);
      }
    }

    public class SetFrames : GenAction
    {
      private bool _frameNeighbors;

      public SetFrames(bool frameNeighbors = false)
      {
        this._frameNeighbors = frameNeighbors;
      }

      public override bool Apply(Point origin, int x, int y, params object[] args)
      {
        WorldUtils.TileFrame(x, y, this._frameNeighbors);
        return this.UnitApply(origin, x, y, args);
      }
    }

    public class Smooth : GenAction
    {
      private bool _applyToNeighbors;

      public Smooth(bool applyToNeighbors = false)
      {
        this._applyToNeighbors = applyToNeighbors;
      }

      public override bool Apply(Point origin, int x, int y, params object[] args)
      {
        Tile.SmoothSlope(x, y, this._applyToNeighbors, false);
        return this.UnitApply(origin, x, y, args);
      }
    }
  }
}
