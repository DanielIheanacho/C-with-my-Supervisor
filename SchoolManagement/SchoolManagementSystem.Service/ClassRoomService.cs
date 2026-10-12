using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models.ClassModels;
using SchoolManagementSystem.Services.SchoolManagementSystem.Data;

namespace SchoolManagementSystem.Service
{
    public class ClassRoomService
    {
        public void SaveClassRoom(ClassRoom classRoom)
        {
            using var context = new AppDbContext();
            context.Add(classRoom);
            context.SaveChanges();
        }

        public List<string> GetClassRooms(User user)
        {
            using var context = new AppDbContext();
            var classRooms = context.ClassRooms
                .AsNoTracking()
                .Where(c => c.SchoolId == user.UserId)
                .OrderBy(c => c.Name)
                .Select(c => c.Name)
                .ToList();

            return classRooms;
        }

    }
}
