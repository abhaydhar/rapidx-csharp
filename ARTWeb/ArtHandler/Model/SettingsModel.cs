using ArtHandler.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHandler.Model
{
    public class SettingsModel
    {
        public int setting_id { get; set; }
        public string setting_name { get; set; }
        public string setting_value { get; set; }
        public DateTime setting_date { get; set; }
    }
    public class OptionsModel
    {
        public int option_id { get; set; }
        public string option_name { get; set; }
        public string option_url { get; set; }
        public bool isEnabled { get; set; }
    }
    public class LanguageModel
    {
        public int lang_id { get; set; }
        public string lang_name { get; set; }
        public string lang_culture_name { get; set; }
        public bool isEnabled { get; set; }
    }
    public class CountryCodeModel
    {
        public string MyProperty { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public string TelephoneCode { get; set; }
        public string MobileNumberLength { get; set; }
    }

    public class ApplicationSettingsModel
    {
        public string Is_Multilingual_Enabled { get; set; }
        public string Is_OTP_Enabled { get; set; }
        public string Default_Lang { get; set; }
        public string Total_Number_Of_Questions { get; set; }
        public string Total_Number_Of_Question_To_Register { get; set; }
        public string Total_Number_Of_Question_To_Validate { get; set; }
        public string Is_User_Info_Required { get; set; }
        public string Account_Lock_Count { get; set; }
        public string Account_Lock_Duration { get; set; }
        public string Account_Lock_Attempt_Threshold { get; set; }
        public string LdapConnectionPath { get; set; }
        public string DomainName { get; set; }
        public string DomainExtn { get; set; }
        public string LdapConnectionContainer { get; set; }
        public string LdapnetworkUsername { get; set; }
        public string LdapNetworkUserPass { get; set; }
        public string EmailFromUserName { get; set; }
        public string NetWorkUserName { get; set; }
        public string NetWorkPassword { get; set; }
        public string SMTPHost { get; set; }
        public string SMTPPort { get; set; }
        public string Is_ITSM_Enabled { get; set; }
        public string ITSM_Provider_Name { get; set; }
        public string Send_Email { get; set; }
        public string IS_DOB_Validation_Needed { get; set; }
        public string Is_AD_Enabled { get; set; }
        public string Account_Unlock_Category { get; set; }
        public string Change_Password_Category { get; set; }
        public string Forgot_Password_Category { get; set; }
        public string Create_Incident_For_All { get; set; }
    }
    public class PasswordSettingsModel
    {
        public int Id { get; set; }
        public int MinLength { get; set; }
        public int MaxLength { get; set; }
        public int CapsLength { get; set; }
        public int SmallLength { get; set; }
        public int NumericLength { get; set; }
        public int SplCharsLength { get; set; }
        public string AllowedSplChars { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public int IsActive { get; set; }
    }
    public class LdapSettingsModel
    {
        public string LdapConnectionPath { get; set; }
        public string DomainName { get; set; }
        public string DomainExtn { get; set; }
        public string LdapConnectionContainer { get; set; }
        public string LdapnetworkUsername { get; set; }
        public string LdapNetworkUserPass { get; set; }
    }
    public class ArtValidationDBContext
    {
        public string SColumnName { get; set; }
        public string ViewName { get; set; }
        public string CColumnName { get; set; }
    }

    public class ArtValidationNonHexaDBContext
    {
        public string SColumnName { get; set; }
        public string ViewName { get; set; }
        public string CColumnName { get; set; }
    }

    public class ExcemptedOUs
    {
        public string OUPath { get; set; }
        public string OUName { get; set; }
        public int IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
    public class Singleton
    {
        private static readonly object _mutex = new object();
        private static volatile Singleton _instance = null;
        private Singleton() { }

        public static Singleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_mutex)
                    {
                        if (_instance == null)
                        {
                            _instance = new Singleton();
                        }
                    }
                }
                return _instance;
            }
        }

        private ApplicationSettingsModel _ClientSessionID;
        public ApplicationSettingsModel ClientSessionID
        {
            get { return _ClientSessionID; }
            set { _ClientSessionID = value; }
        }
    }

    public class SingletonPasswordSettings
    {
        private static readonly object _mutexPass = new object();
        private static volatile SingletonPasswordSettings _PasswordSettingsinstance = null;
        private SingletonPasswordSettings() { }

        public static SingletonPasswordSettings Instance
        {
            get
            {
                if (_PasswordSettingsinstance == null)
                {
                    lock (_mutexPass)
                    {
                        if (_PasswordSettingsinstance == null)
                        {
                            _PasswordSettingsinstance = new SingletonPasswordSettings();
                        }
                    }
                }
                return _PasswordSettingsinstance;
            }
        }

        private PasswordSettingsModel _PasswordSettings;
        public PasswordSettingsModel PasswordSettings
        {
            get { return _PasswordSettings; }
            set { _PasswordSettings = value; }
        }
    }

    public class SingletonLDAPSettings
    {
        private static readonly object _mutexPass = new object();
        private static volatile SingletonLDAPSettings _ldapinstance = null;
        private SingletonLDAPSettings() { }

        public static SingletonLDAPSettings Instance
        {
            get
            {
                if (_ldapinstance == null)
                {
                    lock (_mutexPass)
                    {
                        if (_ldapinstance == null)
                        {
                            _ldapinstance = new SingletonLDAPSettings();
                        }
                    }
                }
                return _ldapinstance;
            }
        }

        private LdapSettingsModel _LdapSettings;
        public LdapSettingsModel LDAPSettings
        {
            get { return _LdapSettings; }
            set { _LdapSettings = value; }
        }
    }


    public class SingletonArtValidationDBContext
    {
        private static readonly object _mutexPass = new object();
        private static volatile SingletonArtValidationDBContext _dbValidationinstance = null;
        private SingletonArtValidationDBContext() { }

        public static SingletonArtValidationDBContext Instance
        {
            get
            {
                if (_dbValidationinstance == null)
                {
                    lock (_mutexPass)
                    {
                        if (_dbValidationinstance == null)
                        {
                            _dbValidationinstance = new SingletonArtValidationDBContext();
                        }
                    }
                }
                return _dbValidationinstance;
            }
        }

        private ArtValidationDBContext _dbValidationContext;
        public ArtValidationDBContext DBValidationContext
        {
            get { return _dbValidationContext; }
            set { _dbValidationContext = value; }
        }

        private ArtValidationDBContext _dbValidationNonHexaContext;
        public ArtValidationDBContext DBValidationNonHexaContext
        {
            get { return _dbValidationNonHexaContext; }
            set { _dbValidationNonHexaContext = value; }
        }


    }
    public class SingletonExcemptedOUs
    {
        private static readonly object _mutexPass = new object();
        private static volatile SingletonExcemptedOUs _excemptedinstance = null;
        private SingletonExcemptedOUs() { }

        public static SingletonExcemptedOUs Instance
        {
            get
            {
                if (_excemptedinstance == null)
                {
                    lock (_mutexPass)
                    {
                        if (_excemptedinstance == null)
                        {
                            _excemptedinstance = new SingletonExcemptedOUs();
                        }
                    }
                }
                return _excemptedinstance;
            }
        }

        private List<ExcemptedOUs> _ExcmptedOUs;
        public List<ExcemptedOUs> ExcemptedOUs
        {
            get { return _ExcmptedOUs; }
            set { _ExcmptedOUs = value; }
        }
    }
}
