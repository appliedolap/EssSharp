using System;

using EssSharp.Client;

namespace EssSharp
{
    /// <summary />
    public abstract class EssObject : IEssObject
    {
        /// <summary />
        internal EssObject() { }

        /// <summary />
        /// <param name="configuration" />
        /// <param name="client" />
        internal EssObject( Configuration configuration, ApiClient client )
        {
            Configuration = configuration;
            Client        = client;
        }

        /// <summary>
        /// The <see cref="EssSharp.Client.ApiClient"/> associated with this <see cref="IEssObject"/> implementation.
        /// </summary>
        internal virtual ApiClient Client { get; set; }

        /// <summary>
        /// The <see cref="EssSharp.Client.Configuration"/> associated with this <see cref="IEssObject"/> implementation.
        /// </summary>
        internal virtual Configuration Configuration { get; set; }

        /// <inheritdoc />
        public virtual string Name => throw new NotImplementedException();

        /// <inheritdoc />
        public virtual EssType Type => throw new NotImplementedException();
    }
}
