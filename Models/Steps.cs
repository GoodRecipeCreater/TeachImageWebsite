//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace TeachImageWebsite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Steps
    {
        [Display(Name = "食譜步驟編號")]
        public int sId { get; set; }
        [Display(Name = "食譜步驟")]
        public string sContent { get; set; }
        [Display(Name = "食譜編號")]
        public int aId { get; set; }
    }
}
