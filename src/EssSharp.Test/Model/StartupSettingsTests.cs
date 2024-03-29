/*
 * The REST API for Oracle Essbase enables you to automate management of Essbase resources and operations. All requests and responses are communicated over secured HTTP.
 *
 * The version of the OpenAPI document: 1.0
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using Xunit;

using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using EssSharp.Model;
using EssSharp.Client;
using System.Reflection;
using Newtonsoft.Json;

namespace EssSharp.Test.Model
{
    /// <summary>
    ///  Class for testing StartupSettings
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the model.
    /// </remarks>
    public class StartupSettingsTests : IDisposable
    {
        // TODO uncomment below to declare an instance variable for StartupSettings
        //private StartupSettings instance;

        public StartupSettingsTests()
        {
            // TODO uncomment below to create an instance of StartupSettings
            //instance = new StartupSettings();
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of StartupSettings
        /// </summary>
        [Fact]
        public void StartupSettingsInstanceTest()
        {
            // TODO uncomment below to test "IsType" StartupSettings
            //Assert.IsType<StartupSettings>(instance);
        }


        /// <summary>
        /// Test the property 'AllowUsersToStartDatabase'
        /// </summary>
        [Fact]
        public void AllowUsersToStartDatabaseTest()
        {
            // TODO unit test for the property 'AllowUsersToStartDatabase'
        }
        /// <summary>
        /// Test the property 'StartDatabaseWhenApplicationStarts'
        /// </summary>
        [Fact]
        public void StartDatabaseWhenApplicationStartsTest()
        {
            // TODO unit test for the property 'StartDatabaseWhenApplicationStarts'
        }

    }

}
