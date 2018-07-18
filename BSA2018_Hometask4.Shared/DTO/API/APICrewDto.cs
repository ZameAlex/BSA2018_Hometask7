using BSA2018_Hometask4.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSA2018_Hometask7.Shared.DTO.API
{
    public class APICrewDto
    {
        public int Id { get; set; }
        public List<APIPilotDto> Pilot { get; set; }
        public List<APIStewardessDto> Stewardess { get; set; }
    }
}
