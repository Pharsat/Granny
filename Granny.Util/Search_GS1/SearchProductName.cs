using System;
using System.Collections.Generic;
using System.Text;

namespace Granny.Util.Search_GS1
{
    class SearchProdcutName
    {
        public static async System.Threading.Tasks.Task<ValidationResult> SearchProductAsync(string gtin)
        {
            string nameproduct;
            var formContent = new FormUrlEncodedContent(new[]
                {
                 new KeyValuePair<string, string>("itemGTIN", gtin)
               });

            System.Net.Http.HttpClient client = new HttpClient();
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
                catch (Exception e)
                {
                    nameproduct = "not found";
                }


            }
            else
            {
                nameproduct = "internal server error";
            }



            return new ValidationResult(nameproduct);
        }

    }
}
