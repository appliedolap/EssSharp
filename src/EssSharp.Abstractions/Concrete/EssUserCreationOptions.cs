using System;
using System.Collections.Generic;

namespace EssSharp
{
    /// <summary />
    public class EssUserCreationOptions
    {
        public EssUserCreationOptions(string id, string password, EssServerRole role = EssServerRole.User, List<string> groups = default )
        {
            ID = id ?? 
                throw new ArgumentNullException(nameof(id), $@"The ID of a user is required to create an {nameof(EssUserCreationOptions)}.");

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
        public EssServerRole Role { get; set; }

    }
}
