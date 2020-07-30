// Decompiled with JetBrains decompiler
// Type: Terraria.Testing.ChatCommands.ArgumentListResult
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Terraria.Testing.ChatCommands
{
  public class ArgumentListResult : IEnumerable<string>, IEnumerable
  {
    public static readonly ArgumentListResult Empty = new ArgumentListResult(true);
    public static readonly ArgumentListResult Invalid = new ArgumentListResult(false);
    public readonly bool IsValid;
    private readonly List<string> _results;

    public int Count
    {
      get
      {
        return this._results.Count;
      }
    }

    public string this[int index]
    {
      get
      {
        return this._results[index];
      }
    }

    public ArgumentListResult(IEnumerable<string> results)
    {
      this._results = results.ToList<string>();
      this.IsValid = true;
    }

    private ArgumentListResult(bool isValid)
    {
      this._results = new List<string>();
      this.IsValid = isValid;
    }

    public IEnumerator<string> GetEnumerator()
    {
      return (IEnumerator<string>) this._results.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }
  }
}
