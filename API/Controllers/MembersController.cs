using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using API.Repository.Interface;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    public class MembersController : Controller
    {
        private readonly IMembers _memberRepository;

        public MembersController(IMembers memberRepository)
        {
            _memberRepository = memberRepository;
        }
        [HttpGet]
        public ActionResult<List<BLL.Modals.Member>> List()
        {
            try
            {
                var MemberList = _memberRepository.GetMembers();
                return Ok(MemberList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<BLL.Modals.Member> Details(int id)
        {
            try
            {
                var Member = _memberRepository.GetMemberDetails(id);
                if (Member == null) return NotFound($"Member with Id = {id} not found");
                return Ok(Member);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Create")]
        public ActionResult<BLL.Modals.Member> Create(BLL.Modals.Member model)
        {
            try
            {
                _memberRepository.AddMember(model);
                return CreatedAtAction(nameof(Details), new { id = model.ID }, model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("Modify")]
        public async Task<IActionResult> Modify(BLL.Modals.Member model)
        {
            try
            {
                if(!_memberRepository.IsExists(model.ID)) return NotFound($"Member with Id = {model.ID} not found");
                _memberRepository.UpdateMember(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if(!_memberRepository.IsExists(id)) return NotFound($"Member with Id = {id} not found");
                _memberRepository.DeleteMember(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
