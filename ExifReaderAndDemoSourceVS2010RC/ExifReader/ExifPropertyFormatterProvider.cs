// <copyright file="ExifPropertyFormatterProvider.cs" company="Nish Sivakumar">
// Copyright (c) Nish Sivakumar. All rights reserved.
// </copyright>

namespace ExifReader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using PropertyFormatters;

    /// <summary>
    /// This class provides appropriate IExifPropertyFormatter objects for Exif property values
    /// </summary>
    public static class ExifPropertyFormatterProvider
    {
        /// <summary>
        /// Gets an IExifPropertyFormatter for the specific tagId
        /// </summary>
        /// <param name="tagId">The Exif Tag Id</param>
        /// <returns>An IExifPropertyFormatter</returns>
        internal static IExifPropertyFormatter GetExifPropertyFormatter(PropertyTagId tagId)
        {
            ExifPropertyFormatterAttribute attribute = CachedAttributeExtractor<PropertyTagId, ExifPropertyFormatterAttribute>.Instance.GetAttributeForField(tagId.ToString());

            if (attribute != null)
            {
                return attribute.ConstructorNeedsPropertyTag ? attribute.GetExifPropertyFormatter(tagId) : attribute.GetExifPropertyFormatter();
            }

            return new SimpleExifPropertyFormatter(tagId);
        }
    }
}
