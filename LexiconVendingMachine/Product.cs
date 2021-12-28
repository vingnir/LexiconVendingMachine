namespace LexiconVendingMachine
{
    public abstract class Product
    {
        public string Name { get; set; }       
        public int Size { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }
       /* public Product(string name, int size, string unit, decimal price  )
        {
            this.Name = name;
            this.Size = size;
            this.Unit = unit;
            this.Price = price;
        }
       */
    }
}
