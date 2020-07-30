// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Creative.ICreativePower
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using System.Collections.Generic;
using System.IO;
using Terraria.UI;

namespace Terraria.GameContent.Creative
{
  public interface ICreativePower
  {
    ushort PowerId { get; set; }

    string ServerConfigName { get; set; }

    PowerPermissionLevel CurrentPermissionLevel { get; set; }

    PowerPermissionLevel DefaultPermissionLevel { get; set; }

    void DeserializeNetMessage(BinaryReader reader, int userId);

    void ProvidePowerButtons(CreativePowerUIElementRequestInfo info, List<UIElement> elements);

    bool GetIsUnlocked();
  }
}
