using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebapiIdentity.Models;

namespace WebapiIdentity.Controllers
{
    [AllowAnonymous]
    public class RequestLogsController : ApiController
    {
        private ShopDBContext db = new ShopDBContext();
        // GET: api/RequestLogs
        public IHttpActionResult GetRequestLogs()
        {
            try
            {
                List<RequestLog> logList = new List<RequestLog>();
                logList = db.RequestLogs.ToList();
                return Json(logList, CommonTools.serializerSettings);
            }
            catch (Exception ex)
            {
                return BadRequest("Search No Data");
            }
        }

        // GET: api/RequestLogs/5
        [ResponseType(typeof(RequestLog))]
        public IHttpActionResult GetRequestLog(int id)
        {
            RequestLog requestLog = db.RequestLogs.Find(id);
            if (requestLog == null)
            {
                return NotFound();
            }

            return Ok(requestLog);
        }

        // PUT: api/RequestLogs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRequestLog(int id, RequestLog requestLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != requestLog.Rid)
            {
                return BadRequest();
            }

            db.Entry(requestLog).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestLogExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/RequestLogs
        [ResponseType(typeof(RequestLog))]
        public IHttpActionResult PostRequestLog(RequestLog requestLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RequestLogs.Add(requestLog);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = requestLog.Rid }, requestLog);
        }

        // DELETE: api/RequestLogs/5
        [ResponseType(typeof(RequestLog))]
        public IHttpActionResult DeleteRequestLog(int id)
        {
            RequestLog requestLog = db.RequestLogs.Find(id);
            if (requestLog == null)
            {
                return NotFound();
            }

            db.RequestLogs.Remove(requestLog);
            db.SaveChanges();

            return Ok(requestLog);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RequestLogExists(int id)
        {
            return db.RequestLogs.Count(e => e.Rid == id) > 0;
        }
    }
}