namespace LexiconVendingMachine
{
    public abstract class Product
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }

        public virtual string Examine()
        {
            string examineItem = $"{Name} {Size} {Unit} {Price}";

            return examineItem;
        }

        public virtual string Use()
        {
            string instructions = "No instructions available";
            return instructions;
        }

    }
    
}
