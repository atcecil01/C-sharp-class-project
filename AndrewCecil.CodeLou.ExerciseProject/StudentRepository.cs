using System;
using System.Collections.Generic;
using System.Text;

namespace AndrewCecil.CodeLou.ExerciseProject
{
    public static class StudentRepository
    {
        public static Dictionary<int, Student> studentRepository = new Dictionary<int, Student>();

        public static void Add(int key, Student student)
        {
            studentRepository.Add(student.StudentId, student);
        }

        public static Student Get(int studentId)
        {
            return studentRepository[studentId];
        }
    }
}
