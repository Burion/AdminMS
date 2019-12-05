using System;
using Musical_WebStore_BlazorApp.Server.Data.Models;

namespace Admin.Models
{
    public class ChatUser
    {
        public int ChatId {get;set;}
        public virtual Chat Chat {get;set;}
        public string UserId {get;set;}
        public virtual User User {get;set;}

    }
}