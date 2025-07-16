// C# Family Age Calculator Program

using System;
using System.Collections.Generic;
using System.Linq;

namespace HelloWorld
{
    // Class to represent a family member
    public class FamilyMember
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age => DateTime.Now.Year - DateOfBirth.Year - 
                         (DateTime.Now.DayOfYear < DateOfBirth.DayOfYear ? 1 : 0);

        public FamilyMember(string name, DateTime dateOfBirth)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
        }

        public override string ToString()
        {
            return $"{Name} (Age: {Age}, Born: {DateOfBirth:yyyy-MM-dd})";
        }
    }

    // Class to manage family members and age calculations
    public class FamilyManager
    {
        private List<FamilyMember> familyMembers;

        public FamilyManager()
        {
            familyMembers = new List<FamilyMember>();
        }

        public void AddFamilyMember(string name, DateTime dateOfBirth)
        {
            familyMembers.Add(new FamilyMember(name, dateOfBirth));
        }

        public string GetEldestMember()
        {
            if (familyMembers.Count == 0)
                return "No family members found.";

            if (familyMembers.Count == 1)
                return $"Eldest family member: {familyMembers[0]}";

            // Check if all members have the same birth date
            var firstBirthDate = familyMembers[0].DateOfBirth;
            if (familyMembers.All(fm => fm.DateOfBirth == firstBirthDate))
            {
                return $"All family members are of the same age ({familyMembers[0].Age} years old)";
            }

            var eldest = familyMembers.OrderBy(fm => fm.DateOfBirth).First();
            return $"Eldest family member: {eldest}";
        }

        public string GetYoungestMember()
        {
            if (familyMembers.Count == 0)
                return "No family members found.";

            if (familyMembers.Count == 1)
                return $"Youngest family member: {familyMembers[0]}";

            // Check if all members have the same birth date
            var firstBirthDate = familyMembers[0].DateOfBirth;
            if (familyMembers.All(fm => fm.DateOfBirth == firstBirthDate))
            {
                return $"All family members are of the same age ({familyMembers[0].Age} years old)";
            }

            var youngest = familyMembers.OrderByDescending(fm => fm.DateOfBirth).First();
            return $"Youngest family member: {youngest}";
        }

        public void DisplayAllMembers()
        {
            Console.WriteLine("\n--- Family Members ---");
            foreach (var member in familyMembers.OrderBy(fm => fm.DateOfBirth))
            {
                Console.WriteLine(member);
            }
        }

        public int GetMemberCount()
        {
            return familyMembers.Count;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Family Age Calculator ===");
            Console.WriteLine("This program helps you find the eldest and youngest family members.\n");

            FamilyManager familyManager = new FamilyManager();

            // Get number of family members
            int numberOfMembers = GetNumberOfMembers();

            // Collect family member information
            for (int i = 1; i <= numberOfMembers; i++)
            {
                Console.WriteLine($"\n--- Family Member {i} ---");
                
                string name = GetMemberName(i);
                DateTime dateOfBirth = GetMemberDateOfBirth(name);
                
                familyManager.AddFamilyMember(name, dateOfBirth);
            }

            // Display results
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("RESULTS:");
            Console.WriteLine(new string('=', 50));
            
            familyManager.DisplayAllMembers();
            
            Console.WriteLine("\n" + new string('-', 50));
            
            string eldestResult = familyManager.GetEldestMember();
            string youngestResult = familyManager.GetYoungestMember();
            
            // Check if all members are the same age
            if (eldestResult == youngestResult && eldestResult.Contains("All family members are of the same age"))
            {
                Console.WriteLine(eldestResult);
            }
            else
            {
                Console.WriteLine(eldestResult);
                Console.WriteLine(youngestResult);
            }
            
            Console.WriteLine(new string('-', 50));
            
            Console.WriteLine($"\nTotal family members processed: {familyManager.GetMemberCount()}");
            Console.WriteLine("\nProgram completed successfully!");
        }

        static int GetNumberOfMembers()
        {
            int numberOfMembers;
            while (true)
            {
                Console.Write("Enter the number of family members: ");
                string? input = Console.ReadLine();
                
                if (!string.IsNullOrEmpty(input) && int.TryParse(input, out numberOfMembers) && numberOfMembers > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter a valid positive number.");
                }
            }
            return numberOfMembers;
        }

        static string GetMemberName(int memberNumber)
        {
            string name;
            while (true)
            {
                Console.Write($"Enter name for member {memberNumber}: ");
                name = Console.ReadLine()?.Trim() ?? "";
                
                if (!string.IsNullOrEmpty(name))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter a valid name.");
                }
            }
            return name;
        }

        static DateTime GetMemberDateOfBirth(string name)
        {
            DateTime dateOfBirth;
            while (true)
            {
                Console.Write($"Enter date of birth for {name} (yyyy-mm-dd): ");
                string? input = Console.ReadLine();
                
                if (!string.IsNullOrEmpty(input) && DateTime.TryParseExact(input, "yyyy-MM-dd", null, 
                    System.Globalization.DateTimeStyles.None, out dateOfBirth))
                {
                    // Validate that the date is not in the future
                    if (dateOfBirth <= DateTime.Now)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Date of birth cannot be in the future. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid date in yyyy-mm-dd format (e.g., 1990-05-15).");
                }
            }
            return dateOfBirth;
        }
    }
}

