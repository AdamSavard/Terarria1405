﻿// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Chat.GlyphTagHandler
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Graphics;
using System.Collections.Generic;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Chat
{
  public class GlyphTagHandler : ITagHandler
  {
    public static float GlyphsScale = 1f;
    private static Dictionary<string, int> GlyphIndexes = new Dictionary<string, int>()
    {
      {
        Buttons.A.ToString(),
        0
      },
      {
        Buttons.B.ToString(),
        1
      },
      {
        Buttons.Back.ToString(),
        4
      },
      {
        Buttons.DPadDown.ToString(),
        15
      },
      {
        Buttons.DPadLeft.ToString(),
        14
      },
      {
        Buttons.DPadRight.ToString(),
        13
      },
      {
        Buttons.DPadUp.ToString(),
        16
      },
      {
        Buttons.LeftShoulder.ToString(),
        6
      },
      {
        Buttons.LeftStick.ToString(),
        10
      },
      {
        Buttons.LeftThumbstickDown.ToString(),
        20
      },
      {
        Buttons.LeftThumbstickLeft.ToString(),
        17
      },
      {
        Buttons.LeftThumbstickRight.ToString(),
        18
      },
      {
        Buttons.LeftThumbstickUp.ToString(),
        19
      },
      {
        Buttons.LeftTrigger.ToString(),
        8
      },
      {
        Buttons.RightShoulder.ToString(),
        7
      },
      {
        Buttons.RightStick.ToString(),
        11
      },
      {
        Buttons.RightThumbstickDown.ToString(),
        24
      },
      {
        Buttons.RightThumbstickLeft.ToString(),
        21
      },
      {
        Buttons.RightThumbstickRight.ToString(),
        22
      },
      {
        Buttons.RightThumbstickUp.ToString(),
        23
      },
      {
        Buttons.RightTrigger.ToString(),
        9
      },
      {
        Buttons.Start.ToString(),
        5
      },
      {
        Buttons.X.ToString(),
        2
      },
      {
        Buttons.Y.ToString(),
        3
      },
      {
        "LR",
        25
      }
    };
    private const int GlyphsPerLine = 25;
    private const int MaxGlyphs = 26;

    TextSnippet ITagHandler.Parse(
      string text,
      Color baseColor,
      string options)
    {
      int result;
      if (!int.TryParse(text, out result) || result >= 26)
        return new TextSnippet(text);
      GlyphTagHandler.GlyphSnippet glyphSnippet = new GlyphTagHandler.GlyphSnippet(result);
      glyphSnippet.DeleteWhole = true;
      glyphSnippet.Text = "[g:" + (object) result + "]";
      return (TextSnippet) glyphSnippet;
    }

    public static string GenerateTag(int index)
    {
      return "[g" + ":" + (object) index + "]";
    }

    public static string GenerateTag(string keyname)
    {
      int index;
      return GlyphTagHandler.GlyphIndexes.TryGetValue(keyname, out index) ? GlyphTagHandler.GenerateTag(index) : keyname;
    }

    private class GlyphSnippet : TextSnippet
    {
      private int _glyphIndex;

      public GlyphSnippet(int index)
        : base("")
      {
        this._glyphIndex = index;
        this.Color = Color.White;
      }

      public override bool UniqueDraw(
        bool justCheckingString,
        out Vector2 size,
        SpriteBatch spriteBatch,
        Vector2 position = default (Vector2),
        Color color = default (Color),
        float scale = 1f)
      {
        if (!justCheckingString && color != Color.Black)
        {
          int frameX = this._glyphIndex;
          if (this._glyphIndex == 25)
            frameX = (double) Main.GlobalTimeWrappedHourly % 0.600000023841858 < 0.300000011920929 ? 17 : 18;
          Texture2D texture2D = TextureAssets.TextGlyph[0].get_Value();
          spriteBatch.Draw(texture2D, position, new Rectangle?(texture2D.Frame(25, 1, frameX, frameX / 25, 0, 0)), color, 0.0f, Vector2.Zero, GlyphTagHandler.GlyphsScale, SpriteEffects.None, 0.0f);
        }
        size = new Vector2(26f) * GlyphTagHandler.GlyphsScale;
        return true;
      }

      public override float GetStringLength(DynamicSpriteFont font)
      {
        return 26f * GlyphTagHandler.GlyphsScale;
      }
    }
  }
}
