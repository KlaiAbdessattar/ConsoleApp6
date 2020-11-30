using System;
using System.Collections.Generic;

namespace JWT.Models
{
    public partial class Action
    {
        public Action()
        {
            ProfileAction = new HashSet<ProfileAction>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ProfileAction> ProfileAction { get; set; }
    }
}
