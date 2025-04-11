using Lab2.CoreAPI.Core.Randomizers;
using CoreAPI.Core.Helpers;

Type type = typeof(RandomMovementRandomizer); 

Console.WriteLine($"Type: \"{type}\"\n");

//! FIELDS OUTPUT
List<string> typeFields = ReflectionHelper.GetFieldsInfo(type);

Console.WriteLine("Fields:");
foreach (var field in typeFields)
{
    Console.WriteLine(field);

}
Console.WriteLine();

//! PROPERTIES OUTPUT
List<string> typeProperties = ReflectionHelper.GetPropertiesInfo(type);

Console.WriteLine("Properties:");
foreach (var property in typeProperties)
{
    Console.WriteLine(property);

}
Console.WriteLine();

//! INDEXERS OUTPUT
List<string> typeIndexers = ReflectionHelper.GetIndexersInfo(type);

Console.WriteLine("Indexers:");
foreach (var indexer in typeIndexers)
{
    Console.WriteLine(indexer);

}
Console.WriteLine();

//! METHODS OUTPUT
List<string> typeMethods = ReflectionHelper.GetMethodsInfo(type);

Console.WriteLine("Methods:");
foreach (var method in typeMethods)
{
    Console.WriteLine(method);

}
Console.WriteLine();


//                                                                                         __         ____     
//                                                   __    __  ____ ___  ____ __________ _/ /_  ___  / / /___ _
//                                                __/ /___/ /_/ __ `__ \/ __ `/ ___/ __ `/ __ \/ _ \/ / / __ `/
//                                               /_  __/_  __/ / / / / / /_/ / /  / /_/ / /_/ /  __/ / / /_/ / 
//                                                /_/   /_/ /_/ /_/ /_/\__,_/_/   \__,_/_.___/\___/_/_/\__,_/  
