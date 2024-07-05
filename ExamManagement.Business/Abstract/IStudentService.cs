using ExamManagement.Business.DTOs.StudentDTOs;
namespace ExamManagement.Business.Abstract
{
    public interface IStudentService
    {
        Task Add(CreateStudentDTO studentDTO);
        List<GetAllStudentDTO> GetAll(int number, int size);
        Task Update(UpdateStudentDTO studentDTO);
        void Deactive(int id);
    }
}
