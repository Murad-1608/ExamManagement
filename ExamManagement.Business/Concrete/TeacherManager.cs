using Entity.Concrete.Identity;
using Entity.Concrete;
using ExamManagement.Business.Abstract;
using ExamManagement.Business.Aspects.Validation;
using ExamManagement.DataAccess.Abstract;
using Microsoft.AspNetCore.Identity;
using ExamManagement.Business.DTOs.TeacherDTOs;
using ExamManagement.Business.Exceptions;
using ExamManagement.Business.ValidationRules.FluentValidation;

namespace ExamManagement.Business.Concrete
{
    public class TeacherManager : ITeacherService
    {
        private readonly ITeacherDal _teacherDal;
        private readonly UserManager<AppUser> _userManager;

        public TeacherManager(ITeacherDal teacherDal,
                              UserManager<AppUser> userManager)
        {
            _teacherDal = teacherDal;
            _userManager = userManager;
        }

        [ValidationAspect(typeof(TeacherValidator))]
        public async Task Add(CreateTeacherDTO teacherDTO)
        {
            AppUser user = new()
            {
                FirstName = teacherDTO.FirstName,
                LastName = teacherDTO.LastName,
                Email = teacherDTO.Email,
                UserName = teacherDTO.UserName,
                PhoneNumber = teacherDTO.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, teacherDTO.Password);

            Teacher entity = new Teacher()
            {
                AppUserId = user.Id,
                Department = teacherDTO.Department,
                IsActive = true
            };

            _teacherDal.Add(entity);
        }

        public void Deactive(int id)
        {
            var teacher = _teacherDal.Get(x => x.Id == id);

            if (teacher == null)
            {
                throw new UserNotFoundException("There are no teacher with such id");
            }

            teacher.IsActive = false;

            _teacherDal.Update(teacher);
        }

        public List<GetAllTeacherDTO> GetAll(int pageNumber, int pageSize)
        {
            var teachers = _teacherDal.GetAllWithUserDetails(false, pageSize, pageNumber, x => x.IsActive == true);

            List<GetAllTeacherDTO> getAllTeacherDTOs = new List<GetAllTeacherDTO>();

            foreach (var teacher in teachers)
            {
                GetAllTeacherDTO getAllTeacherDTO = new GetAllTeacherDTO()
                {
                    FirstName = teacher.AppUser.FirstName,
                    LastName = teacher.AppUser.LastName,
                    Department = teacher.Department
                };
                getAllTeacherDTOs.Add(getAllTeacherDTO);
            }
            return getAllTeacherDTOs;
        }

        [ValidationAspect(typeof(TeacherValidator))]
        public async Task Update(UpdateTeacherDTO teacherDTO)
        {
            var teacher = _teacherDal.Get(x => x.Id == teacherDTO.Id);

            if (teacher == null)
            {
                throw new UserNotFoundException("There are no students with such id");
            }
            teacher.Department = teacherDTO.Department;
            teacher.IsActive = true;

            var appUser = await _userManager.FindByIdAsync(teacher.AppUserId.ToString());
            appUser.PhoneNumber = teacherDTO.PhoneNumber;
            appUser.Email = teacherDTO.Email;
            appUser.UserName = teacherDTO.UserName;
            appUser.FirstName = teacherDTO.FirstName;
            appUser.LastName = teacherDTO.LastName;


            await _userManager.UpdateAsync(appUser);


            _teacherDal.Update(teacher);
        }
    }
}
