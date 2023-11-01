using NorthwindAPI.Entities;

namespace NorthwindAPI.Builders
{
    public class ProductBuilder
    {
        private Product _product = new();

        public ProductBuilder() { }

        public ProductBuilder(int productId) { _product.ProductId = productId; }

        public ProductBuilder SetProductId(int productId)
        {
            _product.ProductId = productId;
            return this;
        }

        public ProductBuilder SetProductName(string productName)
        {
            _product.ProductName = productName;
            return this;
        }

        public ProductBuilder WithSupplierId(int? supplierId)
        {
            _product.SupplierId = supplierId;
            return this;
        }

        public ProductBuilder WithCategoryId(int? categoryId)
        {
            _product.CategoryId = categoryId;
            return this;
        }

        public ProductBuilder WithQuantityPerUnit(string? quantityPerUnit)
        {
            _product.QuantityPerUnit = quantityPerUnit;
            return this;
        }

        public ProductBuilder WithUnitPrice(decimal? unitPrice)
        {
            _product.UnitPrice = unitPrice;
            return this;
        }

        public ProductBuilder WithUnitsInStock(short? unitsInStock)
        {
            _product.UnitsInStock = unitsInStock;
            return this;
        }

        public ProductBuilder WithUnitsOnOrder(short? unitsOnOrder)
        {
            _product.UnitsOnOrder = unitsOnOrder;
            return this;
        }

        public ProductBuilder WithReorderLevel(short? reorderLevel)
        {
            _product.ReorderLevel = reorderLevel;
            return this;
        }

        public ProductBuilder Discontinued(bool discontinued)
        {
            _product.Discontinued = discontinued;
            return this;
        }

        public Product Build()
        {
            return _product;
        }

    }
}
