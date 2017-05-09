using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Web.Models;

namespace Deepend.Models
{
    public class ChequeModel : RenderModel
    {
        public ChequeModel(IPublishedContent content) : base(content) { }

        public int NodeId { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public string AmountInWord { get; set; }


    }
}