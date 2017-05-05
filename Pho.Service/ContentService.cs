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
}
