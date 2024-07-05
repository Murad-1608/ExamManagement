using Entity.Concrete;
using ExamManagement.Business.Abstract;
using ExamManagement.Business.Aspects.Validation;
using ExamManagement.Business.DTOs.SubjectDTOs;
using ExamManagement.Business.Exceptions;
using ExamManagement.Business.ValidationRules.FluentValidation;
using ExamManagement.DataAccess.Abstract;
using ExamManagement.DataAccess.Concrete.EntityFramework;

namespace ExamManagement.Business.Concrete
{
    public class SubjectManager : ISubjectService
    {
        private readonly ISubjectDal _subjectDal;
        public SubjectManager(ISubjectDal subjectDal)
        {
            _subjectDal = subjectDal;
        }

        [ValidationAspect(typeof(SubjectValidator))]
        public void Add(CreateSubjectDTO subjectDTO)
        {
            Subject subject = new()
            {
                TeacherId = subjectDTO.TeacherId,
                Name = subjectDTO.Name,
                IsActive = true
            };
            _subjectDal.Add(subject);
        }

        public void Deactive(int id)
        {
            var subject = _subjectDal.Get(x => x.Id == id);

            if (subject == null)
            {
                throw new SubjectNotFoundException("There are no subject with such id");
            }

            subject.IsActive = false;

            _subjectDal.Update(subject);
        }

        public List<GetAllSubjectDTO> GetAll(int number, int size)
        {
            var values = _subjectDal.GetAllWithPagination(false, size, number, x => x.IsActive == true);

            List<GetAllSubjectDTO> getAllSubjectDTOs = new List<GetAllSubjectDTO>();

            foreach (var subject in values)
            {
                GetAllSubjectDTO getAllSubjectDTO = new GetAllSubjectDTO()
                {
                    Name = subject.Name,
                    Department = subject.Teacher.Department,
                    TeacherName = subject.Teacher.AppUser.FirstName + " " + subject.Teacher.AppUser.LastName
                };

                getAllSubjectDTOs.Add(getAllSubjectDTO);
            }
            return getAllSubjectDTOs;
        }


        [ValidationAspect(typeof(SubjectValidator))]
        public void Update(UpdateSubjectDTO subjectDTO)
        {
            var subject = _subjectDal.Get(x => x.Id == subjectDTO.Id);

            if (subject == null)
            {
                throw new SubjectNotFoundException("There are no subject with such id");
            }

            subject.Name = subjectDTO.Name;
            subject.TeacherId = subjectDTO.TeacherId;
            subject.IsActive = true;

            _subjectDal.Update(subject);
        }
    }
}
