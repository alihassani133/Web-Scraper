using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebScraper.Model;

namespace WebScraper.Operations
{
    class ReadLaboratoryInfo
    {
        private readonly DataCheck _dataCheck;
        public List<Laboratory> Laboratories { get; }
        public ReadLaboratoryInfo(DataCheck dataCheck)
        {
            Laboratories = new List<Laboratory>();
            _dataCheck = dataCheck;
        }

        public void GetLaboratoriesInformation(List<string> pageLinks)
        {
            HtmlWeb web = new()
            {
                AutoDetectEncoding = true
            };
            int maxRetries = 3;
            Parallel.For(0, pageLinks.Count, i =>
            {
                int retryCount = 0;
                bool fetchSuccessful = false;

                while (retryCount < maxRetries && !fetchSuccessful)
                {
                    try
                    {
                        HtmlDocument page = web.Load(pageLinks[i]);

                        Laboratory laboratory = new Laboratory
                        {
                            LaboratoryName = GetLaboratoryName(page),
                            Status = GetLaboratoryStatus(page),
                            Location = GetLaboratoryLocation(page),
                            ManagerName = GetLaboratoryManagerName(page),
                            AgentName = GetLaboratoryAgentName(page),
                            TelephoneNumber = GetLaboratoryTelephoneNumber(page),
                            FaxNumber = GetLaboratoryFaxNumber(page),
                            WebsiteAddress = GetLaboratoryWebsiteAddress(page),
                            Email = GetLaboratoryEmail(page)
                        };

                        lock (Laboratories) // Ensure thread-safe access to Laboratories collection
                        {
                            Laboratories.Add(laboratory);
                        }

                        Console.WriteLine($"{Laboratories.Count}'s laboratory read successfully");

                        fetchSuccessful = true;
                    }
                    catch (HtmlWebException ex)
                    {
                        Console.WriteLine("In getting laboratories information an HtmlWebException occurred: " + ex.Message);
                        retryCount++;
                        if (retryCount < maxRetries)
                            Thread.Sleep(10000);
                    }
                    catch (HttpRequestException ex)
                    {
                        Console.WriteLine("In getting laboratories information an HttpRequestException occurred: " + ex.Message);
                        retryCount++;
                        if (retryCount < maxRetries)
                            Thread.Sleep(10000);
                    }
                    catch (WebException ex)
                    {
                        Console.WriteLine("In getting laboratories information a WebException occurred: " + ex.Message);
                        retryCount++;
                        if (retryCount < maxRetries)
                            Thread.Sleep(10000);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("In getting laboratories information an error occurred: " + ex.Message);
                    }
                }
            });
        }
        private string GetLaboratoryName(HtmlDocument page)
        {
            if (_dataCheck.NameExists(page))
            {
                string labName = page.DocumentNode.SelectSingleNode("//*[@id='lab_view']/div[1]/h1").InnerText;
                return labName;
            }
            else
            {
                return "بدون نام";
            }
        }
        private string? GetLaboratoryStatus(HtmlDocument page)
        {
            if (_dataCheck.StatusExists(page))
            {
                string status = page.DocumentNode.SelectSingleNode("//span[@class='member_icon_grey']/following-sibling::span[1]").InnerText;
                return status.Substring(status.IndexOf(':') + 1, (status.Length - 1) - (status.IndexOf(':')));
            }
            else
            {
                return null;
            }
        }
        private string? GetLaboratoryLocation(HtmlDocument page)
        {
            if (_dataCheck.LocationExists(page))
            {
                string province = page.DocumentNode.SelectSingleNode("//span[@class='location_icon_grey']/following-sibling::span[@class='flexDiv']/i[1]").InnerText;
                string city = page.DocumentNode.SelectSingleNode("//span[@class='location_icon_grey']/following-sibling::span[@class='flexDiv']/i[2]").InnerText;
                return string.Concat(province + "، " + city);
            }
            else
            {
                return null;
            }
        }
        private string? GetLaboratoryManagerName(HtmlDocument page)
        {
            if (_dataCheck.ManagerExists(page))
            {
                string managerName = page.DocumentNode.SelectSingleNode("//span[@class='manager_icon_grey']/following-sibling::span[1]").InnerText;
                return managerName.Substring(managerName.IndexOf(':') + 1, (managerName.Length - 1) - (managerName.IndexOf(':')));
            }
            else
            {
                return null;
            }
        }
        private string? GetLaboratoryAgentName(HtmlDocument page)
        {
            if (_dataCheck.AgentExists(page))
            {
                string agentName = page.DocumentNode.SelectSingleNode("//span[@class='person_icon_grey']/following-sibling::span[1]").InnerText;
                return agentName.Substring(agentName.IndexOf(':') + 1, (agentName.Length - 1) - (agentName.IndexOf(':')));
            }
            else
            {
                return null;
            }
        }
        private string? GetLaboratoryTelephoneNumber(HtmlDocument page)
        {
            if (_dataCheck.TelephoneNumberExists(page))
            {
                string phoneNumber = page.DocumentNode.SelectSingleNode("//span[@class='tel_icon_grey']/following-sibling::span[1]/a").InnerText;
                return phoneNumber;
            }
            else
            {
                return null;
            }
        }
        private string? GetLaboratoryFaxNumber(HtmlDocument page)
        {
            if (_dataCheck.FaxNumberExists(page))
            {
                string faxNumber = page.DocumentNode.SelectSingleNode("//span[@class='fax_icon_grey']/following-sibling::span[1]").InnerText;
                return faxNumber.Substring(faxNumber.IndexOf(':') + 1, (faxNumber.Length - 1) - (faxNumber.IndexOf(':'))); ;
            }
            else
            {
                return null;
            }
        }
        private string? GetLaboratoryWebsiteAddress(HtmlDocument page)
        {
            if (_dataCheck.WebsiteAddressExists(page))
            {
                string websiteAddress = page.DocumentNode.SelectSingleNode("//span[@class='website_icon_grey']/following-sibling::span[1]/a").InnerText;
                return websiteAddress;
            }
            else
            {
                return null;
            }
        }
        private string? GetLaboratoryEmail(HtmlDocument page)
        {
            if (_dataCheck.EmailAddressExists(page))
            {
                string email = page.DocumentNode.SelectSingleNode("//span[@class='email_icon_grey']/following-sibling::span[1]/a").InnerText;
                return email;
            }
            else
            {
                return null;
            }
        }
    }
}
