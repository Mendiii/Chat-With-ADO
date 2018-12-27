using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data.SqlClient;
using System.Data;

namespace CommonTypes
{

    [Serializable]
    public class MessageBase
    {
        public DateTime CurrentTime { get; set; }
        public User CurrenUser { get; set; }
        public TextMessage Text { get; set; }

        public DbOperationException op = new DbOperationException();

    }


    [Serializable]
    public class TextMessage : MessageBase
    {
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public Color Color { get; set; }
        public string Message { get; set; }
        public bool SpecialSignFromServer { get; set; }
        public bool removedByServer { get; set; }
        public DateTime TimeOfSending { get; set; }
    }

    [Serializable]
    public class ConnectionMessage : MessageBase
    { }

    [Serializable]
    public class DisconnectionMessage : MessageBase
    { }

    [Serializable]
    public class User
    {
        public string ID { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool IsNew { get; set; }

        public Stream Stream;
        public Color Color { get; set; }
        public bool IsRegistered { get; set; }
        public string LastConnectionDate { get; set; }


        // this property 'IsConnected' stand for disconnection this user immediately 
        // if server decide to delete this user.
        // **need to creatte an event that will catch change to false**
        public bool IsConnected { get; set; }



    }

    [Serializable]
    public class DbOperationException 
    {
        // 1. user not exist in db = and check box set to false. user must sign in first
        // 2. user trying to connect and details not match  check box set to false
        // 3. user trying to connect and details already exist in db check box set to true

        public bool UserNotExistInDBOrIncorrect { get; set; }//user try to log in --- 
        public bool NewUserIdlsAlreadyExistInDB { get; set; } // new user try to register --- return exception
        public char ErrorState { get; set; }
        //public bool NewUserIdMatchToOther { get; set; }
    }

}
