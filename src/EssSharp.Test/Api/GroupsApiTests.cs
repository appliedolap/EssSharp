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
    ///  Class for testing GroupsApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    public class GroupsApiTests : IDisposable
    {
        private GroupsApi instance;

        public GroupsApiTests()
        {
            instance = new GroupsApi();
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of GroupsApi
        /// </summary>
        [Fact]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsType' GroupsApi
            //Assert.IsType<GroupsApi>(instance);
        }

        /// <summary>
        /// Test GroupsAdd
        /// </summary>
        [Fact]
        public void GroupsAddTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //GroupBean body = null;
            //var response = instance.GroupsAdd(body);
            //Assert.IsType<GroupBean>(response);
        }

        /// <summary>
        /// Test GroupsAddGroupMembersToGroup
        /// </summary>
        [Fact]
        public void GroupsAddGroupMembersToGroupTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string id = null;
            //List<string> body = null;
            //var response = instance.GroupsAddGroupMembersToGroup(id, body);
            //Assert.IsType<Groups>(response);
        }

        /// <summary>
        /// Test GroupsAddUserMembersToGroup
        /// </summary>
        [Fact]
        public void GroupsAddUserMembersToGroupTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string id = null;
            //List<string> body = null;
            //var response = instance.GroupsAddUserMembersToGroup(id, body);
            //Assert.IsType<Users>(response);
        }

        /// <summary>
        /// Test GroupsDelete
        /// </summary>
        [Fact]
        public void GroupsDeleteTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string id = null;
            //instance.GroupsDelete(id);
        }

        /// <summary>
        /// Test GroupsEdit
        /// </summary>
        [Fact]
        public void GroupsEditTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string id = null;
            //GroupBean body = null;
            //var response = instance.GroupsEdit(id, body);
            //Assert.IsType<GroupBean>(response);
        }

        /// <summary>
        /// Test GroupsGet
        /// </summary>
        [Fact]
        public void GroupsGetTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string id = null;
            //var response = instance.GroupsGet(id);
            //Assert.IsType<GroupBean>(response);
        }

        /// <summary>
        /// Test GroupsGetGroupMembersOfGroup
        /// </summary>
        [Fact]
        public void GroupsGetGroupMembersOfGroupTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string id = null;
            //var response = instance.GroupsGetGroupMembersOfGroup(id);
            //Assert.IsType<Groups>(response);
        }

        /// <summary>
        /// Test GroupsGetMembers
        /// </summary>
        [Fact]
        public void GroupsGetMembersTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string id = null;
            //var response = instance.GroupsGetMembers(id);
            //Assert.IsType<UserBean>(response);
        }

        /// <summary>
        /// Test GroupsGetUserMembersOfGroup
        /// </summary>
        [Fact]
        public void GroupsGetUserMembersOfGroupTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string id = null;
            //var response = instance.GroupsGetUserMembersOfGroup(id);
            //Assert.IsType<Users>(response);
        }

        /// <summary>
        /// Test GroupsRemoveGroupMembersFromGroup
        /// </summary>
        [Fact]
        public void GroupsRemoveGroupMembersFromGroupTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string id = null;
            //List<string> body = null;
            //var response = instance.GroupsRemoveGroupMembersFromGroup(id, body);
            //Assert.IsType<UserBean>(response);
        }

        /// <summary>
        /// Test GroupsRemoveUserMembersFromGroup
        /// </summary>
        [Fact]
        public void GroupsRemoveUserMembersFromGroupTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string id = null;
            //List<string> body = null;
            //var response = instance.GroupsRemoveUserMembersFromGroup(id, body);
            //Assert.IsType<UserBean>(response);
        }

        /// <summary>
        /// Test GroupsSearch
        /// </summary>
        [Fact]
        public void GroupsSearchTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string filter = null;
            //int? limit = null;
            //string expand = null;
            //var response = instance.GroupsSearch(filter, limit, expand);
            //Assert.IsType<Groups>(response);
        }

        /// <summary>
        /// Test UsersDeleteGroups
        /// </summary>
        [Fact]
        public void UsersDeleteGroupsTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //var response = instance.UsersDeleteGroups();
            //Assert.IsType<Object>(response);
        }
    }
}
