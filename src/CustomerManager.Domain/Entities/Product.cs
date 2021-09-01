namespace CustomerManager.Domain.Entities
{
    public class Product : EntityBase
    {
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Brand { get; set; }
        public string Title { get; set; }
        public double ReviewScore { get; set; }

        public Product()
        {
        }

    }
}
