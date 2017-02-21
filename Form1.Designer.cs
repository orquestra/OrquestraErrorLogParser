namespace OrquestraErrorLogParser
{
    partial class Form1
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
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnParse = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbDes = new System.Windows.Forms.RadioButton();
            this.rbAES = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEncryptionKey = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDetails = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFolder
            // 
            this.txtFolder.Location = new System.Drawing.Point(6, 90);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(437, 20);
            this.txtFolder.TabIndex = 0;
            this.txtFolder.TextChanged += new System.EventHandler(this.txtFolder_TextChanged);
            this.txtFolder.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFolder_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Pasta com arquivos";
            // 
            // btnParse
            // 
            this.btnParse.Location = new System.Drawing.Point(181, 203);
            this.btnParse.Name = "btnParse";
            this.btnParse.Size = new System.Drawing.Size(89, 23);
            this.btnParse.TabIndex = 4;
            this.btnParse.Text = "Processar Logs";
            this.btnParse.UseVisualStyleBackColor = true;
            this.btnParse.Click += new System.EventHandler(this.btnParse_Click);
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(276, 208);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(38, 13);
            this.lblResult.TabIndex = 3;
            this.lblResult.Text = "Pronto";
            this.lblResult.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(10, 474);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(450, 23);
            this.progressBar1.TabIndex = 4;
            this.progressBar1.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.rbDes);
            this.groupBox1.Controls.Add(this.rbAES);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtEncryptionKey);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtDetails);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtFolder);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnParse);
            this.groupBox1.Controls.Add(this.lblResult);
            this.groupBox1.Location = new System.Drawing.Point(10, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(449, 458);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // rbDes
            // 
            this.rbDes.AutoSize = true;
            this.rbDes.Location = new System.Drawing.Point(109, 134);
            this.rbDes.Name = "rbDes";
            this.rbDes.Size = new System.Drawing.Size(47, 17);
            this.rbDes.TabIndex = 2;
            this.rbDes.Text = "DES";
            this.rbDes.UseVisualStyleBackColor = true;
            this.rbDes.CheckedChanged += new System.EventHandler(this.rbDes_CheckedChanged);
            // 
            // rbAES
            // 
            this.rbAES.AutoSize = true;
            this.rbAES.Checked = true;
            this.rbAES.Location = new System.Drawing.Point(10, 134);
            this.rbAES.Name = "rbAES";
            this.rbAES.Size = new System.Drawing.Size(93, 17);
            this.rbAES.TabIndex = 1;
            this.rbAES.TabStop = true;
            this.rbAES.Text = "AES (Rijndael)";
            this.rbAES.UseVisualStyleBackColor = true;
            this.rbAES.CheckedChanged += new System.EventHandler(this.rbAES_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Tipo de criptografia";
            // 
            // txtEncryptionKey
            // 
            this.txtEncryptionKey.Location = new System.Drawing.Point(5, 173);
            this.txtEncryptionKey.Name = "txtEncryptionKey";
            this.txtEncryptionKey.Size = new System.Drawing.Size(437, 20);
            this.txtEncryptionKey.TabIndex = 3;
            this.txtEncryptionKey.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEncryptionKey_KeyUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Chave de criptografia";
            // 
            // txtDetails
            // 
            this.txtDetails.Location = new System.Drawing.Point(6, 232);
            this.txtDetails.Multiline = true;
            this.txtDetails.Name = "txtDetails";
            this.txtDetails.ReadOnly = true;
            this.txtDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDetails.Size = new System.Drawing.Size(437, 220);
            this.txtDetails.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(380, 39);
            this.label2.TabIndex = 4;
            this.label2.Text = "Transformação de arquivos de log do Orquestra BPM em arquivos CSV.\r\nSelecione a p" +
    "asta onde os arquivos estão localizados e clique em \"Processar\".\r\nUm arquivo CSV" +
    " será gerado na pasta selecionada.";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(367, 203);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 509);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Orquestra Log Parser";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnParse;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDetails;
        private System.Windows.Forms.TextBox txtEncryptionKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbDes;
        private System.Windows.Forms.RadioButton rbAES;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btnCancel;
    }
}

