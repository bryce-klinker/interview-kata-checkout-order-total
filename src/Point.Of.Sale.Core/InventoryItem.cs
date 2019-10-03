namespace Point.Of.Sale.Core
{
    public class InventoryItem
    {
        public string Code { get; }
        public decimal Cost { get; }
        public decimal Markdown { get; }

        public InventoryItem(string code, decimal cost, decimal markdown)
        {
            Code = code;
            Cost = cost;
            Markdown = markdown;
        }
    }
}