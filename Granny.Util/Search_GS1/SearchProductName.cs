using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Granny.Util.Exceptions;

namespace Granny.Util.Search_GS1
{
    public static class SearchProductName
    {
        public static async Task<string> SearchProductAsync(string gtin)
        {
            string nameproduct;
            var formContent = new FormUrlEncodedContent(new[]
                {
                 new KeyValuePair<string, string>("itemGTIN", gtin)
               });

            HttpClient client = new HttpClient();
            var response = await client.PostAsync("http://gepir.gs1co.org/GepirV40/es/Codes/SearchItemGTIN".ToString(), formContent).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var contents = await response.Content.ReadAsStringAsync();
                HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
                htmlDoc.OptionFixNestedTags = true;
                htmlDoc.LoadHtml(contents);

                try
                {
                    nameproduct = htmlDoc.DocumentNode.SelectSingleNode("//td[5]").InnerText.ToString().Substring(2);
                }
                catch
                {
                    throw new ProductNotFoundException("Product not found");
                }
            }
            else
            {
                throw new ExternalRequestException("Error de comunicación con el servidor GS1");
            }

            return nameproduct;
        }

    }
}