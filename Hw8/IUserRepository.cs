public interface IUserRepository
{
    public List<Course> ShowTeacherCourses(Teacher teacher);
    public void ActivateUser(int userId);
    public void DeactivateUser(int userId);
    public void AddUser(User user);
    public List<User> GetAllUsers();
    public void UpdateUser(int userId);
    public void RemoveUser(int userId);


}

