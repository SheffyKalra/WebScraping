using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AngleSharp;
using AngleSharp.Html.Parser;
using WebScraping.Models;
using System.Text.RegularExpressions;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using WebScraping_Docker.Services;
using WebScraping_Docker;
using System.Net;
using System.Drawing;
using System.IO;
using System.Reflection;
using CoreHtmlToImage;
using System.Diagnostics;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace WebScraping.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WebScrapingController : ControllerBase
    {
        private readonly ILogger<WebScrapingController> _logger;
        private readonly WebScrapingService _webScrapingService;

        public WebScrapingController(ILogger<WebScrapingController> logger, WebScrapingService webScrapingService)
        {
            _logger = logger;
            _webScrapingService = webScrapingService;
        }

        [HttpGet]
        [Route("GetData")]
        public string GetData()
        {
            return "string data";
        }

        private async Task<ScrapingResult> GetPageData(string url, List<dynamic> results)
        {
            ScrapingResult result = new ScrapingResult();
            result.Hyperlinks = new List<string>();
            result.SocialMediaLinks = new List<string>();
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(url);
            result.SiteName = url;
            result.Title = (document.GetElementsByTagName("title"))[0].InnerHtml;
            var elements = getAllElements(document.Head);
            string headerData = string.Empty; ;
            string BodyData = string.Empty; ;
            foreach (var ele in elements)
            {
                if (ele.LocalName.ToLower() == "head")
                {
                    headerData = ((AngleSharp.Html.Dom.IHtmlHeadElement)ele).InnerHtml;
                }
                if (ele.LocalName.ToLower() == "meta" && ((AngleSharp.Html.Dom.IHtmlMetaElement)ele).Name == "description")
                {
                    result.MetaDescription = ((AngleSharp.Html.Dom.IHtmlMetaElement)ele).Content;
                }
                else if (ele.LocalName.ToLower() == "meta" && ((AngleSharp.Html.Dom.IHtmlMetaElement)ele).Name == "keywords")
                {
                    result.MetaKeywords = ((AngleSharp.Html.Dom.IHtmlMetaElement)ele).Content;
                }
            }

            var bodyElement = getAllElements(document.Body);

            foreach (var ele in bodyElement)
            {
                if (ele.LocalName.ToLower() == "body")
                {
                    BodyData = ((AngleSharp.Html.Dom.IHtmlBodyElement)ele).InnerHtml;
                }
                if (ele.LocalName.ToLower() == "a")
                {
                    Uri oldUri = new Uri(((AngleSharp.Html.Dom.IHtmlAnchorElement)ele).Href);
                    Uri NewUri;

                    Uri.TryCreate(oldUri.ToString(), UriKind.Absolute, out NewUri);

                    var s = NewUri.Authority;
                    if (s == "www.facebook.com" || s == "www.youtube.com" || s == "twitter.com" || s == "www.linkedin.com")
                    {
                        result.SocialMediaLinks.Add(((AngleSharp.Html.Dom.IHtmlAnchorElement)ele).Href);
                    }
                    else

                        result.Hyperlinks.Add(((AngleSharp.Html.Dom.IHtmlAnchorElement)ele).Href);
                }
            }

            //string ScreenHtml = headerData + BodyData;
            var converter = new HtmlConverter();
            result.SocialMediaLinks = result.SocialMediaLinks.Distinct().ToList();
            result.Hyperlinks = result.Hyperlinks.Where(stringToCheck => stringToCheck.Contains("https://techbitsolution.com/services/")).ToList();
            result.Hyperlinks = result.Hyperlinks.Distinct().ToList();
            return result;
        }

        private List<AngleSharp.Dom.IElement> getAllElements(AngleSharp.Dom.IElement element)
        {
            List<AngleSharp.Dom.IElement> elements = new List<AngleSharp.Dom.IElement>();
            elements.Add(element);

            foreach (var child in element.Children)
            {
                elements.AddRange(getAllElements(child));
            }

            return elements;
        }
        private async Task<ScrapingResult> CheckForUpdates(string url, string mailTitle)
        {
            // We create the container for the data we want
            List<dynamic> adverts = new List<dynamic>();
            return await GetPageData(url, adverts);
        }

        [HttpPost]
        [Route("GetScrapedData")]
        public async Task<ActionResult<ScrapingResult>> GetScrapedData([FromBody]string siteName)
        {

            if (siteName != "http://techbitsolution.com")
            {
                return Ok(null);
            }
            ScrapingResult scrapingResult = await CheckForUpdates(siteName, "Web-Scraper updates");

            return Ok(scrapingResult);
        }
    }
}