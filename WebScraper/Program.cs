using WebScraper.Operations;

ReadLaboratoriesPagesLinks readLaboratoriesPagesLinks = new(@"https://labsnet.ir/labs");
Console.WriteLine("Getting the links of laboratory information pages");
UserInterface.Waiting();
List<string> links = readLaboratoriesPagesLinks.GetPagesLinks();
Console.WriteLine(links.Count);
UserInterface.Done();

ReadLaboratoryInfo readLaboratoryInfo = new(new DataCheck());
Console.WriteLine("Getting the information of laboratories");
UserInterface.Waiting();
readLaboratoryInfo.GetLaboratoriesInformation(links);
UserInterface.Done();

SaveData saveData = new(readLaboratoryInfo.Laboratories);
Console.WriteLine("Saving the information of laboratories into a csv file");
UserInterface.Waiting();
saveData.SaveLaboratoryInformationToCSV();
UserInterface.Done();
