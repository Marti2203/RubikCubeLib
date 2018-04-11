using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RubikCube
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Cube test = new Cube(); 
            test.MoveRow(0,XMovement.Left);
            Console.WriteLine(test);
        }
    }
}
