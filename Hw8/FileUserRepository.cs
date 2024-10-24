//using DotNetJsonDb;
//using Newtonsoft.Json;
//using System.Text.Json.Serialization;


//public class FileUserRepository : IUserRepository
//{
//    string path = "D:File.txt/";
//    public JsonContext<User> _context;
//    public FileUserRepository()
//    {
//        _context = new JsonContext<User>();
//    }
//    public void ActivateUser(int userId)
//    {
//        var user = _context.Get(userId);
//        var data = JsonConvert.SerializeObject(user);
//        File.AppendAllText(path, data);

//        user.IsActived = true;
//        _context.Update(userId, user);

//    }

//    public void AddUser(User user)
//    {
//        _context.Add(user);
//        var data = JsonConvert.SerializeObject(user);
//        File.AppendAllText(path, data);
//    }

//    public void DeactivateUser(int userId)
//    {
//        var user = _context.Get(userId);
//        user.IsActived = false;
//        var data = JsonConvert.SerializeObject(user);
//        _context.Update(userId, user);
//        File.AppendAllText(path, data);

//    }

//    public List<User> GetAllUsers()
//    {
//        return _context.GetAll();
//    }

//    public List<Course> ShowTeacherCourses(Teacher teacher)
//    {
//        throw new NotImplementedException();
//    }

//    public User UpdateUser(int userId)
//    {
//        throw new NotImplementedException();
//    }

//    public User RemoveUser(int userId)
//    {
//        throw new NotImplementedException();
//    }
//}
 
