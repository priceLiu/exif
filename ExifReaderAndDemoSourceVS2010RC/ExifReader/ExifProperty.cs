// <copyright file="ExifProperty.cs" company="Nish Sivakumar">
// Copyright (c) Nish Sivakumar. All rights reserved.
// </copyright>

namespace ExifReader
{
    using System;
    using System.Collections.Generic;
    using System.Drawing.Imaging;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Represents an Exif property.
    /// </summary>
    public class ExifProperty
    {
        /// <summary>
        /// The PropertyItem associated with this object.
        /// </summary>
        private PropertyItem propertyItem;

        /// <summary>
        /// The IExifValue associated with this object.
        /// </summary>
        private IExifValue exifValue;

        /// <summary>
        /// The IExifPropertyFormatter for this property.
        /// </summary>
        private IExifPropertyFormatter propertyFormatter;

        /// <summary>
        /// Set to true if this object represents an unknown property tag
        /// </summary>
        private bool isUnknown;

        /// <summary>
        /// Set to true if this object has a custom property formatter
        /// </summary>
        private bool hasCustomFormatter;

        /// <summary>
        /// The parent ExifReader that owns this ExifProperty object
        /// </summary>
        private ExifReader parentReader;

        /// <summary>
        /// Initializes a new instance of the ExifProperty class.
        /// It's marked internal  as it's not intended to be instantiated independently outside of the library.
        /// </summary>
        /// <param name="propertyItem">The PropertyItem to base the object on</param>
        /// <param name="parentReader">The parent ExifReader</param>
        internal ExifProperty(PropertyItem propertyItem, ExifReader parentReader)
        {
            this.parentReader = parentReader;
            this.propertyItem = propertyItem;
            this.isUnknown = !Enum.IsDefined(typeof(PropertyTagId), this.RawExifTagId);

            var customFormatter = this.parentReader.QueryForCustomPropertyFormatter(this.RawExifTagId);

            if (customFormatter == null)
            {
                this.propertyFormatter = ExifPropertyFormatterProvider.GetExifPropertyFormatter(this.ExifTag);
            }
            else
            {
                this.propertyFormatter = customFormatter;
                this.hasCustomFormatter = true;
            }
        }

        /// <summary>
        /// Gets the IExifValue for this property
        /// </summary>
        public IExifValue ExifValue
        {
            get
            {
                return this.exifValue ?? this.InitializeExifValue();
            }
        }

        /// <summary>
        /// Gets the descriptive name of the Exif property
        /// </summary>
        public string ExifPropertyName
        {
            get
            {
                try
                {
                    return this.hasCustomFormatter || !this.isUnknown ?
                        this.propertyFormatter.DisplayName :
                        String.Format("{0} #{1}", this.propertyFormatter.DisplayName, this.propertyItem.Id);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(
                        "An ExifReaderException was caught. See InnerException for more details",
                        this.GetExifReaderException(ex));
                }
            }
        }        

        /// <summary>
        /// Gets a category name for the property.
        /// Note: This is not part of the Exif standard and is merely for convenience.
        /// </summary>
        public string ExifPropertyCategory
        {
            get
            {
                return this.isUnknown ? "Unknown" : "General";
            }
        }

        /// <summary>
        /// Gets the Exif property tag Id for this property
        /// </summary>
        public PropertyTagId ExifTag
        {
            get
            {
                return this.isUnknown ? PropertyTagId.UnknownExifTag : (PropertyTagId)this.propertyItem.Id;
            }
        }

        /// <summary>
        /// Gets the Exif data type for this property
        /// </summary>
        public PropertyTagType ExifDatatype
        {
            get
            {
                return (PropertyTagType)this.propertyItem.Type;
            }
        }

        /// <summary>
        /// Gets the raw Exif tag. For unknown tags this will not
        /// match the value of the ExifTag property.
        /// </summary>
        public int RawExifTagId
        {
            get
            {
                return this.propertyItem.Id;
            }
        }

        /// <summary>
        /// Override for ToString
        /// </summary>
        /// <returns>Returns a readable string representing the Exif property's value</returns>
        public override string ToString()
        {
            return this.GetFormattedString();
        }

        /// <summary>
        /// Gets the formatted string using the property formatter
        /// </summary>
        /// <returns>The formatted string</returns>
        private string GetFormattedString()
        {
            try
            {
                return this.propertyFormatter.GetFormattedString(this.ExifValue);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "An ExifReaderException was caught. See InnerException for more details",
                    this.GetExifReaderException(ex));
            }
        }

        /// <summary>
        /// Initializes the exifValue field.
        /// </summary>
        /// <returns>The initialized exifValue</returns>
        private IExifValue InitializeExifValue()
        {
            try
            {
                var customExtractor = this.parentReader.QueryForCustomUndefinedExtractor(this.RawExifTagId);
                if (customExtractor != null)
                {
                    return this.exifValue = customExtractor.GetExifValue(this.propertyItem.Value, this.propertyItem.Len);
                } 

                return this.exifValue = this.ExifDatatype == PropertyTagType.Undefined ?
                        ExifValueCreator.CreateUndefined(this.ExifTag, this.propertyItem.Value, this.propertyItem.Len) :
                        ExifValueCreator.Create(this.ExifDatatype, this.propertyItem.Value, this.propertyItem.Len);
            }
            catch (ExifReaderException ex)
            {
                throw new InvalidOperationException(
                    "An ExifReaderException was caught. See InnerException for more details",
                    ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "An ExifReaderException was caught. See InnerException for more details",
                    new ExifReaderException(ex, this.propertyFormatter, null));
            }
        }

        /// <summary>
        /// Returns an ExifReaderException set with the current property formatter
        /// </summary>
        /// <param name="ex">Inner exception object</param>
        /// <returns>The ExifReaderException object</returns>
        private ExifReaderException GetExifReaderException(Exception ex)
        {
            return new ExifReaderException(ex, this.propertyFormatter, null);
        }
    }
}
