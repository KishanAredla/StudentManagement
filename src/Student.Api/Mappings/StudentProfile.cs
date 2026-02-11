namespace StudentApi.Mappings
{
    using AutoMapper;
    using StudentApi.Models;
    using StudentApi.DTOs;   // adjust namespace to match your DTO folder 

    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<StudentPatchDto, Student>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // DTO → Entity
            CreateMap<CreateStudentDto, Student>();
            CreateMap<UpdateStudentDto, Student>();

            // Entity → DTO
            CreateMap<Student, StudentDto>();
        }
    }

}
