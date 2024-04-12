namespace Shapes
{
    public class Rectangle : Shape
    {
        public Rectangle(double height, double width)
        {
            Width = width;
            Length = height;
        }
        private double length;

        public double Length
        {
            get { return length; }
            private set { length = value; }
        }
        private double width;

        public double Width
        {
            get { return width; }
            private set { width = value; }
        }


        public override double CalculateArea()
        {
            return Length * Width;
        }

        public override double CalculatePerimeter()
        {
            return 2 * ( Length + Width );
        }
        public override string Draw()
        {
            return $"Drawing {this.GetType().Name}";
        }
    }
}
