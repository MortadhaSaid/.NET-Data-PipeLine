using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamedPipeLib
{
    class Event
    {
        public string UserKey { get; set; }
        public int IndexEvent { get; set; }
        public DateTime DateEvent { get; set; }
        public string HeureEvent { get; set; }
        public short DoorNumber { get; set; }
        public short? UserNumber { get; set; }
        public short CodeEvent { get; set; }
        public short CodeControler { get; set; }
        public short IndiceControler { get; set; }
        public bool Selected { get; set; }
        public string NumAccessCard { get; set; }
        public sbyte? Data12 { get; set; }
        public short? Flux { get; set; }
        internal void CreateEvent(string[] Keys)
        {
            UserKey = Keys[0];
            IndexEvent = int.Parse(Keys[1]);
            DateEvent = Convert.ToDateTime(Keys[2]);
            HeureEvent = Keys[3];
            DoorNumber = (short)int.Parse(Keys[4]);
            UserNumber = (short)int.Parse(Keys[5]);
            CodeEvent = (short)int.Parse(Keys[6]);
            CodeControler = (short)int.Parse(Keys[7]);
            IndiceControler = (short)int.Parse(Keys[8]);
            Selected = Convert.ToBoolean(Keys[9]);
            NumAccessCard = Keys[10];
            Data12 = (sbyte?)int.Parse(Keys[11]);
            Flux = (short)int.Parse(Keys[12]);
        }
    }
}
