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
