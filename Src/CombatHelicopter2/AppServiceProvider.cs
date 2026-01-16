// Modified by MediaExplorer (2026)
// Type: HelicopterSL.AppServiceProvider
// Assembly: HelicopterSL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 993C8FB4-A202-4C38-BABD-3FA3D695AAE3
// Assembly location: C:\Users\Admin\Desktop\RE\Combat_Helicopter_2_v1.2.0.0\HelicopterSL.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace HelicopterSL
{
  public class AppServiceProvider : IServiceProvider
  {
    private readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

    public void AddService(Type serviceType, object service)
    {
      if ((object) serviceType == null)
        throw new ArgumentNullException(nameof (serviceType));
      if (service == null)
        throw new ArgumentNullException(nameof (service));
      this.services.Add(serviceType, service);
    }

    public object GetService(Type serviceType)
    {
      return (object) serviceType != null ? this.services[serviceType] : throw new ArgumentNullException(nameof (serviceType));
    }

    public void RemoveService(Type serviceType)
    {
      if ((object) serviceType == null)
        throw new ArgumentNullException(nameof (serviceType));
      this.services.Remove(serviceType);
    }
  }
}
