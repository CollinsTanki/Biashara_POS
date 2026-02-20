using Biashara_POS.Data;
using Biashara_POS.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biashara_POS.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment environment;

        public CompanyController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment = environment;
        }
        public IActionResult Index()
        {
            var companies = context.Companies.ToList();
            return View(companies);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CompanyDto companyDto)
        {
            if (companyDto.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "The Image is required.");
            }
            if (!ModelState.IsValid)
            {
                return View(companyDto);
            }

            // Save the uploaded image
            string newFileName = DateTime.Now.ToString("yyyyMMddHHssfff");
            newFileName += Path.GetExtension(companyDto.ImageFile!.FileName);

            string imageFullPath = environment.WebRootPath + "/images/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                companyDto.ImageFile.CopyTo(stream);
            }

            // Save the new logo in the database
            Company company = new Company()
            {
                CompanyName = companyDto.CompanyName,
                RegistrationType = companyDto.RegistrationType,
                KRAPin = companyDto.KRAPin,
                Email = companyDto.Email,
                Phone = companyDto.Phone,
                LogoPath = newFileName, // Save only filename
               // CreatedAt = DateTime.Now   // optional if you have it


            };
            context.Companies.Add(company);
            context.SaveChanges();




                return RedirectToAction("Index", "Companies");
        }

    }
}
