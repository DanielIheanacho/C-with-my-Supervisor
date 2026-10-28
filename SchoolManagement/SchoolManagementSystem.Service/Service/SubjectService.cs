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

        public async Task UpdateSubjectAsync(int subjectId, Subject subjectUpdate)
        {
            var subjectToUpdate = await GetByIdAsync(subjectId);

            if (subjectToUpdate == null)
            {
                return;
            }
            subjectToUpdate.Name = subjectUpdate.Name;
            subjectToUpdate.ClassRoom = subjectUpdate.ClassRoom;
            subjectToUpdate.IsCore = subjectUpdate.IsCore;

            await _unitOfWork.SaveChangesAsync();
        }

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