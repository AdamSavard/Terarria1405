// Decompiled with JetBrains decompiler
// Type: Terraria.Audio.SoundPlayer
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using System.Collections;
using System.Collections.Generic;

namespace Terraria.Audio
{
  public class SoundPlayer
  {
    // so this is basically a hashmap(double/float/int key -> ActiveSound) and SlotId is basically a key
    // SlotId.Invalid could be -1, really any unused key value
    private readonly SlotVector<ActiveSound> _trackedSounds = new SlotVector<ActiveSound>(4096);

    public SlotId Play(SoundStyle style, Vector2 position)
    {
      if (Main.dedServ || style == null || !style.IsTrackable)
        return (SlotId) SlotId.Invalid;
      return (double) Vector2.DistanceSquared(Main.screenPosition + new Vector2((float) (Main.screenWidth / 2), (float) (Main.screenHeight / 2)), position) > 100000000.0 ? (SlotId) SlotId.Invalid : this._trackedSounds.Add(new ActiveSound(style, position));
    }

    public SlotId Play(SoundStyle style)
    {
      return Main.dedServ || style == null || !style.IsTrackable ? (SlotId) SlotId.Invalid : this._trackedSounds.Add(new ActiveSound(style));
    }

    public ActiveSound GetActiveSound(SlotId id)
    {
      return !this._trackedSounds.Has(id) ? (ActiveSound) null : this._trackedSounds.get_Item(id);
    }

    public void PauseAll()
    {
      using (IEnumerator<SlotVector<ActiveSound>.ItemPair> enumerator = ((IEnumerable<SlotVector<ActiveSound>.ItemPair>) this._trackedSounds).GetEnumerator())
      {
        while (((IEnumerator) enumerator).MoveNext())
          ((ActiveSound) enumerator.Current.Value).Pause();
      }
    }

    public void ResumeAll()
    {
      using (IEnumerator<SlotVector<ActiveSound>.ItemPair> enumerator = ((IEnumerable<SlotVector<ActiveSound>.ItemPair>) this._trackedSounds).GetEnumerator())
      {
        while (((IEnumerator) enumerator).MoveNext())
          ((ActiveSound) enumerator.Current.Value).Resume();
      }
    }

    public void StopAll()
    {
      using (IEnumerator<SlotVector<ActiveSound>.ItemPair> enumerator = ((IEnumerable<SlotVector<ActiveSound>.ItemPair>) this._trackedSounds).GetEnumerator())
      {
        while (((IEnumerator) enumerator).MoveNext())
          ((ActiveSound) enumerator.Current.Value).Stop();
      }
      this._trackedSounds.Clear();
    }

    public void Update()
    {
      using (IEnumerator<SlotVector<ActiveSound>.ItemPair> enumerator = ((IEnumerable<SlotVector<ActiveSound>.ItemPair>) this._trackedSounds).GetEnumerator())
      {
        while (((IEnumerator) enumerator).MoveNext())
        {
          SlotVector<ActiveSound>.ItemPair current = enumerator.Current;
          try
          {
            ((ActiveSound) current.Value).Update();
            if (!((ActiveSound) current.Value).IsPlaying)
              this._trackedSounds.Remove((SlotId) current.Id);
          }
          catch
          {
            this._trackedSounds.Remove((SlotId) current.Id);
          }
        }
      }
    }

    public ActiveSound FindActiveSound(SoundStyle style)
    {
      using (IEnumerator<SlotVector<ActiveSound>.ItemPair> enumerator = ((IEnumerable<SlotVector<ActiveSound>.ItemPair>) this._trackedSounds).GetEnumerator())
      {
        while (((IEnumerator) enumerator).MoveNext())
        {
          SlotVector<ActiveSound>.ItemPair current = enumerator.Current;
          if (((ActiveSound) current.Value).Style == style)
            return (ActiveSound) current.Value;
        }
      }
      return (ActiveSound) null;
    }
  }
}
