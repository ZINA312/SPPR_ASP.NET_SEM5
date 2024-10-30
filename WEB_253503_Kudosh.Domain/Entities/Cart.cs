
namespace WEB_253503_Kudosh.Domain.Entities
{
    public class Cart
    {
        public Dictionary<int, CartItem> Items { get; set; } = new();

        public virtual void AddToCart(TelescopeEntity telescope)
        {
            int telescopeId = telescope.Id;

            if (Items.TryGetValue(telescopeId, out CartItem? value))
            {
                value.Quantity += 1;
            }
            else
            {
                var cartItem = new CartItem
                {
                    Item = telescope,
                    Quantity = 1
                };
                Items.Add(telescopeId, cartItem);
            }
        }

        public virtual void RemoveItems(int id)
        {
            Items.Remove(id);
        }

        public virtual void ClearAll()
        {
            Items.Clear();
        }

        public int Count { get => Items.Sum(item => item.Value.Quantity); }

        public double TotalPrice { get => Items?.Sum(item => item.Value.Item.Price * item.Value.Quantity) ?? 0.0; }
    }
}
