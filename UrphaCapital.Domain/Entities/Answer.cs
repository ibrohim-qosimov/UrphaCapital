using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrphaCapital.Domain.Entities
{
    public class Answer
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public bool isCorrect { get; set; }
        public long TestId { get; set; }
        public Test Test { get; set; }
    }
}
