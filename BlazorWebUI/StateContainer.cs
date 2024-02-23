using BlazorWebUI.Models;

namespace BlazorWebUI
{
    public class StateContainer
    {
        private int? globalCounter = 0;
        public int? Property
        {
            get => globalCounter;
            set
            {
                globalCounter = value;
                NotifyStateChanged();
            }
        }

        private List<FollowedProductDto.SubItem> followedProducts = new List<FollowedProductDto.SubItem>();

        public List<FollowedProductDto.SubItem> FollowedProducts
        {
            get => followedProducts;
            set
            {
                followedProducts = value;
                NotifyStateChanged_FollowedProducts();
            }
        }

        public event Action? OnChange;
        public event Action? OnChange_Products;

        private void NotifyStateChanged() => OnChange?.Invoke();
        private void NotifyStateChanged_FollowedProducts() => OnChange_Products?.Invoke();


    }
}
