using NHT.ASM.Bll.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using NHT.ASM.Dal;
using System.Web.Hosting;
using NHT.ASM.Helpers.ExtensionMethods;
using NHT.ASM.Infrastructure;

namespace NHT.ASM.Api.Controllers
{
    public class CustomBaseController<TSource, TDto>:ApiController where TSource : DomainEntity where TDto : BaseDto
    {
        /// <summary>
        /// The logic containing all business rules used in the controllers
        /// </summary>
        protected readonly ILogic<TSource, TDto> Logic;

        /// <summary>
        /// The Database context used in the controllers
        /// </summary>
        protected readonly AsmContext Context;

        /// <summary>
        /// Instantiates the base controller
        /// </summary>
        /// <param name="logic"></param>
        /// <param name="context"></param>
        /// <param name="hostingEnvironment"></param>
        protected CustomBaseController(ILogic<TSource, TDto> logic, AsmContext context, HostingEnvironment hostingEnvironment)
        {
            Logic = logic;
            Context = context;
        }

        /// <summary>
        /// Creates Select list for UI of entity based on the columns specified for Text and Value fields.
        /// It will retrieve data only for columns which are specified
        /// </summary>
        /// <param name="columns">list of columns for creating Select List Item. For example, new SelectListItem { Value = x.Id.ToString(), Text = x.Name } </param>
        /// <param name="defaultText">Default text for Select List</param>
        /// <param name="defaultValue">Default value for Select List</param>
        /// <returns></returns>
        protected IEnumerable<SelectListItem> CreateSelectListItems(Expression<Func<TSource, SelectListItem>> columns, string defaultText = "-Select-", string defaultValue = "0")
        {
            IEnumerable<SelectListItem> items = Logic.GetSelectList(columns);
            if (!items.HasAny()) return null;
            var selectList = new List<SelectListItem> { new SelectListItem { Text = defaultText, Value = defaultValue } };
            selectList.AddRange(Logic.GetSelectList(columns));

            return selectList;
        }


        /// <summary>
        /// Add a new entity
        /// </summary>
        /// <param name="dto">Data Transfer Object for the entity</param>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]

        //[ResponseType(HttpStatusCode.Created), ProducesResponseType((int)HttpStatusCode.InternalServerError), ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IHttpActionResult Post([FromBody] TDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Logic.Post(dto);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }


        }
        /// <summary>
        /// Update an existing entity 
        /// </summary>
        /// <param name="updatedDto">Data Transfer Object for the entity</param>
        /// <returns></returns>
      //  [System.Web.Http.HttpPut("", Name = "Put[controller]")]
        
        public IHttpActionResult Put([FromBody] TDto updatedDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Logic.Put(updatedDto.Id, updatedDto);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }


        }

        /// <summary>
        /// Removes an entity from the database
        /// </summary>
        /// <param name="id">Unique Identifier</param>
        /// <returns></returns>
      //  [System.Web.Http.HttpDelete("{id}", Name = "Delete[controller]")]
        
        public IHttpActionResult Delete(int id)
        {
            try
            {
                TSource entity = Logic.GetEntityById(id);
                if (entity == null)
                    return NotFound();
                Logic.Delete(entity);

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

        }
    }
}