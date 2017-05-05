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
}
