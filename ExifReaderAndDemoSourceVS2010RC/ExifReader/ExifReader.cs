// <copyright file="ExifReader.cs" company="Nish Sivakumar">
// Copyright (c) Nish Sivakumar. All rights reserved.
// </copyright>

namespace ExifReader
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;

    /// <summary>
    /// This is the implementation of the ExifReader class that reads EXIF data from image files.
    /// It partially supports the Exif Version 2.2 standard.
    /// </summary>
    [TypeDescriptionProvider(typeof(ExifReaderTypeDescriptionProvider))]
    public class ExifReader
    {
        /// <summary>
        /// List of Exif properties for the image.
        /// </summary>
        private List<ExifProperty> exifproperties;

        /// <summary>
        /// The Image object associated with the image file
        /// </summary>
        private Image imageFile;

        /// <summary>
        /// Initializes a new instance of the ExifReader class based on a file path.
        /// </summary>
        /// <param name="imageFileName">Full path to the image file</param>
        public ExifReader(string imageFileName)
        {
            try
            {
                this.imageFile = Image.FromFile(imageFileName);
            }
            catch (FileNotFoundException ex)
            {
                throw new ExifReaderException("The image file was not found. See the inner exception for more details.", ex);
            }
            catch (Exception ex)
            {
                throw new ExifReaderException("The image file could not be read. See the inner exception for more details.", ex);
            }
        }

        /// <summary>
        /// Occurs when the class needs to query for a property formatter
        /// </summary>
        public event EventHandler<QueryPropertyFormatterEventArgs> QueryPropertyFormatter;

        /// <summary>
        /// Occurs when the class needs to query for an undefined extractor
        /// </summary>
        public event EventHandler<QueryUndefinedExtractorEventArgs> QueryUndefinedExtractor;

        /// <summary>
        /// Returns a read-only collection of all the Exif properties
        /// </summary>
        /// <returns>The Exif properties</returns>
        public ReadOnlyCollection<ExifProperty> GetExifProperties()
        {
            if (this.exifproperties == null)
            {
                this.InitializeExifProperties();
            }

            return this.exifproperties.AsReadOnly();
        }

        /// <summary>
        /// Checks to see if a custom property formatter is available
        /// </summary>
        /// <param name="tagId">The tag Id to check for a formatter</param>
        /// <returns>An IExifPropertyFormatter or null if there's no formatter available</returns>
        internal IExifPropertyFormatter QueryForCustomPropertyFormatter(int tagId)
        {
            QueryPropertyFormatterEventArgs eventArgs = new QueryPropertyFormatterEventArgs(tagId);
            this.FireQueryPropertyFormatter(eventArgs);
            return eventArgs.PropertyFormatter;
        }

        /// <summary>
        /// Checks to see if a custom undefined extractor is available
        /// </summary>
        /// <param name="tagId">The tag Id to check for an extractor</param>
        /// <returns>An IExifValueUndefinedExtractor or null if there's no formatter available</returns>
        internal IExifValueUndefinedExtractor QueryForCustomUndefinedExtractor(int tagId)
        {
            QueryUndefinedExtractorEventArgs eventArgs = new QueryUndefinedExtractorEventArgs(tagId);
            this.FireQueryUndefinedExtractor(eventArgs);
            return eventArgs.UndefinedExtractor;
        }

        /// <summary>
        /// Fires the QueryPropertyFormatter event
        /// </summary>
        /// <param name="eventArgs">Args data for the QueryPropertyFormatter event</param>
        private void FireQueryPropertyFormatter(QueryPropertyFormatterEventArgs eventArgs)
        {
            EventHandler<QueryPropertyFormatterEventArgs> queryPropertyFormatter = this.QueryPropertyFormatter;

            if (queryPropertyFormatter != null)
            {
                queryPropertyFormatter(this, eventArgs);
            }
        }

        /// <summary>
        /// Fires the QueryUndefinedExtractor event
        /// </summary>
        /// <param name="eventArgs">Args data for the QueryUndefinedExtractor event</param>
        private void FireQueryUndefinedExtractor(QueryUndefinedExtractorEventArgs eventArgs)
        {
            EventHandler<QueryUndefinedExtractorEventArgs> queryUndefinedExtractor = this.QueryUndefinedExtractor;

            if (queryUndefinedExtractor != null)
            {
                queryUndefinedExtractor(this, eventArgs);
            }
        }

        /// <summary>
        /// Initializes the Exif properties for the associated image file
        /// </summary>
        private void InitializeExifProperties()
        {
            this.exifproperties = new List<ExifProperty>();

            foreach (var propertyItem in this.imageFile.PropertyItems)
            {
                this.exifproperties.Add(new ExifProperty(propertyItem, this));
            }
        }
    }
}
