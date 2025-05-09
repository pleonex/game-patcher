namespace PleOps.Moxmi.ModResources;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ModInstallationOptions(
    Dictionary<string, object> resourceParameters,
    Dictionary<string, string> productFeatureParameters)
{
    public T GetProductFeature<T>(string key)
    {
        // TODO: Dictionary -> T it can be custom implement via interface
        throw new NotImplementedException();
    }

    public T GetResourceParameter<T>(string key)
    {
        throw new NotImplementedException();
    }
}
