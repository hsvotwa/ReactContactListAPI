namespace ReactContactListApi.Utilities
{
    public class GlobalResponse<T> where T : struct
    {
        public T Feedback { get; set; }
        public string Description { get; set; }
    }
}