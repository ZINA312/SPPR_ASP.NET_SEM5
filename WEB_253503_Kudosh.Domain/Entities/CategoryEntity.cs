namespace WEB_253503_Kudosh.Domain.Entities
{
    public class CategoryEntity
    {
        public int _id;
        public string _name;
        public string _normalizedName;

        public CategoryEntity() { }
        public CategoryEntity(int id, string name, string normalizedName)
        {
            _id = id;
            _name = name;
            _normalizedName = normalizedName;
        }

        public int Id { get { return _id; } private set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string NormalizedName { get { return _normalizedName; } set { _normalizedName = value; } }
    }
}
