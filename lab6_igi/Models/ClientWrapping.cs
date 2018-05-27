using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab1_ef;

namespace lab6_igi
{
    public class ClientWrapping : Client
    {
        public string RoomNo { get; set; }

        public ClientWrapping() : base() { }
    }
}
