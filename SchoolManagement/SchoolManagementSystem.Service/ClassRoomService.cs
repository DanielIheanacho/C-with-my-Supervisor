using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models.ClassModels;
using SchoolManagementSystem.Services.SchoolManagementSystem.Data;

namespace SchoolManagementSystem.Service
{
    public static class ClassRoomService
    {
        public static async Task CreateClassRoomAsync(ClassRoom classRoom)
        {
            using var context = new AppDbContext();
            context.Add(classRoom);
            await context.SaveChangesAsync();
        }

        public static async Task<List<(int ClassRoomId,string Name)>> GetAllClassRoomsAsync()
        {
            using var context = new AppDbContext();
            return await context.ClassRooms
                .AsNoTracking()
                .Where(c => c.SchoolId == GeneralVariables.SchoolId)
                .OrderBy(c => c.Name)
                .Select(c => new ValueTuple<int, string>(c.ClassRoomId, c.Name))
                .ToListAsync();
        }

        public static async Task<ClassRoom?> GetClassRoomByIdAsync(int classRoomId)
        {
            using var context = new AppDbContext();
            var classRoom = await context.ClassRooms.SingleAsync(c => c.ClassRoomId == classRoomId);

            return classRoom;
        
        }

        public static async Task<bool> UpdateClassRoomAsync(int classRoomId, ClassRoom classUpdate)
        {
            using var context = new AppDbContext();

            var classToUpdate = await context.ClassRooms
                .SingleAsync(c => c.ClassRoomId == classRoomId);

            if (classToUpdate == null)
            {
                return false;
            }

            classToUpdate.Name = classUpdate.Name;
            classToUpdate.Capacity = classUpdate.Capacity;

            await context.SaveChangesAsync();
            return true;
        }

        public static async Task<bool> DeleteClassRoomAsync(int classRoomId)
        {
            using var context = new AppDbContext();
            var classRoom = await context.ClassRooms.SingleAsync(c => c.ClassRoomId == classRoomId);

            if (classRoom == null)
            {
                return false;
            }

            context.ClassRooms.Remove(classRoom);
            await context.SaveChangesAsync();
            return true;
        }
    }
}