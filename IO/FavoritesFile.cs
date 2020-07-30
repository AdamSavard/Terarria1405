// Decompiled with JetBrains decompiler
// Type: Terraria.IO.FavoritesFile
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Terraria.UI;
using Terraria.Utilities;

namespace Terraria.IO
{
  public class FavoritesFile
  {
    private Dictionary<string, Dictionary<string, bool>> _data = new Dictionary<string, Dictionary<string, bool>>();
    public readonly string Path;
    public readonly bool IsCloudSave;

    public FavoritesFile(string path, bool isCloud)
    {
      this.Path = path;
      this.IsCloudSave = isCloud;
    }

    public void SaveFavorite(FileData fileData)
    {
      if (!this._data.ContainsKey(fileData.Type))
        this._data.Add(fileData.Type, new Dictionary<string, bool>());
      this._data[fileData.Type][fileData.GetFileName(true)] = fileData.IsFavorite;
      this.Save();
    }

    public void ClearEntry(FileData fileData)
    {
      if (!this._data.ContainsKey(fileData.Type))
        return;
      this._data[fileData.Type].Remove(fileData.GetFileName(true));
      this.Save();
    }

    public bool IsFavorite(FileData fileData)
    {
      if (!this._data.ContainsKey(fileData.Type))
        return false;
      string fileName = fileData.GetFileName(true);
      bool flag;
      return this._data[fileData.Type].TryGetValue(fileName, out flag) && flag;
    }

    public void Save()
    {
      try
      {
        FileUtilities.WriteAllBytes(this.Path, Encoding.ASCII.GetBytes(JsonConvert.SerializeObject((object) this._data, (Formatting) 1)), this.IsCloudSave);
      }
      catch (Exception ex)
      {
        string path = this.Path;
        FancyErrorPrinter.ShowFileSavingFailError(ex, path);
        throw;
      }
    }

    public void Load()
    {
      if (!FileUtilities.Exists(this.Path, this.IsCloudSave))
      {
        this._data.Clear();
      }
      else
      {
        try
        {
          this._data = (Dictionary<string, Dictionary<string, bool>>) JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, bool>>>(Encoding.ASCII.GetString(FileUtilities.ReadAllBytes(this.Path, this.IsCloudSave)));
          if (this._data != null)
            return;
          this._data = new Dictionary<string, Dictionary<string, bool>>();
        }
        catch (Exception ex)
        {
          Console.WriteLine("Unable to load favorites.json file ({0} : {1})", (object) this.Path, this.IsCloudSave ? (object) "Cloud Save" : (object) "Local Save");
        }
      }
    }
  }
}
