using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyE2ETests.API.Tests.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Status { get; set; }
        public required string[] PhotoUrls { get; set; }
    }
}
