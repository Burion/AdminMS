using System;
namespace Admin.Models
{
    public class Location: Entity
    {
        public string Name {get;set;}
        public string Address {get;set;}
        public string Description {get;set;}
    }
}