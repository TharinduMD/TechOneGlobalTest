namespace Weather.Core
{
    public class ResponseObject
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public object Result { get; set; }
    }
}
