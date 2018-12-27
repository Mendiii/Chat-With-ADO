using System;
using System.Drawing;
using System.Windows.Forms;
using ClientBL;

namespace ClientUI
{
    public partial class ClientForm : Form
    {
        ChatClient Client = new ChatClient();

        Color color = new Color();
        public ClientForm()
        {
            InitializeComponent();
            Client.TextMessageRecieved += ShowIncomingMessage;
            Client.OnSetConnected += ConnectedChangeToFalse;
            btnDisconnect.Enabled = false;
            btnSendMessage.Enabled = false;
            txtMessage.Enabled = false;
            Client.OnMustThrowMessagesFromThread += DisplayErrorMessage;
           
        }

        private void DisplayErrorMessage(object sender, EventArgs e)
        {

            if (sender is string)
            {
                this.BeginInvoke((Action)(() => MessageBox.Show(sender.ToString())));
            }
        }

        void OnEnterPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (String.IsNullOrWhiteSpace(txtMessage.Text))
                {
                    return;
                }
                Client.SendMessage(this.txtMessage.Text);
                txtMessage.Clear();
                e.Handled = true;
                e.SuppressKeyPress = true;

            }
        }


        private void ConnectedChangeToFalse(object sender, EventArgs e)
        {
            ChatClient cc = sender as ChatClient;
            try
            {
                if (cc.Connected == false)
                {
                    if (btnDisconnect.InvokeRequired)
                    {
                        Invoke(new Action<object, EventArgs>(ConnectedChangeToFalse), sender, e);
                        return;
                    }
                    btnDisconnect.Enabled = false;

                    if (btnSendMessage.InvokeRequired)
                    {
                        Invoke(new Action<object, EventArgs>(ConnectedChangeToFalse), sender, e);
                        return;
                    }
                    btnSendMessage.Enabled = false;

                    if (txtMessage.InvokeRequired)
                    {
                        Invoke(new Action<object, EventArgs>(ConnectedChangeToFalse), sender, e);
                        return;
                    }
                    txtMessage.Clear();
                    txtMessage.Enabled = false;
                    chkboxNewUser.Checked = false;
                }
                /*****/
                else if (cc.Connected == true)
                {
                    if (btnDisconnect.InvokeRequired)
                    {
                        Invoke(new Action<object, EventArgs>(ConnectedChangeToFalse), sender, e);
                        return;
                    }
                    btnDisconnect.Enabled = true;

                    if (btnSendMessage.InvokeRequired)
                    {
                        Invoke(new Action<object, EventArgs>(ConnectedChangeToFalse), sender, e);
                        return;
                    }
                    btnSendMessage.Enabled = true;

                    if (txtMessage.InvokeRequired)
                    {
                        Invoke(new Action<object, EventArgs>(ConnectedChangeToFalse), sender, e);
                        return;
                    }
                    txtMessage.Enabled = true;
                }
            }
            catch (Exception)
            {

            }

        }
        private void ShowIncomingMessage(object sender, ChatClient.TextMessageRecievedEventArgs e)
        {
            ChatClient c = sender as ChatClient;
            if (richTextBoxIncomingMessage.InvokeRequired)
            {
                Invoke(new Action<object, ChatClient.TextMessageRecievedEventArgs>(ShowIncomingMessage), sender, e);
                return;
            }
            richTextBoxIncomingMessage.SelectionFont = new Font("Californian FB Bold", 9, FontStyle.Bold);
            string Timenow = DateTime.Now.ToString("HH:mm");
            richTextBoxIncomingMessage.AppendText(e.TextDetails.SenderName + ": ", e.TextDetails.Color);
            richTextBoxIncomingMessage.AppendText(e.TextDetails.Message + "\n", Color.Black);
            richTextBoxIncomingMessage.AppendText("sent at: " + Timenow);
            richTextBoxIncomingMessage.AppendText("__________________________________" + "\n");
            richTextBoxIncomingMessage.Focus();
            txtMessage.Focus();

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (Client.Connected == true)
            {
                MessageBox.Show("you are connected");
            }
            try
            {
                Client.Connect(txtPort.Text, txtIPAddress.Text, txtID.Text, txtName.Text, txtPassword.Text, color, chkboxNewUser.Checked);
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
                        MessageBox.Show("Both IP and Port required.");
                        break;
                    case 'E':
                        MessageBox.Show("your ID is required.");
                        break;
                    case 'F':
                        MessageBox.Show("your name is required.");
                        break;
                    case 'G':
                        MessageBox.Show("name has to contain only letters!");
                        break;
                    case 'H':
                        MessageBox.Show("Password text box not contain any value.");
                        break;
                    case 'I':
                        MessageBox.Show("Password contain invalid characters.");
                        break;
                    case 'J':
                        MessageBox.Show("Sorry. \n1. server is maybe turned off. \n2. input is incorrect. \nrecheck details and try again.");
                        break;
                    case 'K':
                        MessageBox.Show("Oops");
                        break;
                        //case 'L':
                        //    MessageBox.Show("User not exist in data base! or it's just incorrect input. check it again.");
                        //    break;
                }
            }

            //correct
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Client.ClosingClientWindowByX();
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMessage.Text))
            {
                return;
            }
            Client.SendMessage(txtMessage.Text);
            txtMessage.Clear();
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {

            if (Client.Connected == true)
            {
                try
                {
                    Client.Dissconnect();
                    //Client.Connected = false;
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            if (Client.Connected == false)
            {
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    btnColor.BackColor = color = colorDialog1.Color;
                }
            }
            else
            {
                MessageBox.Show("You can not choose the color now!");
            }

        }


    }
}

public static class RichTextBoxExtensions
{

    public static void AppendText(this RichTextBox box, string text, Color color)
    {

        box.SelectionStart = box.TextLength;
        box.SelectionLength = 0;

        box.SelectionColor = color;
        box.AppendText(text);
        box.SelectionColor = box.ForeColor;

    }

}



