using System;
using System.Collections.Generic;
using System.Text;

namespace AndrewCecil.CodeLou.ExerciseProject
{
    public static class StudentRepository
    {
        public static Dictionary<string, Student> studentRepository = new Dictionary<string, Student>();
        
        //private List<Student> AllStudents => Students.Values.ToList();


        public static void Add(Student student)
        {
            studentRepository.Add((student.FirstName + " " + student.LastName), student);
        }

        public static Student Get(string studentName)
        {
            return studentRepository[studentName];
        }
    }
}
