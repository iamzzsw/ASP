using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage= "Enter your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter your address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Enter your city")]
        public string City { get; set; }


        [Required(ErrorMessage = "Enter your country")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }



    }
}
