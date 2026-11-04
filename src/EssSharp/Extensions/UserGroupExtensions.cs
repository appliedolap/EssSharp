using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using EssSharp.Model;

namespace EssSharp
{
    /// <summary>
    /// Conversions between the users and groups API models and EssSharp's types, and between EssSharp's types and the CSV
    /// text that the server's import, export and delete operations take and answer with.
    /// </summary>
    internal static class UserGroupExtensions
    {
        #region Links

        /// <summary>
        /// Returns the given API links as <see cref="EssLink"/> objects.
        /// </summary>
        /// <param name="links">The links the server gave.</param>
        internal static List<EssLink> ToEssLinks( this List<Link> links ) =>
            links?
                .Where(link => link is not null)
                .Select(link => new EssLink(link.Rel, link.Href, link.Method))
                .ToList() ?? new List<EssLink>();

        #endregion

        #region Bulk Operations

        /// <summary>
        /// Returns the given values as a text file of lines, as the server's delete operations take them.
        /// </summary>
        /// <param name="values">The user IDs or group names.</param>
        /// <param name="description">What each value is, such as "user ID", for the error message.</param>
        /// <exception cref="ArgumentException">A value is empty or has a line break, which the file could not hold.</exception>
        internal static MemoryStream ToLineFile( this IEnumerable<string> values, string description )
        {
            var lines = values?.ToList() ?? throw new ArgumentNullException(nameof(values), $"The {description}s to delete are required.");

            foreach ( var line in lines )
            {
                if ( string.IsNullOrWhiteSpace(line) || line.IndexOfAny(new[] { '\r', '\n' }) >= 0 )
                    throw new ArgumentException($@"A {description} cannot be empty or contain a line break. Given: ""{line}"".", nameof(values));
            }

            return new MemoryStream(new UTF8Encoding(false).GetBytes(string.Concat(lines.Select(line => line + "\n"))));
        }

        /// <summary>
        /// Returns what the server reported, as <see cref="EssBulkResult"/> objects, for each line of the CSV text it answered
        /// an import or a delete with.
        /// </summary>
        /// <param name="content">The CSV text, as the bytes the server sent.</param>
        /// <param name="hasStatus">
        /// True for an import, whose lines are <c>id,Info|Error,message</c>; false for a delete, whose lines are <c>id,message</c>,
        /// where the message is "Deleted" for what was deleted.
        /// </param>
        internal static List<EssBulkResult> ToEssBulkResults( this byte[] content, bool hasStatus )
        {
            var results = new List<EssBulkResult>();

            if ( content is not { Length: > 0 } )
                return results;

            foreach ( var line in Encoding.UTF8.GetString(content).TrimStart('﻿').Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries) )
            {
                if ( string.IsNullOrWhiteSpace(line) )
                    continue;

                if ( hasStatus )
                {
                    var parts  = line.Split(new[] { ',' }, 3);
                    var status = parts.Length > 1 ? parts[1].Trim() : string.Empty;

                    results.Add(new EssBulkResult(parts[0].Trim(), string.Equals(status, "Info", StringComparison.OrdinalIgnoreCase), parts.Length > 2 ? parts[2].Trim() : status));
                }
                else
                {
                    var parts   = line.Split(new[] { ',' }, 2);
                    var message = parts.Length > 1 ? parts[1].Trim() : string.Empty;

                    results.Add(new EssBulkResult(parts[0].Trim(), string.Equals(message, "Deleted", StringComparison.OrdinalIgnoreCase), message));
                }
            }

            return results;
        }

        /// <summary>
        /// Returns the given list as long as the given limit allows. A null or negative limit allows the whole list.
        /// </summary>
        /// <typeparam name="T">The type of the items.</typeparam>
        /// <param name="items">The items.</param>
        /// <param name="limit">The most items to return.</param>
        internal static List<T> Limit<T>( this List<T> items, int? limit ) =>
            limit is >= 0 && items.Count > limit.Value ? items.Take(limit.Value).ToList() : items;

        #endregion

        #region Provisioning Reports

        /// <summary>
        /// Returns the given roles, filters or scripts as <see cref="EssProvisionedItem"/> objects.
        /// </summary>
        /// <param name="items">The API list of provisioned items.</param>
        internal static List<EssProvisionedItem> ToEssProvisionedItems( this ProvisionReportItemList items ) =>
            items?.Items?
                .Where(item => item is not null)
                .Select(item => new EssProvisionedItem(item.Name, item.InheritedFromGroups))
                .ToList() ?? new List<EssProvisionedItem>();

        /// <summary>
        /// Returns the given filters or scripts by cube as <see cref="EssDatabaseProvisioning"/> objects.
        /// </summary>
        /// <param name="databases">The API list of provisioned items by cube.</param>
        internal static List<EssDatabaseProvisioning> ToEssDatabaseProvisionings( this DatabaseProvisionReportItemList databases ) =>
            databases?.Items?
                .Where(database => database is not null)
                .Select(database => new EssDatabaseProvisioning(
                    database.Database,
                    database.Items?
                        .Where(item => item is not null)
                        .Select(item => new EssProvisionedItem(item.Name, item.InheritedFromGroups))
                        .ToList()))
                .ToList() ?? new List<EssDatabaseProvisioning>();

        /// <summary>
        /// Returns the given application report as an <see cref="EssApplicationProvisioning"/>.
        /// </summary>
        /// <param name="application">The API report of one application.</param>
        internal static EssApplicationProvisioning ToEssApplicationProvisioning( this ApplicationProvisionReportItem application ) =>
            application is null ? null : new EssApplicationProvisioning(
                application.Name,
                application.Roles.ToEssProvisionedItems(),
                application.Filters.ToEssDatabaseProvisionings(),
                application.Scripts.ToEssDatabaseProvisionings());

        /// <summary>
        /// Returns the given application reports as <see cref="EssApplicationProvisioning"/> objects.
        /// </summary>
        /// <param name="applications">The API list of reports of applications.</param>
        internal static List<EssApplicationProvisioning> ToEssApplicationProvisionings( this ApplicationProvisionReportItemList applications ) =>
            applications?.Items?
                .Where(application => application is not null)
                .Select(application => application.ToEssApplicationProvisioning())
                .ToList() ?? new List<EssApplicationProvisioning>();

        /// <summary>
        /// Returns the given full report as an <see cref="EssProvisioningReport"/>.
        /// </summary>
        /// <param name="report">The API full report.</param>
        internal static EssProvisioningReport ToEssProvisioningReport( this MainProvisionReport report ) =>
            new EssProvisioningReport(report?.Service?.Roles.ToEssProvisionedItems(), report?.Applications.ToEssApplicationProvisionings());

        #endregion
    }
}
