public static class ProductExceptions
{
    public class DuplicateNameException : Exception
    {
        public DuplicateNameException()
            : base("Product with provided name already exists.")
        { }
    }

    public class InvalidPriceException : Exception
    {
        public InvalidPriceException()
            : base("Price must be a non-negative integer.")
        { }
    }

    public class NoProductException : Exception
    {
        public NoProductException()
            : base("No product(s) found")
        { }
    }

    public class NoProductWithCategory : Exception
    {
         public NoProductWithCategory()
            : base("No products with given category found")
        { }
    }
}
