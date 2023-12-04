namespace UI_Layer
{
    public class BackendSystemConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext,
            IRouter route,
            string routeKey,
            RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            // Implement your constraint logic here
            // Return true if the constraint is satisfied, false otherwise
            return true;
        }
    }
}
