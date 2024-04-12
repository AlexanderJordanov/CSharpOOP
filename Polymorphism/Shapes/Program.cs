namespace Shapes
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Rectangle rec = new Rectangle(2.5, 2.6);
            Console.WriteLine(rec.CalculateArea());
            Console.WriteLine(rec.CalculatePerimeter());
            Console.WriteLine(rec.Draw());

            Circle cir = new Circle(2.5);
            Console.WriteLine(cir.CalculateArea());
            Console.WriteLine(cir.CalculatePerimeter());
            Console.WriteLine(cir.Draw());
        }
    }
}