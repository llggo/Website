using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Security.Principal;
using Company.Models;
using System.Diagnostics;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Company.Areas.User.Controllers;

namespace Company.Extends.Library
{

    public class Signal
    {
        ApplicationDbContext db = new ApplicationDbContext();

        [HubName("signalHub")]
        public class SignalHub : Hub
        {
            public void SendPrivate(string toUserId, string msg)
            {
                var cid = Context.ConnectionId;
                var users = LCU.Get();

                var cuser = new User();
                users.TryGetValue(cid, out cuser);

                var touser = users.Where(x => x.Value.Id.Equals(toUserId));

                if (touser != null)
                {
                    Clients.Client(touser.FirstOrDefault().Key).Received(cuser, msg);
                }
                else
                {
                    Clients.Caller.Received(touser.FirstOrDefault().Key, "System: User is Offline");
                }
            }

            public void GuestSend(string name, string msg)
            {
                var cid = Context.ConnectionId;
                var users = LCU.Get();

                var cuser = new User();
                if(users.TryGetValue(cid, out cuser))
                {
                    cuser.UserName = name;
                    cuser.FullName = name;
                    users[cid] = cuser;
                }
                
                foreach (var u in users.Where(x => !x.Key.Equals(cid)))
                {
                    SendPrivate(u.Value.Id, msg);
                }

            }

            public void SendPublic()
            {

            }

            public void Update()
            {
                var cid = Context.ConnectionId;
                var users = LCU.Get();
                

                foreach(var u in users)
                {
                    var usersIgnore = users.Where(x => !x.Key.Equals(u.Key));
                    Clients.Client(u.Key).UpdateList(usersIgnore);
                }
            }

            

            public override Task OnConnected()
            {
                var Id = Context.ConnectionId;
                var user = new User();
                if(HttpContext.Current.User.Identity.GetUserId() != null)
                {
                    using(var db = new ApplicationDbContext())
                    {
                        var userdb = db.Users.Find(HttpContext.Current.User.Identity.GetUserId());
                        user.Id = userdb.Id;
                        user.UserName = userdb.UserName;
                        user.FullName = userdb.UserName;
                    }
                }
                else
                {
                    var userApplication = new ApplicationUser();
                    user.Id = userApplication.Id;
                    user.UserName = "";
                    user.FullName = "";
                }

                if (LCU.Add(Id, user))
                {
                    Update();
                };
                return base.OnConnected();
            }

            public override Task OnDisconnected(bool stopCalled)
            {
                var Id = Context.ConnectionId;
                if (LCU.Remove(Id))
                {
                    Update();
                };
                return base.OnDisconnected(stopCalled);
            }
        }

        public static ListClientUser LCU = new ListClientUser();
        public class ListClientUser
        {
            Dictionary<string, User> Users;
            public ListClientUser()
            {
                Users = new Dictionary<string, User>();
            }

            public bool Add(string Id, User user)
            {
                if(!Users.ContainsKey(Id))
                {
                    Users.Add(Id, user);
                    return true;
                }
                return false;
            }

            public bool Remove(string Id)
            {
                if (Users.ContainsKey(Id))
                {
                    Users.Remove(Id);
                    return true;
                }
                return false;
            }

            public Dictionary<string, User> Get()
            {
                return Users;
            }
        }

        public class User
        {
            public string Id { get; set; }
            public string UserName { get; set; }
            public string FullName { get; set; }
        }

    }


    //public class Signal2
    //{
        
    //    [HubName("signalHub")]
    //    public class SignalHub : Hub
    //    {

    //        public void SendPrivate(string Id, string msg)
    //        {
    //            var sid = Context.ConnectionId;

    //            var ClientView = Signal.Clients.Get(sid);

    //            if (Signal.Clients.Check(Id))
    //            {
    //                Clients.Client(Id).PrivateMessage(ClientView, msg);
    //            }
    //            else
    //            {
    //                Clients.Caller.PrivateMessage(Id, "System: User is Offline");
    //            }
    //        }

    //        public void SendAll(string name, string msg)
    //        {
    //            var sid = Context.ConnectionId;

    //            var cc = Signal.Clients.Get(sid);
    //            if(cc.User.UserName == null)
    //            {
    //                cc.User.UserName = name;
    //            }

    //            var cv = Signal.Clients.GetIgnore(sid);

    //            foreach (var c in cv)
    //            {
                    
    //                SendPrivate(c.Client.Id, msg);
    //            }
    //        }

    //        public void GuestSend(string name, string msg)
    //        {
    //            var sid = Context.ConnectionId;

    //            var ccv = new Hubs.ClientView();
    //            ccv.Client = new Hubs.Client();
    //            ccv.User = new ApplicationUser();

    //            ccv.Client.Id = sid;
    //            ccv.Client.UserId = "Guest";
    //            ccv.User.UserName = name;

    //            var clientall = Signal.Clients.GetIgnore(sid);
    //            var userclient = clientall.Where(x => !x.Client.UserId.Equals("Guest")).ToList();
    //            if (userclient.Count == 0)
    //            {
    //                Clients.Caller.PrivateMessage(sid, "System: No user is Online");
    //            }
    //            else
    //            {
    //                foreach(var uc in userclient)
    //                {
    //                    Debug.WriteLine(ccv + "");
    //                    Clients.Client(uc.Client.Id).PrivateMessage(ccv, msg);
    //                }
    //            }
    //        }

    //        public void UpdateList()
    //        {
    //            Debug.WriteLine("UpdateList");
    //            foreach (var l in Signal.Clients.List)
    //            {
    //                Debug.WriteLine("Clients.Client(l.Id).UpdateList(Signal.Clients.Get(l.Id))");
    //                 Clients.Client(l.Id).UpdateList(Signal.Clients.GetIgnore(l.Id));
    //            }

    //        }

    //        public override Task OnConnected()
    //        {
    //            Debug.WriteLine("OnConnected");
    //            var client = new Hubs.Client();
    //            client.Id = Context.ConnectionId;

    //            if (HttpContext.Current.User != null)
    //            {
    //                Debug.WriteLine("HttpContext.Current.User != null");
    //                var userId = HttpContext.Current.User.Identity.GetUserId();
    //                if (userId != null)
    //                {
    //                    Debug.WriteLine("userId != null");
    //                    client.UserId = userId;
    //                }
    //                else
    //                {
    //                    var user = new ApplicationUser();
    //                    client.UserId = user.Id;
    //                }
    //            }
    //            else
    //            {
    //                var user = new ApplicationUser();
    //                client.UserId = user.Id;
    //            }


    //             if (Signal.Clients.Add(client))
    //            {
    //                Debug.WriteLine("Signal.Clients.Add(client)");
    //                UpdateList();
    //            }

    //            return base.OnConnected();
    //        }

    //        public override Task OnDisconnected(bool stopCalled)
    //        {
    //            var Id = Context.ConnectionId;
    //            if (Signal.Clients.Remove(Id))
    //            {
    //                UpdateList();
    //            }

    //            return base.OnDisconnected(stopCalled);
    //        }

    //        public override Task OnReconnected()
    //        {
    //            return base.OnReconnected();
    //        }


    //    }

    //    public static Hubs.Clients Clients = new Hubs.Clients();

    //    public class Hubs
    //    {
    //        public class Clients
    //        {
    //            public List<Client> List { set; get; }
    //            public Clients()
    //            {
    //                List = new List<Client>();
    //            }

    //            public bool Add(Client c)
    //            {
    //                if (!List.Exists(x => x.Id.Equals(c.Id)))
    //                {
    //                    List.Add(c);
    //                    return true;
    //                }
    //                return false;
    //            }

    //            public bool Remove(string Id)
    //            {
    //                var client = List.Find(x => x.Id == Id);
    //                if (client != null)
    //                {
    //                    return List.Remove(client);
    //                }
    //                return false;
    //            }

    //            ApplicationDbContext db = new ApplicationDbContext();
    //            public List<ClientView> GetIgnore(string Id)
    //            {
    //                var lcv = new List<ClientView>();
    //                foreach (var l in List)
    //                {
    //                    if(l.Id != Id)
    //                    {
    //                        var cv = new ClientView();
    //                        cv.Client = l;
    //                        var user = db.Users.Find(l.UserId);
    //                        if(user == null)
    //                        {
    //                            user = new ApplicationUser();
    //                            user.Id = l.UserId;
    //                        }
    //                        cv.User = user;
    //                        lcv.Add(cv);
    //                    }
    //                }
    //                return lcv;
    //            }

    //            public ClientView Get(string Id)
    //            {
    //                var cv = new ClientView();
    //                var client = List.Find(x => x.Id == Id);
    //                cv.Client = client;

    //                var u = db.Users.Find(client.UserId);
    //                if (u != null)
    //                {
    //                    cv.User = u;
    //                }
    //                else
    //                {
    //                    cv.User = new ApplicationUser();
    //                    cv.User.Id = client.UserId;
    //                }
    //                return cv;
    //            }

    //            public bool Check(string Id)
    //            {
    //                if (List.Exists(x => (x.Id.Equals(Id))))
    //                {
    //                    return true;
    //                }
    //                return false;
    //            }
    //        }

    //        public class Client
    //        {
    //            public string Id { set; get; } // Connection ID
    //            public string UserId { set; get; } // User Map
    //        }

    //        public class ClientView
    //        {
    //            public Client Client { get; set; }
    //            public ApplicationUser User { get; set; }
    //        }
    //    }
    //}
}