
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.Viewmodel.CommentsViewModel;
using RedSocial.Core.Application.Viewmodel.PublicationViewModel;
using RedSocial.Core.Domain.Entities;
using RedSocial.Middlewares;

namespace RedSocial.Controllers
{
    [Authorize]
    public class PublicationsController : Controller
    {
    
        private readonly IPublicationServices _publicationServices;
        private readonly IDboUserServices _userServices;
        private readonly ICommentServices _commentServices;
        public PublicationsController(IPublicationServices publicationServices, IDboUserServices userServices, ICommentServices commentServices)
        {
            _publicationServices = publicationServices;
            _userServices = userServices;
            _commentServices = commentServices;
        }
 
        public async Task <IActionResult> Index(string userId)
        {

           var user = await  _userServices.GetForIdentityId(userId);

            var publication = await _publicationServices.GetAllDesc(user.Id);
            
            foreach(var i in publication)
            {
                i.comments = await _commentServices.GetCommentByPublication(i.Id);
               
            }
            return View(publication);
        }
    
        public async Task<IActionResult> Create()
        {
            return View(new PublicationViewModel() );
        }
    
        [HttpPost]
        public async Task<IActionResult> Create(PublicationPostViewModel publicVm)
        {
            if(publicVm.Text == null && publicVm.MediaPublicationVideo == null && publicVm.file == null)
            {

                var us = await _userServices.GetForIdentityId(publicVm.IdenityUserId);

                var publication = await _publicationServices.GetAllDesc(us.Id);

                foreach (var i in publication)
                {
                    i.comments = await _commentServices.GetCommentByPublication(i.Id);
                }

                ViewBag.alerta = "Probar Alerta";

                return View("Index", publication);
            }
             
            if (!ModelState.IsValid)
            {
                var us = await _userServices.GetForIdentityId(publicVm.IdenityUserId);

                var publication = await _publicationServices.GetAllDesc(us.Id);

                foreach (var i in publication)
                {
                    i.comments = await _commentServices.GetCommentByPublication(i.Id);
                }

                ViewBag.alerta = "Probar Alerta";

                return View("Index", publication);  
            }

            
            var user = await _userServices.GetForIdentityId(publicVm.IdenityUserId);

            if (user is not null)
            {
                publicVm.UserId = user.Id;

               publicVm.Date = DateTime.Now;
              var Vm =  await _publicationServices.AddAsync(publicVm);

                if (publicVm.file is not null)
                {
                    if (Vm != null && Vm.Id != 0)
                    {
                        Vm.MediaPublicationImg = UploadFile(publicVm.file, Vm.Id);

                        await _publicationServices.Editar(Vm, Vm.Id);
                    }
                }


            }

            return RedirectToAction("Index", new { userId = publicVm.IdenityUserId });
        }
     
        public async Task<IActionResult> Edit(int Id)
        {
            var publication = await _publicationServices.GetById(Id);
            return View(publication);
        }
      
        [HttpPost]
        public async Task<IActionResult> Edit(PublicationPostViewModel publicVm)
        {

            PublicationPostViewModel publiccation = await _publicationServices.GetById(publicVm.Id);

            publicVm.MediaPublicationImg = UploadFile(publicVm.file, publiccation.Id, true, publiccation.MediaPublicationImg);

            publicVm.UserId = publiccation.UserId;  
            await _publicationServices.Editar(publicVm, publicVm.Id);

            return RedirectToAction("Index", new { userId = publicVm.IdenityUserId });
        }



        public async Task<IActionResult> Delete()
        {
         
            return View();
        }
     
        public async Task<IActionResult> Delete(int Id, string userId)
        {

             await _publicationServices.Eliminar(Id);

            //Get Directory Path
            string BasePath = $"/img/publication/{Id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{BasePath}");

            if (Directory.Exists(path))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    file.Delete();
                }

                foreach (DirectoryInfo folder in directoryInfo.GetDirectories())
                {
                    folder.Delete(true);
                }

                Directory.Delete(path);
            }

            return RedirectToAction("Index", new { userId = userId });
        }

       

        private string UploadFile(IFormFile file, int Id, bool isEditMode = false, string imageURL = "")
        {
            if (isEditMode && file == null)
            {
                return imageURL;
            }

            //Get Directory Path
            string BasePath = $"/img/publication/{Id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{BasePath}");

            //Create Folder if no exits
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }


            //GetFilePath
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldImage = imageURL.Split("/");
                string olImageName = oldImage[^1];
                string completeImageOldPath = Path.Combine(path, olImageName);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }

            };
            return $"{BasePath}/{fileName}";
        }

    }
}
