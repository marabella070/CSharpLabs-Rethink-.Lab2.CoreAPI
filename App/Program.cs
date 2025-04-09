using Lab2.CoreAPI.Core.Models;
using Lab2.CoreAPI.Core.Randomizers;













var assemblyWorkshop = AssemblyWorkshopRandomizer.Generate((1000, 600));
assemblyWorkshop.ShowInfo(Console.WriteLine);
Console.WriteLine();

/*
var manufacturingWorkshop = ManufacturingWorkshopRandomizer.Generate();
manufacturingWorkshop.ShowInfo(Console.WriteLine);
Console.WriteLine();
*/





