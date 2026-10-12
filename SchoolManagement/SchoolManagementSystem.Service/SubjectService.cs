using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models.ClassModels;
using SchoolManagementSystem.Services.SchoolManagementSystem.Data;

namespace SchoolManagementSystem.Service
{
    public static class SubjectService
    {
        public static void SaveSubject(Subject subject)
        {
            using var context = new AppDbContext();
            context.Add(subject);
            context.SaveChanges();
        }

        public static List<string> GetSubjects()
        {
            using var context = new AppDbContext();
            var subjects = context.Subjects
                .AsNoTracking()
                .Where(c => c.SchoolId == GeneralVariables.SchoolId)
                .OrderBy(c => c.Name)
                .Select(c => c.Name)
                .ToList();

            return subjects;
        }

        public static Subject GetSubject(int id)
        {
            using var context = new AppDbContext();
            var subject = context.Subjects.Single(c => c.SubjectId == id);

            return subject;
        }

        public static void UpdateSubject(Subject subjectUpdate, int id)
        {
            using var context = new AppDbContext();

            var subjectToUpdate = context.Subjects
                .Single(s => s.SubjectId == id);

            subjectToUpdate.Name = subjectUpdate.Name;
            subjectToUpdate.ClassRoom = subjectUpdate.ClassRoom;
            subjectToUpdate.IsCore = subjectUpdate.IsCore;

            context.SaveChanges();
        }

        public static void RemoveSubject(int subjectId)
        {
            using var context = new AppDbContext();
            var subject = context.Subjects.Single(c => c.SubjectId == subjectId);

            context.Subjects.Remove(subject);
            context.SaveChanges();

        }
    }
}
