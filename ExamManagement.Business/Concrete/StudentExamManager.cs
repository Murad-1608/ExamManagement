using Entity.Concrete;
using ExamManagement.Business.Abstract;
using ExamManagement.Business.Aspects.Validation;
using ExamManagement.Business.DTOs.StudentExamDTOs;
using ExamManagement.Business.DTOs.SubjectDTOs;
using ExamManagement.Business.Exceptions;
using ExamManagement.Business.ValidationRules.FluentValidation;
using ExamManagement.DataAccess.Abstract;
using ExamManagement.DataAccess.Concrete.EntityFramework;

namespace ExamManagement.Business.Concrete
{
    public class StudentExamManager : IStudentExamService
    {
        private readonly IStudentExamDal _studentExamDal;
        public StudentExamManager(IStudentExamDal studentExamDal)
        {
            _studentExamDal = studentExamDal;
        }


        [ValidationAspect(typeof(StudentExamValidator))]
        public void Add(CreateStudentExamDTO studentExamDTO)
        {
            StudentExam studentExam = new()
            {
                ExamId = studentExamDTO.ExamId,
                StudentId = studentExamDTO.StudentId,
                Score = studentExamDTO.Score
            };
            _studentExamDal.Add(studentExam);
        }

        public void Deactive(int id)
        {
            var studenExam = _studentExamDal.Get(x => x.Id == id);

            if (studenExam == null)
            {
                throw new SubjectNotFoundException("There are no subject with such id");
            }

            studenExam.IsActive = false;

            _studentExamDal.Update(studenExam);
        }

        public List<GetAllStudentExamDTO> GetAll(int number, int size)
        {
            var studentExams = _studentExamDal.GetAllWithPagination(false, size, number, x => x.IsActive == true);

            List<GetAllStudentExamDTO> getAllStudentExamDTOs = new List<GetAllStudentExamDTO>();

            foreach (var studentExam in studentExams)
            {
                GetAllStudentExamDTO getAllStudentExamDTO = new GetAllStudentExamDTO()
                {
                    StudentName = studentExam.Student.AppUser.FirstName + " " + studentExam.Student.AppUser.FirstName,
                    SubjectName = studentExam.Exam.Subject.Name,
                    Score = studentExam.Score
                };

                getAllStudentExamDTOs.Add(getAllStudentExamDTO);
            }
            return getAllStudentExamDTOs;
        }

        public void Update(UpdateStudenExamDTO studenExamDTO)
        {
            var studenExam = _studentExamDal.Get(x => x.Id == studenExamDTO.Id);

            if (studenExam == null)
            {
                throw new SubjectNotFoundException("There are no subject with such id");
            }

            studenExam.StudentId = studenExamDTO.StudentId;
            studenExam.ExamId = studenExamDTO.ExamId;

            _studentExamDal.Update(studenExam);
        }
    }
}
