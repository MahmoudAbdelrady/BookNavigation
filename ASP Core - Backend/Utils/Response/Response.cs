namespace aspcorebackend.Utils.Response {
    public class Response<T> {
        public string Status { get; set; } = null!;
        public string Message { get; set; } = null!;
        public T? Body { get; set; }

        public Response(string status, string message, T? body) {
            Status = status;
            Message = message;
            Body = body;
        }
    }
}
