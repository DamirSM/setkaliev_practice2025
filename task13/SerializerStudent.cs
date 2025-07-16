using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace task13;

public class Subject
{
  public string Name {get; set; } = "";
  public int Grade {get; set; }
}

public class Student
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public DateTime BirthDate { get; set; }
    public List<Subject> Grades { get; set; } = new List<Subject>();
}

public class JsonDateConverter : JsonConverter<DateTime>
{
    private const string Format = "yyyy-MM-dd";

    public override DateTime Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) =>
            DateTime.ParseExact(reader.GetString()!,
                Format, CultureInfo.InvariantCulture);

    public override void Write(
        Utf8JsonWriter writer,
        DateTime dateTimeValue,
        JsonSerializerOptions options) =>
            writer.WriteStringValue(dateTimeValue.ToString(Format));
}

public static class SerializerStudent
{
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters = { new JsonDateConverter() }
    };

    public static string Serialize(Student student)
        => JsonSerializer.Serialize(student, Options);

    public static Student Deserialize(string json)
    {
        var student = JsonSerializer.Deserialize<Student>(json, Options);

        if (student == null
        || string.IsNullOrWhiteSpace(student.FirstName)
        || string.IsNullOrWhiteSpace(student.LastName)
        || student.Grades == null)
        {
            throw new InvalidDataException("Недопустимые данные в JSON");
        }

        return student;
    }

    public static void SaveToFile(string path, Student student)
        => File.WriteAllText(path, Serialize(student));

    public static Student LoadFromFile(string path)
        => Deserialize(File.ReadAllText(path));
}
