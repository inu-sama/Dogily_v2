//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dogily_v2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderDetail
    {
        public int IDOrDe { get; set; }
        public string IDPro { get; set; }
        public int IDOrder { get; set; }
        public Nullable<byte> Amount { get; set; }
        public Nullable<double> TotalPrice { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
