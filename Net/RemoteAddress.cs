// Decompiled with JetBrains decompiler
// Type: Terraria.Net.RemoteAddress
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

namespace Terraria.Net
{
  public abstract class RemoteAddress
  {
    public AddressType Type;

    public abstract string GetIdentifier();

    public abstract string GetFriendlyName();

    public abstract bool IsLocalHost();
  }
}
