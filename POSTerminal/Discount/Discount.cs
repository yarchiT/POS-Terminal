namespace POSTerminal.Discount
{
    public abstract class Discount
    {
        public abstract decimal GetTotalPrice(int itemsQuantity, decimal unitPrice);
    }
}