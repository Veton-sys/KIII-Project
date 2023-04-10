using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MimeKit;
using Project.Domain.DomainModels;
using Project.Domain.Models;
using Project.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Project.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ApplicationDbContext _context;

		public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
		{
			_logger = logger;
			this._context = context;
		}

		public IActionResult Index()
		{
			List<Product> products = _context.Products.Where(z => true).ToList();
			return View(products);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
		
		public IActionResult Contact()
		{
			var adminUsers = _context.Users.Where(z => z.Role == "Administrator").ToList();
			return View(adminUsers);
		}

		[HttpPost]
		public IActionResult Contact(string userEmailPassword, string adminEmail, string bodyMessage)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var user = _context.Users.Where(z => z.Id == userId).FirstOrDefault();

			var message = new MimeMessage();
			message.From.Add(new MailboxAddress("From", user.Email));
			message.To.Add(new MailboxAddress("To", adminEmail));
			message.Subject = "Question for product/device";
			message.Body = new TextPart("plain")
			{
				Text = bodyMessage
			};
			 using (var client = new SmtpClient())
			{
				client.Connect("smtp.gmail.com", 587, false);
				client.Authenticate(user.Email, userEmailPassword);
				client.Send(message);
				client.Disconnect(true);
			}
			return RedirectToAction(nameof(Index));
		}
	}
}
