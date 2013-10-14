// <copyright file="ExifValueUndefinedExtractorProvider.cs" company="Nish Sivakumar">
// Copyright (c) Nish Sivakumar. All rights reserved.
// </copyright>

namespace ExifReader
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using UndefinedExtractor;

    /// <summary>
    /// This class provides appropriate IExifValueUndefinedExtractor objects for Exif property values with undefined data
    /// </summary>
    public static class ExifValueUndefinedExtractorProvider
    {
        /// <summary>
        /// Gets an IExifValueUndefinedExtractor for the specific tagId
        /// </summary>
        /// <param name="tagId">The Exif Tag Id</param>
        /// <returns>An IExifValueUndefinedExtractor</returns>
        internal static IExifValueUndefinedExtractor GetExifValueUndefinedExtractor(PropertyTagId tagId)
        {
            ExifValueUndefinedExtractorAttribute attribute = CachedAttributeExtractor<PropertyTagId, ExifValueUndefinedExtractorAttribute>.Instance.GetAttributeForField(tagId.ToString());

            if (attribute != null)
            {
                return attribute.GetUndefinedExtractor();
            }

            return new SimpleUndefinedExtractor();
        }
    }
}
