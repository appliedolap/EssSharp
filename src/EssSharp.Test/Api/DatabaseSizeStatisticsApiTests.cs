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
    ///  Class for testing DatabaseSizeStatisticsApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    public class DatabaseSizeStatisticsApiTests : IDisposable
    {
        private DatabaseSizeStatisticsApi instance;

        public DatabaseSizeStatisticsApiTests()
        {
            instance = new DatabaseSizeStatisticsApi();
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of DatabaseSizeStatisticsApi
        /// </summary>
        [Fact]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsType' DatabaseSizeStatisticsApi
            //Assert.IsType<DatabaseSizeStatisticsApi>(instance);
        }

        /// <summary>
        /// Test DatabaseSizeStatisticsGetDBSizes
        /// </summary>
        [Fact]
        public void DatabaseSizeStatisticsGetDBSizesTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //var response = instance.DatabaseSizeStatisticsGetDBSizes();
            //Assert.IsType<DBSizeList>(response);
        }
    }
}
