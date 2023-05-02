using System;
using System.Collections.Generic;

namespace MVCWithDB_Identity.Models
{
    public partial class Shipper
    {
        public int ShipperId { get; set; }
        public string CompanyName { get; set; } = null!;
        public string? Phone { get; set; }
    }
}
