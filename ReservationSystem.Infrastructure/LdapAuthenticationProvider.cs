using Microsoft.Extensions.Options;
using ReservationSystem.Infrastructure.Settings;
using System.DirectoryServices.Protocols;
using System.Net;

namespace ReservationSystem.Infrastructure
{
    public class LdapAuthenticationProvider
    {
        private readonly LdapConnection _connection;
        private readonly LdapSettings _ldapSettings;

        public LdapAuthenticationProvider(IOptions<LdapSettings> ldapSettings)
        {
            _ldapSettings = ldapSettings.Value;
            LdapDirectoryIdentifier ldi = new LdapDirectoryIdentifier(_ldapSettings.ServerAddress);
            _connection = new LdapConnection(ldi);
            _connection.SessionOptions.ProtocolVersion = _ldapSettings.ProtocolVersion;
            _connection.AuthType = AuthType.Basic;
        }

        public async Task<IList<string>> AuthenticateAsync(string username, string password)
        {
            try
            {
                _connection.Credential = new NetworkCredential(
                    _ldapSettings.LoginDN.Replace("{username}", username), password);

                _connection.Bind();

                var userGroups = await GetUserGroupsAsync(username);

                _connection.Dispose();

                return userGroups;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<IList<string>> GetUserGroupsAsync(string username)
        {
            var groups = new List<string>();

            var request = new SearchRequest(
                    _ldapSettings.GroupsSearchBase,
                    _ldapSettings.GroupsSearchFilter.Replace("{username}", username),
                    SearchScope.Subtree);

            var response = (SearchResponse)_connection.SendRequest(request);

            foreach (SearchResultEntry entry in response.Entries)
            {
                groups.Add(entry.DistinguishedName.Replace($",{_ldapSettings.GroupsSearchBase}", ""));
            }

            return groups;
        }
    }
}
