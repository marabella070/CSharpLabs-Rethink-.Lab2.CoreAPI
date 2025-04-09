using CoreAPI.Core.Interfaces;
using Lab2.CoreAPI.Core.Models;

namespace Lab2.CoreAPI.Core.Controllers;

public class WorkshopThreadController
{
    private readonly IMoveable moveable;
    private readonly int formWidth;
    private readonly int formHeight;
    private readonly double interval;

    private volatile ThreadExecutionState state = ThreadExecutionState.NotStarted;
    public ThreadExecutionState State => state;

    public ThreadPriority Priority
    {
        get => thread?.Priority ?? ThreadPriority.Normal;
        set
        {
            if (thread != null && thread.IsAlive)
            {
                thread.Priority = value;
            }
        }
    }

    private Thread? thread;
    public string? ThreadName { get; private set; }
    private volatile bool running;
    private volatile bool paused;
    private readonly object pauseLock = new();

    public WorkshopThreadController(IMoveable moveable, int width, int height, double intervalSeconds)
    {
        this.moveable = moveable;
        formWidth = width;
        formHeight = height;
        interval = intervalSeconds;
    }

    public void Start() 
    {
        if (thread != null && thread.IsAlive)
        {
            return;
        }

        running = true;
        paused = false;
        state = ThreadExecutionState.Running;

        ThreadName = $"WorkshopThread-{Guid.NewGuid()}";

        thread = new Thread(Loop)
        {
            //! indicates that the thread is background, and its execution will not prevent the program from terminating. 
            //! Background threads are automatically terminated when the main thread finishes.
            IsBackground = true,
            Name = ThreadName
        };

        thread.Start();
    }

    public void Pause()
    {
        lock (pauseLock)
        {
            paused = true;

            if (state == ThreadExecutionState.Running)
            {
                state = ThreadExecutionState.WaitingToPause;
            }
        }
    }

    public void Resume()
    {
        lock (pauseLock)
        {
            paused = false;
            Monitor.PulseAll(pauseLock); // "wake up" all threads if they are waiting
            state = ThreadExecutionState.Resuming;
        }
    }

    public void Stop() 
    {
        running = false;

        // Suddenly the stream is on pause — it is necessary to "wake up"
        Resume();

        if (thread != null && thread.IsAlive)
        {
            thread.Join();
        }

        thread = null;
        state = ThreadExecutionState.Stopped;
    }

    private void Loop()
    {
        while (running)
        {
            lock (pauseLock)
            {
                while (paused && running)
                {
                    state = ThreadExecutionState.Paused;
                    Monitor.Wait(pauseLock);
                    if (!running) break;

                    if (!paused)
                    {
                        state = ThreadExecutionState.Resuming;
                    }
                }

                if (!running)
                {
                    break;
                }

                state = ThreadExecutionState.Running;
            }

            moveable.Move(interval, formWidth, formHeight);
            Thread.Sleep((int)(interval * 1000));
        }

        state = ThreadExecutionState.Stopped;
    }
}
