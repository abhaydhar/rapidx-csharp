using ArtHandler.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHandler.Repository
{
    public class IncidentRepository
    {
        public string GetIncidentNo(string userId, string activity)
        {
            DAL_Incident objDALIncident = new DAL_Incident();

            return objDALIncident.GetIncidentNo(userId, activity);
        }

        public string GetSysNo(string userId, string activity)
        {
            DAL_Incident objDALIncident = new DAL_Incident();

            return objDALIncident.GetSysNo(userId, activity);
        }
    }
}
