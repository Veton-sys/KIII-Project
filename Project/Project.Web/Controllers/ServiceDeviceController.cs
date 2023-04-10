using GemBox.Document;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Project.Domain.DomainModels;
using Project.Repository;
using Project.Service.Interface;
using Stripe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Project.Web.Controllers
{
	public class ServiceDeviceController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IServiceDeviceService _serviceDeviceService;

		public ServiceDeviceController(ApplicationDbContext context, IServiceDeviceService serviceDeviceService)
		{
			this._context = context;
			this._serviceDeviceService = serviceDeviceService;
			ComponentInfo.SetLicense("FREE-LIMITED-KEY");
		}
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult AllServiceDevices()
		{
			var devices = _serviceDeviceService.GetAllDevices();
			return View(devices);
		}

		[HttpPost]
		public async Task<IActionResult> CreateNewServiceDevice([Bind("Name,Model,UserMessage")] ServiceDevice device)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (ModelState.IsValid && userId != null)
			{
				device.Id = Guid.NewGuid();
				_context.Add(device);
				await _context.SaveChangesAsync();
				var result = this._serviceDeviceService.AddDeviceToUser(device, userId);
				if (result)
				{
					return RedirectToAction(nameof(Index));
				}
				return RedirectToAction(nameof(Index));
			}
			return RedirectToAction(nameof(Index));
		}

		[Authorize(Roles = "Administrator")]
		public async Task<IActionResult> Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var device = await _context.ServiceDevices.FindAsync(id);
			if (device == null)
			{
				return NotFound();
			}
			return View(device);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Administrator")]
		public async Task<IActionResult> Edit(Guid id, [Bind("Name,Model,UserMessage,AdminMessage,Status,DateAdded,PriceForRepair,Id")] ServiceDevice device)
		{

			//Make the AdminMessage an pdf invoice that can be attached
			if (id != device.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(device);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!DeviceExists(device.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction("AllServiceDevices");
			}
			return View(device);
		}

		private bool DeviceExists(Guid id)
		{
			return _context.ServiceDevices.Any(e => e.Id == id);
		}

		public IActionResult UserDevices()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var devices = _serviceDeviceService.GetAllDevicesFromUser(userId);
			return View(devices);
		}

		public FileContentResult CreateInvoice(Guid deviceId)
		{

			var result = _context.ServiceDevices.Where(z => z.Id == deviceId).FirstOrDefault();
		
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var user = _context.Users.Where(z => z.Id == userId).FirstOrDefault();

			var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Solution.docx");
			var document = DocumentModel.Load(templatePath);
			
			document.Content.Replace("{{DeviceNumber}}", result.Id.ToString());
			document.Content.Replace("{{DeviceName}}", result.Name);
			document.Content.Replace("{{DeviceModel}}", result.Model);
			document.Content.Replace("{{UserName}}", user.Email);
			document.Content.Replace("{{AdminMessage}}", result.AdminMessage);
			document.Content.Replace("{{DateAdded}}", result.DateAdded.ToString());
			document.Content.Replace("{{PriceSolution}}", result.PriceForRepair.ToString() + "$");

			var stream = new MemoryStream();
			document.Save(stream, new PdfSaveOptions());


			return File(stream.ToArray(), new PdfSaveOptions().ContentType, "ExportInvoice.pdf");
		}

		public IActionResult PayDeviceToGetSolution(string stripeEmail, string stripeToken, Guid deviceId)
		{
			var customerService = new CustomerService();
			var chargeService = new ChargeService();

			var device = this._serviceDeviceService.GetDetailsForDevice(deviceId);

			var customer = customerService.Create(new CustomerCreateOptions
			{
				Email = stripeEmail,
				Source = stripeToken
			});

			var charge = chargeService.Create(new ChargeCreateOptions
			{
				Amount = (Convert.ToInt32(device.PriceForRepair) * 100),
				Description = "Project Application Payment",
				Currency = "usd",
				Customer = customer.Id
			});

			if (charge.Status == "succeeded")
			{
				device.PayedFor = true;
				_serviceDeviceService.UpdateExistingDevice(device);
				_context.SaveChanges();
				return RedirectToAction("UserDevices");
			}

			return RedirectToAction("UserDevices");
		}
	}
}
