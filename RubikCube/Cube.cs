using System;
using System.Collections.Generic;
using System.Text;

namespace RubikCube
{
    public class Cube
    {
        int center = 0, left = 1, up = 2, right = 3, down = 4, opposite = 5;

        readonly Face[] faces;
        public readonly int size;
        public Cube(int size = 3)
        {
            faces = new Face[6];
            this.size = size;
            for (int i = 0; i < 6; i++)
                faces[i] = new Face((Color)i, size);
        }

        public Cube MoveRow(int rowIndex, XMovement rotation)
        {
            if (size % 2 == 1 && rowIndex == size / 2)
                throw new ArgumentException("RowIndex cannot be central if cube is uneven sized");
            int next = rotation == XMovement.Left ? left : right;
            int previous = 5 - next;

            if (rowIndex == 0)
            {
                if (rotation == XMovement.Left) faces[up].RotateClockwise();
                else faces[up].RotateCounterClockwise();
            }
            if (rowIndex == size - 1)
            {
                if (rotation == XMovement.Left) faces[up].RotateCounterClockwise();
                else faces[up].RotateClockwise();
            }

            SwapRow(faces[center],rowIndex,faces[next]);
            SwapRow(faces[opposite],rowIndex, faces[opposite]);
            SwapRow(faces[center],rowIndex, faces[opposite]);
            return this;
        }

        void SwapRow(Face current,int rowIndex,Face next)
        {
            Element[] temp = new Element[size];
            for (int i = 0; i < size; i++)
            {
                temp[i] = current.elements[rowIndex, i];
                current.elements[rowIndex, i] = next.elements[rowIndex, i];
                next.elements[rowIndex, i] = temp[i];
            }
        }

        public Cube MoveColumn(int columnIndex, YMovement rotation) => MoveColumn(faces[0], columnIndex, rotation);

        Cube MoveColumn(Face face, int columnIndex, YMovement rotation)
        {
            if (size % 2 == 1 && columnIndex == size / 2)
                throw new ArgumentException("ColumnIndex cannot be central if cube is uneven sized");
            int next = rotation == YMovement.Up ? up : down;
            int previous = 5 - next;
            if (columnIndex == 0)
            {
                if (rotation == YMovement.Down) faces[left].RotateClockwise();
                else faces[left].RotateCounterClockwise();
            }
            if (columnIndex == size - 1)
            {
                if (rotation == YMovement.Down) faces[right].RotateCounterClockwise();
                else faces[right].RotateClockwise();
            }
            Element[] temp = new Element[size];

            SwapColumn(face,columnIndex,faces[next]);
            SwapColumn(faces[opposite],columnIndex,faces[previous]);
            SwapColumn(face,columnIndex,faces[opposite]);
            return this;
        }

        void SwapColumn(Face current,int columnIndex,Face next)
        {
            Element[] temp = new Element[size];
            for (int i = 0; i < size; i++)
            {
                temp[i] = current.elements[i, columnIndex];
                current.elements[i, columnIndex] = next.elements[i, columnIndex];
                next.elements[i, columnIndex] = temp[i];
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(6 * size * 6 + 6);
            foreach (Face face in faces)
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        builder.Append(face.elements[i, j].Color);
                        builder.Append(' ');
                    }
                    builder.AppendLine();
                }
                builder.AppendLine();
            }
            return builder.ToString();
        }
    }
}
