using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MTOMS
{
   public partial class em
    {
        public enum fm
        {
            online_updater=105, 
            create_cuc_file=21,
            backup_full_database=22,
            group_policy=28,
            change_password=29,
            log_off=30,
            membershi=14,
            church_groups=6,
            view_church_members=15,
            enter_offering=16,
            analyze_offering=17,
            analyze_offering_weekly=33,
            sabbath_cash_statement=34,
            analyze_project=25,
            analyze_offering_range=36,
            attendance_manager=37,
            attendance_analysis=38,
            deparments_manager=71,
            sms_mananger=79,
            chart_of_accounts=81,
            incomes_manager=82,
            pledges_manager=109,
            expenses_manager=97,
            creditors=85,
            cash_account=86,
            quarter_cash=87,
            member_statistics=92,
            church_sub_units=93,
            expense_category_settings=95,
            expense_account_settings=96,
            view_accounts=98,
            banking_section=107,
            income_accounts_settings=111,
            offering_range_1=158,
            foreign_exch_manager=159,
            pending_cheques=160,
            expenses_analysis_year=161,
            accounts_balances_manager=162,
            transfers_manager=169,
            lcb_periodic_statement=171,
            local_backup_manager=172,
            bank_reconciliation_manager=174,
            member_activity_analysis=177,
            system_default_settings=182

        }
        public enum FilterSectionType
        {
            Gender = 26,
            Baptism,
            MaritalStatus,
            ChurchGroup,
            AgeGroup,
            MemberStatus,
            Inactive,
            EducationLevel,
            ChurchRoles,
            Occupation,
            Roles,
            Nationality,
            Custom,
            BirthMonth,
            Departments,
            MemberType,
            Others
        }
        public enum xmem_status_custom
        {
            NewlyRegistered= 302,
            ALL
        }
        public enum xmem_status
        {
            Normal=134,
            Transfered=151,
            Deceased=137,
            Ageing=138,
            Apostacy=136,
            Deleted=152,
            OnDiscpline=135,
            None=0
        }
        public enum xgender
        {
            none=-1,
            Male=123,
            Female=124
        }
        public enum xstatus_groups
        {
            member,
            fg_status,
            roles,
            Baptism,
            marital,
            age_group,
            occupation,
        }
        public enum role_gp_typeS
        {
            department = 1,
            church_group
        }
        public enum MembershipType
        {
            none=12,
            RegisteredChurchMember,
            SabbathSchool,
            DualMemberShip,
            Temporary
        }
        public enum EmploymentStatus
        {
            none=13,
            Employed,
            UnEmployed,
            Student,
            SelfEmployed,
            Retired
        }
        public enum off_mem_categoryS
        {
            ChurchMember=12,
            OCI,
            ChurchSabbathSchool,
            ChurchCompany,
            PrimarySchool,
            SecondarySchool,
            Tertiary,
            University,
            ChurchDepartment,
            ChurchFamilyCity,
            ChurchFellowshipGroup,
            Hospital,
            PrivateCompany
        }
        public enum StampTables
        {
            dept_member_tb,
            dept_master_tb_ns,
            accounts_tb,
            member_tb,church_sub_unit_tb,expense_category_tb,expense_type_tb
        }
        public enum off_categoryS
        {
            Standard=5,
            ChurchProject,
            ShortTerm,
            Department,
            FamilyCity

        }
        public enum off_trans_typeS
        {
            none=4,
            normal,
            detail
        }
        public enum sms_type
        {
            company,
            bussiness,

        }
        public enum fsys_typeS
        {
            none = 0,
            folder,
            sub_folder,
            smsfile,
        }
       
    }
}
