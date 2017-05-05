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

    public class DescriptionService : Services
    {
        public WebSiteUtulity GetByName(string name)
        {
            WebSiteUtulity retVal;

            var key = CacheKey.New(EntitiesEnum.Description, name);
            if (CacheProvider.Instance.IsExist(key))
            {
                retVal = CacheProvider.Instance.Get(key) as WebSiteUtulity;
            }
            else
            {
                using (var context = new Repository<Pho.Data.WebSiteUtulity>(new PhotoContext()))
                {
                    retVal = Mapper.Map<WebSiteUtulity>(context.Find(q => q.Name == name));
                }

                CacheProvider.Instance.Set(key, retVal);
            }

            return retVal;
        }
    }
    public class ContentService : Services
    {

        public Content GetById(int contentId)
        {
            Content retVal;

            var key = CacheKey.New(EntitiesEnum.Content, contentId.ToString());
            if (CacheProvider.Instance.IsExist(key))
            {
                retVal = CacheProvider.Instance.Get(key) as Content;
            }
            else
            {
                using (var context = new Repository<Pho.Data.Content>(new PhotoContext()))
                {
                    retVal = Mapper.Map<Content>(context.Find(q => q.ContentId == contentId));

                }


                CacheProvider.Instance.Set(key, retVal);
            }

            return retVal;
        }
        public List<Content> GetBySysName(string sysName)
        {
            List<Content> retVal = null;

            CacheKey key = CacheKey.New(EntitiesEnum.Content, sysName);
            if (CacheProvider.Instance.IsExist(key))
            {
                retVal = CacheProvider.Instance.Get(key) as List<Content>;
            }
            else
            {
                using (var context = new Repository<Pho.Data.Content>(new PhotoContext()))
                {
                    retVal = Mapper.Map<List<Content>>(context.Filter(content => content.SysName == sysName).ToList());
                }


                CacheProvider.Instance.Set(key, retVal);
            }

            return retVal;
        }


    }
    public class PictureService
    {
        public Picture GetById(int pictureId)
        {
            Picture retVal = null;

            var key = CacheKey.New(EntitiesEnum.Picture, pictureId.ToString());
            if (CacheProvider.Instance.IsExist(key))
            {
                retVal = CacheProvider.Instance.Get(key) as Picture;
            }
            else
            {
                using (var context = new Repository<Pho.Data.Picture>(new PhotoContext()))
                {
                    retVal = Mapper.Map<Picture>(context.Find(pictureId));
                }


                CacheProvider.Instance.Set(key, retVal);
            }

            return retVal;
        }
    }
    public class ContentPictureService
    {
        public Content_Picture_Mapping GetById(int contentPictureId)
        {
            Content_Picture_Mapping retVal = null;

            var key = CacheKey.New(EntitiesEnum.Content_Picture_Mapping, contentPictureId.ToString());
            if (CacheProvider.Instance.IsExist(key))
            {
                retVal = CacheProvider.Instance.Get(key) as Content_Picture_Mapping;
            }
            else
            {
                using (var context = new Repository<Pho.Data.Content_Picture_Mapping>(new PhotoContext()))
                {
                    retVal = Mapper.Map<Content_Picture_Mapping>(context.Find(contentPictureId));
                }


                CacheProvider.Instance.Set(key, retVal);
            }

            return retVal;
        }
        public List<Content_Picture_Mapping> GetByContentId(int contentId)
        {
            List<Content_Picture_Mapping> retVal = null;

            CacheKey key = CacheKey.New(EntitiesEnum.Content_Picture_Mapping, contentId.ToString());
            if (CacheProvider.Instance.IsExist(key))
            {
                retVal = (List<Content_Picture_Mapping>)CacheProvider.Instance.Get(key);
            }
            else
            {
                using (var context = new Repository<Pho.Data.Content_Picture_Mapping>(new PhotoContext()))
                {
                    retVal = Mapper.Map<List<Content_Picture_Mapping>>(context.Filter(w => w.Content_Id == contentId).ToList());
                }
                
                CacheProvider.Instance.Set(key, retVal);
            }

            return retVal;
        }
    }
    public class MenuService
    {
        public Menu GetById(int menuId)
        {
            Menu retVal = null;

            CacheKey key = CacheKey.New(EntitiesEnum.Menu, menuId.ToString());
            if (CacheProvider.Instance.IsExist(key))
            {
                retVal = CacheProvider.Instance.Get(key) as Menu;
            }
            else
            {
                using (var context = new Repository<Pho.Data.Menu>(new PhotoContext()))
                {
                    retVal = Mapper.Map<Menu>(context.Find(menuId));
                }
                
                CacheProvider.Instance.Set(key, retVal);
            }

            return retVal;
        }
        public Menu GetByUrl(string url)
        {
            Menu retVal;

            var key = CacheKey.New(EntitiesEnum.Menu, url);
            if (CacheProvider.Instance.IsExist(key))
            {
                retVal = CacheProvider.Instance.Get(key) as Menu;
            }
            else
            {
                using (var context = new Repository<Pho.Data.Menu>(new PhotoContext()))
                {
                    var result = context.Find(q => q.MenuUrl == url);
                    retVal = Mapper.Map<Menu>(result);
                }

                CacheProvider.Instance.Set(key, retVal);
            }

            return retVal;
        }
        public List<Menu> GetMenuItems()
        {
            List<Menu> retVal = null;

            var key = CacheKey.New(EntitiesEnum.Menu, "1000");
            if (CacheProvider.Instance.IsExist(key))
            {
                retVal = (List<Menu>)CacheProvider.Instance.Get(key);
            }
            else
            {
                using (var context = new Repository<Pho.Data.Menu>(new PhotoContext()))
                {
                    retVal = Mapper.Map<List<Menu>>(context.All().ToList());
                }

                CacheProvider.Instance.Set(key, retVal);
            }

            return retVal;
        }
    }

}
