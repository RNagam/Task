using System.Text.RegularExpressions;

namespace MarsRover
{
    public interface ICommandExecuter
    {
        Regex RegexCommandPattern { get; }
        string ExecuteCommand(string command);
        bool MatchCommand(string command);
    }
}