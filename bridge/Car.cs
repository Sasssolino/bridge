using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace bridge
{
    internal class Car : IWritableItem
    {
        private Thread thread;
        private int _plate;
        public int PrevPosition { get; set; }
        public List<string> Texture { get; set; }
        public int Plate
        {
            get { return _plate; }
            set
            {
                if (value < 0)
                    throw new Exception("car: invalid plate number");

                _plate = value;
            }
        }

        private int velocity;

        public bool IsFinished { get; set; }

        private IOutput _out;

        public Car(int num, IOutput output)
        {
            Plate = num;
            _out = output;
            PrevPosition = 0;
            Texture = new List<string>();
        }

        public void Start(int nChannel)
        {
            
        }

        public int GetPosition()
        {
            return 0;
        }


    }

    public enum CarState
    {
        running, unstarted, 
    }
}
