namespace LexiconVendingMachine
{
    public abstract class Product
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }

        public abstract string Examine();

        public abstract string Use();

    }
    
}
