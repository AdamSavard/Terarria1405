// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.TileDrawInfo
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.DataStructures
{
  public class TileDrawInfo
  {
    public Vector3[] colorSlices = new Vector3[9];
    public Tile tileCache;
    public ushort typeCache;
    public short tileFrameX;
    public short tileFrameY;
    public Texture2D drawTexture;
    public Color tileLight;
    public int tileTop;
    public int tileWidth;
    public int tileHeight;
    public int halfBrickHeight;
    public int addFrY;
    public int addFrX;
    public SpriteEffects tileSpriteEffect;
    public Texture2D glowTexture;
    public Rectangle glowSourceRect;
    public Color glowColor;
    public Color finalColor;
    public Color colorTint;
  }
}
