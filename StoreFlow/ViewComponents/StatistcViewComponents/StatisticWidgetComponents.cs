using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents.StatistcViewComponents
{
    public class StatisticWidgetComponents:ViewComponent
    {
        private readonly StoreContext _storeContext;

        public StatisticWidgetComponents(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public IViewComponentResult Invoke()
        {
            //Category Sayısı
            ViewBag.categoryCount = _storeContext.Categories.Count();

            //En Yüksek Ürün Fiyatını Adı
            ViewBag.productMaxPriceProductName = _storeContext.Products.Where(p => p.ProductPrice ==(_storeContext.Products.Max(z => z.ProductPrice))).Select(x => x.ProductName).FirstOrDefault();

            //En Yüksek Ürün Fiyatını Fiyatı
            ViewBag.productMaxPrice = _storeContext.Products.Max(p=>p.ProductPrice);

            //En Düşük Ürün Fiyatını Fiyatı
            ViewBag.productMinPrice = _storeContext.Products.Min(p=>p.ProductPrice);

            //En Düşük Ürün Fiyatını Adı
            ViewBag.productMinPriceProductName = _storeContext.Products.Where(x =>x.ProductPrice == (_storeContext.Products.Min(z => z.ProductPrice))).Select(y =>y.ProductName).FirstOrDefault();

            //Toplam Ürün Stok Sayısı

            ViewBag.totalSumProductStock = _storeContext.Products.Sum(x =>x.ProductStock);

            //Ortalama Ürün Stok sayısı
            ViewBag.averageProductStock = _storeContext.Products.Average(x => x.ProductStock);

            //Ortalama Ürün Fiyatı
            ViewBag.averageProductPrice = _storeContext.Products.Average(_ => _.ProductPrice);

            //1000 TL'den Yüksek Fiyatlı Ürün Sayısı
            ViewBag.biggerPriceThen1000ProductCount = _storeContext.Products.Where(x => x.ProductPrice > 1000).Count();

            //4 Nolu ID'ye Ait Ürün Adı
            ViewBag.getIDIs4ProductName = _storeContext.Products.Where(x => x.ProductId ==4).Select(x => x.ProductName).FirstOrDefault();

            //Stoğu 50-100 Arasındaki Ürün
            ViewBag.stockCountBigger50AndSmaller100ProductCount = _storeContext.Products.Where(x => x.ProductStock > 50 && x.ProductStock < 100).Count();
            return View();
        }
    }
}
