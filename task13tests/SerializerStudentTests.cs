using task13;

namespace task13tests;

public class SerializerStudentTests
{
    [Fact]
    public void SerializeDeserialize_ShouldMatchOriginal()
    {
        var student = new Student
        {
            FirstName = "Кирилл",
            LastName = "Казантипов",
            BirthDate = new DateTime(1968, 6, 17),
            Grades = new List<Subject> { new Subject() { Name = "Педагогика", Grade = 5 },
                                            new Subject() { Name = "Философия", Grade = 4 } }
        };

        var json   = SerializerStudent.Serialize(student);
        var result = SerializerStudent.Deserialize(json);

        Assert.Equal(student.FirstName, result.FirstName);
        Assert.Equal(student.LastName,  result.LastName);
        Assert.Equal(student.BirthDate, result.BirthDate);
        Assert.Equal(2, result.Grades.Count);
        Assert.Equal("Педагогика", result.Grades[0].Name);
        Assert.Equal(5, result.Grades[0].Grade);
        Assert.Equal("Философия", result.Grades[1].Name);
        Assert.Equal(4, result.Grades[1].Grade);
    }

    [Fact]
    public void Deserialize_InvalidJson_ThrowsInvalidDataException()
    {
        var student = new Student
        {
            FirstName = "",
            LastName = "",
            BirthDate = new DateTime(1, 1, 1),
            Grades = new List<Subject>()
        };

        var badJson = SerializerStudent.Serialize(student);

        Assert.Throws<InvalidDataException>(() => SerializerStudent.Deserialize(badJson));
    }

    [Fact]
    public void SaveLoad_FromFile_WorksCorrectly()
    {
        var tmpPath = Path.GetTempFileName();
        try
        {
            var student = new Student
            {
                FirstName = "Григорий",
                LastName  = "Маджумаев",
                BirthDate = new DateTime(1964, 5, 14),
                Grades = new List<Subject> { new Subject { Name = "Менеджмент", Grade = 5 } }
            };

            SerializerStudent.SaveToFile(tmpPath, student);
            var loaded = SerializerStudent.LoadFromFile(tmpPath);

            Assert.Equal(student.FirstName, loaded.FirstName);
            Assert.Equal(student.LastName,  loaded.LastName);
            Assert.Equal(student.BirthDate, loaded.BirthDate);
            Assert.Single(loaded.Grades);
            Assert.Equal("Менеджмент", loaded.Grades[0].Name);
            Assert.Equal(5, loaded.Grades[0].Grade);
        }
        finally
        {
            File.Delete(tmpPath);
        }
    }
}
