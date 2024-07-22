using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordGuessGame.Models.Dto
{
    internal class RequestDto
    {

        public ApiType ApiType { get; set; }
        public string RequestUrl { get; set; }
        public object Data { get; set; }
    }

    public enum ApiType
    {
        Get,
        Post,
    }
}
