using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Application.Wrappers
{
    public class ResponseWrapper<T>
    {
        public bool Succeeded { get; set; }

        public string Message { get; set; } = string.Empty;

        public T? Payload { get; set; } = default;

        public static ResponseWrapper<T> Fail(string message)
        {
            return new ResponseWrapper<T>
            {
                Succeeded = false,
                Message = message
            };
        }

        public static ResponseWrapper<T> Fail(T payload)
        {
            return new ResponseWrapper<T>
            {
                Succeeded = false,
                Payload = payload
            };
        }

        public static ResponseWrapper<T> Success(string message)
        {
            return new ResponseWrapper<T>
            {
                Succeeded = true,
                Message = message
            };
        }

        public static ResponseWrapper<T> Success(T payload)
        {
            return new ResponseWrapper<T>
            {
                Succeeded = true,
                Payload = payload
            };
        }

        public static ResponseWrapper<T> Success(string message, T payload)
        {
            return new ResponseWrapper<T>
            {
                Succeeded = true,
                Message = message,
                Payload = payload
            };
        }
    }
}
