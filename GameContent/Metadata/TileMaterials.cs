// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Metadata.TileMaterials
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Terraria.ID;

namespace Terraria.GameContent.Metadata
{
  public static class TileMaterials
  {
    private static readonly TileMaterial[] MaterialsByTileId = new TileMaterial[623];
    private static Dictionary<string, TileMaterial> _materialsByName = TileMaterials.DeserializeEmbeddedResource<Dictionary<string, TileMaterial>>("Terraria.GameContent.Metadata.MaterialData.Materials.json");

    static TileMaterials()
    {
      TileMaterial tileMaterial = TileMaterials._materialsByName["Default"];
      for (int index = 0; index < TileMaterials.MaterialsByTileId.Length; ++index)
        TileMaterials.MaterialsByTileId[index] = tileMaterial;
      foreach (KeyValuePair<string, string> keyValuePair in TileMaterials.DeserializeEmbeddedResource<Dictionary<string, string>>("Terraria.GameContent.Metadata.MaterialData.Tiles.json"))
      {
        string key = keyValuePair.Key;
        string index = keyValuePair.Value;
        TileMaterials.SetForTileId((ushort) TileID.Search.GetId(key), TileMaterials._materialsByName[index]);
      }
    }

    private static T DeserializeEmbeddedResource<T>(string path)
    {
      using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path))
      {
        using (StreamReader streamReader = new StreamReader(manifestResourceStream))
          return JsonConvert.DeserializeObject<T>(streamReader.ReadToEnd());
      }
    }

    public static void SetForTileId(ushort tileId, TileMaterial material)
    {
      TileMaterials.MaterialsByTileId[(int) tileId] = material;
    }

    public static TileMaterial GetByTileId(ushort tileId)
    {
      return TileMaterials.MaterialsByTileId[(int) tileId];
    }
  }
}
