using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class InternalUser
    {
        public int InternalUserId { get; set; }

        public string WorkNum { get; set; }

        public string Pwd { get; set; }

        private string _name;
        public string Name 
        { 
            get{return _name;}
            set
            {
                _name = value != null ? value : string.Empty;
            }
        }

        //如果为null，则为true
        private System.Nullable<bool> _gender;
        public System.Nullable<bool> Gender
        {
            get { return _gender; }
            set 
            {
                _gender = value != null ? value : true;
            }
        }

        public System.Nullable<System.DateTime> RegDate { get; set; }

        private string _departmentName;
        public string DepartmentName
        {
            get { return _departmentName; }
            set
            {
                _departmentName = value != null ? value : string.Empty;
            }
        }

        private string _jobs;
        public string Jobs
        {
            get { return _jobs; }
            set
            {
                _jobs = value != null ? value : string.Empty;
            }
        }

        private string _userGroup;
        public string UserGroup
        {
            get { return _userGroup; }
            set
            {
                _userGroup = value != null ? value : string.Empty;
            }
        }

        public System.Nullable<int> FkUserGroupId { get; set; }

        public System.Nullable<System.DateTime> AllocateDate { get; set; }

        private string _operater;
        public string Operater
        {
            get { return _operater; }
            set
            {
                _operater = value != null ? value : string.Empty;
            }
        }

        public char UseStatus { get; set; }
    }
}
