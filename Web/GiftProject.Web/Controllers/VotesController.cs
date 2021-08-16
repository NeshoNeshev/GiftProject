﻿namespace GiftProject.Web.Controllers
{
    using System.Threading.Tasks;

    using GiftProject.Data.Models;
    using GiftProject.Services.Data;
    using GiftProject.Web.ViewModels.Vote;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : ControllerBase
    {
        private readonly IVoteService voteService;
        private readonly UserManager<ApplicationUser> user;

        public VotesController(IVoteService voteService, UserManager<ApplicationUser> user)
        {
            this.voteService = voteService;
            this.user = user;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<VoteResponseModel>> Vote(VoteInputModel input)
        {
            var userId = this.user.GetUserId(this.User);
            await this.voteService.VoteAsync(input.ProductId, userId, input.IsUpVote);
            var votes = this.voteService.GetVotes(input.ProductId);
            return new VoteResponseModel { VotesCount = votes };
        }
    }
}
