//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using DocumentFormat.OpenXml.Spreadsheet;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.SignalR;
//using Microsoft.EntityFrameworkCore;
//using ASPNET_API.Domain.Entities;
//using ASPNET_API.Notification;

//namespace ASPNET_API.Controller
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class NotificationsController : ControllerBase
//    {
//        private readonly DonationWebApp_v2Context _context;
//        private readonly IHubContext<NotiHub> _myHub;

//        public NotificationsController(DonationWebApp_v2Context context, IHubContext<NotiHub> myHub)
//        {
//            _context = context;
//            _myHub = myHub;
//        }



//        // GET: api/Notifications
//        [HttpGet]
//        public async Task<IActionResult> GetNotifications()
//        {
//            if (_context.Notifications == null)
//            {
//                return NotFound();
//            }
//            var noties = await _context.Notifications.Include(n => n.User).ToListAsync();
//            var notiedtos = noties.Select(n => new
//            {
//                n.NotificationId,
//                n.NotificationTitle,
//                n.NotificationContent,
//                NotificationAt = n.NotificationAt.ToString("dd MMMM yyyy, 'at' hh:mm:ss tt"),
//                NotificationTo = new { n.User.UserId, Name = n.User.FirstName + " " + n.User.LastName, n.User.Email }

//            }).ToList();
//            return Ok(notiedtos);
//        }

//        // GET: api/Notifications/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Models.Entity.Notification>> GetNotification(int id)
//        {
//            if (_context.Notifications == null)
//            {
//                return NotFound();
//            }
//            var notification = await _context.Notifications.FindAsync(id);

//            if (notification == null)
//            {
//                return NotFound();
//            }

//            return notification;
//        }

//        // PUT: api/Notifications/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutNotification(int id, Models.Entity.Notification notification)
//        {
//            if (id != notification.NotificationId)
//            {
//                return BadRequest();
//            }

//            _context.Entry(notification).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!NotificationExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return Ok(notification);
//        }

//        // POST: api/Notifications
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<Models.Entity.Notification>> PostNotification(NotiInput notification)
//        {
//            if (_context.Notifications == null)
//            {
//                return Problem("Entity set 'DonationWebApp_v2Context.Notifications'  is null.");
//            }
//            var noti = new Models.Entity.Notification()
//            {
//                NotificationTitle = notification.NotificationTitle,
//                NotificationContent = notification.NotificationContent,
//                NotificationTo = notification.NotificationTo,
//                NotificationAt = DateTime.Now,
//                IsRead = false
//            };
//            _context.Notifications.Add(noti);
//            await _context.SaveChangesAsync();

//            UserNoti u = UserList.GetUser(noti.NotificationTo.ToString());
//            if (u != null)
//            {
//                await _myHub.Clients.Client(u.ConnectionId).SendAsync("ReceivedNoti", noti.NotificationTitle, noti.NotificationContent);
//            }

//            return CreatedAtAction("GetNotification", new { id = noti.NotificationId }, noti);
//        }

//        // DELETE: api/Notifications/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteNotification(int id)
//        {
//            if (_context.Notifications == null)
//            {
//                return NotFound();
//            }
//            var notification = await _context.Notifications.FindAsync(id);
//            if (notification == null)
//            {
//                return NotFound();
//            }

//            _context.Notifications.Remove(notification);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool NotificationExists(int id)
//        {
//            return (_context.Notifications?.Any(e => e.NotificationId == id)).GetValueOrDefault();
//        }
//    }

//    public class NotiInput
//    {

//        public string NotificationTitle { get; set; } = null!;
//        public string NotificationContent { get; set; } = null!;
//        public int NotificationTo { get; set; }

//    }
//}
