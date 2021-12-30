namespace LexiconVendingMachine
{
    public abstract class Product
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public string Unit { get; set; }
        public int Price { get; set; }
        public int InStock { get; set; }

        public virtual string Examine()
        {
            string examineItem = $"{Name}, {Size}, {Unit}, {Price}, {InStock}";

            return examineItem;
        }

        public virtual string Use()
        {
            string instructions = "No instructions available";
            return instructions;
        }
    }    
}
