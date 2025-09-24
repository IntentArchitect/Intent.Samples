using System;

namespace eShop.Web.Client.Services
{
    public class ProductImageUrlProvider
    {
        public string GetProductImageUrl(int productId)
        {
            return $"https://picsum.photos/id/{productId}/400/400";
        }
    }
}
