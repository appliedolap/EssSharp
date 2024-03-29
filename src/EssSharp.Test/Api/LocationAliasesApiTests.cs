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
    ///  Class for testing LocationAliasesApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    public class LocationAliasesApiTests : IDisposable
    {
        private LocationAliasesApi instance;

        public LocationAliasesApiTests()
        {
            instance = new LocationAliasesApi();
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of LocationAliasesApi
        /// </summary>
        [Fact]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsType' LocationAliasesApi
            //Assert.IsType<LocationAliasesApi>(instance);
        }

        /// <summary>
        /// Test LocationAliasesCreate
        /// </summary>
        [Fact]
        public void LocationAliasesCreateTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //string databaseName = null;
            //LocationAliasBean body = null;
            //instance.LocationAliasesCreate(applicationName, databaseName, body);
        }

        /// <summary>
        /// Test LocationAliasesDelete
        /// </summary>
        [Fact]
        public void LocationAliasesDeleteTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //string databaseName = null;
            //string aliasName = null;
            //instance.LocationAliasesDelete(applicationName, databaseName, aliasName);
        }

        /// <summary>
        /// Test LocationAliasesGetLocationAlias
        /// </summary>
        [Fact]
        public void LocationAliasesGetLocationAliasTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //string databaseName = null;
            //string aliasName = null;
            //var response = instance.LocationAliasesGetLocationAlias(applicationName, databaseName, aliasName);
            //Assert.IsType<LocationAliasBean>(response);
        }

        /// <summary>
        /// Test LocationAliasesGetLocationAliases
        /// </summary>
        [Fact]
        public void LocationAliasesGetLocationAliasesTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //string databaseName = null;
            //int? offset = null;
            //int? limit = null;
            //string serverName = null;
            //string applicationName2 = null;
            //string databaseName2 = null;
            //var response = instance.LocationAliasesGetLocationAliases(applicationName, databaseName, offset, limit, serverName, applicationName2, databaseName2);
            //Assert.IsType<LocationAliasList>(response);
        }

        /// <summary>
        /// Test LocationAliasesUpdate
        /// </summary>
        [Fact]
        public void LocationAliasesUpdateTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //string databaseName = null;
            //string aliasName = null;
            //LocationAliasBean body = null;
            //instance.LocationAliasesUpdate(applicationName, databaseName, aliasName, body);
        }
    }
}
