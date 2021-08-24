using System;
using System.Collections.Generic;
using System.Text;
namespace Models.Alternative
{
    public class Hotel 
    {
        public string Title { get; set; }
        public string URL { get; set; }
        public string RateString { get; set; }
        public int RateValue { get; set; }
        public string ImageLink { get; set; }
        public string ImageTooltip { get; set; }
        public string ImageHref { get; set; }
        public string ImageLink2 { get; set; }
        public string ImageTooltip2 { get; set; }
        public string ImageHref2 { get; set; }
        public string Description { get; set; }
        public int ViewCount { get; set; }
        public int ReviewCount { get; set; }
        public decimal Score { get; set; }
        public string ScoreTitle { get; set; }
        public string BookNowLink { get; set; }
    }
}
