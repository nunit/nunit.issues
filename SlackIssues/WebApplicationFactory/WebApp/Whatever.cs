namespace WebApp
{
    public class Whatever
    {
        public int Add(int a, int b)
        {
            Console.WriteLine("Inside Add");
            throw new ArgumentException("Some stuff happened");
            return a + b;
        }
    }
}
