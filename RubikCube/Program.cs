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
            Console.WriteLine(test);
            test.MoveColumn(0,YMovement.Up);
            Console.WriteLine(test);
            //test.MoveColumn(0,YMovement.Up)
            //    .MoveRow(2,XMovement.Right);
            //Console.WriteLine(test);
        }
    }
}
