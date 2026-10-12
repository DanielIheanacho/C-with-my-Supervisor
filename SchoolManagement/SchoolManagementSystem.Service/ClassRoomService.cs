using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models.ClassModels;
using SchoolManagementSystem.Services.SchoolManagementSystem.Data;

namespace SchoolManagementSystem.Service
{
    public static class ClassRoomService
    {
        public static void SaveClassRoom(ClassRoom classRoom)
        {
            using var context = new AppDbContext();
            context.Add(classRoom);
            context.SaveChanges();
        }

        public static List<string> GetClassRooms()
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

        public static ClassRoom GetClassRoom(string name)
        {
            using var context = new AppDbContext();
            var classRoom = context.ClassRooms.Single(c => c.SchoolId == user.SchoolId && c.Name == name);

            return classRoom;
        }

        public static void UpdateClassRoom(ClassRoomInfo classInfo)
        {
            using var context = new AppDbContext();

            var classToUpdate = context.ClassRooms
                .Single(c => c.SchoolId == classInfo.SchoolId && c.Name == classInfo.Name);

            classToUpdate.Name = classInfo.Name;
            classToUpdate.Capacity = classInfo.Capacity;

            context.SaveChanges();
        }

        public static void RemoveClassRoom(string name)
        {
            using var context = new AppDbContext();
            var classRoom = context.ClassRooms.Single(c => c.SchoolId == user.SchoolId && c.Name == name);

            context.ClassRooms.Remove(classRoom);
            context.SaveChanges();
        }
    }
}
