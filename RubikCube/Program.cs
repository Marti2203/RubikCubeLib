using System;
using System.Collections.Generic;
using System.Linq;
namespace RubikCube
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            
        }
    }

    class Cube
    {
        Face[] faces;
        public readonly int size;
        public Dictionary<Face, (Face Up,Face Down,Face Right, Face Left)> connections = 
            new Dictionary<Face, (Face Up, Face Down, Face Right, Face Left)>();
        public Cube(int size = 3)
        {
            faces = new Face[6];
            this.size = size;
            for (int i = 0; i < 6;i++)
                faces[i]=new Face((Color)i,size);
            CreateConnections();
        }
        public void CreateConnections()
        {
            Connection(0, 2, 4, 1, 5);
            Connection(1, 2, 4, 3, 0);
            Connection(2, 3, 0, 1, 5);
            Connection(3, 2, 4, 5, 1);
            Connection(4, 0, 3, 1, 5);
            Connection(5, 2, 4, 0, 3);
        }
        void Connection(int starter,int up, int down, int right, int left)
        => connections.Add(faces[starter],(faces[up],faces[down],faces[right],faces[left]));

        Face Up(Face face) => connections[face].Up;
        Face Down(Face face) => connections[face].Down;
        Face Left(Face face) => connections[face].Left;
        Face Right(Face face) => connections[face].Right;


        void MoveRow(Face face,int rowIndex,XMovement rotation)
        {
            Func<Face, Face> next = rotation == XMovement.Left ? (Func<Face, Face>) Left  : Right;
            if (rowIndex == 0)
            {
                if (rotation == XMovement.Left) connections[face].Up.RotateClockwise();
                else connections[face].Up.RotateCounterClockwise();
            }
            if(rowIndex == size - 1)
            {
                if (rotation == XMovement.Left) connections[face].Up.RotateCounterClockwise();
                else connections[face].Up.RotateClockwise();
            }

            Element[] temp = new Element[size];
            void Rotate(Face current)
			{
                for (int i = 0; i < size; i++)
                {
				temp[i] = current.elements[rowIndex, i];
				current.elements[rowIndex, i] = next(current).elements[rowIndex, i];
				next(current).elements[rowIndex, i] = temp[i];
                }
			}
            Rotate(face);
            Rotate(next(next(face)));
            Rotate(face);
        }
        void MoveColumn(Face face,int columnIndex, YMovement rotation)
        {
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
            void Rotate(Face current)
            {
                for (int i = 0; i < size; i++)
                {
                    temp[i] = current.elements[i, columnIndex];
                    current.elements[i, columnIndex] = next(current).elements[i, columnIndex];
                    next(current).elements[i, columnIndex] = temp[i];
                }
            }
            Rotate(face);
            Rotate(next(next(face)));
            Rotate(face);

        }
    }
    enum XMovement {Left, Right}
    enum YMovement {Up, Down}
    class Face
    {
        public Element[,] elements;
        public int size;
        public Face(Color color,int size=3)
        {
            this.size = size;
            elements = new Element[size, size];
            for (int i = 0; i < size;i++)
                for (int j = 0; j < size;j++)
                    elements[i,j] = new Element(color);
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
    }
    struct Element
    {
        Color color;
        public Element(Color color)
        {
            this.color = color;
        }
    }
    enum Color{ Red,White,Blue,Green,Orange,Yellow}
}
