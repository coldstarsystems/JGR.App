using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace JGRBuildingServices.Extras
{
    public class NullableTimePicker : TimePicker
    {
        private string _format = null;
        [Obsolete]
        public static readonly BindableProperty NullableDateProperty =
        BindableProperty.Create<NullableTimePicker, TimeSpan?>(p => p.NullableTime, null);

        public TimeSpan? NullableTime
        {
            get { return (TimeSpan?)GetValue(NullableDateProperty); }
            set { SetValue(NullableDateProperty, value); UpdateTime(); }
        }

        private void UpdateTime()
        {
            if (NullableTime.HasValue)
            {
                if (null != _format)
                {
                    Format = _format; Time = NullableTime.Value;
                } 
            }
            else
            {
                _format = Format;
                Format = "HH:mm";
            }
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            UpdateTime();
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == "Time")
            {
                NullableTime = Time;
            } 
        }
    }
}
