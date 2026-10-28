using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SchoolManagement.Models.ClassModels;
using SchoolManagementSystem.Service.DTOS;
using SchoolManagementSystem.Service.Interfaces;
using SchoolManagementSystem.Service.Repositories;
using SchoolManagementSystem.Service.Service;
using SchoolManagementSystem.Services.SchoolManagementSystem.Data;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        "Data Source=localhost\\SQLEXPRESS;Initial Catalog=SchoolManagementSystem;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Command Timeout=0"));

// Register your dependencies
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AuthenticationService>();

var app = builder.Build();
using var scope = app.Services.CreateScope();

var userService = scope.ServiceProvider
                    .GetRequiredService<UserService>();

Console.WriteLine("UserService is ready for testing.");

while (true)
{
    var authenticationService = scope.ServiceProvider
                    .GetRequiredService<AuthenticationService>();

    Console.WriteLine("---Menu---\n");
    Console.WriteLine("1. Register new School");
    Console.WriteLine("2. Login");
    Console.WriteLine("0. Exit\n\n");
    Console.Write("Select Option: ");

    switch (Console.ReadLine())
    {
        case "0":
            return;
        case "1":
            {
                var schoolInfo = InputSchoolDetail();
                var userInfo = InputUserInfo(true);

                await authenticationService
                    .RegisterSchoolAndAdminAsync(schoolInfo, userInfo);
                break;
            }

        case "2":
            Console.WriteLine("\n\nEmail: ");
            string email = ValidInput();
            Console.WriteLine("Password: ");
            string passWord = ValidInput();
            var user = await authenticationService.LoginUserAsync(email, passWord);
            if (user == null)
            {
                Console.WriteLine("Invalid email or password.");
                break;
            }

            Console.WriteLine($"Welcome, {user.FirstName}!");

            switch (user)
            {
                case Admin admin:
                    await AdminMenuAsync(admin);
                    break;
                case Teacher teacher:
                    // teacher menu
                    break;
                case Guardian guardian:
                    // guardian menu
                    break;
            }
            break;

    }
}


async Task AdminMenuAsync(Admin admin)
{

    while (true)
    {
        Console.WriteLine("---Menu---\n");
        Console.WriteLine("1. Register New user");
        Console.WriteLine("2. View Admin(s)");
        Console.WriteLine("3. View Teahers(s)");
        Console.WriteLine("4. View Guardian(s)");
        Console.WriteLine("5. Delete User");
        Console.WriteLine("6. Update Details");
        Console.WriteLine("0. Exit\n\n");
        Console.Write("Select Option: ");

        switch (Console.ReadLine())
        {
            case "0":
                return; ;
            case "1":
                {
                    var userInfo = InputUserInfo(false);
                    User newUser = admin.RegisterNewUser(userInfo);

                    break;
                }
            case "2":
                await PrintUsersInCategoryAsync(admin, Roles.Admin);
                break;
            case "3":
                await PrintUsersInCategoryAsync(admin, Roles.Teacher);
                break;
            case "4":
                await PrintUsersInCategoryAsync(admin, Roles.Guardian);
                break;
            case "5":
                await DeleteUserAsync(admin);
                break;
            case "6":
                await UpdateDetailsAsync(admin);
                break;
            default:
                Console.WriteLine("Invalid Input");
                break;
        }
    }
}

async Task<(int Count, List<NameAndIdDto> List)> PrintUsersInCategoryAsync(Admin admin, Roles role)

{
    var users = await userService.GetUsersInCategoryAsync(admin, role);

    if (users.Count == 0)
    {
        Console.WriteLine("No users found.");
    }
    else
    {
        int i = 1;
        foreach (var u in users)
        {
            Console.WriteLine($"{i++}. {u.Name}");
        }
    }
    return (users.Count, users);
}

async Task UpdateDetailsAsync<T>(T user) where T : User, new()
{
    var userInfo = ExtractUserInfo(user);
    var Id = user.UserId;
    while (true)
    {
        Console.WriteLine("1. Firstname");
        Console.WriteLine("2. Lastame");
        Console.WriteLine("3. Email");
        Console.WriteLine("4. Password");
        Console.WriteLine("0. Exit");
        Console.WriteLine("Select Option: ");
        switch (Console.ReadLine())
        {
            case "1":
                userInfo.FirstName = ValidInput();
                await userService.UpdateUserAsync(Id, userInfo);
                break;
            case "2":
                userInfo.LastName = ValidInput();
                await userService.UpdateUserAsync(Id, userInfo);
                break;
            case "3":
                userInfo.Email = ValidInput();
                await userService.UpdateUserAsync(Id, userInfo);
                break;
            case "4":
                userInfo.PassWord = ValidInput();
                await userService.UpdateUserAsync(Id, userInfo);
                break;
            case "0":
                return;
            default:
                Console.WriteLine("Invalid Input");
                break;
        }
    }

}

UserInfo ExtractUserInfo<T>(T user) where T : User, new()
{
    var userInfo = new UserInfo
    {
        FirstName = user.FirstName,
        LastName = user.LastName,
        Email = user.Email,
        PassWord = user.PassWord,
    };

    return userInfo;
}

async Task DeleteUserAsync(Admin admin)
{
    while (true)
    {
        Console.WriteLine("What category of users would you like to delete?");
        Console.WriteLine("1. Admin(s)");
        Console.WriteLine("2. Teahers(s)");
        Console.WriteLine("3. Guardian(s)");
        Console.WriteLine("0. Exit\n\n");
        Console.Write("Select Option: ");

        switch (Console.ReadLine())
        {
            case "0":
                return;
            case "1":
                await DeleteUserInCategoryAsync(admin, Roles.Admin);
                break;
            case "2":
                await DeleteUserInCategoryAsync(admin, Roles.Teacher);
                break;
            case "3":
                await DeleteUserInCategoryAsync(admin, Roles.Guardian);
                break;
            default:
                Console.WriteLine("Invalid Input");
                break;
        }
    }
}

async Task DeleteUserInCategoryAsync(Admin admin, Roles role)
{
    var result = await PrintUsersInCategoryAsync(admin, role);
    var userCount = result.Count;
    var users = result.List;
    Console.WriteLine("Select option:");
    if (int.TryParse(Console.ReadLine(), out int option) && option > 0 && option <= userCount)
    {
        int userId = users[option - 1].Id;
        await userService.DeleteUserAsync(userId);
    }
}

SchoolInfo InputSchoolDetail()
{
    SchoolInfo school = new();
    Console.WriteLine("SchooL Name: ");
    school.Name = ValidInput();
    return school;
}

UserInfo InputUserInfo(bool isFirstUser)
{
    UserInfo user = new();

    Console.WriteLine("----Fill User Details----");
    Console.WriteLine("FirstName: ");
    user.FirstName = ValidInput();

    Console.WriteLine("LastName: ");
    user.LastName = ValidInput();

    Console.WriteLine("Email: ");
    user.Email = ValidInput().ToLower();

    Console.WriteLine("Password: ");
    user.PassWord = ValidInput();
    if (!isFirstUser)
    {
        Console.WriteLine("Role: ");
        user.Role = SelectRole();
    }
    else
    {
        user.Role = "admin";
    }

    return user;
}

string SelectRole()
{
    while (true)
    {
        Console.WriteLine("Insert an int value\n" +
        "Admin - 1\n" +
        "Teacher - 2\n" +
        "Guardian - 3\n" +
        "Exit - 0\n\n" +
        "Insert users Role:");
        switch (Console.ReadLine())
        {
            case "1":
                return "admin";
            case "2":
                return "teacher";
            case "3":
                return "guardian";
            default:
                Console.WriteLine("Invalid Input");
                break;
        }
    }
}

static string ValidInput()
{
    while (true)
    {
        string? input = Console.ReadLine();
        if (!string.IsNullOrEmpty(input))
        {
            return input;
        }
        Console.WriteLine("Cannot be left Empty");
    }
}


//===============================================================
//                    How  To Use Test
//===============================================================

//Comment everythis not commented
            
//            AND

//Uncomment everything that is commented






//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;

//using SchoolManagement.Models.ClassModels;
//using SchoolManagementSystem.Service.Interfaces;
//using SchoolManagementSystem.Service.Repositories;
//using SchoolManagementSystem.Service.Service;
//using SchoolManagementSystem.Services.SchoolManagementSystem.Data;

//var builder = Host.CreateApplicationBuilder(args);

//// Database configuration
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(
//        "Data Source=localhost\\SQLEXPRESS;" +
//        "Initial Catalog=SchoolManagementSystem;" +
//        "Integrated Security=True;" +
//        "Encrypt=True;" +
//        "TrustServerCertificate=True"));

//// Dependency injection
//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//builder.Services.AddScoped<ClassRoomService>();
//builder.Services.AddScoped<SubjectService>();

//var app = builder.Build();

//using var scope = app.Services.CreateScope();

//var unitOfWork =
//    scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

//var classroomService =
//    scope.ServiceProvider.GetRequiredService<ClassRoomService>();

//var subjectService =
//    scope.ServiceProvider.GetRequiredService<SubjectService>();

//void WaitForEnter(string message)
//{
//    Console.WriteLine();
//    Console.WriteLine("----------------------------------------");
//    Console.WriteLine(message);
//    Console.WriteLine("Press ENTER to continue...");
//    Console.ReadLine();
//}

//try
//{
//    // Find an existing school for the test records.
//    var schools = await unitOfWork.Schools.GetAllAsync();
//    var school = schools.FirstOrDefault();

//    if (school == null)
//    {
//        Console.WriteLine(
//            "TEST ABORTED: No school exists in the database.");
//        return;
//    }

//    var testAdmin = new Admin
//    {
//        SchoolId = school.SchoolId
//    };

//    string testId = Guid.NewGuid().ToString("N")[..8];

//    ClassRoom? classroom = null;
//    Subject? subject = null;

//    Console.WriteLine("CRUD SERVICE TEST");
//    Console.WriteLine($"Using school: {school.Name}");
//    Console.WriteLine($"School ID: {school.SchoolId}");
//    Console.WriteLine("Test records will be deleted at the end.");

//    // ==========================================
//    // CLASSROOM: CREATE
//    // ==========================================

//    WaitForEnter("CLASSROOM 1/5: CREATE");

//    classroom = new ClassRoom
//    {
//        Name = $"TestRoom_{testId}",
//        Capacity = 30,
//        SchoolId = school.SchoolId
//    };

//    await classroomService.CreateClassRoomAsync(classroom);

//    Console.WriteLine("CLASSROOM CREATED");
//    Console.WriteLine($"ID: {classroom.ClassRoomId}");
//    Console.WriteLine($"Name: {classroom.Name}");
//    Console.WriteLine($"Capacity: {classroom.Capacity}");

//    // ==========================================
//    // CLASSROOM: READ BY ID
//    // ==========================================

//    WaitForEnter("CLASSROOM 2/5: READ BY ID");

//    var foundClassroom =
//        await classroomService.GetByIdAsync(classroom.ClassRoomId);

//    if (foundClassroom == null)
//    {
//        throw new Exception("Could not read the created classroom.");
//    }

//    Console.WriteLine("CLASSROOM RETRIEVED FROM DATABASE");
//    Console.WriteLine($"ID: {foundClassroom.ClassRoomId}");
//    Console.WriteLine($"Name: {foundClassroom.Name}");
//    Console.WriteLine($"Capacity: {foundClassroom.Capacity}");
//    Console.WriteLine($"School ID: {foundClassroom.SchoolId}");

//    // ==========================================
//    // CLASSROOM: READ ALL
//    // ==========================================

//    WaitForEnter("CLASSROOM 3/5: READ ALL FOR THIS SCHOOL");

//    var classrooms =
//        await classroomService.GetAllClassRoomsAsync(testAdmin);

//    Console.WriteLine($"CLASSROOMS FOUND: {classrooms.Count}");

//    foreach (var item in classrooms)
//    {
//        Console.WriteLine($"ID: {item.Id} | Name: {item.Name}");
//    }

//    // ==========================================
//    // CLASSROOM: UPDATE
//    // ==========================================

//    WaitForEnter("CLASSROOM 4/5: UPDATE");

//    var classroomUpdate = new ClassRoom
//    {
//        Name = $"UpdatedRoom_{testId}",
//        Capacity = 40
//    };

//    await classroomService.UpdateClassRoomAsync(
//        classroom.ClassRoomId,
//        classroomUpdate);

//    Console.WriteLine("CLASSROOM UPDATE REQUEST COMPLETED.");
//    Console.WriteLine("The next step will read the updated record.");

//    // ==========================================
//    // CLASSROOM: VERIFY UPDATE
//    // ==========================================

//    WaitForEnter("CLASSROOM: VERIFY UPDATED VALUES");

//    foundClassroom =
//        await classroomService.GetByIdAsync(classroom.ClassRoomId);

//    if (foundClassroom == null ||
//        foundClassroom.Name != classroomUpdate.Name ||
//        foundClassroom.Capacity != classroomUpdate.Capacity)
//    {
//        throw new Exception("Classroom update verification failed.");
//    }

//    Console.WriteLine("UPDATED CLASSROOM RETRIEVED");
//    Console.WriteLine($"ID: {foundClassroom.ClassRoomId}");
//    Console.WriteLine($"Name: {foundClassroom.Name}");
//    Console.WriteLine($"Capacity: {foundClassroom.Capacity}");
//    Console.WriteLine("UPDATE VERIFIED.");

//    // ==========================================
//    // SUBJECT: CREATE
//    // ==========================================

//    WaitForEnter("SUBJECT 1/5: CREATE");

//    subject = new Subject
//    {
//        Name = $"TestSubject_{testId}",
//        ClassRoom = foundClassroom,
//        SchoolId = school.SchoolId,
//        IsCore = true
//    };

//    await subjectService.CreateSubjectAsync(subject);

//    Console.WriteLine("SUBJECT CREATED");
//    Console.WriteLine($"ID: {subject.SubjectId}");
//    Console.WriteLine($"Name: {subject.Name}");
//    Console.WriteLine($"Classroom ID: {foundClassroom.ClassRoomId}");
//    Console.WriteLine($"IsCore: {subject.IsCore}");

//    // ==========================================
//    // SUBJECT: READ BY ID
//    // ==========================================

//    WaitForEnter("SUBJECT 2/5: READ BY ID");

//    var foundSubject =
//        await subjectService.GetByIdAsync(subject.SubjectId);

//    if (foundSubject == null)
//    {
//        throw new Exception("Could not read the created subject.");
//    }

//    Console.WriteLine("SUBJECT RETRIEVED FROM DATABASE");
//    Console.WriteLine($"ID: {foundSubject.SubjectId}");
//    Console.WriteLine($"Name: {foundSubject.Name}");
//    Console.WriteLine($"IsCore: {foundSubject.IsCore}");
//    Console.WriteLine($"School ID: {foundSubject.SchoolId}");

//    // ==========================================
//    // SUBJECT: READ ALL
//    // ==========================================

//    WaitForEnter("SUBJECT 3/5: READ ALL FOR THIS SCHOOL");

//    var subjects =
//        await subjectService.GetAllSubjectsAsync(testAdmin);

//    Console.WriteLine($"SUBJECTS FOUND: {subjects.Count}");

//    foreach (var item in subjects)
//    {
//        Console.WriteLine($"ID: {item.Id} | Name: {item.Name}");
//    }

//    // ==========================================
//    // SUBJECT: UPDATE
//    // ==========================================

//    WaitForEnter("SUBJECT 4/5: UPDATE");

//    var subjectUpdate = new Subject
//    {
//        Name = $"UpdatedSubject_{testId}",
//        ClassRoom = foundClassroom,
//        IsCore = false
//    };

//    await subjectService.UpdateSubjectAsync(
//        subject.SubjectId,
//        subjectUpdate);

//    Console.WriteLine("SUBJECT UPDATE REQUEST COMPLETED.");
//    Console.WriteLine("The next step will read the updated record.");

//    // ==========================================
//    // SUBJECT: VERIFY UPDATE
//    // ==========================================

//    WaitForEnter("SUBJECT: VERIFY UPDATED VALUES");

//    foundSubject =
//        await subjectService.GetByIdAsync(subject.SubjectId);

//    if (foundSubject == null ||
//        foundSubject.Name != subjectUpdate.Name ||
//        foundSubject.IsCore != subjectUpdate.IsCore)
//    {
//        throw new Exception("Subject update verification failed.");
//    }

//    Console.WriteLine("UPDATED SUBJECT RETRIEVED");
//    Console.WriteLine($"ID: {foundSubject.SubjectId}");
//    Console.WriteLine($"Name: {foundSubject.Name}");
//    Console.WriteLine($"IsCore: {foundSubject.IsCore}");
//    Console.WriteLine("UPDATE VERIFIED.");

//    // ==========================================
//    // SUBJECT: DELETE
//    // ==========================================

//    WaitForEnter("SUBJECT 5/5: DELETE");

//    await subjectService.DeleteSubjectAsync(subject.SubjectId);

//    Console.WriteLine($"Delete requested for subject ID {subject.SubjectId}.");

//    // ==========================================
//    // SUBJECT: VERIFY DELETE
//    // ==========================================

//    WaitForEnter("SUBJECT: VERIFY DELETION");

//    foundSubject =
//        await subjectService.GetByIdAsync(subject.SubjectId);

//    if (foundSubject != null)
//    {
//        throw new Exception("Subject still exists after deletion.");
//    }

//    Console.WriteLine("SUBJECT DELETION VERIFIED.");

//    // ==========================================
//    // CLASSROOM: DELETE
//    // ==========================================

//    WaitForEnter("CLASSROOM 5/5: DELETE");

//    await classroomService.DeleteClassRoomAsync(classroom.ClassRoomId);

//    Console.WriteLine(
//        $"Delete requested for classroom ID {classroom.ClassRoomId}.");

//    // ==========================================
//    // CLASSROOM: VERIFY DELETE
//    // ==========================================

//    WaitForEnter("CLASSROOM: VERIFY DELETION");

//    foundClassroom =
//        await classroomService.GetByIdAsync(classroom.ClassRoomId);

//    if (foundClassroom != null)
//    {
//        throw new Exception("Classroom still exists after deletion.");
//    }

//    Console.WriteLine("CLASSROOM DELETION VERIFIED.");

//    Console.WriteLine();
//    Console.WriteLine("ALL CRUD TESTS PASSED.");
//}
//catch (Exception ex)
//{
//    Console.WriteLine();
//    Console.WriteLine("CRUD TEST FAILED:");
//    Console.WriteLine(ex);
//}

//WaitForEnter("TEST FINISHED. Press ENTER to exit.");