namespace ServerUI
{
    partial class ServerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisConnect = new System.Windows.Forms.Button();
            this.txtIPAddress = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControlInfo = new System.Windows.Forms.TabControl();
            this.tabCurrentUsers = new System.Windows.Forms.TabPage();
            this.listViewCurrentUsers = new System.Windows.Forms.ListView();
            this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabHistory = new System.Windows.Forms.TabPage();
            this.listViewHistory = new System.Windows.Forms.ListView();
            this.column2Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column2Action = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column2Time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblOnAndOff = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblCounter = new System.Windows.Forms.Label();
            this.btnDisplayUsersFromDb = new System.Windows.Forms.Button();
            this.btnSearchMessage = new System.Windows.Forms.Button();
            this.btnDeleteUser = new System.Windows.Forms.Button();
            this.lstBoxOfUsersFromDB = new System.Windows.Forms.ListBox();
            this.lstBoxOfMessagesFromDB = new System.Windows.Forms.ListBox();
            this.txtSearchUserByID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSearchUserByName = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblDatePicker = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSearchForMessage = new System.Windows.Forms.TextBox();
            this.tabControlInfo.SuspendLayout();
            this.tabCurrentUsers.SuspendLayout();
            this.tabHistory.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnConnect.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnConnect.Location = new System.Drawing.Point(4, 59);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(243, 23);
            this.btnConnect.TabIndex = 3;
            this.btnConnect.Text = "connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisConnect
            // 
            this.btnDisConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnDisConnect.Location = new System.Drawing.Point(4, 88);
            this.btnDisConnect.Name = "btnDisConnect";
            this.btnDisConnect.Size = new System.Drawing.Size(243, 23);
            this.btnDisConnect.TabIndex = 4;
            this.btnDisConnect.Text = "disconnect";
            this.btnDisConnect.UseVisualStyleBackColor = true;
            this.btnDisConnect.Click += new System.EventHandler(this.btnDisConnect_Click);
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.Location = new System.Drawing.Point(65, 7);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Size = new System.Drawing.Size(182, 20);
            this.txtIPAddress.TabIndex = 1;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(65, 33);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(182, 20);
            this.txtPort.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ip";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(6, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "port";
            // 
            // tabControlInfo
            // 
            this.tabControlInfo.Controls.Add(this.tabCurrentUsers);
            this.tabControlInfo.Controls.Add(this.tabHistory);
            this.tabControlInfo.Dock = System.Windows.Forms.DockStyle.Right;
            this.tabControlInfo.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.tabControlInfo.Location = new System.Drawing.Point(254, 0);
            this.tabControlInfo.Name = "tabControlInfo";
            this.tabControlInfo.SelectedIndex = 0;
            this.tabControlInfo.Size = new System.Drawing.Size(352, 567);
            this.tabControlInfo.TabIndex = 0;
            // 
            // tabCurrentUsers
            // 
            this.tabCurrentUsers.BackColor = System.Drawing.SystemColors.Window;
            this.tabCurrentUsers.Controls.Add(this.listViewCurrentUsers);
            this.tabCurrentUsers.Location = new System.Drawing.Point(4, 22);
            this.tabCurrentUsers.Name = "tabCurrentUsers";
            this.tabCurrentUsers.Padding = new System.Windows.Forms.Padding(3);
            this.tabCurrentUsers.Size = new System.Drawing.Size(344, 541);
            this.tabCurrentUsers.TabIndex = 2;
            this.tabCurrentUsers.Text = "On-line Users";
            // 
            // listViewCurrentUsers
            // 
            this.listViewCurrentUsers.BackColor = System.Drawing.SystemColors.Window;
            this.listViewCurrentUsers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnName,
            this.columnStatus,
            this.columnTime});
            this.listViewCurrentUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewCurrentUsers.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.listViewCurrentUsers.Location = new System.Drawing.Point(3, 3);
            this.listViewCurrentUsers.Name = "listViewCurrentUsers";
            this.listViewCurrentUsers.Size = new System.Drawing.Size(338, 535);
            this.listViewCurrentUsers.TabIndex = 0;
            this.listViewCurrentUsers.UseCompatibleStateImageBehavior = false;
            this.listViewCurrentUsers.View = System.Windows.Forms.View.Details;
            // 
            // columnName
            // 
            this.columnName.Text = "Name";
            this.columnName.Width = 98;
            // 
            // columnStatus
            // 
            this.columnStatus.Text = "Status";
            this.columnStatus.Width = 116;
            // 
            // columnTime
            // 
            this.columnTime.Text = "Time";
            this.columnTime.Width = 75;
            // 
            // tabHistory
            // 
            this.tabHistory.Controls.Add(this.listViewHistory);
            this.tabHistory.Location = new System.Drawing.Point(4, 22);
            this.tabHistory.Name = "tabHistory";
            this.tabHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tabHistory.Size = new System.Drawing.Size(344, 541);
            this.tabHistory.TabIndex = 1;
            this.tabHistory.Text = "History";
            this.tabHistory.UseVisualStyleBackColor = true;
            // 
            // listViewHistory
            // 
            this.listViewHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.column2Name,
            this.column2Action,
            this.column2Time});
            this.listViewHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewHistory.Location = new System.Drawing.Point(3, 3);
            this.listViewHistory.Name = "listViewHistory";
            this.listViewHistory.Size = new System.Drawing.Size(338, 535);
            this.listViewHistory.TabIndex = 0;
            this.listViewHistory.UseCompatibleStateImageBehavior = false;
            this.listViewHistory.View = System.Windows.Forms.View.Details;
            // 
            // column2Name
            // 
            this.column2Name.Text = "Name";
            this.column2Name.Width = 98;
            // 
            // column2Action
            // 
            this.column2Action.Text = "Action";
            this.column2Action.Width = 117;
            // 
            // column2Time
            // 
            this.column2Time.Text = "Time";
            this.column2Time.Width = 78;
            // 
            // lblOnAndOff
            // 
            this.lblOnAndOff.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblOnAndOff.ForeColor = System.Drawing.Color.Gray;
            this.lblOnAndOff.Location = new System.Drawing.Point(1, 544);
            this.lblOnAndOff.Name = "lblOnAndOff";
            this.lblOnAndOff.Size = new System.Drawing.Size(87, 22);
            this.lblOnAndOff.TabIndex = 0;
            this.lblOnAndOff.Text = "Disconnected";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.Location = new System.Drawing.Point(1, 555);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Users:";
            // 
            // lblCounter
            // 
            this.lblCounter.AutoSize = true;
            this.lblCounter.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblCounter.Location = new System.Drawing.Point(43, 555);
            this.lblCounter.Name = "lblCounter";
            this.lblCounter.Size = new System.Drawing.Size(13, 13);
            this.lblCounter.TabIndex = 6;
            this.lblCounter.Text = "0";
            this.lblCounter.Click += new System.EventHandler(this.lblCounter_Click);
            // 
            // btnDisplayUsersFromDb
            // 
            this.btnDisplayUsersFromDb.Location = new System.Drawing.Point(6, 155);
            this.btnDisplayUsersFromDb.Name = "btnDisplayUsersFromDb";
            this.btnDisplayUsersFromDb.Size = new System.Drawing.Size(231, 22);
            this.btnDisplayUsersFromDb.TabIndex = 7;
            this.btnDisplayUsersFromDb.Text = "Display Users";
            this.btnDisplayUsersFromDb.UseVisualStyleBackColor = true;
            this.btnDisplayUsersFromDb.Click += new System.EventHandler(this.btnDisplayUsersFromDb_Click);
            // 
            // btnSearchMessage
            // 
            this.btnSearchMessage.Location = new System.Drawing.Point(6, 156);
            this.btnSearchMessage.Name = "btnSearchMessage";
            this.btnSearchMessage.Size = new System.Drawing.Size(231, 22);
            this.btnSearchMessage.TabIndex = 8;
            this.btnSearchMessage.Text = "Display Messages";
            this.btnSearchMessage.UseVisualStyleBackColor = true;
            this.btnSearchMessage.Click += new System.EventHandler(this.btnSearchMessage_Click);
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.Location = new System.Drawing.Point(6, 183);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new System.Drawing.Size(231, 22);
            this.btnDeleteUser.TabIndex = 9;
            this.btnDeleteUser.Text = "Delete User";
            this.btnDeleteUser.UseVisualStyleBackColor = true;
            this.btnDeleteUser.Click += new System.EventHandler(this.btnDeleteUser_Click);
            // 
            // lstBoxOfUsersFromDB
            // 
            this.lstBoxOfUsersFromDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lstBoxOfUsersFromDB.FormattingEnabled = true;
            this.lstBoxOfUsersFromDB.Location = new System.Drawing.Point(6, 77);
            this.lstBoxOfUsersFromDB.Name = "lstBoxOfUsersFromDB";
            this.lstBoxOfUsersFromDB.Size = new System.Drawing.Size(231, 69);
            this.lstBoxOfUsersFromDB.TabIndex = 10;
            // 
            // lstBoxOfMessagesFromDB
            // 
            this.lstBoxOfMessagesFromDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lstBoxOfMessagesFromDB.FormattingEnabled = true;
            this.lstBoxOfMessagesFromDB.HorizontalScrollbar = true;
            this.lstBoxOfMessagesFromDB.Location = new System.Drawing.Point(6, 81);
            this.lstBoxOfMessagesFromDB.Name = "lstBoxOfMessagesFromDB";
            this.lstBoxOfMessagesFromDB.Size = new System.Drawing.Size(231, 69);
            this.lstBoxOfMessagesFromDB.TabIndex = 11;
            // 
            // txtSearchUserByID
            // 
            this.txtSearchUserByID.Location = new System.Drawing.Point(110, 25);
            this.txtSearchUserByID.Name = "txtSearchUserByID";
            this.txtSearchUserByID.Size = new System.Drawing.Size(127, 20);
            this.txtSearchUserByID.TabIndex = 12;
            this.txtSearchUserByID.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearchUserByID_KeyUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "search by ID:";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Window;
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtSearchUserByName);
            this.groupBox1.Controls.Add(this.lstBoxOfUsersFromDB);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnDisplayUsersFromDb);
            this.groupBox1.Controls.Add(this.txtSearchUserByID);
            this.groupBox1.Controls.Add(this.btnDeleteUser);
            this.groupBox1.Location = new System.Drawing.Point(4, 121);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(243, 218);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Users";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(2, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "search by name:";
            // 
            // txtSearchUserByName
            // 
            this.txtSearchUserByName.Location = new System.Drawing.Point(110, 51);
            this.txtSearchUserByName.Name = "txtSearchUserByName";
            this.txtSearchUserByName.Size = new System.Drawing.Size(127, 20);
            this.txtSearchUserByName.TabIndex = 14;
            this.txtSearchUserByName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearchUserByName_KeyUp);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Window;
            this.groupBox2.Controls.Add(this.lblDatePicker);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.dateTimePicker1);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.lstBoxOfMessagesFromDB);
            this.groupBox2.Controls.Add(this.txtSearchForMessage);
            this.groupBox2.Controls.Add(this.btnSearchMessage);
            this.groupBox2.Location = new System.Drawing.Point(4, 354);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(243, 187);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Messages";
            // 
            // lblDatePicker
            // 
            this.lblDatePicker.Location = new System.Drawing.Point(133, 55);
            this.lblDatePicker.Name = "lblDatePicker";
            this.lblDatePicker.Size = new System.Drawing.Size(104, 15);
            this.lblDatePicker.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(2, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "search by date:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "DD-MM-YY";
            this.dateTimePicker1.Location = new System.Drawing.Point(110, 51);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(17, 20);
            this.dateTimePicker1.TabIndex = 16;
            this.dateTimePicker1.Value = new System.DateTime(2017, 10, 8, 0, 0, 0, 0);
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "search by key:";
            // 
            // txtSearchForMessage
            // 
            this.txtSearchForMessage.Location = new System.Drawing.Point(110, 25);
            this.txtSearchForMessage.Name = "txtSearchForMessage";
            this.txtSearchForMessage.Size = new System.Drawing.Size(127, 20);
            this.txtSearchForMessage.TabIndex = 14;
            this.txtSearchForMessage.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearchForMessage_KeyUp);
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(606, 567);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblCounter);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblOnAndOff);
            this.Controls.Add(this.tabControlInfo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtIPAddress);
            this.Controls.Add(this.btnDisConnect);
            this.Controls.Add(this.btnConnect);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.MaximizeBox = false;
            this.Name = "ServerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Server Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerForm_FormClosing);
            this.tabControlInfo.ResumeLayout(false);
            this.tabCurrentUsers.ResumeLayout(false);
            this.tabHistory.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisConnect;
        private System.Windows.Forms.TextBox txtIPAddress;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControlInfo;
        private System.Windows.Forms.TabPage tabHistory;
        private System.Windows.Forms.TabPage tabCurrentUsers;
        private System.Windows.Forms.Label lblOnAndOff;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblCounter;
        private System.Windows.Forms.ListView listViewCurrentUsers;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ColumnHeader columnStatus;
        private System.Windows.Forms.ColumnHeader columnTime;
        private System.Windows.Forms.ListView listViewHistory;
        private System.Windows.Forms.ColumnHeader column2Name;
        private System.Windows.Forms.ColumnHeader column2Action;
        private System.Windows.Forms.ColumnHeader column2Time;
        private System.Windows.Forms.Button btnDisplayUsersFromDb;
        private System.Windows.Forms.Button btnSearchMessage;
        private System.Windows.Forms.Button btnDeleteUser;
        private System.Windows.Forms.ListBox lstBoxOfUsersFromDB;
        private System.Windows.Forms.ListBox lstBoxOfMessagesFromDB;
        private System.Windows.Forms.TextBox txtSearchUserByID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSearchForMessage;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSearchUserByName;
        private System.Windows.Forms.Label lblDatePicker;
    }
}

