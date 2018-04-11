using System;
using System.Diagnostics;

namespace RubikCube
{
    [DebuggerDisplay("{Color}")]
    public struct Element
    {
        public Color Color;
        public Element(Color color)
        {
            Color = color;
        }
    }
}
