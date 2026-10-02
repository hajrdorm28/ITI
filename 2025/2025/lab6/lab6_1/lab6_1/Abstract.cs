using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace lab6_1
{
    public abstract class Shape3D
    {
        public abstract double GetVolume();
        public abstract double GetSurfaceArea();
    }

    public class Sphere : Shape3D
    {
        private double radius;
        public Sphere(double radius) => this.radius = radius;
        public override double GetVolume() => (4.0 / 3.0) * Math.PI * Math.Pow(radius, 3);
        public override double GetSurfaceArea() => 4 * Math.PI * Math.Pow(radius, 2);
    }
    public class Cube : Shape3D
    {
        private double side;
        public Cube(double side) => this.side = side;
        public override double GetVolume() => Math.Pow(side, 3.0);
        public override double GetSurfaceArea() => 6.0 * Math.Pow(side, 2);
    }
    public class Cylinder : Shape3D
    {
        private double radius, height;
        public Cylinder(double radius, double height)
        {
            this.radius = radius;
            this.height = height;
        }
        public override double GetVolume() => Math.PI * radius * radius * height;
        public override double GetSurfaceArea() => 2 * Math.PI * radius * (radius + height);
    }
}
