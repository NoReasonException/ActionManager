﻿using ApiProject.DBClasses;
using System.Web.Http;
using ApiProject.DBClasses.DB_EFContext;
using System.Web.Http.Results;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
namespace ApiProject.Controllers
{
    
    public class ActivityController : ApiController
    { 
        /// <summary>
        /// Gets the Customer ID , Return every Activity Ascosiated with him
        /// </summary>
        /// <param name="id"> The <see cref="Customer.CustomerID"/> </param>
        ///<returns>
        ///     <list type="bullet">
        ///         <item>
        ///             <term>200(OK)</term>
        ///             <description>Everything fine!</description>
        ///         </item>
        ///         <item>
        ///             <term>404(Not Found)</term>
        ///             <description>id not found</description>
        ///         </item>
        ///          <item>
        ///             <term>204(No Context)</term>
        ///             <description>id found , but has no ascosiated Activities with it!</description>
        ///         </item>
        ///         <item>
        ///             <term>400(Bad GateWay)</term>
        ///             <description>Fatal , Unkown error happened</description>
        ///         </item>
        ///     </list>
        /// 
        /// 
        /// </returns>
        [Route("api/Activity/{id}")]
        [HttpGet]
        public JsonResult<Activity[]> Get(int id)
        {
            Debug.WriteLine("api/Activity/id Get Controller:    Incoming Request GET_ALL_ACTIVITIES with ID{0} from {1}", id, Request.Headers.From);

            Activity[] ActivitiesFromDB;
            try
            {
                ActivitiesFromDB = DatabaseContextSiglenton.Context.ActivityContainer.Where<Activity>(o => o.Customer.CustomerID == id).ToArray();
            }catch(Exception e)
            {
                Debug.WriteLine("api/Activity/id Get Controller ,[From :{0}] Encountered a fatal error {1} ",Request.Headers.From,e.Message);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadGateway)
                {
                    ReasonPhrase = "FATAL ERROR"
                });
            }
            if (ActivitiesFromDB.Length == 0)
            {
                if (DatabaseContextSiglenton.Context.CustomerContainer.Find(id) == null)
                {
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
                }
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NoContent));
            }
            Debug.WriteLine("api/Activity/id Get Controller:     Handles Appropiate Request , Returns 200! [From {0}]",Request.Headers.From);
            return Json(ActivitiesFromDB, DatabaseContextSiglenton.JsonSettings);
        }
        /// <summary>Posts a new Activity for the <see cref="Customer"/> Customer with Id As Specified in <paramref name="id"/></summary>
        /// <param name="id">The <see cref="Customer"/> ID </param>
        /// <param name="ActivityFromForm"> The <see cref="Activity"/> Object To Add</param>
        /// <returns>
        ///      <list type="bullet">
        ///         <item>
        ///             <term>200(OK)</term>
        ///             <description>Everything fine!</description>
        ///         </item>
        ///         <item>
        ///             <term>404(Not Found)</term>
        ///             <description>id not found</description>
        ///         </item>
        ///         <item>
        ///             <term>400(Bad GateWay)</term>
        ///             <description>Fatal , Unkown error happened or StartDate>=EndDate(after bugfix v0.1)</description>
        ///         </item>
        ///     </list>
        /// </returns>
        /// ///todo -> check if startDate>endDate
        [Route("api/Activity/{id}")]
        [HttpPost]
        public IHttpActionResult PostNewActivity(int id,Activity ActivityFromForm)
        {
            Debug.WriteLine("api/Activity/id Post Controller:Incoming Request POST_NEW_ACTIVITY with Activity", Utills.Utills.IFNULL(ActivityFromForm));

            Customer CustomerFromDB = DatabaseContextSiglenton.Context.CustomerContainer.Find(id);
            if (CustomerFromDB == null) return NotFound();
            if (!Utills.Utills.isDateTimesOkay(ActivityFromForm.StartDate, ActivityFromForm.EndDate)) return BadRequest();
            //if (!ModelState.IsValid || ActivityFromForm == null) return BadRequest(); //<---- TODO : Fix this 
            ActivityFromForm.Customer = CustomerFromDB;
            DatabaseContextSiglenton.Context.ActivityContainer.Add(ActivityFromForm);
            try
            {
                DatabaseContextSiglenton.Context.SaveChanges();
            }
            catch (Exception e)//Todo:Catch Specific Exceptions
            {
                Debug.WriteLine("api/Activity/id Post Controller:   Faces Fatal Error{0} , Returns 400(Bad)! [From {1}]",e.Message, Request.Headers.From);

                return BadRequest();
            }
            Debug.WriteLine("api/Activity/id Post Controller:   Handles Appropiate Request , Returns 200! [From {0}]", Request.Headers.From);
            return Ok();
        }
        /// <summary>Deletes a Activity by its ID
        ///         <h3>Important Note! This Method Accept ACTIVITY_ID in contract with all other methods , witch Accepts CustomerID</h3>
        /// </summary>
        /// 
        /// <param name="ActivityId">The ID of the Activity</param>
        /// <returns>
        ///      <list type="bullet">
        ///         <item>
        ///             <term>200(OK)</term>
        ///             <description>Everything fine!</description>
        ///         </item>
        ///         <item>
        ///             <term>404(Not Found)</term>
        ///             <description>id not found</description>
        ///         </item>
        ///         <item>
        ///             <term>400(Bad GateWay)</term>
        ///             <description>Fatal , Unkown error happened</description>
        ///         </item>
        ///     </list>
        /// </returns>
        [Route("api/Activity/{ActivityId}")]
        [HttpDelete]
        public IHttpActionResult DeleteActivityByID(int ActivityId)
        {
            Activity ActivityFromDB_ToAttach = DatabaseContextSiglenton.Context.ActivityContainer.Find(ActivityId);
            if (ActivityFromDB_ToAttach == null) return NotFound();
            Activity Act = DatabaseContextSiglenton.Context.ActivityContainer.Attach(ActivityFromDB_ToAttach);//TODO : Check if nessesary... i think not.. but i am tired ...
            DatabaseContextSiglenton.Context.ActivityContainer.Remove(Act);
            try
            {
                DatabaseContextSiglenton.Context.SaveChanges();
            }
            catch (Exception e)//Todo:Catch Specific Exceptions
            {
                Debug.WriteLine("api/Activity/id Delete Controller:   Faces Fatal Error{0} , Returns 400(Bad)! [From {1}]", e.Message, Request.Headers.From);

                return BadRequest();
            }
            return Ok();
        }

        /// <summary>
        /// Changes some crucial Activity Properties!
        /// Note -> Change Events Customer not supported, in fact,dont send the Customer field at all , else it return BadRequest!
        /// Note->  Change ID Of course not suppoted , it throws BadRequest if you attemt it!
        /// </summary>
        /// <param name="ActivityId"> the <see cref="Activity.ActivityID"/> Property of the Event to change</param>
        /// <param name="ActivityFromForm"> the <see cref="Activity"/> Object to retrieve new information!</param>
        /// <returns>
        ///     <list type="bullet">
        ///         <item>
        ///             <term>200(OK)</term>
        ///             <description>Everything fine!</description>
        ///         </item>
        ///         <item>
        ///             <term>404(Not Found)</term>
        ///             <description>ActivityId not found</description>
        ///         </item>
        ///         <item>
        ///             <term>400(Bad GateWay)</term>
        ///             <description>
        ///                 <list type="bullet">
        ///                     <item>
        ///                      <term>Fatal Error</term>
        ///                     </item>
        ///                     <item>
        ///                      <term>Attemt to change ID</term>
        ///                     </item>
        ///                     <item>
        ///                      <term>Attemt to send Customer field</term>
        ///                     </item>
        ///                     <item>
        ///                      <term>No Send Form Data at all...</term>
        ///                     </item>
        ///                 </list>
        ///             </description>
        ///         </item>
        ///     </list>
        /// 
        /// 
        /// </returns>
        [Route("api/Activity/{ActivityId}")]
        [HttpPut]
        public IHttpActionResult PutActivity(int ActivityId,Activity ActivityFromForm)
        {
            Debug.WriteLine("api/Activity/id Put Controller:Incoming Request POST_NEW_ACTIVITY with Activity", Utills.Utills.IFNULL(ActivityFromForm));
            if (ActivityFromForm == null) return BadRequest();
            Activity ActivityFromDB = DatabaseContextSiglenton.Context.ActivityContainer.Find(ActivityId);
            DatabaseContextSiglenton.Context.Entry<Activity>(ActivityFromDB).State=System.Data.Entity.EntityState.Modified;

            if (ActivityFromDB == null) return NotFound();
            if (ActivityFromForm.ActivityID != ActivityId) return BadRequest();// ChangeID , Not Allowed yet(?)

            if (!System.String.IsNullOrEmpty(ActivityFromForm.Description))
            {
                ActivityFromDB.Description = ActivityFromForm.Description;
            }
            else
            {
                return BadRequest();
            }
            if (ActivityFromForm.Customer != null)
            {
                return BadRequest();
            }
            if (Utills.Utills.isDateTimesOkay(ActivityFromForm.StartDate, ActivityFromForm.EndDate))
            {
                ActivityFromDB.StartDate = ActivityFromForm.StartDate;
                ActivityFromDB.EndDate = ActivityFromForm.EndDate;
            }
            else
            {
                return BadRequest();
            }
            DatabaseContextSiglenton.Context.SaveChanges();
            Debug.WriteLine("api/Activity/id Put Controller:   Handles Appropiate Request , Returns 200! [From {0}]", Request.Headers.From);

            return Ok();
        }

    }
    
}
