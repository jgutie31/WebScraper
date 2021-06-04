using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;

namespace Scraper
{
    class Program
    {

        static ScrapingBrowser _browser = new ScrapingBrowser();
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter a search term:");
            var searchTerm = Console.ReadLine();
            var mainPageLinks = GetMainPageLinks("https://mcallen.craigslist.org/d/computer-gigs/search/cpg");
            var lstGigs = GetPageDetails(mainPageLinks, searchTerm);
        }

        static List<string> GetMainPageLinks(string url)
        {
            var homePageLinks = new List<string>();
            var html = GetHtml(url);
            var links = html.CssSelect("a");

            foreach (var link in links)
            {
                if (link.Attributes["href"].Value.Contains(".html"))
                {
                    homePageLinks.Add(link.Attributes["href"].Value);
                }
            }
            return homePageLinks;
        }

        static List<PageDetails> GetPageDetails(List<string> urls, string searchTerm)
        {
            var lstPageDetails = new List<PageDetails>();
            foreach (var url in urls)
            {
                var htmlNode = GetHtml(url);
                var pageDetails = new PageDetails();
                pageDetails.Title = htmlNode.OwnerDocument.DocumentNode.SelectSingleNode("//html/head/title").InnerText;

                var Description = htmlNode.OwnerDocument.DocumentNode.SelectSingleNode("//html/body/section/section/section/section").InnerText;
                pageDetails.Description = Description
                    .Replace("\n      \n            QR Code Link to This Post\n         \n          \n", "");
                pageDetails.Url = url;
                
                var searchTermInTitle = pageDetails.Title.ToLower().Contains(searchTerm.ToLower());
                var searchTermInDescription = pageDetails.Description.ToLower().Contains(searchTerm.ToLower());
                
                if(searchTermInTitle || searchTermInDescription){
                    lstPageDetails.Add(pageDetails);
                }
                
            }

            return lstPageDetails;
        }

        static HtmlNode GetHtml(string url) {
            WebPage webpage = _browser.NavigateToPage(new Uri(url));
            return webpage.Html;
        }
    }
    public class PageDetails {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
    }
}
