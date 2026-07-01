using System;
using System.Collections.Generic;

namespace Classes_st;

internal class Program
{
    static void Main(string[] args)
    {
     
        try
        {
            Student student1 = new Student(
                    "София",
                    "Доу",
                    "-",
                    new DateTime(1999, 1, 13),
                    "Нью_Васюки",
                    "1234567890",
                    new List<int>(), new List<int>(), new List<int> { 39, 60, 35 }
                );

            Student student2 = new Student(
                   "Платон",
                   "Блек",
                   "Александрович",
                   new DateTime(1993, 12, 15),
                   "Днепр",
                   "0999999999",
                   new List<int>(), new List<int>(), new List<int> { 50, 70, 80 }
               );

            Student student3 = new Student(
                "Олена",
                "Вандекамп",
                "-",
                new DateTime(1950, 2, 28),
                "Киев",
                "1201204556",
                new List<int>(), new List<int>(), new List<int> { 85, 88, 12 }
            );

            Console.WriteLine("Student 1 details:");
            student1.ShowStudent();
            Console.WriteLine();

            Console.WriteLine("Student 2 details:");
            student2.ShowStudent();
            Console.WriteLine();

            Console.WriteLine("Student 3 details:");
            student3.ShowStudent();
            Console.WriteLine();

            var group = new Group();
            group.GroupName = "PV21"; 
            group.GroupSpecialist = "Geography";
            group.CourseNumber = 2025;

            group.AddStudent(student1);
            group.AddStudent(student2);
            group.AddStudent(student3);

            Console.WriteLine("Group details before removing low-performing students:");
            group.ShowGroup();
            Console.WriteLine();

            group.RemoveStudentsWithLowGrades();

            Console.WriteLine("Group details after removing low-performing students:");
            group.ShowGroup();
            Console.WriteLine();

            group.EditStudentData(student1, "Сьюзан", "New Address", "9876543210");

            Console.WriteLine("Updated Student 1 details:");
            student1.ShowStudent();
            Console.WriteLine();

            Console.WriteLine("- Тест генерации исключения -");
            student1.Name = "";

        }
        catch (ArgumentException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n(Перехвачено исключение): {ex.Message}");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n(Непредвиденная ошибка): {ex.Message}");
            Console.ResetColor();
        }
    }
}