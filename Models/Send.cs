using System;
using System.ComponentModel.DataAnnotations;

namespace LabasanCryptoFountain.Models
{
    public class Send
    {
        public int ID { get; set; }

        [Required]
        public string TokenName { get; set; }
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Source { get; set; }
        [Required]
        public string Destination { get; set; }
        
        [Required]
        public DateTimeOffset sendStart { get; set; }
        [Required]
        public DateTimeOffset sendEnd { get; set; }
    }
}