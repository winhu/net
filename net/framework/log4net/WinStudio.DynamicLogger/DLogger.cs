using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Filter;
using log4net.Layout;
using log4net.Repository;

namespace WinStudio.DynamicLogger
{
    public class DLogger
    {
        private static List<string> _repositories = new List<string>();
        private static string _localpath = @"c:\log";
        public static ILog GetLogger<T>(string code) where T : class
        {
            return GetLogger(typeof(T), code);
        }
        public static ILog GetLogger(Type type, string code)
        {
            var rep = string.Format("{0}.{1}", type.FullName, code);
            if (!_repositories.Contains(rep))
            {
                LevelRangeFilter levfilter = new LevelRangeFilter();
                levfilter.LevelMax = log4net.Core.Level.Off;
                levfilter.LevelMin = log4net.Core.Level.All;
                levfilter.ActivateOptions();
                //Appender

                RollingFileAppender appender = new RollingFileAppender();

                appender.AppendToFile = true;
                appender.DatePattern = "yyyyMMdd'.log'";
                appender.MaxFileSize = 10240;
                appender.MaxSizeRollBackups = 700;
                appender.RollingStyle = RollingFileAppender.RollingMode.Date;
                appender.File = Path.Combine(_localpath, Path.Combine(rep.Split('.')), "log");
                appender.ImmediateFlush = true;
                appender.LockingModel = new FileAppender.MinimalLock();

                appender.Name = code;
                appender.AddFilter(levfilter);

                PatternLayout layout = new PatternLayout("[%date][%-5p] - %message%newline");
                layout.Header = "------ New session ------" + Environment.NewLine;
                layout.Footer = "------ End session ------" + Environment.NewLine;

                appender.Layout = layout;
                appender.ActivateOptions();

                ILoggerRepository repository = LogManager.CreateRepository(rep);
                BasicConfigurator.Configure(repository, appender);
                _repositories.Add(rep);
            }

            return LogManager.GetLogger(rep, code);
        }

        public static void SetLocalPath(string path)
        {
            _localpath = path;
        }
    }

    public class test
    {
        public void Test()
        {
            A a = new A() { Name = "a", Age = 1 };
            B b = new B() { Name = "b", Age = 1 };

            ILog la = DLogger.GetLogger<A>(a.Name);
            la.Info(a.ToString());
            ILog lb = DLogger.GetLogger<B>(b.Name);
            lb.Info(a.ToString());
        }
    }

    public class A
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public override string ToString()
        {
            return string.Format("I am {0}, age is {1}", Name, Age);
        }
    }
    public class B
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public override string ToString()
        {
            return string.Format("I am {0}, age is {1}", Name, Age);
        }
    }
}
