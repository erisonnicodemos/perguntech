using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Perguntech.Core.Entities
{
    public class CreateQuestionModel
    {
        public Question Question { get; set; }
        public List<string> CategoryNames { get; set; }
    }

}