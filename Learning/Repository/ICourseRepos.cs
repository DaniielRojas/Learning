using Learning.Models;
using Learning.Models.Dtos;

namespace Learning.Repository
{
    public interface ICourseRepos
    {
        Task<CreateCourseDto> CreateCourse(CreateCourseDto course);
 
    }
}
