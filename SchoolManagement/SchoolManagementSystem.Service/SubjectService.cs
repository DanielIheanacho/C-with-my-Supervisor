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

}
