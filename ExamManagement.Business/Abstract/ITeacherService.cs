
using ExamManagement.Business.DTOs.TeacherDTOs;

namespace ExamManagement.Business.Abstract
{
    public interface ITeacherService
    {
        Task Add(CreateTeacherDTO teacherDTO);
        List<GetAllTeacherDTO> GetAll(int number, int size);
        Task Update(UpdateTeacherDTO teacherDTO);
        void Deactive(int id);
    }
}
