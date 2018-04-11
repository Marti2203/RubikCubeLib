using System;
using System.Diagnostics;
using System.Text;

namespace RubikCube
{
    public class Face
    {
        public Element[,] elements;
        public int size;
        public Face(Color color, int size = 3)
        {
            this.size = size;
            elements = new Element[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    elements[i, j] = new Element(color);
        }
        internal void RotateClockwise()
        {
            Element temp;
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    temp = elements[i, j];
                    elements[i, j] = elements[size - 1 - i, j];
                    elements[size - 1 - i, j] = temp;
                }
        }
        internal void RotateCounterClockwise()
        {
            Element temp;
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    temp = elements[i, j];
                    elements[i, j] = elements[i, size - 1 - j];
                    elements[i, size - 1 - j] = temp;
                }
        }
        public override string ToString()
        {
            StringBuilder result = new StringBuilder(size * size * 2);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result.Append(elements[i, j].Color.ToString()[0]);
                    result.Append(' ');
                }
            result.AppendLine();
            }
            return result.ToString();
        }
    }
}
