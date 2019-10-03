using FluentAssertions;
using Xunit;

namespace Point.Of.Sale.Core.Tests
{
    public class PointOfSaleSystemTests
    {
        private readonly PointOfSaleSystem _pointOfSale;

        public PointOfSaleSystemTests()
        {
            _pointOfSale = new PointOfSaleSystem();
        }

        [Fact]
        public void ShouldReturnTotalForItem()
        {
            _pointOfSale.ConfigureItem("soup", 1.89m);
            
            _pointOfSale.Scan("soup");

            _pointOfSale.SubTotal().Should().Be(1.89m);
        }

        [Fact]
        public void ShouldReturnTotalForOtherItem()
        {
            _pointOfSale.ConfigureItem("ground beef", 2.89m);

            _pointOfSale.Scan("ground beef", 1.5m);

            _pointOfSale.SubTotal().Should().Be(4.34m);
        }

        [Fact]
        public void ShouldReturnTotalForMarkdown()
        {
            _pointOfSale.ConfigureItem("ground beef", 2.89m, 20);

            _pointOfSale.Scan("ground beef", 1);

            _pointOfSale.SubTotal().Should().Be(2.31m);
        }

        [Fact]
        public void ShouldReturnTotalForSpecials()
        {
            _pointOfSale.ConfigureItem("soup", 2);
            _pointOfSale.ConfigureSpecial("soup", 3, 1, 50m);

            _pointOfSale.Scan("soup");
            _pointOfSale.Scan("soup");
            _pointOfSale.Scan("soup");
            _pointOfSale.Scan("soup");

            _pointOfSale.SubTotal().Should().Be(7);
        }

        [Fact]
        public void ShouldReturnTotalForOtherKindOfSpecial()
        {
            _pointOfSale.ConfigureItem("soup", 2);
            _pointOfSale.ConfigureSpecial("soup", 3, 1);

            _pointOfSale.Scan("soup");
            _pointOfSale.Scan("soup");
            _pointOfSale.Scan("soup");
            _pointOfSale.Scan("soup");

            _pointOfSale.SubTotal().Should().Be(6);
        }

        [Fact]
        public void ShouldReturnTotalForLimitSpecial()
        {
            _pointOfSale.ConfigureItem("soup", 2);
            _pointOfSale.ConfigureSpecial("soup", 2, 1, 6);

            _pointOfSale.Scan("soup");
            _pointOfSale.Scan("soup");
            _pointOfSale.Scan("soup");
            _pointOfSale.Scan("soup");
            _pointOfSale.Scan("soup");
            _pointOfSale.Scan("soup");
            _pointOfSale.Scan("soup");
            _pointOfSale.Scan("soup");
            _pointOfSale.Scan("soup");

            _pointOfSale.SubTotal().Should().Be(14);
        }

        [Fact]
        public void ShouldReturnTotalOnceItemRemoved()
        {
            _pointOfSale.ConfigureItem("soup", 2);
            _pointOfSale.ConfigureSpecial("soup", 3, 1);

            _pointOfSale.Scan("soup");
            _pointOfSale.Scan("soup");
            _pointOfSale.Scan("soup");
            _pointOfSale.Remove("soup");

            _pointOfSale.SubTotal().Should().Be(4m);
        }
    }
}
