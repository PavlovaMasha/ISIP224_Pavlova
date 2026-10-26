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

    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Faculty Faculty { get; set; }
        public int MaxStudents { get; set; }
        public Teacher Teacher { get; set; }
        public List<Student> Students { get; set; }

        public Course(int id, string title, Faculty faculty, int maxStudents)
        {
            if (id <= 0)
                throw new ArgumentException("ID курса должен быть больше нуля.");

            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Название курса не может быть пустым.");

            if (maxStudents <= 0)
                throw new ArgumentException("Количество мест должно быть больше нуля.");

            Id = id;
            Title = title;
            Faculty = faculty;
            MaxStudents = maxStudents;
            Students = new List<Student>();
        }

        public bool HasStudent(int studentId)
        {
            return Students.Any(student => student.Id == studentId);
        }

        public bool IsFull()
        {
            return Students.Count >= MaxStudents;
        }

        public string GetInfo()
        {
            string teacherName = Teacher == null ? "не назначен" : Teacher.GetFullName();

            return "Курс #" + Id + ": " + Title + ", факультет: " + Faculty +
                   ", мест: " + Students.Count + "/" + MaxStudents +
                   ", преподаватель: " + teacherName;
        }
    }

    public class University
    {
        public List<Student> Students { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<Course> Courses { get; set; }

        public University()
        {
            Students = new List<Student>();
            Teachers = new List<Teacher>();
            Courses = new List<Course>();
        }

        public Student AddStudent(string lastName, string firstName, string patronymic,
                                  int age, Faculty faculty)
        {
            int id = Students.Count == 0 ? 1 : Students.Max(student => student.Id) + 1;

            Student student = new Student(id, lastName, firstName, patronymic, age, faculty);

            Students.Add(student);

            return student;
        }

        public Teacher AddTeacher(string lastName, string firstName, string patronymic,
                                  int age, AcademicDegree degree)
        {
            int id = Teachers.Count == 0 ? 1 : Teachers.Max(teacher => teacher.Id) + 1;

            Teacher teacher = new Teacher(id, lastName, firstName, patronymic, age, degree);

            Teachers.Add(teacher);

            return teacher;
        }

        public Course AddCourse(string title, Faculty faculty, int maxStudents)
        {
            int id = Courses.Count == 0 ? 1 : Courses.Max(course => course.Id) + 1;

            Course course = new Course(id, title, faculty, maxStudents);

            Courses.Add(course);

            return course;
        }

        public Student FindStudent(int id)
        {
            return Students.FirstOrDefault(student => student.Id == id);
        }

        public Teacher FindTeacher(int id)
        {
            return Teachers.FirstOrDefault(teacher => teacher.Id == id);
        }

        public Course FindCourse(int id)
        {
            return Courses.FirstOrDefault(course => course.Id == id);
        }

        public void EnrollStudent(int studentId, int courseId)
        {
            Student student = FindStudent(studentId);

            if (student == null)
                throw new InvalidOperationException("Студент с таким ID не найден.");

            Course course = FindCourse(courseId);

            if (course == null)
                throw new InvalidOperationException("Курс с таким ID не найден.");

            if (course.HasStudent(studentId))
                throw new InvalidOperationException("Студент уже записан на этот курс.");

            if (course.IsFull())
                throw new InvalidOperationException("На курсе нет свободных мест.");

            course.Students.Add(student);
        }

        public void AssignTeacher(int teacherId, int courseId)
        {
            Teacher teacher = FindTeacher(teacherId);

            if (teacher == null)
                throw new InvalidOperationException("Преподаватель с таким ID не найден.");

            Course course = FindCourse(courseId);

            if (course == null)
                throw new InvalidOperationException("Курс с таким ID не найден.");

            course.Teacher = teacher;
        }

        public List<Course> GetStudentCourses(int studentId)
        {
            return Courses
                .Where(course => course.HasStudent(studentId))
                .ToList();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
