﻿/*
 * The REST API for Oracle Essbase enables you to automate management of Essbase resources and operations. All requests and responses are communicated over secured HTTP.
 *
 * The version of the OpenAPI document: 1.0
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.ComponentModel.DataAnnotations;

namespace EssSharp.Model
{
    /// <summary>
    /// GridRange
    /// </summary>
    public partial class GridRange : IEquatable<GridRange>, IValidatableObject
    {

        /// <summary>
        /// Uncomment this method to serialize only "rowSuppression" rather than both "rowSuppression" and "rowSupression".
        /// </summary>
        public bool ShouldSerializeTypes() => true;
    }
}
