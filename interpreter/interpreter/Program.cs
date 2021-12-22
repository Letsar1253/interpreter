using System;

namespace interpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var a = new FileReader();
            a.ReadFile();
            var str = a.GetString();
            var b = new Parser(str);
            b.ParsString();
            var list = b.GetList();
            var opsGenerator = new OPSGenerator(list);
            opsGenerator.Generate();
            var ops = opsGenerator.GetOPS();
            var interpreter = new Interpreter(ops);
            interpreter.Start();
        }
    }
}
