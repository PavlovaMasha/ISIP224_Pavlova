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

    public class ConsoleMenu
    {
        private University university;

        public ConsoleMenu()
        {
            university = new University();
        }

        public void Run()
        {
            while (true)
            {
                ShowMenu();

                string input = Console.ReadLine();
                int choice;

                if (!int.TryParse(input, out choice))
                {
                    Console.WriteLine("Ошибка: введите число от 0 до 11.");
                    Console.WriteLine();
                    continue;
                }

                if (choice == 0)
                {
                    Console.WriteLine("Работа программы завершена.");
                    return;
                }

                try
                {
                    HandleChoice(choice);
                }
                catch (ArgumentException exception)
                {
                    Console.WriteLine("Ошибка: " + exception.Message);
                }
                catch (InvalidOperationException exception)
                {
                    Console.WriteLine("Ошибка: " + exception.Message);
                }

                Console.WriteLine();
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("СИСТЕМА УПРАВЛЕНИЯ УНИВЕРСИТЕТОМ");
            Console.WriteLine("1. Добавить студента");
            Console.WriteLine("2. Показать всех студентов");
            Console.WriteLine("3. Курсы конкретного студента");
            Console.WriteLine("4. Записать студента на курс");
            Console.WriteLine("5. Добавить преподавателя");
            Console.WriteLine("6. Показать всех преподавателей");
            Console.WriteLine("7. Назначить преподавателя на курс");
            Console.WriteLine("8. Добавить курс");
            Console.WriteLine("9. Показать все курсы");
            Console.WriteLine("10. Информация о курсе");
            Console.WriteLine("11. Студенты конкретного курса");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите действие: ");
        }

        private void HandleChoice(int choice)
        {
            switch (choice)
            {
                case 1:
                    AddStudent();
                    break;

                case 2:
                    ShowStudents();
                    break;

                case 3:
                    ShowStudentCourses();
                    break;

                case 4:
                    EnrollStudent();
                    break;

                case 5:
                    AddTeacher();
                    break;

                case 6:
                    ShowTeachers();
                    break;

                case 7:
                    AssignTeacher();
                    break;

                case 8:
                    AddCourse();
                    break;

                case 9:
                    ShowCourses();
                    break;

                case 10:
                    ShowCourseDetails();
                    break;

                case 11:
                    ShowCourseStudents();
                    break;

                default:
                    Console.WriteLine("Такого пункта меню не существует.");
                    break;
            }
        }

        private string ReadText(string message)
        {
            while (true)
            {
                Console.Write(message);

                string text = Console.ReadLine();

                if (text != null)
                    text = text.Trim();

                if (!string.IsNullOrWhiteSpace(text))
                    return text;

                Console.WriteLine("Ошибка: значение не может быть пустым. Попробуйте снова.");
            }
        }

        private int ReadNumber(string message, int min, int max)
        {
            while (true)
            {
                Console.Write(message);

                string text = Console.ReadLine();
                int number;

                if (int.TryParse(text, out number) && number >= min && number <= max)
                    return number;

                Console.WriteLine("Ошибка: введите целое число от " + min + " до " + max + ".");
            }
        }

        private Faculty ReadFaculty()
        {
            Console.WriteLine("Выберите факультет:");
            Console.WriteLine("1. ComputerScience");
            Console.WriteLine("2. Mathematics");
            Console.WriteLine("3. Physics");
            Console.WriteLine("4. Economics");

            int number = ReadNumber("Выберите номер: ", 1, 4);

            return (Faculty)(number - 1);
        }

        private AcademicDegree ReadDegree()
        {
            Console.WriteLine("Выберите учёную степень:");
            Console.WriteLine("1. Bachelor");
            Console.WriteLine("2. Master");
            Console.WriteLine("3. CandidateOfSciences");
            Console.WriteLine("4. DoctorOfSciences");

            int number = ReadNumber("Выберите номер: ", 1, 4);

            return (AcademicDegree)(number - 1);
        }

        private void AddStudent()
        {
            Console.WriteLine("Добавление студента");

            string lastName = ReadText("Введите фамилию: ");
            string firstName = ReadText("Введите имя: ");
            string patronymic = ReadText("Введите отчество: ");
            int age = ReadNumber("Введите возраст (18-100): ", 18, 100);
            Faculty faculty = ReadFaculty();

            Student student = university.AddStudent(lastName, firstName, patronymic, age, faculty);

            Console.WriteLine("Студент добавлен:");
            Console.WriteLine(student.GetInfo());
        }

        private void ShowStudents()
        {
            Console.WriteLine();
            Console.WriteLine("СПИСОК СТУДЕНТОВ");

            if (university.Students.Count == 0)
            {
                Console.WriteLine("Студенты не добавлены.");
                return;
            }

            foreach (Student student in university.Students)
            {
                Console.WriteLine(student.GetInfo());
            }
        }

        private void ShowStudentCourses()
        {
            int studentId = ReadNumber("Введите ID студента: ", 1, 1000000);

            Student student = university.FindStudent(studentId);

            if (student == null)
            {
                Console.WriteLine("Ошибка: студент с таким ID не найден.");
                return;
            }

            List<Course> courses = university.GetStudentCourses(studentId);

            Console.WriteLine();
            Console.WriteLine("КУРСЫ СТУДЕНТА: " + student.GetFullName());

            if (courses.Count == 0)
            {
                Console.WriteLine("Студент не записан ни на один курс.");
                return;
            }

            foreach (Course course in courses)
            {
                Console.WriteLine(course.GetInfo());
            }
        }

        private void EnrollStudent()
        {
            int studentId = ReadNumber("Введите ID студента: ", 1, 1000000);
            int courseId = ReadNumber("Введите ID курса: ", 1, 1000000);

            university.EnrollStudent(studentId, courseId);

            Console.WriteLine("Студент успешно записан на курс.");
        }

        private void AddTeacher()
        {
            Console.WriteLine("Добавление преподавателя");

            string lastName = ReadText("Введите фамилию: ");
            string firstName = ReadText("Введите имя: ");
            string patronymic = ReadText("Введите отчество: ");
            int age = ReadNumber("Введите возраст (14-100): ", 14, 100);
            AcademicDegree degree = ReadDegree();

            Teacher teacher = university.AddTeacher(lastName, firstName, patronymic, age, degree);

            Console.WriteLine("Преподаватель добавлен:");
            Console.WriteLine(teacher.GetInfo());
        }

        private void ShowTeachers()
        {
            Console.WriteLine();
            Console.WriteLine("СПИСОК ПРЕПОДАВАТЕЛЕЙ");

            if (university.Teachers.Count == 0)
            {
                Console.WriteLine("Преподаватели не добавлены.");
                return;
            }

            foreach (Teacher teacher in university.Teachers)
            {
                Console.WriteLine(teacher.GetInfo());
            }
        }

        private void AssignTeacher()
        {
            int teacherId = ReadNumber("Введите ID преподавателя: ", 1, 1000000);
            int courseId = ReadNumber("Введите ID курса: ", 1, 1000000);

            university.AssignTeacher(teacherId, courseId);

            Console.WriteLine("Преподаватель успешно назначен на курс.");
        }

        private void AddCourse()
        {
            Console.WriteLine("Добавление курса");

            string title = ReadText("Введите название курса: ");
            Faculty faculty = ReadFaculty();
            int maxStudents = ReadNumber("Введите максимальное число студентов: ", 1, 1000);

            Course course = university.AddCourse(title, faculty, maxStudents);

            Console.WriteLine("Курс добавлен:");
            Console.WriteLine(course.GetInfo());
        }

        private void ShowCourses()
        {
            Console.WriteLine();
            Console.WriteLine("СПИСОК КУРСОВ");

            if (university.Courses.Count == 0)
            {
                Console.WriteLine("Курсы не добавлены.");
                return;
            }

            foreach (Course course in university.Courses)
            {
                Console.WriteLine(course.GetInfo());
            }
        }

        private void ShowCourseDetails()
        {
            int courseId = ReadNumber("Введите ID курса: ", 1, 1000000);

            Course course = university.FindCourse(courseId);

            if (course == null)
            {
                Console.WriteLine("Ошибка: курс с таким ID не найден.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("===== ДЕТАЛЬНАЯ ИНФОРМАЦИЯ О КУРСЕ =====");
            Console.WriteLine(course.GetInfo());

            string teacherName = course.Teacher == null ? "не назначен" : course.Teacher.GetFullName();

            Console.WriteLine("Преподаватель: " + teacherName);
            Console.WriteLine("Записано студентов: " + course.Students.Count);
            Console.WriteLine("Свободных мест: " + (course.MaxStudents - course.Students.Count));
        }

        private void ShowCourseStudents()
        {
            int courseId = ReadNumber("Введите ID курса: ", 1, 1000000);

            Course course = university.FindCourse(courseId);

            if (course == null)
            {
                Console.WriteLine("Ошибка: курс с таким ID не найден.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("СТУДЕНТЫ КУРСА");

            if (course.Students.Count == 0)
            {
                Console.WriteLine("На этот курс пока никто не записан.");
                return;
            }

            foreach (Student student in course.Students)
            {
                Console.WriteLine(student.GetInfo());
            }
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
