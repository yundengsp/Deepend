using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Web.Models;

namespace Deepend.Models
{
    /// <summary>
    /// Custom Viewmodel for renderring Cheque Detail Page
    /// </summary>
    public class ChequeModel : RenderModel
    {
        public ChequeModel(IPublishedContent content) : base(content) { }

        public string Name { get; set; }

        public string Date { get; set; }

        public string Amount { get; set; }

        public string AmountInWord { get; set; }


    }
}