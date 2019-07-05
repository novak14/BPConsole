using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AccessFacade.Dal.Entities
{
    public class UserTestInsert
    {
        //public UserTestInsert(string FirstName, string LastName, string Address, int FkOneToTestId)
        //{
        //    this.FirstName = FirstName;
        //    this.LastName = LastName;
        //    this.Address = Address;
        //    this.FkOneToTestId = FkOneToTestId;
        //}
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        public int FkOneToTestId { get; set; }

        //public OneToTest OneToTest { get; set; }
    }
}
