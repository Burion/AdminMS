using System;
using System.Collections.Generic;

namespace Admin.Models
{
    public class Module: Entity
    {
        public string Name {get;set;}
        public string Location {get;set;}
        public virtual IEnumerable<Device> Devices {get;set;}
    }
}