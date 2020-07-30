﻿// Decompiled with JetBrains decompiler
// Type: Terraria.UI.Chat.ChatLine
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;

namespace Terraria.UI.Chat
{
  public class ChatLine
  {
    public Color color = Color.White;
    public string originalText = "";
    public TextSnippet[] parsedText = new TextSnippet[0];
    public int showTime;
    private int? parsingPixelLimit;
    private bool needsParsing;

    public void UpdateTimeLeft()
    {
      if (this.showTime > 0)
        --this.showTime;
      if (!this.needsParsing)
        return;
      this.needsParsing = false;
    }

    public void Copy(ChatLine other)
    {
      this.needsParsing = other.needsParsing;
      this.parsingPixelLimit = other.parsingPixelLimit;
      this.originalText = other.originalText;
      this.parsedText = other.parsedText;
      this.showTime = other.showTime;
      this.color = other.color;
    }

    public void FlagAsNeedsReprocessing()
    {
      this.needsParsing = true;
    }
  }
}
