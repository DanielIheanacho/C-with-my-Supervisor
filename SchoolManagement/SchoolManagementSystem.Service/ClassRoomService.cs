using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models.ClassModels;
using SchoolManagementSystem.Services.SchoolManagementSystem.Data;

namespace SchoolManagementSystem.Service
{
    public static class ClassRoomService
    {
        public static void CreateClassRoom(ClassRoom classRoom)
        {
            using var context = new AppDbContext();
            context.Add(classRoom);
            context.SaveChanges();
        }

        public static List<(int ClassRoomId,string Name)> GetAllClassRooms()
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

        public static ClassRoom? GetClassRoomById(int classRoomId)
        {
            using var context = new AppDbContext();
            var classRoom = context.ClassRooms.Single(c => c.ClassRoomId == classRoomId);

            return classRoom;
        
        }

        public static bool UpdateClassRoom(int classRoomId, ClassRoom classUpdate)
        {
            using var context = new AppDbContext();

            var classToUpdate = context.ClassRooms
                .Single(c => c.ClassRoomId == classRoomId);

            if (classToUpdate == null)
            {
                return false;
            }

            classToUpdate.Name = classUpdate.Name;
            classToUpdate.Capacity = classUpdate.Capacity;

            context.SaveChanges();
            return true;
        }

        public static bool DeleteClassRoom(int classRoomId)
        {
            using var context = new AppDbContext();
            var classRoom = context.ClassRooms.Single(c => c.ClassRoomId == classRoomId);

            if (classRoom == null)
            {
                return false;
            }

            context.ClassRooms.Remove(classRoom);
            context.SaveChanges();
            return true;
        }
    }
}