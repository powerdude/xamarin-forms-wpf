﻿using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;
using Xamarin.Forms.Platform.WPF.Rendereres;

[assembly: ExportRenderer(typeof(Button), typeof(ButtonRenderer))]
namespace Xamarin.Forms.Platform.WPF.Rendereres
{
    public class ButtonRenderer : ViewRenderer<Button, System.Windows.Controls.Button>
    {
        System.Windows.Media.Brush DefaultBorderBrush;
        System.Windows.Media.Brush DefaultFontBrush;
        System.Windows.Media.FontFamily DefaultFontFamily;
        double DefaultFontSize;

        public ButtonRenderer()
        {
            Content = new System.Windows.Controls.Button();
            DefaultBorderBrush = Content.BorderBrush;
            DefaultFontBrush = Content.Foreground;
            DefaultFontFamily = Content.FontFamily;
            DefaultFontSize = Content.FontSize;
            Content.Click += Content_Click;
            HandleProperty(Button.TextProperty, Handle_TextProperty);
            HandleProperty(Button.CommandParameterProperty, Handle_CommandParameterProperty);
            HandleProperty(Button.CommandProperty, Handle_CommandProperty);
            HandleProperty(Button.BorderWidthProperty, Handle_BorderWidth);
            HandleProperty(Button.BorderColorProperty, Handle_BorderColor);
            HandleProperty(Button.FontProperty, Handle_FontProperty);
            HandleProperty(Button.TextColorProperty, Handle_TextColorProperty);
        }

        void Content_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Model != null)
                Model.SendClicked();
        }

        bool Handle_TextProperty(BindableProperty property)
        {
            Content.Content = Model.Text;
            return true;
        }

        bool Handle_CommandProperty(BindableProperty property)
        {
            Content.Command = Model.Command;
            return true;
        }

        bool Handle_CommandParameterProperty(BindableProperty property)
        {
            Content.CommandParameter = Model.CommandParameter;
            return true;
        }

        bool Handle_BorderWidth(BindableProperty property)
        {
            Content.BorderThickness = new System.Windows.Thickness(Model.BorderWidth);
            return true;
        }

        bool Handle_BorderColor(BindableProperty property)
        {
            Content.BorderBrush = Model.BorderColor.ToBrush() ?? DefaultBorderBrush;
            return true;
        }

        private bool Handle_TextColorProperty(BindableProperty property)
        {
            Content.Foreground = Model.TextColor.ToBrush() ?? DefaultFontBrush;
            return true;
        }

        private bool Handle_FontProperty(BindableProperty property)
        {
            if(Model.Font == null)
            {
                Content.FontFamily = DefaultFontFamily;
                Content.FontSize = DefaultFontSize;
                Content.FontWeight = System.Windows.FontWeights.Normal;
                return true;
            }

            Content.FontFamily = Model.Font.FontName == null ? DefaultFontFamily : new System.Windows.Media.FontFamily(Model.Font.FontName);
            Content.FontSize = Model.Font.GetWPFSize();
            Content.FontWeight = Model.Font.Bold ? System.Windows.FontWeights.Bold : System.Windows.FontWeights.Normal;
            return true;
        }
    }
}
