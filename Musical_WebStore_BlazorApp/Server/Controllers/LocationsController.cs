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
using Admin.Models;
using AutoMapper;
using Admin.ViewModels;

namespace Musical_WebStore_BlazorApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationsController : Controller
    {
        private readonly MusicalShopIdentityDbContext ctx;
        private readonly IMapper _mapper;
        public LocationsController(MusicalShopIdentityDbContext ctx, IMapper mapper)
        {
            this.ctx = ctx;
            _mapper = mapper;
        }

        private Task<Location[]> GetLocationsAsync() => ctx.Locations.ToArrayAsync();

        [HttpGet]
        public async Task<IEnumerable<LocationViewModel>> Get()
        {
            var locations = await GetLocationsAsync();
            var locationsModels = locations.Select(l => _mapper.Map<LocationViewModel>(l)).ToArray();
            for(int x = 0; x < locationsModels.Count(); x++)
            {
                locationsModels[x].Status = x % 3 + 1;
            }
            return locationsModels;
        }

        public async Task<Module[]> GetDevicesAsync() => await ctx.Modules.ToArrayAsync();
        [Route("getmodules")]
        public async Task<IEnumerable<ModuleViewModel>> GetDevices()
        {
            var devices = await GetDevicesAsync();
            var deviceViewModels = devices.Select(dev => new ModuleViewModel(){ Name = dev.Name, Location = dev.Location});
            return deviceViewModels;
        }

        
        public async Task<Metering[]> GetMeteringsAsync() => await ctx.Meterings.ToArrayAsync();
        
        [Route("getmeterings")]
        public async Task<IEnumerable<MeteringModel>> GetMeterings()
        {
            var meterings = await GetMeteringsAsync();
            var meteringsModels = meterings.Select(m => _mapper.Map<MeteringModel>(m));
            return meteringsModels;
        }
    }
}

