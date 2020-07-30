// Decompiled with JetBrains decompiler
// Type: Terraria.Entity
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System;

namespace Terraria
{
  public abstract class Entity
  {
    public int direction = 1;
    public int whoAmI;
    public bool active;
    public Vector2 position;
    public Vector2 velocity;
    public Vector2 oldPosition;
    public Vector2 oldVelocity;
    public int oldDirection;
    public int width;
    public int height;
    public bool wet;
    public bool honeyWet;
    public byte wetCount;
    public bool lavaWet;

    public virtual Vector2 VisualPosition
    {
      get
      {
        return this.position;
      }
    }

    public float AngleTo(Vector2 Destination)
    {
      return (float) Math.Atan2((double) Destination.Y - (double) this.Center.Y, (double) Destination.X - (double) this.Center.X);
    }

    public float AngleFrom(Vector2 Source)
    {
      return (float) Math.Atan2((double) this.Center.Y - (double) Source.Y, (double) this.Center.X - (double) Source.X);
    }

    public float Distance(Vector2 Other)
    {
      return Vector2.Distance(this.Center, Other);
    }

    public float DistanceSQ(Vector2 Other)
    {
      return Vector2.DistanceSquared(this.Center, Other);
    }

    public Vector2 DirectionTo(Vector2 Destination)
    {
      return Vector2.Normalize(Destination - this.Center);
    }

    public Vector2 DirectionFrom(Vector2 Source)
    {
      return Vector2.Normalize(this.Center - Source);
    }

    public bool WithinRange(Vector2 Target, float MaxRange)
    {
      return (double) Vector2.DistanceSquared(this.Center, Target) <= (double) MaxRange * (double) MaxRange;
    }

    public Vector2 Center
    {
      get
      {
        return new Vector2(this.position.X + (float) (this.width / 2), this.position.Y + (float) (this.height / 2));
      }
      set
      {
        this.position = new Vector2(value.X - (float) (this.width / 2), value.Y - (float) (this.height / 2));
      }
    }

    public Vector2 Left
    {
      get
      {
        return new Vector2(this.position.X, this.position.Y + (float) (this.height / 2));
      }
      set
      {
        this.position = new Vector2(value.X, value.Y - (float) (this.height / 2));
      }
    }

    public Vector2 Right
    {
      get
      {
        return new Vector2(this.position.X + (float) this.width, this.position.Y + (float) (this.height / 2));
      }
      set
      {
        this.position = new Vector2(value.X - (float) this.width, value.Y - (float) (this.height / 2));
      }
    }

    public Vector2 Top
    {
      get
      {
        return new Vector2(this.position.X + (float) (this.width / 2), this.position.Y);
      }
      set
      {
        this.position = new Vector2(value.X - (float) (this.width / 2), value.Y);
      }
    }

    public Vector2 TopLeft
    {
      get
      {
        return this.position;
      }
      set
      {
        this.position = value;
      }
    }

    public Vector2 TopRight
    {
      get
      {
        return new Vector2(this.position.X + (float) this.width, this.position.Y);
      }
      set
      {
        this.position = new Vector2(value.X - (float) this.width, value.Y);
      }
    }

    public Vector2 Bottom
    {
      get
      {
        return new Vector2(this.position.X + (float) (this.width / 2), this.position.Y + (float) this.height);
      }
      set
      {
        this.position = new Vector2(value.X - (float) (this.width / 2), value.Y - (float) this.height);
      }
    }

    public Vector2 BottomLeft
    {
      get
      {
        return new Vector2(this.position.X, this.position.Y + (float) this.height);
      }
      set
      {
        this.position = new Vector2(value.X, value.Y - (float) this.height);
      }
    }

    public Vector2 BottomRight
    {
      get
      {
        return new Vector2(this.position.X + (float) this.width, this.position.Y + (float) this.height);
      }
      set
      {
        this.position = new Vector2(value.X - (float) this.width, value.Y - (float) this.height);
      }
    }

    public Vector2 Size
    {
      get
      {
        return new Vector2((float) this.width, (float) this.height);
      }
      set
      {
        this.width = (int) value.X;
        this.height = (int) value.Y;
      }
    }

    public Rectangle Hitbox
    {
      get
      {
        return new Rectangle((int) this.position.X, (int) this.position.Y, this.width, this.height);
      }
      set
      {
        this.position = new Vector2((float) value.X, (float) value.Y);
        this.width = value.Width;
        this.height = value.Height;
      }
    }
  }
}
