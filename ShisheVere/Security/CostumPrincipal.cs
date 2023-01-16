using ShisheVere.Models;
using System.Linq;
using System.Security.Principal;

namespace ShisheVere.Security
{
    public class CostumPrincipal
    {
        private Perdorues Perdorues;
        public CostumPrincipal(Perdorues account)
        {
            this.Perdorues = account;
            this.Identity = new GenericIdentity(account.Username);

        }
        public IIdentity Identity
        {
            get;
            set;
        }

        public bool IsInRole(string role)
        {
            var roles = role.Split(new char[] { ',' });
            return roles.Any(r => this.Perdorues.Roli.Contains(r));
        }

    }
}