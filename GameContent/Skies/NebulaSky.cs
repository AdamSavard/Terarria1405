﻿// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Skies.NebulaSky
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
  public class NebulaSky : CustomSky
  {
    private UnifiedRandom _random = new UnifiedRandom();
    private NebulaSky.LightPillar[] _pillars;
    private Asset<Texture2D> _planetTexture;
    private Asset<Texture2D> _bgTexture;
    private Asset<Texture2D> _beamTexture;
    private Asset<Texture2D>[] _rockTextures;
    private bool _isActive;
    private float _fadeOpacity;

    public override void OnLoad()
    {
      this._planetTexture = (Asset<Texture2D>) Main.Assets.Request<Texture2D>("Images/Misc/NebulaSky/Planet", (AssetRequestMode) 1);
      this._bgTexture = (Asset<Texture2D>) Main.Assets.Request<Texture2D>("Images/Misc/NebulaSky/Background", (AssetRequestMode) 1);
      this._beamTexture = (Asset<Texture2D>) Main.Assets.Request<Texture2D>("Images/Misc/NebulaSky/Beam", (AssetRequestMode) 1);
      this._rockTextures = new Asset<Texture2D>[3];
      for (int index = 0; index < this._rockTextures.Length; ++index)
        this._rockTextures[index] = (Asset<Texture2D>) Main.Assets.Request<Texture2D>("Images/Misc/NebulaSky/Rock_" + (object) index, (AssetRequestMode) 1);
    }

    public override void Update(GameTime gameTime)
    {
      if (this._isActive)
        this._fadeOpacity = Math.Min(1f, 0.01f + this._fadeOpacity);
      else
        this._fadeOpacity = Math.Max(0.0f, this._fadeOpacity - 0.01f);
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
        spriteBatch.Draw(this._bgTexture.get_Value(), new Rectangle(0, Math.Max(0, (int) ((Main.worldSurface * 16.0 - (double) Main.screenPosition.Y - 2400.0) * 0.100000001490116)), Main.screenWidth, Main.screenHeight), Color.White * Math.Min(1f, (float) (((double) Main.screenPosition.Y - 800.0) / 1000.0) * this._fadeOpacity));
        Vector2 vector2_1 = new Vector2((float) (Main.screenWidth >> 1), (float) (Main.screenHeight >> 1));
        Vector2 vector2_2 = 0.01f * (new Vector2((float) Main.maxTilesX * 8f, (float) Main.worldSurface / 2f) - Main.screenPosition);
        spriteBatch.Draw(this._planetTexture.get_Value(), vector2_1 + new Vector2(-200f, -200f) + vector2_2, new Rectangle?(), Color.White * 0.9f * this._fadeOpacity, 0.0f, new Vector2((float) (this._planetTexture.Width() >> 1), (float) (this._planetTexture.Height() >> 1)), 1f, SpriteEffects.None, 1f);
      }
      int num1 = -1;
      int num2 = 0;
      for (int index = 0; index < this._pillars.Length; ++index)
      {
        float depth = this._pillars[index].Depth;
        if (num1 == -1 && (double) depth < (double) maxDepth)
          num1 = index;
        if ((double) depth > (double) minDepth)
          num2 = index;
        else
          break;
      }
      if (num1 == -1)
        return;
      Vector2 vector2_3 = Main.screenPosition + new Vector2((float) (Main.screenWidth >> 1), (float) (Main.screenHeight >> 1));
      Rectangle rectangle = new Rectangle(-1000, -1000, 4000, 4000);
      float num3 = Math.Min(1f, (float) (((double) Main.screenPosition.Y - 1000.0) / 1000.0));
      for (int index1 = num1; index1 < num2; ++index1)
      {
        Vector2 vector2_1 = new Vector2(1f / this._pillars[index1].Depth, 0.9f / this._pillars[index1].Depth);
        Vector2 position = (this._pillars[index1].Position - vector2_3) * vector2_1 + vector2_3 - Main.screenPosition;
        if (rectangle.Contains((int) position.X, (int) position.Y))
        {
          float num4 = vector2_1.X * 450f;
          spriteBatch.Draw(this._beamTexture.get_Value(), position, new Rectangle?(), Color.White * 0.2f * num3 * this._fadeOpacity, 0.0f, Vector2.Zero, new Vector2(num4 / 70f, num4 / 45f), SpriteEffects.None, 0.0f);
          int index2 = 0;
          for (float num5 = 0.0f; (double) num5 <= 1.0; num5 += 0.03f)
          {
            float num6 = (float) (1.0 - ((double) num5 + (double) Main.GlobalTimeWrappedHourly * 0.0199999995529652 + Math.Sin((double) index1)) % 1.0);
            spriteBatch.Draw(this._rockTextures[index2].get_Value(), position + new Vector2((float) (Math.Sin((double) num5 * 1582.0) * ((double) num4 * 0.5) + (double) num4 * 0.5), num6 * 2000f), new Rectangle?(), Color.White * num6 * num3 * this._fadeOpacity, num6 * 20f, new Vector2((float) (this._rockTextures[index2].Width() >> 1), (float) (this._rockTextures[index2].Height() >> 1)), 0.9f, SpriteEffects.None, 0.0f);
            index2 = (index2 + 1) % this._rockTextures.Length;
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
      this._pillars = new NebulaSky.LightPillar[40];
      for (int index = 0; index < this._pillars.Length; ++index)
      {
        this._pillars[index].Position.X = (float) ((double) index / (double) this._pillars.Length * ((double) Main.maxTilesX * 16.0 + 20000.0) + (double) this._random.NextFloat() * 40.0 - 20.0 - 20000.0);
        this._pillars[index].Position.Y = (float) ((double) this._random.NextFloat() * 200.0 - 2000.0);
        this._pillars[index].Depth = (float) ((double) this._random.NextFloat() * 8.0 + 7.0);
      }
      Array.Sort<NebulaSky.LightPillar>(this._pillars, new Comparison<NebulaSky.LightPillar>(this.SortMethod));
    }

    private int SortMethod(NebulaSky.LightPillar pillar1, NebulaSky.LightPillar pillar2)
    {
      return pillar2.Depth.CompareTo(pillar1.Depth);
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

    private struct LightPillar
    {
      public Vector2 Position;
      public float Depth;
    }
  }
}
