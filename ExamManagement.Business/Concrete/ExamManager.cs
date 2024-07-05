using Entity.Concrete;
using ExamManagement.Business.Abstract;
using ExamManagement.Business.Aspects.Validation;
using ExamManagement.Business.DTOs.ExamDTOs;
using ExamManagement.Business.Exceptions;
using ExamManagement.Business.ValidationRules.FluentValidation;
using ExamManagement.DataAccess.Abstract;

namespace ExamManagement.Business.Concrete
{
    public class ExamManager : IExamService
    {
        private readonly IExamDal _examDal;
        private readonly ISubjectService _subjectService;
        public ExamManager(IExamDal examDal,
                           ISubjectService subjectService)
        {
            _examDal = examDal;
            _subjectService = subjectService;
        }

        [ValidationAspect(typeof(ExamValidator))]
        public void Add(CreateExamDTO examDTO)
        {
            Exam exam = new Exam()
            {
                SubjectId = examDTO.SubjectId,
                Date = examDTO.Date,
                IsActive = true
            };
            _examDal.Add(exam);
        }

        public void Deactive(int id)
        {
            var exam = _examDal.Get(x => x.Id == id);

            if (exam == null)
            {
                throw new ExamNotFoundException("There are no exam with such id");
            }
            exam.IsActive = false;

            _examDal.Update(exam);
        }

        public List<GetAllExamDTO> GetAll(int number, int size)
        {
            var exams = _examDal.GetAllWithPagination(false, size, number);

            List<GetAllExamDTO> getAllExamDTOs = new List<GetAllExamDTO>();

            foreach (var exam in exams)
            {
                GetAllExamDTO getAllExamDTO = new GetAllExamDTO()
                {
                    SubjectName = exam.Subject.Name,
                    TeacherName = exam.Subject.Teacher.AppUser.FirstName + " " + exam.Subject.Teacher.AppUser.LastName,
                    Date = exam.Date
                };

                getAllExamDTOs.Add(getAllExamDTO);
            }

            return getAllExamDTOs;
        }

        public void Update(UpdateExamDTO examDTO)
        {
            var exam = _examDal.Get(x => x.Id == examDTO.Id);

            if (exam == null)
            {
                throw new ExamNotFoundException("There are no exam with such id");
            }

            exam.SubjectId = examDTO.SubjectId;
            exam.IsActive = true;
            examDTO.Date = examDTO.Date;

            _examDal.Update(exam);
        }
    }
}
