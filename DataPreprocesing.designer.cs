namespace Insider
{
    partial class DataPreprocess
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
            this.components = new System.ComponentModel.Container();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView5 = new System.Windows.Forms.DataGridView();
            this.UserID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Action_Freq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.No_Sites = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.No_Emails = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.No_Attachs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Class = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.idDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pcDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.activityDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.slnoDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.connectBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dbDataSet5 = new Insider.dbDataSet5();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pcDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.activityDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.slnoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loginBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dbDataSet4 = new Insider.dbDataSet4();
            this.pcDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.urlDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.httpBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dbDataSet2 = new Insider.dbDataSet2();
            this.activityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loginBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dbDataSet1 = new Insider.dbDataSet1();
            this.connectBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dbDataSet = new Insider.dbDataSet();
            this.emailBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dbDataSet3 = new Insider.dbDataSet3();
            this.connectTableAdapter = new Insider.dbDataSetTableAdapters.connectTableAdapter();
            this.loginTableAdapter = new Insider.dbDataSet1TableAdapters.loginTableAdapter();
            this.httpTableAdapter = new Insider.dbDataSet2TableAdapters.httpTableAdapter();
            this.emailTableAdapter = new Insider.dbDataSet3TableAdapters.EmailTableAdapter();
            this.loginTableAdapter1 = new Insider.dbDataSet4TableAdapters.loginTableAdapter();
            this.connectTableAdapter1 = new Insider.dbDataSet5TableAdapters.connectTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.connectBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbDataSet5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbDataSet4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.httpBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbDataSet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.connectBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emailBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbDataSet3)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView2
            // 
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.activityDataGridViewTextBoxColumn});
            this.dataGridView2.DataSource = this.loginBindingSource;
            this.dataGridView2.Location = new System.Drawing.Point(16, 325);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(320, 185);
            this.dataGridView2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Showcard Gothic", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(52, 44);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "Device Info";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Showcard Gothic", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(52, 299);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "Login Type";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Showcard Gothic", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(423, 300);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "ConnectInfo";
            // 
            // dataGridView4
            // 
            this.dataGridView4.AutoGenerateColumns = false;
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pcDataGridViewTextBoxColumn,
            this.urlDataGridViewTextBoxColumn});
            this.dataGridView4.DataSource = this.httpBindingSource;
            this.dataGridView4.Location = new System.Drawing.Point(412, 71);
            this.dataGridView4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.Size = new System.Drawing.Size(320, 185);
            this.dataGridView4.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Showcard Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(424, 45);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 18);
            this.label4.TabIndex = 7;
            this.label4.Text = "Http Types";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Mistral", 19.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Crimson;
            this.label5.Location = new System.Drawing.Point(1203, 9);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(238, 41);
            this.label5.TabIndex = 8;
            this.label5.Text = "Granularity Levels";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Showcard Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(877, 78);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 18);
            this.label6.TabIndex = 9;
            this.label6.Text = "Training Set";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Showcard Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(1016, 71);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 10;
            this.button1.Text = "Generate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView5
            // 
            this.dataGridView5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView5.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UserID,
            this.TotalTime,
            this.Action_Freq,
            this.No_Sites,
            this.No_Emails,
            this.No_Attachs,
            this.FileSize,
            this.Class});
            this.dataGridView5.Location = new System.Drawing.Point(795, 133);
            this.dataGridView5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView5.Name = "dataGridView5";
            this.dataGridView5.Size = new System.Drawing.Size(560, 342);
            this.dataGridView5.TabIndex = 11;
            // 
            // UserID
            // 
            this.UserID.HeaderText = "UserID";
            this.UserID.Name = "UserID";
            // 
            // TotalTime
            // 
            this.TotalTime.HeaderText = "TotalTime";
            this.TotalTime.Name = "TotalTime";
            // 
            // Action_Freq
            // 
            this.Action_Freq.HeaderText = "Action_Freq";
            this.Action_Freq.Name = "Action_Freq";
            // 
            // No_Sites
            // 
            this.No_Sites.HeaderText = "No_Sites";
            this.No_Sites.Name = "No_Sites";
            // 
            // No_Emails
            // 
            this.No_Emails.HeaderText = "No_Emails";
            this.No_Emails.Name = "No_Emails";
            // 
            // No_Attachs
            // 
            this.No_Attachs.HeaderText = "No_Attachs";
            this.No_Attachs.Name = "No_Attachs";
            // 
            // FileSize
            // 
            this.FileSize.HeaderText = "FileSize";
            this.FileSize.Name = "FileSize";
            // 
            // Class
            // 
            this.Class.HeaderText = "Class";
            this.Class.Name = "Class";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.dateDataGridViewTextBoxColumn,
            this.userDataGridViewTextBoxColumn,
            this.pcDataGridViewTextBoxColumn1,
            this.activityDataGridViewTextBoxColumn1,
            this.slnoDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.loginBindingSource1;
            this.dataGridView1.Location = new System.Drawing.Point(16, 71);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(320, 185);
            this.dataGridView1.TabIndex = 12;
            // 
            // dataGridView3
            // 
            this.dataGridView3.AutoGenerateColumns = false;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn1,
            this.dateDataGridViewTextBoxColumn1,
            this.userDataGridViewTextBoxColumn1,
            this.pcDataGridViewTextBoxColumn2,
            this.activityDataGridViewTextBoxColumn2,
            this.slnoDataGridViewTextBoxColumn1});
            this.dataGridView3.DataSource = this.connectBindingSource1;
            this.dataGridView3.Location = new System.Drawing.Point(412, 325);
            this.dataGridView3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.Size = new System.Drawing.Size(320, 185);
            this.dataGridView3.TabIndex = 13;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Showcard Gothic", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(1355, 511);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 28);
            this.button2.TabIndex = 14;
            this.button2.Text = "BACK";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // idDataGridViewTextBoxColumn1
            // 
            this.idDataGridViewTextBoxColumn1.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn1.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn1.Name = "idDataGridViewTextBoxColumn1";
            // 
            // dateDataGridViewTextBoxColumn1
            // 
            this.dateDataGridViewTextBoxColumn1.DataPropertyName = "date";
            this.dateDataGridViewTextBoxColumn1.HeaderText = "date";
            this.dateDataGridViewTextBoxColumn1.Name = "dateDataGridViewTextBoxColumn1";
            // 
            // userDataGridViewTextBoxColumn1
            // 
            this.userDataGridViewTextBoxColumn1.DataPropertyName = "user";
            this.userDataGridViewTextBoxColumn1.HeaderText = "user";
            this.userDataGridViewTextBoxColumn1.Name = "userDataGridViewTextBoxColumn1";
            // 
            // pcDataGridViewTextBoxColumn2
            // 
            this.pcDataGridViewTextBoxColumn2.DataPropertyName = "pc";
            this.pcDataGridViewTextBoxColumn2.HeaderText = "pc";
            this.pcDataGridViewTextBoxColumn2.Name = "pcDataGridViewTextBoxColumn2";
            // 
            // activityDataGridViewTextBoxColumn2
            // 
            this.activityDataGridViewTextBoxColumn2.DataPropertyName = "activity";
            this.activityDataGridViewTextBoxColumn2.HeaderText = "activity";
            this.activityDataGridViewTextBoxColumn2.Name = "activityDataGridViewTextBoxColumn2";
            // 
            // slnoDataGridViewTextBoxColumn1
            // 
            this.slnoDataGridViewTextBoxColumn1.DataPropertyName = "slno";
            this.slnoDataGridViewTextBoxColumn1.HeaderText = "slno";
            this.slnoDataGridViewTextBoxColumn1.Name = "slnoDataGridViewTextBoxColumn1";
            this.slnoDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // connectBindingSource1
            // 
            this.connectBindingSource1.DataMember = "connect";
            this.connectBindingSource1.DataSource = this.dbDataSet5;
            // 
            // dbDataSet5
            // 
            this.dbDataSet5.DataSetName = "dbDataSet5";
            this.dbDataSet5.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // dateDataGridViewTextBoxColumn
            // 
            this.dateDataGridViewTextBoxColumn.DataPropertyName = "date";
            this.dateDataGridViewTextBoxColumn.HeaderText = "date";
            this.dateDataGridViewTextBoxColumn.Name = "dateDataGridViewTextBoxColumn";
            // 
            // userDataGridViewTextBoxColumn
            // 
            this.userDataGridViewTextBoxColumn.DataPropertyName = "user";
            this.userDataGridViewTextBoxColumn.HeaderText = "user";
            this.userDataGridViewTextBoxColumn.Name = "userDataGridViewTextBoxColumn";
            // 
            // pcDataGridViewTextBoxColumn1
            // 
            this.pcDataGridViewTextBoxColumn1.DataPropertyName = "pc";
            this.pcDataGridViewTextBoxColumn1.HeaderText = "pc";
            this.pcDataGridViewTextBoxColumn1.Name = "pcDataGridViewTextBoxColumn1";
            // 
            // activityDataGridViewTextBoxColumn1
            // 
            this.activityDataGridViewTextBoxColumn1.DataPropertyName = "activity";
            this.activityDataGridViewTextBoxColumn1.HeaderText = "activity";
            this.activityDataGridViewTextBoxColumn1.Name = "activityDataGridViewTextBoxColumn1";
            // 
            // slnoDataGridViewTextBoxColumn
            // 
            this.slnoDataGridViewTextBoxColumn.DataPropertyName = "slno";
            this.slnoDataGridViewTextBoxColumn.HeaderText = "slno";
            this.slnoDataGridViewTextBoxColumn.Name = "slnoDataGridViewTextBoxColumn";
            this.slnoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // loginBindingSource1
            // 
            this.loginBindingSource1.DataMember = "login";
            this.loginBindingSource1.DataSource = this.dbDataSet4;
            // 
            // dbDataSet4
            // 
            this.dbDataSet4.DataSetName = "dbDataSet4";
            this.dbDataSet4.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // pcDataGridViewTextBoxColumn
            // 
            this.pcDataGridViewTextBoxColumn.DataPropertyName = "pc";
            this.pcDataGridViewTextBoxColumn.HeaderText = "pc";
            this.pcDataGridViewTextBoxColumn.Name = "pcDataGridViewTextBoxColumn";
            // 
            // urlDataGridViewTextBoxColumn
            // 
            this.urlDataGridViewTextBoxColumn.DataPropertyName = "url";
            this.urlDataGridViewTextBoxColumn.HeaderText = "url";
            this.urlDataGridViewTextBoxColumn.Name = "urlDataGridViewTextBoxColumn";
            // 
            // httpBindingSource
            // 
            this.httpBindingSource.DataMember = "http";
            this.httpBindingSource.DataSource = this.dbDataSet2;
            // 
            // dbDataSet2
            // 
            this.dbDataSet2.DataSetName = "dbDataSet2";
            this.dbDataSet2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // activityDataGridViewTextBoxColumn
            // 
            this.activityDataGridViewTextBoxColumn.DataPropertyName = "activity";
            this.activityDataGridViewTextBoxColumn.HeaderText = "activity";
            this.activityDataGridViewTextBoxColumn.Name = "activityDataGridViewTextBoxColumn";
            // 
            // loginBindingSource
            // 
            this.loginBindingSource.DataMember = "login";
            this.loginBindingSource.DataSource = this.dbDataSet1;
            // 
            // dbDataSet1
            // 
            this.dbDataSet1.DataSetName = "dbDataSet1";
            this.dbDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // connectBindingSource
            // 
            this.connectBindingSource.DataMember = "connect";
            this.connectBindingSource.DataSource = this.dbDataSet;
            // 
            // dbDataSet
            // 
            this.dbDataSet.DataSetName = "dbDataSet";
            this.dbDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // emailBindingSource
            // 
            this.emailBindingSource.DataMember = "Email";
            this.emailBindingSource.DataSource = this.dbDataSet3;
            this.emailBindingSource.CurrentChanged += new System.EventHandler(this.emailBindingSource_CurrentChanged);
            // 
            // dbDataSet3
            // 
            this.dbDataSet3.DataSetName = "dbDataSet3";
            this.dbDataSet3.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // connectTableAdapter
            // 
            this.connectTableAdapter.ClearBeforeFill = true;
            // 
            // loginTableAdapter
            // 
            this.loginTableAdapter.ClearBeforeFill = true;
            // 
            // httpTableAdapter
            // 
            this.httpTableAdapter.ClearBeforeFill = true;
            // 
            // emailTableAdapter
            // 
            this.emailTableAdapter.ClearBeforeFill = true;
            // 
            // loginTableAdapter1
            // 
            this.loginTableAdapter1.ClearBeforeFill = true;
            // 
            // connectTableAdapter1
            // 
            this.connectTableAdapter1.ClearBeforeFill = true;
            // 
            // DataPreprocess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.ClientSize = new System.Drawing.Size(1507, 554);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.dataGridView5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataGridView4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView2);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DataPreprocess";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.connectBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbDataSet5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbDataSet4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.httpBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbDataSet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.connectBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emailBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbDataSet3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView5;
        private dbDataSet dbDataSet;
        private System.Windows.Forms.BindingSource connectBindingSource;
        private dbDataSetTableAdapters.connectTableAdapter connectTableAdapter;
        private dbDataSet1 dbDataSet1;
        private System.Windows.Forms.BindingSource loginBindingSource;
        private dbDataSet1TableAdapters.loginTableAdapter loginTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn activityDataGridViewTextBoxColumn;
        private dbDataSet2 dbDataSet2;
        private System.Windows.Forms.BindingSource httpBindingSource;
        private dbDataSet2TableAdapters.httpTableAdapter httpTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn pcDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn urlDataGridViewTextBoxColumn;
        private dbDataSet3 dbDataSet3;
        private System.Windows.Forms.BindingSource emailBindingSource;
        private dbDataSet3TableAdapters.EmailTableAdapter emailTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Action_Freq;
        private System.Windows.Forms.DataGridViewTextBoxColumn No_Sites;
        private System.Windows.Forms.DataGridViewTextBoxColumn No_Emails;
        private System.Windows.Forms.DataGridViewTextBoxColumn No_Attachs;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn Class;
        private System.Windows.Forms.DataGridView dataGridView1;
        private dbDataSet4 dbDataSet4;
        private System.Windows.Forms.BindingSource loginBindingSource1;
        private dbDataSet4TableAdapters.loginTableAdapter loginTableAdapter1;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn userDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pcDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn activityDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn slnoDataGridViewTextBoxColumn;
        private dbDataSet5 dbDataSet5;
        private System.Windows.Forms.BindingSource connectBindingSource1;
        private dbDataSet5TableAdapters.connectTableAdapter connectTableAdapter1;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn userDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn pcDataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn activityDataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn slnoDataGridViewTextBoxColumn1;
        private System.Windows.Forms.Button button2;
    }
}