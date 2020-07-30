// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Shaders.MiscShaderData
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;

namespace Terraria.Graphics.Shaders
{
  public class MiscShaderData : ShaderData
  {
    private Vector3 _uColor = Vector3.One;
    private Vector3 _uSecondaryColor = Vector3.One;
    private float _uSaturation = 1f;
    private float _uOpacity = 1f;
    private Vector4 _shaderSpecificData = Vector4.Zero;
    private Asset<Texture2D> _uImage0;
    private Asset<Texture2D> _uImage1;
    private Asset<Texture2D> _uImage2;
    private bool _useProjectionMatrix;

    public MiscShaderData(Ref<Effect> shader, string passName)
      : base(shader, passName)
    {
    }

    public virtual void Apply(DrawData? drawData = null)
    {
      this.Shader.Parameters["uColor"].SetValue(this._uColor);
      this.Shader.Parameters["uSaturation"].SetValue(this._uSaturation);
      this.Shader.Parameters["uSecondaryColor"].SetValue(this._uSecondaryColor);
      this.Shader.Parameters["uTime"].SetValue(Main.GlobalTimeWrappedHourly);
      this.Shader.Parameters["uOpacity"].SetValue(this._uOpacity);
      this.Shader.Parameters["uShaderSpecificData"].SetValue(this._shaderSpecificData);
      if (drawData.HasValue)
      {
        DrawData drawData1 = drawData.Value;
        Vector4 vector4 = Vector4.Zero;
        if (drawData.Value.sourceRect.HasValue)
          vector4 = new Vector4((float) drawData1.sourceRect.Value.X, (float) drawData1.sourceRect.Value.Y, (float) drawData1.sourceRect.Value.Width, (float) drawData1.sourceRect.Value.Height);
        this.Shader.Parameters["uSourceRect"].SetValue(vector4);
        this.Shader.Parameters["uWorldPosition"].SetValue(Main.screenPosition + drawData1.position);
        this.Shader.Parameters["uImageSize0"].SetValue(new Vector2((float) drawData1.texture.Width, (float) drawData1.texture.Height));
      }
      else
        this.Shader.Parameters["uSourceRect"].SetValue(new Vector4(0.0f, 0.0f, 4f, 4f));
      if (this._uImage0 != null)
      {
        Main.graphics.GraphicsDevice.Textures[0] = (Texture) this._uImage0.get_Value();
        Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
        this.Shader.Parameters["uImageSize0"].SetValue(new Vector2((float) this._uImage0.Width(), (float) this._uImage0.Height()));
      }
      if (this._uImage1 != null)
      {
        Main.graphics.GraphicsDevice.Textures[1] = (Texture) this._uImage1.get_Value();
        Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.LinearWrap;
        this.Shader.Parameters["uImageSize1"].SetValue(new Vector2((float) this._uImage1.Width(), (float) this._uImage1.Height()));
      }
      if (this._uImage2 != null)
      {
        Main.graphics.GraphicsDevice.Textures[2] = (Texture) this._uImage2.get_Value();
        Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.LinearWrap;
        this.Shader.Parameters["uImageSize2"].SetValue(new Vector2((float) this._uImage2.Width(), (float) this._uImage2.Height()));
      }
      if (this._useProjectionMatrix)
        this.Shader.Parameters["uMatrixTransform0"].SetValue(Main.GameViewMatrix.NormalizedTransformationmatrix);
      this.Apply();
    }

    public MiscShaderData UseColor(float r, float g, float b)
    {
      return this.UseColor(new Vector3(r, g, b));
    }

    public MiscShaderData UseColor(Color color)
    {
      return this.UseColor(color.ToVector3());
    }

    public MiscShaderData UseColor(Vector3 color)
    {
      this._uColor = color;
      return this;
    }

    public MiscShaderData UseImage0(string path)
    {
      this._uImage0 = (Asset<Texture2D>) Main.Assets.Request<Texture2D>(path, (AssetRequestMode) 1);
      return this;
    }

    public MiscShaderData UseImage1(string path)
    {
      this._uImage1 = (Asset<Texture2D>) Main.Assets.Request<Texture2D>(path, (AssetRequestMode) 1);
      return this;
    }

    public MiscShaderData UseImage2(string path)
    {
      this._uImage2 = (Asset<Texture2D>) Main.Assets.Request<Texture2D>(path, (AssetRequestMode) 1);
      return this;
    }

    public MiscShaderData UseOpacity(float alpha)
    {
      this._uOpacity = alpha;
      return this;
    }

    public MiscShaderData UseSecondaryColor(float r, float g, float b)
    {
      return this.UseSecondaryColor(new Vector3(r, g, b));
    }

    public MiscShaderData UseSecondaryColor(Color color)
    {
      return this.UseSecondaryColor(color.ToVector3());
    }

    public MiscShaderData UseSecondaryColor(Vector3 color)
    {
      this._uSecondaryColor = color;
      return this;
    }

    public MiscShaderData UseProjectionMatrix(bool doUse)
    {
      this._useProjectionMatrix = doUse;
      return this;
    }

    public MiscShaderData UseSaturation(float saturation)
    {
      this._uSaturation = saturation;
      return this;
    }

    public virtual MiscShaderData GetSecondaryShader(Entity entity)
    {
      return this;
    }

    public MiscShaderData UseShaderSpecificData(Vector4 specificData)
    {
      this._shaderSpecificData = specificData;
      return this;
    }
  }
}
