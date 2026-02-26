using Biashara_POS.Data;
using Biashara_POS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var companies = context.Companies
                       .Include(c => c.Branches)
                       .ToList();
            return View(companies);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CompanyDto companyDto)
        {
            if (!ModelState.IsValid)
            {
                return View(companyDto);
            }

            // Generate unique filename
            string newFileName = Guid.NewGuid().ToString();
            newFileName += Path.GetExtension(companyDto.ImageFile!.FileName);

            // Ensure images folder exists
            string imagesFolder = Path.Combine(environment.WebRootPath, "images");

            if (!Directory.Exists(imagesFolder))
            {
                Directory.CreateDirectory(imagesFolder);
            }

            string imageFullPath = Path.Combine(imagesFolder, newFileName);

            // Save file
            using (var stream = new FileStream(imageFullPath, FileMode.Create))
            {
                companyDto.ImageFile.CopyTo(stream);
            }

            // Save to database
            Company company = new Company()
            {
                CompanyName = companyDto.CompanyName,
                RegistrationType = companyDto.RegistrationType,
                KRAPin = companyDto.KRAPin,
                Email = companyDto.Email,
                Phone = companyDto.Phone,
                LogoPath = newFileName
            };

            context.Companies.Add(company);
            context.SaveChanges();

            return RedirectToAction("Index", "Company");
        }
    }
}