// Decompiled with JetBrains decompiler
// Type: Terraria.Localization.Language
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using System.Text.RegularExpressions;
using Terraria.Utilities;

namespace Terraria.Localization
{
  public static class Language
  {
    public static GameCulture ActiveCulture
    {
      get
      {
        return LanguageManager.Instance.ActiveCulture;
      }
    }

    public static LocalizedText GetText(string key)
    {
      return LanguageManager.Instance.GetText(key);
    }

    public static string GetTextValue(string key)
    {
      return LanguageManager.Instance.GetTextValue(key);
    }

    public static string GetTextValue(string key, object arg0)
    {
      return LanguageManager.Instance.GetTextValue(key, arg0);
    }

    public static string GetTextValue(string key, object arg0, object arg1)
    {
      return LanguageManager.Instance.GetTextValue(key, arg0, arg1);
    }

    public static string GetTextValue(string key, object arg0, object arg1, object arg2)
    {
      return LanguageManager.Instance.GetTextValue(key, arg0, arg1, arg2);
    }

    public static string GetTextValue(string key, params object[] args)
    {
      return LanguageManager.Instance.GetTextValue(key, args);
    }

    public static string GetTextValueWith(string key, object obj)
    {
      return LanguageManager.Instance.GetText(key).FormatWith(obj);
    }

    public static bool Exists(string key)
    {
      return LanguageManager.Instance.Exists(key);
    }

    public static int GetCategorySize(string key)
    {
      return LanguageManager.Instance.GetCategorySize(key);
    }

    public static LocalizedText[] FindAll(Regex regex)
    {
      return LanguageManager.Instance.FindAll(regex);
    }

    public static LocalizedText[] FindAll(LanguageSearchFilter filter)
    {
      return LanguageManager.Instance.FindAll(filter);
    }

    public static LocalizedText SelectRandom(
      LanguageSearchFilter filter,
      UnifiedRandom random = null)
    {
      return LanguageManager.Instance.SelectRandom(filter, random);
    }

    public static LocalizedText RandomFromCategory(
      string categoryName,
      UnifiedRandom random = null)
    {
      return LanguageManager.Instance.RandomFromCategory(categoryName, random);
    }
  }
}
