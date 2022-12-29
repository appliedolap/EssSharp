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
    ///  Class for testing ExecuteMDXApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    public class ExecuteMDXApiTests : IDisposable
    {
        private ExecuteMDXApi instance;

        public ExecuteMDXApiTests()
        {
            instance = new ExecuteMDXApi();
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of ExecuteMDXApi
        /// </summary>
        [Fact]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsType' ExecuteMDXApi
            //Assert.IsType<ExecuteMDXApi>(instance);
        }

        /// <summary>
        /// Test MDXExecuteMDX
        /// </summary>
        [Fact]
        public void MDXExecuteMDXTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string application = null;
            //string database = null;
            //string format = null;
            //MDXInput body = null;
            //var response = instance.MDXExecuteMDX(application, database, format, body);
            //Assert.IsType<Object>(response);
        }

        /// <summary>
        /// Test MDXExecutenq
        /// </summary>
        [Fact]
        public void MDXExecutenqTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string application = null;
            //string database = null;
            //string name = null;
            //string format = null;
            //var response = instance.MDXExecutenq(application, database, name, format);
            //Assert.IsType<Object>(response);
        }
    }
}
