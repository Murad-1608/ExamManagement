using AutoMapper;
using Entity.Concrete;
using Entity.Concrete.Identity;
using ExamManagement.Business.Abstract;
using ExamManagement.Business.Aspects.Validation;
using ExamManagement.Business.DTOs.StudentDTOs;
using ExamManagement.Business.Enums;
using ExamManagement.Business.Exceptions;
using ExamManagement.Business.ValidationRules.FluentValidation;
using ExamManagement.DataAccess.Abstract;
using Microsoft.AspNetCore.Identity;

namespace ExamManagement.Business.Concrete
{
    public class StudentManager : IStudentService
    {
        private readonly IStudentDal _studentDal;
        private readonly UserManager<AppUser> _userManager;

        public StudentManager(IStudentDal studentDal,
                              UserManager<AppUser> userManager)
        {
            _studentDal = studentDal;
            _userManager = userManager;
        }

        [ValidationAspect(typeof(StudentValidator))]
        public async Task Add(CreateStudentDTO studentDTO)
        {
            AppUser user = new()
            {
                FirstName = studentDTO.FirstName,
                LastName = studentDTO.LastName,
                Email = studentDTO.Email,
                UserName = studentDTO.UserName,
                PhoneNumber = studentDTO.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, studentDTO.Password);

            await _userManager.AddToRoleAsync(user, Role.Student.ToString());

            Student entity = new Student()
            {
                AppUserId = user.Id,
                Group = studentDTO.Group,
                IsActive = true
            };

            _studentDal.Add(entity);
        }

        public void Deactive(int id)
        {
            var student = _studentDal.Get(x => x.Id == id);

            if (student == null)
            {
                throw new UserNotFoundException("There are no students with such id");
            }

            student.IsActive = false;

            _studentDal.Update(student);
        }

        public List<GetAllStudentDTO> GetAll(int pageNumber, int pageSize)
        {
            var students = _studentDal.GetAllWithUserDetails(false, pageSize, pageNumber, x => x.IsActive == true);

            List<GetAllStudentDTO> getAllStudentDTOs = new List<GetAllStudentDTO>();

            foreach (var student in students)
            {
                GetAllStudentDTO getAllStudentDTO = new GetAllStudentDTO()
                {
                    FirstName = student.AppUser.FirstName,
                    LastName = student.AppUser.LastName,
                    Group = student.Group
                };
                getAllStudentDTOs.Add(getAllStudentDTO);
            }
            return getAllStudentDTOs;
        }

        [ValidationAspect(typeof(StudentValidator))]
        public async Task Update(UpdateStudentDTO studentDTO)
        {
            var student = _studentDal.Get(x => x.Id == studentDTO.Id);

            if (student == null)
            {
                throw new UserNotFoundException("There are no students with such id");
            }
            student.Group = studentDTO.Group;
            student.IsActive = true;

            var appUser = await _userManager.FindByIdAsync(student.AppUserId.ToString());
            appUser.PhoneNumber = studentDTO.PhoneNumber;
            appUser.Email = studentDTO.Email;
            appUser.UserName = studentDTO.UserName;
            appUser.FirstName = studentDTO.FirstName;
            appUser.LastName = studentDTO.LastName;


            await _userManager.UpdateAsync(appUser);


            _studentDal.Update(student);
        }
    }
}
