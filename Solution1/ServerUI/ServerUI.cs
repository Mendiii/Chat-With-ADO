using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using ServerBL;

namespace ServerUI
{

    public partial class ServerForm : Form
    {
        BL Server = new BL();
        public event EventHandler OnChangeOnAndOffLabel;
        public ServerForm()
        {
            InitializeComponent();
            //Server.GetNewUserInfo += OnNewUserInfo;
            Server.ClientAccepted += OnClientConnect;
            Server.ClientDisconnected += OnClientDisconnect;
            Server.CurrentClientOnLine += OnCurrentOnLineClient;
            Server.ClientCounter += lblCounter_Click;
            OnChangeOnAndOffLabel += ChangeOnAndOffLabel;
        }


        private void OnCurrentOnLineClient(object sender, CurrentUsersOnLineEventArgs e)
        {

            if (this.listViewCurrentUsers.InvokeRequired)
            {
                Invoke(new Action<object, CurrentUsersOnLineEventArgs>(OnCurrentOnLineClient), sender, e);
                return;
            }
            listViewCurrentUsers.Items.Clear();
            switch (e._scenario)
            {
                case 'A':
                    Server.OnLine.Add(e);
                    for (int i = 0; i < 1; i++)
                    {
                        for (int a = 0; a < Server.OnLine.Count; a++)
                        {
                            string[] arrDetails = new string[3] { Server.OnLine[a]._name, "On Line", DateTime.Now.ToShortTimeString() };
                            ListViewItem packToListView = new ListViewItem(arrDetails);
                            listViewCurrentUsers.Items.Add(packToListView).ForeColor = Color.Green;
                        }
                    }
                    break;
                case 'B':
                    for (int i = 0; i < Server.OnLine.Count; i++)
                    {
                        if (Server.OnLine[i]._id == e._id)
                        {
                            Server.OnLine.RemoveAt(i);
                        }
                    }
                    for (int i = 0; i < 1; i++)
                    {
                        for (int a = 0; a < Server.OnLine.Count; a++)
                        {
                            string[] arrDetails = new string[3] { Server.OnLine[a]._name, "On Line", DateTime.Now.ToShortTimeString() };
                            ListViewItem packToListView = new ListViewItem(arrDetails);
                            listViewCurrentUsers.Items.Add(packToListView).ForeColor = Color.Green;
                        }
                    }
                    break;
                case 'C':
                    Server.OnLine.Clear();
                    break;
            }
        }
        private void OnClientDisconnect(object sender, ConnectingAndDisconnectingEventArgs e)
        {
            if (this.listViewHistory.InvokeRequired)
            {
                Invoke(new Action<object, ConnectingAndDisconnectingEventArgs>(OnClientDisconnect), sender, e);
                return;
            }
            string[] arrDetails = new string[3] { e._name, "Disconnected", DateTime.Now.ToShortTimeString() };
            ListViewItem packToListView = new ListViewItem(arrDetails);
            listViewHistory.Items.Add(packToListView);

        }
        public void OnClientConnect(object sender, ConnectingAndDisconnectingEventArgs e)
        {
            if (this.listViewHistory.InvokeRequired)
            {
                Invoke(new Action<object, ConnectingAndDisconnectingEventArgs>(OnClientConnect), sender, e);
                return;
            }
            string[] arrDetails = new string[3] { e._name, "Connected", DateTime.Now.ToShortTimeString() };
            ListViewItem packToListView = new ListViewItem(arrDetails);
            listViewHistory.Items.Add(packToListView);
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (Server.Connected == true)
                {
                    MessageBox.Show("you already connected");
                }
                else
                {
                    Server.ConnectAndStartAcceptClients(txtIPAddress.Text, txtPort.Text);
                    OnChangeOnAndOffLabel(Server, EventArgs.Empty);
                }
            }
            catch (SpecialException se)
            {
                switch (se._option)
                {
                    case 'A':
                        MessageBox.Show("IP is required.");
                        break;
                    case 'B':
                        MessageBox.Show("Port is required.");
                        break;
                    case 'C':
                        MessageBox.Show("Both IP and Port required");
                        break;
                    case 'D':
                        MessageBox.Show("Port digits are bigger than 65534.");
                        break;
                    case 'E':
                        MessageBox.Show("No connection. wrong input information, recheck, and type again.");
                        break;
                }
            }
            catch (Exception)
            {

            }

        }
        private void btnDisConnect_Click(object sender, EventArgs e)
        {
            try
            {
                Server.DisconnectServer();
                OnChangeOnAndOffLabel(Server, EventArgs.Empty);
            }
            catch (Exception)
            {
                
            }
        }
        private void lblCounter_Click(object sender, EventArgs e)
        {
            if (lblCounter.InvokeRequired)
            {
                Invoke(new Action<object, EventArgs>(lblCounter_Click), sender, e);
                return;
            }
            BL s = sender as BL;
            lblCounter.Text = s.Counter.ToString();
        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Server.DisconnectServer();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ChangeOnAndOffLabel(object sender, EventArgs e)
        {
            BL s = sender as BL;
            if (lblOnAndOff.InvokeRequired)
            {
                Invoke(new Action<object, EventArgs>(ChangeOnAndOffLabel), sender, e);
                return;
            }
            if (s.Connected == true)
            {
                lblOnAndOff.ForeColor = Color.LimeGreen;
                lblOnAndOff.Text = "Connected";
            }
            else if (s.Connected == false)
            {
                lblOnAndOff.ForeColor = Color.Gray;
                lblOnAndOff.Text = "Disconnected";
            }

        }


        private void btnDisplayUsersFromDb_Click(object sender, EventArgs e)
        {
            try
            {
                lstBoxOfUsersFromDB.Items.Clear();
                List<string> allUsers = Server.ManagerOfDb.GetAllUsersIDFormDB();
                foreach (var item in allUsers)
                {
                    lstBoxOfUsersFromDB.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void txtSearchUserByID_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSearchUserByID.Text))
                {
                    lstBoxOfUsersFromDB.Items.Clear();
                    return;
                }
                lstBoxOfUsersFromDB.Items.Clear();
                txtSearchUserByName.Clear();
                string SearchString = txtSearchUserByID.Text;
                List<string> allUsers = Server.ManagerOfDb.GetAllUsersIDFormDB();
                foreach (var item in allUsers)
                {
                    if (item.Contains(SearchString))
                    {
                        lstBoxOfUsersFromDB.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }


        private void btnSearchMessage_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearchForMessage.Clear();
                lblDatePicker.Text = "";
                lstBoxOfMessagesFromDB.Items.Clear();
                List<string> allMessages = Server.ManagerOfDb.GetAllMessages();
                foreach (var item in allMessages)
                {
                    lstBoxOfMessagesFromDB.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtSearchUserByName_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSearchUserByName.Text))
                {
                    lstBoxOfUsersFromDB.Items.Clear();
                    return;
                }
                lstBoxOfUsersFromDB.Items.Clear();
                txtSearchUserByID.Clear();
                string SearchString = txtSearchUserByName.Text;
                List<string> allUsers = Server.ManagerOfDb.GetAllUsersNameFormDB();
                foreach (var item in allUsers)
                {
                    if (item.Contains(SearchString))
                    {
                        lstBoxOfUsersFromDB.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dateTimePicker1.Value > DateTime.Today)
                {
                    lstBoxOfMessagesFromDB.Items.Clear();
                    lstBoxOfMessagesFromDB.Items.Add("The day you picked greater than today!");
                    return;
                }
                txtSearchForMessage.Clear();
                lstBoxOfMessagesFromDB.Items.Clear();

                int year = dateTimePicker1.Value.Year;
                int month = dateTimePicker1.Value.Month;
                int day = dateTimePicker1.Value.Day;
                lblDatePicker.Text = string.Format("{0}-{1}-{2}", day, month, year);

                List<string> messagesList = Server.ManagerOfDb.GetMatchedMessagesByDate(dateTimePicker1.Value);
                foreach (var item in messagesList)
                {
                    lstBoxOfMessagesFromDB.Items.Add(item);
                }
                if (messagesList.Count == 0)
                {
                    lstBoxOfMessagesFromDB.Items.Add("No match found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstBoxOfUsersFromDB.SelectedItem != null)
                {
                    DialogResult result = MessageBox.Show("You are now deleting a user from the database permanently. \nAre you sure about that?", "", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        Server.ManagerOfDb.DeleteUserFromDB(lstBoxOfUsersFromDB.SelectedItem.ToString());

                        if (Server._clientList == null)
                        {
                            return;
                        }
                        int originalCount = Server._clientList.Count;
                        foreach (var item in Server._clientList)
                        {

                            if (item.ID == lstBoxOfUsersFromDB.SelectedItem.ToString())
                            {
                                if (item.IsConnected)
                                {
                                    Server.DeletingUserReaction(lstBoxOfUsersFromDB.SelectedItem.ToString());
                                }
                                else
                                {
                                    Server._clientList.Remove(item);
                                }
                            }
                            if (originalCount != Server._clientList.Count)
                            {
                                return;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("In order to delete you need  to select user then click 'Delete User'.'");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void txtSearchForMessage_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSearchForMessage.Text))
                {
                    lstBoxOfMessagesFromDB.Items.Clear();
                    return;
                }
                lblDatePicker.Text = "";
                lstBoxOfMessagesFromDB.Items.Clear();
                List<string> filteredList = Server.ManagerOfDb.GetFilteredMessages(txtSearchForMessage.Text);
                foreach (var item in filteredList)
                {
                    lstBoxOfMessagesFromDB.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }

}
