using Colors.Net;
using Colors.Net.StringColorExtensions;


public class CourseRepository : ICourseRepository
{
    private List<Course> courses = InMemoryDB.Courses;

    public List<Course> GetAllCourses()
    {
        return InMemoryDB.Courses;
    }

    public Course GetCourseById(int courseId)
    {
        return InMemoryDB.Courses.FirstOrDefault(course => course.Id == courseId);
    }
    public void SelectStudentInCourse(Student student, int courseId)
    {
        var course = courses.FirstOrDefault(c => c.Id == courseId);
        if (course != null)
        {
            if (course.Students.Count >= course.Capacity)
            {
                
            }

            if (student.EnrolledCourses.Sum(c => c.Unit) + course.Unit > 20)
            {
                
            }

            foreach (var enrolledCourse in student.EnrolledCourses)
            {
                if (course.CourseTimeStart < enrolledCourse.CourseTimeEnd && course.CourseTimeEnd > enrolledCourse.CourseTimeStart)
                {
                    
                }
            }
            if (student.EnrolledCourses.Any(c => c.Id == course.Id))
            {
                
            }
            if (course.Capacity == 0)
            {
               
            }
            course.Capacity -= 1;
            course.Students.Add(student);
            student.EnrolledCourses.Add(course);
        }
       
    }
    public void ShowStudentCourses(Student student)
    {
        foreach (var course in student.EnrolledCourses)
        {
           
        }
    }

    public void CreateCourse(string name, string prerequisite, int capacity, DateTime courseTime, DateTime endTime, Teacher teacher, int unit)
    {
        var course = new Course(name, prerequisite, capacity, courseTime, endTime, teacher, unit);
        InMemoryDB.Courses.Add(course);
    }
    public void UpdateCapacity(int courseId, int count)
    {
        var selectedCourse = courses.FirstOrDefault(c => c.Id == courseId);
       
        int newCapacity = selectedCourse.Capacity + count;
        selectedCourse.Capacity = newCapacity;
    }
    public void ReduceCourse(int courseId, int count)
    {
        var selectedCourse = courses.FirstOrDefault(c => c.Id == courseId);
        
        int newCapacity = selectedCourse.Capacity - count;
        selectedCourse.Capacity = newCapacity;
    }
}