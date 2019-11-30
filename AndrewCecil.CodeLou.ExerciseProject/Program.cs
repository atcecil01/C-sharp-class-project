using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;


namespace AndrewCecil.CodeLou.ExerciseProject
{
    class Program
    {
        static void Main(string[] args)
        {
            //Load students from Json file
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);
            var fileName = Path.Combine(directory.FullName, "students.json");
            var fileContents = DeserializeFromFile(fileName);
            foreach (var student in fileContents)
            {
                //Add studentRecord to StudentRepository
                StudentRepository.Add(student.Key, student.Value);
                Console.WriteLine(student.Key + "||" + student.Value);
            }

            bool exit = false;
            while (exit == false)
            {
                int selection = MainMenu();
                if (selection == 1)
                {
                    bool exit2 = false;
                    while (exit2 == false)
                    {
                        Console.WriteLine("Enter Student Id");
                        bool success = Int32.TryParse(Console.ReadLine(), out var studentId);
                        if (success == true)
                        {
                            success = !StudentRepository.studentRepository.ContainsKey(studentId);
                            if (success == false)
                            {
                                Console.Write("Duplicate key detected. ");
                            }
                        }
                        while (success == false)
                        {
                            Console.WriteLine("Enter a valid Student Id");
                            success = Int32.TryParse(Console.ReadLine(), out studentId);
                            if (success == true)
                            {
                                success = !StudentRepository.studentRepository.ContainsKey(studentId);
                                if (success == false)
                                {
                                    Console.Write("Duplicate key detected. ");
                                }
                            }
                        }
                        Console.WriteLine("Enter First Name");
                        var studentFirstName = Console.ReadLine();
                        Console.WriteLine("Enter Last Name");
                        var studentLastName = Console.ReadLine();
                        Console.WriteLine("Enter Class Name");
                        var className = Console.ReadLine();
                        Console.WriteLine("Enter Last Class Completed");
                        var lastClass = Console.ReadLine();
                        Console.WriteLine("Enter Last Class Completed Date in format MM/dd/YYYY");
                        success = DateTimeOffset.TryParse(Console.ReadLine(), out var lastCompletedOn);
                        while (success == false)
                        {
                            Console.WriteLine("Enter Last Class Completed Date in format MM/dd/YYYY");
                            success = DateTimeOffset.TryParse(Console.ReadLine(), out lastCompletedOn);
                        }
                        Console.WriteLine("Enter Start Date in format MM/dd/YYYY");
                        success = DateTimeOffset.TryParse(Console.ReadLine(), out var startDate);
                        while (success == false)
                        {
                            Console.WriteLine("Enter Start Date in format MM/dd/YYYY");
                            success = DateTimeOffset.TryParse(Console.ReadLine(), out startDate);
                        }

                        var studentRecord = new Student();
                        studentRecord.StudentId = studentId;
                        studentRecord.FirstName = studentFirstName;
                        studentRecord.LastName = studentLastName;
                        studentRecord.ClassName = className;
                        studentRecord.StartDate = startDate;
                        studentRecord.LastClassCompleted = lastClass;
                        studentRecord.LastClassCompletedOn = lastCompletedOn;

                        //Add studentRecord to StudentRepository
                        StudentRepository.Add(studentId, studentRecord);

                        Console.WriteLine("Continue? Y/N: ");
                        var entry = Console.ReadKey();
                        if (entry.Key == ConsoleKey.N)
                        {
                            exit2 = true;
                        }
                        else
                        {
                            Console.WriteLine();
                        }
                    }
                }
                else if (selection == 2)
                {
                    foreach (KeyValuePair<int, Student> item in StudentRepository.studentRepository)
                    {
                        var studentRecord = StudentRepository.Get(item.Key);
                        Console.WriteLine($"Student Id | Name |  Class ");
                        Console.WriteLine($"{studentRecord.StudentId} | {studentRecord.FirstName} {studentRecord.LastName} | {studentRecord.ClassName} ");
                    }
                }
                else if (selection == 3)
                {
                    Console.WriteLine("Enter a student name: ");
                    var input = Console.ReadLine();
                    foreach (KeyValuePair<int, Student> item in StudentRepository.studentRepository)
                    {
                        var studentRecord = StudentRepository.Get(item.Key);
                        var name = studentRecord.FirstName + " " + studentRecord.LastName;
                        if (name.ToLower() == input.ToLower())
                        {
                            Console.WriteLine($"Student Id | Name |  Class ");
                            Console.WriteLine($"{studentRecord.StudentId} | {studentRecord.FirstName} {studentRecord.LastName} | {studentRecord.ClassName} ");
                        }
                    }
                    //var studentRecord = StudentRepository.Get(input);
                    //Console.WriteLine($"Student Id | Name |  Class ");
                    //Console.WriteLine($"{studentRecord.StudentId} | {studentRecord.FirstName} {studentRecord.LastName} | {studentRecord.ClassName} ");
                }
                else if (selection == 4)
                {
                    Console.WriteLine("Enter class: ");
                    var input = Console.ReadLine();
                    foreach (KeyValuePair<int, Student> item in StudentRepository.studentRepository)
                    {
                        var studentRecord = StudentRepository.Get(item.Key);
                        
                        if (studentRecord.ClassName.ToLower() == input.ToLower())
                        {
                            Console.WriteLine($"Student Id | Name |  Class ");
                            Console.WriteLine($"{studentRecord.StudentId} | {studentRecord.FirstName} {studentRecord.LastName} | {studentRecord.ClassName} ");
                        }
                    }
                }

                else if (selection == 5)
                {
                    SaveToFile();
                    Console.WriteLine("Entries saved to file");
                }
                else
                {
                    exit = true;
                }
            }
        }
        public static int MainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Menu");
            Console.WriteLine("1. New Student");
            Console.WriteLine("2. List Students");
            Console.WriteLine("3. Find Student By Name");
            Console.WriteLine("4. Find Student By Class");
            Console.WriteLine("5. Save to file");
            Console.WriteLine("Any other number to exit");
            bool success = Int32.TryParse(Console.ReadLine(), out var selection);
            while (success == false)
            {
                Console.WriteLine("Menu");
                Console.WriteLine("1. New Student");
                Console.WriteLine("2. List Students");
                Console.WriteLine("3. Find Student By Name");
                Console.WriteLine("4. Find Student By Class");
                Console.WriteLine("5. Save to file");
                Console.WriteLine("Any other number to exit");
                success = Int32.TryParse(Console.ReadLine(), out selection);
            }

            return selection;
        }

        public static void SaveToFile()
        {
        // Save to json file
        string currentDirectory = Directory.GetCurrentDirectory();
        DirectoryInfo directory = new DirectoryInfo(currentDirectory);
        var fileName = Path.Combine(directory.FullName, "students.json");
        SerializeToFile(StudentRepository.studentRepository, fileName);
        }

        public static void SerializeToFile(Dictionary<int,Student> students, string fileName)
        {
            var serializer = new JsonSerializer();
            using (var writer = new StreamWriter(fileName))
            using (var jsonWriter = new JsonTextWriter(writer))
            {
                serializer.Serialize(jsonWriter, students);
            }
        }

        public static Dictionary<int, Student> DeserializeFromFile(string fileName)
        {
            var students = new Dictionary<int, Student>();
            var serializer = new JsonSerializer();
            using (var reader = new StreamReader(fileName))
            using (var jsonReader = new JsonTextReader(reader))
            {
                students = serializer.Deserialize<Dictionary<int, Student>>(jsonReader);
            }
            return students;
        }
    }
}
