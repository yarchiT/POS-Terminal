namespace POSTerminal.Tests.Extensions
{
    public static class TerminalExtensions
    {
        public static void ScanAll(this Terminal terminal, string productSequence)
        {
            foreach (var product in productSequence.ToCharArray())
                terminal.Scan(product.ToString());
        }
    }
}