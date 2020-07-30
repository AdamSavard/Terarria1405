// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.CommonResourceBarMethods
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

namespace Terraria.GameContent.UI
{
  public class CommonResourceBarMethods
  {
    public static void DrawLifeMouseOver()
    {
      if (Main.mouseText)
        return;
      Player localPlayer = Main.LocalPlayer;
      localPlayer.cursorItemIconEnabled = false;
      string text = localPlayer.statLife.ToString() + "/" + (object) localPlayer.statLifeMax2;
      Main.instance.MouseTextHackZoom(text, (string) null);
      Main.mouseText = true;
    }

    public static void DrawManaMouseOver()
    {
      if (Main.mouseText)
        return;
      Player localPlayer = Main.LocalPlayer;
      localPlayer.cursorItemIconEnabled = false;
      string text = localPlayer.statMana.ToString() + "/" + (object) localPlayer.statManaMax2;
      Main.instance.MouseTextHackZoom(text, (string) null);
      Main.mouseText = true;
    }
  }
}
