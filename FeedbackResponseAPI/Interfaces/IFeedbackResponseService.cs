namespace FeedbackResponseAPI.Interfaces
{
    public interface IFeedbackResponseService
    {
        public Task<int?> CreateResponse(int customerFeedbackId, string comments);
    }
}
