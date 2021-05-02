using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;
using Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace Server.Handlers
{
    public class SearchHandler
    {
        private NewsApiClient newsApiClient;
        public SearchHandler()
        {
        }

        public List<Article> search(string query)
        {
            newsApiClient = new NewsApiClient(" feb654e16f7645b9ad90a8ce3bda0d55");
            var articlesResponse = newsApiClient.GetEverything(new EverythingRequest
            {
                Q = query,
                SortBy = SortBys.Popularity,
                Language = Languages.EN,
                From = DateTime.Today
            });
            var articleList = new List<Article>();
            if (articlesResponse.Status == Statuses.Ok)
            {
                // total results found
                Console.WriteLine(articlesResponse.TotalResults);
                // here's the first 20
                foreach (var article in articlesResponse.Articles)
                {
                    articleList.Add(article);
                    // title
                    //Console.WriteLine(article.Title);
                    // author
                    //Console.WriteLine(article.Author);
                    // description
                    //Console.WriteLine(article.Description);
                    // url
                    //Console.WriteLine(article.Url);
                    // published at
                    //Console.WriteLine(article.PublishedAt);
                }
            }
            return articleList;
        }
    }
}
