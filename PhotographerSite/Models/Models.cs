using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotographerSite.Models
{

    public class BaseData
    {

        public DateTime IssueDate { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
    }
    public class User:BaseData
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
       public string Name { get; set; }
       public string Surname { get; set; }

    }
    public class Content:BaseData
    {
        public int ContentId { get; set; }
        public string SysName { get; set; }
        public string Title { get; set; }
        public string SeeName { get; set; }
        public string TemplateName { get; set; }
        public string PageContext { get; set; }
        public virtual List<Picture> ContentPictureMappings { get; set; }
    }
    public class Picture:BaseData
    {
        public int PictureId { get; set; }
        public string ImagePath { get; set; }
        public string ImgAlt { get; set; }
    }
    public class ContentPictureMapping:BaseData
    {
        public int ContentPictureMappingId { get; set; }
        public int ContentId { get; set; }
        public virtual IList<Picture> Pictures{ get; set; }
    }

    public class Menu : BaseData
    {
        public int MenuId { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public virtual Content Content { get; set; }
        public string MenuUrl { get; set; }
    }
}
