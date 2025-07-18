using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.Extensions.Http;

/// <summary>
/// Globally applies a route prefix to all controllers.
/// </summary>
public class GlobalRoutePrefixConvention : IApplicationModelConvention
{
    private readonly AttributeRouteModel _routePrefix;

    public GlobalRoutePrefixConvention(string routePrefix)
    {
        _routePrefix = new AttributeRouteModel(new RouteAttribute(routePrefix));
    }
    public void Apply(ApplicationModel application)
    {
        foreach (var controller in application.Controllers)
        {
            foreach (var selector in controller.Selectors)
            {
                if (selector.AttributeRouteModel != null)
                {
                    // Combine the existing route with the global prefix
                    selector.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(_routePrefix, selector.AttributeRouteModel);
                }
                else
                {
                    // If no existing route, just set the global prefix
                    selector.AttributeRouteModel = _routePrefix;
                }
            }
        }
    }

}