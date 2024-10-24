
using Colors.Net;
using Colors.Net.StringColorExtensions;


public class UserRepository : IUserRepository
{
    private List<User> users = InMemoryDB.Users;

    public List<Course> ShowTeacherCourses(Teacher teacher)
    {
        var teacherCourses = InMemoryDB.Courses.Where(c => c.Teacher.Id == teacher.Id).ToList();
        return teacherCourses;
    }
    public void ActivateUser(int userId)
    {
        var user = users.FirstOrDefault(u => u.Id == userId);
        if (user != null)
        {
            user.IsActived = true;
           
        }
    }

    public void DeactivateUser(int userId)
    {
        var user = InMemoryDB.Users.FirstOrDefault(u => u.Id == userId);
        if (user != null)
        {
            user.IsActived = false;
          
        }

    }
    public void AddUser(User user)
    {
        users.Add(user);
    }
    public List<User> GetAllUsers()
    {
        return InMemoryDB.Users.ToList();

    }

    public void UpdateUser(int userId)
    {
        throw new NotImplementedException();
    }

    public void RemoveUser(int userId)
    {
        throw new NotImplementedException();
    }
}

