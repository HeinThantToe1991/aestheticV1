using UI_Layer.Resources;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace UI_Layer.Services
{
    public class LanguageService
    {
        private readonly IStringLocalizer<Resources.Resource> _localizer;

        public LanguageService(IStringLocalizer<Resources.Resource> localizer)
        {
            _localizer = localizer;
        }

        public LocalizedString Getkey(string key)
        {
            return _localizer[key];
        }
    
    }
}
