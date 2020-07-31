﻿// Decompiled with JetBrains decompiler
// Type: Terraria.IO.Preferences
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria.Localization;

namespace Terraria.IO
{
  public class Preferences
  {
    private Dictionary<string, object> _data = new Dictionary<string, object>();
    private readonly object _lock = new object();
    private readonly string _path;
    private readonly JsonSerializerSettings _serializerSettings;
    public readonly bool UseBson;
    public bool AutoSave;

    public event Action<Preferences> OnSave;

    public event Action<Preferences> OnLoad;

    public event Preferences.TextProcessAction OnProcessText;

    public Preferences(string path, bool parseAllTypes = false, bool useBson = false)
    {
      this._path = path;
      this.UseBson = useBson;
      if (parseAllTypes)
      {
        JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
        serializerSettings.TypeNameHandling = (TypeNameHandling) 4;
        serializerSettings.MetadataPropertyHandling = (MetadataPropertyHandling) 1;
        serializerSettings.Formatting = (Formatting) 1;
        this._serializerSettings = serializerSettings;
      }
      else
      {
        JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
        serializerSettings.Formatting = (Formatting) 1;
        this._serializerSettings = serializerSettings;
      }
    }

    public bool Load()
    {
      lock (this._lock)
      {
        if (!File.Exists(this._path))
          return false;
        try
        {
          if (!this.UseBson)
          {
            this._data = (Dictionary<string, object>) JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(this._path), this._serializerSettings);
          }
          else
          {
            using (FileStream fileStream = File.OpenRead(this._path))
            {
              using (BsonReader bsonReader = new BsonReader((Stream) fileStream))
                this._data = (Dictionary<string, object>) JsonSerializer.Create(this._serializerSettings).Deserialize<Dictionary<string, object>>((JsonReader) bsonReader);
            }
          }
          if (this._data == null)
            this._data = new Dictionary<string, object>();
          if (this.OnLoad != null)
            this.OnLoad(this);
          return true;
        }
        catch (Exception ex)
        {
          return false;
        }
      }
    }

    public bool Save(bool canCreateFile = true)
    {
      lock (this._lock)
      {
        try
        {
          if (this.OnSave != null)
            this.OnSave(this);
          if (!canCreateFile && !File.Exists(this._path))
            return false;
          Directory.GetParent(this._path).Create();
          if (File.Exists(this._path))
            File.SetAttributes(this._path, FileAttributes.Normal);
          if (!this.UseBson)
          {
            string text = JsonConvert.SerializeObject((object) this._data, this._serializerSettings);
            if (this.OnProcessText != null)
              this.OnProcessText(ref text);
            File.WriteAllText(this._path, text);
            File.SetAttributes(this._path, FileAttributes.Normal);
          }
          else
          {
            using (FileStream fileStream = File.Create(this._path))
            {
              using (BsonWriter bsonWriter = new BsonWriter((Stream) fileStream))
              {
                File.SetAttributes(this._path, FileAttributes.Normal);
                JsonSerializer.Create(this._serializerSettings).Serialize((JsonWriter) bsonWriter, (object) this._data);
              }
            }
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine(Language.GetTextValue("Error.UnableToWritePreferences", (object) this._path));
          Console.WriteLine(ex.ToString());
          return false;
        }
        return true;
      }
    }

    public void Clear()
    {
      this._data.Clear();
    }

    public void Put(string name, object value)
    {
      lock (this._lock)
      {
        this._data[name] = value;
        if (!this.AutoSave)
          return;
        this.Save(true);
      }
    }

    public bool Contains(string name)
    {
      lock (this._lock)
        return this._data.ContainsKey(name);
    }

    public T Get<T>(string name, T defaultValue)
    {
      lock (this._lock)
      {
        try
        {
          object obj1;
          if (!this._data.TryGetValue(name, out obj1))
            return defaultValue;
          switch (obj1)
          {
            case T obj:
              return obj;
            case JObject _:
              return JsonConvert.DeserializeObject<T>(((object) (JObject) obj1).ToString());
            default:
              return (T) Convert.ChangeType(obj1, typeof (T));
          }
        }
        catch
        {
          return defaultValue;
        }
      }
    }

    public void Get<T>(string name, ref T currentValue)
    {
      currentValue = this.Get<T>(name, currentValue);
    }

    public List<string> GetAllKeys()
    {
      return this._data.Keys.ToList<string>();
    }

    public delegate void TextProcessAction(ref string text);
  }
}
