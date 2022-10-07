﻿using System.Text.RegularExpressions;

namespace MarsRover
{
    public abstract class CommandExecuter : ICommandExecuter
    {
        public abstract Regex RegexCommandPattern { get; }

        public abstract string ExecuteCommand(string command);

        public bool MatchCommand(string command)
        {
            return RegexCommandPattern.IsMatch(command);
        }
    }
}