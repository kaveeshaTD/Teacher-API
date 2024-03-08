using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApi.Data;
using SchoolApi.Models;

namespace SchoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly TeacherDbContext _teacherDbContext;

        public TeachersController(TeacherDbContext teacherDbContext)
        {
            _teacherDbContext = teacherDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetTeachers()
        {
            var teachers = await _teacherDbContext.Teachers.ToListAsync();
            return Ok(teachers);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeacher([FromBody] Teacher teacherRequest)
        {
            await _teacherDbContext.AddAsync(teacherRequest);
            await _teacherDbContext.SaveChangesAsync();

            return Ok(new
            {
                Message = "Teacher Create  Succesfully...!"
            });
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTeacher([FromRoute]int id)
        {
            var teacher = await _teacherDbContext.Teachers.FirstOrDefaultAsync(x => x.id == id);
            if(teacher is null)
            {
                return NotFound(new
                {
                    Message = "Teacher Not Found...!"
                });
            }

            return Ok(teacher);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateTeacher([FromRoute] int id , Teacher teacherUpdateRequest)
        {
            var teacher = await _teacherDbContext.Teachers.FindAsync(id);
            if(teacher is null)
            {
                return NotFound(new
                {
                    Message = "Not Found User...!"
                });
            }

            teacher.Firstname= teacherUpdateRequest.Firstname;
            teacher.LastName= teacherUpdateRequest.LastName;
            teacher.Grade= teacherUpdateRequest.Grade;
            teacher.Email= teacherUpdateRequest.Email;
          
            await _teacherDbContext.SaveChangesAsync();
            return Ok(teacher);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteTeacher([FromRoute] int id)
        {
            var teacher = await _teacherDbContext.Teachers.FindAsync(id);
            if(teacher is null)
            {
                return NotFound(new
                {
                    Message = "User Not Found Cheack Again...!"
                });

            }

            _teacherDbContext.Teachers.Remove(teacher);
            await _teacherDbContext.SaveChangesAsync();

            return Ok(new
            {
                Meaasge = "User Delete Compleate...!"
            });
        }
    }
}
