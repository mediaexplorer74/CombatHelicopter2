// Decompiled with JetBrains decompiler
// Type: Helicopter.Model.WorldObjects.InstanceComparer
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.WorldObjects.Instances;
using System.Collections.Generic;

#nullable disable
namespace Helicopter.Model.WorldObjects
{
  internal class InstanceComparer : IComparer<Instance>
  {
    public int Compare(Instance x, Instance y) => x.Position.X.CompareTo(y.Position.X);
  }
}
