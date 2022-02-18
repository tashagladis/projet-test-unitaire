using APILibrary.Core.Attributs;
using APILibrary.Core.Extensions;
using APILibrary.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace APILibrary.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllerBaseAPI<TModel, TContext> : ControllerBase where TModel : ModelBase where TContext : DbContext
    {
        public TContext _context;

        private const string LinkHeaderTemplate = "{0}; rel=\"{1}\"";
        public ControllerBaseAPI(TContext context)
        {
            this._context = context;
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpGet("search")]
        public virtual async Task<ActionResult<IEnumerable<dynamic>>> SearchAsync([FromQuery] string fields, [FromQuery] string asc, [FromQuery] string desc)
        {
            IQueryCollection requestQuery = Request.Query;

            var query = _context.Set<TModel>().AsQueryable();

            // TRIS
            if (!string.IsNullOrWhiteSpace(asc) || !string.IsNullOrWhiteSpace(desc))
            {
                query = query.OrderByDynamic(asc, desc);
            }

            // FILTRES
            if (requestQuery != null && requestQuery.Count() > 0)
            {
                query = query.WhereDynamic(requestQuery, true);
            }

            // RENDU PARTIEL
            if (!string.IsNullOrWhiteSpace(fields))
            {
                var tab = fields.Split(',');
                query = query.SelectModel(tab);
                var results = await query.ToListAsync();
                return Ok(results.Select((x) => IQueryableExtensions.SelectObject(x, tab)).ToList());
            }
            else
            {
                return Ok(ToJsonList(await query.ToListAsync()));
            }
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<dynamic>>> GetAllAsync([FromQuery] string fields="", [FromQuery] string asc = "", [FromQuery] string desc = "")
        {
            IQueryCollection requestQuery = null;
            try
            {
                requestQuery = Request.Query;
            }
            catch (Exception ex)
            {

            }

            var query = _context.Set<TModel>().AsQueryable();

            // TRIS
            if (!string.IsNullOrWhiteSpace(asc) || !string.IsNullOrWhiteSpace(desc))
            {
                query = query.OrderByDynamic(asc, desc);
            }

            // FILTRES
            if (requestQuery != null && requestQuery.Count() > 0)
            {
                query = query.WhereDynamic(requestQuery);
            }


            // RENDU PARTIEL
            if (!string.IsNullOrWhiteSpace(fields))
            {
                var tab = fields.Split(',');
                query = query.SelectModel(tab);
                var results = await query.ToListAsync();
                return Ok(results.Select((x) => IQueryableExtensions.SelectObject(x, tab)).ToList());
            }
            else
            {
                return Ok(ToJsonList(await query.ToListAsync()));
            }
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TModel>> GetById([FromRoute] int id, [FromQuery] string fields)
        {
            var query = _context.Set<TModel>().AsQueryable();
            //solution 2: optimisation de la requete SQL

            if (!string.IsNullOrWhiteSpace(fields))
            {
                var tab = new List<string>(fields.Split(','));
                if (!tab.Contains("id"))
                    tab.Add("id");
                var result = query.SelectModel(tab.ToArray()).SingleOrDefault(x => x.ID == id);
                if (result != null)
                {
                    var tabFields = fields.Split(',');
                    return Ok(IQueryableExtensions.SelectObject(result, tabFields));
                }
                else
                {
                    return NotFound(new { Message = $"ID {id} not found" });
                }
            }
            else
            {
                var result = query.SingleOrDefault(x => x.ID == id);
                if (result != null)
                {
                    return Ok(ToJson(result));
                }
                else
                {
                    return NotFound(new { Message = $"ID {id} not found" });
                }
            }
        }

        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public virtual async Task<ActionResult<TModel>> CreateItem([FromBody] TModel item)
        {
            if (ModelState.IsValid)
            {

                _context.Add(item);
                await _context.SaveChangesAsync();
                return Created("", ToJson(item));
            }
            else
            {
                //   ModelState:  {clé: nom du champ // valeur : ce qui ne va pas sur le champ}
                return BadRequest(ModelState);
            }
        }

        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPut("{id}")]
        public async Task<ActionResult<TModel>> UpdateItem([FromRoute] int id, [FromBody] TModel item)
        {
            if (id != item.ID)
            {
                return BadRequest();
            }

            TModel itemShouldExist = _context.Set<TModel>().AsNoTracking().FirstOrDefault(e => e.ID == id);
            if (itemShouldExist == null)
            {
                return NotFound();
            }
            else
            {
                itemShouldExist = item;
            }

            try
            {
                await _context.SaveChangesAsync();
                return Ok(ToJson(item));
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<TModel>> RemoveItem([FromRoute] int id)
        {
            TModel item = await _context.Set<TModel>().FindAsync(id);
            if (item == null)
                return NotFound();
            _context.Remove<TModel>(item);
            try
            {
                await _context.SaveChangesAsync();
                return Ok(ToJsonList(await _context.Set<TModel>().ToListAsync()));
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

        protected IEnumerable<dynamic> ToJsonList(IEnumerable<object> tab)
        {
            var tabNew = tab.Select((x) => ToJson(x));
            return tabNew;
        }

        protected dynamic ToJson(object item)
        {
            var expandoDict = new ExpandoObject() as IDictionary<string, object>;
            var collectionType = typeof(TModel);
            IDictionary<string, object> dico = item as IDictionary<string, object>;

            if (dico != null)
            {
                foreach (var prop in dico)
                {
                    var propInTModel = collectionType.GetProperty(prop.Key, BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance);

                    var isPresentAttribute = propInTModel.CustomAttributes.
                        Any(x => x.AttributeType == typeof(NotJsonAttribute));
                    if (!isPresentAttribute)
                        expandoDict.Add(prop.Key, prop.Value);
                }
            }
            else
            {
                foreach (var prop in collectionType.GetProperties())
                {
                    var isPresentAttribute = prop.CustomAttributes.
                       Any(x => x.AttributeType == typeof(NotJsonAttribute));
                    if (!isPresentAttribute)
                        expandoDict.Add(prop.Name, prop.GetValue(item));
                }
            }
            return expandoDict;
        }
    }

}
