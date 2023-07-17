using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace EssSharp
{
    public enum EssUserRole
    {
        SERVICE_ADMINISTRATOR = 1,

        POWER_USER = 2,

        USER = 3
    }; 

    public class EssUserCreationOptions
    {
        public EssUserCreationOptions(string id, string password, EssUserRole role = EssUserRole.USER, List<string> groups = default )
        {
            ID = id ?? 
                throw new ArgumentNullException( $@"The ID of a user is required to create an {nameof(EssUserCreationOptions)}.", nameof(ID) );

            Password = !string.IsNullOrEmpty(password) && password.Length >= 8 ? password :
                throw new ArgumentException($@"Must set a password that is at least 8 characters to create an {nameof(EssUserCreationOptions)}.", nameof(password));

            Groups = groups;
            Role = role;
        }

        /// <summary>
        /// Gets or Sets Groups
        /// </summary>
        public List<string> Groups { get; set; } = new List<string>();

        /// <summary>
        /// Gets or Sets ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Gets or Sets Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or Sets Role
        /// </summary>
        public EssUserRole Role { get; set; }

    }
}
