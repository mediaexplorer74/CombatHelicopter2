// Modified by MediaExplorer (2026)
// Type: Helicopter.Model.WorldObjects.CollisionAnalyzer
// Assembly: Helicopter.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E40E7087-8854-4E4C-BE08-EC626C20D03F
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\Helicopter.Model.dll

using Helicopter.Model.WorldObjects.Instances;
using System.Collections.Generic;

#nullable disable
namespace Helicopter.Model.WorldObjects
{
  public class CollisionAnalyzer
  {
    public void Analyze(IList<Instance> instances)
    {
      for (int index1 = 0; index1 < instances.Count; ++index1)
      {
        Instance instance1 = instances[index1];
        if (instance1.State != 1)
        {
          int right = instance1.IncreasedContour.Rectangle.Right;
          for (int index2 = index1 + 1; index2 < instances.Count; ++index2)
          {
            Instance instance2 = instances[index2];
            if (instance2.State != 1)
            {
              if (instance2.IncreasedContour.Rectangle.X < right)
              {
                if (instance1.IntersectsIncreasedContour(instance2))
                {
                  this.Proximity(instance1, instance2);
                  if (instance1.Intersects(instance2))
                    this.Collision(instance1, instance2);
                }
              }
              else
                break;
            }
          }
        }
      }
    }

    private void Collision(Instance obj, Instance subj)
    {
      obj.Reaction.ReactTo(subj);
      subj.Reaction.ReactTo(obj);
    }

    private void Proximity(Instance obj, Instance subj)
    {
      obj.Reaction.ProximityTo(subj);
      subj.Reaction.ProximityTo(obj);
    }
  }
}
