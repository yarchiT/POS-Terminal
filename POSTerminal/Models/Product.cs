namespace POSTerminal.Models
{
    public class Product
    {
        public string Code { get; set; }

        public decimal UnitPrice { get; set; }

        public int? NumberForVolume { get; set; }

        public decimal? VolumePrice { get; set; }
    }
}
