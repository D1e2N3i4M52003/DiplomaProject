using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.JSONModels 
{
    public class JSONMessage: BaseModel
    {
        public string Message { get; set; }

        public JSONMessage(string message)
        {
            Message = message;
        }
    }
}
