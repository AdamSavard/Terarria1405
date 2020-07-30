// Decompiled with JetBrains decompiler
// Type: Terraria.WorldBuilding.GenerationProgress
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

namespace Terraria.WorldBuilding
{
  public class GenerationProgress
  {
    private string _message = "";
    public float CurrentPassWeight = 1f;
    private float _value;
    private float _totalProgress;
    public float TotalWeight;

    public string Message
    {
      get
      {
        return string.Format(this._message, (object) this.Value);
      }
      set
      {
        this._message = value.Replace("%", "{0:0.0%}");
      }
    }

    public float Value
    {
      set
      {
        this._value = Utils.Clamp<float>(value, 0.0f, 1f);
      }
      get
      {
        return this._value;
      }
    }

    public float TotalProgress
    {
      get
      {
        return (double) this.TotalWeight == 0.0 ? 0.0f : (this.Value * this.CurrentPassWeight + this._totalProgress) / this.TotalWeight;
      }
    }

    public void Set(float value)
    {
      this.Value = value;
    }

    public void Start(float weight)
    {
      this.CurrentPassWeight = weight;
      this._value = 0.0f;
    }

    public void End()
    {
      this._totalProgress += this.CurrentPassWeight;
      this._value = 0.0f;
    }
  }
}
