using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Utilities.Auth
{
    public abstract class User
    {
        private string operador;
        public abstract bool HasRole(string role);
        public bool HasRoles(string roles)
        {
            List<string> LRoles = ResolveRoles(roles);
            bool hasRole = false;
            switch (LRoles.Count)
            {
                case 1:
                    if (HasRole(LRoles.First()))
                    {
                        hasRole = true;
                    }
                    break;
                default:

                    if (operador == "|" && HasAnyRoles(LRoles))
                    {
                        hasRole = true;
                    }
                    else if (HasOnlyRoles(LRoles))
                    {
                        hasRole = true;
                    }
                    break;
            }

            return hasRole;
        }

        protected bool HasAnyRoles(List<string> roles)
        {
            bool hasRole = false;
            foreach (string role in roles)
            {
                if (HasRole(role))
                {
                    hasRole = true;
                    break;
                }
            }

            return hasRole;
        }

        protected bool HasOnlyRoles(List<string> roles)
        {
            bool hasRole = true;
            foreach (string role in roles)
            {
                if (!HasRole(role))
                {
                    hasRole = false;
                    break;
                }
            }

            return hasRole;
        }

        protected List<string> ResolveRoles(string Roles)
        {
            operador = Roles.Contains("|") ? "|" : ",";
            List<string> LRoles = new List<string>(Roles.Split(operador));

            return LRoles;
        }
    }
}
