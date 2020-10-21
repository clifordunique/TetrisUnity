using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts
{
    class Block
    {
        public int Number { get; set; }
        //public GameObject gameObject{ get; set; }
        
        public Block(int Number_ = 0)
        {
            Number = Number_;
            //gameObject = null;
        }
    }
}
