using PathfindingLab1.ConsoleApp;
using PathfindingLab1.ConsoleApp.Exceptions;

try
{
    Menu.ShowMenu();
}
catch (WrongInputException exception)
{
    Console.WriteLine(exception.Message);
}
catch (ValidationException exception)
{
    Console.WriteLine(exception.Message);
}
catch (Exception exception)
{
    Console.WriteLine(exception.Message);
}