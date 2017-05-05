using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pho.Service;

namespace PhotographerSite.Helpers
{
    public static class HtmlHelpers
    {
        private static DescriptionService DescriptionService
        {
            get
            {
                return new DescriptionService();
            }
        }


        public static string PhoSiteDescriptions(this HtmlHelper helper,string descName)
        {
            return DescriptionService.GetByName(descName).Value.ToString();
        }
    }
}