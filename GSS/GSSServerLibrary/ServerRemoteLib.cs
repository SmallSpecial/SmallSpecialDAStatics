using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSSServerLibrary
{
    public class ServerRemoteLib: MarshalByRefObject
    {
        public string RoleNameChange(string gamename, string bigzonename,int userid, int roleid, string rolename, string newrolename)
        {
            return WebServiceLibR.RoleNameChange(gamename, bigzonename,userid, roleid, rolename, newrolename);
        }
        public string RoleZoneChange(string gamename, string bigzonename,int userid, int roleid, int zoneid)
        {
            return WebServiceLibR.RoleZoneChange(gamename, bigzonename,userid, roleid, zoneid);
        }

        public string UserRoleClearOnline(string gamename, string bigzonename, int userid)
        {
            return WebServiceLibR.UserRoleClearOnline(gamename, bigzonename, userid);
        }
    }
}
