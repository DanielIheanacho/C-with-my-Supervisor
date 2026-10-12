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
                .Where(c => c.SchoolId == user.UserId)
                .OrderBy(c => c.Name)
                .Select(c => c.Name)
                .ToList();

            return subjects;
        }

        public static Subject GetSubject(string name)
        {
            using var context = new AppDbContext();
            var subject = context.Subjects.Single(c => c.SchoolId == user.SchoolId && c.Name == name);

            return subject;
        }

        public static void UpdateSubject(SubjectInfo subjectInfo)
        {
            using var context = new AppDbContext();

            var subjectToUpdate = context.Subjects
                .Single(s => s.SchoolId == subjectInfo.SchoolId && s.Name == subjectInfo.Name);

            subjectToUpdate.Name = subjectInfo.Name;
            subjectToUpdate.ClassRoom = subjectInfo.ClassRoom;
            subjectToUpdate.IsCore = subjectInfo.IsCore;

            context.SaveChanges();
        }

        public static void RemoveSubject(string name)
        {
            using var context = new AppDbContext();
            var subject = context.Subjects.Single(c => c.SchoolId == user.SchoolId && c.Name == name);

            context.Subjects.Remove(subject);
            context.SaveChanges();

        }
    }
}
