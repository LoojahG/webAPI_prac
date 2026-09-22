using Microsoft.AspNetCore.Mvc;

namespace prac_webAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private static List<Student> students = new List<Student>
        {
            new Student { Id = 1, Name = "John", Age = 20 },
            new Student { Id = 2, Name = "Jane", Age = 22 },
            new Student { Id = 3, Name = "Bob", Age = 21 }
        };

        [HttpGet]
        public IEnumerable<Student> GetStudents()
        {
            return students;
        }
        [HttpGet("{id}")]
        public ActionResult<Student> GetStudentsById(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return student;
        }
        [HttpPost]
        public ActionResult<Student> CreateStudent(Student std)
        {
            var student = new Student
            {
                Id = students.Max(s => s.Id) + 1,
                Name = std.Name,
                Age = std.Age
            };

            students.Add(student);

            return CreatedAtAction(    //Returns 201 Created + Location header
                nameof(GetStudentsById), //Tells which method to use for the URL    
                new { id = student.Id }, //Fills the {id} in the route
                student);           //Data returned in the response body
        }
        [HttpPut("{id}")]
        public ActionResult<Student> UpdateStudent(int id,Student std)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound(); // 404 if no matching student
            }

            student.Name = std.Name;
            student.Age = std.Age;

            return Ok(student); // 200 OK + updated student in the body          
        }
        [HttpDelete("{id}")]
        public ActionResult<Student> DeleteStudent(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student != null)
            {
                students.Remove(student);
            }
            else
            {
                return NotFound(); // 404 if no matching student
            }
                return Ok(student);
        }
    }
}
