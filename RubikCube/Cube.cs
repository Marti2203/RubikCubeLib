using System;
using System.Collections.Generic;
using System.Text;

namespace RubikCube
{
    public class Cube
    {
        readonly Face[] faces;
        public readonly int size;
        Dictionary<Face, (Face Up, Face Down, Face Right, Face Left)> connections =
            new Dictionary<Face, (Face Up, Face Down, Face Right, Face Left)>();
        public Cube(int size = 3)
        {
            faces = new Face[6];
            this.size = size;
            for (int i = 0; i < 6; i++)
                faces[i] = new Face((Color)i, size);
            CreateConnections();
        }
        void CreateConnections()
        {
            Connection(0, 2, 4, 1, 5);
            Connection(1, 2, 4, 3, 0);
            Connection(2, 3, 0, 1, 5);
            Connection(3, 2, 4, 5, 1);
            Connection(4, 0, 3, 1, 5);
            Connection(5, 2, 4, 0, 3);
        }
        void Connection(int starter, int up, int down, int right, int left)
        => connections.Add(faces[starter], (faces[up], faces[down], faces[right], faces[left]));

        Face Up(Face face) => connections[face].Up;
        Face Down(Face face) => connections[face].Down;
        Face Left(Face face) => connections[face].Left;
        Face Right(Face face) => connections[face].Right;

        public void MoveRow(int rowIndex, XMovement rotation) => MoveRow(faces[0], rowIndex, rotation);

        void MoveRow(Face face, int rowIndex, XMovement rotation)
        {
            if (size % 2 == 1 && rowIndex == size / 2)
                throw new ArgumentException("RowIndex cannot be central if cube is uneven sized");
            Func<Face, Face> next = rotation == XMovement.Left ? (Func<Face, Face>)Left : Right;
            if (rowIndex == 0)
            {
                if (rotation == XMovement.Left) connections[face].Up.RotateClockwise();
                else connections[face].Up.RotateCounterClockwise();
            }
            if (rowIndex == size - 1)
            {
                if (rotation == XMovement.Left) connections[face].Up.RotateCounterClockwise();
                else connections[face].Up.RotateClockwise();
            }

            SwapRow(face,rowIndex,next(face));
            SwapRow(next(next(face)),rowIndex, next(next(next((face)))));
            SwapRow(face,rowIndex, next(next(face)));
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

        public void MoveColumn(int columnIndex, YMovement rotation) => MoveColumn(faces[0], columnIndex, rotation);

        void MoveColumn(Face face, int columnIndex, YMovement rotation)
        {
            if (size % 2 == 1 && columnIndex == size / 2)
                throw new ArgumentException("ColumnIndex cannot be central if cube is uneven sized");
            Func<Face, Face> next = rotation == YMovement.Up ? (Func<Face, Face>)Up : Down;
            if (columnIndex == 0)
            {
                if (rotation == YMovement.Down) connections[face].Left.RotateClockwise();
                else connections[face].Left.RotateCounterClockwise();
            }
            if (columnIndex == size - 1)
            {
                if (rotation == YMovement.Down) connections[face].Right.RotateCounterClockwise();
                else connections[face].Right.RotateClockwise();
            }
            Element[] temp = new Element[size];

            SwapColumn(face,columnIndex,next(face));
            SwapColumn(next(next(face)),columnIndex,next(next(next(face))));
            SwapColumn(face,columnIndex,next(next(face)));
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
