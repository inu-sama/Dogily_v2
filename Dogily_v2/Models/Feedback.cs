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
    
    public partial class Feedback
    {
        public int IDFB { get; set; }
        public string IDPro { get; set; }
        public string Title { get; set; }
        public Nullable<byte> Rate { get; set; }
        public string Comment { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    
        public virtual Product Product { get; set; }
    }
}
