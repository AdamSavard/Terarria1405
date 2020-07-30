﻿// Decompiled with JetBrains decompiler
// Type: Terraria.Achievements.AchievementManager
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
using System.Security.Cryptography;
using System.Text;
using Terraria.Social;
using Terraria.UI;
using Terraria.Utilities;

namespace Terraria.Achievements
{
  public class AchievementManager
  {
    private static object _ioLock = new object();
    private Dictionary<string, Achievement> _achievements = new Dictionary<string, Achievement>();
    private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings();
    private Dictionary<string, int> _achievementIconIndexes = new Dictionary<string, int>();
    private string _savePath;
    private bool _isCloudSave;
    private byte[] _cryptoKey;

    public event Achievement.AchievementCompleted OnAchievementCompleted;

    public AchievementManager()
    {
      if (SocialAPI.Achievements != null)
      {
        this._savePath = SocialAPI.Achievements.GetSavePath();
        this._isCloudSave = true;
        this._cryptoKey = SocialAPI.Achievements.GetEncryptionKey();
      }
      else
      {
        this._savePath = Main.SavePath + Path.DirectorySeparatorChar.ToString() + "achievements.dat";
        this._isCloudSave = false;
        this._cryptoKey = Encoding.ASCII.GetBytes("RELOGIC-TERRARIA");
      }
    }

    public void Save()
    {
      FileUtilities.ProtectedInvoke((Action) (() => this.Save(this._savePath, this._isCloudSave)));
    }

    private void Save(string path, bool cloud)
    {
      lock (AchievementManager._ioLock)
      {
        if (SocialAPI.Achievements != null)
          SocialAPI.Achievements.StoreStats();
        try
        {
          using (MemoryStream memoryStream = new MemoryStream())
          {
            using (CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, new RijndaelManaged().CreateEncryptor(this._cryptoKey, this._cryptoKey), CryptoStreamMode.Write))
            {
              using (BsonWriter bsonWriter = new BsonWriter((Stream) cryptoStream))
              {
                JsonSerializer.Create(this._serializerSettings).Serialize((JsonWriter) bsonWriter, (object) this._achievements);
                ((JsonWriter) bsonWriter).Flush();
                cryptoStream.FlushFinalBlock();
                FileUtilities.WriteAllBytes(path, memoryStream.ToArray(), cloud);
              }
            }
          }
        }
        catch (Exception ex)
        {
          string savePath = this._savePath;
          FancyErrorPrinter.ShowFileSavingFailError(ex, savePath);
        }
      }
    }

    public List<Achievement> CreateAchievementsList()
    {
      return this._achievements.Values.ToList<Achievement>();
    }

    public void Load()
    {
      this.Load(this._savePath, this._isCloudSave);
    }

    private void Load(string path, bool cloud)
    {
      bool flag = false;
      lock (AchievementManager._ioLock)
      {
        if (!FileUtilities.Exists(path, cloud))
          return;
        byte[] buffer = FileUtilities.ReadAllBytes(path, cloud);
        Dictionary<string, AchievementManager.StoredAchievement> dictionary = (Dictionary<string, AchievementManager.StoredAchievement>) null;
        try
        {
          using (MemoryStream memoryStream = new MemoryStream(buffer))
          {
            using (CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, new RijndaelManaged().CreateDecryptor(this._cryptoKey, this._cryptoKey), CryptoStreamMode.Read))
            {
              using (BsonReader bsonReader = new BsonReader((Stream) cryptoStream))
                dictionary = (Dictionary<string, AchievementManager.StoredAchievement>) JsonSerializer.Create(this._serializerSettings).Deserialize<Dictionary<string, AchievementManager.StoredAchievement>>((JsonReader) bsonReader);
            }
          }
        }
        catch (Exception ex)
        {
          FileUtilities.Delete(path, cloud);
          return;
        }
        if (dictionary == null)
          return;
        foreach (KeyValuePair<string, AchievementManager.StoredAchievement> keyValuePair in dictionary)
        {
          if (this._achievements.ContainsKey(keyValuePair.Key))
            this._achievements[keyValuePair.Key].Load(keyValuePair.Value.Conditions);
        }
        if (SocialAPI.Achievements != null)
        {
          foreach (KeyValuePair<string, Achievement> achievement in this._achievements)
          {
            if (achievement.Value.IsCompleted && !SocialAPI.Achievements.IsAchievementCompleted(achievement.Key))
            {
              flag = true;
              achievement.Value.ClearProgress();
            }
          }
        }
      }
      if (!flag)
        return;
      this.Save();
    }

    public void ClearAll()
    {
      if (SocialAPI.Achievements != null)
        return;
      foreach (KeyValuePair<string, Achievement> achievement in this._achievements)
        achievement.Value.ClearProgress();
      this.Save();
    }

    private void AchievementCompleted(Achievement achievement)
    {
      this.Save();
      if (this.OnAchievementCompleted == null)
        return;
      this.OnAchievementCompleted(achievement);
    }

    public void Register(Achievement achievement)
    {
      this._achievements.Add(achievement.Name, achievement);
      achievement.OnCompleted += new Achievement.AchievementCompleted(this.AchievementCompleted);
    }

    public void RegisterIconIndex(string achievementName, int iconIndex)
    {
      this._achievementIconIndexes.Add(achievementName, iconIndex);
    }

    public void RegisterAchievementCategory(string achievementName, AchievementCategory category)
    {
      this._achievements[achievementName].SetCategory(category);
    }

    public Achievement GetAchievement(string achievementName)
    {
      Achievement achievement;
      return this._achievements.TryGetValue(achievementName, out achievement) ? achievement : (Achievement) null;
    }

    public T GetCondition<T>(string achievementName, string conditionName) where T : AchievementCondition
    {
      return this.GetCondition(achievementName, conditionName) as T;
    }

    public AchievementCondition GetCondition(
      string achievementName,
      string conditionName)
    {
      Achievement achievement;
      return this._achievements.TryGetValue(achievementName, out achievement) ? achievement.GetCondition(conditionName) : (AchievementCondition) null;
    }

    public int GetIconIndex(string achievementName)
    {
      int num;
      return this._achievementIconIndexes.TryGetValue(achievementName, out num) ? num : 0;
    }

    private class StoredAchievement
    {
      [JsonProperty]
      public Dictionary<string, JObject> Conditions;
    }
  }
}
