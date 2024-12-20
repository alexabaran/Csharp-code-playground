//Part 1: File Operations

//Create a New File
//Write a function that creates a new text file called example.txt in a folder named TestFolder.
//Add some content (e.g., "This is a sample file.") to the file.

//Read File Content
//Write a function to read and print the content of example.txt.

//Append Content
//Add new content(e.g., "Adding more content.") to the same file.

//Copy the File
//Create a copy of example.txt named example_copy.txt.

//Move the File
//Move example_copy.txt to a new folder named BackupFolder.

//Rename the File
//Rename example.txt to example_renamed.txt.

//Delete a File
//Delete the example_copy.txt from BackupFolder.

//Read and Modify Attributes
//Write a function that reads and modifies the file attributes of example_renamed.txt. For example, set it to read-only and check its attributes.

//Part 2: Folder Operations

//Create a New Folder
//Write a function to create a folder named NewFolder inside TestFolder.

//List Folder Contents
//Write a function to list all files and subfolders inside TestFolder.

//Rename a Folder
//Rename NewFolder to RenamedFolder.

//Move a Folder

//Move RenamedFolder to BackupFolder.

//Delete a Folder
//Delete RenamedFolder from its new location.


//Additional Requirements
//Error Handling
//Add proper exception handling using try-catch blocks for all operations. Handle specific exceptions such as FileNotFoundException, IOException, UnauthorizedAccessException, etc.
//Resource Management
//Use using statements to ensure files are properly closed and resources are freed.

using System;
using System.IO;

class FileAndFolderHandling
{
    static void Main(string[] args)
    {
        try
        {
            // Part 1: File Operations

            //CreateFile();

            //ReadFile();

            //AppendFile();

            //CopyFile();

            //MoveFile();

            //RenameFile();

            //DeleteFile();

            //ModifyFileAttributes();


            // Part 2: Folder Operations

            //CreateFolder();

            //ListFolderContents();

            //RenameFolder();

            //MoveFolder();

            //DeleteFolder();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    // Method to create a file and write content
    static void CreateFile()
    {
        try
        {
            string folderPath = "TestFolder";
            Directory.CreateDirectory(folderPath); // Ensure folder exists
            string filePath = Path.Combine(folderPath, "example.txt");

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("This is a sample file.");
            }
            Console.WriteLine("File created successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating file: {ex.Message}");
        }
    }

    // Method to read file content
    static void ReadFile()
    {
        try
        {
            string filePath = "TestFolder/example.txt";
            using (StreamReader reader = new StreamReader(filePath))
            {
                string content = reader.ReadToEnd();
                Console.WriteLine("File content:");
                Console.WriteLine(content);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
        }
    }

    // Method to append content to a file
    static void AppendFile()
    {
        try
        {
            string filePath = "TestFolder/example.txt";
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("Adding more content.");
            }
            Console.WriteLine("Content appended successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error appending content: {ex.Message}");
        }
    }

    // Method to copy a file
    static void CopyFile()
    {
        try
        {
            string sourcePath = "TestFolder/example.txt";
            string destPath = "TestFolder/example_copy.txt";
            File.Copy(sourcePath, destPath, true);
            Console.WriteLine("File copied successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error copying file: {ex.Message}");
        }
    }

    // Method to move a file
    static void MoveFile()
    {
        try
        {
            string sourcePath = "TestFolder/example_copy.txt";
            string destPath = "BackupFolder/example_copy.txt";
            Directory.CreateDirectory("BackupFolder"); // Ensure destination folder exists
            File.Move(sourcePath, destPath);
            Console.WriteLine("File moved successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error moving file: {ex.Message}");
        }
    }

    // Method to rename a file
    static void RenameFile()
    {
        try
        {
            string oldPath = "TestFolder/example.txt";
            string newPath = "TestFolder/example_renamed.txt";
            File.Move(oldPath, newPath);
            Console.WriteLine("File renamed successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error renaming file: {ex.Message}");
        }
    }

    // Method to delete a file
    static void DeleteFile()
    {
        try
        {
            string filePath = "BackupFolder/example_copy.txt";
            File.Delete(filePath);
            Console.WriteLine("File deleted successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting file: {ex.Message}");
        }
    }

    // Method to modify file attributes
    static void ModifyFileAttributes()
    {
        try
        {
            string filePath = "TestFolder/example_renamed.txt";
            File.SetAttributes(filePath, FileAttributes.ReadOnly);
            FileAttributes attributes = File.GetAttributes(filePath);
            Console.WriteLine($"File attributes: {attributes}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error modifying file attributes: {ex.Message}");
        }
    }

    // Method to create a folder
    static void CreateFolder()
    {
        try
        {
            string folderPath = "TestFolder/NewFolder";
            Directory.CreateDirectory(folderPath);
            Console.WriteLine("Folder created successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating folder: {ex.Message}");
        }
    }

    // Method to list folder contents
    static void ListFolderContents()
    {
        try
        {
            string folderPath = "TestFolder";
            string[] files = Directory.GetFiles(folderPath);
            string[] directories = Directory.GetDirectories(folderPath);

            Console.WriteLine("Contents of the folder:");
            foreach (string dir in directories)
            {
                Console.WriteLine($"[Folder] {Path.GetFileName(dir)}");
            }
            foreach (string file in files)
            {
                Console.WriteLine($"[File] {Path.GetFileName(file)}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error listing folder contents: {ex.Message}");
        }
    }

    // Method to rename a folder
    static void RenameFolder()
    {
        try
        {
            string oldPath = "TestFolder/NewFolder";
            string newPath = "TestFolder/RenamedFolder";
            Directory.Move(oldPath, newPath);
            Console.WriteLine("Folder renamed successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error renaming folder: {ex.Message}");
        }
    }

    // Method to move a folder
    static void MoveFolder()
    {
        try
        {
            string sourcePath = "TestFolder/RenamedFolder";
            string destPath = "BackupFolder/RenamedFolder";
            Directory.CreateDirectory("BackupFolder"); // Ensure destination folder exists
            Directory.Move(sourcePath, destPath);
            Console.WriteLine("Folder moved successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error moving folder: {ex.Message}");
        }
    }

    // Method to delete a folder
    static void DeleteFolder()
    {
        try
        {
            string folderPath = "BackupFolder/RenamedFolder";
            Directory.Delete(folderPath, true); // Recursively delete
            Console.WriteLine("Folder deleted successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting folder: {ex.Message}");
        }
    }
}
