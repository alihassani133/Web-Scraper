using HtmlAgilityPack;
using System.Globalization;
using System.Net;

namespace WebScraper.Operations
{
    class ReadLaboratoriesPagesLinks
    {
        readonly string _url;
        public ReadLaboratoriesPagesLinks(string url)
        {
            _url = url;
        }
        public List<string> GetPagesLinks()
        {
            HtmlWeb web = new();

            int retryCount = 0;
            int maxRetries = 3;
            int childNodeCount = 10;
            int totalPageNumber = GetPagesNumber();
            bool fetchSuccessful;

            string pageLink;
            List<string> links = new();
            for (int i = 1; i <= totalPageNumber; i++)
            {
                fetchSuccessful = false;
                while (retryCount < maxRetries && !fetchSuccessful)
                    try
                    {
                        var htmlDoc = web.Load(Path.Combine(_url, i == 1 ? "" : $"?page={i}"));
                        if (i == totalPageNumber)
                        {
                            var listOfPages = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='list']");
                            childNodeCount = listOfPages.ChildNodes
                            .Where(node => node.NodeType == HtmlNodeType.Element)
                            .Count();
                        }
                        for (int j = 1; j <= (i== totalPageNumber ? childNodeCount : 10); j++)
                        {
                            var node = htmlDoc.DocumentNode.SelectSingleNode($"//*[@id='list']/div[{j}]/div[2]/a");
                            pageLink = node.GetAttributeValue("href", "");
                            links.Add(pageLink);
                        }
                        fetchSuccessful = true;
                    }
                    catch (HtmlWebException ex)
                    {
                        Console.WriteLine("In getting the links of pages an HtmlWebException occurred: " + ex.Message);
                        retryCount++;
                        if (retryCount < maxRetries)
                            Thread.Sleep(10000);
                    }
                    catch (HttpRequestException ex)
                    {
                    Console.WriteLine("In getting the links of pages an HttpRequestException occurred: " + ex.Message);
                    retryCount++;
                    if (retryCount < maxRetries)
                        Thread.Sleep(10000);
                    }
                    catch (WebException ex)
                    {
                    Console.WriteLine("In getting the links of pages a WebException occurred: " + ex.Message);
                    retryCount++;
                    if (retryCount < maxRetries)
                        Thread.Sleep(10000);
                    }
                    catch (Exception ex)
                    {
                    Console.WriteLine("In getting the links of pages an error occurred: " + ex.Message);
                    }
            }

            return links;
        }
        private int GetPagesNumber()
        {
            HtmlWeb web = new();

            int retryCount = 0;
            int maxRetries = 3;
            bool fetchSuccessful = false;
            int pageNumbers = 0;

            while (retryCount < maxRetries && !fetchSuccessful)
                try
                {
                var mainDoc = web.Load(_url);
                var paginationNode = mainDoc.DocumentNode.SelectSingleNode("//div[@class='pagination']");
                pageNumbers = Convert.ToInt32(paginationNode.GetAttributeValue("total_page", ""));
                fetchSuccessful = true;
                }
                catch (HtmlWebException ex)
                {
                    Console.WriteLine("In getting the total number of pages an HtmlWebException occurred: " + ex.Message);
                    retryCount++;
                    if (retryCount < maxRetries)
                        Thread.Sleep(10000);
                }
                catch (HttpRequestException ex)
                {
                Console.WriteLine("In getting the total number of pages an HttpRequestException occurred: " + ex.Message);
                retryCount++;
                if (retryCount < maxRetries)
                    Thread.Sleep(5000);
                }
                catch (WebException ex)
                {
                Console.WriteLine("In getting the total number of pages a WebException occurred: " + ex.Message);
                retryCount++;
                if (retryCount < maxRetries)
                    Thread.Sleep(5000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("In getting the total number of pages an error occurred: " + ex.Message);
                }
            return pageNumbers;
        }
    }
}
