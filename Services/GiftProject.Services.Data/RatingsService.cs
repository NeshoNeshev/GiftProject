namespace GiftProject.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using GiftProject.Data.Common.Repositories;
    using GiftProject.Data.Models;
    using GiftProject.Services.Data.Common;
    using Microsoft.EntityFrameworkCore;

    public class RatingsService : IRatingsService
    {
        private readonly IDeletableEntityRepository<ProductVote> _productvoteRepository;

        public RatingsService(IDeletableEntityRepository<ProductVote> productvoteRepository)
        {
            _productvoteRepository = productvoteRepository;
        }

        public async Task VoteAsync(int productId, string userId, int rating)
        {
            var starRating = await this._productvoteRepository
                .All()
                .FirstOrDefaultAsync(x => x.ProductId == productId && x.ApplicationUserId == userId);

            if (starRating != null)
            {
                if (DateTime.UtcNow < starRating.NextVoteDate)
                {
                    throw new ArgumentException(ExceptionMessages.AlreadySentVote);
                }

                starRating.Rate += rating;
                starRating.NextVoteDate = DateTime.UtcNow.AddDays(1);
            }
            else
            {
                starRating = new ProductVote
                {
                    ProductId = productId,
                    ApplicationUserId = userId,
                    Rate = rating,
                    NextVoteDate = DateTime.UtcNow.AddDays(1),
                };

                await this._productvoteRepository.AddAsync(starRating);
            }

            await this._productvoteRepository.SaveChangesAsync();
        }

        public async Task<int> GetStarRatingsAsync(int productId)
        {
            var starRatings = await this._productvoteRepository
                .All()
                .Where(x => x.ProductId == productId)
                .SumAsync(x => x.Rate);

            return starRatings;
        }

        public async Task<DateTime> GetNextVoteDateAsync(int productId, string userId)
        {
            var starRating = await this._productvoteRepository
                .All()
                .FirstAsync(x => x.ProductId == productId && x.ApplicationUserId == userId);

            return starRating.NextVoteDate;
        }
    }
}
