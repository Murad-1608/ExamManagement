using ExamManagement.Business.DTOs.StudentExamDTOs;
using ExamManagement.Business.DTOs.SubjectDTOs;

namespace ExamManagement.Business.Abstract
{
    public interface IStudentExamService
    {
        void Add(CreateStudentExamDTO studentExamDTO);
        List<GetAllStudentExamDTO> GetAll(int number, int size);
        void Update(UpdateStudenExamDTO studenExamDTO);
        void Deactive(int id);
    }
}
