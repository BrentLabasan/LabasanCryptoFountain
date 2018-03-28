using System;

namespace TST_Fountain.Models
{
    public class Account
    {
        public int ID { get; set; }
        public decimal TotalSentSecond { get; set; }
        public decimal TotalSentMinute { get; set; }

        public decimal TotalSentHour { get; set; }

        public decimal TotalSentDay { get; set; }

        public decimal TotalSentWeek { get; set; }

        public decimal TotalSentMonth { get; set; }

        public decimal TotalSentYear { get; set; }

    }
}