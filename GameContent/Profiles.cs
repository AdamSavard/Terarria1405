// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Profiles
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria.Localization;

namespace Terraria.GameContent
{
  public class Profiles
  {
    public class LegacyNPCProfile : ITownNPCProfile
    {
      private string _rootFilePath;
      private int _defaultVariationHeadIndex;
      private Asset<Texture2D> _defaultNoAlt;
      private Asset<Texture2D> _defaultParty;

      public LegacyNPCProfile(string npcFileTitleFilePath, int defaultHeadIndex)
      {
        this._rootFilePath = npcFileTitleFilePath;
        this._defaultVariationHeadIndex = defaultHeadIndex;
        this._defaultNoAlt = (Asset<Texture2D>) Main.Assets.Request<Texture2D>(npcFileTitleFilePath + "_Default", (AssetRequestMode) 0);
        this._defaultParty = (Asset<Texture2D>) Main.Assets.Request<Texture2D>(npcFileTitleFilePath + "_Default_Party", (AssetRequestMode) 0);
      }

      public int RollVariation()
      {
        return 0;
      }

      public string GetNameForVariant(NPC npc)
      {
        return NPC.getNewNPCName(npc.type);
      }

      public Asset<Texture2D> GetTextureNPCShouldUse(NPC npc)
      {
        return npc.IsABestiaryIconDummy || npc.altTexture != 1 ? this._defaultNoAlt : this._defaultParty;
      }

      public int GetHeadTextureIndex(NPC npc)
      {
        return this._defaultVariationHeadIndex;
      }
    }

    public class TransformableNPCProfile : ITownNPCProfile
    {
      private string _rootFilePath;
      private int _defaultVariationHeadIndex;
      private Asset<Texture2D> _defaultNoAlt;
      private Asset<Texture2D> _defaultTransformed;

      public TransformableNPCProfile(string npcFileTitleFilePath, int defaultHeadIndex)
      {
        this._rootFilePath = npcFileTitleFilePath;
        this._defaultVariationHeadIndex = defaultHeadIndex;
        this._defaultNoAlt = (Asset<Texture2D>) Main.Assets.Request<Texture2D>(npcFileTitleFilePath + "_Default", (AssetRequestMode) 0);
        this._defaultTransformed = (Asset<Texture2D>) Main.Assets.Request<Texture2D>(npcFileTitleFilePath + "_Default_Transformed", (AssetRequestMode) 0);
      }

      public int RollVariation()
      {
        return 0;
      }

      public string GetNameForVariant(NPC npc)
      {
        return NPC.getNewNPCName(npc.type);
      }

      public Asset<Texture2D> GetTextureNPCShouldUse(NPC npc)
      {
        return npc.IsABestiaryIconDummy || npc.altTexture != 2 ? this._defaultNoAlt : this._defaultTransformed;
      }

      public int GetHeadTextureIndex(NPC npc)
      {
        return this._defaultVariationHeadIndex;
      }
    }

    public class VariantNPCProfile : ITownNPCProfile
    {
      private Dictionary<string, Asset<Texture2D>> _variantTextures = new Dictionary<string, Asset<Texture2D>>();
      private string _rootFilePath;
      private string _npcBaseName;
      private int[] _variantHeadIDs;
      private string[] _variants;

      public VariantNPCProfile(
        string npcFileTitleFilePath,
        string npcBaseName,
        int[] variantHeadIds,
        params string[] variantTextureNames)
      {
        this._rootFilePath = npcFileTitleFilePath;
        this._npcBaseName = npcBaseName;
        this._variantHeadIDs = variantHeadIds;
        this._variants = variantTextureNames;
        foreach (string variant in this._variants)
        {
          string index = this._rootFilePath + "_" + variant;
          this._variantTextures[index] = (Asset<Texture2D>) Main.Assets.Request<Texture2D>(index, (AssetRequestMode) 0);
        }
      }

      public int RollVariation()
      {
        return Main.rand.Next(this._variants.Length);
      }

      public string GetNameForVariant(NPC npc)
      {
        return Language.RandomFromCategory(this._npcBaseName + "Names_" + this._variants[npc.townNpcVariationIndex], WorldGen.genRand).Value;
      }

      public Asset<Texture2D> GetTextureNPCShouldUse(NPC npc)
      {
        string index = this._rootFilePath + "_" + this._variants[npc.townNpcVariationIndex];
        return npc.IsABestiaryIconDummy || npc.altTexture != 1 || !this._variantTextures.ContainsKey(index + "_Party") ? this._variantTextures[index] : this._variantTextures[index + "_Party"];
      }

      public int GetHeadTextureIndex(NPC npc)
      {
        return this._variantHeadIDs[npc.townNpcVariationIndex];
      }
    }
  }
}
