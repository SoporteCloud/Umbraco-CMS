﻿using System.Linq;
using Umbraco.Core.Models;

namespace Umbraco.Core.Services
{
    public static class LocalizationServiceExtensions
    {
        /// <summary>
        /// Returns the configured default variant language 
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static ILanguage GetDefaultVariantLanguage(this ILocalizationService service)
        {
            var langs = service.GetAllLanguages().OrderBy(x => x.Id).ToList();

            if (langs.Count == 0) return null;

            //if there's only one language, by default it is the default
            if (langs.Count == 1)
            {
                langs[0].IsDefaultVariantLanguage = true;
                langs[0].Mandatory = true;
                return langs[0];
            }

            var foundDefault = langs.FirstOrDefault(x => x.IsDefaultVariantLanguage);
            if (foundDefault != null) return foundDefault;

            //if no language has the default flag, then the defaul language is the one with the lowest id
            langs[0].IsDefaultVariantLanguage = true;
            langs[0].Mandatory = true;
            return langs[0];
        }
    }
}
