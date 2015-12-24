using Bebach.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;


namespace Bebach.App_Start
{
    public static class CustomModelBinderConfig
    {
        public static void RegisterCustomModelBinders()
        {
           // ModelBinders.Binders.Add(typeof(DateTime), new Bebach.Extensions.CustomDateBinder.CustomDateBinders());
          //  ModelBinders.Binders.Add(typeof(DateTime?), new Bebach.Extensions.CustomDateBinder.NullableCustomDateBinder());
        }
    }
}