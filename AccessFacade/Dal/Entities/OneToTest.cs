using System;
using System.Collections.Generic;
using System.Text;

namespace AccessFacade.Dal.Entities
{
    public class OneToTest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public List<UserTest> UserTests { get; set; } = new List<UserTest>();
        //public UserTest UserTest { get; set; }
    }
}
