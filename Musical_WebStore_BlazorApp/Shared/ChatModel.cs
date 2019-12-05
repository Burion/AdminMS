using System;
using System.Collections.Generic;

namespace Admin.ViewModels
{
    public class ChatModel
    {
        public string Name {get;set;}
        public string Description {get;set;}
        public List<string> ChatUsersNames {get;set;}
    }
    
}