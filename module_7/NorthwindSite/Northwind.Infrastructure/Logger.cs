using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Castle.DynamicProxy;
using Newtonsoft.Json;

namespace Northwind.Infrastructure
{
    public class Logger : IInterceptor
    {
        private readonly string _file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs.txt");
        public void Intercept(IInvocation invocation)
        {
            var parameters = new StringBuilder();

            var method = invocation.Method;
            var methodParams = method.GetParameters();
            var arguments = invocation.Arguments;


            for (var i = 0; i < methodParams.Length; i++)
            {
                var paramName = methodParams[i].Name;
                var paramValue = JsonConvert.SerializeObject(arguments[i]);
                parameters.AppendFormat($"{paramName} = {paramValue}");
            }


            Debug.WriteLine($"{DateTime.UtcNow}:Calling {method.Name} from Proxy with params: {parameters}");

            LogToFile($"{DateTime.UtcNow}:Calling {method.Name} from Proxy with params: {parameters}");

            invocation.Proceed();

            var result = JsonConvert.SerializeObject(invocation.ReturnValue, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });

            LogToFile($"{DateTime.Now}:Calling {method.Name} from Proxy returns: {result}");
        }

        private void LogToFile(string data)
        {
            using (StreamWriter sw = File.AppendText(_file))
            {
                sw.WriteLine(data);
            }
        }
    }
}
