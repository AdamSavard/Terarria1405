﻿// Decompiled with JetBrains decompiler
// Type: Terraria.Initializers.DyeInitializer
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.GameContent.Dyes;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace Terraria.Initializers
{
  public static class DyeInitializer
  {
    private static void LoadBasicColorDye(
      int baseDyeItem,
      int blackDyeItem,
      int brightDyeItem,
      int silverDyeItem,
      float r,
      float g,
      float b,
      float saturation = 1f,
      int oldShader = 1)
    {
      Ref<Effect> pixelShaderRef = Main.PixelShaderRef;
      GameShaders.Armor.BindShader<ArmorShaderData>(baseDyeItem, new ArmorShaderData(pixelShaderRef, "ArmorColored")).UseColor(r, g, b).UseSaturation(saturation);
      GameShaders.Armor.BindShader<ArmorShaderData>(blackDyeItem, new ArmorShaderData(pixelShaderRef, "ArmorColoredAndBlack")).UseColor(r, g, b).UseSaturation(saturation);
      GameShaders.Armor.BindShader<ArmorShaderData>(brightDyeItem, new ArmorShaderData(pixelShaderRef, "ArmorColored")).UseColor((float) ((double) r * 0.5 + 0.5), (float) ((double) g * 0.5 + 0.5), (float) ((double) b * 0.5 + 0.5)).UseSaturation(saturation);
      GameShaders.Armor.BindShader<ArmorShaderData>(silverDyeItem, new ArmorShaderData(pixelShaderRef, "ArmorColoredAndSilverTrim")).UseColor(r, g, b).UseSaturation(saturation);
    }

    private static void LoadBasicColorDye(
      int baseDyeItem,
      float r,
      float g,
      float b,
      float saturation = 1f,
      int oldShader = 1)
    {
      DyeInitializer.LoadBasicColorDye(baseDyeItem, baseDyeItem + 12, baseDyeItem + 31, baseDyeItem + 44, r, g, b, saturation, oldShader);
    }

    private static void LoadBasicColorDyes()
    {
      DyeInitializer.LoadBasicColorDye(1007, 1f, 0.0f, 0.0f, 1.2f, 1);
      DyeInitializer.LoadBasicColorDye(1008, 1f, 0.5f, 0.0f, 1.2f, 2);
      DyeInitializer.LoadBasicColorDye(1009, 1f, 1f, 0.0f, 1.2f, 3);
      DyeInitializer.LoadBasicColorDye(1010, 0.5f, 1f, 0.0f, 1.2f, 4);
      DyeInitializer.LoadBasicColorDye(1011, 0.0f, 1f, 0.0f, 1.2f, 5);
      DyeInitializer.LoadBasicColorDye(1012, 0.0f, 1f, 0.5f, 1.2f, 6);
      DyeInitializer.LoadBasicColorDye(1013, 0.0f, 1f, 1f, 1.2f, 7);
      DyeInitializer.LoadBasicColorDye(1014, 0.2f, 0.5f, 1f, 1.2f, 8);
      DyeInitializer.LoadBasicColorDye(1015, 0.0f, 0.0f, 1f, 1.2f, 9);
      DyeInitializer.LoadBasicColorDye(1016, 0.5f, 0.0f, 1f, 1.2f, 10);
      DyeInitializer.LoadBasicColorDye(1017, 1f, 0.0f, 1f, 1.2f, 11);
      DyeInitializer.LoadBasicColorDye(1018, 1f, 0.1f, 0.5f, 1.3f, 12);
      DyeInitializer.LoadBasicColorDye(2874, 2875, 2876, 2877, 0.4f, 0.2f, 0.0f, 1f, 1);
    }

    private static void LoadArmorDyes()
    {
      Ref<Effect> pixelShaderRef = Main.PixelShaderRef;
      DyeInitializer.LoadBasicColorDyes();
      GameShaders.Armor.BindShader<ArmorShaderData>(1050, new ArmorShaderData(pixelShaderRef, "ArmorBrightnessColored")).UseColor(0.6f, 0.6f, 0.6f);
      GameShaders.Armor.BindShader<ArmorShaderData>(1037, new ArmorShaderData(pixelShaderRef, "ArmorBrightnessColored")).UseColor(1f, 1f, 1f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3558, new ArmorShaderData(pixelShaderRef, "ArmorBrightnessColored")).UseColor(1.5f, 1.5f, 1.5f);
      GameShaders.Armor.BindShader<ArmorShaderData>(2871, new ArmorShaderData(pixelShaderRef, "ArmorBrightnessColored")).UseColor(0.05f, 0.05f, 0.05f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3559, new ArmorShaderData(pixelShaderRef, "ArmorColoredAndBlack")).UseColor(1f, 1f, 1f).UseSaturation(1.2f);
      GameShaders.Armor.BindShader<ArmorShaderData>(1031, new ArmorShaderData(pixelShaderRef, "ArmorColoredGradient")).UseColor(1f, 0.0f, 0.0f).UseSecondaryColor(1f, 1f, 0.0f).UseSaturation(1.2f);
      GameShaders.Armor.BindShader<ArmorShaderData>(1032, new ArmorShaderData(pixelShaderRef, "ArmorColoredAndBlackGradient")).UseColor(1f, 0.0f, 0.0f).UseSecondaryColor(1f, 1f, 0.0f).UseSaturation(1.5f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3550, new ArmorShaderData(pixelShaderRef, "ArmorColoredAndSilverTrimGradient")).UseColor(1f, 0.0f, 0.0f).UseSecondaryColor(1f, 1f, 0.0f).UseSaturation(1.5f);
      GameShaders.Armor.BindShader<ArmorShaderData>(1063, new ArmorShaderData(pixelShaderRef, "ArmorBrightnessGradient")).UseColor(1f, 0.0f, 0.0f).UseSecondaryColor(1f, 1f, 0.0f);
      GameShaders.Armor.BindShader<ArmorShaderData>(1035, new ArmorShaderData(pixelShaderRef, "ArmorColoredGradient")).UseColor(0.0f, 0.0f, 1f).UseSecondaryColor(0.0f, 1f, 1f).UseSaturation(1.2f);
      GameShaders.Armor.BindShader<ArmorShaderData>(1036, new ArmorShaderData(pixelShaderRef, "ArmorColoredAndBlackGradient")).UseColor(0.0f, 0.0f, 1f).UseSecondaryColor(0.0f, 1f, 1f).UseSaturation(1.5f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3552, new ArmorShaderData(pixelShaderRef, "ArmorColoredAndSilverTrimGradient")).UseColor(0.0f, 0.0f, 1f).UseSecondaryColor(0.0f, 1f, 1f).UseSaturation(1.5f);
      GameShaders.Armor.BindShader<ArmorShaderData>(1065, new ArmorShaderData(pixelShaderRef, "ArmorBrightnessGradient")).UseColor(0.0f, 0.0f, 1f).UseSecondaryColor(0.0f, 1f, 1f);
      GameShaders.Armor.BindShader<ArmorShaderData>(1033, new ArmorShaderData(pixelShaderRef, "ArmorColoredGradient")).UseColor(0.0f, 1f, 0.0f).UseSecondaryColor(1f, 1f, 0.0f).UseSaturation(1.2f);
      GameShaders.Armor.BindShader<ArmorShaderData>(1034, new ArmorShaderData(pixelShaderRef, "ArmorColoredAndBlackGradient")).UseColor(0.0f, 1f, 0.0f).UseSecondaryColor(1f, 1f, 0.0f).UseSaturation(1.5f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3551, new ArmorShaderData(pixelShaderRef, "ArmorColoredAndSilverTrimGradient")).UseColor(0.0f, 1f, 0.0f).UseSecondaryColor(1f, 1f, 0.0f).UseSaturation(1.5f);
      GameShaders.Armor.BindShader<ArmorShaderData>(1064, new ArmorShaderData(pixelShaderRef, "ArmorBrightnessGradient")).UseColor(0.0f, 1f, 0.0f).UseSecondaryColor(1f, 1f, 0.0f);
      GameShaders.Armor.BindShader<ArmorShaderData>(1068, new ArmorShaderData(pixelShaderRef, "ArmorColoredGradient")).UseColor(0.5f, 1f, 0.0f).UseSecondaryColor(1f, 0.5f, 0.0f).UseSaturation(1.5f);
      GameShaders.Armor.BindShader<ArmorShaderData>(1069, new ArmorShaderData(pixelShaderRef, "ArmorColoredGradient")).UseColor(0.0f, 1f, 0.5f).UseSecondaryColor(0.0f, 0.5f, 1f).UseSaturation(1.5f);
      GameShaders.Armor.BindShader<ArmorShaderData>(1070, new ArmorShaderData(pixelShaderRef, "ArmorColoredGradient")).UseColor(1f, 0.0f, 0.5f).UseSecondaryColor(0.5f, 0.0f, 1f).UseSaturation(1.5f);
      GameShaders.Armor.BindShader<ArmorShaderData>(1066, new ArmorShaderData(pixelShaderRef, "ArmorColoredRainbow"));
      GameShaders.Armor.BindShader<ArmorShaderData>(1067, new ArmorShaderData(pixelShaderRef, "ArmorBrightnessRainbow"));
      GameShaders.Armor.BindShader<ArmorShaderData>(3556, new ArmorShaderData(pixelShaderRef, "ArmorMidnightRainbow"));
      GameShaders.Armor.BindShader<ArmorShaderData>(2869, new ArmorShaderData(pixelShaderRef, "ArmorLivingFlame")).UseColor(1f, 0.9f, 0.0f).UseSecondaryColor(1f, 0.2f, 0.0f);
      GameShaders.Armor.BindShader<ArmorShaderData>(2870, new ArmorShaderData(pixelShaderRef, "ArmorLivingRainbow"));
      GameShaders.Armor.BindShader<ArmorShaderData>(2873, new ArmorShaderData(pixelShaderRef, "ArmorLivingOcean"));
      GameShaders.Armor.BindShader<ReflectiveArmorShaderData>(3026, new ReflectiveArmorShaderData(pixelShaderRef, "ArmorReflectiveColor")).UseColor(1f, 1f, 1f);
      GameShaders.Armor.BindShader<ReflectiveArmorShaderData>(3027, new ReflectiveArmorShaderData(pixelShaderRef, "ArmorReflectiveColor")).UseColor(1.5f, 1.2f, 0.5f);
      GameShaders.Armor.BindShader<ReflectiveArmorShaderData>(3553, new ReflectiveArmorShaderData(pixelShaderRef, "ArmorReflectiveColor")).UseColor(1.35f, 0.7f, 0.4f);
      GameShaders.Armor.BindShader<ReflectiveArmorShaderData>(3554, new ReflectiveArmorShaderData(pixelShaderRef, "ArmorReflectiveColor")).UseColor(0.25f, 0.0f, 0.7f);
      GameShaders.Armor.BindShader<ReflectiveArmorShaderData>(3555, new ReflectiveArmorShaderData(pixelShaderRef, "ArmorReflectiveColor")).UseColor(0.4f, 0.4f, 0.4f);
      GameShaders.Armor.BindShader<ReflectiveArmorShaderData>(3190, new ReflectiveArmorShaderData(pixelShaderRef, "ArmorReflective"));
      GameShaders.Armor.BindShader<TeamArmorShaderData>(1969, new TeamArmorShaderData(pixelShaderRef, "ArmorColored"));
      GameShaders.Armor.BindShader<ArmorShaderData>(2864, new ArmorShaderData(pixelShaderRef, "ArmorMartian")).UseColor(0.0f, 2f, 3f);
      GameShaders.Armor.BindShader<ArmorShaderData>(2872, new ArmorShaderData(pixelShaderRef, "ArmorInvert"));
      GameShaders.Armor.BindShader<ArmorShaderData>(2878, new ArmorShaderData(pixelShaderRef, "ArmorWisp")).UseColor(0.7f, 1f, 0.9f).UseSecondaryColor(0.35f, 0.85f, 0.8f);
      GameShaders.Armor.BindShader<ArmorShaderData>(2879, new ArmorShaderData(pixelShaderRef, "ArmorWisp")).UseColor(1f, 1.2f, 0.0f).UseSecondaryColor(1f, 0.6f, 0.3f);
      GameShaders.Armor.BindShader<ArmorShaderData>(2885, new ArmorShaderData(pixelShaderRef, "ArmorWisp")).UseColor(1.2f, 0.8f, 0.0f).UseSecondaryColor(0.8f, 0.2f, 0.0f);
      GameShaders.Armor.BindShader<ArmorShaderData>(2884, new ArmorShaderData(pixelShaderRef, "ArmorWisp")).UseColor(1f, 0.0f, 1f).UseSecondaryColor(1f, 0.3f, 0.6f);
      GameShaders.Armor.BindShader<ArmorShaderData>(2883, new ArmorShaderData(pixelShaderRef, "ArmorHighContrastGlow")).UseColor(0.0f, 1f, 0.0f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3025, new ArmorShaderData(pixelShaderRef, "ArmorFlow")).UseColor(1f, 0.5f, 1f).UseSecondaryColor(0.6f, 0.1f, 1f);
      GameShaders.Armor.BindShader<TwilightDyeShaderData>(3039, new TwilightDyeShaderData(pixelShaderRef, "ArmorTwilight")).UseImage("Images/Misc/noise").UseColor(0.5f, 0.1f, 1f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3040, new ArmorShaderData(pixelShaderRef, "ArmorAcid")).UseColor(0.5f, 1f, 0.3f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3041, new ArmorShaderData(pixelShaderRef, "ArmorMushroom")).UseColor(0.05f, 0.2f, 1f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3042, new ArmorShaderData(pixelShaderRef, "ArmorPhase")).UseImage("Images/Misc/noise").UseColor(0.4f, 0.2f, 1.5f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3560, new ArmorShaderData(pixelShaderRef, "ArmorAcid")).UseColor(0.9f, 0.2f, 0.2f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3561, new ArmorShaderData(pixelShaderRef, "ArmorGel")).UseImage("Images/Misc/noise").UseColor(0.4f, 0.7f, 1.4f).UseSecondaryColor(0.0f, 0.0f, 0.1f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3562, new ArmorShaderData(pixelShaderRef, "ArmorGel")).UseImage("Images/Misc/noise").UseColor(1.4f, 0.75f, 1f).UseSecondaryColor(0.45f, 0.1f, 0.3f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3024, new ArmorShaderData(pixelShaderRef, "ArmorGel")).UseImage("Images/Misc/noise").UseColor(-0.5f, -1f, 0.0f).UseSecondaryColor(1.5f, 1f, 2.2f);
      GameShaders.Armor.BindShader<ArmorShaderData>(4663, new ArmorShaderData(pixelShaderRef, "ArmorGel")).UseImage("Images/Misc/noise").UseColor(2.6f, 0.6f, 0.6f).UseSecondaryColor(0.2f, -0.2f, -0.2f);
      GameShaders.Armor.BindShader<ArmorShaderData>(4662, new ArmorShaderData(pixelShaderRef, "ArmorFog")).UseImage("Images/Misc/noise").UseColor(0.95f, 0.95f, 0.95f).UseSecondaryColor(0.3f, 0.3f, 0.3f);
      GameShaders.Armor.BindShader<ArmorShaderData>(4778, new ArmorShaderData(pixelShaderRef, "ArmorHallowBoss")).UseImage("Images/Extra_" + (object) (short) 156);
      GameShaders.Armor.BindShader<ArmorShaderData>(3534, new ArmorShaderData(pixelShaderRef, "ArmorMirage"));
      GameShaders.Armor.BindShader<ArmorShaderData>(3028, new ArmorShaderData(pixelShaderRef, "ArmorAcid")).UseColor(0.5f, 0.7f, 1.5f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3557, new ArmorShaderData(pixelShaderRef, "ArmorPolarized"));
      GameShaders.Armor.BindShader<ArmorShaderData>(3978, new ArmorShaderData(pixelShaderRef, "ColorOnly"));
      GameShaders.Armor.BindShader<ArmorShaderData>(3038, new ArmorShaderData(pixelShaderRef, "ArmorHades")).UseColor(0.5f, 0.7f, 1.3f).UseSecondaryColor(0.5f, 0.7f, 1.3f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3600, new ArmorShaderData(pixelShaderRef, "ArmorHades")).UseColor(0.7f, 0.4f, 1.5f).UseSecondaryColor(0.7f, 0.4f, 1.5f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3597, new ArmorShaderData(pixelShaderRef, "ArmorHades")).UseColor(1.5f, 0.6f, 0.4f).UseSecondaryColor(1.5f, 0.6f, 0.4f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3598, new ArmorShaderData(pixelShaderRef, "ArmorHades")).UseColor(0.1f, 0.1f, 0.1f).UseSecondaryColor(0.4f, 0.05f, 0.025f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3599, new ArmorShaderData(pixelShaderRef, "ArmorLoki")).UseColor(0.1f, 0.1f, 0.1f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3533, new ArmorShaderData(pixelShaderRef, "ArmorShiftingSands")).UseImage("Images/Misc/noise").UseColor(1.1f, 1f, 0.5f).UseSecondaryColor(0.7f, 0.5f, 0.3f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3535, new ArmorShaderData(pixelShaderRef, "ArmorShiftingPearlsands")).UseImage("Images/Misc/noise").UseColor(1.1f, 0.8f, 0.9f).UseSecondaryColor(0.35f, 0.25f, 0.44f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3526, new ArmorShaderData(pixelShaderRef, "ArmorSolar")).UseColor(1f, 0.0f, 0.0f).UseSecondaryColor(1f, 1f, 0.0f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3527, new ArmorShaderData(pixelShaderRef, "ArmorNebula")).UseImage("Images/Misc/noise").UseColor(1f, 0.0f, 1f).UseSecondaryColor(1f, 1f, 1f).UseSaturation(1f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3528, new ArmorShaderData(pixelShaderRef, "ArmorVortex")).UseImage("Images/Misc/noise").UseColor(0.1f, 0.5f, 0.35f).UseSecondaryColor(1f, 1f, 1f).UseSaturation(1f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3529, new ArmorShaderData(pixelShaderRef, "ArmorStardust")).UseImage("Images/Misc/noise").UseColor(0.4f, 0.6f, 1f).UseSecondaryColor(1f, 1f, 1f).UseSaturation(1f);
      GameShaders.Armor.BindShader<ArmorShaderData>(3530, new ArmorShaderData(pixelShaderRef, "ArmorVoid"));
      DyeInitializer.FixRecipes();
    }

    private static void LoadHairDyes()
    {
      Ref<Effect> pixelShaderRef = Main.PixelShaderRef;
      DyeInitializer.LoadLegacyHairdyes();
      GameShaders.Hair.BindShader<TwilightHairDyeShaderData>(3259, new TwilightHairDyeShaderData(pixelShaderRef, "ArmorTwilight")).UseImage("Images/Misc/noise").UseColor(0.5f, 0.1f, 1f);
    }

    private static void LoadLegacyHairdyes()
    {
      Ref<Effect> pixelShaderRef = Main.PixelShaderRef;
      GameShaders.Hair.BindShader<LegacyHairShaderData>(1977, new LegacyHairShaderData().UseLegacyMethod((LegacyHairShaderData.ColorProcessingMethod) ((Player player, Color newColor, ref bool lighting) =>
      {
        newColor.R = (byte) ((double) player.statLife / (double) player.statLifeMax2 * 235.0 + 20.0);
        newColor.B = (byte) 20;
        newColor.G = (byte) 20;
        return newColor;
      })));
      GameShaders.Hair.BindShader<LegacyHairShaderData>(1978, new LegacyHairShaderData().UseLegacyMethod((LegacyHairShaderData.ColorProcessingMethod) ((Player player, Color newColor, ref bool lighting) =>
      {
        newColor.R = (byte) ((1.0 - (double) player.statMana / (double) player.statManaMax2) * 200.0 + 50.0);
        newColor.B = byte.MaxValue;
        newColor.G = (byte) ((1.0 - (double) player.statMana / (double) player.statManaMax2) * 180.0 + 75.0);
        return newColor;
      })));
      GameShaders.Hair.BindShader<LegacyHairShaderData>(1979, new LegacyHairShaderData().UseLegacyMethod((LegacyHairShaderData.ColorProcessingMethod) ((Player player, Color newColor, ref bool lighting) =>
      {
        float num1 = (float) (Main.worldSurface * 0.45) * 16f;
        float num2 = (float) (Main.worldSurface + Main.rockLayer) * 8f;
        float num3 = ((float) Main.rockLayer + (float) Main.maxTilesY) * 8f;
        float num4 = (float) (Main.maxTilesY - 150) * 16f;
        Vector2 center = player.Center;
        if ((double) center.Y < (double) num1)
        {
          float num5 = center.Y / num1;
          float num6 = 1f - num5;
          newColor.R = (byte) (116.0 * (double) num6 + 28.0 * (double) num5);
          newColor.G = (byte) (160.0 * (double) num6 + 216.0 * (double) num5);
          newColor.B = (byte) (249.0 * (double) num6 + 94.0 * (double) num5);
        }
        else if ((double) center.Y < (double) num2)
        {
          float num5 = num1;
          float num6 = (float) (((double) center.Y - (double) num5) / ((double) num2 - (double) num5));
          float num7 = 1f - num6;
          newColor.R = (byte) (28.0 * (double) num7 + 151.0 * (double) num6);
          newColor.G = (byte) (216.0 * (double) num7 + 107.0 * (double) num6);
          newColor.B = (byte) (94.0 * (double) num7 + 75.0 * (double) num6);
        }
        else if ((double) center.Y < (double) num3)
        {
          float num5 = num2;
          float num6 = (float) (((double) center.Y - (double) num5) / ((double) num3 - (double) num5));
          float num7 = 1f - num6;
          newColor.R = (byte) (151.0 * (double) num7 + 128.0 * (double) num6);
          newColor.G = (byte) (107.0 * (double) num7 + 128.0 * (double) num6);
          newColor.B = (byte) (75.0 * (double) num7 + 128.0 * (double) num6);
        }
        else if ((double) center.Y < (double) num4)
        {
          float num5 = num3;
          float num6 = (float) (((double) center.Y - (double) num5) / ((double) num4 - (double) num5));
          float num7 = 1f - num6;
          newColor.R = (byte) (128.0 * (double) num7 + (double) byte.MaxValue * (double) num6);
          newColor.G = (byte) (128.0 * (double) num7 + 50.0 * (double) num6);
          newColor.B = (byte) (128.0 * (double) num7 + 15.0 * (double) num6);
        }
        else
        {
          newColor.R = byte.MaxValue;
          newColor.G = (byte) 50;
          newColor.B = (byte) 10;
        }
        return newColor;
      })));
      GameShaders.Hair.BindShader<LegacyHairShaderData>(1980, new LegacyHairShaderData().UseLegacyMethod((LegacyHairShaderData.ColorProcessingMethod) ((Player player, Color newColor, ref bool lighting) =>
      {
        int num1 = 0;
        for (int index = 0; index < 54; ++index)
        {
          if (player.inventory[index].type == 71)
            num1 += player.inventory[index].stack;
          if (player.inventory[index].type == 72)
            num1 += player.inventory[index].stack * 100;
          if (player.inventory[index].type == 73)
            num1 += player.inventory[index].stack * 10000;
          if (player.inventory[index].type == 74)
            num1 += player.inventory[index].stack * 1000000;
        }
        float num2 = (float) Item.buyPrice(0, 5, 0, 0);
        float num3 = (float) Item.buyPrice(0, 50, 0, 0);
        float num4 = (float) Item.buyPrice(2, 0, 0, 0);
        Color color1 = new Color(226, 118, 76);
        Color color2 = new Color(174, 194, 196);
        Color color3 = new Color(204, 181, 72);
        Color color4 = new Color(161, 172, 173);
        if ((double) num1 < (double) num2)
        {
          float num5 = (float) num1 / num2;
          float num6 = 1f - num5;
          newColor.R = (byte) ((double) color1.R * (double) num6 + (double) color2.R * (double) num5);
          newColor.G = (byte) ((double) color1.G * (double) num6 + (double) color2.G * (double) num5);
          newColor.B = (byte) ((double) color1.B * (double) num6 + (double) color2.B * (double) num5);
        }
        else if ((double) num1 < (double) num3)
        {
          float num5 = num2;
          float num6 = (float) (((double) num1 - (double) num5) / ((double) num3 - (double) num5));
          float num7 = 1f - num6;
          newColor.R = (byte) ((double) color2.R * (double) num7 + (double) color3.R * (double) num6);
          newColor.G = (byte) ((double) color2.G * (double) num7 + (double) color3.G * (double) num6);
          newColor.B = (byte) ((double) color2.B * (double) num7 + (double) color3.B * (double) num6);
        }
        else if ((double) num1 < (double) num4)
        {
          float num5 = num3;
          float num6 = (float) (((double) num1 - (double) num5) / ((double) num4 - (double) num5));
          float num7 = 1f - num6;
          newColor.R = (byte) ((double) color3.R * (double) num7 + (double) color4.R * (double) num6);
          newColor.G = (byte) ((double) color3.G * (double) num7 + (double) color4.G * (double) num6);
          newColor.B = (byte) ((double) color3.B * (double) num7 + (double) color4.B * (double) num6);
        }
        else
          newColor = color4;
        return newColor;
      })));
      GameShaders.Hair.BindShader<LegacyHairShaderData>(1981, new LegacyHairShaderData().UseLegacyMethod((LegacyHairShaderData.ColorProcessingMethod) ((Player player, Color newColor, ref bool lighting) =>
      {
        Color color1 = new Color(1, 142, (int) byte.MaxValue);
        Color color2 = new Color((int) byte.MaxValue, (int) byte.MaxValue, 0);
        Color color3 = new Color(211, 45, (int) sbyte.MaxValue);
        Color color4 = new Color(67, 44, 118);
        if (Main.dayTime)
        {
          if (Main.time < 27000.0)
          {
            float num1 = (float) (Main.time / 27000.0);
            float num2 = 1f - num1;
            newColor.R = (byte) ((double) color1.R * (double) num2 + (double) color2.R * (double) num1);
            newColor.G = (byte) ((double) color1.G * (double) num2 + (double) color2.G * (double) num1);
            newColor.B = (byte) ((double) color1.B * (double) num2 + (double) color2.B * (double) num1);
          }
          else
          {
            float num1 = 27000f;
            float num2 = (float) ((Main.time - (double) num1) / (54000.0 - (double) num1));
            float num3 = 1f - num2;
            newColor.R = (byte) ((double) color2.R * (double) num3 + (double) color3.R * (double) num2);
            newColor.G = (byte) ((double) color2.G * (double) num3 + (double) color3.G * (double) num2);
            newColor.B = (byte) ((double) color2.B * (double) num3 + (double) color3.B * (double) num2);
          }
        }
        else if (Main.time < 16200.0)
        {
          float num1 = (float) (Main.time / 16200.0);
          float num2 = 1f - num1;
          newColor.R = (byte) ((double) color3.R * (double) num2 + (double) color4.R * (double) num1);
          newColor.G = (byte) ((double) color3.G * (double) num2 + (double) color4.G * (double) num1);
          newColor.B = (byte) ((double) color3.B * (double) num2 + (double) color4.B * (double) num1);
        }
        else
        {
          float num1 = 16200f;
          float num2 = (float) ((Main.time - (double) num1) / (32400.0 - (double) num1));
          float num3 = 1f - num2;
          newColor.R = (byte) ((double) color4.R * (double) num3 + (double) color1.R * (double) num2);
          newColor.G = (byte) ((double) color4.G * (double) num3 + (double) color1.G * (double) num2);
          newColor.B = (byte) ((double) color4.B * (double) num3 + (double) color1.B * (double) num2);
        }
        return newColor;
      })));
      GameShaders.Hair.BindShader<LegacyHairShaderData>(1982, new LegacyHairShaderData().UseLegacyMethod((LegacyHairShaderData.ColorProcessingMethod) ((Player player, Color newColor, ref bool lighting) =>
      {
        if (player.team >= 0 && player.team < Main.teamColor.Length)
          newColor = Main.teamColor[player.team];
        return newColor;
      })));
      GameShaders.Hair.BindShader<LegacyHairShaderData>(1983, new LegacyHairShaderData().UseLegacyMethod((LegacyHairShaderData.ColorProcessingMethod) ((Player player, Color newColor, ref bool lighting) =>
      {
        Color color1 = new Color();
        switch (Main.waterStyle)
        {
          case 2:
            color1 = new Color(124, 118, 242);
            break;
          case 3:
            color1 = new Color(143, 215, 29);
            break;
          case 4:
            color1 = new Color(78, 193, 227);
            break;
          case 5:
            color1 = new Color(189, 231, (int) byte.MaxValue);
            break;
          case 6:
            color1 = new Color(230, 219, 100);
            break;
          case 7:
            color1 = new Color(151, 107, 75);
            break;
          case 8:
            color1 = new Color(128, 128, 128);
            break;
          case 9:
            color1 = new Color(200, 0, 0);
            break;
          case 10:
            color1 = new Color(208, 80, 80);
            break;
          case 12:
            color1 = new Color(230, 219, 100);
            break;
          default:
            color1 = new Color(28, 216, 94);
            break;
        }
        Color color2 = player.hairDyeColor;
        if (color2.A == (byte) 0)
          color2 = color1;
        if ((int) color2.R > (int) color1.R)
          --color2.R;
        if ((int) color2.R < (int) color1.R)
          ++color2.R;
        if ((int) color2.G > (int) color1.G)
          --color2.G;
        if ((int) color2.G < (int) color1.G)
          ++color2.G;
        if ((int) color2.B > (int) color1.B)
          --color2.B;
        if ((int) color2.B < (int) color1.B)
          ++color2.B;
        newColor = color2;
        return newColor;
      })));
      GameShaders.Hair.BindShader<LegacyHairShaderData>(1984, new LegacyHairShaderData().UseLegacyMethod((LegacyHairShaderData.ColorProcessingMethod) ((Player player, Color newColor, ref bool lighting) =>
      {
        newColor = new Color(244, 22, 175);
        return newColor;
      })));
      GameShaders.Hair.BindShader<LegacyHairShaderData>(1985, new LegacyHairShaderData().UseLegacyMethod((LegacyHairShaderData.ColorProcessingMethod) ((Player player, Color newColor, ref bool lighting) =>
      {
        newColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
        return newColor;
      })));
      GameShaders.Hair.BindShader<LegacyHairShaderData>(1986, new LegacyHairShaderData().UseLegacyMethod((LegacyHairShaderData.ColorProcessingMethod) ((Player player, Color newColor, ref bool lighting) =>
      {
        float num1 = Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y);
        float num2 = 10f;
        if ((double) num1 > (double) num2)
          num1 = num2;
        float num3 = num1 / num2;
        float num4 = 1f - num3;
        newColor.R = (byte) (75.0 * (double) num3 + (double) player.hairColor.R * (double) num4);
        newColor.G = (byte) ((double) byte.MaxValue * (double) num3 + (double) player.hairColor.G * (double) num4);
        newColor.B = (byte) (200.0 * (double) num3 + (double) player.hairColor.B * (double) num4);
        return newColor;
      })));
      GameShaders.Hair.BindShader<LegacyHairShaderData>(2863, new LegacyHairShaderData().UseLegacyMethod((LegacyHairShaderData.ColorProcessingMethod) ((Player player, Color newColor, ref bool lighting) =>
      {
        lighting = false;
        Color color = Lighting.GetColor((int) ((double) player.position.X + (double) player.width * 0.5) / 16, (int) (((double) player.position.Y + (double) player.height * 0.25) / 16.0));
        newColor.R = (byte) ((int) color.R + (int) newColor.R >> 1);
        newColor.G = (byte) ((int) color.G + (int) newColor.G >> 1);
        newColor.B = (byte) ((int) color.B + (int) newColor.B >> 1);
        return newColor;
      })));
    }

    private static void LoadMisc()
    {
      Ref<Effect> pixelShaderRef = Main.PixelShaderRef;
      GameShaders.Misc["ForceField"] = new MiscShaderData(pixelShaderRef, "ForceField");
      GameShaders.Misc["WaterProcessor"] = new MiscShaderData(pixelShaderRef, "WaterProcessor");
      GameShaders.Misc["WaterDistortionObject"] = new MiscShaderData(pixelShaderRef, "WaterDistortionObject");
      GameShaders.Misc["WaterDebugDraw"] = new MiscShaderData(Main.ScreenShaderRef, "WaterDebugDraw");
      GameShaders.Misc["HallowBoss"] = new MiscShaderData(pixelShaderRef, "HallowBoss");
      GameShaders.Misc["HallowBoss"].UseImage1("Images/Extra_" + (object) (short) 156);
      GameShaders.Misc["QueenSlime"] = new MiscShaderData(pixelShaderRef, "QueenSlime");
      GameShaders.Misc["QueenSlime"].UseImage1("Images/Extra_" + (object) (short) 180);
      GameShaders.Misc["QueenSlime"].UseImage2("Images/Extra_" + (object) (short) 179);
      int type = 3530;
      bool[] flagArray = new bool[GameShaders.Armor.GetShaderIdFromItemId(type) + 1];
      for (int index = 0; index < flagArray.Length; ++index)
        flagArray[index] = true;
      foreach (int nonColorfulDyeItem in ItemID.Sets.NonColorfulDyeItems)
        flagArray[GameShaders.Armor.GetShaderIdFromItemId(nonColorfulDyeItem)] = false;
      ItemID.Sets.ColorfulDyeValues = flagArray;
      DyeInitializer.LoadMiscVertexShaders();
    }

    private static void LoadMiscVertexShaders()
    {
      Ref<Effect> vertexPixelShaderRef = Main.VertexPixelShaderRef;
      GameShaders.Misc["MagicMissile"] = new MiscShaderData(vertexPixelShaderRef, "MagicMissile").UseProjectionMatrix(true);
      GameShaders.Misc["MagicMissile"].UseImage0("Images/Extra_" + (object) (short) 192);
      GameShaders.Misc["MagicMissile"].UseImage1("Images/Extra_" + (object) (short) 194);
      GameShaders.Misc["MagicMissile"].UseImage2("Images/Extra_" + (object) (short) 193);
      GameShaders.Misc["FlameLash"] = new MiscShaderData(vertexPixelShaderRef, "MagicMissile").UseProjectionMatrix(true);
      GameShaders.Misc["FlameLash"].UseImage0("Images/Extra_" + (object) (short) 191);
      GameShaders.Misc["FlameLash"].UseImage1("Images/Extra_" + (object) (short) 189);
      GameShaders.Misc["FlameLash"].UseImage2("Images/Extra_" + (object) (short) 190);
      GameShaders.Misc["RainbowRod"] = new MiscShaderData(vertexPixelShaderRef, "MagicMissile").UseProjectionMatrix(true);
      GameShaders.Misc["RainbowRod"].UseImage0("Images/Extra_" + (object) (short) 195);
      GameShaders.Misc["RainbowRod"].UseImage1("Images/Extra_" + (object) (short) 197);
      GameShaders.Misc["RainbowRod"].UseImage2("Images/Extra_" + (object) (short) 196);
      GameShaders.Misc["FinalFractal"] = new MiscShaderData(vertexPixelShaderRef, "FinalFractalVertex").UseProjectionMatrix(true);
      GameShaders.Misc["FinalFractal"].UseImage0("Images/Extra_" + (object) (short) 195);
      GameShaders.Misc["FinalFractal"].UseImage1("Images/Extra_" + (object) (short) 197);
      GameShaders.Misc["EmpressBlade"] = new MiscShaderData(vertexPixelShaderRef, "FinalFractalVertex").UseProjectionMatrix(true);
      GameShaders.Misc["EmpressBlade"].UseImage0("Images/Extra_" + (object) (short) 209);
      GameShaders.Misc["EmpressBlade"].UseImage1("Images/Extra_" + (object) (short) 210);
    }

    public static void Load()
    {
      DyeInitializer.LoadArmorDyes();
      DyeInitializer.LoadHairDyes();
      DyeInitializer.LoadMisc();
    }

    private static void FixRecipes()
    {
      for (int index = 0; index < Recipe.maxRecipes; ++index)
      {
        Main.recipe[index].createItem.dye = (byte) GameShaders.Armor.GetShaderIdFromItemId(Main.recipe[index].createItem.type);
        Main.recipe[index].createItem.hairDye = GameShaders.Hair.GetShaderIdFromItemId(Main.recipe[index].createItem.type);
      }
    }
  }
}
