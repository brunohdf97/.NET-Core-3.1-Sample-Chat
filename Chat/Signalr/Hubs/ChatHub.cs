using Chat.Domain.Models.ViewModels;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.Extensions;
using Chat.DOMAIN.Extensions;

namespace Chat.Signalr.Hubs
{
    public class ConnectionMapping
    {
        private readonly Dictionary<int, KeyValuePair<UserViewModel, HashSet<string>>> _connections =
            new Dictionary<int, KeyValuePair<UserViewModel, HashSet<string>>>();

        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }

        public void Add(UserViewModel user_vmodel, string connectionId)
        {
            lock (_connections)
            {

                KeyValuePair<UserViewModel, HashSet<string>> keypar;
                if (!_connections.TryGetValue(user_vmodel.User.Id, out keypar))
                {
                    HashSet<string> connections = new HashSet<string>();
                    keypar = new KeyValuePair<UserViewModel, HashSet<string>>(user_vmodel, connections);
                    _connections.Add(user_vmodel.User.Id, keypar);
                }

                lock (keypar.Value)
                {
                    if (keypar.Value.Count > 0)
                        keypar.Value.Clear();

                    keypar.Value.Add(connectionId);
                }
            }
        }

        public List<UserViewModel> GetAllConnectedUsers()
        {
            return _connections.Values.Select(a => a.Key).ToList();
        }


        public HashSet<string> GetAllConnectionIds()
        {
            HashSet<string> connection_ids = new HashSet<string>();
            foreach (var connection in _connections.Values)
            {
                foreach (string connection_id in connection.Value)
                {
                    connection_ids.Add(connection_id);
                }

            }

            return connection_ids;
        }

        public List<string> GetAllConnectionIdsAsList(int[] users_id = null)
        {

            List<string> connection_ids = new List<string>();

            var values = users_id == null || users_id.Length == 0 
                ? _connections.Values 
                : _connections.Values.Where(a => users_id.Contains(a.Key.User.Id));

            foreach (var connection in values)
            {
                foreach (string connection_id in connection.Value)
                {
                    connection_ids.Add(connection_id);
                }
            }

            return connection_ids;
        }

        public List<string> GetConnections(UserViewModel user_vmodel)
        {
            KeyValuePair<UserViewModel, HashSet<string>> keypar;
            if (_connections.TryGetValue(user_vmodel.User.Id, out keypar))
            {
                return keypar.Value.ToList();
            }

            return new List<string>();
        }

        public void Remove(UserViewModel user_vmodel, string connectionId)
        {
            lock (_connections)
            {
                KeyValuePair<UserViewModel, HashSet<string>> keypar;
                if (!_connections.TryGetValue(user_vmodel.User.Id, out keypar))
                {
                    return;
                }

                lock (keypar.Value)
                {
                    keypar.Value.Remove(connectionId);

                    if (keypar.Value.Count == 0)
                    {
                        _connections.Remove(user_vmodel.User.Id);
                    }
                }
            }
        }
    }

    public class ChatHub : Hub
    {
        private readonly static ConnectionMapping _connections =
            new ConnectionMapping();

        public void GetAllConnectedUsers()
        {
            var connected_users = _connections.GetAllConnectedUsers();

            Clients.Caller.SendAsync("ReceiveAllConnectedUsers", connected_users);
        }

        public void SendChatMessage(string from, string to, string message)
        {
            int[] in_to = to.Split(',').Select(int.Parse).ToArray();
            int[] in_users_id = in_to.Union(new int[] { from.ToInt() }).ToArray();

            var connection_ids = _connections.GetAllConnectionIdsAsList(in_users_id);
            Clients.Clients(connection_ids).SendAsync("ReceiveMessage", from, to, message);
        }

        //public void SendChatGroupMessage(string groupid, string message)
        //{

        //    //Clients.Clients()
        //}

        //public void JoinGroup(string groupid)
        //{

        //}

        //public void LeaveGroup(string groupid)
        //{

        //}

        public override async Task OnConnectedAsync()
        {
            UserViewModel logged_user = Context.GetHttpContext().Session.GetJsonSession<UserViewModel>("user");

            try
            {
                _connections.Add(logged_user, Context.ConnectionId);

                var connection_ids = _connections.GetAllConnectionIdsAsList();

                await Clients.All.SendAsync("ConnectedUsersUpdated");

            }
            catch (Exception e)
            {

            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            UserViewModel logged_user = Context.GetHttpContext().Session.GetJsonSession<UserViewModel>("user");

            try
            {
                _connections.Remove(logged_user, Context.ConnectionId);

                await Clients.All.SendAsync("ConnectedUsersUpdated");
            }
            catch (Exception e)
            {

            }

            await base.OnDisconnectedAsync(exception);
        }

    }
}
