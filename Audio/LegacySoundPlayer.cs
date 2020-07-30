// Decompiled with JetBrains decompiler
// Type: Terraria.Audio.LegacySoundPlayer
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using ReLogic.Content;
using ReLogic.Utilities;
using System;
using System.IO;
using Terraria.ID;

namespace Terraria.Audio
{
  public class LegacySoundPlayer
  {
    private Asset<SoundEffect>[] _soundDrip = new Asset<SoundEffect>[3];
    private SoundEffectInstance[] _soundInstanceDrip = new SoundEffectInstance[3];
    private Asset<SoundEffect>[] _soundLiquid = new Asset<SoundEffect>[2];
    private SoundEffectInstance[] _soundInstanceLiquid = new SoundEffectInstance[2];
    private Asset<SoundEffect>[] _soundMech = new Asset<SoundEffect>[1];
    private SoundEffectInstance[] _soundInstanceMech = new SoundEffectInstance[1];
    private Asset<SoundEffect>[] _soundDig = new Asset<SoundEffect>[3];
    private SoundEffectInstance[] _soundInstanceDig = new SoundEffectInstance[3];
    private Asset<SoundEffect>[] _soundThunder = new Asset<SoundEffect>[7];
    private SoundEffectInstance[] _soundInstanceThunder = new SoundEffectInstance[7];
    private Asset<SoundEffect>[] _soundResearch = new Asset<SoundEffect>[4];
    private SoundEffectInstance[] _soundInstanceResearch = new SoundEffectInstance[4];
    private Asset<SoundEffect>[] _soundTink = new Asset<SoundEffect>[3];
    private SoundEffectInstance[] _soundInstanceTink = new SoundEffectInstance[3];
    private Asset<SoundEffect>[] _soundCoin = new Asset<SoundEffect>[5];
    private SoundEffectInstance[] _soundInstanceCoin = new SoundEffectInstance[5];
    private Asset<SoundEffect>[] _soundPlayerHit = new Asset<SoundEffect>[3];
    private SoundEffectInstance[] _soundInstancePlayerHit = new SoundEffectInstance[3];
    private Asset<SoundEffect>[] _soundFemaleHit = new Asset<SoundEffect>[3];
    private SoundEffectInstance[] _soundInstanceFemaleHit = new SoundEffectInstance[3];
    private Asset<SoundEffect>[] _soundItem = new Asset<SoundEffect>[(int) SoundID.ItemSoundCount];
    private SoundEffectInstance[] _soundInstanceItem = new SoundEffectInstance[(int) SoundID.ItemSoundCount];
    private Asset<SoundEffect>[] _soundNpcHit = new Asset<SoundEffect>[58];
    private SoundEffectInstance[] _soundInstanceNpcHit = new SoundEffectInstance[58];
    private Asset<SoundEffect>[] _soundNpcKilled = new Asset<SoundEffect>[(int) SoundID.NPCDeathCount];
    private SoundEffectInstance[] _soundInstanceNpcKilled = new SoundEffectInstance[(int) SoundID.NPCDeathCount];
    private Asset<SoundEffect>[] _soundZombie = new Asset<SoundEffect>[118];
    private SoundEffectInstance[] _soundInstanceZombie = new SoundEffectInstance[118];
    private Asset<SoundEffect>[] _soundRoar = new Asset<SoundEffect>[3];
    private SoundEffectInstance[] _soundInstanceRoar = new SoundEffectInstance[3];
    private Asset<SoundEffect>[] _soundSplash = new Asset<SoundEffect>[2];
    private SoundEffectInstance[] _soundInstanceSplash = new SoundEffectInstance[2];
    private Asset<SoundEffect> _soundPlayerKilled;
    private SoundEffectInstance _soundInstancePlayerKilled;
    private Asset<SoundEffect> _soundGrass;
    private SoundEffectInstance _soundInstanceGrass;
    private Asset<SoundEffect> _soundGrab;
    private SoundEffectInstance _soundInstanceGrab;
    private Asset<SoundEffect> _soundPixie;
    private SoundEffectInstance _soundInstancePixie;
    private SoundEffectInstance _soundInstanceMoonlordCry;
    private Asset<SoundEffect> _soundDoorOpen;
    private SoundEffectInstance _soundInstanceDoorOpen;
    private Asset<SoundEffect> _soundDoorClosed;
    private SoundEffectInstance _soundInstanceDoorClosed;
    private Asset<SoundEffect> _soundMenuOpen;
    private SoundEffectInstance _soundInstanceMenuOpen;
    private Asset<SoundEffect> _soundMenuClose;
    private SoundEffectInstance _soundInstanceMenuClose;
    private Asset<SoundEffect> _soundMenuTick;
    private SoundEffectInstance _soundInstanceMenuTick;
    private Asset<SoundEffect> _soundShatter;
    private SoundEffectInstance _soundInstanceShatter;
    private Asset<SoundEffect> _soundCamera;
    private SoundEffectInstance _soundInstanceCamera;
    private Asset<SoundEffect> _soundDoubleJump;
    private SoundEffectInstance _soundInstanceDoubleJump;
    private Asset<SoundEffect> _soundRun;
    private SoundEffectInstance _soundInstanceRun;
    private Asset<SoundEffect> _soundCoins;
    private SoundEffectInstance _soundInstanceCoins;
    private Asset<SoundEffect> _soundUnlock;
    private SoundEffectInstance _soundInstanceUnlock;
    private Asset<SoundEffect> _soundChat;
    private SoundEffectInstance _soundInstanceChat;
    private Asset<SoundEffect> _soundMaxMana;
    private SoundEffectInstance _soundInstanceMaxMana;
    private Asset<SoundEffect> _soundDrown;
    private SoundEffectInstance _soundInstanceDrown;
    private Asset<SoundEffect>[] _trackableSounds;
    private SoundEffectInstance[] _trackableSoundInstances;
    private readonly IServiceProvider _services;

    public LegacySoundPlayer(IServiceProvider services)
    {
      this._services = services;
      this.LoadAll();
    }

    private void LoadAll()
    {
      this._soundMech[0] = this.Load("Sounds/Mech_0");
      this._soundGrab = this.Load("Sounds/Grab");
      this._soundPixie = this.Load("Sounds/Pixie");
      this._soundDig[0] = this.Load("Sounds/Dig_0");
      this._soundDig[1] = this.Load("Sounds/Dig_1");
      this._soundDig[2] = this.Load("Sounds/Dig_2");
      this._soundThunder[0] = this.Load("Sounds/Thunder_0");
      this._soundThunder[1] = this.Load("Sounds/Thunder_1");
      this._soundThunder[2] = this.Load("Sounds/Thunder_2");
      this._soundThunder[3] = this.Load("Sounds/Thunder_3");
      this._soundThunder[4] = this.Load("Sounds/Thunder_4");
      this._soundThunder[5] = this.Load("Sounds/Thunder_5");
      this._soundThunder[6] = this.Load("Sounds/Thunder_6");
      this._soundResearch[0] = this.Load("Sounds/Research_0");
      this._soundResearch[1] = this.Load("Sounds/Research_1");
      this._soundResearch[2] = this.Load("Sounds/Research_2");
      this._soundResearch[3] = this.Load("Sounds/Research_3");
      this._soundTink[0] = this.Load("Sounds/Tink_0");
      this._soundTink[1] = this.Load("Sounds/Tink_1");
      this._soundTink[2] = this.Load("Sounds/Tink_2");
      this._soundPlayerHit[0] = this.Load("Sounds/Player_Hit_0");
      this._soundPlayerHit[1] = this.Load("Sounds/Player_Hit_1");
      this._soundPlayerHit[2] = this.Load("Sounds/Player_Hit_2");
      this._soundFemaleHit[0] = this.Load("Sounds/Female_Hit_0");
      this._soundFemaleHit[1] = this.Load("Sounds/Female_Hit_1");
      this._soundFemaleHit[2] = this.Load("Sounds/Female_Hit_2");
      this._soundPlayerKilled = this.Load("Sounds/Player_Killed");
      this._soundChat = this.Load("Sounds/Chat");
      this._soundGrass = this.Load("Sounds/Grass");
      this._soundDoorOpen = this.Load("Sounds/Door_Opened");
      this._soundDoorClosed = this.Load("Sounds/Door_Closed");
      this._soundMenuTick = this.Load("Sounds/Menu_Tick");
      this._soundMenuOpen = this.Load("Sounds/Menu_Open");
      this._soundMenuClose = this.Load("Sounds/Menu_Close");
      this._soundShatter = this.Load("Sounds/Shatter");
      this._soundCamera = this.Load("Sounds/Camera");
      for (int index = 0; index < this._soundCoin.Length; ++index)
        this._soundCoin[index] = this.Load("Sounds/Coin_" + (object) index);
      for (int index = 0; index < this._soundDrip.Length; ++index)
        this._soundDrip[index] = this.Load("Sounds/Drip_" + (object) index);
      for (int index = 0; index < this._soundZombie.Length; ++index)
        this._soundZombie[index] = this.Load("Sounds/Zombie_" + (object) index);
      for (int index = 0; index < this._soundLiquid.Length; ++index)
        this._soundLiquid[index] = this.Load("Sounds/Liquid_" + (object) index);
      for (int index = 0; index < this._soundRoar.Length; ++index)
        this._soundRoar[index] = this.Load("Sounds/Roar_" + (object) index);
      this._soundSplash[0] = this.Load("Sounds/Splash_0");
      this._soundSplash[1] = this.Load("Sounds/Splash_1");
      this._soundDoubleJump = this.Load("Sounds/Double_Jump");
      this._soundRun = this.Load("Sounds/Run");
      this._soundCoins = this.Load("Sounds/Coins");
      this._soundUnlock = this.Load("Sounds/Unlock");
      this._soundMaxMana = this.Load("Sounds/MaxMana");
      this._soundDrown = this.Load("Sounds/Drown");
      for (int index = 1; index < this._soundItem.Length; ++index)
        this._soundItem[index] = this.Load("Sounds/Item_" + (object) index);
      for (int index = 1; index < this._soundNpcHit.Length; ++index)
        this._soundNpcHit[index] = this.Load("Sounds/NPC_Hit_" + (object) index);
      for (int index = 1; index < this._soundNpcKilled.Length; ++index)
        this._soundNpcKilled[index] = this.Load("Sounds/NPC_Killed_" + (object) index);
      this._trackableSounds = new Asset<SoundEffect>[SoundID.TrackableLegacySoundCount];
      this._trackableSoundInstances = new SoundEffectInstance[this._trackableSounds.Length];
      for (int id = 0; id < this._trackableSounds.Length; ++id)
        this._trackableSounds[id] = this.Load("Sounds/Custom" + Path.DirectorySeparatorChar.ToString() + SoundID.GetTrackableLegacySoundPath(id));
    }

    public void CreateAllSoundInstances()
    {
      this._soundInstanceMech[0] = this._soundMech[0].get_Value().CreateInstance();
      this._soundInstanceGrab = this._soundGrab.get_Value().CreateInstance();
      this._soundInstancePixie = this._soundGrab.get_Value().CreateInstance();
      this._soundInstanceDig[0] = this._soundDig[0].get_Value().CreateInstance();
      this._soundInstanceDig[1] = this._soundDig[1].get_Value().CreateInstance();
      this._soundInstanceDig[2] = this._soundDig[2].get_Value().CreateInstance();
      this._soundInstanceTink[0] = this._soundTink[0].get_Value().CreateInstance();
      this._soundInstanceTink[1] = this._soundTink[1].get_Value().CreateInstance();
      this._soundInstanceTink[2] = this._soundTink[2].get_Value().CreateInstance();
      this._soundInstancePlayerHit[0] = this._soundPlayerHit[0].get_Value().CreateInstance();
      this._soundInstancePlayerHit[1] = this._soundPlayerHit[1].get_Value().CreateInstance();
      this._soundInstancePlayerHit[2] = this._soundPlayerHit[2].get_Value().CreateInstance();
      this._soundInstanceFemaleHit[0] = this._soundFemaleHit[0].get_Value().CreateInstance();
      this._soundInstanceFemaleHit[1] = this._soundFemaleHit[1].get_Value().CreateInstance();
      this._soundInstanceFemaleHit[2] = this._soundFemaleHit[2].get_Value().CreateInstance();
      this._soundInstancePlayerKilled = this._soundPlayerKilled.get_Value().CreateInstance();
      this._soundInstanceChat = this._soundChat.get_Value().CreateInstance();
      this._soundInstanceGrass = this._soundGrass.get_Value().CreateInstance();
      this._soundInstanceDoorOpen = this._soundDoorOpen.get_Value().CreateInstance();
      this._soundInstanceDoorClosed = this._soundDoorClosed.get_Value().CreateInstance();
      this._soundInstanceMenuTick = this._soundMenuTick.get_Value().CreateInstance();
      this._soundInstanceMenuOpen = this._soundMenuOpen.get_Value().CreateInstance();
      this._soundInstanceMenuClose = this._soundMenuClose.get_Value().CreateInstance();
      this._soundInstanceShatter = this._soundShatter.get_Value().CreateInstance();
      this._soundInstanceCamera = this._soundCamera.get_Value().CreateInstance();
      for (int index = 0; index < this._soundThunder.Length; ++index)
        this._soundInstanceThunder[index] = this._soundThunder[index].get_Value().CreateInstance();
      for (int index = 0; index < this._soundResearch.Length; ++index)
        this._soundInstanceResearch[index] = this._soundResearch[index].get_Value().CreateInstance();
      for (int index = 0; index < this._soundCoin.Length; ++index)
        this._soundInstanceCoin[index] = this._soundCoin[index].get_Value().CreateInstance();
      for (int index = 0; index < this._soundDrip.Length; ++index)
        this._soundInstanceDrip[index] = this._soundDrip[index].get_Value().CreateInstance();
      for (int index = 0; index < this._soundZombie.Length; ++index)
        this._soundInstanceZombie[index] = this._soundZombie[index].get_Value().CreateInstance();
      for (int index = 0; index < this._soundLiquid.Length; ++index)
        this._soundInstanceLiquid[index] = this._soundLiquid[index].get_Value().CreateInstance();
      for (int index = 0; index < this._soundRoar.Length; ++index)
        this._soundInstanceRoar[index] = this._soundRoar[index].get_Value().CreateInstance();
      this._soundInstanceSplash[0] = this._soundRoar[0].get_Value().CreateInstance();
      this._soundInstanceSplash[1] = this._soundSplash[1].get_Value().CreateInstance();
      this._soundInstanceDoubleJump = this._soundRoar[0].get_Value().CreateInstance();
      this._soundInstanceRun = this._soundRun.get_Value().CreateInstance();
      this._soundInstanceCoins = this._soundCoins.get_Value().CreateInstance();
      this._soundInstanceUnlock = this._soundUnlock.get_Value().CreateInstance();
      this._soundInstanceMaxMana = this._soundMaxMana.get_Value().CreateInstance();
      this._soundInstanceDrown = this._soundDrown.get_Value().CreateInstance();
      for (int index = 1; index < this._soundItem.Length; ++index)
        this._soundInstanceItem[index] = this._soundItem[index].get_Value().CreateInstance();
      for (int index = 1; index < this._soundNpcHit.Length; ++index)
        this._soundInstanceNpcHit[index] = this._soundNpcHit[index].get_Value().CreateInstance();
      for (int index = 1; index < this._soundNpcKilled.Length; ++index)
        this._soundInstanceNpcKilled[index] = this._soundNpcKilled[index].get_Value().CreateInstance();
      for (int index = 0; index < this._trackableSounds.Length; ++index)
        this._trackableSoundInstances[index] = this._trackableSounds[index].get_Value().CreateInstance();
      this._soundInstanceMoonlordCry = this._soundNpcKilled[10].get_Value().CreateInstance();
    }

    private Asset<SoundEffect> Load(string assetName)
    {
      return (Asset<SoundEffect>) ((IAssetRepository) XnaExtensions.Get<IAssetRepository>(this._services)).Request<SoundEffect>(assetName, (AssetRequestMode) 2);
    }

    public SoundEffectInstance PlaySound(
      int type,
      int x = -1,
      int y = -1,
      int Style = 1,
      float volumeScale = 1f,
      float pitchOffset = 0.0f)
    {
      int index1 = Style;
      try
      {
        if (Main.dedServ || (double) Main.soundVolume == 0.0 && (type < 30 || type > 35))
          return (SoundEffectInstance) null;
        bool flag = false;
        float num1 = 1f;
        float num2 = 0.0f;
        if (x == -1 || y == -1)
        {
          flag = true;
        }
        else
        {
          if (WorldGen.gen || Main.netMode == 2)
            return (SoundEffectInstance) null;
          Vector2 vector2 = new Vector2(Main.screenPosition.X + (float) Main.screenWidth * 0.5f, Main.screenPosition.Y + (float) Main.screenHeight * 0.5f);
          double num3 = (double) Math.Abs((float) x - vector2.X);
          float num4 = Math.Abs((float) y - vector2.Y);
          float num5 = (float) Math.Sqrt(num3 * num3 + (double) num4 * (double) num4);
          int num6 = 2500;
          if ((double) num5 < (double) num6)
          {
            flag = true;
            num2 = type != 43 ? (float) (((double) x - (double) vector2.X) / ((double) Main.screenWidth * 0.5)) : (float) (((double) x - (double) vector2.X) / 900.0);
            num1 = (float) (1.0 - (double) num5 / (double) num6);
          }
        }
        if ((double) num2 < -1.0)
          num2 = -1f;
        if ((double) num2 > 1.0)
          num2 = 1f;
        if ((double) num1 > 1.0)
          num1 = 1f;
        if ((double) num1 <= 0.0 && (type < 34 || type > 35 || type > 39))
          return (SoundEffectInstance) null;
        if (flag)
        {
          float num3;
          if (type >= 30 && type <= 35 || type == 39)
          {
            num3 = num1 * (Main.ambientVolume * (Main.gameInactive ? 0.0f : 1f));
            if (Main.gameMenu)
              num3 = 0.0f;
          }
          else
            num3 = num1 * Main.soundVolume;
          if ((double) num3 > 1.0)
            num3 = 1f;
          if ((double) num3 <= 0.0 && (type < 30 || type > 35) && type != 39)
            return (SoundEffectInstance) null;
          SoundEffectInstance sound = (SoundEffectInstance) null;
          if (type == 0)
          {
            int index2 = Main.rand.Next(3);
            if (this._soundInstanceDig[index2] != null)
              this._soundInstanceDig[index2].Stop();
            this._soundInstanceDig[index2] = this._soundDig[index2].get_Value().CreateInstance();
            this._soundInstanceDig[index2].Volume = num3;
            this._soundInstanceDig[index2].Pan = num2;
            this._soundInstanceDig[index2].Pitch = (float) Main.rand.Next(-10, 11) * 0.01f;
            sound = this._soundInstanceDig[index2];
          }
          else if (type == 43)
          {
            int index2 = Main.rand.Next(this._soundThunder.Length);
            for (int index3 = 0; index3 < this._soundThunder.Length && (this._soundInstanceThunder[index2] != null && this._soundInstanceThunder[index2].State == SoundState.Playing); ++index3)
              index2 = Main.rand.Next(this._soundThunder.Length);
            if (this._soundInstanceThunder[index2] != null)
              this._soundInstanceThunder[index2].Stop();
            this._soundInstanceThunder[index2] = this._soundThunder[index2].get_Value().CreateInstance();
            this._soundInstanceThunder[index2].Volume = num3;
            this._soundInstanceThunder[index2].Pan = num2;
            this._soundInstanceThunder[index2].Pitch = (float) Main.rand.Next(-10, 11) * 0.01f;
            sound = this._soundInstanceThunder[index2];
          }
          else if (type == 63)
          {
            int index2 = Main.rand.Next(1, 4);
            if (this._soundInstanceResearch[index2] != null)
              this._soundInstanceResearch[index2].Stop();
            this._soundInstanceResearch[index2] = this._soundResearch[index2].get_Value().CreateInstance();
            this._soundInstanceResearch[index2].Volume = num3;
            this._soundInstanceResearch[index2].Pan = num2;
            sound = this._soundInstanceResearch[index2];
          }
          else if (type == 64)
          {
            if (this._soundInstanceResearch[0] != null)
              this._soundInstanceResearch[0].Stop();
            this._soundInstanceResearch[0] = this._soundResearch[0].get_Value().CreateInstance();
            this._soundInstanceResearch[0].Volume = num3;
            this._soundInstanceResearch[0].Pan = num2;
            sound = this._soundInstanceResearch[0];
          }
          else if (type == 1)
          {
            int index2 = Main.rand.Next(3);
            if (this._soundInstancePlayerHit[index2] != null)
              this._soundInstancePlayerHit[index2].Stop();
            this._soundInstancePlayerHit[index2] = this._soundPlayerHit[index2].get_Value().CreateInstance();
            this._soundInstancePlayerHit[index2].Volume = num3;
            this._soundInstancePlayerHit[index2].Pan = num2;
            sound = this._soundInstancePlayerHit[index2];
          }
          else if (type == 2)
          {
            if (index1 == 129)
              num3 *= 0.6f;
            if (index1 == 123)
              num3 *= 0.5f;
            if (index1 == 124 || index1 == 125)
              num3 *= 0.65f;
            if (index1 == 116)
              num3 *= 0.5f;
            if (index1 == 1)
            {
              int num4 = Main.rand.Next(3);
              if (num4 == 1)
                index1 = 18;
              if (num4 == 2)
                index1 = 19;
            }
            else if (index1 == 55 || index1 == 53)
            {
              num3 *= 0.75f;
              if (index1 == 55)
                num3 *= 0.75f;
              if (this._soundInstanceItem[index1] != null && this._soundInstanceItem[index1].State == SoundState.Playing)
                return (SoundEffectInstance) null;
            }
            else if (index1 == 37)
              num3 *= 0.5f;
            else if (index1 == 52)
              num3 *= 0.35f;
            else if (index1 == 157)
              num3 *= 0.7f;
            else if (index1 == 158)
              num3 *= 0.8f;
            if (index1 == 159)
            {
              if (this._soundInstanceItem[index1] != null && this._soundInstanceItem[index1].State == SoundState.Playing)
                return (SoundEffectInstance) null;
              num3 *= 0.75f;
            }
            else if (index1 != 9 && index1 != 10 && (index1 != 24 && index1 != 26) && (index1 != 34 && index1 != 43 && (index1 != 103 && index1 != 156)) && (index1 != 162 && this._soundInstanceItem[index1] != null))
              this._soundInstanceItem[index1].Stop();
            this._soundInstanceItem[index1] = this._soundItem[index1].get_Value().CreateInstance();
            this._soundInstanceItem[index1].Volume = num3;
            this._soundInstanceItem[index1].Pan = num2;
            switch (index1)
            {
              case 53:
                this._soundInstanceItem[index1].Pitch = (float) Main.rand.Next(-20, -11) * 0.02f;
                break;
              case 55:
                this._soundInstanceItem[index1].Pitch = (float) -Main.rand.Next(-20, -11) * 0.02f;
                break;
              case 132:
                this._soundInstanceItem[index1].Pitch = (float) Main.rand.Next(-20, 21) * (1f / 1000f);
                break;
              case 153:
                this._soundInstanceItem[index1].Pitch = (float) Main.rand.Next(-50, 51) * (3f / 1000f);
                break;
              case 156:
                this._soundInstanceItem[index1].Pitch = (float) Main.rand.Next(-50, 51) * (1f / 500f);
                this._soundInstanceItem[index1].Volume *= 0.6f;
                break;
              default:
                this._soundInstanceItem[index1].Pitch = (float) Main.rand.Next(-6, 7) * 0.01f;
                break;
            }
            if (index1 == 26 || index1 == 35 || index1 == 47)
            {
              this._soundInstanceItem[index1].Volume = num3 * 0.75f;
              this._soundInstanceItem[index1].Pitch = Main.musicPitch;
            }
            if (index1 == 169)
              this._soundInstanceItem[index1].Pitch -= 0.8f;
            sound = this._soundInstanceItem[index1];
          }
          else if (type == 3)
          {
            if (index1 >= 20 && index1 <= 54)
              num3 *= 0.5f;
            if (index1 == 57 && this._soundInstanceNpcHit[index1] != null && this._soundInstanceNpcHit[index1].State == SoundState.Playing)
              return (SoundEffectInstance) null;
            if (index1 == 57)
              num3 *= 0.6f;
            if (index1 == 55 || index1 == 56)
              num3 *= 0.5f;
            if (this._soundInstanceNpcHit[index1] != null)
              this._soundInstanceNpcHit[index1].Stop();
            this._soundInstanceNpcHit[index1] = this._soundNpcHit[index1].get_Value().CreateInstance();
            this._soundInstanceNpcHit[index1].Volume = num3;
            this._soundInstanceNpcHit[index1].Pan = num2;
            this._soundInstanceNpcHit[index1].Pitch = (float) Main.rand.Next(-10, 11) * 0.01f;
            sound = this._soundInstanceNpcHit[index1];
          }
          else if (type == 4)
          {
            if (index1 >= 23 && index1 <= 57)
              num3 *= 0.5f;
            if (index1 == 61)
              num3 *= 0.6f;
            if (index1 == 62)
              num3 *= 0.6f;
            if (index1 == 10 && this._soundInstanceNpcKilled[index1] != null && this._soundInstanceNpcKilled[index1].State == SoundState.Playing)
              return (SoundEffectInstance) null;
            this._soundInstanceNpcKilled[index1] = this._soundNpcKilled[index1].get_Value().CreateInstance();
            this._soundInstanceNpcKilled[index1].Volume = num3;
            this._soundInstanceNpcKilled[index1].Pan = num2;
            this._soundInstanceNpcKilled[index1].Pitch = (float) Main.rand.Next(-10, 11) * 0.01f;
            sound = this._soundInstanceNpcKilled[index1];
          }
          else if (type == 5)
          {
            if (this._soundInstancePlayerKilled != null)
              this._soundInstancePlayerKilled.Stop();
            this._soundInstancePlayerKilled = this._soundPlayerKilled.get_Value().CreateInstance();
            this._soundInstancePlayerKilled.Volume = num3;
            this._soundInstancePlayerKilled.Pan = num2;
            sound = this._soundInstancePlayerKilled;
          }
          else if (type == 6)
          {
            if (this._soundInstanceGrass != null)
              this._soundInstanceGrass.Stop();
            this._soundInstanceGrass = this._soundGrass.get_Value().CreateInstance();
            this._soundInstanceGrass.Volume = num3;
            this._soundInstanceGrass.Pan = num2;
            this._soundInstanceGrass.Pitch = (float) Main.rand.Next(-30, 31) * 0.01f;
            sound = this._soundInstanceGrass;
          }
          else if (type == 7)
          {
            if (this._soundInstanceGrab != null)
              this._soundInstanceGrab.Stop();
            this._soundInstanceGrab = this._soundGrab.get_Value().CreateInstance();
            this._soundInstanceGrab.Volume = num3;
            this._soundInstanceGrab.Pan = num2;
            this._soundInstanceGrab.Pitch = (float) Main.rand.Next(-10, 11) * 0.01f;
            sound = this._soundInstanceGrab;
          }
          else if (type == 8)
          {
            if (this._soundInstanceDoorOpen != null)
              this._soundInstanceDoorOpen.Stop();
            this._soundInstanceDoorOpen = this._soundDoorOpen.get_Value().CreateInstance();
            this._soundInstanceDoorOpen.Volume = num3;
            this._soundInstanceDoorOpen.Pan = num2;
            this._soundInstanceDoorOpen.Pitch = (float) Main.rand.Next(-20, 21) * 0.01f;
            sound = this._soundInstanceDoorOpen;
          }
          else if (type == 9)
          {
            if (this._soundInstanceDoorClosed != null)
              this._soundInstanceDoorClosed.Stop();
            this._soundInstanceDoorClosed = this._soundDoorClosed.get_Value().CreateInstance();
            this._soundInstanceDoorClosed.Volume = num3;
            this._soundInstanceDoorClosed.Pan = num2;
            this._soundInstanceDoorClosed.Pitch = (float) Main.rand.Next(-20, 21) * 0.01f;
            sound = this._soundInstanceDoorClosed;
          }
          else if (type == 10)
          {
            if (this._soundInstanceMenuOpen != null)
              this._soundInstanceMenuOpen.Stop();
            this._soundInstanceMenuOpen = this._soundMenuOpen.get_Value().CreateInstance();
            this._soundInstanceMenuOpen.Volume = num3;
            this._soundInstanceMenuOpen.Pan = num2;
            sound = this._soundInstanceMenuOpen;
          }
          else if (type == 11)
          {
            if (this._soundInstanceMenuClose != null)
              this._soundInstanceMenuClose.Stop();
            this._soundInstanceMenuClose = this._soundMenuClose.get_Value().CreateInstance();
            this._soundInstanceMenuClose.Volume = num3;
            this._soundInstanceMenuClose.Pan = num2;
            sound = this._soundInstanceMenuClose;
          }
          else if (type == 12)
          {
            if (Main.hasFocus)
            {
              if (this._soundInstanceMenuTick != null)
                this._soundInstanceMenuTick.Stop();
              this._soundInstanceMenuTick = this._soundMenuTick.get_Value().CreateInstance();
              this._soundInstanceMenuTick.Volume = num3;
              this._soundInstanceMenuTick.Pan = num2;
              sound = this._soundInstanceMenuTick;
            }
          }
          else if (type == 13)
          {
            if (this._soundInstanceShatter != null)
              this._soundInstanceShatter.Stop();
            this._soundInstanceShatter = this._soundShatter.get_Value().CreateInstance();
            this._soundInstanceShatter.Volume = num3;
            this._soundInstanceShatter.Pan = num2;
            sound = this._soundInstanceShatter;
          }
          else if (type == 14)
          {
            switch (Style)
            {
              case 489:
              case 586:
                int index4 = Main.rand.Next(21, 24);
                this._soundInstanceZombie[index4] = this._soundZombie[index4].get_Value().CreateInstance();
                this._soundInstanceZombie[index4].Volume = num3 * 0.4f;
                this._soundInstanceZombie[index4].Pan = num2;
                sound = this._soundInstanceZombie[index4];
                break;
              case 542:
                int index5 = 7;
                this._soundInstanceZombie[index5] = this._soundZombie[index5].get_Value().CreateInstance();
                this._soundInstanceZombie[index5].Volume = num3 * 0.4f;
                this._soundInstanceZombie[index5].Pan = num2;
                sound = this._soundInstanceZombie[index5];
                break;
              default:
                int index6 = Main.rand.Next(3);
                this._soundInstanceZombie[index6] = this._soundZombie[index6].get_Value().CreateInstance();
                this._soundInstanceZombie[index6].Volume = num3 * 0.4f;
                this._soundInstanceZombie[index6].Pan = num2;
                sound = this._soundInstanceZombie[index6];
                break;
            }
          }
          else if (type == 15)
          {
            float num4 = 1f;
            if (index1 == 4)
            {
              index1 = 1;
              num4 = 0.25f;
            }
            if (this._soundInstanceRoar[index1] == null || this._soundInstanceRoar[index1].State == SoundState.Stopped)
            {
              this._soundInstanceRoar[index1] = this._soundRoar[index1].get_Value().CreateInstance();
              this._soundInstanceRoar[index1].Volume = num3 * num4;
              this._soundInstanceRoar[index1].Pan = num2;
              sound = this._soundInstanceRoar[index1];
            }
          }
          else if (type == 16)
          {
            if (this._soundInstanceDoubleJump != null)
              this._soundInstanceDoubleJump.Stop();
            this._soundInstanceDoubleJump = this._soundDoubleJump.get_Value().CreateInstance();
            this._soundInstanceDoubleJump.Volume = num3;
            this._soundInstanceDoubleJump.Pan = num2;
            this._soundInstanceDoubleJump.Pitch = (float) Main.rand.Next(-10, 11) * 0.01f;
            sound = this._soundInstanceDoubleJump;
          }
          else if (type == 17)
          {
            if (this._soundInstanceRun != null)
              this._soundInstanceRun.Stop();
            this._soundInstanceRun = this._soundRun.get_Value().CreateInstance();
            this._soundInstanceRun.Volume = num3;
            this._soundInstanceRun.Pan = num2;
            this._soundInstanceRun.Pitch = (float) Main.rand.Next(-10, 11) * 0.01f;
            sound = this._soundInstanceRun;
          }
          else if (type == 18)
          {
            this._soundInstanceCoins = this._soundCoins.get_Value().CreateInstance();
            this._soundInstanceCoins.Volume = num3;
            this._soundInstanceCoins.Pan = num2;
            sound = this._soundInstanceCoins;
          }
          else if (type == 19)
          {
            if (this._soundInstanceSplash[index1] == null || this._soundInstanceSplash[index1].State == SoundState.Stopped)
            {
              this._soundInstanceSplash[index1] = this._soundSplash[index1].get_Value().CreateInstance();
              this._soundInstanceSplash[index1].Volume = num3;
              this._soundInstanceSplash[index1].Pan = num2;
              this._soundInstanceSplash[index1].Pitch = (float) Main.rand.Next(-10, 11) * 0.01f;
              sound = this._soundInstanceSplash[index1];
            }
          }
          else if (type == 20)
          {
            int index2 = Main.rand.Next(3);
            if (this._soundInstanceFemaleHit[index2] != null)
              this._soundInstanceFemaleHit[index2].Stop();
            this._soundInstanceFemaleHit[index2] = this._soundFemaleHit[index2].get_Value().CreateInstance();
            this._soundInstanceFemaleHit[index2].Volume = num3;
            this._soundInstanceFemaleHit[index2].Pan = num2;
            sound = this._soundInstanceFemaleHit[index2];
          }
          else if (type == 21)
          {
            int index2 = Main.rand.Next(3);
            if (this._soundInstanceTink[index2] != null)
              this._soundInstanceTink[index2].Stop();
            this._soundInstanceTink[index2] = this._soundTink[index2].get_Value().CreateInstance();
            this._soundInstanceTink[index2].Volume = num3;
            this._soundInstanceTink[index2].Pan = num2;
            sound = this._soundInstanceTink[index2];
          }
          else if (type == 22)
          {
            if (this._soundInstanceUnlock != null)
              this._soundInstanceUnlock.Stop();
            this._soundInstanceUnlock = this._soundUnlock.get_Value().CreateInstance();
            this._soundInstanceUnlock.Volume = num3;
            this._soundInstanceUnlock.Pan = num2;
            sound = this._soundInstanceUnlock;
          }
          else if (type == 23)
          {
            if (this._soundInstanceDrown != null)
              this._soundInstanceDrown.Stop();
            this._soundInstanceDrown = this._soundDrown.get_Value().CreateInstance();
            this._soundInstanceDrown.Volume = num3;
            this._soundInstanceDrown.Pan = num2;
            sound = this._soundInstanceDrown;
          }
          else if (type == 24)
          {
            this._soundInstanceChat = this._soundChat.get_Value().CreateInstance();
            this._soundInstanceChat.Volume = num3;
            this._soundInstanceChat.Pan = num2;
            sound = this._soundInstanceChat;
          }
          else if (type == 25)
          {
            this._soundInstanceMaxMana = this._soundMaxMana.get_Value().CreateInstance();
            this._soundInstanceMaxMana.Volume = num3;
            this._soundInstanceMaxMana.Pan = num2;
            sound = this._soundInstanceMaxMana;
          }
          else if (type == 26)
          {
            int index2 = Main.rand.Next(3, 5);
            this._soundInstanceZombie[index2] = this._soundZombie[index2].get_Value().CreateInstance();
            this._soundInstanceZombie[index2].Volume = num3 * 0.9f;
            this._soundInstanceZombie[index2].Pan = num2;
            this._soundInstanceZombie[index2].Pitch = (float) Main.rand.Next(-10, 11) * 0.01f;
            sound = this._soundInstanceZombie[index2];
          }
          else if (type == 27)
          {
            if (this._soundInstancePixie != null && this._soundInstancePixie.State == SoundState.Playing)
            {
              this._soundInstancePixie.Volume = num3;
              this._soundInstancePixie.Pan = num2;
              this._soundInstancePixie.Pitch = (float) Main.rand.Next(-10, 11) * 0.01f;
              return (SoundEffectInstance) null;
            }
            if (this._soundInstancePixie != null)
              this._soundInstancePixie.Stop();
            this._soundInstancePixie = this._soundPixie.get_Value().CreateInstance();
            this._soundInstancePixie.Volume = num3;
            this._soundInstancePixie.Pan = num2;
            this._soundInstancePixie.Pitch = (float) Main.rand.Next(-10, 11) * 0.01f;
            sound = this._soundInstancePixie;
          }
          else if (type == 28)
          {
            if (this._soundInstanceMech[index1] != null && this._soundInstanceMech[index1].State == SoundState.Playing)
              return (SoundEffectInstance) null;
            this._soundInstanceMech[index1] = this._soundMech[index1].get_Value().CreateInstance();
            this._soundInstanceMech[index1].Volume = num3;
            this._soundInstanceMech[index1].Pan = num2;
            this._soundInstanceMech[index1].Pitch = (float) Main.rand.Next(-10, 11) * 0.01f;
            sound = this._soundInstanceMech[index1];
          }
          else if (type == 29)
          {
            if (index1 >= 24 && index1 <= 87)
              num3 *= 0.5f;
            if (index1 >= 88 && index1 <= 91)
              num3 *= 0.7f;
            if (index1 >= 93 && index1 <= 99)
              num3 *= 0.4f;
            if (index1 == 92)
              num3 *= 0.5f;
            if (index1 == 103)
              num3 *= 0.4f;
            if (index1 == 104)
              num3 *= 0.55f;
            if (index1 == 100 || index1 == 101)
              num3 *= 0.25f;
            if (index1 == 102)
              num3 *= 0.4f;
            if (this._soundInstanceZombie[index1] != null && this._soundInstanceZombie[index1].State == SoundState.Playing)
              return (SoundEffectInstance) null;
            this._soundInstanceZombie[index1] = this._soundZombie[index1].get_Value().CreateInstance();
            this._soundInstanceZombie[index1].Volume = num3;
            this._soundInstanceZombie[index1].Pan = num2;
            this._soundInstanceZombie[index1].Pitch = (float) Main.rand.Next(-10, 11) * 0.01f;
            sound = this._soundInstanceZombie[index1];
          }
          else if (type == 44)
          {
            int index2 = Main.rand.Next(106, 109);
            this._soundInstanceZombie[index2] = this._soundZombie[index2].get_Value().CreateInstance();
            this._soundInstanceZombie[index2].Volume = num3 * 0.2f;
            this._soundInstanceZombie[index2].Pan = num2;
            this._soundInstanceZombie[index2].Pitch = (float) Main.rand.Next(-70, 1) * 0.01f;
            sound = this._soundInstanceZombie[index2];
          }
          else if (type == 45)
          {
            int index2 = 109;
            if (this._soundInstanceZombie[index2] != null && this._soundInstanceZombie[index2].State == SoundState.Playing)
              return (SoundEffectInstance) null;
            this._soundInstanceZombie[index2] = this._soundZombie[index2].get_Value().CreateInstance();
            this._soundInstanceZombie[index2].Volume = num3 * 0.3f;
            this._soundInstanceZombie[index2].Pan = num2;
            this._soundInstanceZombie[index2].Pitch = (float) Main.rand.Next(-10, 11) * 0.01f;
            sound = this._soundInstanceZombie[index2];
          }
          else if (type == 46)
          {
            if (this._soundInstanceZombie[110] != null && this._soundInstanceZombie[110].State == SoundState.Playing || this._soundInstanceZombie[111] != null && this._soundInstanceZombie[111].State == SoundState.Playing)
              return (SoundEffectInstance) null;
            int index2 = Main.rand.Next(110, 112);
            if (Main.rand.Next(300) == 0)
              index2 = Main.rand.Next(3) != 0 ? (Main.rand.Next(2) != 0 ? 112 : 113) : 114;
            this._soundInstanceZombie[index2] = this._soundZombie[index2].get_Value().CreateInstance();
            this._soundInstanceZombie[index2].Volume = num3 * 0.9f;
            this._soundInstanceZombie[index2].Pan = num2;
            this._soundInstanceZombie[index2].Pitch = (float) Main.rand.Next(-10, 11) * 0.01f;
            sound = this._soundInstanceZombie[index2];
          }
          else if (type == 45)
          {
            int index2 = 109;
            this._soundInstanceZombie[index2] = this._soundZombie[index2].get_Value().CreateInstance();
            this._soundInstanceZombie[index2].Volume = num3 * 0.2f;
            this._soundInstanceZombie[index2].Pan = num2;
            this._soundInstanceZombie[index2].Pitch = (float) Main.rand.Next(-70, 1) * 0.01f;
            sound = this._soundInstanceZombie[index2];
          }
          else if (type == 30)
          {
            int index2 = Main.rand.Next(10, 12);
            if (Main.rand.Next(300) == 0)
            {
              index2 = 12;
              if (this._soundInstanceZombie[index2] != null && this._soundInstanceZombie[index2].State == SoundState.Playing)
                return (SoundEffectInstance) null;
            }
            this._soundInstanceZombie[index2] = this._soundZombie[index2].get_Value().CreateInstance();
            this._soundInstanceZombie[index2].Volume = num3 * 0.75f;
            this._soundInstanceZombie[index2].Pan = num2;
            this._soundInstanceZombie[index2].Pitch = index2 == 12 ? (float) Main.rand.Next(-40, 21) * 0.01f : (float) Main.rand.Next(-70, 1) * 0.01f;
            sound = this._soundInstanceZombie[index2];
          }
          else if (type == 31)
          {
            int index2 = 13;
            this._soundInstanceZombie[index2] = this._soundZombie[index2].get_Value().CreateInstance();
            this._soundInstanceZombie[index2].Volume = num3 * 0.35f;
            this._soundInstanceZombie[index2].Pan = num2;
            this._soundInstanceZombie[index2].Pitch = (float) Main.rand.Next(-40, 21) * 0.01f;
            sound = this._soundInstanceZombie[index2];
          }
          else if (type == 32)
          {
            if (this._soundInstanceZombie[index1] != null && this._soundInstanceZombie[index1].State == SoundState.Playing)
              return (SoundEffectInstance) null;
            this._soundInstanceZombie[index1] = this._soundZombie[index1].get_Value().CreateInstance();
            this._soundInstanceZombie[index1].Volume = num3 * 0.15f;
            this._soundInstanceZombie[index1].Pan = num2;
            this._soundInstanceZombie[index1].Pitch = (float) Main.rand.Next(-70, 26) * 0.01f;
            sound = this._soundInstanceZombie[index1];
          }
          else if (type == 33)
          {
            int index2 = 15;
            if (this._soundInstanceZombie[index2] != null && this._soundInstanceZombie[index2].State == SoundState.Playing)
              return (SoundEffectInstance) null;
            this._soundInstanceZombie[index2] = this._soundZombie[index2].get_Value().CreateInstance();
            this._soundInstanceZombie[index2].Volume = num3 * 0.2f;
            this._soundInstanceZombie[index2].Pan = num2;
            this._soundInstanceZombie[index2].Pitch = (float) Main.rand.Next(-10, 31) * 0.01f;
            sound = this._soundInstanceZombie[index2];
          }
          else if (type >= 47 && type <= 52)
          {
            int index2 = 133 + type - 47;
            for (int index3 = 133; index3 <= 138; ++index3)
            {
              if (this._soundInstanceItem[index3] != null && this._soundInstanceItem[index3].State == SoundState.Playing)
                this._soundInstanceItem[index3].Stop();
            }
            this._soundInstanceItem[index2] = this._soundItem[index2].get_Value().CreateInstance();
            this._soundInstanceItem[index2].Volume = num3 * 0.45f;
            this._soundInstanceItem[index2].Pan = num2;
            sound = this._soundInstanceItem[index2];
          }
          else if (type >= 53 && type <= 62)
          {
            int index2 = 139 + type - 53;
            if (this._soundInstanceItem[index2] != null && this._soundInstanceItem[index2].State == SoundState.Playing)
              this._soundInstanceItem[index2].Stop();
            this._soundInstanceItem[index2] = this._soundItem[index2].get_Value().CreateInstance();
            this._soundInstanceItem[index2].Volume = num3 * 0.7f;
            this._soundInstanceItem[index2].Pan = num2;
            sound = this._soundInstanceItem[index2];
          }
          else
          {
            switch (type)
            {
              case 34:
                float num5 = (float) index1 / 50f;
                if ((double) num5 > 1.0)
                  num5 = 1f;
                float num6 = num3 * num5 * 0.2f;
                if ((double) num6 <= 0.0 || x == -1 || y == -1)
                {
                  if (this._soundInstanceLiquid[0] != null && this._soundInstanceLiquid[0].State == SoundState.Playing)
                  {
                    this._soundInstanceLiquid[0].Stop();
                    break;
                  }
                  break;
                }
                if (this._soundInstanceLiquid[0] != null && this._soundInstanceLiquid[0].State == SoundState.Playing)
                {
                  this._soundInstanceLiquid[0].Volume = num6;
                  this._soundInstanceLiquid[0].Pan = num2;
                  this._soundInstanceLiquid[0].Pitch = -0.2f;
                  break;
                }
                this._soundInstanceLiquid[0] = this._soundLiquid[0].get_Value().CreateInstance();
                this._soundInstanceLiquid[0].Volume = num6;
                this._soundInstanceLiquid[0].Pan = num2;
                sound = this._soundInstanceLiquid[0];
                break;
              case 35:
                float num7 = (float) index1 / 50f;
                if ((double) num7 > 1.0)
                  num7 = 1f;
                float num8 = num3 * num7 * 0.65f;
                if ((double) num8 <= 0.0 || x == -1 || y == -1)
                {
                  if (this._soundInstanceLiquid[1] != null && this._soundInstanceLiquid[1].State == SoundState.Playing)
                  {
                    this._soundInstanceLiquid[1].Stop();
                    break;
                  }
                  break;
                }
                if (this._soundInstanceLiquid[1] != null && this._soundInstanceLiquid[1].State == SoundState.Playing)
                {
                  this._soundInstanceLiquid[1].Volume = num8;
                  this._soundInstanceLiquid[1].Pan = num2;
                  this._soundInstanceLiquid[1].Pitch = -0.0f;
                  break;
                }
                this._soundInstanceLiquid[1] = this._soundLiquid[1].get_Value().CreateInstance();
                this._soundInstanceLiquid[1].Volume = num8;
                this._soundInstanceLiquid[1].Pan = num2;
                sound = this._soundInstanceLiquid[1];
                break;
              case 36:
                int index7 = Style;
                if (Style == -1)
                  index7 = 0;
                this._soundInstanceRoar[index7] = this._soundRoar[index7].get_Value().CreateInstance();
                this._soundInstanceRoar[index7].Volume = num3;
                this._soundInstanceRoar[index7].Pan = num2;
                if (Style == -1)
                  this._soundInstanceRoar[index7].Pitch += 0.6f;
                sound = this._soundInstanceRoar[index7];
                break;
              case 37:
                int index8 = Main.rand.Next(57, 59);
                float num9 = num3 * ((float) Style * 0.05f);
                this._soundInstanceItem[index8] = this._soundItem[index8].get_Value().CreateInstance();
                this._soundInstanceItem[index8].Volume = num9;
                this._soundInstanceItem[index8].Pan = num2;
                this._soundInstanceItem[index8].Pitch = (float) Main.rand.Next(-40, 41) * 0.01f;
                sound = this._soundInstanceItem[index8];
                break;
              case 38:
                int index9 = Main.rand.Next(5);
                this._soundInstanceCoin[index9] = this._soundCoin[index9].get_Value().CreateInstance();
                this._soundInstanceCoin[index9].Volume = num3;
                this._soundInstanceCoin[index9].Pan = num2;
                this._soundInstanceCoin[index9].Pitch = (float) Main.rand.Next(-40, 41) * (1f / 500f);
                sound = this._soundInstanceCoin[index9];
                break;
              case 39:
                int index10 = Style;
                this._soundInstanceDrip[index10] = this._soundDrip[index10].get_Value().CreateInstance();
                this._soundInstanceDrip[index10].Volume = num3 * 0.5f;
                this._soundInstanceDrip[index10].Pan = num2;
                this._soundInstanceDrip[index10].Pitch = (float) Main.rand.Next(-30, 31) * 0.01f;
                sound = this._soundInstanceDrip[index10];
                break;
              case 40:
                if (this._soundInstanceCamera != null)
                  this._soundInstanceCamera.Stop();
                this._soundInstanceCamera = this._soundCamera.get_Value().CreateInstance();
                this._soundInstanceCamera.Volume = num3;
                this._soundInstanceCamera.Pan = num2;
                sound = this._soundInstanceCamera;
                break;
              case 41:
                this._soundInstanceMoonlordCry = this._soundNpcKilled[10].get_Value().CreateInstance();
                this._soundInstanceMoonlordCry.Volume = (float) (1.0 / (1.0 + (double) (new Vector2((float) x, (float) y) - Main.player[Main.myPlayer].position).Length()));
                this._soundInstanceMoonlordCry.Pan = num2;
                this._soundInstanceMoonlordCry.Pitch = (float) Main.rand.Next(-10, 11) * 0.01f;
                sound = this._soundInstanceMoonlordCry;
                break;
              case 42:
                sound = this._trackableSounds[index1].get_Value().CreateInstance();
                sound.Volume = num3;
                sound.Pan = num2;
                this._trackableSoundInstances[index1] = sound;
                break;
              case 65:
                if (this._soundInstanceZombie[115] != null && this._soundInstanceZombie[115].State == SoundState.Playing || this._soundInstanceZombie[116] != null && this._soundInstanceZombie[116].State == SoundState.Playing || this._soundInstanceZombie[117] != null && this._soundInstanceZombie[117].State == SoundState.Playing)
                  return (SoundEffectInstance) null;
                int index11 = Main.rand.Next(115, 118);
                this._soundInstanceZombie[index11] = this._soundZombie[index11].get_Value().CreateInstance();
                this._soundInstanceZombie[index11].Volume = num3 * 0.5f;
                this._soundInstanceZombie[index11].Pan = num2;
                sound = this._soundInstanceZombie[index11];
                break;
            }
          }
          if (sound != null)
          {
            sound.Pitch += pitchOffset;
            sound.Volume *= volumeScale;
            sound.Play();
            SoundInstanceGarbageCollector.Track(sound);
          }
          return sound;
        }
      }
      catch
      {
      }
      return (SoundEffectInstance) null;
    }

    public SoundEffect GetTrackableSoundByStyleId(int id)
    {
      return this._trackableSounds[id].get_Value();
    }

    public void StopAmbientSounds()
    {
      for (int index = 0; index < this._soundInstanceLiquid.Length; ++index)
      {
        if (this._soundInstanceLiquid[index] != null)
          this._soundInstanceLiquid[index].Stop();
      }
    }
  }
}
