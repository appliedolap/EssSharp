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
    ///  Class for testing ServiceRoleProvisioningApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    public class ServiceRoleProvisioningApiTests : IDisposable
    {
        private ServiceRoleProvisioningApi instance;

        public ServiceRoleProvisioningApiTests()
        {
            instance = new ServiceRoleProvisioningApi();
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of ServiceRoleProvisioningApi
        /// </summary>
        [Fact]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsType' ServiceRoleProvisioningApi
            //Assert.IsType<ServiceRoleProvisioningApi>(instance);
        }

        /// <summary>
        /// Test ServiceRoleProvisioningDeprovision
        /// </summary>
        [Fact]
        public void ServiceRoleProvisioningDeprovisionTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string id = null;
            //bool? group = null;
            //instance.ServiceRoleProvisioningDeprovision(id, group);
        }

        /// <summary>
        /// Test ServiceRoleProvisioningGetProvision
        /// </summary>
        [Fact]
        public void ServiceRoleProvisioningGetProvisionTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string id = null;
            //bool? group = null;
            //var response = instance.ServiceRoleProvisioningGetProvision(id, group);
            //Assert.IsType<UserGroupProvisionInfo>(response);
        }

        /// <summary>
        /// Test ServiceRoleProvisioningProvision
        /// </summary>
        [Fact]
        public void ServiceRoleProvisioningProvisionTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string id = null;
            //UserGroupProvisionInfo body = null;
            //instance.ServiceRoleProvisioningProvision(id, body);
        }

        /// <summary>
        /// Test ServiceRoleProvisioningSearchProvision
        /// </summary>
        [Fact]
        public void ServiceRoleProvisioningSearchProvisionTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string id = null;
            //string role = null;
            //string filter = null;
            //int? page = null;
            //var response = instance.ServiceRoleProvisioningSearchProvision(id, role, filter, page);
            //Assert.IsType<UserGroupProvisionInfoList>(response);
        }
    }
}
