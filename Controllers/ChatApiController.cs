using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using chatapi.Model;
using System.Data;
using System;
using chatapi.Data;
using chatapi.Model;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace chatapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChatApiController : ControllerBase
    {
        private readonly ApplicationDBContext _dcontext;

        public ChatApiController(ApplicationDBContext dcontext)
        {
            _dcontext = dcontext;
        }

        [HttpGet]
        [Route("/getmsg")]
        public ActionResult<IEnumerable<models>> getData()
        {
            var SupportList = _dcontext.Users.FromSqlRaw("exec Udp_Registration @Action, @UserID,@Username, @Email, @PasswordHash, @ProfilePicUrl, @StatusMessage, @CreatedAt",
              new SqlParameter("@Action", "get"),
              new SqlParameter("@UserID", ""),
              new SqlParameter("@Username",""),
              new SqlParameter("@Email", ""),
              new SqlParameter("@PasswordHash", ""),
              new SqlParameter("@ProfilePicUrl", ""),
              new SqlParameter("@StatusMessage", ""),
            new SqlParameter("@CreatedAt", "")

            ).ToList();
            // var result = new Dictionary<string, object>
            //     {
            //     { "data", SupportList},
            // { "message", msg.Value.ToString() }
            //       };
            return Ok(SupportList);
        }
        [HttpPost]
        [Route("/postdata")]

        public async Task<ActionResult<List<models>>> postData(models models)
        {
            var data = 0;
            try
            {
                var SqlQuery = "exec Udp_Registration @Action, @UserID,@Username, @Email, @PasswordHash, @ProfilePicUrl, @StatusMessage, @CreatedAt";
             
                SqlParameter[] parameters =
                {
                        new SqlParameter("@Action", "insert"),
                        new SqlParameter("@UserID", models.UserID),
                         new SqlParameter("@Username", models.Username),
                        new SqlParameter("@Email", models.Email),
                        new SqlParameter("@PasswordHash", models.PasswordHash),
                        new SqlParameter("@ProfilePicUrl", models.ProfilePicUrl),
                        new SqlParameter("@StatusMessage", models.StatusMessage),
                        new SqlParameter("@CreatedAt", models.CreatedAt)

                };
                data = await _dcontext.Database.ExecuteSqlRawAsync(SqlQuery, parameters);
                //var result = new Dictionary<string, object>
                //  {
                //  { "data", data },
                //  { "message", msg.Value.ToString() }
                //   };

                //var messageparts = msg.Value.ToString().Split(' ');
                //if (messageparts.Length > 1)
                //{
                //    supportmodel.Id = messageparts[1].Trim();
                //}


                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("/getmsg1")]
        public ActionResult<IEnumerable<loggedin>> getloggedin()
        {
            var SupportList = _dcontext.loggedin.FromSqlRaw("exec Udp_Loggedin @Action, @id, @title,@description, @images, @UserID, @TargAud ",
              new SqlParameter("@Action", "get1"),
              new SqlParameter("@id", ""),
              new SqlParameter("@title", ""),
              new SqlParameter("@description", ""),
              new SqlParameter("@images", ""),
              new SqlParameter("@UserID", ""), new SqlParameter("@TargAud","")).ToList();

            return Ok(SupportList);
        }

        [HttpPost]
        [Route("/postlogged")]
        public async Task<ActionResult<List<loggedin>>> postloggedin(loggedin loggedin)
        {
            var data = 0;
            try
            {
                var SqlQuery = "exec Udp_Loggedin @Action, @id, @title,@description, @images, @UserID, @TargAud ";
                // var msg = new SqlParameter("@msg", SqlDbType.VarChar, 10) { Direction = ParameterDirection.Output };

                SqlParameter[] parameters =
                {
                        new SqlParameter("@Action", "insert1"),
                        new SqlParameter("@id", loggedin.id),
                        new SqlParameter("@title", loggedin.title),
                        new SqlParameter("@description", loggedin.description),
                        new SqlParameter("@images", loggedin.images),
                        new SqlParameter("@UserID", loggedin.UserID),  new SqlParameter("@TargAud", loggedin.TargAud)

                };
                data = await _dcontext.Database.ExecuteSqlRawAsync(SqlQuery, parameters);

                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



        [HttpPost]
        [Route("/loggin")]
        public async Task<ActionResult<List<Login>>> postlogin(Login login)
        {
            var data = 0;
            try
            {
                var SqlQuery = "exec Udp_login @Action, @Email, @PasswordHash, @msg output, @userid output, @emmail output, @Username output, @imagge output ";
                var msg = new SqlParameter("@msg", SqlDbType.VarChar, 10) { Direction = ParameterDirection.Output };
                var userid = new SqlParameter("@userid", SqlDbType.VarChar, 40) { Direction = ParameterDirection.Output };
                var username = new SqlParameter("@Username", SqlDbType.VarChar, 50) { Direction = ParameterDirection.Output };
                var emmail = new SqlParameter("@emmail", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
                var imagge = new SqlParameter("@imagge", SqlDbType.VarChar, int.MaxValue) { Direction = ParameterDirection.Output };
                SqlParameter[] parameters =
                {
                        new SqlParameter("@Action", "login"),
                        new SqlParameter("@Email", login.Email),
                        new SqlParameter("@PasswordHash", login.PasswordHash), msg, userid, emmail, username, imagge

                };
                data = await _dcontext.Database.ExecuteSqlRawAsync(SqlQuery, parameters);

                var result = new Dictionary<string, object>
                {
                   { "data", data },
                   { "message", msg.Value.ToString() },
                   {"userid", userid.Value.ToString() },
                   {"email", emmail.Value.ToString() },
                   {"username", username.Value.ToString() },
                    {"image", imagge.Value.ToString() }
                   
                };

                //var messageparts = msg.Value.ToString().Split(' ');
                //if (messageparts.Length > 1)
                //{
                //    supportmodel.Id = messageparts[1].Trim();
                //}


                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }




        [HttpGet]
        [Route("/getprice")]
        public ActionResult<IEnumerable<gPrice>> getPrice()
        {
            var SupportList = _dcontext.Plans.FromSqlRaw("exec Udp_mypack @Action, @PlanName, @Price, @Details, @color, @packno",
              new SqlParameter("@Action", "getprice"),
              new SqlParameter("@PlanName", ""),
              new SqlParameter("@Price", ""),
              new SqlParameter("@Details", ""),
              new SqlParameter("@color", ""),
              new SqlParameter("@packno", "")

            ).ToList();
            // var result = new Dictionary<string, object>
            //     {
            //     { "data", SupportList},
            // { "message", msg.Value.ToString() }
            //       };
            return Ok(SupportList);
        }

        [HttpPost]
        [Route("/payment")]
        public async Task<ActionResult<List<paymentt>>> postPayment(paymentt payment)
        {
            var data = 0;
            try
            {
                var SqlQuery = "exec Udp_Payment @Action, @UserID, @Name, @CardNumber, @CVV, @ExpiryDate,@Packno, @msg output ";
                var msg = new SqlParameter("@msg", SqlDbType.VarChar, 10) { Direction = ParameterDirection.Output };
                 SqlParameter[] parameters =
                {
                        new SqlParameter("@Action", "INSERTPAY"),
                        new SqlParameter("@UserID", payment.UserID),
                        new SqlParameter("@Name",payment.Name),
                        new SqlParameter("@CardNumber", payment.CardNumber),
                        new SqlParameter("@CVV", payment.CVV),
                        new SqlParameter("@ExpiryDate", payment.ExpiryDate),
                        new SqlParameter("@packno", payment.Packno) ,       msg, 

                };
                data = await _dcontext.Database.ExecuteSqlRawAsync(SqlQuery, parameters);

                var result = new Dictionary<string, object>
                {
                   { "data", data },
                   { "message", msg.Value.ToString() }

                };



                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
    
}

