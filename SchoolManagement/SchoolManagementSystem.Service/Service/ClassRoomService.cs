using SchoolManagement.Models.ClassModels;
using SchoolManagementSystem.Service.Interfaces;

namespace SchoolManagementSystem.Service.Service
{
    public class ClassRoomService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClassRoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateClassRoomAsync(ClassRoom classRoom)
        {
            await _unitOfWork.ClassRooms.AddAsync(classRoom);
            await _unitOfWork.SaveChangesAsync();

        }

        //public async Task<List<(int ClassRoomId,string Name)>> GetAllClassRoomsAsync()
        //{
        //    await _unitOfWork.ClassRooms.GetAllAsync();
        //    await _unitOfWork.SaveChangesAsync();
        //}

        public async Task<ClassRoom?> GetByIdAsync(int classRoomId)
        {
            var classRoom = await _unitOfWork.ClassRooms.GetByIdAsync(classRoomId);

            if (classRoom == null)
            {
                return null;
            }

            return classRoom;
        }

        //public async Task UpdateClassRoomAsync(int classRoomId, ClassRoom classUpdate)
        //{

        //}

        public async Task DeleteClassRoomAsync(int classRoomId)
        {
            var classRoom = await _unitOfWork.ClassRooms.GetByIdAsync(classRoomId);

            if (classRoom == null)
            {
                return;
            }

            await _unitOfWork.ClassRooms.Delete(classRoom);
            await _unitOfWork.SaveChangesAsync();

        }
    }
}