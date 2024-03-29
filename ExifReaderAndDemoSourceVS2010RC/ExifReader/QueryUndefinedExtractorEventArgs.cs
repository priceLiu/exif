﻿// <copyright file="QueryUndefinedExtractorEventArgs.cs" company="Nish Sivakumar">
// Copyright (c) Nish Sivakumar. All rights reserved.
// </copyright>

namespace ExifReader
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// Provides data for the QueryUndefinedExtractor event
    /// </summary>
    public class QueryUndefinedExtractorEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the QueryUndefinedExtractorEventArgs class.
        /// </summary>
        /// <param name="tagId">The tag Id to query a property formatter for</param>
        public QueryUndefinedExtractorEventArgs(int tagId)
        {
            this.TagId = tagId;
        }

        /// <summary>
        /// Gets or sets the associated property formatter
        /// </summary>
        public IExifValueUndefinedExtractor UndefinedExtractor { get; set; }

        /// <summary>
        /// Gets the associated tag Id
        /// </summary>
        public int TagId { get; private set; }
    }
}
