using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using CommonTypes;
using System.Drawing;
using System.Runtime.Serialization;
using System.IO;
using System.Text.RegularExpressions;

namespace ClientBL
{
    public class ChatClient
    {

        public bool Connected { get; set; }
        TcpClient _tcpClient;
        NetworkStream _stream;
        public User _currentUser;
        int _port;
        IPAddress _ip;
        BinaryFormatter _bf = new BinaryFormatter();
        MessageBase _mb;
        Thread t;
        public event EventHandler<TextMessageRecievedEventArgs> TextMessageRecieved;
        public event EventHandler OnSetConnected;
        Regex regularEx = new Regex("^[a-zA-Z0-9]*$");
        public event EventHandler OnMustThrowMessagesFromThread;

        public void Connect(string Port, string IP, string Id, string Name, string Password, Color color, bool IsNewUser)
        {
            
            if (Connected)
            {
                return;
            }
            bool validPort = int.TryParse(Port, out _port);
            bool validIP = IPAddress.TryParse(IP, out _ip);
            bool validName = Name != null;
            bool EmptyPassword = string.IsNullOrWhiteSpace(Password);
            bool EmptyId = string.IsNullOrWhiteSpace(Id);



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

            if (validPort && validIP && validName)
            {

                if (EmptyId)
                {
                    throw new SpecialException('E');
                }
                if (String.IsNullOrWhiteSpace(Name))
                {
                    throw new SpecialException('F');
                }
                char[] arrOfTxtName = Name.ToCharArray();
                for (int i = 0; i < arrOfTxtName.Length; i++)
                {
                    if (!char.IsLetter(arrOfTxtName[i]))
                    {
                        throw new SpecialException('G');
                    }
                }
                if (EmptyPassword)
                {
                    throw new SpecialException('H');
                }
                else if (!regularEx.IsMatch(Password))
                {
                    throw new SpecialException('I');
                }

                try
                {
                    _currentUser = new User();
                    _currentUser.ID = Id;
                    _currentUser.Name = Name;
                    _currentUser.Password = Password;
                    _currentUser.Color = color;
                    _currentUser.IsNew = IsNewUser;
                    _currentUser.IsConnected = true;
                    _mb = new MessageBase() { CurrentTime = DateTime.Now, CurrenUser = _currentUser, };

                    _tcpClient = new TcpClient();
                    _tcpClient.Connect(_ip, _port);
                    _stream = _tcpClient.GetStream();
                    
                    t = new Thread(() =>
                    {

                        _bf.Serialize(_stream, _mb);

                        MessageBase m = (MessageBase)_bf.Deserialize(_stream);

                        if (m is MessageBase)
                        {
                            MessagesPack mp = new MessagesPack();
                            //***i must use an event. because its inside thread. when abort function will call it will wipe out all details
                            if (m.op.NewUserIdlsAlreadyExistInDB)
                            {
                                OnMustThrowMessagesFromThread(mp.message4, EventArgs.Empty);
                                t.Abort();
                                return;
                            }
                            if (m.op.ErrorState == 'd')
                            {
                                OnMustThrowMessagesFromThread(mp.message1, EventArgs.Empty);
                                t.Abort();
                                return;
                            }
                            if (m.op.ErrorState == 'a')
                            {
                                OnMustThrowMessagesFromThread(mp.message2, EventArgs.Empty);
                                t.Abort();
                                return;
                            }
                            if (m.op.ErrorState == 'b')
                            {
                                OnMustThrowMessagesFromThread(mp.message3, EventArgs.Empty);
                                t.Abort();
                                return;
                            }
                            Connected = true;
                            OnSetConnected(this, EventArgs.Empty);
                            this._currentUser.ID = (string)m.CurrenUser.ID;
                            StartRecieveMessages();
                        }
                    });
                    t.Start();
                }
               
               
                catch (SocketException)
                {
                    throw new SpecialException('J');
                }
                catch (ArgumentException)
                {
                    throw new ArgumentException();
                }
                catch (SpecialException)
                {

                }
                catch (Exception)
                {
                    //throw new Exception();
                }

            }

        }


        public void Dissconnect()
        {

            if (Connected)
            {
                try
                {
                    DisconnectionMessage _disconnect = new DisconnectionMessage() { CurrentTime = DateTime.Now, CurrenUser = _currentUser };
                    _disconnect.CurrentTime = DateTime.Now;
                    _bf.Serialize(_stream, _disconnect);
                    Connected = false;
                    OnSetConnected(this, EventArgs.Empty);
                    _stream.Close();
                    _tcpClient.Close();
                }
                catch (SerializationException)
                { }
                catch (IOException)
                { }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public void SendMessage(string message)
        {
            try
            {
                TextMessage txt = new TextMessage();
                txt.Message = message;
                txt.SenderId = this._currentUser.ID;
                txt.SenderName = this._currentUser.Name;
                txt.Color = this._currentUser.Color;
                _bf.Serialize(_stream, txt);
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void StartRecieveMessages()
        {
            new Thread(() => CatchMessage()).Start();
        }
        private void CatchMessage()
        {
            while (Connected)
            {
                try
                {
                    TextMessage text = new TextMessage();
                    BinaryFormatter bf = new BinaryFormatter();
                    text = (TextMessage)bf.Deserialize(_stream);
                    TextMessageRecieved(this, new TextMessageRecievedEventArgs(text.Message, text.SenderName, text.Color));
                    if ((text.Message == "server disconnect" && text.SpecialSignFromServer == true) ||
                        (text.Message == "Removed By Server" && text.SpecialSignFromServer == true)
                        )
                    {
                        this.Connected = false;
                        OnSetConnected(this, EventArgs.Empty);
                    }
                }
                catch (SerializationException)
                {

                }
                catch (ArgumentException)
                {

                }
                catch (IOException) { }
                catch (Exception)
                {
                    throw;
                }
                //finally
                //{
                //    _stream.Close();

                //    _tcpClient.Close();
                //}
            }
        }
        public void ClosingClientWindowByX()
        {
            Dissconnect();
        }
        public class TextMessageRecievedEventArgs : EventArgs
        {
            public TextMessage TextDetails = new TextMessage();
            public TextMessageRecievedEventArgs(string Message, string Name, Color color)
            {
                TextDetails.SenderName = Name;
                TextDetails.Message = Message;
                TextDetails.Color = color;
            }
        }

        public class MessagesPack 
        {
            public string message1 = "User not Exist in data base. \nConsider registration.";
            public string message2 = "incorrect Name";
            public string message3 = "incorrect Password";
            public string message4 = "registration failed! \nThis ID already taken. \nID must be different.";
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
}

