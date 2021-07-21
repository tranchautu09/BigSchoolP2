using BigSchool.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BigSchool.Controllers
{
    public class AttendancesController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Attend(Course attendanceDto)
        {
            var userID = User.Identity.GetUserId();
            BigSchoolContext db = new BigSchoolContext();
            if (db.Attendances.Any(prop => prop.Attendee == userID && prop.CourseId == attendanceDto.Id))
            {
                db.Attendances.Remove(db.Attendances.SingleOrDefault(p =>
                p.Attendee == userID && p.CourseId == attendanceDto.Id));
                db.SaveChanges();
                return Ok("cancel");
            }
            var attendance = new Attendance() { CourseId = attendanceDto.Id, Attendee = User.Identity.GetUserId() };
            db.Attendances.Add(attendance);
            db.SaveChanges();
            return Ok();
        }
    }
}
