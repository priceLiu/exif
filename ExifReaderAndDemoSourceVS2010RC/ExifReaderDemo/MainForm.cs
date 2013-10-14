// <copyright file="MainForm.cs" company="Nish Sivakumar">
// Copyright (c) Nish Sivakumar. All rights reserved.
// </copyright>

namespace ExifReaderDemo
{
    using System;
    using System.Linq;
    using System.Windows.Forms;
    using ExifReader;

    /// <summary>
    /// Main form class implementation.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the MainForm class.
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Event handler for Browse button Click.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ButtonBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog()
                {
                    Filter = "Image Files(*.PNG;*.JPG)|*.PNG;*.JPG;"
                })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ExifReader exifReader = new ExifReader(dialog.FileName);
                        this.propertyGridExif.SelectedObject = exifReader;
                        this.pictureBoxPreview.ImageLocation = dialog.FileName;
                    }
                    catch (ExifReaderException ex)
                    {
                        MessageBox.Show(ex.Message, "ExifReaderException was caught");
                    }
                }
            }
        }
    }
}
