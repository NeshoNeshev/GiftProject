namespace GiftProject.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using GiftProject.Data.Common.Repositories;
    using GiftProject.Data.Models;
    using GiftProject.Data.Models.Enumeration;

    public class VoteService : IVoteService
    {
        private readonly IDeletableEntityRepository<ProductVote> voteRepository;

        public VoteService(IDeletableEntityRepository<ProductVote> voteRepository)
        {
            this.voteRepository = voteRepository;
        }

        public async Task VoteAsync(int productId, string userId, bool isUpVote)
        {
            var vote = this.voteRepository.All()
                .FirstOrDefault(x => x.ProductId == productId && x.ApplicationUserId == userId);
            if (vote != null)
            {
                vote.Vote = isUpVote ? VoteType.UpVote : VoteType.Neutral;
            }
            else
            {
                vote = new ProductVote
                {
                    ProductId = productId,
                    ApplicationUserId = userId,
                    Vote = isUpVote ? VoteType.UpVote : VoteType.Neutral,
                };
                await this.voteRepository.AddAsync(vote);
            }

            await this.voteRepository.SaveChangesAsync();
        }

        public int GetVotes(int productId)
        {
           var votes= this.voteRepository.All().Where(x => x.ProductId == productId).Sum(x => (int)x.Vote);

           return votes;
        }
    }
}
