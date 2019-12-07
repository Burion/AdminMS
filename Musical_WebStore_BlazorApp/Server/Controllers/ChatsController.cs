using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Musical_WebStore_BlazorApp.Shared;
using Musical_WebStore_BlazorApp.Client;
using Musical_WebStore_BlazorApp.Server.Data;
using Microsoft.EntityFrameworkCore;
using Musical_WebStore_BlazorApp.Server.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using Admin.Models;
using AutoMapper;
using Admin.ViewModels;
using Admin.ResultModels;

namespace Musical_WebStore_BlazorApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatsController : Controller
    {
        private readonly MusicalShopIdentityDbContext ctx;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public ChatsController(MusicalShopIdentityDbContext ctx, UserManager<User> userManager, IMapper mapper)
        {
            this.ctx = ctx;
            _userManager = userManager;
            _mapper = mapper;
        }
        private Task<Chat[]> GetChatsAsync() => ctx.Chats.ToArrayAsync();
        [HttpGet]
        public async Task<IEnumerable<ChatModel>> Get()
        {
            var chats = await GetChatsAsync();
            var chatsmodels = chats.Select(s => new ChatModel(){ Name = s.Name, Description = s.Description, ChatUsersNames = s.ChatUsers.Select(cu => cu.User.Email).ToList()});
            return chatsmodels;
        }
        [Route("getchats")]
        public async Task<ChatModel[]> GetChats()
        {
            var chats = await GetChatsAsync();
            var chatsmodels = chats.Select(s => new ChatModel(){ Name = s.Name, Description = s.Description, ChatUsersNames = s.ChatUsers.Select(cu => cu.User.Email).ToList()}).ToArray();
            return chatsmodels;
        }
        public Task<Message[]> GetMessagesAsync() => ctx.Messages.ToArrayAsync();
        [Route("getmessages")]
        public async Task<MessageModel[]> GetMessages()
        {
            var messages = await GetMessagesAsync();
            var messagesmodels = messages.Select(m => _mapper.Map<MessageModel>(m)).ToArray();
            return messagesmodels;
        }
        [HttpPost]
        [Route("addmessage")]
        public async Task<AddMessageResult> AddMessage(AddMessageModel model)
        {
            var mes = _mapper.Map<Message>(model);
            mes.Date = DateTime.Now;
            var email = User.Identity.Name;
            var user = await _userManager.FindByEmailAsync(email);
            mes.UserId = user.Id;
            ctx.Messages.Add(mes);
            await ctx.SaveChangesAsync();
            return new AddMessageResult() {Success = true};

            
        }

    }
}