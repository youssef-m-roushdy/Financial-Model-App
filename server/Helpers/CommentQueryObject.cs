using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace server.Helpers
{
    public class CommentQueryObject
    {
        public string Symbol {get; set;}

        public bool IsDecsending {get; set;} = true;
    }
}