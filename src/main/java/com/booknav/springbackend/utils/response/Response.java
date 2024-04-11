package com.booknav.springbackend.utils.response;

public class Response {
    public static <T> ResponseModel<T> successRes(String message, T body) {
        return new ResponseModel<>("success", message, body);
    }

    public static <T> ResponseModel<T> errorRes(String message, T body) {
        if (body == null) {
            body = (T) "No data exists!";
        }
        return new ResponseModel<>("failure", message, body);
    }
}
