//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Uspa.Domain.LocalDb
{
    using System;
    using System.Collections.Generic;
    
    public partial class Banners
    {
        public int id { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public bool state { get; set; }
        public Nullable<int> site_id { get; set; }
    
        public virtual Sites Sites { get; set; }
    }
}
