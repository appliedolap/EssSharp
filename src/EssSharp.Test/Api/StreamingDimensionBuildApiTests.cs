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
    ///  Class for testing StreamingDimensionBuildApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    public class StreamingDimensionBuildApiTests : IDisposable
    {
        private StreamingDimensionBuildApi instance;

        public StreamingDimensionBuildApiTests()
        {
            instance = new StreamingDimensionBuildApi();
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of StreamingDimensionBuildApi
        /// </summary>
        [Fact]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsType' StreamingDimensionBuildApi
            //Assert.IsType<StreamingDimensionBuildApi>(instance);
        }

        /// <summary>
        /// Test DimensionBuildEnd
        /// </summary>
        [Fact]
        public void DimensionBuildEndTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //string databaseName = null;
            //string streamId = null;
            //var response = instance.DimensionBuildEnd(applicationName, databaseName, streamId);
            //Assert.IsType<StreamProcessEndResponse>(response);
        }

        /// <summary>
        /// Test DimensionBuildEndDimBuild
        /// </summary>
        [Fact]
        public void DimensionBuildEndDimBuildTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //string databaseName = null;
            //string streamId = null;
            //string ruleFileName = null;
            //var response = instance.DimensionBuildEndDimBuild(applicationName, databaseName, streamId, ruleFileName);
            //Assert.IsType<StreamProcessEndResponse>(response);
        }

        /// <summary>
        /// Test DimensionBuildStart
        /// </summary>
        [Fact]
        public void DimensionBuildStartTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //string databaseName = null;
            //DimBuildStartPayload body = null;
            //var response = instance.DimensionBuildStart(applicationName, databaseName, body);
            //Assert.IsType<StreamProcessStartResponse>(response);
        }

        /// <summary>
        /// Test DimensionBuildStartDimBuild
        /// </summary>
        [Fact]
        public void DimensionBuildStartDimBuildTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //string databaseName = null;
            //string streamId = null;
            //string ruleFileName = null;
            //var response = instance.DimensionBuildStartDimBuild(applicationName, databaseName, streamId, ruleFileName);
            //Assert.IsType<StreamProcessStartResponse>(response);
        }

        /// <summary>
        /// Test DimensionBuildStreamDimBuildData
        /// </summary>
        [Fact]
        public void DimensionBuildStreamDimBuildDataTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //string databaseName = null;
            //string streamId = null;
            //string body = null;
            //var response = instance.DimensionBuildStreamDimBuildData(applicationName, databaseName, streamId, body);
            //Assert.IsType<StreamProcessStartResponse>(response);
        }
    }
}
