namespace QYS_Project.Responses
{
    public abstract class ReqResp
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public static Response<T> Create<T>(T data, int code = 0, string msg = "")
        {
            var response = new Response<T> {Code = code, Message = msg, Data = data};
            return response;
        }

        public static ResponseSimple Create(int code = 0, string msg = "")
        {
            var responseSimple = new ResponseSimple {Code = code, Message = msg};
            return responseSimple;
        }
    }
}
