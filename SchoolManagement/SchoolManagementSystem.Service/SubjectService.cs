using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models.ClassModels;
using SchoolManagementSystem.Services.SchoolManagementSystem.Data;

namespace SchoolManagementSystem.Service
{
    public static class SubjectService
    {
        public static async Task CreateSubjectAsync(Subject subject)
        {
            using var context = new AppDbContext();
            context.Add(subject);
            await context.SaveChangesAsync();
        }

        public static async Task<List<(int SubjectId, string Name)>> GetAllSubjectsAsync()
        {
            using var context = new AppDbContext();

            return await context.Subjects
                .AsNoTracking()
                .Where(c => c.SchoolId == GeneralVariables.SchoolId)
                .OrderBy(c => c.Name)
                .Select(c => new ValueTuple<int, string>(c.SubjectId, c.Name))
                .ToListAsync();
        }

        public static async Task<Subject?> GetSubjectByIdAsync(int id)
        {
            using var context = new AppDbContext();
            var subject = await  context.Subjects.SingleOrDefaultAsync(c => c.SubjectId == id);
     
            return subject;
        }

        public static async Task<bool> UpdateSubjectAsync(int id, Subject subjectUpdate)
        {
            using var context = new AppDbContext();

            var subjectToUpdate = await context.Subjects
                .SingleOrDefaultAsync(s => s.SubjectId == id);

            if (subjectToUpdate == null)
            {
                return false;
            }

            subjectToUpdate.Name = subjectUpdate.Name;
            subjectToUpdate.ClassRoom = subjectUpdate.ClassRoom;
            subjectToUpdate.IsCore = subjectUpdate.IsCore;
            await  context.SaveChangesAsync();

            return true;
        }

        public static async Task<bool> DeleteSubjectAsync(int subjectId)
        {
            using var context = new AppDbContext();
            var subject = await context.Subjects.SingleOrDefaultAsync(c => c.SubjectId == subjectId);

            if (subject == null)
            {
                return false;
            }

            context.Subjects.Remove(subject);
            await context.SaveChangesAsync();
            return true;
        }
    }
}