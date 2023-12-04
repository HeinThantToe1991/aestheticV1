using Microsoft.Extensions.Localization;
using System.ComponentModel;

namespace UI_Layer.Resources
{
    public class Resource
    {
    }
    public class LocalizedDisplayName : DisplayNameAttribute
    {
        private static IStringLocalizer<Resource> _localizer;
        public LocalizedDisplayName(string resourceId)
            : base(GetMessageFromResource(resourceId))
        {
        }

        private static string GetMessageFromResource(string resourceId)
        {
            return _localizer[resourceId];
            // TODO: Return the string from the resource file
        }
    }
}
