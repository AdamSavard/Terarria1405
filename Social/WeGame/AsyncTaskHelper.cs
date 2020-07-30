// Decompiled with JetBrains decompiler
// Type: Terraria.Social.WeGame.AsyncTaskHelper
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using System;
using System.Threading.Tasks;

namespace Terraria.Social.WeGame
{
  public class AsyncTaskHelper
  {
    private CurrentThreadRunner _currentThreadRunner;

    private AsyncTaskHelper()
    {
      this._currentThreadRunner = new CurrentThreadRunner();
    }

    public void RunAsyncTaskAndReply(Action task, Action replay)
    {
      Task.Factory.StartNew((Action) (() =>
      {
        task();
        this._currentThreadRunner.Run(replay);
      }));
    }

    public void RunAsyncTask(Action task)
    {
      Task.Factory.StartNew(task);
    }
  }
}
