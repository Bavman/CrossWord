using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CrossWord.ViewModel
{
    public class ThemeCellView : ViewCell
    {
        public ThemeCellView()
        {

            var boxView = new BoxView()
            {
                CornerRadius = 5,
            };
            boxView.SetBinding(BoxView.ColorProperty, new Binding("BackgroundCellColour", BindingMode.OneTime));

            var label = new Label()
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
            label.SetBinding(Label.TextProperty, new Binding("ThemeName"));
            label.SetBinding(Label.TextColorProperty, new Binding("FontColour"));

            var stackLayout = new StackLayout()
            {
                Children = { label },
                Orientation = StackOrientation.Horizontal,
            };

            var gridLayout = new Grid()
            {
                Padding = 5,
                Children = { boxView, label}
            };

            View = gridLayout;

        }

    }
}
