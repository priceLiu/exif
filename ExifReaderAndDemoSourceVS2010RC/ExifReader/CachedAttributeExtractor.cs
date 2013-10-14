// <copyright file="CachedAttributeExtractor.cs" company="Nish Sivakumar">
// Copyright (c) Nish Sivakumar. All rights reserved.
// </copyright>

namespace ExifReader
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// A generic class used to retrieve an attribute from a type, 
    /// and cache the extracted values for future access.
    /// </summary>
    /// <typeparam name="T">The type to search on</typeparam>
    /// <typeparam name="A">The attribute type to extract</typeparam>
    internal class CachedAttributeExtractor<T, A> where A : Attribute
    {
        /// <summary>
        /// The singleton instance
        /// </summary>
        private static CachedAttributeExtractor<T, A> instance = new CachedAttributeExtractor<T, A>();

        /// <summary>
        /// The map of fields to attributes
        /// </summary>
        private Dictionary<string, A> fieldAttributeMap = new Dictionary<string, A>();

        /// <summary>
        /// Prevents a default instance of the CachedAttributeExtractor class from being created.
        /// </summary>
        private CachedAttributeExtractor()
        {
        }

        /// <summary>
        /// Gets the singleton instance
        /// </summary>
        internal static CachedAttributeExtractor<T, A> Instance
        {
            get { return CachedAttributeExtractor<T, A>.instance; }
        }

        /// <summary>
        /// Gets the attribute for the field
        /// </summary>
        /// <param name="field">Name of the field</param>
        /// <returns>The attribute on the field or null</returns>
        public A GetAttributeForField(string field)
        {
            A attribute;

            if (!this.fieldAttributeMap.TryGetValue(field, out attribute))
            {
                if (this.TryExtractAttributeFromField(field, out attribute))
                {
                    this.fieldAttributeMap[field] = attribute;
                }
                else
                {
                    attribute = null;
                }
            }

            return attribute;
        }

        /// <summary>
        /// Get the attribute for the field 
        /// </summary>
        /// <param name="field">Name of the field</param>
        /// <param name="attribute">The attribute</param>
        /// <returns>Returns true of the attribute was found</returns>
        private bool TryExtractAttributeFromField(string field, out A attribute)
        {
            var fieldInfo = typeof(T).GetField(field);
            attribute = null;

            if (fieldInfo != null)
            {
                A[] attributes = fieldInfo.GetCustomAttributes(typeof(A), false) as A[];
                if (attributes.Length > 0)
                {
                    attribute = attributes[0];
                }
            }

            return attribute != null;
        }
    }
}
