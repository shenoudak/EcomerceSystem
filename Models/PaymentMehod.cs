﻿using System.ComponentModel.DataAnnotations;

namespace Jovera.Models
{
    public class PaymentMehod
    {
        [Key]
        public int PaymentMethodId { get; set; }
        public string PaymentMethodNameAr { get; set; }
        public string PaymentMethodNameEn { get; set; }
        public string PaymentMethodPic { get; set; }
    }
}
