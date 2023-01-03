namespace GiftProject.Services.Data
{
    using System.Threading.Tasks;

    public interface IVoteService
    {
        Task VoteAsync(int productId, bool isUpVote);

        int GetVotes(int productId);
    }
}
