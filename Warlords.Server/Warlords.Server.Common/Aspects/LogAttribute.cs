using System.Diagnostics.Contracts;
using log4net;
using PostSharp.Aspects;
using System;
using System.Text;

namespace Warlords.Server.Common.Aspects
{
    [Serializable]
    public class LogAttribute : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            var logger = LogManager.GetLogger(args.Instance.GetType());

            var stringBuilder = new StringBuilder();

            WriteListOfArguments(args, stringBuilder);

            logger.Debug(string.Format("Entered {0}({1})", args.Method.Name, stringBuilder));
        }

        public override void OnException(MethodExecutionArgs args)
        {
            var logger = LogManager.GetLogger(args.Instance.GetType());

            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Exception while executing:");
            stringBuilder.Append(args.Method.Name);
            stringBuilder.Append('('); 

            WriteListOfArguments(args, stringBuilder);

            // Write the exception message. 
            stringBuilder.AppendFormat("): Exception ");
            stringBuilder.Append(args.Exception.GetType().Name);
            stringBuilder.Append(": ");
            stringBuilder.Append(args.Exception.Message); 
 
            logger.Error(stringBuilder);
        }

        private static void WriteListOfArguments(MethodExecutionArgs args, StringBuilder stringBuilder)
        {
            Contract.Requires(args != null);
            for (int i = 0; i < args.Arguments.Count; i++)
            {
                if (i > 0)
                    stringBuilder.Append(", ");
                stringBuilder.Append(args.Arguments.GetArgument(i) ?? "null");
            }
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            var logger = LogManager.GetLogger(args.Instance.GetType());
            logger.Debug(string.Format("Leaving {0}", args.Method.Name));
        }
    }
}
