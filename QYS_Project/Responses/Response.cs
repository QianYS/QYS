namespace QYS_Project.Responses
{
    public class Response<T> : ReqResp
    {
        public T Data { get; set; }
    }
}
