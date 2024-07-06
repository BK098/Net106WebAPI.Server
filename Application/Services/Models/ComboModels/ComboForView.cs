
using Application.Services.Models.ComboModels.Base;

namespace Application.Services.Models.ComboModels
{
    public class ComboForView
    {
        public IList<ComboForViewItems> Combos { get; set; } = new List<ComboForViewItems>();
    }
    public class ComboForViewItems : ComboBaseDto
    {
        public Guid Id { get; set; }
        public List<ComboItemInfoForView> ComboItems { get; set; } = new List<ComboItemInfoForView>();
    }
    public class ComboItemInfoForView
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string? ProductImage { get; set; }
        public double ProductPrice { get; set; }
        public int Quantity { get; set; }
    }
}
