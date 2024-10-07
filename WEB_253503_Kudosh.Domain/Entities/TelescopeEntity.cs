namespace WEB_253503_Kudosh.Domain.Entities
{
    public class TelescopeEntity
    {
        public int _id;
        public string _name;
        public string _description;
        public CategoryEntity _category;
        public double _price;
        public string _imgPath;
        public string _mimeType;

        public TelescopeEntity()
        {
        }
        public TelescopeEntity(int id, string name, string description, CategoryEntity category, double price, string imgPath)
        {
            _id = id;
            _name = name;
            _description = description;
            _category = category;
            _price = price;
            _imgPath = imgPath;
        }

        public int Id { get { return _id; } private set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string Description { get { return _description; } set { _description = value; } }
        public CategoryEntity Category { get { return _category; } set { _category = value; } }
        public double Price { get { return _price; } set { _price = value; } }
        public string ImagePath { get { return _imgPath; } set { _imgPath = value; } }
        public string MimeType { get { return _mimeType; } set { _mimeType = value; } }

    }
}
