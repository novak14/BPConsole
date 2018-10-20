using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccessFacade.Dal.Entities
{
    public class UserTestUpdate
    {
        public UserTestUpdate(string FirstName, int id)
        {
            this.Id = id;
            this.FirstName = FirstName;
        }

        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
    }
}
