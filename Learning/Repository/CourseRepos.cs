using AutoMapper;
using Learning.Data;
using Learning.Models.Dtos;
using Learning.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace Learning.Repository
{
    public class CourseRepos : ICourseRepos
    {
        private readonly DataContext _db;
        private IMapper _mapper;
        private readonly ILogger<CourseRepos> _logger;
        
        public CourseRepos (DataContext db, IMapper mapper, ILogger<CourseRepos> logger)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CreateCourseDto> CreateCourse (CreateCourseDto courseDto)
        {
            try
            {
                var conflicts = await _db.Courses
                    .Where(c => c.Title == courseDto.Title && c.InstructorId == courseDto.InstructorId)
                    .Select(c => new { c.Title, c.InstructorId })
                    .ToListAsync();
                if (conflicts.Any())
                {
                    throw new ArgumentException("Ya existe un curso con el mismo título para este instructor.");
                }
                courseDto.ImageUrl ??= "/images/default-course-avatar.png"; 
                {
                    var course = _mapper.Map<Course>(courseDto);
                    course.CreatedAt = DateTime.UtcNow;
                    course.UpdatedAt = DateTime.UtcNow;
                    await _db.Courses.AddAsync(course);
                    await _db.SaveChangesAsync();
                    return _mapper.Map<Course, CreateCourseDto>(course);
                }
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Error creando course");
                throw;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al crear course");
                throw new Exception("Ocurrió un error al crear el curso. Por favor, inténtelo de nuevo más tarde.");
            }
        }
    }
}
