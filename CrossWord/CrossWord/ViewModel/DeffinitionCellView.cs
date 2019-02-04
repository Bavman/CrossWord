using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CrossWord.ViewModel
{
    class DeffinitionCellView : ViewCell
    {

        public DeffinitionCellView()
        {

            var index = new Label()
            {
                Margin = new Thickness(5, 0, 0, 0),
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                WidthRequest = 40
            };
            index.SetBinding(Label.TextProperty, new Binding("Index"));
            index.SetBinding(Label.FontAttributesProperty, new Binding("FontWeight"));
            index.SetBinding(Label.TextColorProperty, new Binding("FontColour"));

            var definition = new Label()
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            };

            definition.SetBinding(Label.TextProperty, new Binding("Phrase"));
            definition.SetBinding(Label.TextColorProperty, new Binding("FontColour"));
            definition.SetBinding(Label.FontAttributesProperty, new Binding("FontWeight"));

            var stackLayout = new StackLayout()
            {
                Children = { index, definition },
                Orientation = StackOrientation.Horizontal,  
            };

            View = stackLayout;
            
        }
    }
}
