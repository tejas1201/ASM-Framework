using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Mvc;
using NHT.ASM.Bll.Interfaces;
using NHT.ASM.Dal;
using NHT.ASM.Helpers.ExtensionMethods;
using NHT.ASM.Models.DataTransferObjects.UserModel;
using NHT.ASM.Models.Entities.UserModel;

namespace NHT.ASM.Api.Controllers
{
    public class UserController : CustomBaseController<User,UserDto >
    {
        private readonly IUserLogic _userLogic;

        protected UserController(IUserLogic userLogic, AsmContext context, HostingEnvironment hostingEnvironment) : base(userLogic, context, hostingEnvironment)
        {
            _userLogic = userLogic;
        }

        [System.Web.Http.HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                var userDtos = _userLogic.GetAll();
                if (userDtos.HasAny())
                {
                    return Ok(userDtos);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
