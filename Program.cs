using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;

namespace Scraper { 

  class Program 
  {
    // This is a Global Variable that we will be using for scraping
      static ScrapingBrowser _scrapingBrowser = new ScrapingBrowser();


    // MAINT METHOD //
    static void Main(string[] args) 
    {
        // Paste in to GetHtml whatever website it is you are trying to parse
        // I chose a url with a list of lesson & tutoring in the mcallen/edinburg area
        GetHtml("https://mcallen.craigslist.org/d/lessons-tutoring/search/lss");
    } 

    // This is the Function that will return to us the html of a given page
    static HtmlNode GetHtml(string url) {
        WebPage webpage = _scrapingBrowser.NavigateToPage(new Uri(url));
        return webpage.Html; 
    }
  } 
}
