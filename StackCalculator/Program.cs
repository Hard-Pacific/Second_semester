using StackCalculator;

Console.WriteLine("Enter the string, that you want to calculate (in postfix form):");

var stringForCalculator = Console.ReadLine();

if (stringForCalculator == null)
{
    Console.WriteLine("Incorrect input");
    return;
}

var stackOnArray = new StackOnArray();

StackCalculator.StackCalculator stackOnArrayCalculator = new StackCalculator.StackCalculator(stackOnArray);

(float resultOnStackOnArray, bool checkOfCorrectWorkForStackOnArray)
    = stackOnArrayCalculator.CalculateInPostfixForm(stringForCalculator);

if (!checkOfCorrectWorkForStackOnArray)
{
    Console.WriteLine("Incorrect string. Try again :)");
    return;
}

Console.WriteLine($"Result from calculator with stack on array: {resultOnStackOnArray};");

var stackOnList = new StackOnList();

StackCalculator.StackCalculator stackOnListCalculator = new StackCalculator.StackCalculator(stackOnList);

(float resultOnStackOnList, bool checkOfCorrectWorkForStackOnList)
    = stackOnListCalculator.CalculateInPostfixForm(stringForCalculator);

if (!checkOfCorrectWorkForStackOnList)
{
    Console.WriteLine("Incorrect string. Try again :)");
    return;
}

Console.WriteLine($"Result from calculator with stack on list: {resultOnStackOnList}.");
