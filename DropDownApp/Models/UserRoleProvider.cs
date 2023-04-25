using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace DropDownApp.Models
{
    public class UserRoleProvider : RoleProvider
    {



        SampleDbContext db = null;

       public UserRoleProvider()
        {
            db = new SampleDbContext();

        }


        public override string ApplicationName 
        { 
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();

        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {

            //var userRoleList = (from x in db.sampleEmps
            //                    where x.Name == username
            //                    select x.Roles).ToString();
            //var RoleArr = userRoleList.Split(new[] { ',' }).ToArray();

            ////return RoleArr;
            // if(username != null)
            //{



            //}
            string[] str = null;

            var userRoleList = db
                                        .sampleEmps
                                        .Where(x => x.Name == username)
                                        .Select(x => x.Roles)
                                        .SingleOrDefault();
            if(userRoleList != null)
            {
                var result = userRoleList.ToString();
                str = userRoleList.Split(new[] { ',' });


            }



            return str;







        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}