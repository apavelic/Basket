using Basket.Core.Infrastructure;

namespace Basket.Core.Interfaces
{
	public interface ILogger
	{
		void Log(string message, LogType type);
	}
}
