// Decompiled with JetBrains decompiler
// Type: Terraria.MessageBuffer
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.Achievements;
using Terraria.GameContent.Creative;
using Terraria.GameContent.Events;
using Terraria.GameContent.Golf;
using Terraria.GameContent.Tile_Entities;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Net;
using Terraria.Testing;
using Terraria.UI;

namespace Terraria
{
  public class MessageBuffer
  {
    public byte[] readBuffer = new byte[131070];
    public byte[] writeBuffer = new byte[131070];
    public PacketHistory History = new PacketHistory(100, (int) ushort.MaxValue);
    public const int readBufferMax = 131070;
    public const int writeBufferMax = 131070;
    public bool broadcast;
    public bool writeLocked;
    public int messageLength;
    public int totalData;
    public int whoAmI;
    public int spamCount;
    public int maxSpam;
    public bool checkBytes;
    public MemoryStream readerStream;
    public MemoryStream writerStream;
    public BinaryReader reader;
    public BinaryWriter writer;

    public static event TileChangeReceivedEvent OnTileChangeReceived;

    public void Reset()
    {
      Array.Clear((Array) this.readBuffer, 0, this.readBuffer.Length);
      Array.Clear((Array) this.writeBuffer, 0, this.writeBuffer.Length);
      this.writeLocked = false;
      this.messageLength = 0;
      this.totalData = 0;
      this.spamCount = 0;
      this.broadcast = false;
      this.checkBytes = false;
      this.ResetReader();
      this.ResetWriter();
    }

    public void ResetReader()
    {
      if (this.readerStream != null)
        this.readerStream.Close();
      this.readerStream = new MemoryStream(this.readBuffer);
      this.reader = new BinaryReader((Stream) this.readerStream);
    }

    public void ResetWriter()
    {
      if (this.writerStream != null)
        this.writerStream.Close();
      this.writerStream = new MemoryStream(this.writeBuffer);
      this.writer = new BinaryWriter((Stream) this.writerStream);
    }

    public void GetData(int start, int length, out int messageType)
    {
      if (this.whoAmI < 256)
        Netplay.Clients[this.whoAmI].TimeOutTimer = 0;
      else
        Netplay.Connection.TimeOutTimer = 0;
      int bufferStart = start + 1;
      byte num1 = this.readBuffer[start];
      messageType = (int) num1;
      if (num1 >= (byte) 140)
        return;
      Main.ActiveNetDiagnosticsUI.CountReadMessage((int) num1, length);
      if (Main.netMode == 1 && Netplay.Connection.StatusMax > 0)
        ++Netplay.Connection.StatusCount;
      if (Main.verboseNetplay)
      {
        int num2 = start;
        while (num2 < start + length)
          ++num2;
        for (int index = start; index < start + length; ++index)
        {
          int num3 = (int) this.readBuffer[index];
        }
      }
      if (Main.netMode == 2 && num1 != (byte) 38 && Netplay.Clients[this.whoAmI].State == -1)
      {
        NetMessage.TrySendData(2, this.whoAmI, -1, Lang.mp[1].ToNetworkText(), 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      }
      else
      {
        if (Main.netMode == 2)
        {
          if (Netplay.Clients[this.whoAmI].State < 10 && num1 > (byte) 12 && (num1 != (byte) 93 && num1 != (byte) 16) && (num1 != (byte) 42 && num1 != (byte) 50 && (num1 != (byte) 38 && num1 != (byte) 68)))
            NetMessage.BootPlayer(this.whoAmI, Lang.mp[2].ToNetworkText());
          if (Netplay.Clients[this.whoAmI].State == 0 && num1 != (byte) 1)
            NetMessage.BootPlayer(this.whoAmI, Lang.mp[2].ToNetworkText());
        }
        if (this.reader == null)
          this.ResetReader();
        this.reader.BaseStream.Position = (long) bufferStart;
        switch (num1)
        {
          case 1:
            if (Main.netMode != 2)
              break;
            if (Main.dedServ && Netplay.IsBanned(Netplay.Clients[this.whoAmI].Socket.GetRemoteAddress()))
            {
              NetMessage.TrySendData(2, this.whoAmI, -1, Lang.mp[3].ToNetworkText(), 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
              break;
            }
            if (Netplay.Clients[this.whoAmI].State != 0)
              break;
            if (this.reader.ReadString() == "Terraria" + (object) 230)
            {
              if (string.IsNullOrEmpty(Netplay.ServerPassword))
              {
                Netplay.Clients[this.whoAmI].State = 1;
                NetMessage.TrySendData(3, this.whoAmI, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
                break;
              }
              Netplay.Clients[this.whoAmI].State = -1;
              NetMessage.TrySendData(37, this.whoAmI, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
              break;
            }
            NetMessage.TrySendData(2, this.whoAmI, -1, Lang.mp[4].ToNetworkText(), 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            break;
          case 2:
            if (Main.netMode != 1)
              break;
            Netplay.Disconnect = true;
            Main.statusText = NetworkText.Deserialize(this.reader).ToString();
            break;
          case 3:
            if (Main.netMode != 1)
              break;
            if (Netplay.Connection.State == 1)
              Netplay.Connection.State = 2;
            int number1 = (int) this.reader.ReadByte();
            if (number1 != Main.myPlayer)
            {
              Main.player[number1] = Main.ActivePlayerFileData.Player;
              Main.player[Main.myPlayer] = new Player();
            }
            Main.player[number1].whoAmI = number1;
            Main.myPlayer = number1;
            Player player1 = Main.player[number1];
            NetMessage.TrySendData(4, -1, -1, (NetworkText) null, number1, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            NetMessage.TrySendData(68, -1, -1, (NetworkText) null, number1, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            NetMessage.TrySendData(16, -1, -1, (NetworkText) null, number1, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            NetMessage.TrySendData(42, -1, -1, (NetworkText) null, number1, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            NetMessage.TrySendData(50, -1, -1, (NetworkText) null, number1, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            for (int index = 0; index < 59; ++index)
              NetMessage.TrySendData(5, -1, -1, (NetworkText) null, number1, (float) index, (float) player1.inventory[index].prefix, 0.0f, 0, 0, 0);
            for (int index = 0; index < player1.armor.Length; ++index)
              NetMessage.TrySendData(5, -1, -1, (NetworkText) null, number1, (float) (59 + index), (float) player1.armor[index].prefix, 0.0f, 0, 0, 0);
            for (int index = 0; index < player1.dye.Length; ++index)
              NetMessage.TrySendData(5, -1, -1, (NetworkText) null, number1, (float) (58 + player1.armor.Length + 1 + index), (float) player1.dye[index].prefix, 0.0f, 0, 0, 0);
            for (int index = 0; index < player1.miscEquips.Length; ++index)
              NetMessage.TrySendData(5, -1, -1, (NetworkText) null, number1, (float) (58 + player1.armor.Length + player1.dye.Length + 1 + index), (float) player1.miscEquips[index].prefix, 0.0f, 0, 0, 0);
            for (int index = 0; index < player1.miscDyes.Length; ++index)
              NetMessage.TrySendData(5, -1, -1, (NetworkText) null, number1, (float) (58 + player1.armor.Length + player1.dye.Length + player1.miscEquips.Length + 1 + index), (float) player1.miscDyes[index].prefix, 0.0f, 0, 0, 0);
            for (int index = 0; index < player1.bank.item.Length; ++index)
              NetMessage.TrySendData(5, -1, -1, (NetworkText) null, number1, (float) (58 + player1.armor.Length + player1.dye.Length + player1.miscEquips.Length + player1.miscDyes.Length + 1 + index), (float) player1.bank.item[index].prefix, 0.0f, 0, 0, 0);
            for (int index = 0; index < player1.bank2.item.Length; ++index)
              NetMessage.TrySendData(5, -1, -1, (NetworkText) null, number1, (float) (58 + player1.armor.Length + player1.dye.Length + player1.miscEquips.Length + player1.miscDyes.Length + player1.bank.item.Length + 1 + index), (float) player1.bank2.item[index].prefix, 0.0f, 0, 0, 0);
            NetMessage.TrySendData(5, -1, -1, (NetworkText) null, number1, (float) (58 + player1.armor.Length + player1.dye.Length + player1.miscEquips.Length + player1.miscDyes.Length + player1.bank.item.Length + player1.bank2.item.Length + 1), (float) player1.trashItem.prefix, 0.0f, 0, 0, 0);
            for (int index = 0; index < player1.bank3.item.Length; ++index)
              NetMessage.TrySendData(5, -1, -1, (NetworkText) null, number1, (float) (58 + player1.armor.Length + player1.dye.Length + player1.miscEquips.Length + player1.miscDyes.Length + player1.bank.item.Length + player1.bank2.item.Length + 2 + index), (float) player1.bank3.item[index].prefix, 0.0f, 0, 0, 0);
            for (int index = 0; index < player1.bank4.item.Length; ++index)
              NetMessage.TrySendData(5, -1, -1, (NetworkText) null, number1, (float) (58 + player1.armor.Length + player1.dye.Length + player1.miscEquips.Length + player1.miscDyes.Length + player1.bank.item.Length + player1.bank2.item.Length + player1.bank3.item.Length + 2 + index), (float) player1.bank4.item[index].prefix, 0.0f, 0, 0, 0);
            NetMessage.TrySendData(6, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            if (Netplay.Connection.State != 2)
              break;
            Netplay.Connection.State = 3;
            break;
          case 4:
            int number2 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              number2 = this.whoAmI;
            if (number2 == Main.myPlayer && !Main.ServerSideCharacter)
              break;
            Player player2 = Main.player[number2];
            player2.whoAmI = number2;
            player2.skinVariant = (int) this.reader.ReadByte();
            player2.skinVariant = (int) MathHelper.Clamp((float) player2.skinVariant, 0.0f, 11f);
            player2.hair = (int) this.reader.ReadByte();
            if (player2.hair >= 162)
              player2.hair = 0;
            player2.name = this.reader.ReadString().Trim().Trim();
            player2.hairDye = this.reader.ReadByte();
            BitsByte bitsByte1 = (BitsByte) this.reader.ReadByte();
            for (int index = 0; index < 8; ++index)
              player2.hideVisibleAccessory[index] = bitsByte1[index];
            bitsByte1 = (BitsByte) this.reader.ReadByte();
            for (int index = 0; index < 2; ++index)
              player2.hideVisibleAccessory[index + 8] = bitsByte1[index];
            player2.hideMisc = (BitsByte) this.reader.ReadByte();
            player2.hairColor = this.reader.ReadRGB();
            player2.skinColor = this.reader.ReadRGB();
            player2.eyeColor = this.reader.ReadRGB();
            player2.shirtColor = this.reader.ReadRGB();
            player2.underShirtColor = this.reader.ReadRGB();
            player2.pantsColor = this.reader.ReadRGB();
            player2.shoeColor = this.reader.ReadRGB();
            BitsByte bitsByte2 = (BitsByte) this.reader.ReadByte();
            player2.difficulty = (byte) 0;
            if (bitsByte2[0])
              player2.difficulty = (byte) 1;
            if (bitsByte2[1])
              player2.difficulty = (byte) 2;
            if (bitsByte2[3])
              player2.difficulty = (byte) 3;
            if (player2.difficulty > (byte) 3)
              player2.difficulty = (byte) 3;
            player2.extraAccessory = bitsByte2[2];
            BitsByte bitsByte3 = (BitsByte) this.reader.ReadByte();
            player2.UsingBiomeTorches = bitsByte3[0];
            player2.happyFunTorchTime = bitsByte3[1];
            if (Main.netMode != 2)
              break;
            bool flag1 = false;
            if (Netplay.Clients[this.whoAmI].State < 10)
            {
              for (int index = 0; index < (int) byte.MaxValue; ++index)
              {
                if (index != number2 && player2.name == Main.player[index].name && Netplay.Clients[index].IsActive)
                  flag1 = true;
              }
            }
            if (flag1)
            {
              NetMessage.TrySendData(2, this.whoAmI, -1, NetworkText.FromKey(Lang.mp[5].Key, (object) player2.name), 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
              break;
            }
            if (player2.name.Length > Player.nameLen)
            {
              NetMessage.TrySendData(2, this.whoAmI, -1, NetworkText.FromKey("Net.NameTooLong"), 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
              break;
            }
            if (player2.name == "")
            {
              NetMessage.TrySendData(2, this.whoAmI, -1, NetworkText.FromKey("Net.EmptyName"), 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
              break;
            }
            if (player2.difficulty == (byte) 3 && !Main.GameModeInfo.IsJourneyMode)
            {
              NetMessage.TrySendData(2, this.whoAmI, -1, NetworkText.FromKey("Net.PlayerIsCreativeAndWorldIsNotCreative"), 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
              break;
            }
            if (player2.difficulty != (byte) 3 && Main.GameModeInfo.IsJourneyMode)
            {
              NetMessage.TrySendData(2, this.whoAmI, -1, NetworkText.FromKey("Net.PlayerIsNotCreativeAndWorldIsCreative"), 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
              break;
            }
            Netplay.Clients[this.whoAmI].Name = player2.name;
            Netplay.Clients[this.whoAmI].Name = player2.name;
            NetMessage.TrySendData(4, -1, this.whoAmI, (NetworkText) null, number2, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            break;
          case 5:
            int number3 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              number3 = this.whoAmI;
            if (number3 == Main.myPlayer && !Main.ServerSideCharacter && !Main.player[number3].HasLockedInventory())
              break;
            Player player3 = Main.player[number3];
            lock (player3)
            {
              int index1 = (int) this.reader.ReadInt16();
              int num2 = (int) this.reader.ReadInt16();
              int pre = (int) this.reader.ReadByte();
              int type1 = (int) this.reader.ReadInt16();
              Item[] objArray1 = (Item[]) null;
              Item[] objArray2 = (Item[]) null;
              int index2 = 0;
              bool flag2 = false;
              if (index1 > 58 + player3.armor.Length + player3.dye.Length + player3.miscEquips.Length + player3.miscDyes.Length + player3.bank.item.Length + player3.bank2.item.Length + player3.bank3.item.Length + 1)
              {
                index2 = index1 - 58 - (player3.armor.Length + player3.dye.Length + player3.miscEquips.Length + player3.miscDyes.Length + player3.bank.item.Length + player3.bank2.item.Length + player3.bank3.item.Length + 1) - 1;
                objArray1 = player3.bank4.item;
                objArray2 = Main.clientPlayer.bank4.item;
              }
              else if (index1 > 58 + player3.armor.Length + player3.dye.Length + player3.miscEquips.Length + player3.miscDyes.Length + player3.bank.item.Length + player3.bank2.item.Length + 1)
              {
                index2 = index1 - 58 - (player3.armor.Length + player3.dye.Length + player3.miscEquips.Length + player3.miscDyes.Length + player3.bank.item.Length + player3.bank2.item.Length + 1) - 1;
                objArray1 = player3.bank3.item;
                objArray2 = Main.clientPlayer.bank3.item;
              }
              else if (index1 > 58 + player3.armor.Length + player3.dye.Length + player3.miscEquips.Length + player3.miscDyes.Length + player3.bank.item.Length + player3.bank2.item.Length)
                flag2 = true;
              else if (index1 > 58 + player3.armor.Length + player3.dye.Length + player3.miscEquips.Length + player3.miscDyes.Length + player3.bank.item.Length)
              {
                index2 = index1 - 58 - (player3.armor.Length + player3.dye.Length + player3.miscEquips.Length + player3.miscDyes.Length + player3.bank.item.Length) - 1;
                objArray1 = player3.bank2.item;
                objArray2 = Main.clientPlayer.bank2.item;
              }
              else if (index1 > 58 + player3.armor.Length + player3.dye.Length + player3.miscEquips.Length + player3.miscDyes.Length)
              {
                index2 = index1 - 58 - (player3.armor.Length + player3.dye.Length + player3.miscEquips.Length + player3.miscDyes.Length) - 1;
                objArray1 = player3.bank.item;
                objArray2 = Main.clientPlayer.bank.item;
              }
              else if (index1 > 58 + player3.armor.Length + player3.dye.Length + player3.miscEquips.Length)
              {
                index2 = index1 - 58 - (player3.armor.Length + player3.dye.Length + player3.miscEquips.Length) - 1;
                objArray1 = player3.miscDyes;
                objArray2 = Main.clientPlayer.miscDyes;
              }
              else if (index1 > 58 + player3.armor.Length + player3.dye.Length)
              {
                index2 = index1 - 58 - (player3.armor.Length + player3.dye.Length) - 1;
                objArray1 = player3.miscEquips;
                objArray2 = Main.clientPlayer.miscEquips;
              }
              else if (index1 > 58 + player3.armor.Length)
              {
                index2 = index1 - 58 - player3.armor.Length - 1;
                objArray1 = player3.dye;
                objArray2 = Main.clientPlayer.dye;
              }
              else if (index1 > 58)
              {
                index2 = index1 - 58 - 1;
                objArray1 = player3.armor;
                objArray2 = Main.clientPlayer.armor;
              }
              else
              {
                index2 = index1;
                objArray1 = player3.inventory;
                objArray2 = Main.clientPlayer.inventory;
              }
              if (flag2)
              {
                player3.trashItem = new Item();
                player3.trashItem.netDefaults(type1);
                player3.trashItem.stack = num2;
                player3.trashItem.Prefix(pre);
                if (number3 == Main.myPlayer && !Main.ServerSideCharacter)
                  Main.clientPlayer.trashItem = player3.trashItem.Clone();
              }
              else if (index1 <= 58)
              {
                int type2 = objArray1[index2].type;
                int stack = objArray1[index2].stack;
                objArray1[index2] = new Item();
                objArray1[index2].netDefaults(type1);
                objArray1[index2].stack = num2;
                objArray1[index2].Prefix(pre);
                if (number3 == Main.myPlayer && !Main.ServerSideCharacter)
                  objArray2[index2] = objArray1[index2].Clone();
                if (number3 == Main.myPlayer && index2 == 58)
                  Main.mouseItem = objArray1[index2].Clone();
                if (number3 == Main.myPlayer && Main.netMode == 1)
                {
                  Main.player[number3].inventoryChestStack[index1] = false;
                  if (objArray1[index2].stack != stack || objArray1[index2].type != type2)
                  {
                    Recipe.FindRecipes(true);
                    SoundEngine.PlaySound(7, -1, -1, 1, 1f, 0.0f);
                  }
                }
              }
              else
              {
                objArray1[index2] = new Item();
                objArray1[index2].netDefaults(type1);
                objArray1[index2].stack = num2;
                objArray1[index2].Prefix(pre);
                if (number3 == Main.myPlayer && !Main.ServerSideCharacter)
                  objArray2[index2] = objArray1[index2].Clone();
              }
              if (Main.netMode != 2 || number3 != this.whoAmI || index1 > 58 + player3.armor.Length + player3.dye.Length + player3.miscEquips.Length + player3.miscDyes.Length)
                break;
              NetMessage.TrySendData(5, -1, this.whoAmI, (NetworkText) null, number3, (float) index1, (float) pre, 0.0f, 0, 0, 0);
              break;
            }
          case 6:
            if (Main.netMode != 2)
              break;
            if (Netplay.Clients[this.whoAmI].State == 1)
              Netplay.Clients[this.whoAmI].State = 2;
            NetMessage.TrySendData(7, this.whoAmI, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            Main.SyncAnInvasion(this.whoAmI);
            break;
          case 7:
            if (Main.netMode != 1)
              break;
            Main.time = (double) this.reader.ReadInt32();
            BitsByte bitsByte4 = (BitsByte) this.reader.ReadByte();
            Main.dayTime = bitsByte4[0];
            Main.bloodMoon = bitsByte4[1];
            Main.eclipse = bitsByte4[2];
            Main.moonPhase = (int) this.reader.ReadByte();
            Main.maxTilesX = (int) this.reader.ReadInt16();
            Main.maxTilesY = (int) this.reader.ReadInt16();
            Main.spawnTileX = (int) this.reader.ReadInt16();
            Main.spawnTileY = (int) this.reader.ReadInt16();
            Main.worldSurface = (double) this.reader.ReadInt16();
            Main.rockLayer = (double) this.reader.ReadInt16();
            Main.worldID = this.reader.ReadInt32();
            Main.worldName = this.reader.ReadString();
            Main.GameMode = (int) this.reader.ReadByte();
            Main.ActiveWorldFileData.UniqueId = new Guid(this.reader.ReadBytes(16));
            Main.ActiveWorldFileData.WorldGeneratorVersion = this.reader.ReadUInt64();
            Main.moonType = (int) this.reader.ReadByte();
            WorldGen.setBG(0, (int) this.reader.ReadByte());
            WorldGen.setBG(10, (int) this.reader.ReadByte());
            WorldGen.setBG(11, (int) this.reader.ReadByte());
            WorldGen.setBG(12, (int) this.reader.ReadByte());
            WorldGen.setBG(1, (int) this.reader.ReadByte());
            WorldGen.setBG(2, (int) this.reader.ReadByte());
            WorldGen.setBG(3, (int) this.reader.ReadByte());
            WorldGen.setBG(4, (int) this.reader.ReadByte());
            WorldGen.setBG(5, (int) this.reader.ReadByte());
            WorldGen.setBG(6, (int) this.reader.ReadByte());
            WorldGen.setBG(7, (int) this.reader.ReadByte());
            WorldGen.setBG(8, (int) this.reader.ReadByte());
            WorldGen.setBG(9, (int) this.reader.ReadByte());
            Main.iceBackStyle = (int) this.reader.ReadByte();
            Main.jungleBackStyle = (int) this.reader.ReadByte();
            Main.hellBackStyle = (int) this.reader.ReadByte();
            Main.windSpeedTarget = this.reader.ReadSingle();
            Main.numClouds = (int) this.reader.ReadByte();
            for (int index = 0; index < 3; ++index)
              Main.treeX[index] = this.reader.ReadInt32();
            for (int index = 0; index < 4; ++index)
              Main.treeStyle[index] = (int) this.reader.ReadByte();
            for (int index = 0; index < 3; ++index)
              Main.caveBackX[index] = this.reader.ReadInt32();
            for (int index = 0; index < 4; ++index)
              Main.caveBackStyle[index] = (int) this.reader.ReadByte();
            WorldGen.TreeTops.SyncReceive(this.reader);
            WorldGen.BackgroundsCache.UpdateCache();
            Main.maxRaining = this.reader.ReadSingle();
            Main.raining = (double) Main.maxRaining > 0.0;
            BitsByte bitsByte5 = (BitsByte) this.reader.ReadByte();
            WorldGen.shadowOrbSmashed = bitsByte5[0];
            NPC.downedBoss1 = bitsByte5[1];
            NPC.downedBoss2 = bitsByte5[2];
            NPC.downedBoss3 = bitsByte5[3];
            Main.hardMode = bitsByte5[4];
            NPC.downedClown = bitsByte5[5];
            Main.ServerSideCharacter = bitsByte5[6];
            NPC.downedPlantBoss = bitsByte5[7];
            BitsByte bitsByte6 = (BitsByte) this.reader.ReadByte();
            NPC.downedMechBoss1 = bitsByte6[0];
            NPC.downedMechBoss2 = bitsByte6[1];
            NPC.downedMechBoss3 = bitsByte6[2];
            NPC.downedMechBossAny = bitsByte6[3];
            Main.cloudBGActive = bitsByte6[4] ? 1f : 0.0f;
            WorldGen.crimson = bitsByte6[5];
            Main.pumpkinMoon = bitsByte6[6];
            Main.snowMoon = bitsByte6[7];
            BitsByte bitsByte7 = (BitsByte) this.reader.ReadByte();
            Main.fastForwardTime = bitsByte7[1];
            Main.UpdateTimeRate();
            int num3 = bitsByte7[2] ? 1 : 0;
            NPC.downedSlimeKing = bitsByte7[3];
            NPC.downedQueenBee = bitsByte7[4];
            NPC.downedFishron = bitsByte7[5];
            NPC.downedMartians = bitsByte7[6];
            NPC.downedAncientCultist = bitsByte7[7];
            BitsByte bitsByte8 = (BitsByte) this.reader.ReadByte();
            NPC.downedMoonlord = bitsByte8[0];
            NPC.downedHalloweenKing = bitsByte8[1];
            NPC.downedHalloweenTree = bitsByte8[2];
            NPC.downedChristmasIceQueen = bitsByte8[3];
            NPC.downedChristmasSantank = bitsByte8[4];
            NPC.downedChristmasTree = bitsByte8[5];
            NPC.downedGolemBoss = bitsByte8[6];
            BirthdayParty.ManualParty = bitsByte8[7];
            BitsByte bitsByte9 = (BitsByte) this.reader.ReadByte();
            NPC.downedPirates = bitsByte9[0];
            NPC.downedFrost = bitsByte9[1];
            NPC.downedGoblins = bitsByte9[2];
            Sandstorm.Happening = bitsByte9[3];
            DD2Event.Ongoing = bitsByte9[4];
            DD2Event.DownedInvasionT1 = bitsByte9[5];
            DD2Event.DownedInvasionT2 = bitsByte9[6];
            DD2Event.DownedInvasionT3 = bitsByte9[7];
            BitsByte bitsByte10 = (BitsByte) this.reader.ReadByte();
            NPC.combatBookWasUsed = bitsByte10[0];
            LanternNight.ManualLanterns = bitsByte10[1];
            NPC.downedTowerSolar = bitsByte10[2];
            NPC.downedTowerVortex = bitsByte10[3];
            NPC.downedTowerNebula = bitsByte10[4];
            NPC.downedTowerStardust = bitsByte10[5];
            Main.forceHalloweenForToday = bitsByte10[6];
            Main.forceXMasForToday = bitsByte10[7];
            BitsByte bitsByte11 = (BitsByte) this.reader.ReadByte();
            NPC.boughtCat = bitsByte11[0];
            NPC.boughtDog = bitsByte11[1];
            NPC.boughtBunny = bitsByte11[2];
            NPC.freeCake = bitsByte11[3];
            Main.drunkWorld = bitsByte11[4];
            NPC.downedEmpressOfLight = bitsByte11[5];
            NPC.downedQueenSlime = bitsByte11[6];
            Main.getGoodWorld = bitsByte11[7];
            WorldGen.SavedOreTiers.Copper = (int) this.reader.ReadInt16();
            WorldGen.SavedOreTiers.Iron = (int) this.reader.ReadInt16();
            WorldGen.SavedOreTiers.Silver = (int) this.reader.ReadInt16();
            WorldGen.SavedOreTiers.Gold = (int) this.reader.ReadInt16();
            WorldGen.SavedOreTiers.Cobalt = (int) this.reader.ReadInt16();
            WorldGen.SavedOreTiers.Mythril = (int) this.reader.ReadInt16();
            WorldGen.SavedOreTiers.Adamantite = (int) this.reader.ReadInt16();
            if (num3 != 0)
              Main.StartSlimeRain(true);
            else
              Main.StopSlimeRain(true);
            Main.invasionType = (int) this.reader.ReadSByte();
            Main.LobbyId = this.reader.ReadUInt64();
            Sandstorm.IntendedSeverity = this.reader.ReadSingle();
            if (Netplay.Connection.State == 3)
            {
              Main.windSpeedCurrent = Main.windSpeedTarget;
              Netplay.Connection.State = 4;
            }
            Main.checkHalloween();
            Main.checkXMas();
            break;
          case 8:
            if (Main.netMode != 2)
              break;
            int num4 = this.reader.ReadInt32();
            int y1 = this.reader.ReadInt32();
            bool flag3 = true;
            if (num4 == -1 || y1 == -1)
              flag3 = false;
            else if (num4 < 10 || num4 > Main.maxTilesX - 10)
              flag3 = false;
            else if (y1 < 10 || y1 > Main.maxTilesY - 10)
              flag3 = false;
            int number4 = Netplay.GetSectionX(Main.spawnTileX) - 2;
            int num5 = Netplay.GetSectionY(Main.spawnTileY) - 1;
            int num6 = number4 + 5;
            int num7 = num5 + 3;
            if (number4 < 0)
              number4 = 0;
            if (num6 >= Main.maxSectionsX)
              num6 = Main.maxSectionsX - 1;
            if (num5 < 0)
              num5 = 0;
            if (num7 >= Main.maxSectionsY)
              num7 = Main.maxSectionsY - 1;
            int num8 = (num6 - number4) * (num7 - num5);
            List<Point> dontInclude = new List<Point>();
            for (int x = number4; x < num6; ++x)
            {
              for (int y2 = num5; y2 < num7; ++y2)
                dontInclude.Add(new Point(x, y2));
            }
            int num9 = -1;
            int num10 = -1;
            if (flag3)
            {
              num4 = Netplay.GetSectionX(num4) - 2;
              y1 = Netplay.GetSectionY(y1) - 1;
              num9 = num4 + 5;
              num10 = y1 + 3;
              if (num4 < 0)
                num4 = 0;
              if (num9 >= Main.maxSectionsX)
                num9 = Main.maxSectionsX - 1;
              if (y1 < 0)
                y1 = 0;
              if (num10 >= Main.maxSectionsY)
                num10 = Main.maxSectionsY - 1;
              for (int x = num4; x < num9; ++x)
              {
                for (int y2 = y1; y2 < num10; ++y2)
                {
                  if (x < number4 || x >= num6 || (y2 < num5 || y2 >= num7))
                  {
                    dontInclude.Add(new Point(x, y2));
                    ++num8;
                  }
                }
              }
            }
            int num11 = 1;
            List<Point> portals;
            List<Point> portalCenters;
            PortalHelper.SyncPortalsOnPlayerJoin(this.whoAmI, 1, dontInclude, out portals, out portalCenters);
            int number5 = num8 + portals.Count;
            if (Netplay.Clients[this.whoAmI].State == 2)
              Netplay.Clients[this.whoAmI].State = 3;
            NetMessage.TrySendData(9, this.whoAmI, -1, Lang.inter[44].ToNetworkText(), number5, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            Netplay.Clients[this.whoAmI].StatusText2 = Language.GetTextValue("Net.IsReceivingTileData");
            Netplay.Clients[this.whoAmI].StatusMax += number5;
            for (int sectionX = number4; sectionX < num6; ++sectionX)
            {
              for (int sectionY = num5; sectionY < num7; ++sectionY)
                NetMessage.SendSection(this.whoAmI, sectionX, sectionY, false);
            }
            NetMessage.TrySendData(11, this.whoAmI, -1, (NetworkText) null, number4, (float) num5, (float) (num6 - 1), (float) (num7 - 1), 0, 0, 0);
            if (flag3)
            {
              for (int sectionX = num4; sectionX < num9; ++sectionX)
              {
                for (int sectionY = y1; sectionY < num10; ++sectionY)
                  NetMessage.SendSection(this.whoAmI, sectionX, sectionY, true);
              }
              NetMessage.TrySendData(11, this.whoAmI, -1, (NetworkText) null, num4, (float) y1, (float) (num9 - 1), (float) (num10 - 1), 0, 0, 0);
            }
            for (int index = 0; index < portals.Count; ++index)
              NetMessage.SendSection(this.whoAmI, portals[index].X, portals[index].Y, true);
            for (int index = 0; index < portalCenters.Count; ++index)
              NetMessage.TrySendData(11, this.whoAmI, -1, (NetworkText) null, portalCenters[index].X - num11, (float) (portalCenters[index].Y - num11), (float) (portalCenters[index].X + num11 + 1), (float) (portalCenters[index].Y + num11 + 1), 0, 0, 0);
            for (int number6 = 0; number6 < 400; ++number6)
            {
              if (Main.item[number6].active)
              {
                NetMessage.TrySendData(21, this.whoAmI, -1, (NetworkText) null, number6, 0.0f, 0.0f, 0.0f, 0, 0, 0);
                NetMessage.TrySendData(22, this.whoAmI, -1, (NetworkText) null, number6, 0.0f, 0.0f, 0.0f, 0, 0, 0);
              }
            }
            for (int number6 = 0; number6 < 200; ++number6)
            {
              if (Main.npc[number6].active)
                NetMessage.TrySendData(23, this.whoAmI, -1, (NetworkText) null, number6, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            }
            for (int number6 = 0; number6 < 1000; ++number6)
            {
              if (Main.projectile[number6].active && (Main.projPet[Main.projectile[number6].type] || Main.projectile[number6].netImportant))
                NetMessage.TrySendData(27, this.whoAmI, -1, (NetworkText) null, number6, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            }
            for (int number6 = 0; number6 < 289; ++number6)
              NetMessage.TrySendData(83, this.whoAmI, -1, (NetworkText) null, number6, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            NetMessage.TrySendData(49, this.whoAmI, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            NetMessage.TrySendData(57, this.whoAmI, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            NetMessage.TrySendData(7, this.whoAmI, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            NetMessage.TrySendData(103, -1, -1, (NetworkText) null, NPC.MoonLordCountdown, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            NetMessage.TrySendData(101, this.whoAmI, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            NetMessage.TrySendData(136, this.whoAmI, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            Main.BestiaryTracker.OnPlayerJoining(this.whoAmI);
            CreativePowerManager.Instance.SyncThingsToJoiningPlayer(this.whoAmI);
            Main.PylonSystem.OnPlayerJoining(this.whoAmI);
            break;
          case 9:
            if (Main.netMode != 1)
              break;
            Netplay.Connection.StatusMax += this.reader.ReadInt32();
            Netplay.Connection.StatusText = NetworkText.Deserialize(this.reader).ToString();
            Netplay.Connection.StatusTextFlags = (BitsByte) this.reader.ReadByte();
            break;
          case 10:
            if (Main.netMode != 1)
              break;
            NetMessage.DecompressTileBlock(this.readBuffer, bufferStart, length);
            break;
          case 11:
            if (Main.netMode != 1)
              break;
            WorldGen.SectionTileFrame((int) this.reader.ReadInt16(), (int) this.reader.ReadInt16(), (int) this.reader.ReadInt16(), (int) this.reader.ReadInt16());
            break;
          case 12:
            int index3 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              index3 = this.whoAmI;
            Player player4 = Main.player[index3];
            player4.SpawnX = (int) this.reader.ReadInt16();
            player4.SpawnY = (int) this.reader.ReadInt16();
            player4.respawnTimer = this.reader.ReadInt32();
            if (player4.respawnTimer > 0)
              player4.dead = true;
            PlayerSpawnContext context = (PlayerSpawnContext) this.reader.ReadByte();
            player4.Spawn(context);
            if (index3 == Main.myPlayer && Main.netMode != 2)
            {
              Main.ActivePlayerFileData.StartPlayTimer();
              Player.Hooks.EnterWorld(Main.myPlayer);
            }
            if (Main.netMode != 2 || Netplay.Clients[this.whoAmI].State < 3)
              break;
            if (Netplay.Clients[this.whoAmI].State == 3)
            {
              Netplay.Clients[this.whoAmI].State = 10;
              NetMessage.buffer[this.whoAmI].broadcast = true;
              NetMessage.SyncConnectedPlayer(this.whoAmI);
              bool flag2 = NetMessage.DoesPlayerSlotCountAsAHost(this.whoAmI);
              Main.countsAsHostForGameplay[this.whoAmI] = flag2;
              if (NetMessage.DoesPlayerSlotCountAsAHost(this.whoAmI))
                NetMessage.TrySendData(139, this.whoAmI, -1, (NetworkText) null, this.whoAmI, (float) flag2.ToInt(), 0.0f, 0.0f, 0, 0, 0);
              NetMessage.TrySendData(12, -1, this.whoAmI, (NetworkText) null, this.whoAmI, (float) (byte) context, 0.0f, 0.0f, 0, 0, 0);
              NetMessage.TrySendData(74, this.whoAmI, -1, NetworkText.FromLiteral(Main.player[this.whoAmI].name), Main.anglerQuest, 0.0f, 0.0f, 0.0f, 0, 0, 0);
              NetMessage.TrySendData(129, this.whoAmI, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
              NetMessage.greetPlayer(this.whoAmI);
              break;
            }
            NetMessage.TrySendData(12, -1, this.whoAmI, (NetworkText) null, this.whoAmI, (float) (byte) context, 0.0f, 0.0f, 0, 0, 0);
            break;
          case 13:
            int number7 = (int) this.reader.ReadByte();
            if (number7 == Main.myPlayer && !Main.ServerSideCharacter)
              break;
            if (Main.netMode == 2)
              number7 = this.whoAmI;
            Player player5 = Main.player[number7];
            BitsByte bitsByte12 = (BitsByte) this.reader.ReadByte();
            BitsByte bitsByte13 = (BitsByte) this.reader.ReadByte();
            BitsByte bitsByte14 = (BitsByte) this.reader.ReadByte();
            BitsByte bitsByte15 = (BitsByte) this.reader.ReadByte();
            player5.controlUp = bitsByte12[0];
            player5.controlDown = bitsByte12[1];
            player5.controlLeft = bitsByte12[2];
            player5.controlRight = bitsByte12[3];
            player5.controlJump = bitsByte12[4];
            player5.controlUseItem = bitsByte12[5];
            player5.direction = bitsByte12[6] ? 1 : -1;
            if (bitsByte13[0])
            {
              player5.pulley = true;
              player5.pulleyDir = bitsByte13[1] ? (byte) 2 : (byte) 1;
            }
            else
              player5.pulley = false;
            player5.vortexStealthActive = bitsByte13[3];
            player5.gravDir = bitsByte13[4] ? 1f : -1f;
            player5.TryTogglingShield(bitsByte13[5]);
            player5.ghost = bitsByte13[6];
            player5.selectedItem = (int) this.reader.ReadByte();
            player5.position = this.reader.ReadVector2();
            if (bitsByte13[2])
              player5.velocity = this.reader.ReadVector2();
            else
              player5.velocity = Vector2.Zero;
            if (bitsByte14[6])
            {
              player5.PotionOfReturnOriginalUsePosition = new Vector2?(this.reader.ReadVector2());
              player5.PotionOfReturnHomePosition = new Vector2?(this.reader.ReadVector2());
            }
            else
            {
              player5.PotionOfReturnOriginalUsePosition = new Vector2?();
              player5.PotionOfReturnHomePosition = new Vector2?();
            }
            player5.tryKeepingHoveringUp = bitsByte14[0];
            player5.IsVoidVaultEnabled = bitsByte14[1];
            player5.sitting.isSitting = bitsByte14[2];
            player5.downedDD2EventAnyDifficulty = bitsByte14[3];
            player5.isPettingAnimal = bitsByte14[4];
            player5.isTheAnimalBeingPetSmall = bitsByte14[5];
            player5.tryKeepingHoveringDown = bitsByte14[7];
            player5.sleeping.SetIsSleepingAndAdjustPlayerRotation(player5, bitsByte15[0]);
            if (Main.netMode != 2 || Netplay.Clients[this.whoAmI].State != 10)
              break;
            NetMessage.TrySendData(13, -1, this.whoAmI, (NetworkText) null, number7, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            break;
          case 14:
            int playerIndex = (int) this.reader.ReadByte();
            int num12 = (int) this.reader.ReadByte();
            if (Main.netMode != 1)
              break;
            int num13 = Main.player[playerIndex].active ? 1 : 0;
            if (num12 == 1)
            {
              if (!Main.player[playerIndex].active)
                Main.player[playerIndex] = new Player();
              Main.player[playerIndex].active = true;
            }
            else
              Main.player[playerIndex].active = false;
            int num14 = Main.player[playerIndex].active ? 1 : 0;
            if (num13 == num14)
              break;
            if (Main.player[playerIndex].active)
            {
              Player.Hooks.PlayerConnect(playerIndex);
              break;
            }
            Player.Hooks.PlayerDisconnect(playerIndex);
            break;
          case 15:
            break;
          case 16:
            int number8 = (int) this.reader.ReadByte();
            if (number8 == Main.myPlayer && !Main.ServerSideCharacter)
              break;
            if (Main.netMode == 2)
              number8 = this.whoAmI;
            Player player6 = Main.player[number8];
            player6.statLife = (int) this.reader.ReadInt16();
            player6.statLifeMax = (int) this.reader.ReadInt16();
            if (player6.statLifeMax < 100)
              player6.statLifeMax = 100;
            player6.dead = player6.statLife <= 0;
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(16, -1, this.whoAmI, (NetworkText) null, number8, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            break;
          case 17:
            byte num15 = this.reader.ReadByte();
            int index4 = (int) this.reader.ReadInt16();
            int index5 = (int) this.reader.ReadInt16();
            short num16 = this.reader.ReadInt16();
            int num17 = (int) this.reader.ReadByte();
            bool fail = num16 == (short) 1;
            if (!WorldGen.InWorld(index4, index5, 3))
              break;
            if (Main.tile[index4, index5] == null)
              Main.tile[index4, index5] = new Tile();
            if (Main.netMode == 2)
            {
              if (!fail)
              {
                if (num15 == (byte) 0 || num15 == (byte) 2 || num15 == (byte) 4)
                  ++Netplay.Clients[this.whoAmI].SpamDeleteBlock;
                if (num15 == (byte) 1 || num15 == (byte) 3)
                  ++Netplay.Clients[this.whoAmI].SpamAddBlock;
              }
              if (!Netplay.Clients[this.whoAmI].TileSections[Netplay.GetSectionX(index4), Netplay.GetSectionY(index5)])
                fail = true;
            }
            if (num15 == (byte) 0)
            {
              WorldGen.KillTile(index4, index5, fail, false, false);
              if (Main.netMode == 1 && !fail)
                HitTile.ClearAllTilesAtThisLocation(index4, index5);
            }
            if (num15 == (byte) 1)
              WorldGen.PlaceTile(index4, index5, (int) num16, false, true, -1, num17);
            if (num15 == (byte) 2)
              WorldGen.KillWall(index4, index5, fail);
            if (num15 == (byte) 3)
              WorldGen.PlaceWall(index4, index5, (int) num16, false);
            if (num15 == (byte) 4)
              WorldGen.KillTile(index4, index5, fail, false, true);
            if (num15 == (byte) 5)
              WorldGen.PlaceWire(index4, index5);
            if (num15 == (byte) 6)
              WorldGen.KillWire(index4, index5);
            if (num15 == (byte) 7)
              WorldGen.PoundTile(index4, index5);
            if (num15 == (byte) 8)
              WorldGen.PlaceActuator(index4, index5);
            if (num15 == (byte) 9)
              WorldGen.KillActuator(index4, index5);
            if (num15 == (byte) 10)
              WorldGen.PlaceWire2(index4, index5);
            if (num15 == (byte) 11)
              WorldGen.KillWire2(index4, index5);
            if (num15 == (byte) 12)
              WorldGen.PlaceWire3(index4, index5);
            if (num15 == (byte) 13)
              WorldGen.KillWire3(index4, index5);
            if (num15 == (byte) 14)
              WorldGen.SlopeTile(index4, index5, (int) num16, false);
            if (num15 == (byte) 15)
              Minecart.FrameTrack(index4, index5, true, false);
            if (num15 == (byte) 16)
              WorldGen.PlaceWire4(index4, index5);
            if (num15 == (byte) 17)
              WorldGen.KillWire4(index4, index5);
            if (num15 == (byte) 18)
            {
              Wiring.SetCurrentUser(this.whoAmI);
              Wiring.PokeLogicGate(index4, index5);
              Wiring.SetCurrentUser(-1);
              break;
            }
            if (num15 == (byte) 19)
            {
              Wiring.SetCurrentUser(this.whoAmI);
              Wiring.Actuate(index4, index5);
              Wiring.SetCurrentUser(-1);
              break;
            }
            if (num15 == (byte) 20)
            {
              if (!WorldGen.InWorld(index4, index5, 2))
                break;
              int type = (int) Main.tile[index4, index5].type;
              WorldGen.KillTile(index4, index5, fail, false, false);
              short num2 = (int) Main.tile[index4, index5].type == type ? (short) 1 : (short) 0;
              if (Main.netMode != 2)
                break;
              NetMessage.TrySendData(17, -1, -1, (NetworkText) null, (int) num15, (float) index4, (float) index5, (float) num2, num17, 0, 0);
              break;
            }
            if (num15 == (byte) 21)
              WorldGen.ReplaceTile(index4, index5, (ushort) num16, num17);
            if (num15 == (byte) 22)
              WorldGen.ReplaceWall(index4, index5, (ushort) num16);
            if (num15 == (byte) 23)
            {
              WorldGen.SlopeTile(index4, index5, (int) num16, false);
              WorldGen.PoundTile(index4, index5);
            }
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(17, -1, this.whoAmI, (NetworkText) null, (int) num15, (float) index4, (float) index5, (float) num16, num17, 0, 0);
            if (num15 != (byte) 1 && num15 != (byte) 21 || !TileID.Sets.Falling[(int) num16])
              break;
            NetMessage.SendTileSquare(-1, index4, index5, 1, TileChangeType.None);
            break;
          case 18:
            if (Main.netMode != 1)
              break;
            Main.dayTime = this.reader.ReadByte() == (byte) 1;
            Main.time = (double) this.reader.ReadInt32();
            Main.sunModY = this.reader.ReadInt16();
            Main.moonModY = this.reader.ReadInt16();
            break;
          case 19:
            byte num18 = this.reader.ReadByte();
            int num19 = (int) this.reader.ReadInt16();
            int num20 = (int) this.reader.ReadInt16();
            if (!WorldGen.InWorld(num19, num20, 3))
              break;
            int direction1 = this.reader.ReadByte() == (byte) 0 ? -1 : 1;
            switch (num18)
            {
              case 0:
                WorldGen.OpenDoor(num19, num20, direction1);
                break;
              case 1:
                WorldGen.CloseDoor(num19, num20, true);
                break;
              case 2:
                WorldGen.ShiftTrapdoor(num19, num20, direction1 == 1, 1);
                break;
              case 3:
                WorldGen.ShiftTrapdoor(num19, num20, direction1 == 1, 0);
                break;
              case 4:
                WorldGen.ShiftTallGate(num19, num20, false, true);
                break;
              case 5:
                WorldGen.ShiftTallGate(num19, num20, true, true);
                break;
            }
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(19, -1, this.whoAmI, (NetworkText) null, (int) num18, (float) num19, (float) num20, direction1 == 1 ? 1f : 0.0f, 0, 0, 0);
            break;
          case 20:
            int num21 = (int) this.reader.ReadUInt16();
            short num22 = (short) (num21 & (int) short.MaxValue);
            int num23 = (uint) (num21 & 32768) > 0U ? 1 : 0;
            byte num24 = 0;
            if (num23 != 0)
              num24 = this.reader.ReadByte();
            int num25 = (int) this.reader.ReadInt16();
            int num26 = (int) this.reader.ReadInt16();
            if (!WorldGen.InWorld(num25, num26, 3))
              break;
            TileChangeType type3 = TileChangeType.None;
            if (Enum.IsDefined(typeof (TileChangeType), (object) num24))
              type3 = (TileChangeType) num24;
            if (MessageBuffer.OnTileChangeReceived != null)
              MessageBuffer.OnTileChangeReceived(num25, num26, (int) num22, type3);
            BitsByte bitsByte16 = (BitsByte) (byte) 0;
            BitsByte bitsByte17 = (BitsByte) (byte) 0;
            for (int index1 = num25; index1 < num25 + (int) num22; ++index1)
            {
              for (int index2 = num26; index2 < num26 + (int) num22; ++index2)
              {
                if (Main.tile[index1, index2] == null)
                  Main.tile[index1, index2] = new Tile();
                Tile tile = Main.tile[index1, index2];
                bool flag2 = tile.active();
                BitsByte bitsByte18 = (BitsByte) this.reader.ReadByte();
                BitsByte bitsByte19 = (BitsByte) this.reader.ReadByte();
                tile.active(bitsByte18[0]);
                tile.wall = bitsByte18[2] ? (ushort) 1 : (ushort) 0;
                bool flag4 = bitsByte18[3];
                if (Main.netMode != 2)
                  tile.liquid = flag4 ? (byte) 1 : (byte) 0;
                tile.wire(bitsByte18[4]);
                tile.halfBrick(bitsByte18[5]);
                tile.actuator(bitsByte18[6]);
                tile.inActive(bitsByte18[7]);
                tile.wire2(bitsByte19[0]);
                tile.wire3(bitsByte19[1]);
                if (bitsByte19[2])
                  tile.color(this.reader.ReadByte());
                if (bitsByte19[3])
                  tile.wallColor(this.reader.ReadByte());
                if (tile.active())
                {
                  int type1 = (int) tile.type;
                  tile.type = this.reader.ReadUInt16();
                  if (Main.tileFrameImportant[(int) tile.type])
                  {
                    tile.frameX = this.reader.ReadInt16();
                    tile.frameY = this.reader.ReadInt16();
                  }
                  else if (!flag2 || (int) tile.type != type1)
                  {
                    tile.frameX = (short) -1;
                    tile.frameY = (short) -1;
                  }
                  byte slope = 0;
                  if (bitsByte19[4])
                    ++slope;
                  if (bitsByte19[5])
                    slope += (byte) 2;
                  if (bitsByte19[6])
                    slope += (byte) 4;
                  tile.slope(slope);
                }
                tile.wire4(bitsByte19[7]);
                if (tile.wall > (ushort) 0)
                  tile.wall = this.reader.ReadUInt16();
                if (flag4)
                {
                  tile.liquid = this.reader.ReadByte();
                  tile.liquidType((int) this.reader.ReadByte());
                }
              }
            }
            WorldGen.RangeFrame(num25, num26, num25 + (int) num22, num26 + (int) num22);
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData((int) num1, -1, this.whoAmI, (NetworkText) null, (int) num22, (float) num25, (float) num26, 0.0f, 0, 0, 0);
            break;
          case 21:
          case 90:
            int index6 = (int) this.reader.ReadInt16();
            Vector2 vector2_1 = this.reader.ReadVector2();
            Vector2 vector2_2 = this.reader.ReadVector2();
            int Stack = (int) this.reader.ReadInt16();
            int pre1 = (int) this.reader.ReadByte();
            int num27 = (int) this.reader.ReadByte();
            int type4 = (int) this.reader.ReadInt16();
            if (Main.netMode == 1)
            {
              if (type4 == 0)
              {
                Main.item[index6].active = false;
                break;
              }
              int index1 = index6;
              Item obj = Main.item[index1];
              ItemSyncPersistentStats syncPersistentStats = new ItemSyncPersistentStats();
              syncPersistentStats.CopyFrom(obj);
              bool flag2 = (obj.newAndShiny || obj.netID != type4) && ItemSlot.Options.HighlightNewItems && (type4 < 0 || type4 >= 5045 || !ItemID.Sets.NeverAppearsAsNewInInventory[type4]);
              obj.netDefaults(type4);
              obj.newAndShiny = flag2;
              obj.Prefix(pre1);
              obj.stack = Stack;
              obj.position = vector2_1;
              obj.velocity = vector2_2;
              obj.active = true;
              if (num1 == (byte) 90)
              {
                obj.instanced = true;
                obj.playerIndexTheItemIsReservedFor = Main.myPlayer;
                obj.keepTime = 600;
              }
              obj.wet = Collision.WetCollision(obj.position, obj.width, obj.height);
              syncPersistentStats.PasteInto(obj);
              break;
            }
            if (Main.timeItemSlotCannotBeReusedFor[index6] > 0)
              break;
            if (type4 == 0)
            {
              if (index6 >= 400)
                break;
              Main.item[index6].active = false;
              NetMessage.TrySendData(21, -1, -1, (NetworkText) null, index6, 0.0f, 0.0f, 0.0f, 0, 0, 0);
              break;
            }
            bool flag5 = false;
            if (index6 == 400)
              flag5 = true;
            if (flag5)
            {
              Item obj = new Item();
              obj.netDefaults(type4);
              index6 = Item.NewItem((int) vector2_1.X, (int) vector2_1.Y, obj.width, obj.height, obj.type, Stack, true, 0, false, false);
            }
            Item obj1 = Main.item[index6];
            obj1.netDefaults(type4);
            obj1.Prefix(pre1);
            obj1.stack = Stack;
            obj1.position = vector2_1;
            obj1.velocity = vector2_2;
            obj1.active = true;
            obj1.playerIndexTheItemIsReservedFor = Main.myPlayer;
            if (flag5)
            {
              NetMessage.TrySendData(21, -1, -1, (NetworkText) null, index6, 0.0f, 0.0f, 0.0f, 0, 0, 0);
              if (num27 == 0)
              {
                Main.item[index6].ownIgnore = this.whoAmI;
                Main.item[index6].ownTime = 100;
              }
              Main.item[index6].FindOwner(index6);
              break;
            }
            NetMessage.TrySendData(21, -1, this.whoAmI, (NetworkText) null, index6, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            break;
          case 22:
            int number9 = (int) this.reader.ReadInt16();
            int num28 = (int) this.reader.ReadByte();
            if (Main.netMode == 2 && Main.item[number9].playerIndexTheItemIsReservedFor != this.whoAmI)
              break;
            Main.item[number9].playerIndexTheItemIsReservedFor = num28;
            Main.item[number9].keepTime = num28 != Main.myPlayer ? 0 : 15;
            if (Main.netMode != 2)
              break;
            Main.item[number9].playerIndexTheItemIsReservedFor = (int) byte.MaxValue;
            Main.item[number9].keepTime = 15;
            NetMessage.TrySendData(22, -1, -1, (NetworkText) null, number9, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            break;
          case 23:
            if (Main.netMode != 1)
              break;
            int index7 = (int) this.reader.ReadInt16();
            Vector2 vector2_3 = this.reader.ReadVector2();
            Vector2 vector2_4 = this.reader.ReadVector2();
            int num29 = (int) this.reader.ReadUInt16();
            if (num29 == (int) ushort.MaxValue)
              num29 = 0;
            BitsByte bitsByte20 = (BitsByte) this.reader.ReadByte();
            BitsByte bitsByte21 = (BitsByte) this.reader.ReadByte();
            float[] numArray1 = new float[NPC.maxAI];
            for (int index1 = 0; index1 < NPC.maxAI; ++index1)
              numArray1[index1] = !bitsByte20[index1 + 2] ? 0.0f : this.reader.ReadSingle();
            int Type1 = (int) this.reader.ReadInt16();
            int? nullable = new int?(1);
            if (bitsByte21[0])
              nullable = new int?((int) this.reader.ReadByte());
            float num30 = 1f;
            if (bitsByte21[2])
              num30 = this.reader.ReadSingle();
            int num31 = 0;
            if (!bitsByte20[7])
            {
              switch (this.reader.ReadByte())
              {
                case 2:
                  num31 = (int) this.reader.ReadInt16();
                  break;
                case 4:
                  num31 = this.reader.ReadInt32();
                  break;
                default:
                  num31 = (int) this.reader.ReadSByte();
                  break;
              }
            }
            int oldType = -1;
            NPC npc1 = Main.npc[index7];
            if (npc1.active && Main.multiplayerNPCSmoothingRange > 0 && (double) Vector2.DistanceSquared(npc1.position, vector2_3) < 640000.0)
              npc1.netOffset += npc1.position - vector2_3;
            if (!npc1.active || npc1.netID != Type1)
            {
              npc1.netOffset *= 0.0f;
              if (npc1.active)
                oldType = npc1.type;
              npc1.active = true;
              npc1.SetDefaults(Type1, new NPCSpawnParams()
              {
                playerCountForMultiplayerDifficultyOverride = nullable,
                strengthMultiplierOverride = new float?(num30)
              });
            }
            npc1.position = vector2_3;
            npc1.velocity = vector2_4;
            npc1.target = num29;
            npc1.direction = bitsByte20[0] ? 1 : -1;
            npc1.directionY = bitsByte20[1] ? 1 : -1;
            npc1.spriteDirection = bitsByte20[6] ? 1 : -1;
            if (bitsByte20[7])
              num31 = npc1.life = npc1.lifeMax;
            else
              npc1.life = num31;
            if (num31 <= 0)
              npc1.active = false;
            npc1.SpawnedFromStatue = bitsByte21[0];
            if (npc1.SpawnedFromStatue)
              npc1.value = 0.0f;
            for (int index1 = 0; index1 < NPC.maxAI; ++index1)
              npc1.ai[index1] = numArray1[index1];
            if (oldType > -1 && oldType != npc1.type)
              npc1.TransformVisuals(oldType, npc1.type);
            if (Type1 == 262)
              NPC.plantBoss = index7;
            if (Type1 == 245)
              NPC.golemBoss = index7;
            if (npc1.type < 0 || npc1.type >= 663 || !Main.npcCatchable[npc1.type])
              break;
            npc1.releaseOwner = (short) this.reader.ReadByte();
            break;
          case 24:
            int number10 = (int) this.reader.ReadInt16();
            int index8 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              index8 = this.whoAmI;
            Player player7 = Main.player[index8];
            Main.npc[number10].StrikeNPC(player7.inventory[player7.selectedItem].damage, player7.inventory[player7.selectedItem].knockBack, player7.direction, false, false, false);
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(24, -1, this.whoAmI, (NetworkText) null, number10, (float) index8, 0.0f, 0.0f, 0, 0, 0);
            NetMessage.TrySendData(23, -1, -1, (NetworkText) null, number10, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            break;
          case 25:
            break;
          case 26:
            break;
          case 27:
            int num32 = (int) this.reader.ReadInt16();
            Vector2 vector2_5 = this.reader.ReadVector2();
            Vector2 vector2_6 = this.reader.ReadVector2();
            int index9 = (int) this.reader.ReadByte();
            int Type2 = (int) this.reader.ReadInt16();
            BitsByte bitsByte22 = (BitsByte) this.reader.ReadByte();
            float[] numArray2 = new float[Projectile.maxAI];
            for (int index1 = 0; index1 < Projectile.maxAI; ++index1)
              numArray2[index1] = !bitsByte22[index1] ? 0.0f : this.reader.ReadSingle();
            int num33 = bitsByte22[4] ? (int) this.reader.ReadInt16() : 0;
            float num34 = bitsByte22[5] ? this.reader.ReadSingle() : 0.0f;
            int num35 = bitsByte22[6] ? (int) this.reader.ReadInt16() : 0;
            int index10 = bitsByte22[7] ? (int) this.reader.ReadInt16() : -1;
            if (index10 >= 1000)
              index10 = -1;
            if (Main.netMode == 2)
            {
              if (Type2 == 949)
              {
                index9 = (int) byte.MaxValue;
              }
              else
              {
                index9 = this.whoAmI;
                if (Main.projHostile[Type2])
                  break;
              }
            }
            int number11 = 1000;
            for (int index1 = 0; index1 < 1000; ++index1)
            {
              if (Main.projectile[index1].owner == index9 && Main.projectile[index1].identity == num32 && Main.projectile[index1].active)
              {
                number11 = index1;
                break;
              }
            }
            if (number11 == 1000)
            {
              for (int index1 = 0; index1 < 1000; ++index1)
              {
                if (!Main.projectile[index1].active)
                {
                  number11 = index1;
                  break;
                }
              }
            }
            if (number11 == 1000)
              number11 = Projectile.FindOldestProjectile();
            Projectile projectile = Main.projectile[number11];
            if (!projectile.active || projectile.type != Type2)
            {
              projectile.SetDefaults(Type2);
              if (Main.netMode == 2)
                ++Netplay.Clients[this.whoAmI].SpamProjectile;
            }
            projectile.identity = num32;
            projectile.position = vector2_5;
            projectile.velocity = vector2_6;
            projectile.type = Type2;
            projectile.damage = num33;
            projectile.originalDamage = num35;
            projectile.knockBack = num34;
            projectile.owner = index9;
            for (int index1 = 0; index1 < Projectile.maxAI; ++index1)
              projectile.ai[index1] = numArray2[index1];
            if (index10 >= 0)
            {
              projectile.projUUID = index10;
              Main.projectileIdentity[index9, index10] = number11;
            }
            projectile.ProjectileFixDesperation();
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(27, -1, this.whoAmI, (NetworkText) null, number11, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            break;
          case 28:
            int number12 = (int) this.reader.ReadInt16();
            int Damage1 = (int) this.reader.ReadInt16();
            float num36 = this.reader.ReadSingle();
            int hitDirection = (int) this.reader.ReadByte() - 1;
            byte num37 = this.reader.ReadByte();
            if (Main.netMode == 2)
            {
              if (Damage1 < 0)
                Damage1 = 0;
              Main.npc[number12].PlayerInteraction(this.whoAmI);
            }
            if (Damage1 >= 0)
            {
              Main.npc[number12].StrikeNPC(Damage1, num36, hitDirection, num37 == (byte) 1, false, true);
            }
            else
            {
              Main.npc[number12].life = 0;
              Main.npc[number12].HitEffect(0, 10.0);
              Main.npc[number12].active = false;
            }
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(28, -1, this.whoAmI, (NetworkText) null, number12, (float) Damage1, num36, (float) hitDirection, (int) num37, 0, 0);
            if (Main.npc[number12].life <= 0)
              NetMessage.TrySendData(23, -1, -1, (NetworkText) null, number12, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            else
              Main.npc[number12].netUpdate = true;
            if (Main.npc[number12].realLife < 0)
              break;
            if (Main.npc[Main.npc[number12].realLife].life <= 0)
            {
              NetMessage.TrySendData(23, -1, -1, (NetworkText) null, Main.npc[number12].realLife, 0.0f, 0.0f, 0.0f, 0, 0, 0);
              break;
            }
            Main.npc[Main.npc[number12].realLife].netUpdate = true;
            break;
          case 29:
            int number13 = (int) this.reader.ReadInt16();
            int num38 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              num38 = this.whoAmI;
            for (int index1 = 0; index1 < 1000; ++index1)
            {
              if (Main.projectile[index1].owner == num38 && Main.projectile[index1].identity == number13 && Main.projectile[index1].active)
              {
                Main.projectile[index1].Kill();
                break;
              }
            }
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(29, -1, this.whoAmI, (NetworkText) null, number13, (float) num38, 0.0f, 0.0f, 0, 0, 0);
            break;
          case 30:
            int number14 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              number14 = this.whoAmI;
            bool flag6 = this.reader.ReadBoolean();
            Main.player[number14].hostile = flag6;
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(30, -1, this.whoAmI, (NetworkText) null, number14, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            LocalizedText localizedText1 = flag6 ? Lang.mp[11] : Lang.mp[12];
            Color color1 = Main.teamColor[Main.player[number14].team];
            ChatHelper.BroadcastChatMessage(NetworkText.FromKey(localizedText1.Key, (object) Main.player[number14].name), color1, -1);
            break;
          case 31:
            if (Main.netMode != 2)
              break;
            int num39 = (int) this.reader.ReadInt16();
            int num40 = (int) this.reader.ReadInt16();
            int chest1 = Chest.FindChest(num39, num40);
            if (chest1 <= -1 || Chest.UsingChest(chest1) != -1)
              break;
            for (int index1 = 0; index1 < 40; ++index1)
              NetMessage.TrySendData(32, this.whoAmI, -1, (NetworkText) null, chest1, (float) index1, 0.0f, 0.0f, 0, 0, 0);
            NetMessage.TrySendData(33, this.whoAmI, -1, (NetworkText) null, chest1, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            Main.player[this.whoAmI].chest = chest1;
            if (Main.myPlayer == this.whoAmI)
              Main.recBigList = false;
            NetMessage.TrySendData(80, -1, this.whoAmI, (NetworkText) null, this.whoAmI, (float) chest1, 0.0f, 0.0f, 0, 0, 0);
            if (Main.netMode != 2 || !WorldGen.IsChestRigged(num39, num40))
              break;
            Wiring.SetCurrentUser(this.whoAmI);
            Wiring.HitSwitch(num39, num40);
            Wiring.SetCurrentUser(-1);
            NetMessage.TrySendData(59, -1, this.whoAmI, (NetworkText) null, num39, (float) num40, 0.0f, 0.0f, 0, 0, 0);
            break;
          case 32:
            int index11 = (int) this.reader.ReadInt16();
            int index12 = (int) this.reader.ReadByte();
            int num41 = (int) this.reader.ReadInt16();
            int pre2 = (int) this.reader.ReadByte();
            int type5 = (int) this.reader.ReadInt16();
            if (index11 < 0 || index11 >= 8000)
              break;
            if (Main.chest[index11] == null)
              Main.chest[index11] = new Chest(false);
            if (Main.chest[index11].item[index12] == null)
              Main.chest[index11].item[index12] = new Item();
            Main.chest[index11].item[index12].netDefaults(type5);
            Main.chest[index11].item[index12].Prefix(pre2);
            Main.chest[index11].item[index12].stack = num41;
            Recipe.FindRecipes(true);
            break;
          case 33:
            int num42 = (int) this.reader.ReadInt16();
            int index13 = (int) this.reader.ReadInt16();
            int index14 = (int) this.reader.ReadInt16();
            int num43 = (int) this.reader.ReadByte();
            string str1 = string.Empty;
            if (num43 != 0)
            {
              if (num43 <= 20)
                str1 = this.reader.ReadString();
              else if (num43 != (int) byte.MaxValue)
                num43 = 0;
            }
            if (Main.netMode == 1)
            {
              Player player8 = Main.player[Main.myPlayer];
              if (player8.chest == -1)
              {
                Main.playerInventory = true;
                SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0.0f);
              }
              else if (player8.chest != num42 && num42 != -1)
              {
                Main.playerInventory = true;
                SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0.0f);
                Main.recBigList = false;
              }
              else if (player8.chest != -1 && num42 == -1)
              {
                SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0.0f);
                Main.recBigList = false;
              }
              player8.chest = num42;
              player8.chestX = index13;
              player8.chestY = index14;
              Recipe.FindRecipes(true);
              if (Main.tile[index13, index14].frameX < (short) 36 || Main.tile[index13, index14].frameX >= (short) 72)
                break;
              AchievementsHelper.HandleSpecialEvent(Main.player[Main.myPlayer], 16);
              break;
            }
            if (num43 != 0)
            {
              int chest2 = Main.player[this.whoAmI].chest;
              Chest chest3 = Main.chest[chest2];
              chest3.name = str1;
              NetMessage.TrySendData(69, -1, this.whoAmI, (NetworkText) null, chest2, (float) chest3.x, (float) chest3.y, 0.0f, 0, 0, 0);
            }
            Main.player[this.whoAmI].chest = num42;
            Recipe.FindRecipes(true);
            NetMessage.TrySendData(80, -1, this.whoAmI, (NetworkText) null, this.whoAmI, (float) num42, 0.0f, 0.0f, 0, 0, 0);
            break;
          case 34:
            byte num44 = this.reader.ReadByte();
            int index15 = (int) this.reader.ReadInt16();
            int index16 = (int) this.reader.ReadInt16();
            int style1 = (int) this.reader.ReadInt16();
            int id = (int) this.reader.ReadInt16();
            if (Main.netMode == 2)
              id = 0;
            if (Main.netMode == 2)
            {
              if (num44 == (byte) 0)
              {
                int number5_1 = WorldGen.PlaceChest(index15, index16, (ushort) 21, false, style1);
                if (number5_1 == -1)
                {
                  NetMessage.TrySendData(34, this.whoAmI, -1, (NetworkText) null, (int) num44, (float) index15, (float) index16, (float) style1, number5_1, 0, 0);
                  Item.NewItem(index15 * 16, index16 * 16, 32, 32, Chest.chestItemSpawn[style1], 1, true, 0, false, false);
                  break;
                }
                NetMessage.TrySendData(34, -1, -1, (NetworkText) null, (int) num44, (float) index15, (float) index16, (float) style1, number5_1, 0, 0);
                break;
              }
              if (num44 == (byte) 1 && Main.tile[index15, index16].type == (ushort) 21)
              {
                Tile tile = Main.tile[index15, index16];
                if ((int) tile.frameX % 36 != 0)
                  --index15;
                if ((int) tile.frameY % 36 != 0)
                  --index16;
                int chest2 = Chest.FindChest(index15, index16);
                WorldGen.KillTile(index15, index16, false, false, false);
                if (tile.active())
                  break;
                NetMessage.TrySendData(34, -1, -1, (NetworkText) null, (int) num44, (float) index15, (float) index16, 0.0f, chest2, 0, 0);
                break;
              }
              if (num44 == (byte) 2)
              {
                int number5_1 = WorldGen.PlaceChest(index15, index16, (ushort) 88, false, style1);
                if (number5_1 == -1)
                {
                  NetMessage.TrySendData(34, this.whoAmI, -1, (NetworkText) null, (int) num44, (float) index15, (float) index16, (float) style1, number5_1, 0, 0);
                  Item.NewItem(index15 * 16, index16 * 16, 32, 32, Chest.dresserItemSpawn[style1], 1, true, 0, false, false);
                  break;
                }
                NetMessage.TrySendData(34, -1, -1, (NetworkText) null, (int) num44, (float) index15, (float) index16, (float) style1, number5_1, 0, 0);
                break;
              }
              if (num44 == (byte) 3 && Main.tile[index15, index16].type == (ushort) 88)
              {
                Tile tile = Main.tile[index15, index16];
                int num2 = index15 - (int) tile.frameX % 54 / 18;
                if ((int) tile.frameY % 36 != 0)
                  --index16;
                int chest2 = Chest.FindChest(num2, index16);
                WorldGen.KillTile(num2, index16, false, false, false);
                if (tile.active())
                  break;
                NetMessage.TrySendData(34, -1, -1, (NetworkText) null, (int) num44, (float) num2, (float) index16, 0.0f, chest2, 0, 0);
                break;
              }
              if (num44 == (byte) 4)
              {
                int number5_1 = WorldGen.PlaceChest(index15, index16, (ushort) 467, false, style1);
                if (number5_1 == -1)
                {
                  NetMessage.TrySendData(34, this.whoAmI, -1, (NetworkText) null, (int) num44, (float) index15, (float) index16, (float) style1, number5_1, 0, 0);
                  Item.NewItem(index15 * 16, index16 * 16, 32, 32, Chest.chestItemSpawn2[style1], 1, true, 0, false, false);
                  break;
                }
                NetMessage.TrySendData(34, -1, -1, (NetworkText) null, (int) num44, (float) index15, (float) index16, (float) style1, number5_1, 0, 0);
                break;
              }
              if (num44 != (byte) 5 || Main.tile[index15, index16].type != (ushort) 467)
                break;
              Tile tile1 = Main.tile[index15, index16];
              if ((int) tile1.frameX % 36 != 0)
                --index15;
              if ((int) tile1.frameY % 36 != 0)
                --index16;
              int chest3 = Chest.FindChest(index15, index16);
              WorldGen.KillTile(index15, index16, false, false, false);
              if (tile1.active())
                break;
              NetMessage.TrySendData(34, -1, -1, (NetworkText) null, (int) num44, (float) index15, (float) index16, 0.0f, chest3, 0, 0);
              break;
            }
            switch (num44)
            {
              case 0:
                if (id == -1)
                {
                  WorldGen.KillTile(index15, index16, false, false, false);
                  return;
                }
                SoundEngine.PlaySound(0, index15 * 16, index16 * 16, 1, 1f, 0.0f);
                WorldGen.PlaceChestDirect(index15, index16, (ushort) 21, style1, id);
                return;
              case 2:
                if (id == -1)
                {
                  WorldGen.KillTile(index15, index16, false, false, false);
                  return;
                }
                SoundEngine.PlaySound(0, index15 * 16, index16 * 16, 1, 1f, 0.0f);
                WorldGen.PlaceDresserDirect(index15, index16, (ushort) 88, style1, id);
                return;
              case 4:
                if (id == -1)
                {
                  WorldGen.KillTile(index15, index16, false, false, false);
                  return;
                }
                SoundEngine.PlaySound(0, index15 * 16, index16 * 16, 1, 1f, 0.0f);
                WorldGen.PlaceChestDirect(index15, index16, (ushort) 467, style1, id);
                return;
              default:
                Chest.DestroyChestDirect(index15, index16, id);
                WorldGen.KillTile(index15, index16, false, false, false);
                return;
            }
          case 35:
            int number15 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              number15 = this.whoAmI;
            int healAmount1 = (int) this.reader.ReadInt16();
            if (number15 != Main.myPlayer || Main.ServerSideCharacter)
              Main.player[number15].HealEffect(healAmount1, true);
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(35, -1, this.whoAmI, (NetworkText) null, number15, (float) healAmount1, 0.0f, 0.0f, 0, 0, 0);
            break;
          case 36:
            int number16 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              number16 = this.whoAmI;
            Player player9 = Main.player[number16];
            player9.zone1 = (BitsByte) this.reader.ReadByte();
            player9.zone2 = (BitsByte) this.reader.ReadByte();
            player9.zone3 = (BitsByte) this.reader.ReadByte();
            player9.zone4 = (BitsByte) this.reader.ReadByte();
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(36, -1, this.whoAmI, (NetworkText) null, number16, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            break;
          case 37:
            if (Main.netMode != 1)
              break;
            if (Main.autoPass)
            {
              NetMessage.TrySendData(38, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
              Main.autoPass = false;
              break;
            }
            Netplay.ServerPassword = "";
            Main.menuMode = 31;
            break;
          case 38:
            if (Main.netMode != 2)
              break;
            if (this.reader.ReadString() == Netplay.ServerPassword)
            {
              Netplay.Clients[this.whoAmI].State = 1;
              NetMessage.TrySendData(3, this.whoAmI, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
              break;
            }
            NetMessage.TrySendData(2, this.whoAmI, -1, Lang.mp[1].ToNetworkText(), 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            break;
          case 39:
            if (Main.netMode != 1)
              break;
            int number17 = (int) this.reader.ReadInt16();
            Main.item[number17].playerIndexTheItemIsReservedFor = (int) byte.MaxValue;
            NetMessage.TrySendData(22, -1, -1, (NetworkText) null, number17, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            break;
          case 40:
            int number18 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              number18 = this.whoAmI;
            int npcIndex = (int) this.reader.ReadInt16();
            Main.player[number18].SetTalkNPC(npcIndex, true);
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(40, -1, this.whoAmI, (NetworkText) null, number18, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            break;
          case 41:
            int number19 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              number19 = this.whoAmI;
            Player player10 = Main.player[number19];
            float num45 = this.reader.ReadSingle();
            int num46 = (int) this.reader.ReadInt16();
            player10.itemRotation = num45;
            player10.itemAnimation = num46;
            player10.channel = player10.inventory[player10.selectedItem].channel;
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(41, -1, this.whoAmI, (NetworkText) null, number19, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            break;
          case 42:
            int index17 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              index17 = this.whoAmI;
            else if (Main.myPlayer == index17 && !Main.ServerSideCharacter)
              break;
            int num47 = (int) this.reader.ReadInt16();
            int num48 = (int) this.reader.ReadInt16();
            Main.player[index17].statMana = num47;
            Main.player[index17].statManaMax = num48;
            break;
          case 43:
            int number20 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              number20 = this.whoAmI;
            int manaAmount = (int) this.reader.ReadInt16();
            if (number20 != Main.myPlayer)
              Main.player[number20].ManaEffect(manaAmount);
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(43, -1, this.whoAmI, (NetworkText) null, number20, (float) manaAmount, 0.0f, 0.0f, 0, 0, 0);
            break;
          case 44:
            break;
          case 45:
            int number21 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              number21 = this.whoAmI;
            int index18 = (int) this.reader.ReadByte();
            Player player11 = Main.player[number21];
            int team = player11.team;
            player11.team = index18;
            Color color2 = Main.teamColor[index18];
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(45, -1, this.whoAmI, (NetworkText) null, number21, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            LocalizedText localizedText2 = Lang.mp[13 + index18];
            if (index18 == 5)
              localizedText2 = Lang.mp[22];
            for (int playerId = 0; playerId < (int) byte.MaxValue; ++playerId)
            {
              if (playerId == this.whoAmI || team > 0 && Main.player[playerId].team == team || index18 > 0 && Main.player[playerId].team == index18)
                ChatHelper.SendChatMessageToClient(NetworkText.FromKey(localizedText2.Key, (object) player11.name), color2, playerId);
            }
            break;
          case 46:
            if (Main.netMode != 2)
              break;
            int number22 = Sign.ReadSign((int) this.reader.ReadInt16(), (int) this.reader.ReadInt16(), true);
            if (number22 < 0)
              break;
            NetMessage.TrySendData(47, this.whoAmI, -1, (NetworkText) null, number22, (float) this.whoAmI, 0.0f, 0.0f, 0, 0, 0);
            break;
          case 47:
            int index19 = (int) this.reader.ReadInt16();
            int num49 = (int) this.reader.ReadInt16();
            int num50 = (int) this.reader.ReadInt16();
            string text1 = this.reader.ReadString();
            int num51 = (int) this.reader.ReadByte();
            BitsByte bitsByte23 = (BitsByte) this.reader.ReadByte();
            if (index19 < 0 || index19 >= 1000)
              break;
            string str2 = (string) null;
            if (Main.sign[index19] != null)
              str2 = Main.sign[index19].text;
            Main.sign[index19] = new Sign();
            Main.sign[index19].x = num49;
            Main.sign[index19].y = num50;
            Sign.TextSign(index19, text1);
            if (Main.netMode == 2 && str2 != text1)
            {
              num51 = this.whoAmI;
              NetMessage.TrySendData(47, -1, this.whoAmI, (NetworkText) null, index19, (float) num51, 0.0f, 0.0f, 0, 0, 0);
            }
            if (Main.netMode != 1 || num51 != Main.myPlayer || (Main.sign[index19] == null || bitsByte23[0]))
              break;
            Main.playerInventory = false;
            Main.player[Main.myPlayer].SetTalkNPC(-1, true);
            Main.npcChatCornerItem = 0;
            Main.editSign = false;
            SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0.0f);
            Main.player[Main.myPlayer].sign = index19;
            Main.npcChatText = Main.sign[index19].text;
            break;
          case 48:
            int i1 = (int) this.reader.ReadInt16();
            int j1 = (int) this.reader.ReadInt16();
            byte num52 = this.reader.ReadByte();
            byte num53 = this.reader.ReadByte();
            if (Main.netMode == 2 && Netplay.SpamCheck)
            {
              int whoAmI = this.whoAmI;
              int num2 = (int) ((double) Main.player[whoAmI].position.X + (double) (Main.player[whoAmI].width / 2));
              int num54 = (int) ((double) Main.player[whoAmI].position.Y + (double) (Main.player[whoAmI].height / 2));
              int num55 = 10;
              int num56 = num2 - num55;
              int num57 = num2 + num55;
              int num58 = num54 - num55;
              int num59 = num54 + num55;
              if (i1 < num56 || i1 > num57 || (j1 < num58 || j1 > num59))
              {
                NetMessage.BootPlayer(this.whoAmI, NetworkText.FromKey("Net.CheatingLiquidSpam"));
                break;
              }
            }
            if (Main.tile[i1, j1] == null)
              Main.tile[i1, j1] = new Tile();
            lock (Main.tile[i1, j1])
            {
              Main.tile[i1, j1].liquid = num52;
              Main.tile[i1, j1].liquidType((int) num53);
              if (Main.netMode != 2)
                break;
              WorldGen.SquareTileFrame(i1, j1, true);
              break;
            }
          case 49:
            if (Netplay.Connection.State != 6)
              break;
            Netplay.Connection.State = 10;
            Main.ActivePlayerFileData.StartPlayTimer();
            Player.Hooks.EnterWorld(Main.myPlayer);
            Main.player[Main.myPlayer].Spawn(PlayerSpawnContext.SpawningIntoWorld);
            break;
          case 50:
            int number23 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              number23 = this.whoAmI;
            else if (number23 == Main.myPlayer && !Main.ServerSideCharacter)
              break;
            Player player12 = Main.player[number23];
            for (int index1 = 0; index1 < 22; ++index1)
            {
              player12.buffType[index1] = (int) this.reader.ReadUInt16();
              player12.buffTime[index1] = player12.buffType[index1] <= 0 ? 0 : 60;
            }
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(50, -1, this.whoAmI, (NetworkText) null, number23, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            break;
          case 51:
            byte num60 = this.reader.ReadByte();
            byte num61 = this.reader.ReadByte();
            switch (num61)
            {
              case 1:
                NPC.SpawnSkeletron();
                return;
              case 2:
                if (Main.netMode == 2)
                {
                  NetMessage.TrySendData(51, -1, this.whoAmI, (NetworkText) null, (int) num60, (float) num61, 0.0f, 0.0f, 0, 0, 0);
                  return;
                }
                SoundEngine.PlaySound(SoundID.Item1, (int) Main.player[(int) num60].position.X, (int) Main.player[(int) num60].position.Y);
                return;
              case 3:
                if (Main.netMode != 2)
                  return;
                Main.Sundialing();
                return;
              case 4:
                Main.npc[(int) num60].BigMimicSpawnSmoke();
                return;
              default:
                return;
            }
          case 52:
            int num62 = (int) this.reader.ReadByte();
            int num63 = (int) this.reader.ReadInt16();
            int num64 = (int) this.reader.ReadInt16();
            if (num62 == 1)
            {
              Chest.Unlock(num63, num64);
              if (Main.netMode == 2)
              {
                NetMessage.TrySendData(52, -1, this.whoAmI, (NetworkText) null, 0, (float) num62, (float) num63, (float) num64, 0, 0, 0);
                NetMessage.SendTileSquare(-1, num63, num64, 2, TileChangeType.None);
              }
            }
            if (num62 != 2)
              break;
            WorldGen.UnlockDoor(num63, num64);
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(52, -1, this.whoAmI, (NetworkText) null, 0, (float) num62, (float) num63, (float) num64, 0, 0, 0);
            NetMessage.SendTileSquare(-1, num63, num64, 2, TileChangeType.None);
            break;
          case 53:
            int number24 = (int) this.reader.ReadInt16();
            int type6 = (int) this.reader.ReadUInt16();
            int time1 = (int) this.reader.ReadInt16();
            Main.npc[number24].AddBuff(type6, time1, true);
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(54, -1, -1, (NetworkText) null, number24, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            break;
          case 54:
            if (Main.netMode != 1)
              break;
            int index20 = (int) this.reader.ReadInt16();
            NPC npc2 = Main.npc[index20];
            for (int index1 = 0; index1 < 5; ++index1)
            {
              npc2.buffType[index1] = (int) this.reader.ReadUInt16();
              npc2.buffTime[index1] = (int) this.reader.ReadInt16();
            }
            break;
          case 55:
            int index21 = (int) this.reader.ReadByte();
            int type7 = (int) this.reader.ReadUInt16();
            int timeToAdd = this.reader.ReadInt32();
            if (Main.netMode == 2 && index21 != this.whoAmI && !Main.pvpBuff[type7])
              break;
            if (Main.netMode == 1 && index21 == Main.myPlayer)
            {
              Main.player[index21].AddBuff(type7, timeToAdd, true, false);
              break;
            }
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(55, index21, -1, (NetworkText) null, index21, (float) type7, (float) timeToAdd, 0.0f, 0, 0, 0);
            break;
          case 56:
            int number25 = (int) this.reader.ReadInt16();
            if (number25 < 0 || number25 >= 200)
              break;
            if (Main.netMode == 1)
            {
              string str3 = this.reader.ReadString();
              Main.npc[number25].GivenName = str3;
              int num2 = this.reader.ReadInt32();
              Main.npc[number25].townNpcVariationIndex = num2;
              break;
            }
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(56, this.whoAmI, -1, (NetworkText) null, number25, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            break;
          case 57:
            if (Main.netMode != 1)
              break;
            WorldGen.tGood = this.reader.ReadByte();
            WorldGen.tEvil = this.reader.ReadByte();
            WorldGen.tBlood = this.reader.ReadByte();
            break;
          case 58:
            int index22 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              index22 = this.whoAmI;
            float num65 = this.reader.ReadSingle();
            if (Main.netMode == 2)
            {
              NetMessage.TrySendData(58, -1, this.whoAmI, (NetworkText) null, this.whoAmI, num65, 0.0f, 0.0f, 0, 0, 0);
              break;
            }
            Player player13 = Main.player[index22];
            int type8 = player13.inventory[player13.selectedItem].type;
            switch (type8)
            {
              case 4057:
              case 4372:
              case 4715:
                player13.PlayGuitarChord(num65);
                return;
              case 4673:
                player13.PlayDrums(num65);
                return;
              default:
                Main.musicPitch = num65;
                LegacySoundStyle type9 = SoundID.Item26;
                if (type8 == 507)
                  type9 = SoundID.Item35;
                if (type8 == 1305)
                  type9 = SoundID.Item47;
                SoundEngine.PlaySound(type9, player13.position);
                return;
            }
          case 59:
            int num66 = (int) this.reader.ReadInt16();
            int j2 = (int) this.reader.ReadInt16();
            Wiring.SetCurrentUser(this.whoAmI);
            Wiring.HitSwitch(num66, j2);
            Wiring.SetCurrentUser(-1);
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(59, -1, this.whoAmI, (NetworkText) null, num66, (float) j2, 0.0f, 0.0f, 0, 0, 0);
            break;
          case 60:
            int n = (int) this.reader.ReadInt16();
            int x1 = (int) this.reader.ReadInt16();
            int y3 = (int) this.reader.ReadInt16();
            byte num67 = this.reader.ReadByte();
            if (n >= 200)
            {
              NetMessage.BootPlayer(this.whoAmI, NetworkText.FromKey("Net.CheatingInvalid"));
              break;
            }
            if (Main.netMode == 1)
            {
              Main.npc[n].homeless = num67 == (byte) 1;
              Main.npc[n].homeTileX = x1;
              Main.npc[n].homeTileY = y3;
              if (num67 == (byte) 1)
              {
                WorldGen.TownManager.KickOut(Main.npc[n].type);
                break;
              }
              if (num67 != (byte) 2)
                break;
              WorldGen.TownManager.SetRoom(Main.npc[n].type, x1, y3);
              break;
            }
            if (num67 == (byte) 1)
            {
              WorldGen.kickOut(n);
              break;
            }
            WorldGen.moveRoom(x1, y3, n);
            break;
          case 61:
            int plr = (int) this.reader.ReadInt16();
            int Type3 = (int) this.reader.ReadInt16();
            if (Main.netMode != 2)
              break;
            if (Type3 >= 0 && Type3 < 663 && NPCID.Sets.MPAllowedEnemies[Type3])
            {
              if (NPC.AnyNPCs(Type3))
                break;
              NPC.SpawnOnPlayer(plr, Type3);
              break;
            }
            switch (Type3)
            {
              case -14:
                ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Misc.LicenseBunnyUsed"), new Color(50, (int) byte.MaxValue, 130), -1);
                NPC.boughtBunny = true;
                NetMessage.TrySendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
                return;
              case -13:
                ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Misc.LicenseDogUsed"), new Color(50, (int) byte.MaxValue, 130), -1);
                NPC.boughtDog = true;
                NetMessage.TrySendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
                return;
              case -12:
                ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Misc.LicenseCatUsed"), new Color(50, (int) byte.MaxValue, 130), -1);
                NPC.boughtCat = true;
                NetMessage.TrySendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
                return;
              case -11:
                ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Misc.CombatBookUsed"), new Color(50, (int) byte.MaxValue, 130), -1);
                NPC.combatBookWasUsed = true;
                NetMessage.TrySendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
                return;
              case -10:
                if (Main.dayTime || Main.bloodMoon)
                  return;
                ChatHelper.BroadcastChatMessage(NetworkText.FromKey(Lang.misc[8].Key), new Color(50, (int) byte.MaxValue, 130), -1);
                Main.bloodMoon = true;
                if (Main.GetMoonPhase() == MoonPhase.Empty)
                  Main.moonPhase = 5;
                AchievementsHelper.NotifyProgressionEvent(4);
                NetMessage.TrySendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
                return;
              case -8:
                if (!NPC.downedGolemBoss || !Main.hardMode || (NPC.AnyDanger(false) || NPC.AnyoneNearCultists()))
                  return;
                WorldGen.StartImpendingDoom();
                NetMessage.TrySendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
                return;
              case -7:
                Main.invasionDelay = 0;
                Main.StartInvasion(4);
                NetMessage.TrySendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
                NetMessage.TrySendData(78, -1, -1, (NetworkText) null, 0, 1f, (float) (Main.invasionType + 3), 0.0f, 0, 0, 0);
                return;
              case -6:
                if (!Main.dayTime || Main.eclipse)
                  return;
                ChatHelper.BroadcastChatMessage(NetworkText.FromKey(Lang.misc[20].Key), new Color(50, (int) byte.MaxValue, 130), -1);
                Main.eclipse = true;
                NetMessage.TrySendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
                return;
              case -5:
                if (Main.dayTime || DD2Event.Ongoing)
                  return;
                ChatHelper.BroadcastChatMessage(NetworkText.FromKey(Lang.misc[34].Key), new Color(50, (int) byte.MaxValue, 130), -1);
                Main.startSnowMoon();
                NetMessage.TrySendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
                NetMessage.TrySendData(78, -1, -1, (NetworkText) null, 0, 1f, 1f, 1f, 0, 0, 0);
                return;
              case -4:
                if (Main.dayTime || DD2Event.Ongoing)
                  return;
                ChatHelper.BroadcastChatMessage(NetworkText.FromKey(Lang.misc[31].Key), new Color(50, (int) byte.MaxValue, 130), -1);
                Main.startPumpkinMoon();
                NetMessage.TrySendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
                NetMessage.TrySendData(78, -1, -1, (NetworkText) null, 0, 1f, 2f, 1f, 0, 0, 0);
                return;
              default:
                if (Type3 >= 0)
                  return;
                int type10 = 1;
                if (Type3 > -5)
                  type10 = -Type3;
                if (type10 > 0 && Main.invasionType == 0)
                {
                  Main.invasionDelay = 0;
                  Main.StartInvasion(type10);
                }
                NetMessage.TrySendData(78, -1, -1, (NetworkText) null, 0, 1f, (float) (Main.invasionType + 3), 0.0f, 0, 0, 0);
                return;
            }
          case 62:
            int number26 = (int) this.reader.ReadByte();
            int num68 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              number26 = this.whoAmI;
            if (num68 == 1)
              Main.player[number26].NinjaDodge();
            if (num68 == 2)
              Main.player[number26].ShadowDodge();
            if (num68 == 4)
              Main.player[number26].BrainOfConfusionDodge();
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(62, -1, this.whoAmI, (NetworkText) null, number26, (float) num68, 0.0f, 0.0f, 0, 0, 0);
            break;
          case 63:
            int num69 = (int) this.reader.ReadInt16();
            int y4 = (int) this.reader.ReadInt16();
            byte color3 = this.reader.ReadByte();
            WorldGen.paintTile(num69, y4, color3, false);
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(63, -1, this.whoAmI, (NetworkText) null, num69, (float) y4, (float) color3, 0.0f, 0, 0, 0);
            break;
          case 64:
            int num70 = (int) this.reader.ReadInt16();
            int y5 = (int) this.reader.ReadInt16();
            byte color4 = this.reader.ReadByte();
            WorldGen.paintWall(num70, y5, color4, false);
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(64, -1, this.whoAmI, (NetworkText) null, num70, (float) y5, (float) color4, 0.0f, 0, 0, 0);
            break;
          case 65:
            BitsByte bitsByte24 = (BitsByte) this.reader.ReadByte();
            int index23 = (int) this.reader.ReadInt16();
            if (Main.netMode == 2)
              index23 = this.whoAmI;
            Vector2 vector2_7 = this.reader.ReadVector2();
            int num71 = (int) this.reader.ReadByte();
            int number27 = 0;
            if (bitsByte24[0])
              ++number27;
            if (bitsByte24[1])
              number27 += 2;
            bool flag7 = false;
            if (bitsByte24[2])
              flag7 = true;
            int num72 = 0;
            if (bitsByte24[3])
              num72 = this.reader.ReadInt32();
            if (flag7)
              vector2_7 = Main.player[index23].position;
            switch (number27)
            {
              case 0:
                Main.player[index23].Teleport(vector2_7, num71, num72);
                break;
              case 1:
                Main.npc[index23].Teleport(vector2_7, num71, num72);
                break;
              case 2:
                Main.player[index23].Teleport(vector2_7, num71, num72);
                if (Main.netMode == 2)
                {
                  RemoteClient.CheckSection(this.whoAmI, vector2_7, 1);
                  NetMessage.TrySendData(65, -1, -1, (NetworkText) null, 0, (float) index23, vector2_7.X, vector2_7.Y, num71, flag7.ToInt(), num72);
                  int index1 = -1;
                  float num2 = 9999f;
                  for (int index2 = 0; index2 < (int) byte.MaxValue; ++index2)
                  {
                    if (Main.player[index2].active && index2 != this.whoAmI)
                    {
                      Vector2 vector2_8 = Main.player[index2].position - Main.player[this.whoAmI].position;
                      if ((double) vector2_8.Length() < (double) num2)
                      {
                        num2 = vector2_8.Length();
                        index1 = index2;
                      }
                    }
                  }
                  if (index1 >= 0)
                  {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Game.HasTeleportedTo", (object) Main.player[this.whoAmI].name, (object) Main.player[index1].name), new Color(250, 250, 0), -1);
                    break;
                  }
                  break;
                }
                break;
            }
            if (Main.netMode != 2 || number27 != 0)
              break;
            NetMessage.TrySendData(65, -1, this.whoAmI, (NetworkText) null, number27, (float) index23, vector2_7.X, vector2_7.Y, num71, flag7.ToInt(), num72);
            break;
          case 66:
            int number28 = (int) this.reader.ReadByte();
            int healAmount2 = (int) this.reader.ReadInt16();
            if (healAmount2 <= 0)
              break;
            Player player14 = Main.player[number28];
            player14.statLife += healAmount2;
            if (player14.statLife > player14.statLifeMax2)
              player14.statLife = player14.statLifeMax2;
            player14.HealEffect(healAmount2, false);
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(66, -1, this.whoAmI, (NetworkText) null, number28, (float) healAmount2, 0.0f, 0.0f, 0, 0, 0);
            break;
          case 67:
            break;
          case 68:
            this.reader.ReadString();
            break;
          case 69:
            int number29 = (int) this.reader.ReadInt16();
            int X1 = (int) this.reader.ReadInt16();
            int Y1 = (int) this.reader.ReadInt16();
            if (Main.netMode == 1)
            {
              if (number29 < 0 || number29 >= 8000)
                break;
              Chest chest2 = Main.chest[number29];
              if (chest2 == null)
              {
                chest2 = new Chest(false);
                chest2.x = X1;
                chest2.y = Y1;
                Main.chest[number29] = chest2;
              }
              else if (chest2.x != X1 || chest2.y != Y1)
                break;
              chest2.name = this.reader.ReadString();
              break;
            }
            if (number29 < -1 || number29 >= 8000)
              break;
            if (number29 == -1)
            {
              number29 = Chest.FindChest(X1, Y1);
              if (number29 == -1)
                break;
            }
            Chest chest4 = Main.chest[number29];
            if (chest4.x != X1 || chest4.y != Y1)
              break;
            NetMessage.TrySendData(69, this.whoAmI, -1, (NetworkText) null, number29, (float) X1, (float) Y1, 0.0f, 0, 0, 0);
            break;
          case 70:
            if (Main.netMode != 2)
              break;
            int i2 = (int) this.reader.ReadInt16();
            int who = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              who = this.whoAmI;
            if (i2 >= 200 || i2 < 0)
              break;
            NPC.CatchNPC(i2, who);
            break;
          case 71:
            if (Main.netMode != 2)
              break;
            int x2 = this.reader.ReadInt32();
            int num73 = this.reader.ReadInt32();
            int num74 = (int) this.reader.ReadInt16();
            byte num75 = this.reader.ReadByte();
            int y6 = num73;
            int Type4 = num74;
            int Style1 = (int) num75;
            int whoAmI1 = this.whoAmI;
            NPC.ReleaseNPC(x2, y6, Type4, Style1, whoAmI1);
            break;
          case 72:
            if (Main.netMode != 1)
              break;
            for (int index1 = 0; index1 < 40; ++index1)
              Main.travelShop[index1] = (int) this.reader.ReadInt16();
            break;
          case 73:
            switch (this.reader.ReadByte())
            {
              case 0:
                Main.player[this.whoAmI].TeleportationPotion();
                return;
              case 1:
                Main.player[this.whoAmI].MagicConch();
                return;
              case 2:
                Main.player[this.whoAmI].DemonConch();
                return;
              default:
                return;
            }
          case 74:
            if (Main.netMode != 1)
              break;
            Main.anglerQuest = (int) this.reader.ReadByte();
            Main.anglerQuestFinished = this.reader.ReadBoolean();
            break;
          case 75:
            if (Main.netMode != 2)
              break;
            string name = Main.player[this.whoAmI].name;
            if (Main.anglerWhoFinishedToday.Contains(name))
              break;
            Main.anglerWhoFinishedToday.Add(name);
            break;
          case 76:
            int number30 = (int) this.reader.ReadByte();
            if (number30 == Main.myPlayer && !Main.ServerSideCharacter)
              break;
            if (Main.netMode == 2)
              number30 = this.whoAmI;
            Player player15 = Main.player[number30];
            player15.anglerQuestsFinished = this.reader.ReadInt32();
            player15.golferScoreAccumulated = this.reader.ReadInt32();
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(76, -1, this.whoAmI, (NetworkText) null, number30, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            break;
          case 77:
            int type11 = (int) this.reader.ReadInt16();
            ushort num76 = this.reader.ReadUInt16();
            short num77 = this.reader.ReadInt16();
            short num78 = this.reader.ReadInt16();
            int num79 = (int) num76;
            int x3 = (int) num77;
            int y7 = (int) num78;
            Animation.NewTemporaryAnimation(type11, (ushort) num79, x3, y7);
            break;
          case 78:
            if (Main.netMode != 1)
              break;
            Main.ReportInvasionProgress(this.reader.ReadInt32(), this.reader.ReadInt32(), (int) this.reader.ReadSByte(), (int) this.reader.ReadSByte());
            break;
          case 79:
            int x4 = (int) this.reader.ReadInt16();
            int y8 = (int) this.reader.ReadInt16();
            short num80 = this.reader.ReadInt16();
            int style2 = (int) this.reader.ReadInt16();
            int num81 = (int) this.reader.ReadByte();
            int random = (int) this.reader.ReadSByte();
            int direction2 = !this.reader.ReadBoolean() ? -1 : 1;
            if (Main.netMode == 2)
            {
              ++Netplay.Clients[this.whoAmI].SpamAddBlock;
              if (!WorldGen.InWorld(x4, y8, 10) || !Netplay.Clients[this.whoAmI].TileSections[Netplay.GetSectionX(x4), Netplay.GetSectionY(y8)])
                break;
            }
            WorldGen.PlaceObject(x4, y8, (int) num80, false, style2, num81, random, direction2);
            if (Main.netMode != 2)
              break;
            NetMessage.SendObjectPlacment(this.whoAmI, x4, y8, (int) num80, style2, num81, random, direction2);
            break;
          case 80:
            if (Main.netMode != 1)
              break;
            int index24 = (int) this.reader.ReadByte();
            int num82 = (int) this.reader.ReadInt16();
            if (num82 < -3 || num82 >= 8000)
              break;
            Main.player[index24].chest = num82;
            Recipe.FindRecipes(true);
            break;
          case 81:
            if (Main.netMode != 1)
              break;
            int x5 = (int) this.reader.ReadSingle();
            int num83 = (int) this.reader.ReadSingle();
            Color color5 = this.reader.ReadRGB();
            int amount = this.reader.ReadInt32();
            int y9 = num83;
            CombatText.NewText(new Rectangle(x5, y9, 0, 0), color5, amount, false, false);
            break;
          case 82:
            NetManager.Instance.Read(this.reader, this.whoAmI, length);
            break;
          case 83:
            if (Main.netMode != 1)
              break;
            int index25 = (int) this.reader.ReadInt16();
            int num84 = this.reader.ReadInt32();
            if (index25 < 0 || index25 >= 289)
              break;
            NPC.killCount[index25] = num84;
            break;
          case 84:
            int number31 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              number31 = this.whoAmI;
            float num85 = this.reader.ReadSingle();
            Main.player[number31].stealth = num85;
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(84, -1, this.whoAmI, (NetworkText) null, number31, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            break;
          case 85:
            int whoAmI2 = this.whoAmI;
            byte num86 = this.reader.ReadByte();
            if (Main.netMode != 2 || whoAmI2 >= (int) byte.MaxValue || num86 >= (byte) 58)
              break;
            Chest.ServerPlaceItem(this.whoAmI, (int) num86);
            break;
          case 86:
            if (Main.netMode != 1)
              break;
            int key1 = this.reader.ReadInt32();
            if (!this.reader.ReadBoolean())
            {
              TileEntity tileEntity;
              if (!TileEntity.ByID.TryGetValue(key1, out tileEntity))
                break;
              TileEntity.ByID.Remove(key1);
              TileEntity.ByPosition.Remove(tileEntity.Position);
              break;
            }
            TileEntity tileEntity1 = TileEntity.Read(this.reader, true);
            tileEntity1.ID = key1;
            TileEntity.ByID[tileEntity1.ID] = tileEntity1;
            TileEntity.ByPosition[tileEntity1.Position] = tileEntity1;
            break;
          case 87:
            if (Main.netMode != 2)
              break;
            int num87 = (int) this.reader.ReadInt16();
            int num88 = (int) this.reader.ReadInt16();
            int type12 = (int) this.reader.ReadByte();
            if (!WorldGen.InWorld(num87, num88, 0) || TileEntity.ByPosition.ContainsKey(new Point16(num87, num88)))
              break;
            TileEntity.PlaceEntityNet(num87, num88, type12);
            break;
          case 88:
            if (Main.netMode != 1)
              break;
            int index26 = (int) this.reader.ReadInt16();
            if (index26 < 0 || index26 > 400)
              break;
            Item obj2 = Main.item[index26];
            BitsByte bitsByte25 = (BitsByte) this.reader.ReadByte();
            if (bitsByte25[0])
              obj2.color.PackedValue = this.reader.ReadUInt32();
            if (bitsByte25[1])
              obj2.damage = (int) this.reader.ReadUInt16();
            if (bitsByte25[2])
              obj2.knockBack = this.reader.ReadSingle();
            if (bitsByte25[3])
              obj2.useAnimation = (int) this.reader.ReadUInt16();
            if (bitsByte25[4])
              obj2.useTime = (int) this.reader.ReadUInt16();
            if (bitsByte25[5])
              obj2.shoot = (int) this.reader.ReadInt16();
            if (bitsByte25[6])
              obj2.shootSpeed = this.reader.ReadSingle();
            if (!bitsByte25[7])
              break;
            bitsByte25 = (BitsByte) this.reader.ReadByte();
            if (bitsByte25[0])
              obj2.width = (int) this.reader.ReadInt16();
            if (bitsByte25[1])
              obj2.height = (int) this.reader.ReadInt16();
            if (bitsByte25[2])
              obj2.scale = this.reader.ReadSingle();
            if (bitsByte25[3])
              obj2.ammo = (int) this.reader.ReadInt16();
            if (bitsByte25[4])
              obj2.useAmmo = (int) this.reader.ReadInt16();
            if (!bitsByte25[5])
              break;
            obj2.notAmmo = this.reader.ReadBoolean();
            break;
          case 89:
            if (Main.netMode != 2)
              break;
            int x6 = (int) this.reader.ReadInt16();
            int num89 = (int) this.reader.ReadInt16();
            int num90 = (int) this.reader.ReadInt16();
            int num91 = (int) this.reader.ReadByte();
            int num92 = (int) this.reader.ReadInt16();
            int y10 = num89;
            int netid1 = num90;
            int prefix1 = num91;
            int stack1 = num92;
            TEItemFrame.TryPlacing(x6, y10, netid1, prefix1, stack1);
            break;
          case 91:
            if (Main.netMode != 1)
              break;
            int index27 = this.reader.ReadInt32();
            int type13 = (int) this.reader.ReadByte();
            if (type13 == (int) byte.MaxValue)
            {
              if (!EmoteBubble.byID.ContainsKey(index27))
                break;
              EmoteBubble.byID.Remove(index27);
              break;
            }
            int meta = (int) this.reader.ReadUInt16();
            int time2 = (int) this.reader.ReadUInt16();
            int emotion = (int) this.reader.ReadByte();
            int num93 = 0;
            if (emotion < 0)
              num93 = (int) this.reader.ReadInt16();
            WorldUIAnchor bubbleAnchor = EmoteBubble.DeserializeNetAnchor(type13, meta);
            if (type13 == 1)
              Main.player[meta].emoteTime = 360;
            lock (EmoteBubble.byID)
            {
              if (!EmoteBubble.byID.ContainsKey(index27))
              {
                EmoteBubble.byID[index27] = new EmoteBubble(emotion, bubbleAnchor, time2);
              }
              else
              {
                EmoteBubble.byID[index27].lifeTime = time2;
                EmoteBubble.byID[index27].lifeTimeStart = time2;
                EmoteBubble.byID[index27].emote = emotion;
                EmoteBubble.byID[index27].anchor = bubbleAnchor;
              }
              EmoteBubble.byID[index27].ID = index27;
              EmoteBubble.byID[index27].metadata = num93;
              EmoteBubble.OnBubbleChange(index27);
              break;
            }
          case 92:
            int number32 = (int) this.reader.ReadInt16();
            int num94 = this.reader.ReadInt32();
            float num95 = this.reader.ReadSingle();
            float num96 = this.reader.ReadSingle();
            if (number32 < 0 || number32 > 200)
              break;
            if (Main.netMode == 1)
            {
              Main.npc[number32].moneyPing(new Vector2(num95, num96));
              Main.npc[number32].extraValue = num94;
              break;
            }
            Main.npc[number32].extraValue += num94;
            NetMessage.TrySendData(92, -1, -1, (NetworkText) null, number32, (float) Main.npc[number32].extraValue, num95, num96, 0, 0, 0);
            break;
          case 93:
            break;
          case 95:
            ushort num97 = this.reader.ReadUInt16();
            int num98 = (int) this.reader.ReadByte();
            if (Main.netMode != 2)
              break;
            for (int index1 = 0; index1 < 1000; ++index1)
            {
              if (Main.projectile[index1].owner == (int) num97 && Main.projectile[index1].active && (Main.projectile[index1].type == 602 && (double) Main.projectile[index1].ai[1] == (double) num98))
              {
                Main.projectile[index1].Kill();
                NetMessage.TrySendData(29, -1, -1, (NetworkText) null, Main.projectile[index1].identity, (float) num97, 0.0f, 0.0f, 0, 0, 0);
                break;
              }
            }
            break;
          case 96:
            int number33 = (int) this.reader.ReadByte();
            Player player16 = Main.player[number33];
            int extraInfo1 = (int) this.reader.ReadInt16();
            Vector2 newPos1 = this.reader.ReadVector2();
            Vector2 vector2_9 = this.reader.ReadVector2();
            player16.lastPortalColorIndex = extraInfo1 + (extraInfo1 % 2 == 0 ? 1 : -1);
            player16.Teleport(newPos1, 4, extraInfo1);
            player16.velocity = vector2_9;
            if (Main.netMode != 2)
              break;
            NetMessage.SendData(96, -1, -1, (NetworkText) null, number33, newPos1.X, newPos1.Y, (float) extraInfo1, 0, 0, 0);
            break;
          case 97:
            if (Main.netMode != 1)
              break;
            AchievementsHelper.NotifyNPCKilledDirect(Main.player[Main.myPlayer], (int) this.reader.ReadInt16());
            break;
          case 98:
            if (Main.netMode != 1)
              break;
            AchievementsHelper.NotifyProgressionEvent((int) this.reader.ReadInt16());
            break;
          case 99:
            int number34 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              number34 = this.whoAmI;
            Main.player[number34].MinionRestTargetPoint = this.reader.ReadVector2();
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(99, -1, this.whoAmI, (NetworkText) null, number34, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            break;
          case 100:
            int index28 = (int) this.reader.ReadUInt16();
            NPC npc3 = Main.npc[index28];
            int extraInfo2 = (int) this.reader.ReadInt16();
            Vector2 newPos2 = this.reader.ReadVector2();
            Vector2 vector2_10 = this.reader.ReadVector2();
            npc3.lastPortalColorIndex = extraInfo2 + (extraInfo2 % 2 == 0 ? 1 : -1);
            npc3.Teleport(newPos2, 4, extraInfo2);
            npc3.velocity = vector2_10;
            npc3.netOffset *= 0.0f;
            break;
          case 101:
            if (Main.netMode == 2)
              break;
            NPC.ShieldStrengthTowerSolar = (int) this.reader.ReadUInt16();
            NPC.ShieldStrengthTowerVortex = (int) this.reader.ReadUInt16();
            NPC.ShieldStrengthTowerNebula = (int) this.reader.ReadUInt16();
            NPC.ShieldStrengthTowerStardust = (int) this.reader.ReadUInt16();
            if (NPC.ShieldStrengthTowerSolar < 0)
              NPC.ShieldStrengthTowerSolar = 0;
            if (NPC.ShieldStrengthTowerVortex < 0)
              NPC.ShieldStrengthTowerVortex = 0;
            if (NPC.ShieldStrengthTowerNebula < 0)
              NPC.ShieldStrengthTowerNebula = 0;
            if (NPC.ShieldStrengthTowerStardust < 0)
              NPC.ShieldStrengthTowerStardust = 0;
            if (NPC.ShieldStrengthTowerSolar > NPC.LunarShieldPowerExpert)
              NPC.ShieldStrengthTowerSolar = NPC.LunarShieldPowerExpert;
            if (NPC.ShieldStrengthTowerVortex > NPC.LunarShieldPowerExpert)
              NPC.ShieldStrengthTowerVortex = NPC.LunarShieldPowerExpert;
            if (NPC.ShieldStrengthTowerNebula > NPC.LunarShieldPowerExpert)
              NPC.ShieldStrengthTowerNebula = NPC.LunarShieldPowerExpert;
            if (NPC.ShieldStrengthTowerStardust <= NPC.LunarShieldPowerExpert)
              break;
            NPC.ShieldStrengthTowerStardust = NPC.LunarShieldPowerExpert;
            break;
          case 102:
            int index29 = (int) this.reader.ReadByte();
            ushort num99 = this.reader.ReadUInt16();
            Vector2 Other = this.reader.ReadVector2();
            if (Main.netMode == 2)
            {
              NetMessage.TrySendData(102, -1, -1, (NetworkText) null, this.whoAmI, (float) num99, Other.X, Other.Y, 0, 0, 0);
              break;
            }
            Player player17 = Main.player[index29];
            for (int index1 = 0; index1 < (int) byte.MaxValue; ++index1)
            {
              Player player8 = Main.player[index1];
              if (player8.active && !player8.dead && (player17.team == 0 || player17.team == player8.team) && (double) player8.Distance(Other) < 700.0)
              {
                Vector2 vector2_8 = player17.Center - player8.Center;
                Vector2 vec = Vector2.Normalize(vector2_8);
                if (!vec.HasNaNs())
                {
                  int num2 = 90;
                  float num54 = 0.0f;
                  float num55 = 0.2094395f;
                  Vector2 spinningpoint = new Vector2(0.0f, -8f);
                  Vector2 vector2_11 = new Vector2(-3f);
                  float num56 = 0.0f;
                  float num57 = 0.005f;
                  switch (num99)
                  {
                    case 173:
                      num2 = 90;
                      break;
                    case 176:
                      num2 = 88;
                      break;
                    case 179:
                      num2 = 86;
                      break;
                  }
                  for (int index2 = 0; (double) index2 < (double) vector2_8.Length() / 6.0; ++index2)
                  {
                    Vector2 Position = player8.Center + 6f * (float) index2 * vec + spinningpoint.RotatedBy((double) num54, new Vector2()) + vector2_11;
                    num54 += num55;
                    int Type5 = num2;
                    Color newColor = new Color();
                    int index30 = Dust.NewDust(Position, 6, 6, Type5, 0.0f, 0.0f, 100, newColor, 1.5f);
                    Main.dust[index30].noGravity = true;
                    Main.dust[index30].velocity = Vector2.Zero;
                    Main.dust[index30].fadeIn = (num56 += num57);
                    Main.dust[index30].velocity += vec * 1.5f;
                  }
                }
                player8.NebulaLevelup((int) num99);
              }
            }
            break;
          case 103:
            if (Main.netMode != 1)
              break;
            NPC.MoonLordCountdown = this.reader.ReadInt32();
            break;
          case 104:
            if (Main.netMode != 1 || Main.npcShop <= 0)
              break;
            Item[] objArray = Main.instance.shop[Main.npcShop].item;
            int index31 = (int) this.reader.ReadByte();
            int type14 = (int) this.reader.ReadInt16();
            int num100 = (int) this.reader.ReadInt16();
            int pre3 = (int) this.reader.ReadByte();
            int num101 = this.reader.ReadInt32();
            BitsByte bitsByte26 = (BitsByte) this.reader.ReadByte();
            if (index31 >= objArray.Length)
              break;
            objArray[index31] = new Item();
            objArray[index31].netDefaults(type14);
            objArray[index31].stack = num100;
            objArray[index31].Prefix(pre3);
            objArray[index31].value = num101;
            objArray[index31].buyOnce = bitsByte26[0];
            break;
          case 105:
            if (Main.netMode == 1)
              break;
            int i3 = (int) this.reader.ReadInt16();
            int num102 = (int) this.reader.ReadInt16();
            bool flag8 = this.reader.ReadBoolean();
            int j3 = num102;
            int num103 = flag8 ? 1 : 0;
            WorldGen.ToggleGemLock(i3, j3, num103 != 0);
            break;
          case 106:
            if (Main.netMode != 1)
              break;
            Utils.PoofOfSmoke(new HalfVector2()
            {
              PackedValue = this.reader.ReadUInt32()
            }.ToVector2());
            break;
          case 107:
            if (Main.netMode != 1)
              break;
            Color color6 = this.reader.ReadRGB();
            string text2 = NetworkText.Deserialize(this.reader).ToString();
            int num104 = (int) this.reader.ReadInt16();
            Color c = color6;
            int WidthLimit = num104;
            Main.NewTextMultiline(text2, false, c, WidthLimit);
            break;
          case 108:
            if (Main.netMode != 1)
              break;
            int Damage2 = (int) this.reader.ReadInt16();
            float KnockBack = this.reader.ReadSingle();
            int x7 = (int) this.reader.ReadInt16();
            int y11 = (int) this.reader.ReadInt16();
            int angle = (int) this.reader.ReadInt16();
            int ammo = (int) this.reader.ReadInt16();
            int owner = (int) this.reader.ReadByte();
            if (owner != Main.myPlayer)
              break;
            WorldGen.ShootFromCannon(x7, y11, angle, ammo, Damage2, KnockBack, owner);
            break;
          case 109:
            if (Main.netMode != 2)
              break;
            int x8 = (int) this.reader.ReadInt16();
            int num105 = (int) this.reader.ReadInt16();
            int x9 = (int) this.reader.ReadInt16();
            int y12 = (int) this.reader.ReadInt16();
            int num106 = (int) this.reader.ReadByte();
            int whoAmI3 = this.whoAmI;
            WiresUI.Settings.MultiToolMode toolMode = WiresUI.Settings.ToolMode;
            WiresUI.Settings.ToolMode = (WiresUI.Settings.MultiToolMode) num106;
            int y13 = num105;
            Wiring.MassWireOperation(new Point(x8, y13), new Point(x9, y12), Main.player[whoAmI3]);
            WiresUI.Settings.ToolMode = toolMode;
            break;
          case 110:
            if (Main.netMode != 1)
              break;
            int type15 = (int) this.reader.ReadInt16();
            int num107 = (int) this.reader.ReadInt16();
            int index32 = (int) this.reader.ReadByte();
            if (index32 != Main.myPlayer)
              break;
            Player player18 = Main.player[index32];
            for (int index1 = 0; index1 < num107; ++index1)
              player18.ConsumeItem(type15, false);
            player18.wireOperationsCooldown = 0;
            break;
          case 111:
            if (Main.netMode != 2)
              break;
            BirthdayParty.ToggleManualParty();
            break;
          case 112:
            int number35 = (int) this.reader.ReadByte();
            int x10 = this.reader.ReadInt32();
            int y14 = this.reader.ReadInt32();
            int num108 = (int) this.reader.ReadByte();
            int num109 = (int) this.reader.ReadInt16();
            switch (number35)
            {
              case 1:
                if (Main.netMode == 1)
                  WorldGen.TreeGrowFX(x10, y14, num108, num109, false);
                if (Main.netMode != 2)
                  return;
                NetMessage.TrySendData((int) num1, -1, -1, (NetworkText) null, number35, (float) x10, (float) y14, (float) num108, num109, 0, 0);
                return;
              case 2:
                NPC.FairyEffects(new Vector2((float) x10, (float) y14), num108);
                return;
              default:
                return;
            }
          case 113:
            int x11 = (int) this.reader.ReadInt16();
            int y15 = (int) this.reader.ReadInt16();
            if (Main.netMode != 2 || Main.snowMoon || Main.pumpkinMoon)
              break;
            if (DD2Event.WouldFailSpawningHere(x11, y15))
              DD2Event.FailureMessage(this.whoAmI);
            DD2Event.SummonCrystal(x11, y15);
            break;
          case 114:
            if (Main.netMode != 1)
              break;
            DD2Event.WipeEntities();
            break;
          case 115:
            int number36 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              number36 = this.whoAmI;
            Main.player[number36].MinionAttackTargetNPC = (int) this.reader.ReadInt16();
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(115, -1, this.whoAmI, (NetworkText) null, number36, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            break;
          case 116:
            if (Main.netMode != 1)
              break;
            DD2Event.TimeLeftBetweenWaves = this.reader.ReadInt32();
            break;
          case 117:
            int playerTargetIndex1 = (int) this.reader.ReadByte();
            if (Main.netMode == 2 && this.whoAmI != playerTargetIndex1 && (!Main.player[playerTargetIndex1].hostile || !Main.player[this.whoAmI].hostile))
              break;
            PlayerDeathReason playerDeathReason1 = PlayerDeathReason.FromReader(this.reader);
            int num110 = (int) this.reader.ReadInt16();
            int num111 = (int) this.reader.ReadByte() - 1;
            BitsByte bitsByte27 = (BitsByte) this.reader.ReadByte();
            bool flag9 = bitsByte27[0];
            bool pvp1 = bitsByte27[1];
            int num112 = (int) this.reader.ReadSByte();
            Main.player[playerTargetIndex1].Hurt(playerDeathReason1, num110, num111, pvp1, true, flag9, num112);
            if (Main.netMode != 2)
              break;
            NetMessage.SendPlayerHurt(playerTargetIndex1, playerDeathReason1, num110, num111, flag9, pvp1, num112, -1, this.whoAmI);
            break;
          case 118:
            int playerTargetIndex2 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              playerTargetIndex2 = this.whoAmI;
            PlayerDeathReason playerDeathReason2 = PlayerDeathReason.FromReader(this.reader);
            int damage = (int) this.reader.ReadInt16();
            int num113 = (int) this.reader.ReadByte() - 1;
            bool pvp2 = ((BitsByte) this.reader.ReadByte())[0];
            Main.player[playerTargetIndex2].KillMe(playerDeathReason2, (double) damage, num113, pvp2);
            if (Main.netMode != 2)
              break;
            NetMessage.SendPlayerDeath(playerTargetIndex2, playerDeathReason2, damage, num113, pvp2, -1, this.whoAmI);
            break;
          case 119:
            if (Main.netMode != 1)
              break;
            int x12 = (int) this.reader.ReadSingle();
            int num114 = (int) this.reader.ReadSingle();
            Color color7 = this.reader.ReadRGB();
            NetworkText networkText = NetworkText.Deserialize(this.reader);
            int y16 = num114;
            CombatText.NewText(new Rectangle(x12, y16, 0, 0), color7, networkText.ToString(), false, false);
            break;
          case 120:
            int index33 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              index33 = this.whoAmI;
            int num115 = (int) this.reader.ReadByte();
            if (num115 < 0 || num115 >= 145 || Main.netMode != 2)
              break;
            EmoteBubble.NewBubble(num115, new WorldUIAnchor((Entity) Main.player[index33]), 360);
            EmoteBubble.CheckForNPCsToReactToEmoteBubble(num115, Main.player[index33]);
            break;
          case 121:
            int num116 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              num116 = this.whoAmI;
            int key2 = this.reader.ReadInt32();
            int itemIndex1 = (int) this.reader.ReadByte();
            bool dye1 = false;
            if (itemIndex1 >= 8)
            {
              dye1 = true;
              itemIndex1 -= 8;
            }
            TileEntity tileEntity2;
            if (!TileEntity.ByID.TryGetValue(key2, out tileEntity2))
            {
              this.reader.ReadInt32();
              int num2 = (int) this.reader.ReadByte();
              break;
            }
            if (itemIndex1 >= 8)
              tileEntity2 = (TileEntity) null;
            if (tileEntity2 is TEDisplayDoll teDisplayDoll)
            {
              teDisplayDoll.ReadItem(itemIndex1, this.reader, dye1);
            }
            else
            {
              this.reader.ReadInt32();
              int num2 = (int) this.reader.ReadByte();
            }
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData((int) num1, -1, num116, (NetworkText) null, num116, (float) key2, (float) itemIndex1, (float) dye1.ToInt(), 0, 0, 0);
            break;
          case 122:
            int num117 = this.reader.ReadInt32();
            int index34 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              index34 = this.whoAmI;
            if (Main.netMode == 2)
            {
              if (num117 == -1)
              {
                Main.player[index34].tileEntityAnchor.Clear();
                NetMessage.TrySendData((int) num1, -1, -1, (NetworkText) null, num117, (float) index34, 0.0f, 0.0f, 0, 0, 0);
                break;
              }
              TileEntity tileEntity3;
              if (!TileEntity.IsOccupied(num117, out int _) && TileEntity.ByID.TryGetValue(num117, out tileEntity3))
              {
                Main.player[index34].tileEntityAnchor.Set(num117, (int) tileEntity3.Position.X, (int) tileEntity3.Position.Y);
                NetMessage.TrySendData((int) num1, -1, -1, (NetworkText) null, num117, (float) index34, 0.0f, 0.0f, 0, 0, 0);
              }
            }
            if (Main.netMode != 1)
              break;
            if (num117 == -1)
            {
              Main.player[index34].tileEntityAnchor.Clear();
              break;
            }
            TileEntity tileEntity4;
            if (!TileEntity.ByID.TryGetValue(num117, out tileEntity4))
              break;
            TileEntity.SetInteractionAnchor(Main.player[index34], (int) tileEntity4.Position.X, (int) tileEntity4.Position.Y, num117);
            break;
          case 123:
            if (Main.netMode != 2)
              break;
            int x13 = (int) this.reader.ReadInt16();
            int num118 = (int) this.reader.ReadInt16();
            int num119 = (int) this.reader.ReadInt16();
            int num120 = (int) this.reader.ReadByte();
            int num121 = (int) this.reader.ReadInt16();
            int y17 = num118;
            int netid2 = num119;
            int prefix2 = num120;
            int stack2 = num121;
            TEWeaponsRack.TryPlacing(x13, y17, netid2, prefix2, stack2);
            break;
          case 124:
            int num122 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              num122 = this.whoAmI;
            int key3 = this.reader.ReadInt32();
            int itemIndex2 = (int) this.reader.ReadByte();
            bool dye2 = false;
            if (itemIndex2 >= 2)
            {
              dye2 = true;
              itemIndex2 -= 2;
            }
            TileEntity tileEntity5;
            if (!TileEntity.ByID.TryGetValue(key3, out tileEntity5))
            {
              this.reader.ReadInt32();
              int num2 = (int) this.reader.ReadByte();
              break;
            }
            if (itemIndex2 >= 2)
              tileEntity5 = (TileEntity) null;
            if (tileEntity5 is TEHatRack teHatRack)
            {
              teHatRack.ReadItem(itemIndex2, this.reader, dye2);
            }
            else
            {
              this.reader.ReadInt32();
              int num2 = (int) this.reader.ReadByte();
            }
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData((int) num1, -1, num122, (NetworkText) null, num122, (float) key3, (float) itemIndex2, (float) dye2.ToInt(), 0, 0, 0);
            break;
          case 125:
            int num123 = (int) this.reader.ReadByte();
            int x14 = (int) this.reader.ReadInt16();
            int y18 = (int) this.reader.ReadInt16();
            int pickDamage = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              num123 = this.whoAmI;
            if (Main.netMode == 1)
              Main.player[Main.myPlayer].GetOtherPlayersPickTile(x14, y18, pickDamage);
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(125, -1, num123, (NetworkText) null, num123, (float) x14, (float) y18, (float) pickDamage, 0, 0, 0);
            break;
          case 126:
            if (Main.netMode != 1)
              break;
            NPC.RevengeManager.AddMarkerFromReader(this.reader);
            break;
          case 127:
            int markerUniqueID = this.reader.ReadInt32();
            if (Main.netMode != 1)
              break;
            NPC.RevengeManager.DestroyMarker(markerUniqueID);
            break;
          case 128:
            int num124 = (int) this.reader.ReadByte();
            int num125 = (int) this.reader.ReadUInt16();
            int num126 = (int) this.reader.ReadUInt16();
            int numberOfHits = (int) this.reader.ReadUInt16();
            int projid = (int) this.reader.ReadUInt16();
            if (Main.netMode == 2)
            {
              NetMessage.SendData(128, -1, num124, (NetworkText) null, num124, (float) numberOfHits, (float) projid, 0.0f, num125, num126, 0);
              break;
            }
            GolfHelper.ContactListener.PutBallInCup_TextAndEffects(new Point(num125, num126), num124, numberOfHits, projid);
            break;
          case 129:
            if (Main.netMode != 1)
              break;
            Main.FixUIScale();
            Main.TrySetPreparationState(Main.WorldPreparationState.ProcessingData);
            break;
          case 130:
            if (Main.netMode != 2)
              break;
            int num127 = (int) this.reader.ReadUInt16();
            int num128 = (int) this.reader.ReadUInt16();
            int Type6 = (int) this.reader.ReadInt16();
            int X2 = num127 * 16;
            int num129 = num128 * 16;
            NPC npc4 = new NPC();
            npc4.SetDefaults(Type6, new NPCSpawnParams());
            int type16 = npc4.type;
            int netId = npc4.netID;
            int Y2 = num129;
            int Type7 = Type6;
            int number37 = NPC.NewNPC(X2, Y2, Type7, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
            if (netId == type16)
              break;
            Main.npc[number37].SetDefaults(netId, new NPCSpawnParams());
            NetMessage.TrySendData(23, -1, -1, (NetworkText) null, number37, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            break;
          case 131:
            if (Main.netMode != 1)
              break;
            int index35 = (int) this.reader.ReadUInt16();
            NPC npc5 = index35 >= 200 ? new NPC() : Main.npc[index35];
            if (this.reader.ReadByte() != (byte) 1)
              break;
            int time3 = this.reader.ReadInt32();
            int fromWho = (int) this.reader.ReadInt16();
            npc5.GetImmuneTime(fromWho, time3);
            break;
          case 132:
            if (Main.netMode != 1)
              break;
            Point point = this.reader.ReadVector2().ToPoint();
            ushort index36 = this.reader.ReadUInt16();
            LegacySoundStyle legacySoundStyle = SoundID.SoundByIndex[index36];
            BitsByte bitsByte28 = (BitsByte) this.reader.ReadByte();
            int Style2 = !bitsByte28[0] ? legacySoundStyle.Style : this.reader.ReadInt32();
            float volumeScale = !bitsByte28[1] ? legacySoundStyle.Volume : MathHelper.Clamp(this.reader.ReadSingle(), 0.0f, 1f);
            float pitchOffset = !bitsByte28[2] ? legacySoundStyle.GetRandomPitch() : MathHelper.Clamp(this.reader.ReadSingle(), -1f, 1f);
            SoundEngine.PlaySound(legacySoundStyle.SoundId, point.X, point.Y, Style2, volumeScale, pitchOffset);
            break;
          case 133:
            if (Main.netMode != 2)
              break;
            int x15 = (int) this.reader.ReadInt16();
            int num130 = (int) this.reader.ReadInt16();
            int num131 = (int) this.reader.ReadInt16();
            int num132 = (int) this.reader.ReadByte();
            int num133 = (int) this.reader.ReadInt16();
            int y19 = num130;
            int netid3 = num131;
            int prefix3 = num132;
            int stack3 = num133;
            TEFoodPlatter.TryPlacing(x15, y19, netid3, prefix3, stack3);
            break;
          case 134:
            int index37 = (int) this.reader.ReadByte();
            int num134 = this.reader.ReadInt32();
            float num135 = this.reader.ReadSingle();
            byte num136 = this.reader.ReadByte();
            bool flag10 = this.reader.ReadBoolean();
            if (Main.netMode == 2)
              index37 = this.whoAmI;
            Player player19 = Main.player[index37];
            player19.ladyBugLuckTimeLeft = num134;
            player19.torchLuck = num135;
            player19.luckPotion = num136;
            player19.HasGardenGnomeNearby = flag10;
            player19.RecalculateLuck();
            if (Main.netMode != 2)
              break;
            NetMessage.SendData(134, -1, index37, (NetworkText) null, index37, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            break;
          case 135:
            int index38 = (int) this.reader.ReadByte();
            if (Main.netMode != 1)
              break;
            Main.player[index38].immuneAlpha = (int) byte.MaxValue;
            break;
          case 136:
            for (int index1 = 0; index1 < 2; ++index1)
            {
              for (int index2 = 0; index2 < 3; ++index2)
                NPC.cavernMonsterType[index1, index2] = (int) this.reader.ReadUInt16();
            }
            break;
          case 137:
            if (Main.netMode != 2)
              break;
            int index39 = (int) this.reader.ReadInt16();
            int buffTypeToRemove = (int) this.reader.ReadUInt16();
            if (index39 < 0 || index39 >= 200)
              break;
            Main.npc[index39].RequestBuffRemoval(buffTypeToRemove);
            break;
          case 139:
            if (Main.netMode == 2)
              break;
            int index40 = (int) this.reader.ReadByte();
            bool flag11 = this.reader.ReadBoolean();
            Main.countsAsHostForGameplay[index40] = flag11;
            break;
          default:
            if (Netplay.Clients[this.whoAmI].State != 0)
              break;
            NetMessage.BootPlayer(this.whoAmI, Lang.mp[2].ToNetworkText());
            break;
        }
      }
    }
  }
}
