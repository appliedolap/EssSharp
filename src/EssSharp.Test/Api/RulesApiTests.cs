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
    ///  Class for testing RulesApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    public class RulesApiTests : IDisposable
    {
        private RulesApi instance;

        public RulesApiTests()
        {
            instance = new RulesApi();
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of RulesApi
        /// </summary>
        [Fact]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsType' RulesApi
            //Assert.IsType<RulesApi>(instance);
        }

        /// <summary>
        /// Test RulesGet
        /// </summary>
        [Fact]
        public void RulesGetTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string path = null;
            //var response = instance.RulesGet(path);
            //Assert.IsType<Rules>(response);
        }

        /// <summary>
        /// Test RulesGetPreviewData
        /// </summary>
        [Fact]
        public void RulesGetPreviewDataTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //RulePreviewInput body = null;
            //var response = instance.RulesGetPreviewData(body);
            //Assert.IsType<RulePreviewOutput>(response);
        }

        /// <summary>
        /// Test RulesImportRule
        /// </summary>
        [Fact]
        public void RulesImportRuleTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //bool? overwrite = null;
            //FilePathDetail body = null;
            //instance.RulesImportRule(overwrite, body);
        }

        /// <summary>
        /// Test RulesSave
        /// </summary>
        [Fact]
        public void RulesSaveTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string path = null;
            //bool? overwrite = null;
            //Rules body = null;
            //instance.RulesSave(path, overwrite, body);
        }

        /// <summary>
        /// Test RulesVerify
        /// </summary>
        [Fact]
        public void RulesVerifyTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //Rules body = null;
            //instance.RulesVerify(body);
        }

        /// <summary>
        /// Test RulesVerifyRule
        /// </summary>
        [Fact]
        public void RulesVerifyRuleTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string application = null;
            //string database = null;
            //Rules body = null;
            //instance.RulesVerifyRule(application, database, body);
        }
    }
}
