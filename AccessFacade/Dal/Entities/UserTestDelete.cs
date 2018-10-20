using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccessFacade.Dal.Entities
{
    public class UserTestDelete
    {
        public UserTestDelete(int id)
        {
            this.Id = id;
        }

        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
    }
}
