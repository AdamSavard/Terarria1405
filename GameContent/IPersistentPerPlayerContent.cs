// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.IPersistentPerPlayerContent
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using System.IO;

namespace Terraria.GameContent
{
  public interface IPersistentPerPlayerContent
  {
    void Save(Player player, BinaryWriter writer);

    void Load(Player player, BinaryReader reader, int gameVersionSaveWasMadeOn);

    void ApplyLoadedDataToOutOfPlayerFields(Player player);

    void ResetDataForNewPlayer(Player player);

    void Reset();
  }
}
