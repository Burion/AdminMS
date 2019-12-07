using System;
using Musical_WebStore_BlazorApp.Server.Data.Models;

namespace Admin.Models
{
    public class Message: Entity
    {

        public string UserId {get;set;}
        public virtual User User {get;set;}
        public int ChatId {get;set;}
        public virtual Chat Chat {get;set;}
        public DateTime Date {get;set;}

        public string Text {get;set;}

    }
}