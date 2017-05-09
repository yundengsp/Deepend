using Deepend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using umbraco.NodeFactory;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace Deepend.Controllers
{
    public class ChequeDetailController : RenderMvcController
    {
       
        /// <summary>
        /// This is the Custom Controller's Custom Action matching the render view's template name
        /// </summary>
        /// <param name="model">Default RenderModel</param>
        /// <param name="chequeId">Individual chequeId</param>
        /// <returns>View with the custom view model</returns>
        public ActionResult ChequeDetail(RenderModel model, int chequeId)
        {
            // get the cheque node from the cheque Id
            var node = new Node(chequeId);

            //create custom viewmodel used by the Cheque Detail Page(or say, Cheque Detail Page's Template)
            ChequeModel customViewModel = new ChequeModel(model.Content)
            {
                Name = node.GetProperty("accountName").Value,
                Date= DateTime.Parse(node.GetProperty("date").ToString()).ToString("dd/MM/yyyy"),
                Amount= Decimal.Parse(node.GetProperty("amount").Value).ToString("#,##0.00"),
                AmountInWord=DecimalToWords(node.GetProperty("amount").Value).ToUpper()
               
            };

            return CurrentTemplate(customViewModel);
        }

        /// <summary>
        /// Separate the Decimal string into two parts, before point, and after point. Turn them to words separatively
        /// </summary>
        /// <param name="Amount">Amount as string</param>
        /// <returns>Converted words from decimal string</returns>
        private string DecimalToWords(string Amount)
        {
            string decimals = "";
            if (Amount.Contains("."))
            {
                decimals = Amount.Substring(Amount.IndexOf(".") + 1);
                // remove decimal part from input
                Amount = Amount.Remove(Amount.IndexOf("."));
            }

            // Convert input into words. save it into strWords
            string strWords = GetWords(Amount);


            if (decimals.Length > 0)
            {
                // if there is any decimal part convert it to words and add it to strWords.
                strWords += " Point " + GetWords(decimals);
            }

            return strWords;

        }

        /// <summary>
        /// Turn the upper 3 digits into words
        /// </summary>
        /// <param name="input">separated number as string</param>
        /// <returns>converted words</returns>
        private static string GetWords(string input)
        {
            // these are seperators for each 3 digit in numbers. you can add more if you want convert beigger numbers.
            string[] seperators = { "", " Thousand ", " Million ", " Billion " };

            // Counter is indexer for seperators. each 3 digit converted this will count.
            int i = 0;

            string strWords = "";

            while (input.Length > 0)
            {
                // get the 3 last numbers from input and store it. if there is not 3 numbers just use take it.
                string _3digits = input.Length < 3 ? input : input.Substring(input.Length - 3);
                // remove the 3 last digits from input. if there is not 3 numbers just remove it.
                input = input.Length < 3 ? "" : input.Remove(input.Length - 3);

                int no = int.Parse(_3digits);
                // Convert 3 digit number into words.
                _3digits = GetWord(no);

                // apply the seperator.
                _3digits += seperators[i];
                // since we are getting numbers from right to left then we must append resault to strWords like this.
                strWords = _3digits + strWords;

                // 3 digits converted. count and go for next 3 digits
                i++;
            }
            return strWords;
        }

        
        /// <summary>
        /// Turn the 3 digits into words
        /// </summary>
        /// <param name="no">3 digits number</param>
        /// <returns>converted words</returns>
        private static string GetWord(int no)
        {
            string[] Ones =
            {
            "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven",
            "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"
        };

            string[] Tens = { "Ten", "Twenty", "Thirty", "Fourty", "Fift", "Sixty", "Seventy", "Eighty", "Ninety" };

            string word = "";

            if (no > 99 && no < 1000)
            {
                int i = no / 100;
                word = word + Ones[i - 1] + " Hundred ";
                no = no % 100;
            }

            if (no > 19 && no < 100)
            {
                int i = no / 10;
                word = word + Tens[i - 1] + " ";
                no = no % 10;
            }

            if (no > 0 && no < 20)
            {
                word = word + Ones[no - 1];
            }

            return word;
        }
    }
}