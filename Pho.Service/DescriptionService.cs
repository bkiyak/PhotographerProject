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
}
