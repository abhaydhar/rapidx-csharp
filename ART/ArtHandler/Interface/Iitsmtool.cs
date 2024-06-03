namespace ArtHandler.Interface
{
    public interface Iitsmtool
    {
        string CreateIncident(string userID, string emailID, string Usrmessage, string GSDmessage, string category, string description, string adPhysicalOfficeDelivery, ref string sys_id);

        ////string AssignIncident(string userID, string emailID);
        //string ReassignIncident(string ticketno, string Usrmessage, string GSDmessage, string emailID, string category, string sys_id);

        //string UpdateIncident(string ticketno, string Usrmessage, string GSDmessage, string emailID, string category, bool isfirstime, string sys_id);

        string ResolveIncident(string ticketno, string Usrmessage, string GSDmessage, string emailID, string category, string sys_id, bool iscreateResolve);

        string UpdateIncidentDetails(string status, bool reassign, string ticketno, string message, string userEmailID, string category, string sys_id);
    }
}