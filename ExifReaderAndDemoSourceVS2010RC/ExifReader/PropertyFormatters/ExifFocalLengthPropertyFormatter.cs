// <copyright file="ExifFocalLengthPropertyFormatter.cs" company="Nish Sivakumar">
// Copyright (c) Nish Sivakumar. All rights reserved.
// </copyright>

namespace ExifReader.PropertyFormatters
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// A generic IExifPropertyFormatter that handles focal length property values.
    /// </summary>
    internal class ExifFocalLengthPropertyFormatter : SimpleExifPropertyFormatter
    {
        /// <summary>
        /// Initializes a new instance of the ExifFocalLengthPropertyFormatter class.
        /// </summary>
        /// <param name="tagId">The associated PropertyTagId</param>
        public ExifFocalLengthPropertyFormatter(PropertyTagId tagId)
            : base(tagId)
        {            
        }

        /// <summary>
        /// Gets a formatted string for a given Exif value
        /// </summary>
        /// <param name="exifValue">The source Exif value</param>
        /// <returns>The formatted string</returns>
        public override string GetFormattedString(IExifValue exifValue)
        {
            var values = exifValue.Values.Cast<Rational32>();
            return values.Count() == 0 ? String.Empty : String.Concat(((double)values.First()).ToString("0"), " mm");
        }
    }
}
