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
    ///  Class for testing StreamingDataLoadApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    public class StreamingDataLoadApiTests : IDisposable
    {
        private StreamingDataLoadApi instance;

        public StreamingDataLoadApiTests()
        {
            instance = new StreamingDataLoadApi();
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of StreamingDataLoadApi
        /// </summary>
        [Fact]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsType' StreamingDataLoadApi
            //Assert.IsType<StreamingDataLoadApi>(instance);
        }

        /// <summary>
        /// Test DataloadEnd
        /// </summary>
        [Fact]
        public void DataloadEndTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //string databaseName = null;
            //string streamId = null;
            //var response = instance.DataloadEnd(applicationName, databaseName, streamId);
            //Assert.IsType<StreamProcessEndResponse>(response);
        }

        /// <summary>
        /// Test DataloadStart
        /// </summary>
        [Fact]
        public void DataloadStartTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //string databaseName = null;
            //DataLoadStartPayload body = null;
            //var response = instance.DataloadStart(applicationName, databaseName, body);
            //Assert.IsType<StreamProcessStartResponse>(response);
        }

        /// <summary>
        /// Test DataloadStreamData
        /// </summary>
        [Fact]
        public void DataloadStreamDataTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //string databaseName = null;
            //string streamId = null;
            //string body = null;
            //var response = instance.DataloadStreamData(applicationName, databaseName, streamId, body);
            //Assert.IsType<StreamProcessStartResponse>(response);
        }
    }
}
