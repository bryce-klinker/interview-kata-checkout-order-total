using System;
using System.Collections.Generic;
using System.Linq;

namespace Point.Of.Sale.Core
{
    public class PointOfSaleSystem
    {
        private readonly List<InventoryItem> _inventory = new List<InventoryItem>();
        private readonly List<ScannedItem> _scannedItems = new List<ScannedItem>();
        private readonly List<Special> _specials = new List<Special>();
        
        public void ConfigureItem(string code, decimal costPerItem, decimal markdownPercent = 0)
        {
            _inventory.Add(new InventoryItem(code, costPerItem, markdownPercent));
        }

        public void ConfigureSpecial(string code, int qualifyingQuantity, int discountedQuantity, decimal discountPercent)
        {
            _specials.Add(new Special(code, qualifyingQuantity, discountedQuantity, discountPercent));
        }
        
        public void ConfigureSpecial(string code, int qualifyingQuantity, int discountedQuantity, int limit)
        {
            _specials.Add(new Special(code, qualifyingQuantity, discountedQuantity, 100m, limit));
        }

        public void ConfigureSpecial(string code, int qualifyingQuantity, int discountedQuantity)
        {
            _specials.Add(new Special(code, qualifyingQuantity, discountedQuantity, 100m));
        }

        public void Scan(string code, decimal? weight = null)
        {
            var inventoryItem = _inventory.Single(i => i.Code == code);
            _scannedItems.Add(new ScannedItem(inventoryItem, weight));
        }

        public decimal SubTotal()
        {
            var discount = _specials.Sum(s => s.CalculateDiscount(_scannedItems));
            return Math.Round(_scannedItems.Sum(i => i.SubTotal()) - discount, 2);
        }

        public void Remove(string code)
        {
            var lastScannedItem = _scannedItems.Last(s => s.Code == code);
            _scannedItems.Remove(lastScannedItem);
        }
    }
}