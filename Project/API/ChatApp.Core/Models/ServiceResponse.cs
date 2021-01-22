using ChatApp.Core.Infrastructure;
using System.Collections.Generic;

namespace ChatApp.Core.Models.ApiModel
{
    public class ApiResponse
    {
        public string Status { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public object Data { get; set; }
        public List<ErrorList> Errors { get; set; }
    }

    public class ApiRequest
    {
        public string token { get; set; }
        public string key { get; set; }
    }

    public class ServiceResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }

    public class ErrorList
    {
        public string field { get; set; }
        public string errorCode { get; set; }
        public string message { get; set; }
    }
}