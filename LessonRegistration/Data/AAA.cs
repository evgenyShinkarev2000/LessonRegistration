using System.Collections;
using System.Collections.Generic;

namespace LessonRegistration.Data
{
    public class AAA : PostgreEntity
    {
        public virtual ICollection<BBB> BBBs { get; set; }
    }
}
