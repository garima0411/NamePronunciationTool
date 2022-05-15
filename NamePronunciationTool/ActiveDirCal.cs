using System;
using System.Collections.Generic;
//using System.DirectoryServices;

namespace NamePronunciationTool
{
    public class ActiveDirCal
    {
        public static List<ADUser> GetADUsersByLDAPGroup(string grpname,string domainName)
        {
            List<ADUser> aDUser = new List<ADUser>();
            try
            {
                ADSearch adsearch = new ADSearch(domainName);
                //aDUser = adsearch.GetGroupUsers(grpname);
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return aDUser;
        }
        
    }
    public class ADUser
    {
        public ADUser() { }

    }
    public class ADSearch
    {
        private string _domain;
        public ADSearch(string domain)
        {
            _domain = domain;
        }
    }
}
