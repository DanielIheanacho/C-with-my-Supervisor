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
    }
}
