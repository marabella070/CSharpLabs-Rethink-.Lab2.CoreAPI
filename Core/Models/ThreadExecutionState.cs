namespace Lab2.CoreAPI.Core.Models;

public enum ThreadExecutionState
{
    NotStarted,
    Running,
    Paused,
    WaitingToPause,
    Resuming,
    Stopped
}