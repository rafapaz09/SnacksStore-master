using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SnacksStore.Data;
using SnacksStore.Data.Interfaces;
using SnacksStore.Data.Model;

namespace SnacksStore.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolsController : ControllerBase
    {
        private readonly IRolRepository _rolRepository;

        public RolsController(IRolRepository rolRepository)
        {
            _rolRepository = rolRepository;
        }

        // GET: api/Rols
        [HttpGet]
        public IEnumerable<Rol> GetRoles()
        {
            return _rolRepository.GetAll();
        }

        // GET: api/Rols/5
        [HttpGet("{id}")]
        public ActionResult<Rol> GetRol(int id)
        {
            var rol = _rolRepository.GetById(id);

            if (rol == null)
            {
                return NotFound();
            }

            return rol;
        }

        // PUT: api/Rols/5
        [HttpPut("{id}")]
        public IActionResult PutRol(int id, Rol rol)
        {
            if (id != rol.Id)
            {
                return BadRequest();
            }

            try
            {
                var oldRol = _rolRepository.GetById(id);
                oldRol.Name = rol.Name ?? oldRol.Name;
                oldRol.Active = rol.Active ?? oldRol.Active;
                oldRol.CreatedAt = rol.CreatedAt ?? oldRol.CreatedAt;

                _rolRepository.Update(oldRol);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok();
        }

        // POST: api/Rols
        [HttpPost]
        public ActionResult<Rol> PostRol(Rol rol)
        {
            _rolRepository.Create(rol);

            return CreatedAtAction("GetRol", new { id = rol.Id }, rol);
        }

        // DELETE: api/Rols/5
        [HttpDelete("{id}")]
        public ActionResult<Rol> DeleteRol(int id)
        {
            var rol = _rolRepository.GetById(id);
            if (rol == null)
            {
                return NotFound();
            }

            _rolRepository.Delete(rol);
            return rol;
        }

       
    }
}
