namespace AhmedStore.Models
{

public class CartProduct
{
    public int CartId { get; set; }
    public virtual Cart Cart { get; set; }

    public int ProductId { get; set; }
    public virtual Product Product { get; set; }

    // Additional field to store quantity
    public int Quantity { get; set; }
}
}
