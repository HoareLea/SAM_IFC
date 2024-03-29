﻿using System.ComponentModel;

namespace SAM.Geometry.IFC
{
    [Description("Ifc Default Context Identifier")]
    public enum IfcDefaultContextIdentifier
    {
        [Description("Undefined")] Undefined,
        [Description("Body")] Body,
        [Description("Axis")] Axis,
        [Description("Box")] Box,
        [Description("FootPrint")] FootPrint,
    }
}