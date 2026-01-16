// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.WorldObjects.IdFactory
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

#nullable disable
namespace Helicopter.Model.WorldObjects
{
  public class IdFactory
  {
    private static readonly object _mutex = new object();
    private static IdFactory _instance;
    private int _prevId;

    public static IdFactory Instance
    {
      get
      {
        lock (IdFactory._mutex)
          return IdFactory._instance ?? (IdFactory._instance = new IdFactory());
      }
    }

    public int GetId()
    {
      lock (IdFactory._mutex)
      {
        ++this._prevId;
        return this._prevId;
      }
    }

    public void SetPrevId(int value) => this._prevId = value;
  }
}
