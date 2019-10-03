namespace Point.Of.Sale.Core
{
    public class ScannedItem
    {
        private readonly InventoryItem _item;
        private readonly decimal _units;

        public string Code => _item.Code;

        public ScannedItem(InventoryItem item, decimal? units)
        {
            _item = item;
            _units = units.GetValueOrDefault(1);
        }

        public decimal SubTotal()
        {
            var discount = _item.Cost * _item.Markdown / 100;
            return _item.Cost * _units - discount;
        }
    }
}