using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace bridge
{
    internal class OutputSystem : IOutput
    {
        Thread th;
        public SemaphoreSlim consoleSem;
        public SemaphoreSlim bufferSem;
        int _lenBuffer;
        public int LenBuffer 
        { 
            get { return _lenBuffer; } 
            set
            {
                if(value < 1) 
                    throw new Exception("OutSys: buffer must have at least one item");
            
                _lenBuffer = value;
            }
        }

        public List<IWritableItem> buffer { get; set; }

        public OutputSystem(int lenBuffer)
        {
            LenBuffer = lenBuffer;
            consoleSem = new SemaphoreSlim(0, 1);
            bufferSem = new SemaphoreSlim(0, LenBuffer);
        }

        public void StartPrintBuffer()
        {
            th = new Thread(new ThreadStart(PrintBuffer));
            th.Start();
        }
        public void StopPrintBuffer()
        {
#pragma warning disable SYSLIB0006
            th.Abort();
#pragma warning restore SYSLIB0006
        }

        public void AddItemToBuffer(IWritableItem item)
        {
            bufferSem.Wait();
            buffer.Add(item);
        }

        public void PrintBuffer()
        {
            // mettere una variabile per la condizione
            while (true)
            {
                // turn int method
                for(int i = 0; i < buffer.Count; i++)
                {
                    if (!buffer[i].IsFinished)
                        RemoveItemFromBuffer(buffer[i]);

                    EraseCar(buffer[i]);
                    PrintCar(buffer[i]);
                }
            }
        }

        public void RemoveItemFromBuffer(IWritableItem item)
        {
            buffer.Remove(item);
            bufferSem.Release();
        }

        public void EraseCar(IWritableItem item)
        {

        }

        public void PrintCar(IWritableItem item)
        {

        }

        public void PrintBridge()
        {

        }

        public void Write(string message, int x = 0, int y = 0)
        {

        }
    }

    internal interface IOutput
    {
        public void AddItemToBuffer(IWritableItem obj);
        public void StartPrintBuffer();
        public void StopPrintBuffer();
    }

    internal interface IWritableItem
    {
        public bool IsFinished { get; set; }
        public List<string> Texture { get; set; }
        public int GetPosition();
        public int PrevPosition { get; set; }
    }
}
