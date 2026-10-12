using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models.ClassModels;
using SchoolManagementSystem.Services.SchoolManagementSystem.Data;

namespace SchoolManagementSystem.Service
{
    public class SubjectService
    {
        public void SaveSubject(Subject subject)
        {
            using var context = new AppDbContext();
            context.Add(subject);
            context.SaveChanges();
        }

        public List<string> GetSubjects(User user)
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

}
