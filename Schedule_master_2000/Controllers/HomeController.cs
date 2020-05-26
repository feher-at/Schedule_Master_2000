using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Schedule_master_2000.Models;
using Schedule_master_2000.Services;

namespace Schedule_master_2000.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IScheduleService _scheduleService;
        private readonly ISlotService _slotService;
        private readonly ITaskService _taskService;
        private readonly IColumnService _columnService;

        public HomeController(IUserService userService, IScheduleService scheduleService, ISlotService slotService, ITaskService taskService, IColumnService columnService)
        {
            _userService = userService;
            _scheduleService = scheduleService;
            _slotService = slotService;
            _taskService = taskService;
            _columnService = columnService;
        }

        public IActionResult Index()
        {
            return View();
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

        [Authorize(Roles = "admin")]
        public IActionResult Schedule()
        {
            var user = HttpContext.User;
            var claim = user.Claims.First(c => c.Type == ClaimTypes.Email);
            var email = claim.Value;
            User currentUser = _userService.GetOne(email);
            List <Schedule> scheduleList =  _scheduleService.GetOneUserAllSchedule(currentUser.ID);
            List<Slot> slotList = _slotService.GetOneUsersAllSlots(currentUser.ID);
            List<Tasks> taskList = _taskService.GetOneUserAllTasks(currentUser.ID);
            List<Column> columnList = _columnService.GetAllCollumnToOneUser(currentUser.ID);
            OneUserSchedules oneUserSchedules = new OneUserSchedules(currentUser, scheduleList, columnList, slotList, taskList);
            return Json(oneUserSchedules);
        }
        [Authorize]
        [HttpPost]
        public IActionResult NewSchedule([FromBody] Schedule schedule)
        {
            int scheduleId = _scheduleService.InsertSchedule(schedule.UserID, schedule.Title);
            schedule.ScheduleID = scheduleId;
            return Json(schedule);

        }

        [Authorize]
        [HttpPost]
        public IActionResult NewColumn([FromBody] List<Column> columnList)
        {
            foreach(Column column in columnList)
            {
                _columnService.InsertColumn(column.ScheduleID, column.UserID, column.Title);
            }
            return Content("success");

        }
    }
}

