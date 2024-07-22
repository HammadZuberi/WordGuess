using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordGuessGame.Models.Dto
{
    internal class ResponseDto
    {

        public bool Issuccess { get; set; }
        public string Response { get; set; }
        public string Message { get; set; }
    }
}
