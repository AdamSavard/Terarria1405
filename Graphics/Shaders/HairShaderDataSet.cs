﻿// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Shaders.HairShaderDataSet
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace Terraria.Graphics.Shaders
{
  public class HairShaderDataSet
  {
    protected List<HairShaderData> _shaderData = new List<HairShaderData>();
    protected Dictionary<int, short> _shaderLookupDictionary = new Dictionary<int, short>();
    protected byte _shaderDataCount;

    public T BindShader<T>(int itemId, T shaderData) where T : HairShaderData
    {
      if (this._shaderDataCount == byte.MaxValue)
        throw new Exception("Too many shaders bound.");
      this._shaderLookupDictionary[itemId] = (short) ++this._shaderDataCount;
      this._shaderData.Add((HairShaderData) shaderData);
      return shaderData;
    }

    public void Apply(short shaderId, Player player, DrawData? drawData = null)
    {
      if (shaderId != (short) 0 && (int) shaderId <= (int) this._shaderDataCount)
        this._shaderData[(int) shaderId - 1].Apply(player, drawData);
      else
        Main.pixelShader.CurrentTechnique.Passes[0].Apply();
    }

    public Color GetColor(short shaderId, Player player, Color lightColor)
    {
      return shaderId != (short) 0 && (int) shaderId <= (int) this._shaderDataCount ? this._shaderData[(int) shaderId - 1].GetColor(player, lightColor) : new Color(lightColor.ToVector4() * player.hairColor.ToVector4());
    }

    public HairShaderData GetShaderFromItemId(int type)
    {
      return this._shaderLookupDictionary.ContainsKey(type) ? this._shaderData[(int) this._shaderLookupDictionary[type] - 1] : (HairShaderData) null;
    }

    public short GetShaderIdFromItemId(int type)
    {
      return this._shaderLookupDictionary.ContainsKey(type) ? this._shaderLookupDictionary[type] : (short) -1;
    }
  }
}
