﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SineahBot.Tools
{
    public class MudTimer
    {
        protected Thread thread;
        protected DateTime startTime;
        protected int seconds;
        public bool expired { get; protected set; } = false;
        public MudTimer(int seconds, Action handler)
        {
            this.seconds = seconds;
            (thread = new System.Threading.Thread(() =>
            {
                Thread.Sleep(seconds * 1000);
                handler?.Invoke();
                expired = true;
            })).Start();
            startTime = DateTime.Now;
        }
        public int GetRemainingSeconds()
        {
            return seconds - (DateTime.Now - startTime).Seconds;
        }
    }

    public class CancelableMudTimer : MudTimer
    {
        public CancelableMudTimer(int seconds, Action handler) : base(seconds, handler) { }

        public void Cancel()
        {
            thread.Abort();
        }
    }

    public class MudInterval
    {
        protected Thread thread;
        protected DateTime startTime;
        protected int interval;
        public void Interupt()
        {
            if (thread != null) thread.Abort();
        }

        public bool expired { get; protected set; } = false;
        public MudInterval(int interval, Action handler)
        {
            this.interval = interval;
            (thread = new System.Threading.Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(interval * 1000);
                    handler?.Invoke();
                }
            })).Start();
            startTime = DateTime.Now;
        }
    }
}