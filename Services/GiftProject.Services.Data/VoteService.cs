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
        private readonly IDeletableEntityRepository<Product> productRepository;

        public VoteService(IDeletableEntityRepository<ProductVote> voteRepository, IDeletableEntityRepository<Product> productRepository)
        {
            this.voteRepository = voteRepository;
            this.productRepository = productRepository;
        }

        public async Task VoteAsync(int productId, bool isUpVote)
        {
            var productVote = this.productRepository.All().FirstOrDefault(x => x.Id == productId);
            if (productVote != null)
            {
                var vote = new ProductVote
                {
                    ProductId = productId,
                    Vote = isUpVote ? VoteType.UpVote : VoteType.Neutral,
                };
                await this.voteRepository.AddAsync(vote);
            }
            //var vote = this.voteRepository.All()
            //    .FirstOrDefault(x => x.ProductId == productId);
            //if (vote != null)
            //{
            //    vote.Vote = !isUpVote ? VoteType.Neutral : VoteType.UpVote;
            //}
            //else
            //{
            //    vote = new ProductVote
            //    {
            //        ProductId = productId,
            //        Vote = isUpVote ? VoteType.UpVote : VoteType.Neutral,
            //    };
            //    await this.voteRepository.AddAsync(vote);
            //}

            await this.voteRepository.SaveChangesAsync();
        }

        public int GetVotes(int productId)
        {
           var votes= this.voteRepository.All().Where(x => x.ProductId == productId).Sum(x => (int)x.Vote);

           return votes;
        }
    }
}
