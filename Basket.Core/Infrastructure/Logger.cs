using Basket.Core.Interfaces;
using System;
using System.Diagnostics;

namespace Basket.Core.Infrastructure
{
	public class Logger : ILogger
	{
		public void Log(string message, LogType type)
		{
			Trace.WriteLine($"{message} [{type.ToString()}]");
		}
	}
}
