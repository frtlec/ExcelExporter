using BlazorWebUI.Databases.Mongo.DataAccess;
using BlazorWebUI.Databases.Mongo.DataModels;
using BlazorWebUI.Models;
using static MudBlazor.CategoryTypes;

namespace BlazorWebUI.Bussines
{
    public class FollowedProductService
    {
        private readonly FollowedProductDataAccess _followedProductData;
        private readonly TahtakaleIntegration tahtakaleIntegration;
        public FollowedProductService(FollowedProductDataAccess followedProductData, TahtakaleIntegration tahtakaleIntegration)
        {
            _followedProductData = followedProductData;
            this.tahtakaleIntegration = tahtakaleIntegration;
        }

        public async Task<FollowedProductDto> GetAll()
        {
            var followedProducts = await _followedProductData.GetAll();

            TahtaKaleResponseDto resp = await tahtakaleIntegration.GetAll();

            var followedProducts_tahtaKale = resp.Datas.Where(f => followedProducts.Any(x => x.ProductId == Convert.ToInt64(f.Id))).Select(x => new FollowedProductDto.SubItem
            {
                Id= followedProducts.First(y => y.ProductId == Convert.ToInt64(x.Id)).Id,
                Title=x.Title,
                ImageLinks=new List<string> { x.ImageLink,x.AdditionalImageLink1},
                SourceSite=SourceSite.TAHTAKALE,
                Stock=x.Quantity,
                Barcode=x.Barcode,
                Categories=x.Category,
                Description=x.Description,
                Link=x.Link,
                ModelNo=x.ModelNumber,
                TahtaKaleItem=x,
                Price=x.Price,
              
                FollowedProduct = followedProducts.First(y=>y.ProductId== Convert.ToInt64(x.Id)),
                TrendyolStock = followedProducts.First(y => y.ProductId == Convert.ToInt64(x.Id)).TrendyolStock,
                TrendyolLink = followedProducts.First(y => y.ProductId == Convert.ToInt64(x.Id)).TrendyolLink,
                TrendyolPrice = followedProducts.First(y => y.ProductId == Convert.ToInt64(x.Id)).TrendyolPrice,
                ProductId = Convert.ToInt64(x.Id),
                ProductTypes=x.ProductType
            }).ToList();
            return new FollowedProductDto()
            {
               Items= followedProducts_tahtaKale
            };

        }
        public async Task Add(FollowedProductAddDto item)
        {
            await _followedProductData.Create(new FollowedProduct
            {
                Barcode=item.Barcode,
                SourceSite=SourceSite.TAHTAKALE,
                ProductId=item.ProductId
            });
        }
        public async Task Remove(string id)
        {
            await _followedProductData.Remove(id);
        }
        public async Task Update(FollowedProductUpdate updateItem)
        {
            await _followedProductData.Update(updateItem.Id,updateItem.TrendyolPrice,updateItem.TrendyolStock,updateItem.TrendyolLink);
        }
        public async Task RemoveAndAdd(List<FollowedProductAddDto> items)
        {
            var lastitems = items.Select(item =>
            new FollowedProduct
            {
                Barcode = item.Barcode,
                SourceSite = SourceSite.TAHTAKALE,
                ProductId = item.ProductId
            }).ToList();
            await _followedProductData.RemoveAndCreateMany(lastitems);
        }
    }
}
