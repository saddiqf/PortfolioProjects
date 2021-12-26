/* Programming Assignment Six
 * Programmer: Saddiq Ford
 * Date: November 6, 2019
 * Purpose: the purpose of this program is to create a POS application that has at least four parallel arrays.
 * */
using System;
using static System.Console;
namespace Assignment6
{
    class POS
    {
        public static void Main()
        {
            DisplayInstructions();

            string[] itemName = new string[4];
            itemName = ReturnName();
            Clear();

            decimal[] itemPrice = new decimal[4];
            itemPrice = ReturnPrice();
            Clear();

            int[] itemStock = new int[4];
            itemStock = ReturnStock();
            Clear();

            double[] itemRating = new double[4];
            itemRating = ReturnRating();
            Clear();

            
            DisplayResults(itemName, itemPrice, itemStock, itemRating);
            DisplayAnItem(itemName, itemPrice, itemStock, itemRating);

        }
        public static void DisplayInstructions()
        {
            WriteLine("The Purpose of this program is to create a POS that captures four items that a user has and allows them to see the description of an item of their choice.");
            WriteLine("The user will input the name of each item, the price of each item, how many of each item are in stock, and their rating for each item.");
            WriteLine("Then, the user will be prompted to enter a number (between 1 and 4) that will display the description of the item of choice");
            WriteLine("Example: After you fill out the information, if you want to see the full description of the first item, press 1.");
            WriteLine();
            WriteLine("If you understand the instructions, press enter to continue");
            ReadKey();
            Clear();
        }
        public static string[] ReturnName()
        {
            string[] itemName = new string[4];
            for (int i = 0; i < itemName.Length; i++)
            {
                Console.WriteLine("Enter item number {0}", i + 1);
                itemName[i] = ReadLine();
            }
            return itemName;
        }
        public static decimal[] ReturnPrice()
        {
            decimal[] itemPrice = new decimal[4];
            for (int i = 0; i < itemPrice.Length; i++)
            {
               Console.WriteLine("Enter the price for item number {0}", i + 1);
               itemPrice[i] = decimal.Parse(ReadLine());
            }
            return itemPrice;
            
        }
        public static int[] ReturnStock()
        {
            int[] itemStock = new int[4];
            for (int i = 0; i < itemStock.Length; i++)
            {
                Console.WriteLine("Enter the amount of items in stock for item number {0}", i + 1);
                itemStock[i] = int.Parse(ReadLine());
            }
            return itemStock;
        }
        public static double[] ReturnRating()
        {
            double[] itemRating = new double[4];
            for (int i = 0; i < itemRating.Length; i++)
            {
                Console.WriteLine("Enter the rating for item number {0}", i + 1);
                itemRating[i] = double.Parse(ReadLine());
            }
            return itemRating;
        }
        public static void DisplayResults(string[] itemName, decimal[] itemPrice, int[] itemStock, double[] itemRating)
        {
            WriteLine("Here is each item that you entered in order:");
            foreach (string val in itemName)
            {
                WriteLine(val);
            }
            WriteLine("Here is the price of each item that you entered in order:");
            foreach (decimal val in itemPrice)
            {
                WriteLine("{0:C}", val);
            }
            WriteLine("Here is the amount of items in stock for each item in stock in order:");
            foreach (int val in itemStock)
            {
                WriteLine(val);
            }
            WriteLine("Here is the rating for each item listed above in order:");
            foreach (double val in itemRating)
            {
                WriteLine("{0:F2}", val);
            }
            WriteLine("To find out the description of one of the four items above, follow the directions below.");
            WriteLine();
        }
        public static void DisplayAnItem(string[] itemName, decimal[] itemPrice, int[] itemStock, double[] itemRating)
        {

            int userInput;
            Write("Enter the number of the item above if you would like to see it's full description: ");
            
            userInput = int.Parse(ReadLine());
            Console.WriteLine("Item\tPrice\tStock\tRating\t");
            switch (userInput)
            {
                case 1: WriteLine("{0}\t{1:C}\t{2}\t{3:F2}\t",itemName[0], itemPrice[0], itemStock[0], itemRating[0]); break;
                case 2: WriteLine("{0}\t{1:C}\t{2}\t{3:F2}\t",itemName[1], itemPrice[1], itemStock[1], itemRating[1]); break;
                case 3: WriteLine("{0}\t{1:C}\t{2}\t{3:F2}\t",itemName[2], itemPrice[2], itemStock[2], itemRating[2]); break;
                case 4: WriteLine("{0}\t{1:C}\t{2}\t{3:F2}\t",itemName[3], itemPrice[3], itemStock[3], itemRating[3]); break;
            }
        }
    }
}
