using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP224_Pavlova
{
    public enum Faculty
    {
        ComputerScience,
        Mathematics,
        Physics,
        Economics
    }

    public enum AcademicDegree
    {
        Bachelor,
        Master,
        CandidateOfSciences,
        DoctorOfSciences
    }

    public abstract class Person
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public int Age { get; set; }

        public string GetFullName()
        {
            return LastName + " " + FirstName + " " + Patronymic;
        }

        protected Person(int id, string lastName, string firstName, string patronymic, int age)
        {
            if (id <= 0)
                throw new ArgumentException("ID должен быть больше нуля.");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Фамилия не может быть пустой.");

            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("Имя не может быть пустым.");

            if (string.IsNullOrWhiteSpace(patronymic))
                throw new ArgumentException("Отчество не может быть пустым.");

            if (age < 18 || age > 100)
                throw new ArgumentException("Возраст должен быть от 18 до 100 лет.");

            Id = id;
            LastName = lastName;
            FirstName = firstName;
            Patronymic = patronymic;
            Age = age;
        }

        public abstract string GetInfo();
    }

    public class Student : Person
    {
        public Faculty Faculty { get; set; }

        public Student(int id, string lastName, string firstName, string patronymic,
                       int age, Faculty faculty)
            : base(id, lastName, firstName, patronymic, age)
        {
            Faculty = faculty;
        }

        public override string GetInfo()
        {
            return "Студент #" + Id + ": " + GetFullName() + ", " + Age + " лет, факультет: " + Faculty;
        }
    }

    public class Teacher : Person
    {
        public AcademicDegree Degree { get; set; }

        public Teacher(int id, string lastName, string firstName, string patronymic,
                       int age, AcademicDegree degree)
            : base(id, lastName, firstName, patronymic, age)
        {
            Degree = degree;
        }

        public override string GetInfo()
        {
            return "Преподаватель #" + Id + ": " + GetFullName() + ", " + Age + " лет, степень: " + Degree;
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
