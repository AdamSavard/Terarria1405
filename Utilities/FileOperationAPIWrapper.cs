// Decompiled with JetBrains decompiler
// Type: Terraria.Utilities.FileOperationAPIWrapper
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using System;
using System.Runtime.InteropServices;

namespace Terraria.Utilities
{
  public static class FileOperationAPIWrapper
  {
    [DllImport("shell32.dll", CharSet = CharSet.Auto)]
    private static extern int SHFileOperation(ref FileOperationAPIWrapper.SHFILEOPSTRUCT FileOp);

    private static bool Send(string path, FileOperationAPIWrapper.FileOperationFlags flags)
    {
      try
      {
        FileOperationAPIWrapper.SHFILEOPSTRUCT FileOp = new FileOperationAPIWrapper.SHFILEOPSTRUCT()
        {
          wFunc = FileOperationAPIWrapper.FileOperationType.FO_DELETE,
          pFrom = path + "\0\0",
          fFlags = FileOperationAPIWrapper.FileOperationFlags.FOF_ALLOWUNDO | flags
        };
        FileOperationAPIWrapper.SHFileOperation(ref FileOp);
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    private static bool Send(string path)
    {
      return FileOperationAPIWrapper.Send(path, FileOperationAPIWrapper.FileOperationFlags.FOF_NOCONFIRMATION | FileOperationAPIWrapper.FileOperationFlags.FOF_WANTNUKEWARNING);
    }

    public static bool MoveToRecycleBin(string path)
    {
      return FileOperationAPIWrapper.Send(path, FileOperationAPIWrapper.FileOperationFlags.FOF_SILENT | FileOperationAPIWrapper.FileOperationFlags.FOF_NOCONFIRMATION | FileOperationAPIWrapper.FileOperationFlags.FOF_NOERRORUI);
    }

    private static bool DeleteFile(string path, FileOperationAPIWrapper.FileOperationFlags flags)
    {
      try
      {
        FileOperationAPIWrapper.SHFILEOPSTRUCT FileOp = new FileOperationAPIWrapper.SHFILEOPSTRUCT()
        {
          wFunc = FileOperationAPIWrapper.FileOperationType.FO_DELETE,
          pFrom = path + "\0\0",
          fFlags = flags
        };
        FileOperationAPIWrapper.SHFileOperation(ref FileOp);
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    private static bool DeleteCompletelySilent(string path)
    {
      return FileOperationAPIWrapper.DeleteFile(path, FileOperationAPIWrapper.FileOperationFlags.FOF_SILENT | FileOperationAPIWrapper.FileOperationFlags.FOF_NOCONFIRMATION | FileOperationAPIWrapper.FileOperationFlags.FOF_NOERRORUI);
    }

    [Flags]
    private enum FileOperationFlags : ushort
    {
      FOF_SILENT = 4,
      FOF_NOCONFIRMATION = 16, // 0x0010
      FOF_ALLOWUNDO = 64, // 0x0040
      FOF_SIMPLEPROGRESS = 256, // 0x0100
      FOF_NOERRORUI = 1024, // 0x0400
      FOF_WANTNUKEWARNING = 16384, // 0x4000
    }

    private enum FileOperationType : uint
    {
      FO_MOVE = 1,
      FO_COPY = 2,
      FO_DELETE = 3,
      FO_RENAME = 4,
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Auto)]
    private struct SHFILEOPSTRUCT
    {
      public IntPtr hwnd;
      [MarshalAs(UnmanagedType.U4)]
      public FileOperationAPIWrapper.FileOperationType wFunc;
      public string pFrom;
      public string pTo;
      public FileOperationAPIWrapper.FileOperationFlags fFlags;
      [MarshalAs(UnmanagedType.Bool)]
      public bool fAnyOperationsAborted;
      public IntPtr hNameMappings;
      public string lpszProgressTitle;
    }
  }
}
