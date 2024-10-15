using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using server.Dtos.Comment;
using server.Extensions;
using server.Helpers;
using server.Interfaces;
using server.Mappers;
using server.Models;

namespace server.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFMPService _fmbService;
        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo, UserManager<AppUser> userManager, IFMPService fmbService)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
            _userManager = userManager;
            _fmbService = fmbService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult>  GetAll([FromQuery]CommentQueryObject queryObject)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var comments = await _commentRepo.GetAllAsync(queryObject);
            var commentDto = comments.Select(x => x.ToCommentDto());
            return Ok(commentDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepo.GetByIdAsync(id);
            if(comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{symbol:alpha}")]
        public async Task<IActionResult> Create([FromRoute]string symbol, CreateCommentDto commentDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _stockRepo.GetBySymbolAsync(symbol);

            if(stock == null)
            {
                stock = await _fmbService.FindStockBySymbolAsync(symbol);
                if(stock == null)
                {
                    return BadRequest("Stock does not exists");
                }
                else
                {
                    await _stockRepo.CreateAsync(stock);
                }
            }

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
  
            var commentModel = commentDto.ToCommentFromCreate(stock.Id);
            commentModel.AppUserId = appUser.Id;
            
            await _commentRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new {id= commentModel.Id }, commentModel.ToCommentDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute]int id,[FromBody] UpdateCommentRequestDto updateDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepo.UpdateAsync(id, updateDto.ToCommentFromUpdate());
            if(comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var commentModel = await _commentRepo.DeleteAsync(id);
            if(commentModel == null)
            {
                return NotFound("comment does not exist");
            }
            return Ok(commentModel);
        }
    }
}