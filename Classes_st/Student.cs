using System;
using System.Collections.Generic;

namespace Classes_st;

class Student
{
    private string _name;
    private string _surname;
    private string _patronymic;
    private string _address;
    private string _telNumber;

    private DateTime _birthday;

    private List<int> _gradesCredit = new List<int>();
    private List<int> _gradesCourseWork = new List<int>();
    private List<int> _gradesExam = new List<int>();

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Name cannot be null or empty.");
            _name = value;
        }
    }

    public string Surname
    {
        get => _surname;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Surname cannot be null or empty.");
            _surname = value;
        }
    }

    public string Patronymic
    {
        get => _patronymic;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Patronymic cannot be null or empty.");
            _patronymic = value;
        }
    }

    public string Address
    {
        get => _address;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Address cannot be null or empty.");
            _address = value;
        }
    }

    public string TelNumber
    {
        get => _telNumber;
        set
        {
            if (value == null || value.Length < 10 || value.Length > 15)
                throw new ArgumentException("Telephone number must be between 10 and 15 characters long.");
            _telNumber = value;
        }
    }

    public DateTime Birthday
    {
        get => _birthday;
        set
        {
            DateTime today = DateTime.Now;
            if (value > today)
                throw new ArgumentException("Birthday cannot be in the future.");

            int age = today.Year - value.Year;
            if (age < 15 || age > 100)
                throw new ArgumentException("Age is out of realistic range (15-100).");

            _birthday = value;
        }
    }

    public List<int> GradesCredit
    {
        get => _gradesCredit;
        set
        {
            if (value == null) throw new ArgumentNullException(nameof(value), "Grades list cannot be null.");
            _gradesCredit.Clear();
            foreach (int grade in value)
            {
                if (grade >= 0 && grade <= 100) _gradesCredit.Add(grade);
            }
        }
    }

    public List<int> GradesCourseWork
    {
        get => _gradesCourseWork;
        set
        {
            if (value == null) throw new ArgumentNullException(nameof(value), "Grades list cannot be null.");
            _gradesCourseWork.Clear();
            foreach (int grade in value)
            {
                if (grade >= 0 && grade <= 50) _gradesCourseWork.Add(grade);
            }
        }
    }

    public List<int> GradesExam
    {
        get => _gradesExam;
        set
        {
            if (value == null) throw new ArgumentNullException(nameof(value), "Grades list cannot be null.");
            _gradesExam.Clear();
            foreach (int grade in value)
            {
                if (grade >= 0 && grade <= 100) _gradesExam.Add(grade);
            }
        }
    }

    public Student()
        : this("Name", "Surname", "Patronymic", DateTime.Now, "undefined", "undefined")
    {
    }

    public Student(string name, string surname, string patronymic, DateTime birthday, string address, string telNumber)
        : this(name, surname, patronymic, birthday, address, telNumber, new List<int>(), new List<int>(), new List<int>())
    {
    }

    public Student(string name, string surname, string patronymic, DateTime birthday, string address, string telNumber, List<int> gradesCredit,
        List<int> gradesCourseWork, List<int> gradesExam)
    {
        SetName(name);
        SetSurname(surname);
        SetPatronymic(patronymic);
        SetBirthday(birthday);
        SetAddress(address);
        SetTelNumber(telNumber);
        SetGradesCredit(gradesCredit);
        SetGradeCourseWork(gradesCourseWork);
        SetGradesExam(gradesExam);
    }

    public void SetName(string name) => Name = name;
    public void SetSurname(string surname) => Surname = surname;
    public void SetPatronymic(string patronymic) => Patronymic = patronymic;
    public void SetAddress(string address) => Address = address;
    public void SetTelNumber(string telNumber) => TelNumber = telNumber;
    public void SetBirthday(DateTime birthday) => Birthday = birthday;
    public void SetGradesCredit(List<int> grades) => GradesCredit = grades;
    public void SetGradeCourseWork(List<int> grades) => GradesCourseWork = grades;
    public void SetGradesExam(List<int> grades) => GradesExam = grades;

    public string GetName() => Name;
    public string GetSurname() => Surname;
    public string GetPatronymic() => Patronymic;
    public string GetAddress() => Address;
    public string GetTelNumber() => TelNumber;
    public DateTime GetBirthday() => Birthday;
    public List<int> GetGradesCredit() => GradesCredit;
    public List<int> GetGradesCourseWork() => GradesCourseWork;
    public List<int> GetGradesExam() => GradesExam;

    public override string ToString() =>
          $"Surname: {_surname}\n" +
          $"Name: {_name}\n" +
          $"Patronymic: {_patronymic}\n" +
          $"Birthday: {_birthday}\n" +
          $"Address: {_address}\n" +
          $"Phone number: {_telNumber}\n" +
          $"Grades credit: {string.Join(", ", _gradesCredit)}\n" +
          $"Grades course work: {string.Join(", ", _gradesCourseWork)}\n" +
          $"Grades exam: {string.Join(", ", _gradesExam)}";

    public void ShowStudent()
    {
        Console.WriteLine(ToString());
    }
}