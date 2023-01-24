using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using HtmlAgilityPack;

class WebScraper
{
    public static string ExtractProducts(string html)
    {
        HtmlDocument htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);
        List<Product> products = new List<Product>();

        var itemNodes = htmlDoc.DocumentNode.SelectNodes("//div[@class='item']");

        foreach (var item in itemNodes)
        {
            Product product = new Product();
            product.productName = HttpUtility.HtmlDecode(item.SelectSingleNode(".//img").Attributes["alt"].Value);//set decoded product name
            var priceNode = item.SelectSingleNode(".//p[@class='price']/span[@itemprop='price']");
            if (priceNode != null)
            {
                var dollarNode = priceNode.SelectSingleNode(".//span[contains(@class, 'dollars')]");
                var centsNode = priceNode.SelectSingleNode(".//span[contains(@class, 'cents')]");
                if (dollarNode != null && centsNode != null)
                {
                    var priceText = dollarNode.InnerText.Replace(",", "") + centsNode.InnerText;
                    product.price = priceText;//set price
                }
            }
            var ratingNode = Convert.ToDouble(item.Attributes["rating"].Value);
            if (ratingNode > 5)
            {
                var multiplier = 10; //10 is the base value we assume the rating is smaller than
                while (ratingNode > 5){
                    if (ratingNode > multiplier) { 
                    multiplier *= 10;
                    ratingNode = ratingNode / multiplier * 5;
                    }else ratingNode = ratingNode / multiplier * 5;


                }
            }
            product.rating = ratingNode.ToString();//set rating
            products.Add(product);
        }

        return JsonConvert.SerializeObject(products);
    }

    static void Main(string[] args)
    {
        string html = "<div class=\"item\" rating = \"3\" data-pdid=\"5426\"><figure><a\r\n\r\nhref=\"https://www.100percent.co.nz/Product/WCM7000WD/Electrolux-700L-\r\nChest-Freezer\"><img alt=\"Electrolux 700L Chest Freezer &amp; Filter\"\r\n\r\nsrc=\"/productimages/thumb/1/5426_5731_4009.jpg\" data-alternate-\r\nimage=\"/productimages/thumb/2/5426_5731_4010.jpg\" class=\"mouseover-\r\nset\"><span class=\"overlay top-horizontal\"><span class=\"sold-out\"><img\r\n\r\nalt=\"Sold Out\"\r\nSrc=\"/Images/Overlay/overlay_1_2_1.png\"></span></span></a></figure><div\r\nclass=\"item-detail\"><h4><a\r\n\r\nhref=\"https://www.100percent.co.nz/Product/WCM7000WD/Electrolux-700L-\r\nChest-Freezer\">Electrolux 700L Chest Freezer</a></h4><div class=\"pricing\"\r\n\r\nitemprop=\"offers\" itemscope=\"itemscope\"\r\nitemtype=\"http://schema.org/Offer\"><meta itemprop=\"priceCurrency\"\r\ncontent=\"NZD\"><p class=\"price\"><span class=\"price-display formatted\"\r\nitemprop=\"price\"><span style=\"display: none\">$2,099.00</span>$<span\r\nclass=\"dollars over500\">2,099</span><span class=\"cents\r\nzero\">.00</span></span></p></div><p class=\"style-number\">WCM7000WD</p><p\r\nclass=\"offer\"><a\r\n\r\nhref=\"https://www.100percent.co.nz/Product/WCM7000WD/Electrolux-700L-\r\nChest-Freezer\"><span style=\"color:#CC0000;\">WCM7000WD</span></a></p><div\r\n\r\nclass=\"item-asset\"><!--.--></div></div></div>\r\n<div class=\"item\" rating = \"3.6\" data-pdid=\"5862\"><figure><a\r\n\r\nhref=\"https://www.100percent.co.nz/Product/E203S/Electrolux-Anti-Odour-\r\nVacuum-Bags\"><img alt=\"Electrolux Anti-Odour Vacuum Bags\"\r\n\r\nsrc=\"/productimages/thumb/1/5862_6182_4541.jpg\"></a></figure><div\r\nclass=\"item-detail\"><h4><a\r\n\r\nhref=\"https://www.100percent.co.nz/Product/E203S/Electrolux-Anti-Odour-\r\nVacuum-Bags\">Electrolux Anti-Odour Vacuum Bags</a></h4><div\r\n\r\nclass=\"pricing\" itemprop=\"offers\" itemscope=\"itemscope\"\r\nitemtype=\"http://schema.org/Offer\"><meta itemprop=\"priceCurrency\"\r\ncontent=\"NZD\"><p class=\"price\"><span class=\"price-display formatted\"\r\nitemprop=\"price\"><span style=\"display: none\">$22.99</span>$<span\r\nclass=\"dollars\">22</span><span class=\"cents\">.99</span></span></p></div><p\r\nclass=\"style-number\">E203S</p><p class=\"offer\"><a\r\nhref=\"https://www.100percent.co.nz/Product/E203S/Electrolux-Anti-Odour-\r\n\r\nVacuum-Bags\"><span style=\"color:#CC0000;\">E203S</span></a></p><div\r\nclass=\"item-asset\"><!--.--></div></div></div>\r\n<div class=\"item\" rating = \"8.4\" data-pdid=\"4599\"><figure><a\r\n\r\nhref=\"https://www.100percent.co.nz/Product/USK11ANZ/Electrolux-UltraFlex-\r\nStarter-Kit\"><img alt=\"Electrolux UltraFlex Starter &#91; Kit &#93; \"\r\n\r\nsrc=\"/productimages/thumb/1/4599_4843_2928.jpg\"></a></figure><div\r\nclass=\"item-detail\"><h4><a\r\n\r\nhref=\"https://www.100percent.co.nz/Product/USK11ANZ/Electrolux-UltraFlex-\r\nStarter-Kit\">Electrolux UltraFlex &#64; Starter Kit</a></h4><div\r\n\r\nclass=\"pricing\" itemprop=\"offers\" itemscope=\"itemscope\"\r\nitemtype=\"http://schema.org/Offer\"><meta itemprop=\"priceCurrency\"\r\ncontent=\"NZD\"><p class=\"price\"><span class=\"price-display formatted\"\r\nitemprop=\"price\"><span style=\"display: none\">$44.99</span>$<span\r\nclass=\"dollars\">44</span><span class=\"cents\">.99</span></span></p></div><p\r\nclass=\"style-number\">USK11ANZ</p><p class=\"offer\"><a\r\n\r\nhref=\"https://www.100percent.co.nz/Product/USK11ANZ/Electrolux-UltraFlex-\r\nStarter-Kit\"><span style=\"color:#CC0000;\">USK11ANZ</span></a></p><div\r\n\r\nclass=\"item-asset\"><!--.--></div></div></div>";
        Console.WriteLine(ExtractProducts(html));
    }
    }

class Product
{
    public string productName { get; set; }
    public string price { get; set; }
    public string rating { get; set; }
}
