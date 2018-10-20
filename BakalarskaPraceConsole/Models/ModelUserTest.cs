using RandomNameGeneratorLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BakalarskaPraceConsole.Models
{
    public class ModelUserTest
    {
        public ModelUserTest()
        {
            Random random = new Random();
            FirstName = random.GenerateRandomFirstName();
            LastName = random.GenerateRandomLastName();
            Address = random.GenerateRandomLastName();
            FkOneToTestId = random.Next(1, 2000);
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int FkOneToTestId { get; set; }

        public ModelUserTestCollection userTestCollection { get; } = new ModelUserTestCollection();
    }
}
