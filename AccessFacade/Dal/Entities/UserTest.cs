using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AccessFacade.Dal.Entities
{
    public class UserTest
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        public int FkOneToTestId { get; set; }

        //public OneToTest OneToTest { get; set; }
    }
}
