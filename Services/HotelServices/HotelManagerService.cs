using FileExtractor;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using Microsoft.Extensions.Logging;
using Models.Alternative;
using Models.AlternativeHotel;
using Models.Availability;
using Services.Interfecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility.ExtentionMethods;

namespace Services.HotelServices
{
   public class HotelManagerService : IHotelManagerService
    {
        HtmlDocument _htmlDocument;
        private IExtractor _extractor;
        ILogger<HotelManagerService> _logger;
        public HotelManagerService( IExtractor extractor , ILogger<HotelManagerService> logger)
        {
            _htmlDocument = new HtmlDocument();
            _extractor = extractor;
            _htmlDocument.LoadHtml(_extractor.ReadFileHTMLtoString(@"D:\Practice\task1\task1.html"));
            _logger = logger;
        }
        public List<Hotel> AlternativeHotel()
        {
            List<Hotel> hotels = new List<Hotel>();
            _logger.LogInformation("Alt Started");
            
            HtmlNode titleNode = _htmlDocument.DocumentNode.SelectSingleNode("//*[@id = 'hp_cs_persuasive_headers']");
            var allContent = _htmlDocument.DocumentNode.SelectSingleNode("//tr[@id = 'althotelsRow']");
            foreach (var item in allContent.Descendants("td").Where(e => e.NodeType == HtmlNodeType.Element))
            {
                Hotel hotel = new Hotel();
                var paragraph = item.Descendants("p").FirstOrDefault();
                var aTag = item.Descendants("p").FirstOrDefault().Descendants("a").FirstOrDefault();
                var iTag = item.Descendants("p").FirstOrDefault().Descendants("i").FirstOrDefault();
                var aTag2 = item.Descendants("a").FirstOrDefault();
                // var img2 = item.Descendants("a").FirstOrDefault().Descendants("img").FirstOrDefault();
                hotel.Title = aTag.InnerText.Trim();
                hotel.URL = aTag.GetAttributeValue("href", "").Trim();
                hotel.RateString = iTag.GetAttributeValue("title", "").Trim();
                hotel.RateValue = hotel.RateString.ExtractNumberFromString();
                hotel.ImageTooltip = aTag2.InnerText.Trim();
                hotel.ImageHref = aTag2.GetAttributeValue("href", "").Trim();
                // hotel.ImageLink = img2.GetAttributeValue("src", "");
                hotel.Description = item.QuerySelector("span .hp_compset_description").InnerText;
                hotel.ViewCount = item.QuerySelector("span .hp_compset_description").NextSiblingElement().InnerText.Trim().ExtractNumberFromString();
                hotel.ReviewCount = Convert.ToInt32(item.QuerySelector("div span strong").InnerText.Trim());
                hotel.Score = Convert.ToDecimal(item.QuerySelector("div a span .js--hp-scorecard-scoreval").InnerText.Trim());
                hotel.ScoreTitle = item.QuerySelector("div a span .js--hp-scorecard-scoreword").InnerText.Trim();
                hotel.BookNowLink = item.QuerySelector("div .alt_hotels_info_row a").GetAttributeValue("href", "").Trim();

                hotels.Add(hotel);
            }


            return hotels;
        }
        public List<Link> GetAlternativeLink()
        {
            var doc = _htmlDocument.DocumentNode;
            var Links = doc.QuerySelectorAll("p #altThemeLinks a");
            List<Link> LinkList = new List<Link>();
            foreach (var item in Links)
            {
                LinkList.Add(
                    new Link()
                    {
                        Title = item.InnerText.Trim(),
                        URL = item.GetAttributeValue("href", ""),
                    });                    
            }
            return LinkList;
        }
        public RoomTable GetAvailability()
        {
            RoomTable roomTable = new RoomTable();
            List<string> maxList = new List<string>();
            roomTable.Rooms = new List<Room>();
            roomTable.Header = new List<string>();
            //-------------------------Get title
            var doc = _htmlDocument.DocumentNode;
            var trDoc = _htmlDocument;
            HtmlNode titleNode = doc.SelectSingleNode("//div[@class='hp_last_booking']");
            roomTable.Title = titleNode.InnerText.Trim();


            //--------------------------Get Header Rows
            var headerTitleNodes = doc.SelectSingleNode("//*[@id ='maxotel_table_header']").Descendants("th");
            foreach (var item in headerTitleNodes)
            {
                roomTable.Header.Add(item.InnerText.Trim());
            }

            //--------------------------Get Body Rows
            var bodyTr = doc.QuerySelectorAll("table #maxotel_rooms tbody tr");
            IEnumerable<HtmlNode> tdList;
            int counter = 0;
            string roomText = string.Empty;

            foreach (var item in bodyTr)//Get all Rows
            {
                tdList = item.Descendants("td");//Get TD of Any Rows

                foreach (var item2 in tdList)
                {
                    if (counter == 0)//Get max Column
                    {
                        var span_td = item2.Descendants();
                        foreach (var item3 in span_td)//Get MaxColumn
                        {
                            string x = string.Empty;
                            x = item3.GetAttributeValue("title", "").Trim();
                            if (x != string.Empty)
                                maxList.Add(x);
                        }
                        roomText = "";
                    }
                    if (counter == 1)//Get RoomType
                    {
                        roomText = item2.InnerText.Trim();
                        break;
                    }
                    counter++;
                }
                counter = 0;

                roomTable.Rooms.Add(
                new Room
                {
                    Max = new List<string>(maxList),
                    RoomType = roomText,
                });
                maxList.Clear();
            }
            return roomTable;

        }
        public string GetHodelDescription()
        {
            string result = string.Empty;
            var doc = _htmlDocument.DocumentNode;
            var node = doc.SelectNodes("//*[@id = 'summary']");
            IEnumerable<HtmlNode> nodeCollection = node.Descendants("p");
            foreach (var item in nodeCollection)
            {
                result += item.InnerText.Trim() + "\n";
            }

            return result;
        }
        public List<string> GetHotelName()
        {
            var doc = _htmlDocument.DocumentNode;
            HtmlNode NamehtmlNode = doc.SelectSingleNode("//*[@id = 'hp_hotel_name']");
            HtmlNode AddresshtmlNode = doc.SelectSingleNode("//*[@id = 'hp_address_subtitle']");
            return new List<string>
            {
                NamehtmlNode.InnerText.Trim(),
                AddresshtmlNode.InnerText.Trim()
            };
        }
    }
}
