using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Labs
{
    public class Triangle:Shape
    {
		private double side;
		private double height;
        public Triangle(double s,double h)
        {
			side = s;
			height = h;
			xPos = DataModel.getNewXPos();
			yPos = DataModel.getNewYPos();
		}
		public override double getArea()
		{
			return (side * height) / 2;
		}

		public override double getPerimeter()
		{
			return 3 * side;
		}

		public override void printData()
		{
			Console.WriteLine();
			Console.WriteLine("My type: " + this.GetType());
			Console.WriteLine("Side of triangle = " + side);
			Console.WriteLine("Height of triangle = " + height);
			Console.WriteLine("X position = " + xPos);
			Console.WriteLine("Y position = " + yPos);
		}
	}
}
