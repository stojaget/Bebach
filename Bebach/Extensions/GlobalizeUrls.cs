﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace Bebach.Extensions
{
    public static class GlobalizeUrls
    {
        public static string Globalize { get { return "~/Scripts/globalize.js"; } }

        /// <summary>
        /// URL for the specific Globalize culture
        /// </summary>
        public static string GlobalizeCulture
        {
            get
            {
                //Determine culture - GUI culture for preference, user selected culture as fallback
                var currentCulture = CultureInfo.CurrentCulture;
                var filePattern = "~/scripts/globalize/globalize.culture.{0}.js";
                var regionalisedFileToUse = string.Format(filePattern, "hr-HR"); //Default localisation to use

                //Try to pick a more appropriate regionalisation
                if (File.Exists(HostingEnvironment.MapPath(string.Format(filePattern, currentCulture.Name)))) //First try for a globalize.culture.en-GB.js style file
                    regionalisedFileToUse = string.Format(filePattern, currentCulture.Name);
                else if (File.Exists(HostingEnvironment.MapPath(string.Format(filePattern, currentCulture.TwoLetterISOLanguageName)))) //That failed; now try for a globalize.culture.en.js style file
                    regionalisedFileToUse = string.Format(filePattern, currentCulture.TwoLetterISOLanguageName);

                return regionalisedFileToUse;
            }
        }
    }
}