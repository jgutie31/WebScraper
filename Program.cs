using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;

namespace Scraper { 

  class Program 
  {
    // This is a Global Variable that we will be using for scraping
      static ScrapingBrowser _browser = new ScrapingBrowser();


    // MAINT METHOD //
    static void Main(string[] args) 
    {
        // Paste in to GetHtml whatever website it is you are trying to parse
        // I chose a url with a list of lesson & tutoring in the mcallen/edinburg area
        GetMainPageLinks("https://mcallen.craigslist.org/d/lessons-tutoring/search/lss");
    } 

    // Creating another method that is going to return a list of string

    static List<string> GetMainPageLinks(string url) {
        // This is where we are going to store the list of urls to ALL of those CraigsList listings
        var homePageLinks = new List<string>();
        // This will get the HTML from each link
        var html = GetHtml(url);
        // This should grab us ALL OF THE LINKS that are on the page
        var links = html.CssSelect("a");

        // for each link we pull, if "href" contains ".html" then pull it (finding verified pages)
        foreach (var link in links) {
            if(link.Attributes["href"].Value.Contains(".hmtl")) {
                homePageLinks.Add(link.Attributes["href"].Value);
            }
        }
        // return or output 'homePageLinks', which is where all that info will be output
        return homePageLinks;
    }


    // This is the Function that will return to us the html of a given page
    static HtmlNode GetHtml(string url) {
        WebPage webpage = _browser.NavigateToPage(new Uri(url));
        return webpage.Html; 
    }
  } 
}
