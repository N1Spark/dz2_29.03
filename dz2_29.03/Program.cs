using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dz2_29._03
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
    class TV
    {
        public void On() => Console.WriteLine($"TV is on\n");
        public void Off() => Console.WriteLine("TV is off\n");
    }
    class Microwave
    {
        public void On(int time)
        {
            Console.WriteLine($"Warming up food...\n");
            System.Threading.Thread.Sleep(time);
        }
        public void Off() => Console.WriteLine("The food is warmed up!\n");
    }
    class TVonCommand : ICommand
    {
        public TV tv;
        public TVonCommand(TV tv) => this.tv = tv;
        public void Undo() => tv.Off();
        public void Execute() => tv.On();
    }
    class MicrowaveCommand : ICommand
    {
        public Microwave microwave;
        private int time;
        public MicrowaveCommand(Microwave microwave, int t)
        {
            this.microwave = microwave;
            time = t;
        }
        public void Undo() => microwave.Off();
        public void Execute()
        {
            microwave.On(time);
            microwave.Off();
        }
    }
    class Receiver
    {
        public ICommand command;
        public void SetCommand(ICommand command) => this.command = command;
        public void PressButton()
        {
            if (command != null)
                command.Execute();
        }
        public void PressUndo()
        {
            if (command != null)
                command.Undo();
        }
    }
    class Invoker
    {
        public ICommand command;
        public bool Undo;
        public Invoker(ICommand command, bool undo)
        {
            this.command = command;
            Undo = undo;
            Receiver controller = new Receiver();
            controller.SetCommand(command);
            controller.PressButton();
            if (Undo)
                controller.PressUndo();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            TV tv = new TV();
            ICommand command = new TVonCommand(tv);
            Invoker inv = new Invoker(command, true);
            Microwave microwave = new Microwave();
            command = new MicrowaveCommand(microwave, 3000);
            Invoker inv2 = new Invoker(command, false);
        }
    }
}
