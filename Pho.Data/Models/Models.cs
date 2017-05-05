using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pho.Data
{
    public class Models
    {
        


    }
    public class BaseData
    {

       [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime IssueDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreateDate { get; set; }
        [DefaultValue(true)]
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
        public string TemplateName { get; set; }
    }
    public class Picture:BaseData
    {
        public int PictureId { get; set; }
        public string ImagePath { get; set; }
        public string ImgAlt { get; set; }
    }
    public class Content_Picture_Mapping:BaseData
    {
        public int Content_Picture_MappingId { get; set; }
        public int Content_Id { get; set; }
        public int Picture_Id { get; set; }
    }

    public class Menu : BaseData
    {
        public int MenuId { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public virtual Content Content{ get; set; }
        public string MenuUrl { get; set; }
        public virtual PageContext PageContext { get; set; }
    }
    public class WebSiteUtulity : BaseData
    {
        public int WebSiteUtulityId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
      
    }
    public class PageContext: BaseData
    {
        public int PageContextId { get; set; }
        public int MenuId { get; set; }
        public string PageHtmlTitle { get; set; }
        public string PageHtmlMainText { get; set; }

    }
}
