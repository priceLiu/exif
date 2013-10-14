// <copyright file="MainViewModel.cs" company="Nish Sivakumar">
// Copyright (c) Nish Sivakumar. All rights reserved.
// </copyright>

namespace ExifReaderWpfDemo
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Microsoft.Win32;
    using System.Windows;
    using ExifReader;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Data;

    internal class MainViewModel : INotifyPropertyChanged
    {
        private ICommand browseCommand;

        private ICommand exitCommand;

        private ICommand filterCommand;

        private string searchText = String.Empty;

        private ObservableCollection<ExifProperty> exifPropertiesInternal = new ObservableCollection<ExifProperty>();

        private CollectionViewSource exifProperties = new CollectionViewSource();

        private ImageSource previewImage;

        public MainViewModel()
        {
            exifProperties.Source = exifPropertiesInternal;
            exifProperties.Filter += ExifProperties_Filter;
        }

        public CollectionViewSource ExifProperties
        {
            get { return exifProperties; }
        }

        public ICommand BrowseCommand
        {
            get
            {
                return browseCommand ?? (browseCommand = new DelegateCommand(BrowseForImage));
            }
        }

        public ICommand ExitCommand
        {
            get
            {
                return exitCommand ?? (exitCommand = new DelegateCommand(() => Application.Current.Shutdown()));
            }
        }

        public ICommand FilterCommand
        {
            get
            {
                return filterCommand ?? (filterCommand = new DelegateCommand(() => exifProperties.View.Refresh()));
            }
        }

        public ImageSource PreviewImage
        {
            get
            {
                return previewImage;
            }

            set
            {
                if (previewImage != value)
                {
                    previewImage = value;
                    FirePropertyChanged("PreviewImage");
                }
            }
        }

        public string SearchText
        {
            get
            {
                return searchText;
            }

            set
            {
                if (searchText != value)
                {
                    searchText = value;
                    FirePropertyChanged("SearchText");
                }
            }
        }

        private void BrowseForImage()
        {
            OpenFileDialog fileDialog = new OpenFileDialog()
                {
                    Filter = "Image Files(*.PNG;*.JPG)|*.PNG;*.JPG;"
                };

            if (fileDialog.ShowDialog().GetValueOrDefault())
            {
                try
                {
                    ExifReader exifReader = new ExifReader(fileDialog.FileName);
                    exifPropertiesInternal.Clear();
                    this.SearchText = String.Empty;
                    foreach (var item in exifReader.GetExifProperties())
                    {
                        exifPropertiesInternal.Add(item);
                    }

                    this.PreviewImage = new BitmapImage(new Uri(fileDialog.FileName));
                }
                catch (ExifReaderException ex)
                {
                    MessageBox.Show(ex.Message, "ExifReaderException was caught");
                }
            }
        }

        private void ExifProperties_Filter(object sender, FilterEventArgs e)
        {
            ExifProperty exifProperty = e.Item as ExifProperty;
            if (exifProperty == null)
            {
                return;
            }

            foreach (string body in new[] { exifProperty.ExifPropertyName, exifProperty.ExifTag.ToString(), exifProperty.ToString() })
            {
                e.Accepted = body.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) != -1;

                if (e.Accepted)
                {
                    break;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void FirePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}