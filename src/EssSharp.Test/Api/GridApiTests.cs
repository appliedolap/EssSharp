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
    ///  Class for testing GridApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    public class GridApiTests : IDisposable
    {
        private GridApi instance;

        public GridApiTests()
        {
            instance = new GridApi();
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of GridApi
        /// </summary>
        [Fact]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsType' GridApi
            //Assert.IsType<GridApi>(instance);
        }

        /// <summary>
        /// Test GridExecute
        /// </summary>
        [Fact]
        public void GridExecuteTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //string databaseName = null;
            //GridOperation body = null;
            //var response = instance.GridExecute(applicationName, databaseName, body);
            //Assert.IsType<Grid>(response);
        }

        /// <summary>
        /// Test GridExecuteLayout
        /// </summary>
        [Fact]
        public void GridExecuteLayoutTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //string databaseName = null;
            //string layout = null;
            //string user = null;
            //var response = instance.GridExecuteLayout(applicationName, databaseName, layout, user);
            //Assert.IsType<Grid>(response);
        }

        /// <summary>
        /// Test GridExecuteMDX
        /// </summary>
        [Fact]
        public void GridExecuteMDXTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //string databaseName = null;
            //MDXOperation body = null;
            //var response = instance.GridExecuteMDX(applicationName, databaseName, body);
            //Assert.IsType<Grid>(response);
        }

        /// <summary>
        /// Test GridGetDefault
        /// </summary>
        [Fact]
        public void GridGetDefaultTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //string databaseName = null;
            //bool? reset = null;
            //var response = instance.GridGetDefault(applicationName, databaseName, reset);
            //Assert.IsType<Grid>(response);
        }

        /// <summary>
        /// Test GridGetLayoutGrid
        /// </summary>
        [Fact]
        public void GridGetLayoutGridTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //string databaseName = null;
            //Grid body = null;
            //var response = instance.GridGetLayoutGrid(applicationName, databaseName, body);
            //Assert.IsType<GridLayout>(response);
        }
    }
}
