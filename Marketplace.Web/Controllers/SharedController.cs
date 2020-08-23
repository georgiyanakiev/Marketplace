using System;
using Marketplace.Entities;
using Marketplace.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Marketplace.Web.ViewModels;
using Microsoft.AspNet.Identity;

namespace Marketplace.Web.Controllers
{
    public class SharedController : Controller
    {
        SharedService service = new SharedService();


        [HttpPost]
        public JsonResult UploadPictures()
        {
            JsonResult result = new JsonResult();

            List<object> picturesJSON = new List<object>();

            var pictures = Request.Files;

            for (int g = 0; g < pictures.Count; g++)
            {
                var picture = pictures[g];

                var fileName = Guid.NewGuid() + Path.GetExtension(picture.FileName);

                var path = Server.MapPath("~/Content/images/") + fileName;

                picture.SaveAs(path);

                var dbPicture = new Picture();
                dbPicture.URL = fileName;

                int pictureID = service.SavePicture(dbPicture);

                picturesJSON.Add(new { ID = pictureID, pictureURL = fileName });
            }

            result.Data = picturesJSON;

            return result;
        }

        [HttpPost]
        public JsonResult LeaveComment(CommentViewModel model)
        {

            JsonResult result = new JsonResult();
            try
            {
                var comment = new Comment();
                comment.Text = model.Text;
                comment.Rating = model.Rating;
                comment.EntityID = model.EntityID;
                comment.RecordID = model.RecordID;
                comment.UserID = User.Identity.GetUserId();
                comment.TimeStamp = DateTime.Now;


                var res = service.LeaveComment(comment);

                result.Data = new { Success = res };
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = ex.Message };
            }
            return result;

        }
    }
}