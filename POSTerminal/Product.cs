namespace POSTerminal
{
    public class Product
    {
        public readonly string Code;
        public readonly decimal Price;

        public Product(string code, decimal price)
        {
            Code = code;
            Price = price;
        }
    }
}