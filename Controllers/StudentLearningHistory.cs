using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public StudentsController(ApplicationDbContext context)
    {
        _context = context;
    }
    [HttpPost("UploadStudents")]
    public async Task<IActionResult> UploadStudents([FromBody] List<Student> students)
    {
        try
        {
            // Step 1: Retrieve existing students with PRIMKEYs (directly join to fetch relevant fields)
            var existingStudents = await _context.Students
                .Where(s => students.Select(st => st.PRIMKEY).Contains(s.PRIMKEY)) // Filter by PRIMKEYs in the incoming students list
                .ToListAsync();

            // Step 2: Split the students into two lists - one for updates and one for inserts
            var studentsToUpdate = new List<Student>();
            var studentsToInsert = new List<Student>();

            foreach (var student in students)
            {
                var existingStudent = existingStudents.FirstOrDefault(s => s.PRIMKEY == student.PRIMKEY);
                if (existingStudent != null)
                {
                    // If the student exists, add to the update list
                    studentsToUpdate.Add(student);
                }
                else
                {
                    // If the student doesn't exist, add to the insert list
                    studentsToInsert.Add(student);
                }
            }

            // Step 3: Perform batch update for existing records (directly on the entities)
            if (studentsToUpdate.Any())
            {
                foreach (var updatedStudent in studentsToUpdate)
                {
                    var existingStudent = existingStudents.First(s => s.PRIMKEY == updatedStudent.PRIMKEY);

                    // Update fields for the existing student
                    existingStudent.LASTNAME = updatedStudent.LASTNAME;
                    existingStudent.FIRSTNAME = updatedStudent.FIRSTNAME;
                    existingStudent.ARABIC_NAME = updatedStudent.ARABIC_NAME;
                    existingStudent.JOB_TITLE = updatedStudent.JOB_TITLE;
                    existingStudent.ORG_ID = updatedStudent.ORG_ID;
                    existingStudent.ACTIVE = updatedStudent.ACTIVE;
                    existingStudent.ADDR = updatedStudent.ADDR;
                    existingStudent.CITY = updatedStudent.CITY;
                    existingStudent.STAT = updatedStudent.STAT;
                    existingStudent.CNTRY = updatedStudent.CNTRY;
                    existingStudent.EMAIL = updatedStudent.EMAIL;
                    existingStudent.COMMENTS = updatedStudent.COMMENTS;
                    existingStudent.GENDER = updatedStudent.GENDER;
                    existingStudent.CPNTTYPEID = updatedStudent.CPNTTYPEID;
                    existingStudent.CPNTID = updatedStudent.CPNTID;
                    existingStudent.REVDATE = updatedStudent.REVDATE;
                    existingStudent.SCHEDID = updatedStudent.SCHEDID;
                    existingStudent.CPNTDESC = updatedStudent.CPNTDESC;
                    existingStudent.GRADE = updatedStudent.GRADE;
                    existingStudent.COMPLDATE = updatedStudent.COMPLDATE;
                    existingStudent.COMPLSTATID = updatedStudent.COMPLSTATID;
                    existingStudent.COMPLSTATDESC = updatedStudent.COMPLSTATDESC;
                    existingStudent.TOTALHRS = updatedStudent.TOTALHRS;
                    existingStudent.CREDITHRS = updatedStudent.CREDITHRS;
                    existingStudent.CONTACTHRS = updatedStudent.CONTACTHRS;
                    existingStudent.CPEHRS = updatedStudent.CPEHRS;
                    existingStudent.TUITION = updatedStudent.TUITION;
                    existingStudent.INSTNAME = updatedStudent.INSTNAME;
                    existingStudent.EVENTCOMMENTS = updatedStudent.EVENTCOMMENTS;
                    existingStudent.LSTTMSP = updatedStudent.LSTTMSP;
                    existingStudent.CPNTCLASSIFICATION = updatedStudent.CPNTCLASSIFICATION;
                }
            }

            // Step 4: Perform batch insert for new records
            if (studentsToInsert.Any())
            {
                _context.Students.AddRange(studentsToInsert);
            }

            // Step 5: Save all changes in one go (both inserts and updates)
            await _context.SaveChangesAsync();

            return Ok("Data successfully uploaded.");
        }
        catch (Exception ex)
        {
            string errorMessage = $"Error uploading data: {ex.Message}";
            if (ex.InnerException != null)
            {
                errorMessage += $" Inner Exception: {ex.InnerException.Message}";
            }
            Console.WriteLine(errorMessage);  // Print to console, or log to a logging system
            return BadRequest(errorMessage);
        }
    }



    [HttpPost("DeleteStudents")]
    public async Task<IActionResult> DeleteStudents([FromBody] List<string> primkeys)
    {
        try
        {
            if (primkeys == null || !primkeys.Any())
            {
                return BadRequest("No PRIMKEYs provided for deletion.");
            }

            // Step 1: Fetch students to delete
            var studentsToDelete = await _context.Students
                .Where(s => primkeys.Contains(s.PRIMKEY))
                .ToListAsync();

            if (!studentsToDelete.Any())
            {
                return NotFound("No matching students found to delete.");
            }

            // Step 2: Remove the students
            _context.Students.RemoveRange(studentsToDelete);

            // Step 3: Save changes
            await _context.SaveChangesAsync();

            return Ok($"{studentsToDelete.Count} student(s) deleted successfully.");
        }
        catch (Exception ex)
        {
            string errorMessage = $"Error deleting students: {ex.Message}";
            if (ex.InnerException != null)
            {
                errorMessage += $" Inner Exception: {ex.InnerException.Message}";
            }
            Console.WriteLine(errorMessage);
            return BadRequest(errorMessage);
        }
    }




}