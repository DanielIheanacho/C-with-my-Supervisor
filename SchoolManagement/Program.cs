
public class SchoolManagement
{
    private class User
    {
        int iD;
        string firstName;
        string lastName;
        string email;
        Role role;
        string password;


        enum Role
        {
            admin,
            teacher,
            gardian
        }
    }

    private class Subject
    {
    int iD;
    string name;
    Category category;
    ClassRoom classRoom;
    bool isCore;

    enum Category
        {
            Sci,
            Art,
            Gen
        }
    }

    private class ClassRoom
    {
        int ID;
        string name;
        int Capacity;
    }

    private class Period
    {
        int ID;
        string name;
        int startTime;
        int endTime;
        bool isBreakPeriod;    
    }
}