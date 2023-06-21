using System.Collections;
using System.Collections.Generic;

namespace LessonRegistration.Data.Models
{
    public class AAA : PostgreEntity
    {
        public virtual ICollection<BBB> BBBs { get; set; }
    }
}
