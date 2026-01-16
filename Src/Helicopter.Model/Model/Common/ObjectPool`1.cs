// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.Common.ObjectPool`1
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Helicopter.Model.Common
{
  public class ObjectPool<T> where T : class, IReusable
  {
    private readonly ICreation<T> _creator;
    private readonly IList<WeakReference> _pool;

    public int Size
    {
      get
      {
        lock (this._pool)
          return this._pool.Count;
      }
    }

    public int InstanceCount { get; private set; }

    public ObjectPool(ICreation<T> creator)
    {
      this._creator = creator;
      this.InstanceCount = 0;
      this._pool = (IList<WeakReference>) new List<WeakReference>();
    }

    private T CreateObject()
    {
      T obj = this._creator.Create();
      ++this.InstanceCount;
      return obj;
    }

    public T GetObject()
    {
      lock (this._pool)
        return this.RemoveObject() ?? this.CreateObject();
    }

    public void Release(T obj)
    {
      if ((object) obj == null)
        throw new NullReferenceException();
      lock (this._pool)
      {
        obj.ResetState();
        this._pool.Add(new WeakReference((object) obj));
      }
    }

    private T RemoveObject()
    {
      while (this._pool.Count > 0)
      {
        WeakReference weakReference = this._pool[this._pool.Count - 1];
        this._pool.RemoveAt(this._pool.Count - 1);
        T target = (T) weakReference.Target;
        if ((object) target != null)
          return target;
        --this.InstanceCount;
      }
      return default (T);
    }
  }
}
