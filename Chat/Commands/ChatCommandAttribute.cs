// Decompiled with JetBrains decompiler
// Type: Terraria.Chat.Commands.ChatCommandAttribute
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using System;

namespace Terraria.Chat.Commands
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
  public sealed class ChatCommandAttribute : Attribute
  {
    public readonly string Name;

    public ChatCommandAttribute(string name)
    {
      this.Name = name;
    }
  }
}
