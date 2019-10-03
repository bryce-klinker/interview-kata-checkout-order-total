using System.Collections.Generic;
using System.Linq;

namespace Point.Of.Sale.Core
{
    public class Special
    {
        public string Code { get; }
        public int QualifyingQuantity { get; }
        public int DiscountedQuantity { get; }
        public decimal DiscountPercent { get; }
        public int Limit { get; }

        public Special(string code, int qualifyingQuantity, int discountedQuantity, decimal discountPercent, int limit = -1)
        {
            Code = code;
            QualifyingQuantity = qualifyingQuantity;
            DiscountedQuantity = discountedQuantity;
            DiscountPercent = discountPercent;
            Limit = limit;
        }

        public decimal CalculateDiscount(List<ScannedItem> scannedItems)
        {
            var quantity = scannedItems.Count(s => s.Code == Code);
            if (quantity < QualifyingQuantity)
                return 0;

            return Limit == -1 
                ? CalculateDiscountWithoutLimit(scannedItems) 
                : CalculateDiscountWithLimit(scannedItems);
        }

        private decimal CalculateDiscountWithLimit(List<ScannedItem> scannedItems)
        {
            var itemsIncludedInDiscount = scannedItems.Where(s => s.Code == Code)
                .Take(Limit)
                .ToList();

            var currentCount = 0;
            var discountedCount = 0;
            var discountAmount = 0m;
            foreach (var item in itemsIncludedInDiscount)
            {
                if (currentCount == QualifyingQuantity)
                {
                    discountAmount += item.SubTotal() * (DiscountPercent / 100);
                    discountedCount++;

                    if (discountedCount == DiscountedQuantity)
                    {
                        discountedCount = 0;
                        currentCount = 0;
                    }
                }

                currentCount++;
            }
            return discountAmount;
        }

        private decimal CalculateDiscountWithoutLimit(IEnumerable<ScannedItem> scannedItems)
        {
            var discountedItems = scannedItems.Where(s => s.Code == Code)
                .Skip(QualifyingQuantity)
                .Take(DiscountedQuantity);

            return discountedItems.Sum(i => i.SubTotal() * (DiscountPercent / 100));
        }
    }
}