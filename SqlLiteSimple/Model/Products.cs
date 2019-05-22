namespace SqlLiteSimple.Model
{
    public class Products
    {
        public Products()
        {
        }

        public int ID { get; set; }
        public string Description { get; set; }
        public string PName { get; set; }

        public Products(string _Name, int _id, string _Description)
        {
            PName = _Name;
            ID = _id; ;
            Description = _Description;
        }
    }
}