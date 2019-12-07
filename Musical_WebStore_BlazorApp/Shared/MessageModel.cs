using System;
using Musical_WebStore_BlazorApp.Shared;

namespace Admin.ViewModels
{
    public class MessageModel
    {
        public UserLimited User {get;set;}
        public DateTime Date {get;set;}
        public string Text {get;set;}
    }
}