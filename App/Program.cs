using Lab2.CoreAPI.Core.Models;














var assemblyWorkshop = AssemblyWorkshopRandomizer.Generate();
assemblyWorkshop.ShowInfo(Console.WriteLine);
Console.WriteLine();

var manufacturingWorkshop = ManufacturingWorkshopRandomizer.Generate();
manufacturingWorkshop.ShowInfo(Console.WriteLine);
Console.WriteLine();






