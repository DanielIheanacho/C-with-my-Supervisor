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

        public static List<(int ClassRoomId,string Name)> GetClassRooms()
        {
            using var context = new AppDbContext();
            return context.ClassRooms
                .AsNoTracking()
                .Where(c => c.SchoolId == GeneralVariables.SchoolId)
                .OrderBy(c => c.Name)
                .AsEnumerable()
                .Select(c => (ClassRoomId: c.ClassRoomId, Name: c.Name))
                .ToList();
        }

        public static ClassRoom GetClassRoom(int classRoomId)
        {
            using var context = new AppDbContext();
            var classRoom = context.ClassRooms.Single(c => c.ClassRoomId == classRoomId);

            return classRoom;
        }

        public static void UpdateClassRoom(ClassRoom classUpdate, int classRoomId)
        {
            using var context = new AppDbContext();

            var classToUpdate = context.ClassRooms
                .Single(c => c.ClassRoomId == classRoomId);

            classToUpdate.Name = classUpdate.Name;
            classToUpdate.Capacity = classUpdate.Capacity;

            context.SaveChanges();
        }

        public static void RemoveClassRoom(int classRoomId)
        {
            using var context = new AppDbContext();
            var classRoom = context.ClassRooms.Single(c => c.ClassRoomId == classRoomId);

            context.ClassRooms.Remove(classRoom);
            context.SaveChanges();
        }
    }
}
