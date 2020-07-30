// Decompiled with JetBrains decompiler
// Type: Terraria.Cinematics.CinematicManager
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Terraria.Cinematics
{
  public class CinematicManager
  {
    public static CinematicManager Instance = new CinematicManager();
    private List<Film> _films = new List<Film>();

    public void Update(GameTime gameTime)
    {
      if (this._films.Count <= 0)
        return;
      if (!this._films[0].IsActive)
        this._films[0].OnBegin();
      if (!Main.hasFocus || Main.gamePaused || this._films[0].OnUpdate(gameTime))
        return;
      this._films[0].OnEnd();
      this._films.RemoveAt(0);
    }

    public void PlayFilm(Film film)
    {
      this._films.Add(film);
    }

    public void StopAll()
    {
    }
  }
}
