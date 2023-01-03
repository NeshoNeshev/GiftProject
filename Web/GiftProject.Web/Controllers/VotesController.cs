namespace GiftProject.Web.Controllers
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

        public VotesController(IVoteService voteService)
        {
            this.voteService = voteService;
        }

        [HttpPost]
        public async Task<ActionResult<VoteResponseModel>> Vote(VoteInputModel input)
        {
            await this.voteService.VoteAsync(input.ProductId, input.IsUpVote);
            var votes = this.voteService.GetVotes(input.ProductId);
            return new VoteResponseModel { VotesCount = votes };
        }
    }
}
