/*
 * The REST API for Oracle Essbase enables you to automate management of Essbase resources and operations. All requests and responses are communicated over secured HTTP.
 *
 * The version of the OpenAPI document: 1.0
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */

using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using RestSharp;
using Xunit;

using EssSharp.Client;
using EssSharp.Api;
// uncomment below to import models
//using EssSharp.Model;

namespace EssSharp.Test.Api
{
    /// <summary>
    ///  Class for testing PreferencesApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    public class PreferencesApiTests : IDisposable
    {
        private PreferencesApi instance;

        public PreferencesApiTests()
        {
            instance = new PreferencesApi();
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of PreferencesApi
        /// </summary>
        [Fact]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsType' PreferencesApi
            //Assert.IsType<PreferencesApi>(instance);
        }

        /// <summary>
        /// Test PreferencesGet
        /// </summary>
        [Fact]
        public void PreferencesGetTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //var response = instance.PreferencesGet();
            //Assert.IsType<Preference>(response);
        }
    }
}
