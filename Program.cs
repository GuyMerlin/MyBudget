using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;

/* Here started my program*/

class Expense
{
    public string Description { get; set; } = "";
    public decimal Amount { get; set; }
    public ExpenseCategoryType Category { get; set; } = "";
    public DateTime Date { get; set; }
    public string? Note { get; set; }
    public static Expense (String Description,
                             decimal Amount,
                             ExpenseType Category,
                             DateOnly Date,
                             String Note)
    {
        Description = description;
        Amount = amount;
        Category = category;
        Date = date;
        Note = string.IsNullOrWhiteSpace(note) ? null : note.Trim();
    }
}
public enum ExpenseCategoryType
{
    Food,
    Utilities,
    Transport,
    Entertainment,
    Other
}

bool exect = true; /* Checks whether the option is in the list and returns true by default*/

Console.WriteLine("==============================================");
Console.WriteLine("       Welcome to my Budget Expense Tracker   ");
Console.WriteLine("==============================================");

while (exect)
{
    Console.WriteLine("      \nPlease choose an option:");
    Console.WriteLine("        1) Add an expense");
    Console.WriteLine("        2) View summary");
    Console.WriteLine("        3) Set monthly budget");
    Console.WriteLine("        4) Exit");
    Console.Write("             Enter your choice: ");

    string option = Console.ReadLine() ?? "";
    /* Start of treatment based on the choice made */
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
            EnterMonthlyBudget();
        break;
        case "4":
            Console.WriteLine("Thank you for using MyBudget!");
            exect = false;
        break;

        default:
            Console.WriteLine(  "Enter a number between 1 and 4.");
        break;
    }
    //if (exect)
    //{
    //    Console.WriteLine();
    //}
}

//Method that is treatened Expenses
//Budget Management and Approval of Amounts
public static void  AddExpense ()
{
    //public static string 
}
private static void ViewSummary()
{ 

}
private static void EnterMonthlyBudget()
{

}