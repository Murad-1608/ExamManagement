﻿namespace ExamManagement.Business.DTOs.StudentDTOs
{
    public class UpdateStudentDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Group { get; set; }
    }
}