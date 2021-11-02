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

        public void Add(UserViewModel userVmodel, string connectionId)
        {
            lock (_connections)
            {

                KeyValuePair<UserViewModel, HashSet<string>> keypar;
                if (!_connections.TryGetValue(userVmodel.User.Id, out keypar))
                {
                    HashSet<string> connections = new HashSet<string>();
                    keypar = new KeyValuePair<UserViewModel, HashSet<string>>(userVmodel, connections);
                    _connections.Add(userVmodel.User.Id, keypar);
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
            HashSet<string> connectionIds = new HashSet<string>();
            foreach (var connection in _connections.Values)
            {
                foreach (string connectionId in connection.Value)
                {
                    connectionIds.Add(connectionId);
                }

            }

            return connectionIds;
        }

        public List<string> GetAllConnectionIdsAsList(int[] usersId = null)
        {

            List<string> connectionIds = new List<string>();

            var values = usersId == null || usersId.Length == 0 
                ? _connections.Values 
                : _connections.Values.Where(a => usersId.Contains(a.Key.User.Id));

            foreach (var connection in values)
            {
                foreach (string connectionId in connection.Value)
                {
                    connectionIds.Add(connectionId);
                }
            }

            return connectionIds;
        }

        public List<string> GetConnections(UserViewModel userVModel)
        {
            KeyValuePair<UserViewModel, HashSet<string>> keypar;
            if (_connections.TryGetValue(userVModel.User.Id, out keypar))
            {
                return keypar.Value.ToList();
            }

            return new List<string>();
        }

        public void Remove(UserViewModel userVModel, string connectionId)
        {
            lock (_connections)
            {
                KeyValuePair<UserViewModel, HashSet<string>> keypar;
                if (!_connections.TryGetValue(userVModel.User.Id, out keypar))
                {
                    return;
                }

                lock (keypar.Value)
                {
                    keypar.Value.Remove(connectionId);

                    if (keypar.Value.Count == 0)
                    {
                        _connections.Remove(userVModel.User.Id);
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
            var connectedUsers = _connections.GetAllConnectedUsers();

            Clients.Caller.SendAsync("ReceiveAllConnectedUsers", connectedUsers);
        }

        public void SendChatMessage(string from, string to, string message)
        {
            int[] inTo = to.Split(',').Select(int.Parse).ToArray();
            int[] inUsersId = inTo.Union(new int[] { from.ToInt() }).ToArray();

            var connectionIds = _connections.GetAllConnectionIdsAsList(inUsersId);
            Clients.Clients(connectionIds).SendAsync("ReceiveMessage", from, to, message);
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
            UserViewModel loggedUser = Context.GetHttpContext().Session.GetJsonSession<UserViewModel>("user");

            try
            {
                _connections.Add(loggedUser, Context.ConnectionId);

                var connectionIds = _connections.GetAllConnectionIdsAsList();

                await Clients.All.SendAsync("ConnectedUsersUpdated");

            }
            catch (Exception e)
            {

            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            UserViewModel loggedUser = Context.GetHttpContext().Session.GetJsonSession<UserViewModel>("user");

            try
            {
                _connections.Remove(loggedUser, Context.ConnectionId);

                await Clients.All.SendAsync("ConnectedUsersUpdated");
            }
            catch (Exception e)
            {

            }

            await base.OnDisconnectedAsync(exception);
        }

    }
}
