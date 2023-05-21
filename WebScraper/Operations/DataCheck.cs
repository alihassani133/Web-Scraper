using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper.Operations
{
    class DataCheck
    {
        /// <summary>
        /// Checks whether the specific HTML document contains the name of the laboratory
        /// </summary>
        /// <param name="document"></param>
        /// <returns>a boolean value represents the existence of the laboratory name</returns>
        public bool NameExists(HtmlDocument document)
        {
            bool existence = document.DocumentNode.SelectSingleNode($"//*[@id='lab_view']/div[1]/h1") != null;
            return existence;
        }
        /// <summary>
        /// Checks whether the specific HTML document contains the Laboratory Status span
        /// </summary>
        /// <param name="document"></param>
        /// <returns>a boolean value represents the existence of the Laboratory Status span</returns>
        public bool StatusExists(HtmlDocument document)
        {
            bool existence = document.DocumentNode.SelectSingleNode("//span[@class='member_icon_grey']") != null;
            return existence;
        }
        /// <summary>
        /// Checks whether the specific HTML document contains the Location span
        /// </summary>
        /// <param name="document"></param>
        /// <returns>a boolean value represents the existence of the Location span</returns>
        public bool LocationExists(HtmlDocument document)
        {
            bool existence = document.DocumentNode.SelectSingleNode("//span[@class='location_icon_grey']") != null;
            return existence;
        }
        /// <summary>
        /// Checks whether the specific HTML document contains the Laboratory Manager span
        /// </summary>
        /// <param name="document"></param>
        /// <returns>a boolean value represents the existence of the Laboratory Manager span</returns>
        public bool ManagerExists(HtmlDocument document)
        {
            bool existence = document.DocumentNode.SelectSingleNode("//span[@class='manager_icon_grey']") != null;
            return existence;
        }
        /// <summary>
        /// Checks whether the specific HTML document contains the Laboratory Agent span
        /// </summary>
        /// <param name="document"></param>
        /// <returns>a boolean value represents the existence of the Laboratory Agent</returns>
        public bool AgentExists(HtmlDocument document)
        {
            bool existence = document.DocumentNode.SelectSingleNode("//span[@class='person_icon_grey']") != null;
            return existence;
        }
        /// <summary>
        /// Checks whether the specific HTML document contains the Laboratory Telephone Number span
        /// </summary>
        /// <param name="document"></param>
        /// <returns>a boolean value represents the existence of the Laboratory Telephone Number</returns>
        public bool TelephoneNumberExists(HtmlDocument document)
        {
            bool existence = document.DocumentNode.SelectSingleNode("//span[@class='tel_icon_grey']") != null;
            return existence;
        }
        /// <summary>
        /// Checks whether the specific HTML document contains the Laboratory Fax Number span
        /// </summary>
        /// <param name="document"></param>
        /// <returns>a boolean value represents the existence of the Laboratory Fax Number</returns>
        public bool FaxNumberExists(HtmlDocument document)
        {
            bool existence = document.DocumentNode.SelectSingleNode("//span[@class='fax_icon_grey']") != null;
            return existence;
        }
        /// <summary>
        /// Checks whether the specific HTML document contains the Laboratory Website Address span
        /// </summary>
        /// <param name="document"></param>
        /// <returns>a boolean value represents the existence of the Laboratory Website Address</returns>
        public bool WebsiteAddressExists(HtmlDocument document)
        {
            bool existence = document.DocumentNode.SelectSingleNode("//span[@class='website_icon_grey']") != null;
            return existence;
        }
        /// <summary>
        /// Checks whether the specific HTML document contains the Laboratory Email Address span
        /// </summary>
        /// <param name="document"></param>
        /// <returns>a boolean value represents the existence of the Laboratory Email Address</returns>
        public bool EmailAddressExists(HtmlDocument document)
        {
            bool existence = document.DocumentNode.SelectSingleNode("//span[@class='email_icon_grey']") != null;
            return existence;
        }
    }
}
