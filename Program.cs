using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;

/* Here started my program*/

// + overload.

class Expense
{
    public string Description { get; set; } = "";
    public decimal Amount { get; set; }
    public string Category { get; set; } = "";
    public DateTime Date { get; set; }
    public string? Note { get; set; }
}

bool exect = true; /* Checks whether the option is in the list and returns true by default*/

Console.WriteLine("==============================================");
Console.WriteLine("       Welcome to my Budget Expense Tracker   ");
Console.WriteLine("==============================================");
//Console.WriteLine("                Make your choice              ");
//Console.WriteLine(" 1) Add an expense    2) View summary    3) Set monthly budget    4) Exit");
while (exect)
{
    Console.WriteLine("      \nPlease choose an option:");
    Console.WriteLine("        1) Add an expense");
    Console.WriteLine("        2) View summary");
    Console.WriteLine("        3) Set monthly budget");
    Console.WriteLine("        4) Exit");
    Console.Write("             Enter your choice: ");

    string option = Console.ReadLine() ?? "";
    /* Star of treatment based on the choice made */
    /* Creation of processing functions  */
    switch (option)
    {
        case "1":
            Console.WriteLine("Add an expense selected.");
            AddExpense();
            break;
        case "2":
            Console.WriteLine("View summary selected.");
            ViewSummary();
        break;
        case "3":
            Console.WriteLine("Set monthly budget selected.");
            MonthBudget();
        break;
        case "4":
            Console.WriteLine("Thank you for using MyBudget!");
            exect = false;
        break;

        default:
            Console.WriteLine(
                           "Enter a number between 1 and 4.");
        break;
    }
}

private static void AddExpense() 
{ 
}
private static void ViewSummary()
{ 
}
private static void MonthBudget()
{
}