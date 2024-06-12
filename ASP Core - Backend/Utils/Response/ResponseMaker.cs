namespace aspcorebackend.Utils.Response {
    public class ResponseMaker<T> {
        public static Response<T> SuccessRes(string message, T? body) {
            return new Response<T>("success", message, body);
        }

        public static Response<T> ErrorRes(string message) {
            return new Response<T>("failure", message, default);
        }

        public static Response<T> ErrorRes(string message, T? body) {
            return new Response<T>("failure", message, body);
        }
    }
}
