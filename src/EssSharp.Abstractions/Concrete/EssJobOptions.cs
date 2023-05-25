using System.ComponentModel;

namespace EssSharp
{
    /// <summary />
    public class EssJobOptions
    {
        /// <summary />
        public EssJobOptions( EssJobType jobType )
        {
            JobType = jobType;
        }

        /// <summary />
        public string ApplicationName { get; set; }

        /// <summary />
        public string CubeName { get; set; }

        /// <summary />
        public EssJobType JobType { get; private set; } = EssJobType.Unknown;
    }
}
