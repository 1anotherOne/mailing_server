using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;
using Server.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Server.Helpers
{
    public class SearchHelper
    {
        private NewsApiClient newsApiClient;
        public SearchHelper()
        {
            newsApiClient = new NewsApiClient(" feb654e16f7645b9ad90a8ce3bda0d55");
        }

        public List<Article> Search(string query, string date)
        {
            ArticlesResult articlesResponse = newsApiClient.GetEverything(GetRequest(query, date));
            var articleList = new List<Article>();
            if (articlesResponse.Status == Statuses.Ok)
            {
                // total results found
                Console.WriteLine(articlesResponse.TotalResults);
                // here's the first 20
                foreach (var article in articlesResponse.Articles)
                {
                    articleList.Add(article);
                }
            }
            return articleList;
        }

        private EverythingRequest GetRequest(string query, string date)
        {
            EverythingRequest req;
            if(date == null)
            {
                req = new EverythingRequest
                {
                    Q = query,
                    SortBy = SortBys.Popularity,
                    Language = Languages.EN,
                    From = DateTime.Today
                };
            }
            else
            {
                int year;
                int month;
                int day;

                String[] dateSplit = date.Split(",");
                if(dateSplit.Length >= 3)
                {
                    year = Int32.Parse(dateSplit[0]);
                    month = Int32.Parse(dateSplit[1]);
                    day = Int32.Parse(dateSplit[2]);
                }
                else
                {
                    throw new IOException("Invalid date format.The date format must follow the pattern YEAR,MONTH,DAY");
                }
                req = new EverythingRequest
                {
                    Q = query,
                    SortBy = SortBys.Popularity,
                    Language = Languages.EN,
                    From = new DateTime(year,month,day)
                };
            }
            return req;
        }
    }

}
