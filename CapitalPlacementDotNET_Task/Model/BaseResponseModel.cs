namespace CapitalPlacementDotNET_Task.Model
{
    public class BaseResponseModel<T>
    {
        public string Message { get; set; } = string.Empty;
        public bool Status { get; set; }
        public T? Data { get; set; }
    }

    public class BaseResponse
    {
        public string Message { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
