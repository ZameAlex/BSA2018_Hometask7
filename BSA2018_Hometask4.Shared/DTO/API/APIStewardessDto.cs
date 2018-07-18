using System;
using System.Collections.Generic;
using System.Text;

namespace BSA2018_Hometask7.Shared.DTO.API
{
    public class APIStewardessDto
    {
        public int Id { get; set; }
        public int CrewId { get; set; }
        public DateTime BirthDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
