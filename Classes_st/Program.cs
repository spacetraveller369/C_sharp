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

            var group = new Group();
            group.GroupName = "PV21";
            group.GroupSpecialist = "Geography";
            group.CourseNumber = 2025;

            group.AddStudent(student1);
            group.AddStudent(student2);

            Console.WriteLine("Исходная группы");
            group.ShowGroup();
            Console.WriteLine();

            // Тестирование клонирования
            Console.WriteLine("Выполняется тестирование клонирования");
            Group clonedGroup = (Group)group.Clone();

            Console.WriteLine("\n Меняем оригинальные данные");
           
            group.GroupName = "New changed name";
            
            group.GetStudents()[0].Surname = "Изменена_в_оригинале";

            Console.WriteLine("\n Оригинальная группа псле изменений");
            group.ShowGroup();
            Console.WriteLine();

            Console.WriteLine(" Клонированная группа");
            clonedGroup.ShowGroup();
            Console.WriteLine();
            // 

            // Демонстрация старого перехвата исключений
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