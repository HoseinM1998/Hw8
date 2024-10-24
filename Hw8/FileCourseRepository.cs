//using DotNetJsonDb;


//public class FileCourseRepository : ICourseRepository
//{
//    public JsonContext<Course> _coursetext;
//    public FileCourseRepository()
//    {
//        _coursetext = new JsonContext<Course>();
//    }

//    public void CreateCourse(string name, string prerequisite, int capacity, DateTime courseTime, DateTime endTime, Teacher teacher, int unit)
//    {
//        var course = new Course(name, prerequisite, capacity, courseTime, endTime, teacher, unit);
//        _coursetext.Add(course);
//    }

//    public List<Course> GetAllCourses()
//    {
//        return _coursetext.GetAll();
//    }

//    public Course GetCourseById(int courseId)
//    {
//        return _coursetext.Get(courseId);
//    }


//    public void SelectStudentInCourse(Student student, int courseId)
//    {
//        var course = _coursetext.GetAll().FirstOrDefault(c => c.Id == courseId);

//        if (course.Students.Count >= course.Capacity)
//        {
//            return;
//        }

//        if (student.EnrolledCourses.Sum(c => c.Unit) + course.Unit > 20)
//        {
//            return;
//        }
//        foreach (var enrolledCourse in student.EnrolledCourses)
//        {
//            if (course.CourseTimeStart < enrolledCourse.CourseTimeEnd && course.CourseTimeEnd > enrolledCourse.CourseTimeStart)
//            {
//                return;
//            }
//        }
//        if (student.EnrolledCourses.Any(c => c.Id == course.Id))
//        {
//            return;
//        }
//        if (course.Capacity > 0)
//        {
//            course.Capacity -= 1;
//            course.Students.Add(student);
//            student.EnrolledCourses.Add(course);
//        }
//    }

//    public void ShowStudentCourses(Student student)
//    {
//        foreach (var course in student.EnrolledCourses)
//        {
//            return;
//        }
//    }

//    public Course RemoveCourse(int courseId)
//    {
//        throw new NotImplementedException();
//    }
//    public void UpdateCourse(int courseId)
//    {
//        throw new NotImplementedException();
//    }
//}

