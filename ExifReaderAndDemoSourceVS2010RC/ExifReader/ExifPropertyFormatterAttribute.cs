// <copyright file="ExifPropertyFormatterAttribute.cs" company="Nish Sivakumar">
// Copyright (c) Nish Sivakumar. All rights reserved.
// </copyright>

namespace ExifReader
{
    using System;

    /// <summary>
    /// An attribute used to specify an IExifPropertyFormatter for Exif Tag Ids 
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    internal class ExifPropertyFormatterAttribute : Attribute
    {
        /// <summary>
        /// The IExifPropertyFormatter object
        /// </summary>
        private IExifPropertyFormatter exifPropertyFormatter;

        /// <summary>
        /// The type of the IExifPropertyFormatter
        /// </summary>
        private Type exifPropertyFormatterType;

        /// <summary>
        /// Initializes a new instance of the ExifPropertyFormatterAttribute class
        /// </summary>
        /// <param name="exifPropertyFormatterType">The type of the IExifPropertyFormatter</param>
        public ExifPropertyFormatterAttribute(Type exifPropertyFormatterType)
        {
            this.exifPropertyFormatterType = exifPropertyFormatterType;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the constructor for the property formatter
        /// needs to be passed the property tag as an argument. 
        /// </summary>
        public bool ConstructorNeedsPropertyTag { get; set; }

        /// <summary>
        /// Gets the IExifPropertyFormatter
        /// </summary>
        /// <param name="args">Optional arguments</param>
        /// <returns>The IExifPropertyFormatter</returns>
        public IExifPropertyFormatter GetExifPropertyFormatter(params object[] args)
        {
                return this.exifPropertyFormatter ??
                    (this.exifPropertyFormatter = Activator.CreateInstance(this.exifPropertyFormatterType, args) as IExifPropertyFormatter);
        }
    }
}
