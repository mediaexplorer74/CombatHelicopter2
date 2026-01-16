// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.SpriteObjects.Sprites.SpriteCircle
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

#nullable disable
namespace Helicopter.Model.SpriteObjects.Sprites
{
  public class SpriteCircle
  {
    private BasicEffect effect;
    private Rectangle _rectangle;
    private GraphicsDevice _device;
    private Vector3 normal = new Vector3(0.0f, 0.0f, -1f);

    public SpriteCircle(GraphicsDevice device, Sprite sprite, Rectangle rectangle)
    {
      this._rectangle = rectangle;
      this._rectangle.X = 800 - this._rectangle.X - this._rectangle.Width;
      this._device = device;
      this.effect = new BasicEffect(device)
      {
        Projection = Matrix.CreateOrthographic(800f, 480f, -1f, 1f),
        View = Matrix.CreateLookAt(new Vector3((float) device.Viewport.Bounds.Center.X, (float) device.Viewport.Bounds.Center.Y, 1f), new Vector3((float) device.Viewport.Bounds.Center.X, (float) device.Viewport.Bounds.Center.Y, 0.0f), Vector3.Down),
        TextureEnabled = true,
        Texture = sprite.Texture
      };
    }

    public void DrawCircle(float angle) => this.RenderToDevice(this.ConstructCube(angle));

    public void RenderToDevice(VertexPositionNormalTexture[] vertices)
    {
      SamplerState samplerState = this._device.SamplerStates[0];
      RasterizerState rasterizerState = this._device.RasterizerState;
      this._device.SamplerStates[0] = SamplerState.LinearClamp;
      this._device.RasterizerState = RasterizerState.CullNone;
      if (vertices == null)
        return;
      foreach (EffectPass pass in this.effect.CurrentTechnique.Passes)
      {
        pass.Apply();
        this._device.DrawUserPrimitives<VertexPositionNormalTexture>(PrimitiveType.TriangleStrip, vertices, 0, vertices.Length - 2);
      }
      this._device.SamplerStates[0] = samplerState;
      this._device.RasterizerState = rasterizerState;
    }

    private VertexPositionNormalTexture[] ConstructCube(float percent)
    {
      float a = percent * 6.28318548f;
      if ((double) percent < 0.125)
      {
        VertexPositionNormalTexture[] positionNormalTextureArray = new VertexPositionNormalTexture[3];
        Vector2 vector2_1 = new Vector2((float) (0.5 - 0.5 * Math.Tan((double) a)), 0.0f);
        Vector3 position1 = this.Vector3Scaled(vector2_1);
        positionNormalTextureArray[0] = new VertexPositionNormalTexture(position1, this.normal, vector2_1);
        Vector2 vector2_2 = new Vector2(0.5f, 0.0f);
        Vector3 position2 = this.Vector3Scaled(vector2_2);
        positionNormalTextureArray[1] = new VertexPositionNormalTexture(position2, this.normal, vector2_2);
        Vector2 vector2_3 = new Vector2(0.5f, 0.5f);
        Vector3 position3 = this.Vector3Scaled(vector2_3);
        positionNormalTextureArray[2] = new VertexPositionNormalTexture(position3, this.normal, vector2_3);
        return positionNormalTextureArray;
      }
      if ((double) percent < 0.25)
      {
        VertexPositionNormalTexture[] positionNormalTextureArray = new VertexPositionNormalTexture[4];
        Vector2 vector2_4 = new Vector2(0.0f, 0.0f);
        Vector3 position4 = this.Vector3Scaled(vector2_4);
        positionNormalTextureArray[1] = new VertexPositionNormalTexture(position4, this.normal, vector2_4);
        Vector2 vector2_5 = new Vector2(0.5f, 0.0f);
        Vector3 position5 = this.Vector3Scaled(vector2_5);
        positionNormalTextureArray[0] = new VertexPositionNormalTexture(position5, this.normal, vector2_5);
        Vector2 vector2_6 = new Vector2(0.5f, 0.5f);
        Vector3 position6 = this.Vector3Scaled(vector2_6);
        positionNormalTextureArray[2] = new VertexPositionNormalTexture(position6, this.normal, vector2_6);
        Vector2 vector2_7 = new Vector2(0.0f, (float) (Math.Sqrt(0.5) * Math.Sin((double) a - 0.78539818525314331)));
        Vector3 position7 = this.Vector3Scaled(vector2_7);
        positionNormalTextureArray[3] = new VertexPositionNormalTexture(position7, this.normal, vector2_7);
        return positionNormalTextureArray;
      }
      if ((double) percent < 0.375)
      {
        VertexPositionNormalTexture[] positionNormalTextureArray = new VertexPositionNormalTexture[4];
        Vector2 vector2_8 = new Vector2(0.5f, 0.0f);
        Vector3 position8 = this.Vector3Scaled(vector2_8);
        positionNormalTextureArray[0] = new VertexPositionNormalTexture(position8, this.normal, vector2_8);
        Vector2 vector2_9 = new Vector2(0.5f, 0.5f);
        Vector3 position9 = this.Vector3Scaled(vector2_9);
        positionNormalTextureArray[1] = new VertexPositionNormalTexture(position9, this.normal, vector2_9);
        Vector2 vector2_10 = new Vector2(0.0f, 0.0f);
        Vector3 position10 = this.Vector3Scaled(vector2_10);
        positionNormalTextureArray[2] = new VertexPositionNormalTexture(position10, this.normal, vector2_10);
        Vector2 vector2_11 = new Vector2(0.0f, (float) (0.5 + 0.5 * Math.Tan((double) a - 1.5707963705062866)));
        Vector3 position11 = this.Vector3Scaled(vector2_11);
        positionNormalTextureArray[3] = new VertexPositionNormalTexture(position11, this.normal, vector2_11);
        return positionNormalTextureArray;
      }
      if ((double) percent < 0.5)
      {
        VertexPositionNormalTexture[] positionNormalTextureArray = new VertexPositionNormalTexture[5];
        Vector2 vector2_12 = new Vector2(0.5f, 0.0f);
        Vector3 position12 = this.Vector3Scaled(vector2_12);
        positionNormalTextureArray[1] = new VertexPositionNormalTexture(position12, this.normal, vector2_12);
        Vector2 vector2_13 = new Vector2(0.5f, 0.5f);
        Vector3 position13 = this.Vector3Scaled(vector2_13);
        positionNormalTextureArray[3] = new VertexPositionNormalTexture(position13, this.normal, vector2_13);
        Vector2 vector2_14 = new Vector2(0.0f, 0.0f);
        Vector3 position14 = this.Vector3Scaled(vector2_14);
        positionNormalTextureArray[0] = new VertexPositionNormalTexture(position14, this.normal, vector2_14);
        Vector2 vector2_15 = new Vector2(0.0f, 1f);
        Vector3 position15 = this.Vector3Scaled(vector2_15);
        positionNormalTextureArray[2] = new VertexPositionNormalTexture(position15, this.normal, vector2_15);
        Vector2 vector2_16 = new Vector2((float) (Math.Sqrt(0.5) * Math.Sin((double) a - 0.78539818525314331 - 1.5707963705062866)), 1f);
        Vector3 position16 = this.Vector3Scaled(vector2_16);
        positionNormalTextureArray[4] = new VertexPositionNormalTexture(position16, this.normal, vector2_16);
        return positionNormalTextureArray;
      }
      if ((double) percent < 0.625)
      {
        VertexPositionNormalTexture[] positionNormalTextureArray = new VertexPositionNormalTexture[5];
        Vector2 vector2_17 = new Vector2(0.5f, 0.0f);
        Vector3 position17 = this.Vector3Scaled(vector2_17);
        positionNormalTextureArray[1] = new VertexPositionNormalTexture(position17, this.normal, vector2_17);
        Vector2 vector2_18 = new Vector2(0.5f, 0.5f);
        Vector3 position18 = this.Vector3Scaled(vector2_18);
        positionNormalTextureArray[3] = new VertexPositionNormalTexture(position18, this.normal, vector2_18);
        Vector2 vector2_19 = new Vector2(0.0f, 0.0f);
        Vector3 position19 = this.Vector3Scaled(vector2_19);
        positionNormalTextureArray[0] = new VertexPositionNormalTexture(position19, this.normal, vector2_19);
        Vector2 vector2_20 = new Vector2(0.0f, 1f);
        Vector3 position20 = this.Vector3Scaled(vector2_20);
        positionNormalTextureArray[2] = new VertexPositionNormalTexture(position20, this.normal, vector2_20);
        Vector2 vector2_21 = new Vector2((float) (0.5 + 0.5 * Math.Tan((double) a - 3.1415927410125732)), 1f);
        Vector3 position21 = this.Vector3Scaled(vector2_21);
        positionNormalTextureArray[4] = new VertexPositionNormalTexture(position21, this.normal, vector2_21);
        return positionNormalTextureArray;
      }
      if ((double) percent < 0.75)
      {
        VertexPositionNormalTexture[] positionNormalTextureArray = new VertexPositionNormalTexture[6];
        Vector2 vector2_22 = new Vector2(0.5f, 0.0f);
        Vector3 position22 = this.Vector3Scaled(vector2_22);
        positionNormalTextureArray[1] = new VertexPositionNormalTexture(position22, this.normal, vector2_22);
        Vector2 vector2_23 = new Vector2(0.5f, 0.5f);
        Vector3 position23 = this.Vector3Scaled(vector2_23);
        positionNormalTextureArray[3] = new VertexPositionNormalTexture(position23, this.normal, vector2_23);
        Vector2 vector2_24 = new Vector2(0.0f, 0.0f);
        Vector3 position24 = this.Vector3Scaled(vector2_24);
        positionNormalTextureArray[0] = new VertexPositionNormalTexture(position24, this.normal, vector2_24);
        Vector2 vector2_25 = new Vector2(0.0f, 1f);
        Vector3 position25 = this.Vector3Scaled(vector2_25);
        positionNormalTextureArray[2] = new VertexPositionNormalTexture(position25, this.normal, vector2_25);
        Vector2 vector2_26 = new Vector2(1f, 1f);
        Vector3 position26 = this.Vector3Scaled(vector2_26);
        positionNormalTextureArray[4] = new VertexPositionNormalTexture(position26, this.normal, vector2_26);
        Vector2 vector2_27 = new Vector2(1f, (float) (1.0 - Math.Sqrt(0.5) * Math.Sin((double) a - 0.78539818525314331 - 3.1415927410125732)));
        Vector3 position27 = this.Vector3Scaled(vector2_27);
        positionNormalTextureArray[5] = new VertexPositionNormalTexture(position27, this.normal, vector2_27);
        return positionNormalTextureArray;
      }
      if ((double) percent < 0.875)
      {
        VertexPositionNormalTexture[] positionNormalTextureArray = new VertexPositionNormalTexture[6];
        Vector2 vector2_28 = new Vector2(0.5f, 0.0f);
        Vector3 position28 = this.Vector3Scaled(vector2_28);
        positionNormalTextureArray[1] = new VertexPositionNormalTexture(position28, this.normal, vector2_28);
        Vector2 vector2_29 = new Vector2(0.5f, 0.5f);
        Vector3 position29 = this.Vector3Scaled(vector2_29);
        positionNormalTextureArray[3] = new VertexPositionNormalTexture(position29, this.normal, vector2_29);
        Vector2 vector2_30 = new Vector2(0.0f, 0.0f);
        Vector3 position30 = this.Vector3Scaled(vector2_30);
        positionNormalTextureArray[0] = new VertexPositionNormalTexture(position30, this.normal, vector2_30);
        Vector2 vector2_31 = new Vector2(0.0f, 1f);
        Vector3 position31 = this.Vector3Scaled(vector2_31);
        positionNormalTextureArray[2] = new VertexPositionNormalTexture(position31, this.normal, vector2_31);
        Vector2 vector2_32 = new Vector2(1f, 1f);
        Vector3 position32 = this.Vector3Scaled(vector2_32);
        positionNormalTextureArray[4] = new VertexPositionNormalTexture(position32, this.normal, vector2_32);
        Vector2 vector2_33 = new Vector2(1f, (float) (0.5 - 0.5 * Math.Tan((double) a - 3.1415927410125732 - 1.5707963705062866)));
        Vector3 position33 = this.Vector3Scaled(vector2_33);
        positionNormalTextureArray[5] = new VertexPositionNormalTexture(position33, this.normal, vector2_33);
        return positionNormalTextureArray;
      }
      if ((double) percent >= 1.0)
        return (VertexPositionNormalTexture[]) null;
      VertexPositionNormalTexture[] positionNormalTextureArray1 = new VertexPositionNormalTexture[7];
      Vector2 vector2_34 = new Vector2(0.5f, 0.0f);
      Vector3 position34 = this.Vector3Scaled(vector2_34);
      positionNormalTextureArray1[1] = new VertexPositionNormalTexture(position34, this.normal, vector2_34);
      Vector2 vector2_35 = new Vector2(0.5f, 0.5f);
      Vector3 position35 = this.Vector3Scaled(vector2_35);
      positionNormalTextureArray1[3] = new VertexPositionNormalTexture(position35, this.normal, vector2_35);
      Vector2 vector2_36 = new Vector2(0.0f, 0.0f);
      Vector3 position36 = this.Vector3Scaled(vector2_36);
      positionNormalTextureArray1[0] = new VertexPositionNormalTexture(position36, this.normal, vector2_36);
      Vector2 vector2_37 = new Vector2(0.0f, 1f);
      Vector3 position37 = this.Vector3Scaled(vector2_37);
      positionNormalTextureArray1[2] = new VertexPositionNormalTexture(position37, this.normal, vector2_37);
      Vector2 vector2_38 = new Vector2(1f, 1f);
      Vector3 position38 = this.Vector3Scaled(vector2_38);
      positionNormalTextureArray1[4] = new VertexPositionNormalTexture(position38, this.normal, vector2_38);
      Vector2 vector2_39 = new Vector2((float) (0.5 + 0.5 * Math.Tan(0.78539818525314331 - ((double) a - 3.1415927410125732 - 1.5707963705062866 - 0.78539818525314331))), 0.0f);
      Vector3 position39 = this.Vector3Scaled(vector2_39);
      positionNormalTextureArray1[5] = new VertexPositionNormalTexture(position39, this.normal, vector2_39);
      Vector2 vector2_40 = new Vector2(1f, 0.0f);
      Vector3 position40 = this.Vector3Scaled(vector2_40);
      positionNormalTextureArray1[6] = new VertexPositionNormalTexture(position40, this.normal, vector2_40);
      return positionNormalTextureArray1;
    }

    private List<VertexPositionNormalTexture> FirstQuater(int width, int height)
    {
      List<VertexPositionNormalTexture> positionNormalTextureList = new List<VertexPositionNormalTexture>();
      Vector2 textureCoordinate = new Vector2(0.0f, 0.0f);
      Vector3 position1 = new Vector3(0.0f, 0.0f, 0.0f);
      positionNormalTextureList.Add(new VertexPositionNormalTexture(position1, this.normal, textureCoordinate));
      Vector2 vector2_1 = new Vector2(0.5f, 0.0f);
      Vector3 position2 = this.Vector3Scaled(vector2_1);
      positionNormalTextureList.Add(new VertexPositionNormalTexture(position2, this.normal, vector2_1));
      Vector2 vector2_2 = new Vector2(0.5f, 0.5f);
      Vector3 position3 = this.Vector3Scaled(vector2_2);
      positionNormalTextureList.Add(new VertexPositionNormalTexture(position3, this.normal, vector2_2));
      return positionNormalTextureList;
    }

    private Vector3 Vector3Scaled(Vector2 vector2)
    {
      return new Vector3((float) this._rectangle.X + vector2.X * (float) this._rectangle.Width, (float) this._rectangle.Y + vector2.Y * (float) this._rectangle.Height, 0.5f);
    }
  }
}
