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
}
