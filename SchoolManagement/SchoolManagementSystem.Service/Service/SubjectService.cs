using SchoolManagement.Models.ClassModels;
using SchoolManagementSystem.Service.Interfaces;

namespace SchoolManagementSystem.Service.Service
{
    public class SubjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SubjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateSubjectAsync(Subject subject)
        {
            await _unitOfWork.Subjects.AddAsync(subject);
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task<List<NameAndIdDto>> GetAllSubjectsAsync(Admin user)
        {

            return await _unitOfWork.Subjects.GetAllProjectedAsync(
            s => s.SchoolId == user.SchoolId,
            s => new NameAndIdDto
            {
                Id = s.SubjectId,
                Name = s.Name
            });
        }

        public async Task<Subject?> GetByIdAsync(int subjectId)
        {
            var subject = await _unitOfWork.Subjects.GetByIdAsync(subjectId);

            if (subject == null)
            {
                return null;
            }

            return subject;
        }

        //public async Task<bool> UpdateSubjectAsync(int id, Subject subjectUpdate)
        //{
        //    using var context = new AppDbContext();

        //    var subjectToUpdate = await context.Subjects
        //        .SingleOrDefaultAsync(s => s.SubjectId == id);

        //    if (subjectToUpdate == null)
        //    {
        //        return false;
        //    }

        //    subjectToUpdate.Name = subjectUpdate.Name;
        //    subjectToUpdate.ClassRoom = subjectUpdate.ClassRoom;
        //    subjectToUpdate.IsCore = subjectUpdate.IsCore;
        //    await  context.SaveChangesAsync();

        //    return true;
        //}

        public async Task DeleteSubjectAsync(int subjectId)
        {
            var subject = await _unitOfWork.Subjects.GetByIdAsync(subjectId);

            if (subject == null)
            {
                return;
            }

            await _unitOfWork.Subjects.Delete(subject);
            await _unitOfWork.SaveChangesAsync();

        }
    }
}