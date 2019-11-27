using System;

namespace Admin.Models
{
    public class Metering: Entity
    {
        public DateTime Date {get;set;}
        public float Value {get;set;}
    }
}