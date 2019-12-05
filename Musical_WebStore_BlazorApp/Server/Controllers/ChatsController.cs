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

    }
}