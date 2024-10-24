using Colors.Net.StringColorExtensions;
using Colors.Net;
using System.Timers;


IUserRepository _userRepository = new UserRepository();
ICourseRepository _courseRepository = new CourseRepository();
UserService _userService = new UserService();
bool _isRunning = true;
bool isUserMenuRunning = true;

Run();

void Run()
{
    while (_isRunning)
    {
        ShowMainMenu();
    }
}

void ShowMainMenu()
{
    ColoredConsole.WriteLine("                                         ****Welcome To Golestan*****".DarkGreen());
    ColoredConsole.WriteLine("1. Register ".Blue());
    ColoredConsole.WriteLine("2. Login".Blue());
    ColoredConsole.WriteLine("3. Exit".DarkRed());
    string option = Console.ReadLine();
    switch (option)
    {
        case "1":
            RegisterUser();
            break;
        case "2":
            Login();
            break;
        case "3":
            _isRunning = false;
            break;
        default:
            ColoredConsole.WriteLine("Invalid Option".DarkRed());
            break;
    }
}

void RegisterUser()
{
    ColoredConsole.WriteLine("******* Register User *******".DarkMagenta());
    ColoredConsole.Write("First Name: ".Blue());
    string firstName = Console.ReadLine();
    ColoredConsole.Write("Last Name: ".Blue());
    string lastName = Console.ReadLine();
    ColoredConsole.Write("Email: ".Blue());
    string email = Console.ReadLine();
    ColoredConsole.Write("Username: ".Blue());
    string username = Console.ReadLine();
    ColoredConsole.Write("Mobile: ".Blue());
    int mobile = Convert.ToInt32(Console.ReadLine());
    ColoredConsole.Write("Address: ".Blue());
    string address = Console.ReadLine();
    ColoredConsole.Write("Password: ".Blue());
    string password = Console.ReadLine();
    ColoredConsole.Write("Choose role (1- Student, 2- Teacher, 3- Operator): ".DarkMagenta());
    string input = Console.ReadLine();
    RoleEnum role = input switch
    {
        "1" => RoleEnum.Student,
        "2" => RoleEnum.Teacher,
        "3" => RoleEnum.Operator,
    };

    User user = role switch
    {
        RoleEnum.Student => new Student(firstName, lastName, email, username, mobile, address, password),
        RoleEnum.Teacher => new Teacher(firstName, lastName, email, username, mobile, address, password),
        RoleEnum.Operator => new Operator(firstName, lastName, email, username, mobile, address, password),
    };
    ColoredConsole.WriteLine($"User Role {role} Created Successfully.".DarkGreen());

    if (user != null)
    {
        bool isRegistered = _userService.RegisterUser(user);
        if (isRegistered)
        {
            ColoredConsole.WriteLine("Successfully registered".Green());
        }
        else
        {
            ColoredConsole.WriteLine("User not registered.".DarkRed());
        }
    }
    else
    {
        ColoredConsole.WriteLine("User not registered.".DarkRed());
    }
}

void Login()
{
    isUserMenuRunning = true;
    ColoredConsole.WriteLine("*******Login*******".DarkBlue());
    ColoredConsole.Write("Username: ".Blue());
    string username = Console.ReadLine();
    ColoredConsole.Write("Password: ".Blue());
    string password = Console.ReadLine();
    var user = _userService.Login(username, password);
    if (user != null)
    {
        if (!user.IsActived)
        {
            ColoredConsole.WriteLine("Your Account Is Inactive.".DarkCyan());
            return;
        }
        ShowUserMenu(user);
    }
}

void ShowUserMenu(User user)
{
    while (isUserMenuRunning)
    {
        ColoredConsole.WriteLine($"\n   ****Welcome, {user.FirstName}******".DarkGreen());
        switch (user)
        {
            case Teacher teacher:
                ShowTeacherMenu(teacher);
                break;
            case Operator operatorUser:
                ShowOperatorMenu(operatorUser);
                break;
            case Student student:
                ShowStudentMenu(student);
                break;
            default:
                ColoredConsole.WriteLine("Unknown user role.".DarkRed());
                break;
        }
    }
}


void ShowTeacherMenu(Teacher teacher)
{
    while (isUserMenuRunning)
    {
        ColoredConsole.WriteLine("\n**********   Teacher Menu  **********".DarkCyan());
        ColoredConsole.WriteLine("1. Add Course: ".Cyan());
        ColoredConsole.WriteLine("2. Shoe Corse: ".Cyan());
        ColoredConsole.WriteLine("3. Logout".DarkRed());
        string option = Console.ReadLine();
        switch (option)
        {
            case "1":
                AddCourse(teacher);
                break;
            case "2":
                ViewCourseTeacher(teacher);
                break;
            case "3":
                InMemoryDB.CurrentUser = null;
                isUserMenuRunning = false;
                return;
            default:
                ColoredConsole.WriteLine("Invalid Option".DarkRed());
                break;
        }

    }
}

void ShowOperatorMenu(Operator operatorUser)
{
    while (isUserMenuRunning)
    {
        ColoredConsole.WriteLine("\n**********Operator Menu**********".DarkGray());
        ColoredConsole.WriteLine("1. Show Users".Gray());
        ColoredConsole.WriteLine("2. Activate User".Gray());
        ColoredConsole.WriteLine("3. Deactivate User".Gray());
        ColoredConsole.WriteLine("4. Update Capacity: ".Gray());
        ColoredConsole.WriteLine("5. ReduceCapacity: ".Gray());
        ColoredConsole.WriteLine("6. Logout".DarkRed());
        string option = Console.ReadLine();
        switch (option)
        {
            case "1":
                ShowUsers();
                break;
            case "2":
                ActivateUser();
                break;
            case "3":
                DeactivateUser();
                break;
            case "4":
                UpdateCapacity();
                break;
            case "5":
                ReduceCapacity();
                return;

            case "6":
                InMemoryDB.CurrentUser = null;
                isUserMenuRunning = false;
                return;
            default:
                Console.WriteLine("Invalid Option".DarkRed());
                break;
        }
    }
}

void ShowStudentMenu(Student student)
{
    while (isUserMenuRunning)
    {
        ColoredConsole.WriteLine("\nStudent Menu:".DarkMagenta());
        ColoredConsole.WriteLine("1. Show Courses".Blue());
        ColoredConsole.WriteLine("2. Select In Course".Blue());
        ColoredConsole.WriteLine("3. Show My Courses".Blue());
        ColoredConsole.WriteLine("4. Logout".DarkRed());
        string option = Console.ReadLine();
        switch (option)
        {
            case "1":
                ShowCourses();
                break;
            case "2":
                SelectInCourse(student);
                break;
            case "3":

                ColoredConsole.WriteLine($"Courses Enrolled By {student.FirstName}:".DarkGray());
                _courseRepository.ShowStudentCourses(student);
                foreach (var course in student.EnrolledCourses)
                {
                    ColoredConsole.WriteLine($"ID: {course.Id}/ Name: {course.Name} /  Capacity: {course.Capacity} / Unit: {course.Unit} / StartTime:{course.CourseTimeStart} / EndTime:{course.CourseTimeEnd}".Green());
                }
                break;
            case "4":
                InMemoryDB.CurrentUser = null;
                isUserMenuRunning = false;
                break;
            default:
                Console.WriteLine("Invalid Option".DarkRed());
                break;
        }

    }
}

void ShowCourses()
{
    ColoredConsole.WriteLine("*******All Courses******".Green());
    var courses = _courseRepository.GetAllCourses();
    if (courses.Count == 0)
    {
        ColoredConsole.WriteLine("No Courses".DarkRed());
        return;
    }
    foreach (var course in courses)
    {
        ColoredConsole.WriteLine($"ID: {course.Id} / Name: {course.Name} / Teacher: {course.Teacher.FirstName} {course.Teacher.LastName} / StartTime: {course.CourseTimeStart} / EndTime: {course.CourseTimeEnd} / Capacity: {course.Capacity} / Enrolled: {course.Students.Count} / Unit: {course.Unit}".DarkGray());
    }
}

void SelectInCourse(Student student)
{
    ColoredConsole.WriteLine("Select In  Course:".Gray());
    ShowCourses();
    ColoredConsole.Write("Enter Course ID To Select: ".Gray());
    int courseId = Convert.ToInt32(Console.ReadLine());
    var course = _courseRepository.GetCourseById(courseId);
    if (course.Students.Count >= course.Capacity)
    {
        ColoredConsole.WriteLine($"Course {course.Name} Full".DarkRed());
        return;
    }

    if (student.EnrolledCourses.Sum(c => c.Unit) + course.Unit > 20)
    {
        ColoredConsole.WriteLine($"{student.FirstName} Can not {course.Name}/Only Unit<20".DarkRed());
        return;
    }

    foreach (var enrolledCourse in student.EnrolledCourses)
    {
        if (course.CourseTimeStart < enrolledCourse.CourseTimeEnd && course.CourseTimeEnd > enrolledCourse.CourseTimeStart)
        {
            ColoredConsole.WriteLine($"Time Course {course.Name} Time Interference {enrolledCourse.Name}".DarkRed());
            return;
        }
    }
    if (student.EnrolledCourses.Any(c => c.Id == course.Id))
    {
        ColoredConsole.WriteLine($"You Have Already Chosen {course.Name}".DarkRed());
        return;
    }
    if (course.Capacity == 0)
    {
        ColoredConsole.WriteLine($"Capacity Of {course.Name} Full".DarkRed());
    }
    if (course == null)
    {
        ColoredConsole.WriteLine("Course Not Found".DarkRed());
        return;
    }
    _courseRepository.SelectStudentInCourse(student, courseId);

    ColoredConsole.WriteLine($"{student.FirstName} : {course.Name} Course Successfully".DarkGreen());
}


void AddCourse(Teacher teacher)
{
    ColoredConsole.WriteLine("****Add New Course****".DarkGreen());
    ColoredConsole.Write("Course Name: ".Blue());
    string courseName = Console.ReadLine();
    ColoredConsole.Write("Prerequisite: ".Blue());
    string prerequisite = Console.ReadLine();
    ColoredConsole.Write("Unit: ".Blue());
    int unit = Convert.ToInt32(Console.ReadLine());
    if (unit <= 0)
    {
        ColoredConsole.WriteLine("Wrong.Unit>0".DarkRed());

    }
    ColoredConsole.Write("Capacity:".Blue());
    int capacity = Convert.ToInt32(Console.ReadLine());
    if (capacity <= 0)
    {
        ColoredConsole.WriteLine("Wrong.capacity>0".DarkRed());
    }
    DateTime startTime = CreateDateTime();
    ColoredConsole.WriteLine($"Course Start Time: {startTime}".DarkCyan());
    DateTime endTime = CreateDateTime();
    ColoredConsole.WriteLine($"Course End Time: {endTime}".DarkGray());
    _courseRepository.CreateCourse(courseName, prerequisite, capacity, startTime, endTime, teacher, unit);
    ColoredConsole.WriteLine($"Course '{courseName}' added successfully".DarkGreen());

}

void ViewCourseTeacher(Teacher teacher)
{
    ColoredConsole.WriteLine("******** Your Courses ********".Green());
    var courses = _userRepository.ShowTeacherCourses(teacher);

    if (courses.Count == 0)
    {
        ColoredConsole.WriteLine("No Courses Found For This Teacher.".DarkRed());
        return;
    }
    foreach (var course in courses)
    {
        ColoredConsole.WriteLine($"ID: {course.Id} / Name: {course.Name} / Enrolled: {course.Students.Count} / Unit: {course.Unit} / Prerequisite: {course.Prerequisite} / StartTime: {course.CourseTimeStart} / EndTime: {course.CourseTimeEnd}".DarkCyan());

        if (course.Students.Count > 0)
        {
            ColoredConsole.WriteLine("Enrolled Students:".Yellow());
            foreach (var student in course.Students)
            {
                ColoredConsole.WriteLine($"IDStudent: {student.Id} / Student Name: {student.FirstName} {student.LastName} / Course: {course.Name}".DarkGray());
            }
        }
        else
        {
            ColoredConsole.WriteLine("No Students Enrolled.".DarkRed());
        }

    }
}

void ActivateUser()
{
    ColoredConsole.Write("Enter UserID To Activate: ".Blue());
    int userID = Convert.ToInt32(Console.ReadLine());
    _userRepository.ActivateUser(userID);
    ColoredConsole.WriteLine($"User  ID {userID} Activated".Green());
}

void DeactivateUser()
{
    ColoredConsole.Write("Enter UserID To Deactivate: ".Blue());
    int userID = Convert.ToInt32(Console.ReadLine());
    if (userID == 1)
    {
        ColoredConsole.WriteLine("User ID 1 Can Not Be Deactivated.".Red());
        return;
    }
    _userRepository.DeactivateUser(userID);
    ColoredConsole.WriteLine($"User ID {userID} Deactivated".Green());
}

void ShowUsers()
{
    ColoredConsole.WriteLine("Users List:".Green());
    var users = _userRepository.GetAllUsers();
    foreach (var user in users)
    {
        string role = user switch
        {
            Student => "Student",
            Teacher => "Teacher",
            Operator => "Operator"
        };
        ColoredConsole.WriteLine($" ID: {user.Id}, Username: {user.UserName} (Role: {role}, Active: {user.IsActived})".DarkGray());
    }
}
DateTime CreateDateTime()
{
    ColoredConsole.Write("Enter Day:".DarkBlue());
    int day = Convert.ToInt32(Console.ReadLine());
    ColoredConsole.Write("Enter Hour (0-23):".DarkGray());
    int hour = Convert.ToInt32(Console.ReadLine());
    ColoredConsole.Write("Enter Minute (0-59):".DarkBlue());
    int minute = Convert.ToInt32(Console.ReadLine());
    DateTime dateTime = new DateTime(1403, 07, day, hour, minute, 0);
    return dateTime;
}

void UpdateCapacity()
{
    ColoredConsole.WriteLine("List of Courses: ".DarkBlue());
    foreach (var course in _courseRepository.GetAllCourses())
    {
        ColoredConsole.WriteLine($"Course ID: {course.Id}, Name: {course.Name}, Capacity: {course.Capacity}, Enrolled: {course.Students.Count}".DarkBlue());
    }
    ColoredConsole.Write("Enter Course ID By Update Capacity: ".Blue());
    int courseId = Convert.ToInt32(Console.ReadLine());
    ColoredConsole.Write("Enter Count Update: ".Blue());
    int count = Convert.ToInt32(Console.ReadLine());
    _courseRepository.UpdateCapacity(courseId, count);
    var updatedCourse = _courseRepository.GetCourseById(courseId);
    ColoredConsole.WriteLine($"Capacity Updated Successfully/ New Capacity: {updatedCourse.Capacity}".DarkGreen());
}

void ReduceCapacity()
{
    ColoredConsole.WriteLine("List of Courses: ".DarkBlue());
    foreach (var course in _courseRepository.GetAllCourses())
    {
        ColoredConsole.WriteLine($"Course ID: {course.Id}, Name: {course.Name}, Capacity: {course.Capacity}, Enrolled: {course.Students.Count}".DarkBlue());
    }
    ColoredConsole.Write("Enter Course ID By Reduce Capacity: ".Blue());
    int courseId = Convert.ToInt32(Console.ReadLine());
    ColoredConsole.Write("Enter Count Update: ".Blue());
    int count = Convert.ToInt32(Console.ReadLine());
    var courses = _courseRepository.GetCourseById(courseId);
    if (courses.Capacity - count < 0)
    {
        ColoredConsole.WriteLine("Capacity Can Not Reduced Zero".DarkRed());
    }
    else
    {
        _courseRepository.ReduceCourse(courseId, count);
        var reduceCourse = _courseRepository.GetCourseById(courseId);
        ColoredConsole.WriteLine($"Capacity Reduced Successfully! New Capacity: {reduceCourse.Capacity}".DarkGreen());
    }



}




