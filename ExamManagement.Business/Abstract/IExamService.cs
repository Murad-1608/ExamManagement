using Entity.Concrete;
using ExamManagement.Business.DTOs.ExamDTOs;

namespace ExamManagement.Business.Abstract
{
    public interface IExamService
    {
        void Add(CreateExamDTO studentDTO);
        List<GetAllExamDTO> GetAll(int number, int size);
        void Update(UpdateExamDTO examDTO);
        void Deactive(int id);
    }
}
