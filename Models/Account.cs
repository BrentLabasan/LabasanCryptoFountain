using System;
using System.ComponentModel.DataAnnotations;

namespace LabasanCryptoFountain.Models
{
    public class Account
    {
        public int ID { get; set; }

        [Required]
        public string Address { get; set; }

        public decimal TotalSentXlm { get; set; }

        public decimal TotalSentSecond { get; set; }
        public decimal TotalSentMinute { get; set; }
        public decimal TotalSentHour { get; set; }
        public decimal TotalSentDay { get; set; }
        public decimal TotalSentWeek { get; set; }
        public decimal TotalSentMonth { get; set; }
        public decimal TotalSentYear { get; set; }

        public decimal TotalSentMaslow1 { get; set; }
        public decimal TotalSentMaslow2 { get; set; }
        public decimal TotalSentMaslow3 { get; set; }
        public decimal TotalSentMaslow4 { get; set; }
        public decimal TotalSentMaslow5 { get; set; }

    }
}