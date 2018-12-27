using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using CommonTypes;
using System.IO;
using System.Runtime.Serialization;
using DatabaseManagement;

namespace ServerBL
{


    public class BL
    {

        public bool Connected { get; set; }
        public int Counter { get { return _clientList.Count; } }
        TcpListener _listener;
        public List<User> _clientList;
        TcpClient _tcpClient;
        IPAddress _ip;
        int _port;
        Thread _mainTread;
        Stream ns;
        public List<CurrentUsersOnLineEventArgs> OnLine = new List<CurrentUsersOnLineEventArgs>();
        public UsersAndMessagesManager ManagerOfDb = new UsersAndMessagesManager();

        public event EventHandler<ConnectingAndDisconnectingEventArgs> ClientAccepted;
        public event EventHandler<ConnectingAndDisconnectingEventArgs> ClientDisconnected;
        public event EventHandler<CurrentUsersOnLineEventArgs> CurrentClientOnLine;
        public event EventHandler ClientCounter;



        public void ConnectAndStartAcceptClients(string IP, string Port)
        {
            bool validIP = IPAddress.TryParse(IP, out _ip);
            bool validPort = int.TryParse(Port, out _port);

            if (validIP == false && validPort == true)
            {
                throw new SpecialException('A');
            }
            else if (validPort == false && validIP == true)
            {
                throw new SpecialException('B');
            }
            else if (validIP == false && validPort == false)
            {
                throw new SpecialException('C');
            }
            if (validPort && validIP && Connected == false)
            {
                if (_port > 65534)
                {
                    throw new SpecialException('D');
                }
                try
                {
                    _listener = new TcpListener(_ip, _port);
                    _listener.Start();
                    Connected = true;
                    _clientList = new List<User>();
                    _mainTread = new Thread(() =>
                    {
                        while (Connected)
                        {
                            if (!_listener.Pending())
                            {
                                Thread.Sleep(10);
                                continue;
                            }
                            _tcpClient = _listener.AcceptTcpClient();
                            ns = _tcpClient.GetStream();
                            new Thread(() => HandleClient(_tcpClient, ns)).Start();
                        }
                    });
                    _mainTread.Start();
                }
                catch (Exception)
                {
                    Connected = false;
                    _listener.Stop();
                    throw new SpecialException('E');
                }

            }
            else
            {
                throw new Exception();
            }

        }
        public void HandleClient(TcpClient Tcp, Stream TheStream)
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                while (true)
                {
                    MessageBase mb = (MessageBase)bf.Deserialize(TheStream);
                    UsersAndMessagesManager manager = new UsersAndMessagesManager();

                    if (mb is TextMessage)
                    {
                        TextMessage messageToSend = new TextMessage();
                        messageToSend = (TextMessage)mb;
                        RouteMessages(messageToSend, TheStream);
                        manager.InsertNewMessageToDB(messageToSend.SenderId, messageToSend.Message);
                    }
                    else if (mb is DisconnectionMessage)
                    {
                        DisconnectionMessage disconnect = new DisconnectionMessage();
                        disconnect = (DisconnectionMessage)mb;
                        ClientDisconnected(this, new ConnectingAndDisconnectingEventArgs(disconnect.CurrenUser.Name));
                        DisconnectUser(disconnect.CurrenUser);
                        CurrentClientOnLine(this, new CurrentUsersOnLineEventArgs(mb.CurrenUser.Name, mb.CurrenUser.ID, 'B'));
                        ClientCounter(this, new EventArgs());
                        TextMessage byeMessageFromServer = new TextMessage() { Message = "[**Server message**]: " + disconnect.CurrenUser.Name + " left" };
                        RouteMessages(byeMessageFromServer, TheStream);
                        manager.UpdateLastSeenWhenDisconnecting(disconnect.CurrenUser.ID);
                    }
                    else if (mb is MessageBase)
                    {

                        //verify if details are unique
                        if (mb.CurrenUser.IsNew == true)
                        {
                            bool checkUniqueDetails = manager.CheckIfUserIdExistInDb(mb.CurrenUser.ID, mb.CurrenUser.Name);
                            if (checkUniqueDetails)
                            {
                                MessageBase m = mb;
                                m.op.NewUserIdlsAlreadyExistInDB = true;
                                bf.Serialize(TheStream, m);
                                return;
                            }
                            manager.InsertNewUserToDB(mb.CurrenUser.ID, mb.CurrenUser.Name, mb.CurrenUser.Password);
                        }
                        else
                        {
                            char verifyUserBeforeLogIn = manager.MatchingTheDetailsOfLogingUser(mb.CurrenUser.ID, mb.CurrenUser.Name, mb.CurrenUser.Password);
                            if (verifyUserBeforeLogIn == 'a' || verifyUserBeforeLogIn == 'b' || verifyUserBeforeLogIn == 'd')
                            {
                                MessageBase m = mb;
                                m.op.ErrorState = verifyUserBeforeLogIn;
                                bf.Serialize(TheStream, m);
                                return;
                            }
                        }

                        bf.Serialize(TheStream, mb);
                        _clientList.Add(mb.CurrenUser);
                        mb.CurrenUser.Stream = TheStream;
                        ClientAccepted(this, new ConnectingAndDisconnectingEventArgs(mb.CurrenUser.Name.ToString()));
                        CurrentClientOnLine(this, new CurrentUsersOnLineEventArgs(mb.CurrenUser.Name, mb.CurrenUser.ID, 'A'));
                        ClientCounter(this, new EventArgs());
                        TextMessage welcomeMessageFromServer = new TextMessage() { Message = "[**Server message**]: " + mb.CurrenUser.Name + " joined " };
                        RouteMessages(welcomeMessageFromServer, TheStream);
                    }
                }

            }
            catch (SerializationException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (IOException)
            { }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void RouteMessages(TextMessage message, Stream Stream)
        {
            BinaryFormatter bf = new BinaryFormatter();
            foreach (User user in _clientList)
            {
                bf.Serialize(user.Stream, message);
            }
        }
        public void DisconnectUser(User CurrentUser)
        {
            foreach (User user in _clientList)
            {
                if (user.ID == CurrentUser.ID)
                {
                    _clientList.Remove(user);
                    return;
                }
            }
        }
        public void DisconnectServer()
        {
            try
            {
                if (Connected == true)
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    TextMessage t = new TextMessage() { Message = "server disconnect", SpecialSignFromServer = true };
                    foreach (User user in _clientList)
                    {
                        bf.Serialize(user.Stream, t);
                    }
                    _clientList.RemoveRange(0, _clientList.Count);
                    CurrentClientOnLine(this, new CurrentUsersOnLineEventArgs("Server", "0", 'C'));
                    ClientCounter(this, new EventArgs());
                    Connected = false;
                    _listener.Stop();
                    _listener.Server.Disconnect(true);
                    _listener.Server.Close();
                }
            }
            catch (SocketException)
            {

            }
            catch (Exception)
            {
                
            }
        }
        public void DeletingUserReaction(string UserID)
        {
            BinaryFormatter bf = new BinaryFormatter();
            TextMessage t = new TextMessage() { Message = "Removed By Server", SpecialSignFromServer = true };
            User u = new User();
            foreach (var item in _clientList)
            {
                if (item.ID == UserID)
                {
                    u = item;
                    bf.Serialize(u.Stream, t);
                }
            }
            ClientDisconnected(this, new ConnectingAndDisconnectingEventArgs(u.Name));
            DisconnectUser(u);
            CurrentClientOnLine(this, new CurrentUsersOnLineEventArgs(u.Name, u.ID, 'B'));
            ClientCounter(this, new EventArgs());
            TextMessage byeMessageFromServer = new TextMessage() { Message = "[**Server message**]: " + u.Name + " removed" };
            RouteMessages(byeMessageFromServer, ns);
        }
    }
    public class CurrentUsersOnLineEventArgs : EventArgs
    {
        public char _scenario;
        public string _name;
        public string _id;
        public CurrentUsersOnLineEventArgs(string Name, string ID, char Scenario)
        {
            _name = Name;
            _id = ID;
            _scenario = Scenario;
        }
    }
    public class ConnectingAndDisconnectingEventArgs : EventArgs
    {
        public string _name;
        public ConnectingAndDisconnectingEventArgs(string Name)
        {
            _name = Name;
        }
    }

    public class SpecialException : Exception
    {
        public char _option;
        public SpecialException(char Option)
        {
            _option = Option;
        }
    }

    public class UsersAndMessagesManager
    {
        private SqlServerDAL _dal = new SqlServerDAL();
        public bool CheckIfUserIdExistInDb(string UserID, string UserName)
        {
            List<User> Users = new List<User>();
            Users = (List<User>)_dal.GetAllUsersDetailsFromDB();
            foreach (var user in Users)
            {
                if (user.ID == UserID)
                {
                    return true;
                }
            }
            return false;
        }
        public char MatchingTheDetailsOfLogingUser(string UserID, string UserName, string Password)
        {
            List<User> Users = new List<User>();
            Users = (List<User>)_dal.GetAllUsersDetailsFromDB();
            foreach (var user in Users)
            {
                if (user.ID == UserID)
                {
                    if (user.Name == UserName)
                    {
                        if (user.Password == Password)
                        {
                            return 'c';
                        }
                        return 'b';
                    }
                    return 'a';
                }
            }
            return 'd';
        }
        public int InsertNewUserToDB(string UserID, string UserName, string Password)
        {
            return _dal.UpdateData(string.Format("INSERT INTO Users(UserID, UserName, PasswordString, LastConnectionDate) VALUES('{0}','{1}','{2}','{3}')", UserID, UserName, Password, DateTime.Now));
        }
        public int InsertNewMessageToDB(string UserID, string Text)
        {
            return _dal.UpdateData(string.Format("INSERT INTO Messages(UserId, MessageText, SentDate) VALUES('{0}','{1}', '{2}')", UserID, Text, DateTime.Now));
        }
        public int UpdateLastSeenWhenDisconnecting(string UserID)
        {
            string date = DateTime.Now.ToString();
            return _dal.UpdateData(string.Format("UPDATE Users SET LastConnectionDate = '{0}' WHERE UserID = '{1}'", DateTime.Now, UserID));
        }
        //All function below will use by serverUI
        public List<string> GetAllUsersIDFormDB()
        {
            IEnumerable<User> users = _dal.GetAllUsersDetailsFromDB();
            List<string> UsersID = new List<string>();
            foreach (var item in users)
            {
                UsersID.Add(item.ID);
            }
            return UsersID;
        }
        public List<string> GetAllUsersNameFormDB()
        {
            IEnumerable<User> users = _dal.GetAllUsersDetailsFromDB();
            List<string> UsersName = new List<string>();
            foreach (var item in users)
            {
                UsersName.Add(item.Name);
            }
            return UsersName;
        }
        public List<string> GetAllMessages()
        {
            IEnumerable<TextMessage> allText = _dal.GetTextMessagesFromDB();
            List<string> ListofTextToUI = new List<string>();
            foreach (var item in allText)
            {
                string packTextMessage = item.Message;
                string packUserId = item.SenderId;
                ListofTextToUI.Add(string.Format("'{0}' / Message sent from: {1}", packTextMessage, packUserId));
            }
            return ListofTextToUI;
        }
        public List<string> GetMatchedMessagesByDate(DateTime Date)
        {

            IEnumerable<TextMessage> allText = _dal.GetTextMessagesFromDB();
            List<string> filteredListOfMessages = new List<string>();
            foreach (var item in allText)
            {
                if (item.TimeOfSending.ToShortDateString() == Date.ToShortDateString())
                {
                    filteredListOfMessages.Add(string.Format("'{0}' / Message sent from: {1}", item.Message, item.SenderId));

                    //filteredListOfMessages.Add(item.Message + " / Message sent from: " + item.SenderId);
                }
            }
            return filteredListOfMessages;
        }
        public int DeleteUserFromDB(string UserID)
        {

            return _dal.UpdateData(string.Format("DELETE FROM Users WHERE UserId = '{0}'", UserID));
        }

        public List<string> GetFilteredMessages(string FilterWord)
        {
            IEnumerable<TextMessage> allText = _dal.GetTextMessagesFromDB();
            List<string> ListofFilteredTextToUI = new List<string>();
            foreach (var item in allText)
            {
                if (item.Message.Contains(FilterWord))
                {
                    string packTextMessage = item.Message;
                    string packUserId = item.SenderId;
                    ListofFilteredTextToUI.Add(string.Format("'{0}' / Message sent from: {1}", packTextMessage, packUserId));
                }
            }
            return ListofFilteredTextToUI;
        }
        public void DisconnectFromDB() { }
    }


}
