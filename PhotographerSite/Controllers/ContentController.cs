using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Pho.Service;
using PhotographerSite.Models;
using Content = PhotographerSite.Models.Content;
using Menu = PhotographerSite.Models.Menu;
using Picture = PhotographerSite.Models.Picture;

namespace PhotographerSite.Controllers
{
    public class ContentController : Controller
    {
        private readonly ContentService _contentService;
        private MenuService _menuService;
        private readonly ContentPictureService _contentPictureService;
        private readonly PictureService _pictureService;
        public ContentController()
        {
            _contentService =new ContentService();
            _contentPictureService=new ContentPictureService();
            _pictureService=new PictureService();
            _menuService=new MenuService();
            Mapper.CreateMap<Pho.Service.Menu, Menu>();
            Mapper.CreateMap<Pho.Service.Content, Content>();
        }

        public PartialViewResult MenuResult()
        {
            var menu = Mapper.Map<List<Menu>>(_menuService.GetMenuItems()); 
            return PartialView("Menu", menu);
        }
        public ActionResult ContentBlock(string url)
        {
            var menu = _menuService.GetByUrl(url);
            var content = _contentService.GetById(menu.Content.ContentId);
            var contentModel = PrepareContentModel(content,menu);

            return contentModel != null ? View(contentModel.TemplateName, contentModel) : View();
        }

        private Content PrepareContentModel(Pho.Service.Content content,Pho.Service.Menu menu)
        {
            if (content == null) return new Content();
            var contentModel = new Content
            {
                SysName = content.SysName,
                TemplateName = content.TemplateName,
                Title = menu.PageContext?.PageHtmlTitle,
                PageContext=menu.PageContext?.PageHtmlMainText,
                IsActive = content.IsActive,
                ContentId = content.ContentId,
                CreateDate = content.CreateDate,
                IssueDate = content.IssueDate,
                ContentPictureMappings = PrepareContentPictureModel(content.ContentId)
            };
            return contentModel;
        }

        private List<Picture> PrepareContentPictureModel(int contentId)
        {
            var contentPictureMapping = _contentPictureService.GetByContentId(contentId: contentId);
            var pictures = contentPictureMapping.Select(item => _pictureService.GetById(item.Picture_Id)).Select(pictureData => new Picture
            {
                ImagePath = pictureData.ImagePath, ImgAlt = pictureData.ImgAlt, IsActive = pictureData.IsActive, IssueDate = pictureData.IssueDate, PictureId = pictureData.PictureId, CreateDate = pictureData.CreateDate
            }).ToList();
            return pictures;
        }
    }
}