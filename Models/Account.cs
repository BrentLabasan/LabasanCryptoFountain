using System;
using System.ComponentModel.DataAnnotations;

namespace TST_Fountain.Models
{
    public class Account
    {
        public int ID { get; set; }

        [Required]
        public string Address { get; set; }
        public decimal TotalSentSecond { get; set; }
        public decimal TotalSentMinute { get; set; }

        public decimal TotalSentHour { get; set; }

        public decimal TotalSentDay { get; set; }

        public decimal TotalSentWeek { get; set; }

        public decimal TotalSentMonth { get; set; }

        public decimal TotalSentYear { get; set; }

    }
}