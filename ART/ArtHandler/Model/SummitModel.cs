using System;
using System.Collections.Generic;
namespace ArtHandler.Model
{
    public class SummitIncident
    {
        public _Proxydetails _ProxyDetails { get; set; }
        public Oticket oTicket { get; set; }
    }

    public class _Proxydetails
    {
        public string Password { get; set; }
        public int ProxyID { get; set; }
        public string ReturnType { get; set; }
        public string UserName { get; set; }
        public int OrgID { get; set; }
    }

    public class Oticket
    {
        public string AssignedExecEmailID { get; set; }
        //public object AssignedWorkgroup { get; set; }
        public string CallerEmailID { get; set; }
        //public string OpenCategory { get; set; }
        public string Classification { get; set; }
        public object ClosureCode { get; set; }
        public string Description { get; set; }
        public string Workgroup { get; set; }
        //public string IMLinkToCMDB { get; set; }
        //public string IMLinkToCR { get; set; }
        //public string IMLinkToPM { get; set; }
        //public string IMLinkToPR { get; set; }
        //public string IMLinkToSC { get; set; }
        //public string IMLinkToWO { get; set; }
        //public bool IgnoreReLoggingIncident { get; set; }
        public string Impact { get; set; }
        //public int IncidentID { get; set; }
        public int IncidentNo { get; set; }
        public string InstanceCode { get; set; }
        //public object LogTime { get; set; }
        //public string LoggedByEmailID { get; set; }
        //public object Medium { get; set; }
        //public object PendingCode { get; set; }
        //public object PendingReason { get; set; }
        public int Priority { get; set; }
        //public object Resolution_Deadline { get; set; }
        //public bool Resolution_SLA_Met { get; set; }
       public string Resolution_SLA_Reason { get; set; }
        public object Resolution_Time { get; set; }
        //public object Response_Deadline { get; set; }
        public bool Response_SLA_Met { get; set; }
        public string Response_SLA_Reason { get; set; }
        public object Response_Time { get; set; }
        public int SLA { get; set; }
        public object Solution { get; set; }
        public string Status { get; set; }
        public string Symptom { get; set; }
        public string Urgency { get; set; }
        //public string Assigned_Workgroup { get; set; }
        public string Source { get; set; }
        public string OpenCategory { get; set; }
        public string InternalLog { get; set; }
    }


    public class SummitAPIResponse
    {
        public string Errors { get; set; }
        public string Message { get; set; }
        public int OrgID { get; set; }
        public object Output { get; set; }
        public object TokenID { get; set; }
        public int TicketID_Internal { get; set; }
        public int TicketNo { get; set; }
        public TicketDetails[] OutputObject { get; set; }
    }


    public class SummitGetSRDetailsAPIResponse
    {
        public string Errors { get; set; }
        public string Message { get; set; }
        public int OrgID { get; set; }
        public object Output { get; set; }
        public object TokenID { get; set; }
        public int TicketID_Internal { get; set; }
        public int TicketNo { get; set; }
        public OutputObject OutputObject { get; set; }
    }


    public class SRInputParameters
    {
        public string ServiceName { get; set; }
        public objCommonParameters objCommonParameters { get; set; }
        public TicketDetails objSRServiceTicket { get; set; }

    }

    public class objCommonParameters
    {
        public _Proxydetails _ProxyDetails { get; set; }
        public string Instance { get; set; }
        public string SrTicket_No { get; set; }
        public string SrFromDate { get; set; }
        public string SrToDate { get; set; }
        public string srStatus { get; set; }
        public bool srchkViewTkt { get; set; }
        public bool IsGetAllRequest { get; set; }
    }
    public class SRDetails
    {
        public TicketDetails[] TicketDetails { get; set; }
        public CatalogAttributes[] CatalogAttributes { get; set; }
        public ChangeHistory[] ChangeHistory { get; set; }
    }

    public class TicketDetails
    {
        public string Org_Id { get; set; }
        public string Ticket_ID { get; set; }
        public string Ticket_No { get; set; }
        public string Reg_Time { get; set; }
        public string Status { get; set; }
        public string Medium { get; set; }
        public string LoggedBy { get; set; }
        public string LoggedBy_EmailID { get; set; }
        public string ClassificationID { get; set; }
        public string Classification { get; set; }
        public string CatalogID { get; set; }
        public string Category { get; set; }
        public string CategoryID { get; set; }
        public string Subject { get; set; }
        public string Pending_Code { get; set; }
        public string PendingReason { get; set; }
        public string Severity { get; set; }
        public string TicketNumber { get; set; }
        public string Severity_Name { get; set; }
        public string Severity_Display { get; set; }
        public string PriorityName { get; set; }
        public string Criticality { get; set; }
        public string Criticality_Name { get; set; }
        public string Impact { get; set; }
        public string SLA { get; set; }
        public string SLA_Name { get; set; }
        public string Assigned_Workgroup { get; set; }
        public string WG_Name { get; set; }
        public string Assigned_Engineer { get; set; }
        public string Assigned_Engineer_Name { get; set; }
        public string Assigned_Engineer_Email { get; set; }
        public string Assigned_Engineer_EmpID { get; set; }
        public string Response_Deadline { get; set; }
        public string Response_Time { get; set; }
        public string Response_SLA_Met { get; set; }
        public string Response_SLA_Reason { get; set; }
        public string Resolution_Deadline { get; set; }
        public string Resolution_Time { get; set; }
        public string Resolution_SLA_Met { get; set; }
        public string Resolution_SLA_Reason { get; set; }
        public string Closure_Code { get; set; }
        public string ClCode_Name { get; set; }
        public string Repeat_Ticket { get; set; }
        public string Reopen_Ticket { get; set; }
        public string attachments { get; set; }
        public string Information { get; set; }
        public string Solution { get; set; }
        public string Userlog { get; set; }
        public string Internallog { get; set; }
        public string Caller { get; set; }
        public string Caller_Name { get; set; }
        public string Caller_EmailID { get; set; }
        public string Sup_Function { get; set; }
        public string Sup_Function_Name { get; set; }
        public string KB { get; set; }
        public string ParentTicketID { get; set; }
        public string Updated_Time1 { get; set; }
        public string Updated_Time { get; set; }
        public string Schedule_Date { get; set; }
        public string Target_Completion_Date { get; set; }
        public string Resolution_SLAOfSeverity { get; set; }
        public string UserID { get; set; }
        public string ManualEscalationDate { get; set; }
        public string ManualEscalationLevelID { get; set; }
        public string ManualEscalationRemarks { get; set; }
        public string Impact_Name { get; set; }
        public string NotificationMethod { get; set; }
        public string ServiceRequestTypeName { get; set; }
        public string ServiceRequestTypeId { get; set; }
        public string ServiceCategoryName { get; set; }
        public string CategoryCatalogName { get; set; }
        public string IsApproved { get; set; }
        public string ActualRegTime { get; set; }
        public string SR_Status { get; set; }
        public string CancellationRemarks { get; set; }
        public string StrCatalogID { get; set; }
        public string ServiceCategoryId { get; set; }
        public string Response_PriorityName { get; set; }
        public string Response_DeadlineMinutes { get; set; }
        public string ResolveSRUsingLastWO { get; set; }
        public string ClosureCategory { get; set; }
        public string ResolutionCode { get; set; }
        public string ResolutionCodeName { get; set; }
        public string ClosureRemarks { get; set; }
        public string TicketClosingMode { get; set; }
        public string Type { get; set; }
        public CatalogAttributes[] CatalogAttributes { get; set; }
        public DateTime LoggedDateTime { get; set; }
        public string ConnectorStatus { get; set; }
        public string TraceRefId { get; set; }
        public string SRRequestDetails { get; set; }
    }

    public class CatalogAttributes
    {
        public int SR_CtAttribute_ID { get; set; }
        public int SR_Catalog_ID { get; set; }
        public string CatalogName { get; set; }
        public string SR_CtAttribute_Name { get; set; }
        public string DefaultValue { get; set; }
        public int SR_CtAttribute_DataType { get; set; }
        public string DataType { get; set; }
        public int SR_CtAttribute_MaxLength { get; set; }
        public bool SR_CtAttribute_IsMadatory { get; set; }
        public string SR_CtAttribute_ParentID { get; set; }
        public int newRefID { get; set; }
        public int SR_CtAttribute_DispalyMode { get; set; }
        public bool SR_CtAttribute_IsLineItem { get; set; }
        public string DisplayName { get; set; }
        public int SR_CtAttribute_SortOrder { get; set; }
        public bool SR_CtAttribute_IsUnique { get; set; }
        public bool SR_CtAttribute_Active { get; set; }
        public int SR_CtAttribute_CreatedBy { get; set; }
        public string SR_CtAttribute_Description { get; set; }
        public int SR_CtAttribute_UpdatedBy { get; set; }
        public DateTime SR_CtAttribute_CreatedDate { get; set; }
        public DateTime SR_CtAttribute_UpdatedDate { get; set; }
        public bool SR_CtAttribute_IsDisplay { get; set; }
        public string ParentNameText { get; set; }
        public int SR_AttributeValue_UpdatedByValue { get; set; }
        public string SR_AttributeValue_UpdatedBy { get; set; }
        public string SR_AttributeValue { get; set; }
        public string SR_AttributeFormula { get; set; }
        public int RefAttributeID { get; set; }
        public bool IsAddedtoPackage { get; set; }
        public int Group_ID { get; set; }
        public string Group_Name { get; set; }
        public int Group_SortOrder { get; set; }
        public string IsMultivalued { get; set; }
        public int PageSize { get; set; }
        public bool IsEditable { get; set; }
        public int GroupApprovalLevel { get; set; }
        public int CustomGroupID { get; set; }
        public int UID { get; set; }
        public bool SR_ShowTotal { get; set; }
        public string SR_GroupByAttributeID { get; set; }
        public string GroupByAttributeName { get; set; }
        public string SR_CtAttribute_DispalyModeText { get; set; }
        public string StrCatalogId { get; set; }
        public int GroupColumns { get; set; }
        public string AttributeSize { get; set; }
        public string FBControlType { get; set; }
        public string Tooltip { get; set; }
        public string Watermark { get; set; }
        public int Minlength { get; set; }
        public string PriceUnits { get; set; }
        public bool Number_AcceptOnlyInteger { get; set; }
        public int Number_RangeFrom { get; set; }
        public int Number_RangeTo { get; set; }
        public int Maxlength { get; set; }
        public bool EndUserView { get; set; }
        public string AttributeText { get; set; }
        public bool SR_CtAttribute_RBIncludeOtherOption { get; set; }
        public int SR_CtAttribute_RB_Columns { get; set; }
        public string MultiValuedButtonText { get; set; }
        public bool IsDefaultVisible { get; set; }
        public bool IsCalculated { get; set; }
        public bool User_Search_IncludeInActive { get; set; }
        public int DefaultID { get; set; }
        public bool IsApproverGroup { get; set; }
        public int RequiredRows { get; set; }
        public bool AllControlsAreHiddenInGroup { get; set; }
    }

    public class ChangeHistory
    {
        public int uid { get; set; }
        public string column_name { get; set; }
        public string change_date { get; set; }
        public string change_by { get; set; }
        public string change_byName { get; set; }
        public string old_Value { get; set; }
        public string new_Value { get; set; }
    }

    public class OutputObject
    {
        public SRDetails SRDetails { get; set; }
    }

    public class SummitCreateIncident
    {
        public string ServiceName { get; set; }
        public objIncidentCommonParameters objCommonParameters { get; set; }
    }

    public class objIncidentCommonParameters
    {
        public _Proxydetails _ProxyDetails { get; set; }
        public IncidentParamsJSON incidentParamsJSON { get; set; }
        public string RequestType { get; set; }
    }

    public class IncidentParamsJSON
    {
        public string IncidentContainerJson { get; set; }
    }

    public class IncidentContainerJson
    {
        public string SelectedAssets { get; set; }
        public string Updater { get; set; }
        public Ticket Ticket { get; set; }
        public TicketInformation TicketInformation { get; set; }
        public Array CustomFields { get; set; }
    }


    public class SummitUpdateIncident
    {
        public string ServiceName { get; set; }
        public objUpdateIncidentCommonParameters objCommonParameters { get; set; }
    }

    public class objUpdateIncidentCommonParameters
    {
        public _Proxydetails _ProxyDetails { get; set; }
        public UpdateIncidentParamsJSON incidentParamsJSON { get; set; }
        public string RequestType { get; set; }
    }

    public class UpdateIncidentParamsJSON
    {
        public string IncidentContainerJson { get; set; }
    }

    public class UpdateIncidentContainerJson
    {
        public string SelectedAssets { get; set; }
        public string Updater { get; set; }
        public UpdateIncidentTicket Ticket { get; set; }
        public TicketInformation TicketInformation { get; set; }
        public Array CustomFields { get; set; }
    }

    public class TicketInformation
    {
        public string ClosureRemarks { get; set; }
        public string Solution { get; set; }
        public string InternalLog { get; set; }
        public string UserLog { get; set; }
        public string Information { get; set; }
    }

    public class Ticket
    {
        public bool IsFromWebService { get; set; }
        public string Criticality { get; set; }
        public bool Response_SLA_Met { get; set; }
        public DateTime Resolution_Deadline { get; set; }
        public string Classification { get; set; }
        public string Desc { get; set; }
        public string InternalLog { get; set; }
        public string SLA { get; set; }
        public DateTime Response_Deadline { get; set; }
        public string Sup_Function { get; set; }
        public string SupportFunction { get; set; }
        public string Solution { get; set; }
        public string Caller { get; set; }
        public string Caller_EmailID { get; set; }
        public int ResolutionCode { get; set; }
        public string Status { get; set; }
        public string ScheduledDate { get; set; }
        public string TicketClosingMode { get; set; }
        public string Severity { get; set; }
        public bool Resolution_SLA_Met { get; set; }
        public string Assigned_WorkGroup { get; set; }
        public string PendingReason { get; set; }
        public string Medium { get; set; }
        public string Resolution_SLA_Reason { get; set; }
        public string Impact_Id { get; set; }
        public string UserLog { get; set; }
        public string EncriptTicketID { get; set; }
        public string Closure_Code { get; set; }
        public DateTime Reg_Time { get; set; }
        public string Category { get; set; }
        public int NotificationMethod { get; set; }
        public string Response_SLA_Reason { get; set; }
        public string OpenCategory { get; set; }
        public string Assigned_Engineer { get; set; }
        public string Assigned_Engineer_Email { get; set; }
        public int Pending_Code { get; set; }
        public string Description { get; set; }
        public string Ticket_No { get; set; }
        public string Resolution_Time { get; set; }
        public string PageName { get; set; }
        public string Priority_Name { get; set; }
        public string Classification_Name { get; set; }
        public string SLA_Name { get; set; }
        public string ResolutionCodeName { get; set; }
        public string Urgency_Name { get; set; }
        public string Assigned_WorkGroup_Name { get; set; }
        public string Impact_Name { get; set; }
        public string Closure_Code_Name { get; set; }
        public string Category_Name { get; set; }
        public string OpenCategory_Name { get; set; }
        public string IncidentTicketID { get; set; }
        public string Response_Time { get; set; }
        public string Updated_Time { get; set; }
        public string Instance { get; set; }
        public string Source { get; set; }
    }

    public class UpdateIncidentTicket
    {
        public bool IsFromWebService { get; set; }
        public string Criticality { get; set; }
        public bool Response_SLA_Met { get; set; }
        public DateTime Resolution_Deadline { get; set; }
        public string Classification { get; set; }
        public string Desc { get; set; }
        public string InternalLog { get; set; }
        public string SLA { get; set; }
        public DateTime Response_Deadline { get; set; }
        public string Sup_Function { get; set; }
        public string Solution { get; set; }
        public string Caller { get; set; }
        public string Caller_EmailID { get; set; }
        public int ResolutionCode { get; set; }
        public string Status { get; set; }
        public string ScheduledDate { get; set; }
        public string TicketClosingMode { get; set; }
        public string Severity { get; set; }
        public bool Resolution_SLA_Met { get; set; }
        public string Assigned_WorkGroup { get; set; }
        public string PendingReason { get; set; }
        public string Medium { get; set; }
        public string Resolution_SLA_Reason { get; set; }
        public string Impact_Id { get; set; }
        public string UserLog { get; set; }
        public string EncriptTicketID { get; set; }
        public string Closure_Code { get; set; }
        public string Category { get; set; }
        public int NotificationMethod { get; set; }
        public string Response_SLA_Reason { get; set; }
        public string OpenCategory { get; set; }
        public string Assigned_Engineer { get; set; }
        public string Assigned_Engineer_Email { get; set; }
        public int Pending_Code { get; set; }
        public string Description { get; set; }
        public string Ticket_No { get; set; }
        public string Resolution_Time { get; set; }
        public string PageName { get; set; }
        public string Priority_Name { get; set; }
        public string Classification_Name { get; set; }
        public string SLA_Name { get; set; }
        public string ResolutionCodeName { get; set; }
        public string Urgency_Name { get; set; }
        public string Assigned_WorkGroup_Name { get; set; }
        public string Impact_Name { get; set; }
        public string Closure_Code_Name { get; set; }
        public string Category_Name { get; set; }
        public string OpenCategory_Name { get; set; }
        public string IncidentTicketID { get; set; }
        public string Updated_Time { get; set; }
        public string Instance { get; set; }
    }
    public class SRUpdateInputParameters
    {
        public string ServiceName { get; set; }
        public objCommonParametersForSRUpdate objCommonParameters { get; set; }
    }
    public class objCommonParametersForSRUpdate
    {
        public _Proxydetails _ProxyDetails { get; set; }
        public SRTicketUpdate objSRServiceTicket { get; set; }
    }

    public class SRTicketUpdate
    {
        public string Criticality { get; set; }
        public bool Response_SLA_Met { get; set; }
        public string Classification { get; set; }
        public string Desc { get; set; }
        public string InternalLog { get; set; }
        public string SLA { get; set; }
        public string Sup_Function { get; set; }
        public string SupportFunction { get; set; }
        public string Solution { get; set; }
        public string Caller { get; set; }
        public string Caller_EmailID { get; set; }
        public int ResolutionCode { get; set; }
        public string Status { get; set; }
        public string TicketClosingMode { get; set; }
        public string Severity { get; set; }
        public bool Resolution_SLA_Met { get; set; }
        public string Assigned_WorkGroup { get; set; }
        public string PendingReason { get; set; }
        public string Resolution_SLA_Reason { get; set; }
        public string Impact_Id { get; set; }
        public string UserLog { get; set; }
        public string Closure_Code { get; set; }
        public string Category { get; set; }
        public string Response_SLA_Reason { get; set; }
        public string OpenCategory { get; set; }
        public string Assigned_Engineer { get; set; }
        public string Assigned_Engineer_Email { get; set; }
        public int Pending_Code { get; set; }
        public string Description { get; set; }
        public string Ticket_No { get; set; }
        public string Resolution_Time { get; set; }
        public string Priority_Name { get; set; }
        public string Classification_Name { get; set; }
        public string SLA_Name { get; set; }
        public string ResolutionCodeName { get; set; }
        public string Urgency_Name { get; set; }
        public string Assigned_WorkGroup_Name { get; set; }
        public string Impact_Name { get; set; }
        public string Closure_Code_Name { get; set; }
        public string Category_Name { get; set; }
        public string OpenCategory_Name { get; set; }
        public string IncidentTicketID { get; set; }
        public string Response_Time { get; set; }
        public string Updated_Time { get; set; }

    }

}
