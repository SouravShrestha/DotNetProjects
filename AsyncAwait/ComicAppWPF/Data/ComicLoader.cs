using ComicAppWPF.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ComicAppWPF.Data
{
    class ComicLoader
    {
        public static async Task<ComicModel> LoadComic(int comicNumber = 0)
        {
            string url = "https://xkcd.com/info.0.json";
            if(comicNumber > 0)
            {
                url = $"https://xkcd.com/{ comicNumber }/info.0.json";
            }
            using(HttpResponseMessage httpResponse = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (httpResponse.IsSuccessStatusCode)
                {
                    string reponseString = await httpResponse.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ComicModel>(reponseString);
                }
                throw new Exception(httpResponse.ReasonPhrase);
            }
        }
    }
}
