﻿// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Skies.VortexSky
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
  public class VortexSky : CustomSky
  {
    private UnifiedRandom _random = new UnifiedRandom();
    private Asset<Texture2D> _planetTexture;
    private Asset<Texture2D> _bgTexture;
    private Asset<Texture2D> _boltTexture;
    private Asset<Texture2D> _flashTexture;
    private bool _isActive;
    private int _ticksUntilNextBolt;
    private float _fadeOpacity;
    private VortexSky.Bolt[] _bolts;

    public override void OnLoad()
    {
      this._planetTexture = (Asset<Texture2D>) Main.Assets.Request<Texture2D>("Images/Misc/VortexSky/Planet", (AssetRequestMode) 1);
      this._bgTexture = (Asset<Texture2D>) Main.Assets.Request<Texture2D>("Images/Misc/VortexSky/Background", (AssetRequestMode) 1);
      this._boltTexture = (Asset<Texture2D>) Main.Assets.Request<Texture2D>("Images/Misc/VortexSky/Bolt", (AssetRequestMode) 1);
      this._flashTexture = (Asset<Texture2D>) Main.Assets.Request<Texture2D>("Images/Misc/VortexSky/Flash", (AssetRequestMode) 1);
    }

    public override void Update(GameTime gameTime)
    {
      this._fadeOpacity = !this._isActive ? Math.Max(0.0f, this._fadeOpacity - 0.01f) : Math.Min(1f, 0.01f + this._fadeOpacity);
      if (this._ticksUntilNextBolt <= 0)
      {
        this._ticksUntilNextBolt = this._random.Next(1, 5);
        int index = 0;
        while (this._bolts[index].IsAlive && index != this._bolts.Length - 1)
          ++index;
        this._bolts[index].IsAlive = true;
        this._bolts[index].Position.X = (float) ((double) this._random.NextFloat() * ((double) Main.maxTilesX * 16.0 + 4000.0) - 2000.0);
        this._bolts[index].Position.Y = this._random.NextFloat() * 500f;
        this._bolts[index].Depth = (float) ((double) this._random.NextFloat() * 8.0 + 2.0);
        this._bolts[index].Life = 30;
      }
      --this._ticksUntilNextBolt;
      for (int index = 0; index < this._bolts.Length; ++index)
      {
        if (this._bolts[index].IsAlive)
        {
          --this._bolts[index].Life;
          if (this._bolts[index].Life <= 0)
            this._bolts[index].IsAlive = false;
        }
      }
    }

    public override Color OnTileColor(Color inColor)
    {
      return new Color(Vector4.Lerp(inColor.ToVector4(), Vector4.One, this._fadeOpacity * 0.5f));
    }

    public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
    {
      if ((double) maxDepth >= 3.40282346638529E+38 && (double) minDepth < 3.40282346638529E+38)
      {
        spriteBatch.Draw(TextureAssets.BlackTile.get_Value(), new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black * this._fadeOpacity);
        spriteBatch.Draw(this._bgTexture.get_Value(), new Rectangle(0, Math.Max(0, (int) ((Main.worldSurface * 16.0 - (double) Main.screenPosition.Y - 2400.0) * 0.100000001490116)), Main.screenWidth, Main.screenHeight), Color.White * Math.Min(1f, (float) (((double) Main.screenPosition.Y - 800.0) / 1000.0)) * this._fadeOpacity);
        Vector2 vector2_1 = new Vector2((float) (Main.screenWidth >> 1), (float) (Main.screenHeight >> 1));
        Vector2 vector2_2 = 0.01f * (new Vector2((float) Main.maxTilesX * 8f, (float) Main.worldSurface / 2f) - Main.screenPosition);
        spriteBatch.Draw(this._planetTexture.get_Value(), vector2_1 + new Vector2(-200f, -200f) + vector2_2, new Rectangle?(), Color.White * 0.9f * this._fadeOpacity, 0.0f, new Vector2((float) (this._planetTexture.Width() >> 1), (float) (this._planetTexture.Height() >> 1)), 1f, SpriteEffects.None, 1f);
      }
      float num1 = Math.Min(1f, (float) (((double) Main.screenPosition.Y - 1000.0) / 1000.0));
      Vector2 vector2_3 = Main.screenPosition + new Vector2((float) (Main.screenWidth >> 1), (float) (Main.screenHeight >> 1));
      Rectangle rectangle = new Rectangle(-1000, -1000, 4000, 4000);
      for (int index = 0; index < this._bolts.Length; ++index)
      {
        if (this._bolts[index].IsAlive && (double) this._bolts[index].Depth > (double) minDepth && (double) this._bolts[index].Depth < (double) maxDepth)
        {
          Vector2 vector2_1 = new Vector2(1f / this._bolts[index].Depth, 0.9f / this._bolts[index].Depth);
          Vector2 position = (this._bolts[index].Position - vector2_3) * vector2_1 + vector2_3 - Main.screenPosition;
          if (rectangle.Contains((int) position.X, (int) position.Y))
          {
            Texture2D texture = this._boltTexture.get_Value();
            int life = this._bolts[index].Life;
            if (life > 26 && life % 2 == 0)
              texture = this._flashTexture.get_Value();
            float num2 = (float) life / 30f;
            spriteBatch.Draw(texture, position, new Rectangle?(), Color.White * num1 * num2 * this._fadeOpacity, 0.0f, Vector2.Zero, vector2_1.X * 5f, SpriteEffects.None, 0.0f);
          }
        }
      }
    }

    public override float GetCloudAlpha()
    {
      return (float) ((1.0 - (double) this._fadeOpacity) * 0.300000011920929 + 0.699999988079071);
    }

    public override void Activate(Vector2 position, params object[] args)
    {
      this._fadeOpacity = 1f / 500f;
      this._isActive = true;
      this._bolts = new VortexSky.Bolt[500];
      for (int index = 0; index < this._bolts.Length; ++index)
        this._bolts[index].IsAlive = false;
    }

    public override void Deactivate(params object[] args)
    {
      this._isActive = false;
    }

    public override void Reset()
    {
      this._isActive = false;
    }

    public override bool IsActive()
    {
      return this._isActive || (double) this._fadeOpacity > 1.0 / 1000.0;
    }

    private struct Bolt
    {
      public Vector2 Position;
      public float Depth;
      public int Life;
      public bool IsAlive;
    }
  }
}
