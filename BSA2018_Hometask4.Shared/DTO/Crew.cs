using System;
using System.Collections.Generic;
using System.Text;

namespace BSA2018_Hometask4.Shared.DTO
{
    public class CrewDto
    {
        public int ID { get; set; }
        public PilotDto Pilot { get; set; }
        public List<StewardessDto> Stewardess { get; set; }
    }
}
