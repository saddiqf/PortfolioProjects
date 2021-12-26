/* Programming Assignment 7
 * Programmer: Saddiq Ford
 * Date: November 18, 2019
 * Purpose: The purpose of this program is to create two classes that implement and test display characteristics of the user's favorite location
 * */
using System;
using static System.Console;

namespace Assignment7
{
    class LocationApp
    {
        public static void Main()
        {
            DisplayInstructions();

            Location locationObjectOne = new Location();
            locationObjectOne.LocationName = ReturnName();
            locationObjectOne.LocationAddress = ReturnAddress();
            locationObjectOne.LocationDescription = ReturnDescription();
            locationObjectOne.LocationRating = ReturnRating();
            locationObjectOne.LocationComment = ReturnComment();

            Clear();
            WriteLine("Location One:");
            WriteLine(locationObjectOne);

            Location locationObjectTwo = new Location("Regency Mall", "1111 Onpine drive", "Fun place to shop", 7, "I would visit again");
            WriteLine("Location Two:");
            WriteLine(locationObjectTwo);

            Location locationObjectThree = new Location("Short Pump Mall", 9);
            WriteLine("Location Three:");
            WriteLine(" Location Name: {0}\n Location Rating: {1}", locationObjectThree.LocationName, locationObjectThree.LocationRating);

        }
        public static void DisplayInstructions()
        {
            WriteLine("The purpose of this program is to implement two classes that initially collects the information from the user in one class, and tests the information in another class.");
            WriteLine();
            WriteLine("The user will be asked to enter the name of their favorite location, the address of the location, a short description of the location, what they rate the lcoation (from 1 to 10), and any additional comments.");
            WriteLine();
            WriteLine("Press Enter to continue with the program");
            ReadKey();
            Clear();
        }
        public static string ReturnName()
        {
            
            string locName;
            Console.WriteLine("Enter the name of the location");
            locName = ReadLine();
            return locName;
        }
        public static string ReturnAddress()
        {

            string locAddress;
            Console.WriteLine("Enter the address of the location");
            locAddress = ReadLine();
            return locAddress;
        }
        public static string ReturnDescription()
        {

            string locDesc;
            Console.WriteLine("Enter the description of the location");
            locDesc = ReadLine();
            return locDesc;
        }
        public static int ReturnRating()
        {

            int locRating;
            Console.WriteLine("Enter the rating of the location");
            locRating = int.Parse(ReadLine());
            return locRating;

        }
        public static string ReturnComment()
        {

            string locComment;
            Console.WriteLine("Enter addition comments regarding the location");
            locComment = ReadLine();
            return locComment;

        }
    }

    class Location
    {
        private string locationName;
        private string locationAddress;
        private string locationDescription;
        private int locationRating;
        private string locationComment;

        public Location()
        {
        }
        public Location(string locName, string locAddress, string locDesc, int locRating, string locComment)
        {
            locationName = locName;
            locationAddress = locAddress;
            locationDescription = locDesc;
            locationRating = locRating;
            locationComment = locComment;
        }
        public Location(string locName, int locRating)
        {
            locationName = locName;
            locationRating = locRating;
        }

        public string LocationName
        {
            get
            {
                return locationName;
            }
            set
            {
                locationName = value;
            }
        }
        public string LocationAddress
        {
            get
            {
                return locationAddress;
            }
            set
            {
                locationAddress = value;
            }
        }
        public string LocationDescription
        {
            get
            {
                return locationDescription;
            }
            set
            {
                locationDescription = value;
            }
        }
        public int LocationRating
        {
            get
            {
                return locationRating;
            }
            set
            {
                locationRating = value;
            }

        }
        public string LocationComment
        {
            get
            {
                return locationComment;
            }
            set
            {
                locationComment = value;
            }
        }

        public override string ToString()
        {
            return 
                " Location Name: " + locationName
                + "\n Location Address: " + locationAddress
                + "\n Location Description: " + locationDescription
                + "\n Location Rating: " + locationRating
                + "\n Location Comment: " + locationComment;
        }
    }
}
