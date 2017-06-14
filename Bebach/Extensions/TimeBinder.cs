using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bebach.Extensions
{
    public class TimeBinder: IModelBinder
    {
        object IModelBinder.BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            //Ensure there's incoming data
            //*************************
            //*** Validate "HOURS"  ***
            //*************************
            var hoursKey = bindingContext.ModelName; //key = EODTime
            var hoursValueProviderResult = bindingContext.ValueProvider.GetValue(hoursKey + ".hours"); //valueProviderResult = {15} (for selection of 15:10); RawValue has array of 15           

            if ((hoursValueProviderResult == null) ||
               string.IsNullOrEmpty(hoursValueProviderResult.AttemptedValue))
            {
                return null;
            }

            //Preserve it in case we need to redisplay the form
            bindingContext.ModelState.SetModelValue(hoursKey, hoursValueProviderResult);

            //Parse
            var hours = ((string[])hoursValueProviderResult.RawValue)[0];

            //**************************
            //*** Validate "MINUTES" *** 
            //**************************          
            var minuteKey = bindingContext.ModelName; //key = EODTime
            var minutesValueProviderResult = bindingContext.ValueProvider.GetValue(minuteKey + ".minutes");

            //valueProviderResult = {10} (for selection of 15:10); RawValue has array of 10           

            if ((minutesValueProviderResult == null) ||
               string.IsNullOrEmpty(minutesValueProviderResult.AttemptedValue))
            {
                return null;
            }

            //Preserve it in case we need to redisplay the form
            bindingContext.ModelState.SetModelValue(minuteKey, minutesValueProviderResult);

            //Parse           
            var minutes = ((string[])minutesValueProviderResult.RawValue)[0];


            //A TimeSpan represents the time elapsed sice midnight
            //var time = new TimeSpan(Convert.ToInt32(hours), Convert.ToInt32(minutes), 0);
            
             var time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(hours), Convert.ToInt32(minutes),0);
            return time;

        }
    }
}