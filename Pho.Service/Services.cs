using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Pho.Core;
using Pho.Data;

namespace Pho.Service
{
    public class Services
    {
        public Services()
        {
            Mapper.CreateMap<Pho.Data.Content, Content>();
            Mapper.CreateMap<Pho.Data.PageContext,PageContext>();
            Mapper.CreateMap<Pho.Data.Menu, Menu>();

            Mapper.CreateMap<Pho.Data.Picture, Pho.Service.Picture>();
            Mapper.CreateMap<Pho.Data.User, Pho.Service.User>();
            Mapper.CreateMap<Pho.Data.Content_Picture_Mapping, Pho.Service.Content_Picture_Mapping>();
            Mapper.CreateMap<Pho.Data.WebSiteUtulity, Pho.Service.WebSiteUtulity>();
            CacheProvider.Instance = new DefaultCacheProvider();
        }
    }
  
}
