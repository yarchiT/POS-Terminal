namespace POSTerminal.Discount
{
    public class DiscountCardCondition
    {
        public readonly decimal Percent;
        public readonly decimal AmountFrom;
        public readonly decimal? AmountTo;

        public DiscountCardCondition(decimal percent, decimal amountFrom, decimal? amountTo = null)
        {
            Percent = percent;
            AmountFrom = amountFrom;
            AmountTo = amountTo;
        }
    }
}