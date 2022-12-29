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
    ///  Class for testing ScriptsApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    public class ScriptsApiTests : IDisposable
    {
        private ScriptsApi instance;

        public ScriptsApiTests()
        {
            instance = new ScriptsApi();
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of ScriptsApi
        /// </summary>
        [Fact]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsType' ScriptsApi
            //Assert.IsType<ScriptsApi>(instance);
        }

        /// <summary>
        /// Test ScriptsAddScriptPermission
        /// </summary>
        [Fact]
        public void ScriptsAddScriptPermissionTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //string databaseName = null;
            //string scriptName = null;
            //UserGroupProvisionInfo body = null;
            //var response = instance.ScriptsAddScriptPermission(applicationName, databaseName, scriptName, body);
            //Assert.IsType<UserGroupProvisionInfo>(response);
        }

        /// <summary>
        /// Test ScriptsCopyScript
        /// </summary>
        [Fact]
        public void ScriptsCopyScriptTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //string databaseName = null;
            //ScriptCopy body = null;
            //var response = instance.ScriptsCopyScript(applicationName, databaseName, body);
            //Assert.IsType<Script>(response);
        }

        /// <summary>
        /// Test ScriptsCreateScript
        /// </summary>
        [Fact]
        public void ScriptsCreateScriptTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //string databaseName = null;
            //Script body = null;
            //string file = null;
            //var response = instance.ScriptsCreateScript(applicationName, databaseName, body, file);
            //Assert.IsType<Script>(response);
        }

        /// <summary>
        /// Test ScriptsDeleteScript
        /// </summary>
        [Fact]
        public void ScriptsDeleteScriptTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //string databaseName = null;
            //string scriptName = null;
            //string file = null;
            //instance.ScriptsDeleteScript(applicationName, databaseName, scriptName, file);
        }

        /// <summary>
        /// Test ScriptsEditScript
        /// </summary>
        [Fact]
        public void ScriptsEditScriptTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //string databaseName = null;
            //string scriptName = null;
            //Script body = null;
            //string file = null;
            //var response = instance.ScriptsEditScript(applicationName, databaseName, scriptName, body, file);
            //Assert.IsType<Script>(response);
        }

        /// <summary>
        /// Test ScriptsGetRTSVsForScripts
        /// </summary>
        [Fact]
        public void ScriptsGetRTSVsForScriptsTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //string databaseName = null;
            //string scriptName = null;
            //var response = instance.ScriptsGetRTSVsForScripts(applicationName, databaseName, scriptName);
            //Assert.IsType<List<RTSVList>>(response);
        }

        /// <summary>
        /// Test ScriptsGetScript
        /// </summary>
        [Fact]
        public void ScriptsGetScriptTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //string databaseName = null;
            //string scriptName = null;
            //string file = null;
            //var response = instance.ScriptsGetScript(applicationName, databaseName, scriptName, file);
            //Assert.IsType<Script>(response);
        }

        /// <summary>
        /// Test ScriptsGetScriptContent
        /// </summary>
        [Fact]
        public void ScriptsGetScriptContentTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //string databaseName = null;
            //string scriptName = null;
            //string file = null;
            //instance.ScriptsGetScriptContent(applicationName, databaseName, scriptName, file);
        }

        /// <summary>
        /// Test ScriptsGetScriptPermissions
        /// </summary>
        [Fact]
        public void ScriptsGetScriptPermissionsTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //string databaseName = null;
            //string scriptName = null;
            //var response = instance.ScriptsGetScriptPermissions(applicationName, databaseName, scriptName);
            //Assert.IsType<List<UserGroupProvisionInfoList>>(response);
        }

        /// <summary>
        /// Test ScriptsListScripts
        /// </summary>
        [Fact]
        public void ScriptsListScriptsTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //string databaseName = null;
            //string file = null;
            //var response = instance.ScriptsListScripts(applicationName, databaseName, file);
            //Assert.IsType<ScriptList>(response);
        }

        /// <summary>
        /// Test ScriptsRemoveScriptPermission
        /// </summary>
        [Fact]
        public void ScriptsRemoveScriptPermissionTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //string databaseName = null;
            //string scriptName = null;
            //string userGroupId = null;
            //bool group = null;
            //instance.ScriptsRemoveScriptPermission(applicationName, databaseName, scriptName, userGroupId, group);
        }

        /// <summary>
        /// Test ScriptsRenameScript
        /// </summary>
        [Fact]
        public void ScriptsRenameScriptTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //string databaseName = null;
            //ScriptCopy body = null;
            //var response = instance.ScriptsRenameScript(applicationName, databaseName, body);
            //Assert.IsType<Script>(response);
        }

        /// <summary>
        /// Test ScriptsValidateScript
        /// </summary>
        [Fact]
        public void ScriptsValidateScriptTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string applicationName = null;
            //string databaseName = null;
            //Script body = null;
            //string file = null;
            //instance.ScriptsValidateScript(applicationName, databaseName, body, file);
        }
    }
}
