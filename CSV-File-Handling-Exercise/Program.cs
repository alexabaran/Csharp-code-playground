//Exercise 1: CSV File Handling

//Tasks:
//Creating CSV File
//Add a new record:
//Allow the user to input a new student record(ID, Name, Age, Grade) and append it to the file.
//Delete a record:
//Allow the user to delete a record based on the student's ID.
//Update a record:
//Allow the user to modify a specific field (e.g., Age or Grade) for a given student ID.
//Find a record:
//Search for and display records by:
//Student's name.
//Grade.
//Read all records:
//Display all records in a tabular format.

internal class Program
{
    private static void Main(string[] args)
    {
        string filePath = "students.csv";

        //Creating CSV file if not exists
        CSVCreate(filePath);

        bool appRunning = true;
        while (appRunning)
        {
            Console.WriteLine();
            ShowMenu(filePath);
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "0":
                    Console.Clear();
                    // Diplay records in tabular form
                    Console.WriteLine($"Goodbye!");
                    appRunning = false;
                    break;
                case "1":
                    Console.Clear();
                    DisplayRecords(filePath);
                    continue;
                case "2":
                    Console.Clear();
                    // Record adding with automatic ID
                    AddRecord(filePath);
                    DisplayRecords(filePath);
                    continue;
                case "3":
                    Console.Clear();
                    // Deleting record based on students ID
                    DeleteRecord(filePath);
                    // Updating ID
                    Console.WriteLine($"Update ID numbers");
                    UpdateID(filePath);
                    DisplayRecords(filePath);
                    continue;
                case "4":
                    Console.Clear();
                    // Updating Record
                    UpdateRecord(filePath);
                    DisplayRecords(filePath);
                    continue;
                case "5":
                    Console.Clear();
                    // Finding record
                    FindRecordByName(filePath);
                    DisplayRecords(filePath);
                    continue;
                default:
                    Console.Clear(); 
                    Console.WriteLine($"please write correct number!");
                    continue;
            }
        }
    }

    static void CSVCreate(string filePath)
    {
        try
        {
            if (File.Exists(filePath)) // checking if file exists
            {
                Console.WriteLine($"File {filePath} exists");
            }
            else
            {
                // Create new file
                using (StreamWriter file = new StreamWriter(filePath))
                {
                    file.WriteLine("ID,Name,Age,Grade"); // Header
                    Console.WriteLine("File created");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    static void AddRecord(string filePath)
    {
        // asking user for input

        string[] lines = File.ReadAllLines(filePath);
        int id = lines.Length;
        //Console.WriteLine("Please give ID: ");
        //string id = Console.ReadLine();
        Console.WriteLine("Please give a name: ");
        string name = Console.ReadLine();
        Console.WriteLine("Please give an age: ");
        string age = Console.ReadLine();
        Console.WriteLine("Please give a grade: ");
        string grade = Console.ReadLine();

        if (File.Exists(filePath)) //checking if file exists
        {
            // Add a new record from user input
            using (StreamWriter file = new StreamWriter(filePath, true))
            {

                file.WriteLine($"{id},{name},{age},{grade}");
            }
            Console.WriteLine("Record added successfully.");
        }
        else
        {
            Console.WriteLine("File does not exist. Please create the file first.");
        }
    }

    static void DeleteRecord(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File does not exist.");
                return;
            }

            // reading all lines and seperating header:
            Console.WriteLine("Please give ID number to delete: ");
            int id = int.Parse(Console.ReadLine());
            var lines = File.ReadAllLines(filePath);
            var header = lines.FirstOrDefault();
            var data = lines.Skip(1);

            // Checking where ID fits the record
            var updatedData = data.Where(line => !line.StartsWith($"{id}"));

            // Checking if records were removed
            if (data.Count() == updatedData.Count())
            {
                Console.WriteLine($"No record found with ID {id}.");
                return;
            }

            // Write the updated content back to the file
            File.WriteAllLines(filePath, new[] { header }.Concat(updatedData));
            Console.WriteLine($"Record with ID {id} has been deleted.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    static void UpdateRecord(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File does not exist.");
                return;
            }

            // reading all lines and seperating header:
            Console.WriteLine("Please give ID number to update: ");
            int id = int.Parse(Console.ReadLine());
            var lines = File.ReadAllLines(filePath);
            var header = lines.FirstOrDefault();
            var data = lines.Skip(1).ToList();

            // Find the record to edit
            var recordIndex = data.FindIndex(line => line.StartsWith($"{id}"));
            if (recordIndex == -1)
            {
                Console.WriteLine($"No record found with ID {id}.");
                return;
            }

            // Display the current record
            Console.WriteLine("Current record: " + data[recordIndex]);

            // Ask for new data
            Console.Write("Enter new Name: ");
            string newName = Console.ReadLine();
            Console.Write("Enter new Age: ");
            int newAge = int.Parse(Console.ReadLine());
            Console.Write("Enter new Grade: ");
            string newGrade = Console.ReadLine();

            // Update record
            var updatedRecord = $"{id},{newName},{newAge},{newGrade}";
            data[recordIndex] = updatedRecord;

            // Write the updated data back to the file
            File.WriteAllLines(filePath, new[] { header }.Concat(data));
            Console.WriteLine($"Record with ID {id} has been updated.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    static void UpdateID(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File does not exist.");
                return;
            }

            // reading all lines and seperating header:
            var lines = File.ReadAllLines(filePath);
            var header = lines.FirstOrDefault();
            var data = lines.Skip(1).ToList();

            // Find the record to edit
            for (int i = 0; i < data.Count; i++)
            {
                string[] fields = data[i].Split(',');

                int id = int.Parse(fields[0]);
                if (id == (i + 1))
                {
                    var oldRecord = $"{fields[0]},{fields[1]},{fields[2]},{fields[3]}";
                    data[i] = oldRecord;
                }
                else
                {
                    var updatedRecord = $"{i + 1},{fields[1]},{fields[2]},{fields[3]}";
                    data[i] = updatedRecord;
                }
            }
            // Write the updated data back to the file
            File.WriteAllLines(filePath, new[] { header }.Concat(data));
            Console.WriteLine($"Records IDs have been updated.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    static void FindRecordByName(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File does not exist.");
                return;
            }

            // reading all lines and seperating header:
            var lines = File.ReadAllLines(filePath);
            var header = lines.FirstOrDefault();
            var data = lines.Skip(1).ToList();

            Console.WriteLine("Please give name: ");
            string searchedName = Console.ReadLine();

            var foundElement = false;
            // Find the record to edit
            for (int i = 0; i < data.Count; i++)
            {
                string[] fields = data[i].Split(',');

                string name = fields[1];
                if (name.ToLower() == searchedName.ToLower())
                {
                    Console.WriteLine($"Name {fields[1]} exist in data. ID: {fields[0]},Age: {fields[2]}, Grade:{fields[3]}");
                    foundElement = true;
                    break;
                }
            }
            if (!foundElement) Console.WriteLine($"Name {searchedName} doesn't exist in data");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    static void DisplayRecords(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File does not exist.");
            return;
        }

        // Read all lines from the file
        var lines = File.ReadAllLines(filePath);

        if (lines.Length == 0)
        {
            Console.WriteLine("The file is empty.");
            return;
        }

        // Display the header row
        var header = lines[0].Split(',');
        Console.WriteLine($"\n{header[0],-5} {header[1],-20} {header[2],-5} {header[3],-6}");
        Console.WriteLine(new string('-', 40)); // Separator line

        // Display each record in a formatted table
        for (int i = 1; i < lines.Length; i++) // Skip the header row
        {
            var fields = lines[i].Split(',');
            Console.WriteLine($"{fields[0],-5} {fields[1],-20} {fields[2],-5} {fields[3],-6}");
        }
    }

    static void ShowMenu(string filePath)
    {
        Console.WriteLine($"What do you want to do with {filePath}?");
        Console.WriteLine($"Choose: ");
        Console.WriteLine($"0 - close");
        Console.WriteLine($"1 - show records");
        Console.WriteLine($"2 - add new record");
        Console.WriteLine($"3 - delete record");
        Console.WriteLine($"4 - update record");
        Console.WriteLine($"5 - find record by name");
    }
}