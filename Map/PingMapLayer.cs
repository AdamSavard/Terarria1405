﻿// Decompiled with JetBrains decompiler
// Type: Terraria.Map.PingMapLayer
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.UI;

namespace Terraria.Map
{
  public class PingMapLayer : IMapLayer
  {
    private readonly SlotVector<PingMapLayer.Ping> _pings = new SlotVector<PingMapLayer.Ping>(100);
    private const double PING_DURATION_IN_SECONDS = 15.0;
    private const double PING_FRAME_RATE = 10.0;

    public void Draw(ref MapOverlayDrawContext context, ref string text)
    {
      SpriteFrame frame = new SpriteFrame((byte) 1, (byte) 5);
      DateTime now = DateTime.Now;
      using (IEnumerator<SlotVector<PingMapLayer.Ping>.ItemPair> enumerator = ((IEnumerable<SlotVector<PingMapLayer.Ping>.ItemPair>) this._pings).GetEnumerator())
      {
        while (((IEnumerator) enumerator).MoveNext())
        {
          SlotVector<PingMapLayer.Ping>.ItemPair current = enumerator.Current;
          PingMapLayer.Ping ping = (PingMapLayer.Ping) current.Value;
          double totalSeconds = (now - ping.Time).TotalSeconds;
          int num = (int) (totalSeconds * 10.0);
          frame.CurrentRow = (byte) ((uint) num % (uint) frame.RowCount);
          context.Draw(TextureAssets.MapPing.Value, ping.Position, frame, Alignment.Center);
          if (totalSeconds > 15.0)
            this._pings.Remove((SlotId) current.Id);
        }
      }
    }

    public void Add(Vector2 position)
    {
      if (this._pings.Count == this._pings.Capacity)
        return;
      this._pings.Add(new PingMapLayer.Ping(position));
    }

    private struct Ping
    {
      public readonly Vector2 Position;
      public readonly DateTime Time;

      public Ping(Vector2 position)
      {
        this.Position = position;
        this.Time = DateTime.Now;
      }
    }
  }
}
