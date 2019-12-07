using Admin.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Musical_WebStore_BlazorApp.Server.Data.Models;
using Musical_WebStore_BlazorApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Musical_WebStore_BlazorApp.Server.Data
{
    public class MusicalShopIdentityDbContext : IdentityDbContext<User>
    {
        public MusicalShopIdentityDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<Comment> Comments {get;set;}
        public DbSet<Star> Stars {get;set;}
        public DbSet<Location> Locations {get;set;} 
        public DbSet<Device> Devices {get;set;}
        public DbSet<DeviceType> DeviceTypes {get;set;}
        public DbSet<Module> Modules {get;set;}
        public DbSet<Metering> Meterings {get;set;}
        public DbSet<Chat> Chats {get;set;}
        public DbSet<ChatUser> ChatUsers {get;set;}
        public DbSet<Message> Messages {get;set;}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ChatUser>().HasKey(cu => new {cu.ChatId, cu.UserId});
            builder.Entity<ChatUser>().HasOne(cu => cu.Chat).WithMany(u => u.ChatUsers).HasForeignKey(cu => cu.ChatId);
            builder.Entity<Guitar>()
                .HasBaseType<Instrument>();

            builder.Entity<Pedal>()
                .HasBaseType<Instrument>();

            builder.Entity<Amplifier>()
                .HasBaseType<Instrument>();

            builder.Entity<Instrument>()
                .HasDiscriminator<string>("TypeName")
            .HasValue<Instrument>(nameof(Instrument))
            .HasValue<Amplifier>(nameof(Amplifier))
            .HasValue<Guitar>(nameof(Guitar))
            .HasValue<Pedal>(nameof(Pedal))
            ;

            builder.Seed();

            base.OnModelCreating(builder);
        }
    }

    public static class ModelBuilderExtensions
    {
        private static string[] GetName(string type)
        {
            string[] guitarTitles = new string[]{"Yamaha S1209", "Fender CD60", "Takamine EF360S-TT", "Taylor 314ce",
                "Blueridge BR-160", "Martin 16 Series D-16GT", "Yamaha A Series A3M", "Seagull S6 Original",
                "The Loar LH-204 Brownstone", "Yamaha APX600"};
            string[] pedalTitles = new string[]{"MXR Phase 90", "Boss DD-7 Digital", "Ibanez TS808 Tube Screamer", "Electro-Harmonix SOULFOOD",
                "Boss CH-1 Stereo", "MXR Dyna Comp Effects Pedal", "Dunlop The Original Crybaby", "Boss RC-3 Loop Station Pedal",
                "	Electro-Harmonix LPB-1", "Boss FV-500H"};
            string[] amplifierTitles = new string[]{ "Peavey 6505", "Vox AC4HW1", "EVH 5150III 50W", "Vox Custom AC15C2", 
                "Blackstar HT Stage 60 MKII", "Orange Crush Pro CR60C", "Boss Katana KTN-100", "Fender Champion 100",
                "AER Compact 60", "Roland Cube Street" };

            switch (type)
            {
                case "guitar":
                    return guitarTitles;
                case "pedal":
                    return pedalTitles;
                case "amplifier":
                    return amplifierTitles;
            }
            return null;
        }
        private static string[] GetDescription(string type)
        {
            string[] guitarDescriptions = new string[] 
            {
                "Vintage tone and style to spare with this high-end Takamine.", "A gorgeous premium all-solid-wood Taylor.",
                "Great looking dreadnaught body guitar from Bristol.", "Classic style with this solid-wood Martin.",
                "A mid-range performance-focused acoustic with a high-end feel.", "Beautiful style, quality and playability with this Seagull.",
                "A taste of vintage America with this stylish Chinese guitar.", "Affordable slimline Yamaha with upgraded performance.",
                "A proud representative from the Takamine family.", "FG800 shows what made Yamaha's FG series so legendary to begin with." 
            };
            string[] pedalDescriptions = new string[]
            {
                "Focused pedal that excels at distortion, overdrive and classic fuzz sounds.","British tube distortion with outstanding dynamic range.",
                "Fully analog distortion that brings an impressive range of tones and gain.","Unique features and great tones with an outstanding pedigree.",
                "More refined affordable dist box that brings great performance and limited versatility.","A faithful reproduction of the classic Tube Screamer sound.",
                "Another classic stompbox that brings impressive performance and abundant flexibility.","Very light distortion that caters to Rock, but brings high quality tone.",
                "Quintessential distortion pedal used both by legendary guitar players and the masses.","One of the best and only Klon Centaur clones on the market."
            };
            string[] amplifierDescriptions = new string[]
            {
                "One of the most legendary amps for the fans of heavier tone.","A high-end hand-wired tube amp with great style.",
                "A legendary tone monster designed by Eddie Van Halen.","A modern reimagining of the iconic AC15.",
                "Superb flexibility and power from this all-tube combo.","A stand-out combo that crushes stage performances.",
                "Flexible combo providing legendary Boss tone at an attractive price.","A classic Fender combo amp with great power.",
                "Huge tone and power from this high-end acoustic amp.","A solid dual-channel choice for street performers."
            };

            switch (type)
            {
                case "guitar":
                    return guitarDescriptions;
                case "pedal":
                    return pedalDescriptions;
                case "amplifier":
                    return amplifierDescriptions;
            }
            return null;
        }
        private static (int price,int quantity) GetRandomPrice()
        {
            int[] prices = new int[] { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000};
            int[] quantities = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Random rnd = new Random();
            return (prices[rnd.Next(prices.Length)], quantities[rnd.Next(quantities.Length)]);
        }
        private static IEnumerable<Guitar> GetGuitars(int length, int offset)
        {

            string[] guitarTitles = GetName("guitar");
            string[] guitarDescription = GetDescription("guitar");

            int j = 1;
            for (int i = 0; i < length; i++,j++)
            {
                (int price, int quantity) priceQuantity = GetRandomPrice();
                yield return new Guitar
                {
                    Id = -(j + offset),
                    Title = guitarTitles[i],
                    Price = priceQuantity.price,
                    Quantity = priceQuantity.quantity,
                    Description = guitarDescription[i],
                    Image = $"images/Guitars/{j}.jpg"
                };
            }
        }

        private static IEnumerable<Amplifier> GetAmplifiers(int length, int offset)
        {
            string[] guitarTitles = GetName("amplifier");
            string[] guitarDescription = GetDescription("amplifier");

            int j = 1;
            for (int i = 0; i < length; i++, j++)
            {
                (int price, int quantity) priceQuantity = GetRandomPrice();
                yield return new Amplifier
                {
                    Id = -(j + offset),
                    Title = guitarTitles[i],
                    Price = priceQuantity.price,
                    Quantity = priceQuantity.quantity,
                    Description = guitarDescription[i],
                    Image = $"images/Amplifiers/{j}.jpg"
                };
            }
        }

        private static IEnumerable<Location> GetLocations()
        {
            IEnumerable<Location> list = new List<Location>()
            {
                new Location() {Id = -1, Name = "First Data Store", Address = "Bakulina 35"},
                new Location() {Id = -2, Name = "Second Data Store", Address = "Bakulina 13"},
                new Location() {Id = -3, Name = "Third Data Store", Address = "Toloka 29"},

            };
            return list;
        }


        private static IEnumerable<DeviceType> GetTypes()
        {
            List<DeviceType> types = new List<DeviceType>()
            {
                new DeviceType(){Id = -1, Name = "Temperature"},
                new DeviceType(){Id = -2, Name = "Pressure"},
                new DeviceType(){Id = -3, Name = "Шllumination"}
            };
            return types;
        }

        private static IEnumerable<Device> GetDevices()
        {
            List<Device> devs = new List<Device>()
            {
                new Device() {Id = -1, Name = "1-st Beer Block"},
                new Device() {Id = -2, Name = "2-st Beer Block"}
            };
            return devs;
        }
        private static IEnumerable<Pedal> GetPedals(int length, int offset)
        {
            string[] guitarTitles = GetName("pedal");
            string[] guitarDescription = GetDescription("pedal");

            int j = 1;
            for (int i = 0; i < length; i++, j++)
            {
                (int price, int quantity) priceQuantity = GetRandomPrice();
                yield return new Pedal
                {
                    Id = -(j + offset),
                    Title = guitarTitles[i],
                    Price = priceQuantity.price,
                    Quantity = priceQuantity.quantity,
                    Description = guitarDescription[i],
                    Image = $"images/Pedals/{j}.jpg"
                };
            }
        }
        public static void Seed(this ModelBuilder blder)
        {
            var @base = 10;

            var pedals = GetPedals(@base, @base * 0).ToArray();
            var amps = GetAmplifiers(@base, @base * 1).ToArray();
            var guitars = GetGuitars(@base, @base * 2).ToArray();
            var locations = GetLocations().ToArray();
            var devices = GetDevices().ToArray();

            blder.Entity<User>()
                .HasData(
                    new User
                    {
                        Id = "qwjehfpkjnvdpjwn3pro",
                        Email = "vladislavburyak00@gmail.com",
                        UserName = "Fucker"
                    }
                );
            blder.Entity<Chat>().HasData
            (
                new Chat()
                {
                    Id = -1, 
                    Name = "Fucker's chat",
                    Description = "Motherfucker's chat"                    
                },
                new Chat()
                {
                    Id = -2, 
                    Name = "Second Fucker's chat",
                    Description = "Second Motherfucker's chat"                    
                }
            );

            blder.Entity<Message>().HasData(
                new Message() 
                {
                    Id = -1, 
                    UserId = "qwjehfpkjnvdpjwn3pro",
                    ChatId = -1,
                    Date = DateTime.Now,
                    Text = "Hello, friend!"
                },
                new Message() 
                {
                    Id = -2, 
                    UserId = "qwjehfpkjnvdpjwn3pro",
                    ChatId = -1,
                    Date = DateTime.Now,
                    Text = "I just wanted to ask you something."
                },
                new Message() 
                {
                    Id = -3, 
                    UserId = "qwjehfpkjnvdpjwn3pro",
                    ChatId = -2,
                    Date = DateTime.Now,
                    Text = "Тошо ты пидор, епта."
                },
                new Message() 
                {
                    Id = -4, 
                    UserId = "qwjehfpkjnvdpjwn3pro",
                    ChatId = -2,
                    Date = DateTime.Now,
                    Text = "А может это ты пидор?"
                }
            );


            blder.Entity<ChatUser>().HasData
            (
                new ChatUser()
                {
                    ChatId = -1,
                    UserId = "qwjehfpkjnvdpjwn3pro"
                }    
            );

            blder.Entity<DeviceType>()
                .HasData(
                    new DeviceType()
                    {   Id = -1,
                        Name = "Temperature"
                    },
                    new DeviceType()
                    {   Id = -2,
                        Name = "Pressure"
                    }
                );

            blder.Entity<Device>()
                .HasData(
                    new Device()
                    {
                        Id = -1,
                        Name = "1-st Beer Module",
                        TypeId = -1,
                        LocationId = -1
                    },
                    new Device()
                    {
                        Id = -2,
                        Name = "2-nd Beer Module",
                        TypeId = -2,
                        LocationId = -1,
                        ModuleId = -1
                    }
                );
                Random random = new Random();
                List<Metering> meterings = new List<Metering>();
                for(int x = 1; x < 50; x++)
                {
                    int day = random.Next(5, 10);
                    int hour = random.Next(5, 10);
                    int minute = random.Next(5, 10);
                    int second = random.Next(5, 10);
                    int deviceId = -((x % 2) + 1);
                    Metering metering = new Metering()
                    {
                        Date = new DateTime(2019, 12, day, hour, minute, second),
                        DeviceId = deviceId,
                        Id = -x,
                        Value = (float)random.Next(50, 100)
                    };
                    meterings.Add(metering);
                }
            blder.Entity<Metering>()
                .HasData(
                    meterings
                );
                
            blder.Entity<Module>()
                .HasData(
                    new Module()
                    {
                        Id = -1,
                        Location = "Right wing of office center",
                        Name = "Cooling block"
                    }
                );
            blder.Entity<Location>()
                .HasData(locations);

            blder.Entity<Pedal>()
                .HasData(pedals);

            blder.Entity<Guitar>()
                .HasData(guitars);

            blder.Entity<Amplifier>()
                .HasData(amps);
        }
    }
}
