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
    ///  Class for testing ApplicationLogsApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    public class ApplicationLogsApiTests : IDisposable
    {
        private ApplicationLogsApi instance;

        public ApplicationLogsApiTests()
        {
            instance = new ApplicationLogsApi();
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of ApplicationLogsApi
        /// </summary>
        [Fact]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsType' ApplicationLogsApi
            //Assert.IsType<ApplicationLogsApi>(instance);
        }

        /// <summary>
        /// Test ApplicationLogsDownloadAllLogFiles
        /// </summary>
        [Fact]
        public void ApplicationLogsDownloadAllLogFilesTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //instance.ApplicationLogsDownloadAllLogFiles(applicationName);
        }

        /// <summary>
        /// Test ApplicationLogsDownloadAppLogFiles
        /// </summary>
        [Fact]
        public void ApplicationLogsDownloadAppLogFilesTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //var response = instance.ApplicationLogsDownloadAppLogFiles(applicationName);
            //Assert.IsType<Link>(response);
        }

        /// <summary>
        /// Test ApplicationLogsDownloadLatestLogFile
        /// </summary>
        [Fact]
        public void ApplicationLogsDownloadLatestLogFileTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //instance.ApplicationLogsDownloadLatestLogFile(applicationName);
        }
    }
}
