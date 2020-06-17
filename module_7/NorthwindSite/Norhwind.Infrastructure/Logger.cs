using Newtonsoft.Json;
using PostSharp.Aspects;
using System;
using System.IO;
using System.Text;

namespace Norhwind.Infrastructure
{
    [Serializable]
    public class Logger : OnMethodBoundaryAspect
    {
        private readonly string _file = "log.txt";

        public override void OnEntry(MethodExecutionArgs args)
        {
            if (!args.Method.IsConstructor)
            {
                var method = args.Method;
                var methodParams = method.GetParameters();
                var arguments = args.Arguments;
                var parameters = new StringBuilder();

                for (var i = 0; i < methodParams.Length; i++)
                {
                    var paramNane = methodParams[i].Name;
                    var paramValue = JsonConvert.SerializeObject(arguments[i], Formatting.Indented, new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    });
                    parameters.AppendFormat("{0} = {1} ", paramNane, paramValue);
                }

                LogToFile($"{DateTime.UtcNow} call {method.Name} with params: {parameters}");
            }
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            if (!args.Method.IsConstructor)
            {
                var method = args.Method;
                var result = JsonConvert.SerializeObject(args.ReturnValue, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });

                LogToFile($"{DateTime.UtcNow} {method.Name} returns: {result}");
            }
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
