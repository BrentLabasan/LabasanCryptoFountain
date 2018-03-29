using System;
using System.ComponentModel.DataAnnotations;

namespace TST_Fountain.Models
{
    public class Send
    {
        public int ID { get; set; }
        [Required]
        public string TokenName { get; set; }
        [Required]
        public decimal Amount { get; set; }

    }
}