// <copyright file="TextChangedBehavior.cs" company="Nish Sivakumar">
// Copyright (c) Nish Sivakumar. All rights reserved.
// </copyright>

namespace ExifReaderWpfDemo
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Controls;
    using System.Windows.Data;

    internal class TextChangedBehavior
    {
        public static DependencyProperty TextChangedCommandProperty = DependencyProperty.RegisterAttached(
            "TextChanged", typeof(ICommand), typeof(TextChangedBehavior),
            new FrameworkPropertyMetadata(null, new PropertyChangedCallback(TextChangedBehavior.TextChangedChanged)));

        public static void SetTextChanged(TextBox target, ICommand value)
        {
            target.SetValue(TextChangedBehavior.TextChangedCommandProperty, value);
        }

        public static ICommand GetTextChanged(TextBox target)
        {
            return (ICommand)target.GetValue(TextChangedCommandProperty);
        }

        private static void TextChangedChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            TextBox element = target as TextBox;

            if (element != null)
            {
                if (e.NewValue != null)
                {
                    element.TextChanged += Element_TextChanged;
                }
                else
                {
                    element.TextChanged -= Element_TextChanged;
                }
            }
        }

        static void Element_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            BindingExpression bindingExpression = textBox.GetBindingExpression(TextBox.TextProperty);

            if (bindingExpression != null)
            {                
                bindingExpression.UpdateSource();
            }

            ICommand command = GetTextChanged(textBox);

            if (command.CanExecute(null))
            {
                command.Execute(null);
            }
        }
    }
}
