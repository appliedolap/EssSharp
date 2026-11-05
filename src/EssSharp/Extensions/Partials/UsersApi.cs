using System.IO;
using System.Threading;
using System.Threading.Tasks;

using EssSharp.Client;

namespace EssSharp.Api
{
    /// <summary />
    public partial class UsersApi
    {
        /// <summary>
        /// Delete Users in a File &lt;p&gt;Deletes the users whose IDs are listed in the file, one to a line. Requires service administrator role.&lt;/p&gt;
        /// Overloads the generated <c>UsersDeleteUsersWithHttpInfoAsync</c>, which sends no file because the specification documents none.
        /// </summary>
        /// <exception cref="EssSharp.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="file">The text file of user IDs, one to a line.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (a CSV text of <c>id,result</c> lines, as the bytes the server sent)</returns>
        public Task<ApiResponse<byte[]>> UsersDeleteUsersWithHttpInfoAsync( Stream file, int operationIndex = 0, CancellationToken cancellationToken = default ) =>
            UserGroupFileRequests.PostAsync(AsynchronousClient, Configuration, ExceptionFactory, "/users/actions/delete", "UsersApi.UsersDeleteUsers", file, operationIndex, cancellationToken);

        /// <summary>
        /// Import Users &lt;p&gt;Adds or edits the users in a CSV file with the columns <c>id,name,email,password,role,groups</c>.&lt;/p&gt;
        /// The generated <c>UsersAddWithHttpInfoAsync</c> can send only JSON.
        /// </summary>
        /// <exception cref="EssSharp.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="file">The CSV file.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (a CSV text of <c>id,Info|Error,message</c> lines, as the bytes the server sent)</returns>
        public Task<ApiResponse<byte[]>> UsersImportWithHttpInfoAsync( Stream file, int operationIndex = 0, CancellationToken cancellationToken = default ) =>
            UserGroupFileRequests.PostAsync(AsynchronousClient, Configuration, ExceptionFactory, "/users", "UsersApi.UsersAdd", file, operationIndex, cancellationToken);

        /// <summary>
        /// Export Users &lt;p&gt;Exports all the users as a CSV file with the columns <c>id,name,email,password,role,groups</c>.&lt;/p&gt;
        /// The generated <c>UsersSearchWithHttpInfoAsync</c> can accept only JSON. Essbase applies the <c>filter</c> to the JSON form alone, so there is none here.
        /// </summary>
        /// <exception cref="EssSharp.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (the CSV file, as the bytes the server sent)</returns>
        public Task<ApiResponse<byte[]>> UsersExportWithHttpInfoAsync( int operationIndex = 0, CancellationToken cancellationToken = default ) =>
            UserGroupFileRequests.GetAsync(AsynchronousClient, Configuration, ExceptionFactory, "/users", "UsersApi.UsersSearch", operationIndex, cancellationToken);
    }
}
