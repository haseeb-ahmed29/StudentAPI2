using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAPI2.Data;
using StudentAPI2.Models;

namespace StudentAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        //_context is our database manager(AppDbContext)
        //_ prefix, it shows that is private field
        //private means only code inside this class can use it.
        //readonly means once it is set, so it cannot be changed

        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
            //We store the context in our private field to all methods.
        }

        //Read All Students(GET/ api/students)

        //[HttpGet] means this method respond to Get requests.
        [HttpGet]
        //async' means this method runs asynchronously.
        //task<ActionResult> it is the return type for async API result/actions
        //IEnumerable<Student> means a collection(List)of Student object
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            //_context.Students is the Students table in our database.
            //await wait for the database to respond, then continue
            //ToListAsync() convert the database records into a C# list
            //Ok() send back
            var students = await _context.Students.ToListAsync();
            return Ok(students);
        }
        // Read one student by ID (GET: api/Students/5)
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            // If no student is found
            if (student == null)
            {
                return NotFound("No student found with Id: " + id);
            }

            // Return the student
            return Ok(student);
        }

        //Create a new student (Post /api/students)
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {
            //Add the new student to the Students table in memory.
            _context.Students.Add(student);

            //SavechangesAsync() save all changes to the database
            //we must always call this after Add,Update or Delete
            await _context.SaveChangesAsync();

            //CreatedAtAction() send back an HTTP 201 response.
            //201 means 'Created', a new resource was successfully created
            //nameof (GetStudent becomes the string "GetStudent")
            //becz if you rename the method, C# updated it automatically.
            //Route values(URL parameter).
            // new { id = student.Id }, This creates an anonymous objects
            //Example Student.Id =5 becomes {id =5}
            //Get api/student/5
            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }
    }
}