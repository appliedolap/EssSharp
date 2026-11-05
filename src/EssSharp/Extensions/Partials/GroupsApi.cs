using System.IO;
using System.Threading;
using System.Threading.Tasks;

using EssSharp.Client;

namespace EssSharp.Api
{
    /// <summary />
    public partial class GroupsApi
    {
        /// <summary>
        /// Delete Groups in a File &lt;p&gt;Deletes the groups whose names are listed in the file, one to a line.&lt;/p&gt;
        /// Overloads the generated <c>UsersDeleteGroupsWithHttpInfoAsync</c>, which sends no file because the specification documents none.
        /// </summary>
        /// <exception cref="EssSharp.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="file">The text file of group names, one to a line.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (a CSV text of <c>name,result</c> lines, as the bytes the server sent)</returns>
        public Task<ApiResponse<byte[]>> UsersDeleteGroupsWithHttpInfoAsync( Stream file, int operationIndex = 0, CancellationToken cancellationToken = default ) =>
            UserGroupFileRequests.PostAsync(AsynchronousClient, Configuration, ExceptionFactory, "/groups/actions/delete", "GroupsApi.UsersDeleteGroups", file, operationIndex, cancellationToken);

        /// <summary>
        /// Import Groups &lt;p&gt;Adds or edits the groups in a CSV file with the columns <c>name,description,role,groups</c>.&lt;/p&gt;
        /// The generated <c>GroupsAddWithHttpInfoAsync</c> can send only JSON.
        /// </summary>
        /// <exception cref="EssSharp.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="file">The CSV file.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (a CSV text of <c>name,Info|Error,message</c> lines, as the bytes the server sent)</returns>
        public Task<ApiResponse<byte[]>> GroupsImportWithHttpInfoAsync( Stream file, int operationIndex = 0, CancellationToken cancellationToken = default ) =>
            UserGroupFileRequests.PostAsync(AsynchronousClient, Configuration, ExceptionFactory, "/groups", "GroupsApi.GroupsAdd", file, operationIndex, cancellationToken);

        /// <summary>
        /// Export Groups &lt;p&gt;Exports all the groups as a CSV file with the columns <c>name,description,role,groups</c>.&lt;/p&gt;
        /// The generated <c>GroupsSearchWithHttpInfoAsync</c> can accept only JSON. Essbase applies the <c>filter</c> to the JSON form alone, so there is none here.
        /// </summary>
        /// <exception cref="EssSharp.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (the CSV file, as the bytes the server sent)</returns>
        public Task<ApiResponse<byte[]>> GroupsExportWithHttpInfoAsync( int operationIndex = 0, CancellationToken cancellationToken = default ) =>
            UserGroupFileRequests.GetAsync(AsynchronousClient, Configuration, ExceptionFactory, "/groups", "GroupsApi.GroupsSearch", operationIndex, cancellationToken);
    }
}
