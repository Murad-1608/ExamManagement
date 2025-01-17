﻿using ExamManagement.Business.DTOs.SubjectDTOs;

namespace ExamManagement.Business.Abstract
{
    public interface ISubjectService
    {
        void Add(CreateSubjectDTO subjectDTO);
        List<GetAllSubjectDTO> GetAll(int number, int size);
        void Update(UpdateSubjectDTO subjectDTO);
        void Deactive(int id);        
    }
}
