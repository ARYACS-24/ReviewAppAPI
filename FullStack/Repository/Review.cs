using FullStack.Data;
using FullStack.Models;
using System;

namespace FullStack.Repository
{
    public class Review : IReview
    {
        private readonly FullStackDbContext fullStackDbContext;

        public Review(FullStackDbContext fullStackDbContext)
        {
            this.fullStackDbContext = fullStackDbContext;
        }
        public void AddReviews(Models.Review reviewAdd)
        {
            fullStackDbContext.AddReview.Add(reviewAdd);
            fullStackDbContext.SaveChanges();
        }

        public Models.Review DeleteReview(int reviewID)
        {
            var review = fullStackDbContext.AddReview.FirstOrDefault(x => x.ReviewID == reviewID);
            fullStackDbContext.AddReview.Remove(review);
            fullStackDbContext.SaveChanges();
            return review;
        }

        public object GetReviewbyID(int empID)
        {
            var empReview = (
                             from e in fullStackDbContext.EmpTabs
                             join r in fullStackDbContext.AddReview
                             on e.EmpID equals r.EmpID
                             where (e.EmpID == empID)
                             select new
                             {
                                 EmpID = e.EmpID,
                                 ReviewID = r.ReviewID,
                                 RDate = r.RDate,
                                 Feedback = r.Feedback
                             }).ToList(); 

            return empReview;
        }
    }
}
