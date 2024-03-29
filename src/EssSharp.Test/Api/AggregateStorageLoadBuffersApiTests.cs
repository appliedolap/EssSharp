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
    ///  Class for testing AggregateStorageLoadBuffersApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    public class AggregateStorageLoadBuffersApiTests : IDisposable
    {
        private AggregateStorageLoadBuffersApi instance;

        public AggregateStorageLoadBuffersApiTests()
        {
            instance = new AggregateStorageLoadBuffersApi();
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of AggregateStorageLoadBuffersApi
        /// </summary>
        [Fact]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsType' AggregateStorageLoadBuffersApi
            //Assert.IsType<AggregateStorageLoadBuffersApi>(instance);
        }

        /// <summary>
        /// Test ASOLoadBuffersCreateBuffer
        /// </summary>
        [Fact]
        public void ASOLoadBuffersCreateBufferTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //string databaseName = null;
            //DataLoadBuffer body = null;
            //instance.ASOLoadBuffersCreateBuffer(applicationName, databaseName, body);
        }

        /// <summary>
        /// Test ASOLoadBuffersDestroyBuffers
        /// </summary>
        [Fact]
        public void ASOLoadBuffersDestroyBuffersTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //string databaseName = null;
            //DestroyBuffer body = null;
            //instance.ASOLoadBuffersDestroyBuffers(applicationName, databaseName, body);
        }

        /// <summary>
        /// Test ASOLoadBuffersListBuffers
        /// </summary>
        [Fact]
        public void ASOLoadBuffersListBuffersTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //string databaseName = null;
            //var response = instance.ASOLoadBuffersListBuffers(applicationName, databaseName);
            //Assert.IsType<LoadBuffersList>(response);
        }

        /// <summary>
        /// Test ASOLoadBuffersMerge
        /// </summary>
        [Fact]
        public void ASOLoadBuffersMergeTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //string databaseName = null;
            //MergeSilceOption body = null;
            //instance.ASOLoadBuffersMerge(applicationName, databaseName, body);
        }
    }
}
