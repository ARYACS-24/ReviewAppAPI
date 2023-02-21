using FullStack.Models;

namespace FullStack.Repository
{
    public interface IReview
    {
        void AddReviews(Models.Review reviewAdd);
        object GetReviewbyID(int empID);
        Models.Review DeleteReview(int reviewID);
    }
}
