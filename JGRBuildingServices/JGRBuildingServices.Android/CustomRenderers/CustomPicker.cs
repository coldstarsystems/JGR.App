using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using JGRBuildingServices.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(JGRBuildingServices.Extras.CustomPicker), typeof(JGRBuildingServices.Droid.CustomRenderers.CustomPicker))]

namespace JGRBuildingServices.Droid.CustomRenderers
{
    public class CustomPicker : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Gravity = GravityFlags.CenterHorizontal | GravityFlags.Bottom;
            }
        }
    }
}