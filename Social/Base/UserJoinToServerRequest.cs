// Decompiled with JetBrains decompiler
// Type: Terraria.Social.Base.UserJoinToServerRequest
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using System;

namespace Terraria.Social.Base
{
  public abstract class UserJoinToServerRequest
  {
    internal string UserDisplayName { get; private set; }

    internal string UserFullIdentifier { get; private set; }

    public event Action OnAccepted;

    public event Action OnRejected;

    public UserJoinToServerRequest(string userDisplayName, string fullIdentifier)
    {
      this.UserDisplayName = userDisplayName;
      this.UserFullIdentifier = fullIdentifier;
    }

    public void Accept()
    {
      if (this.OnAccepted == null)
        return;
      this.OnAccepted();
    }

    public void Reject()
    {
      if (this.OnRejected == null)
        return;
      this.OnRejected();
    }

    public abstract bool IsValid();

    public abstract string GetUserWrapperText();
  }
}
