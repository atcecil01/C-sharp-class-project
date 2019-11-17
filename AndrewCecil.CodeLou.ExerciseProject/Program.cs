using System;
using System.Collections.Generic;

namespace AndrewCecil.CodeLou.ExerciseProject
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            while (exit == false)
            {
                Console.WriteLine();
                Console.WriteLine("Menu");
                Console.WriteLine("1. New Student");
                Console.WriteLine("2. List Students");
                Console.WriteLine("3. Find Student By Name");
                Console.WriteLine("4. Find Student By Class");
                Console.WriteLine("Any other number to exit");
                bool success = Int32.TryParse(Console.ReadLine(), out var selection);
                while (success == false)
                {
                    Console.WriteLine("Menu");
                    Console.WriteLine("1. New Student");
                    Console.WriteLine("2. List Students");
                    Console.WriteLine("3. Find Student By Name");
                    Console.WriteLine("4. Find Student By Class");
                    Console.WriteLine("Any other number to exit");
                    success = Int32.TryParse(Console.ReadLine(), out selection);
                }
                if (selection == 1)
                {
                    bool exit2 = false;
                    while (exit2 == false)
                    {
                        Console.WriteLine("Enter Student Id");
                        success = Int32.TryParse(Console.ReadLine(), out var studentId);
                        while (success == false)
                        {
                            Console.WriteLine("Enter a valid Student Id");
                            success = Int32.TryParse(Console.ReadLine(), out studentId);
                        }
                        //var studentId = Convert.ToInt32(Console.ReadLine());
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
                        StudentRepository.Add(studentRecord);

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
                    foreach (KeyValuePair<string, Student> item in StudentRepository.studentRepository)
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
                    var studentRecord = StudentRepository.Get(input);
                    Console.WriteLine($"Student Id | Name |  Class ");
                    Console.WriteLine($"{studentRecord.StudentId} | {studentRecord.FirstName} {studentRecord.LastName} | {studentRecord.ClassName} ");
                }
                else if (selection == 4)
                {
                    Console.WriteLine("Enter class: ");
                    var input = Console.ReadLine();
                    foreach (KeyValuePair<string, Student> item in StudentRepository.studentRepository)
                    {
                        var studentRecord = StudentRepository.Get(item.Key);
                        
                        if (studentRecord.ClassName.ToLower() == input.ToLower())
                        {
                            Console.WriteLine($"Student Id | Name |  Class ");
                            Console.WriteLine($"{studentRecord.StudentId} | {studentRecord.FirstName} {studentRecord.LastName} | {studentRecord.ClassName} ");
                        }
                    }
                }
                else
                {
                    exit = true;
                }
            }
        }
    }
}
