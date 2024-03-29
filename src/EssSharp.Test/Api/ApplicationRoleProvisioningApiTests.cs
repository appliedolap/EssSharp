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
    ///  Class for testing ApplicationRoleProvisioningApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    public class ApplicationRoleProvisioningApiTests : IDisposable
    {
        private ApplicationRoleProvisioningApi instance;

        public ApplicationRoleProvisioningApiTests()
        {
            instance = new ApplicationRoleProvisioningApi();
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of ApplicationRoleProvisioningApi
        /// </summary>
        [Fact]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsType' ApplicationRoleProvisioningApi
            //Assert.IsType<ApplicationRoleProvisioningApi>(instance);
        }

        /// <summary>
        /// Test ApplicationRoleProvisioningDeprovision
        /// </summary>
        [Fact]
        public void ApplicationRoleProvisioningDeprovisionTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string app = null;
            //string id = null;
            //bool? group = null;
            //instance.ApplicationRoleProvisioningDeprovision(app, id, group);
        }

        /// <summary>
        /// Test ApplicationRoleProvisioningGetProvision
        /// </summary>
        [Fact]
        public void ApplicationRoleProvisioningGetProvisionTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string app = null;
            //string id = null;
            //bool? group = null;
            //bool? inherited = null;
            //var response = instance.ApplicationRoleProvisioningGetProvision(app, id, group, inherited);
            //Assert.IsType<UserGroupProvisionInfo>(response);
        }

        /// <summary>
        /// Test ApplicationRoleProvisioningImportProvision
        /// </summary>
        [Fact]
        public void ApplicationRoleProvisioningImportProvisionTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string app = null;
            //instance.ApplicationRoleProvisioningImportProvision(app);
        }

        /// <summary>
        /// Test ApplicationRoleProvisioningProvision
        /// </summary>
        [Fact]
        public void ApplicationRoleProvisioningProvisionTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string app = null;
            //string id = null;
            //UserGroupProvisionInfo body = null;
            //instance.ApplicationRoleProvisioningProvision(app, id, body);
        }

        /// <summary>
        /// Test ApplicationRoleProvisioningSearchProvision
        /// </summary>
        [Fact]
        public void ApplicationRoleProvisioningSearchProvisionTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string app = null;
            //string id = null;
            //string role = null;
            //string filter = null;
            //bool? inherited = null;
            //var response = instance.ApplicationRoleProvisioningSearchProvision(app, id, role, filter, inherited);
            //Assert.IsType<UserGroupProvisionInfoList>(response);
        }
    }
}
