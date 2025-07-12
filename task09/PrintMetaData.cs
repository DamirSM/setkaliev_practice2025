
using System.Reflection;
using task07;

namespace task09;

public class PrintMetaData
{
    public static void PrintAttrs(Attribute[] attrs)
    {
        foreach (var attr in attrs)
        {
            var attrType = attr.GetType();
            Console.WriteLine("Attribute - {0}", attrType.Name);
            var props = attrType.GetProperties();
            foreach (var prop in props)
            {
                var value = prop.GetValue(attr);
                Console.WriteLine("{0} - {1}", prop.Name, value);
            }
        }
    }

    public static void Main(string[] args)
    {
        string dllPath = args[0];
        Console.WriteLine(dllPath);

        Assembly assem = Assembly.LoadFrom(dllPath);
        Type[] types = assem.GetTypes();

        foreach (var type in types)
        {
            Console.WriteLine("Class Name - {0}", type.Name);
            var classAttrs = type.GetCustomAttributes().ToArray();

            if(classAttrs.Any())
                PrintAttrs(classAttrs);

            Console.WriteLine("Methods:");
            foreach (var method in type.GetMethods())
            {
                Console.WriteLine("Method - {0}", method.Name);
                var methodAttrs = method.GetCustomAttributes().ToArray();

                PrintAttrs(methodAttrs);
            }
            
            Console.WriteLine("Properties:");
            foreach (var prop in type.GetProperties())
            {
                Console.WriteLine("Property - {0}", prop.Name);
                
                var propAttrs = prop.GetCustomAttributes().ToArray();
                PrintAttrs(propAttrs);
            }

            Console.WriteLine("Constructors:");
            foreach (var ctor in type.GetConstructors())
            {
                var parameters = ctor.GetParameters().Select(param => ($"{param.ParameterType.Name} {param.Name}"));
                Console.WriteLine($"{type.Name}({string.Join(", ", parameters)})");
            }
        }
    }
}
