using System;
using System.Collections.Generic;

namespace Admin.Models
{
    public class Device: Entity
    {
        public string Name {get;set;}
        public int TypeId {get;set;}
        public virtual DeviceType Type {get;set;}
        public int LocationId {get;set;}
        public virtual Location Location {get;set;}
        public float MaxAllow {get;set;}
        public float BoundAllowUpper {get;set;}
        public float BoundAllowLower {get;set;}
        public float MinAllow {get;set;}
        public int ModuleId {get;set;}
        public virtual Module Module {get;set;}
        
    }
}