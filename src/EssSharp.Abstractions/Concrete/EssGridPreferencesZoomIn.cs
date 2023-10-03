using System;
using System.Collections.Generic;
using System.Text;

namespace EssSharp
{
    public enum ZoomInAncestor
    {
        UNKNOWN = 0,

        TOP = 1,

        BOTTOM = 2
    };

    public enum ZoomInMode
    {
        UNKNOWN = 0,

        CHILDREN = 1,

        DESCENDENTS = 2,

        BASE = 3
    };

    public class EssGridPreferencesZoomIn
    {
        public ZoomInAncestor Ancestor { get; set; }

        public ZoomInMode Mode { get; set; }
    }
}
