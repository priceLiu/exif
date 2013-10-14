// <copyright file="ExifValueUndefinedExtractorAttribute.cs" company="Nish Sivakumar">
// Copyright (c) Nish Sivakumar. All rights reserved.
// </copyright>

namespace ExifReader
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// An attribute used to specify an IExifValueUndefinedExtractor for Exif Tags with undefined data value types
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    internal class ExifValueUndefinedExtractorAttribute : Attribute
    {
        /// <summary>
        /// The IExifValueUndefinedExtractor object
        /// </summary>
        private IExifValueUndefinedExtractor undefinedExtractor;

        /// <summary>
        /// The type of the IExifValueUndefinedExtractor
        /// </summary>
        private Type undefinedExtractorType;

        /// <summary>
        /// Initializes a new instance of the ExifValueUndefinedExtractorAttribute class
        /// </summary>
        /// <param name="undefinedExtractorType">The type of the IExifValueUndefinedExtractor</param>
        public ExifValueUndefinedExtractorAttribute(Type undefinedExtractorType)
        {
            this.undefinedExtractorType = undefinedExtractorType;
        }

        /// <summary>
        /// Gets the IExifValueUndefinedExtractor
        /// </summary>
        /// <returns>The IExifValueUndefinedExtractor</returns>
        public IExifValueUndefinedExtractor GetUndefinedExtractor()
        {
            return this.undefinedExtractor ??
                (this.undefinedExtractor = Activator.CreateInstance(this.undefinedExtractorType) as IExifValueUndefinedExtractor);
        }
    }
}
