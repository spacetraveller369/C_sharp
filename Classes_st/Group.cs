using System;
using System.Collections.Generic;
using System.Linq;

namespace Classes_st;

// Наследование интерфейса ICloneable
class Group : ICloneable
{
    private List<Student> _students;

    private string _groupName;
    private string _groupSpecialist;
    private int _courseNumber;

    public string GroupName
    {
        get => _groupName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Group name cannot be null or empty.");
            _groupName = value;
        }
    }

    public string GroupSpecialist
    {
        get => _groupSpecialist;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Specialization cannot be null or empty.");
            _groupSpecialist = value;
        }
    }

    public int CourseNumber
    {
        get => _courseNumber;
        set
        {
            if (value < 1 || value > 2100)
                throw new ArgumentException("Course number is invalid.");
            _courseNumber = value;
        }
    }

    // c-tor
    public Group() : this("Default Group", "Default Specialist", 1) { }

    public Group(string groupName, string groupSpecialist, int courseNumber)
    {
        _students = new List<Student>();

        SetGroupName(groupName);
        SetGroupSpecialist(groupSpecialist);
        SetCourseNumber(courseNumber);
    }

    // ICloneable
    public object Clone()
    {
        Group clonedGroup = new Group(this.GroupName, this.GroupSpecialist, this.CourseNumber);

        foreach (var student in this._students)
        {
            clonedGroup.AddStudent((Student)student.Clone());
        }

        return clonedGroup;
    }
    //

    public void SetGroupName(string groupName) => GroupName = groupName;
    public void SetGroupSpecialist(string groupSpecialist) => GroupSpecialist = groupSpecialist;
    public void SetCourseNumber(int courseNumber) => CourseNumber = courseNumber;

    public string GetGroupName() => GroupName;
    public string GetGroupSpecialist() => GroupSpecialist;
    public int GetCourseNumber() => CourseNumber;

    // Вспомогательный геттер для демонстрационных тестов клонирования
    public List<Student> GetStudents() => _students;

    public void ShowGroup()
    {
        Console.WriteLine($"Group: {_groupName}, Specialisation: {_groupSpecialist}, Course: {_courseNumber}");
        Console.WriteLine("Students:");

        _students.Sort((s1, s2) => string.Compare(s1.GetSurname(), s2.GetSurname(), StringComparison.Ordinal));

        for (int i = 0; i < _students.Count; i++)
        {
            Console.WriteLine($"{i + 1}.{_students[i].GetSurname()} {_students[i].GetName()}");
        }
    }

    public void AddStudent(Student student)
    {
        _students.Add(student);
    }

    public void EditStudentData(Student student, string surname, string addess, string telNumber)
    {
        student.SetSurname(surname);
        student.SetAddress(addess);
        student.SetTelNumber(telNumber);
    }

    public void RemoveStudent(List<Student> students)
    {
        foreach (Student student in students)
        {
            if (!CheckGradeExam(student, student.GetGradesExam()))
            {
                students.Remove(student);
            }
        }
    }

    public void RemoveStudentsWithLowGrades()
    {
        _students = _students.Where(student => CheckGradeExam(student, student.GetGradesExam())).ToList();
    }

    public bool CheckGradeExam(Student student, List<int> gradesExam)
    {
        if (gradesExam == null || gradesExam.Count == 0)
            return false;

        int gradeSum = 0;
        foreach (int grade in gradesExam)
        {
            gradeSum += grade;
        }

        double avgGrade = (double)gradeSum / gradesExam.Count;
        return avgGrade >= 50;
    }
}