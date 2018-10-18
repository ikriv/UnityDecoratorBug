using System;

#if USE_MICROSOFT_PRACTICES_NAMESPACE
    using Microsoft.Practices.Unity;
#else
    using Unity;
    using Unity.Injection;
#endif

namespace UnityDecoratorBug.Common
{
    /// <summary>
    /// Simple string writer interface
    /// </summary>
    public interface IWriter
    {
        void Write(string s);
    }

    /// <summary>
    /// Writes strings to console
    /// </summary>
    public class ConsoleWriter : IWriter
    {
        public void Write(string s)
        {
            Console.WriteLine(s);
        }
    }

    /// <summary>
    /// Adds time stamp to written strings
    /// </summary>
    public class TimeStampDecorator : IWriter
    {
        private readonly IWriter _source;

        public TimeStampDecorator(IWriter source)
        {
            _source = source;
        }

        public void Write(string s)
        {
            var timeStr = DateTime.UtcNow.ToString("HH:mm:ss.fff UTC: ");
            _source.Write(timeStr + s);
        }
    }

    /// <summary>
    /// Writes greeting to a writer
    /// </summary>
    public class Greeter
    {
        private readonly IWriter _writer;

        public Greeter(IWriter writer)
        {
            _writer = writer;
        }

        public void WriteGreeting()
        {
            _writer.Write("Hello, Unity");
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer();
            container.RegisterType<IWriter, TimeStampDecorator>(
                new InjectionFactory(c => new TimeStampDecorator(c.Resolve<ConsoleWriter>())));

            var greeter = container.Resolve<Greeter>();
            greeter.WriteGreeting();
        }
    }
}