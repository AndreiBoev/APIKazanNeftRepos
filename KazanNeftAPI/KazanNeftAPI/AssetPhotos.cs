//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KazanNeftAPI
{
    using System;
    using System.Collections.Generic;
    
    public partial class AssetPhotos
    {
        public long ID { get; set; }
        public long AssetID { get; set; }
        public byte[] AssetPhoto { get; set; }
    
        public virtual Assets Assets { get; set; }
    }
}
