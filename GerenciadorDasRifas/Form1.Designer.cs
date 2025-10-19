namespace GerenciadorDasRifas
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lbPendings = new ListBox();
            groupBox1 = new GroupBox();
            lblphone = new Label();
            lblemail = new Label();
            lbldata = new Label();
            lbltotal = new Label();
            lblNome = new Label();
            button1 = new Button();
            img = new PictureBox();
            lblRifas = new ListBox();
            groupBox2 = new GroupBox();
            btnrevogar = new Button();
            btnvendido = new Button();
            lblstatus = new Label();
            lblrifa = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)img).BeginInit();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // lbPendings
            // 
            lbPendings.FormattingEnabled = true;
            lbPendings.ItemHeight = 15;
            lbPendings.Location = new Point(12, 12);
            lbPendings.Name = "lbPendings";
            lbPendings.Size = new Size(281, 379);
            lbPendings.TabIndex = 0;
            lbPendings.SelectedIndexChanged += lbPendings_SelectedIndexChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lblphone);
            groupBox1.Controls.Add(lblemail);
            groupBox1.Controls.Add(lbldata);
            groupBox1.Controls.Add(lbltotal);
            groupBox1.Controls.Add(lblNome);
            groupBox1.Location = new Point(315, 14);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(225, 216);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Informações do comprador";
            // 
            // lblphone
            // 
            lblphone.AutoSize = true;
            lblphone.Location = new Point(22, 82);
            lblphone.Name = "lblphone";
            lblphone.Size = new Size(41, 15);
            lblphone.TabIndex = 4;
            lblphone.Text = "Phone";
            lblphone.Click += label5_Click;
            // 
            // lblemail
            // 
            lblemail.AutoSize = true;
            lblemail.Location = new Point(22, 56);
            lblemail.Name = "lblemail";
            lblemail.Size = new Size(36, 15);
            lblemail.TabIndex = 3;
            lblemail.Text = "Email";
            // 
            // lbldata
            // 
            lbldata.AutoSize = true;
            lbldata.Location = new Point(22, 188);
            lbldata.Name = "lbldata";
            lbldata.Size = new Size(31, 15);
            lbldata.TabIndex = 2;
            lbldata.Text = "Data";
            // 
            // lbltotal
            // 
            lbltotal.AutoSize = true;
            lbltotal.Location = new Point(22, 163);
            lbltotal.Name = "lbltotal";
            lbltotal.Size = new Size(32, 15);
            lbltotal.TabIndex = 1;
            lbltotal.Text = "Total";
            // 
            // lblNome
            // 
            lblNome.AutoSize = true;
            lblNome.Location = new Point(22, 30);
            lblNome.Name = "lblNome";
            lblNome.Size = new Size(40, 15);
            lblNome.TabIndex = 0;
            lblNome.Text = "Nome";
            // 
            // button1
            // 
            button1.Location = new Point(12, 398);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 3;
            button1.Text = "Atualizar lista";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // img
            // 
            img.Location = new Point(546, 14);
            img.Name = "img";
            img.Size = new Size(231, 216);
            img.TabIndex = 4;
            img.TabStop = false;
            // 
            // lblRifas
            // 
            lblRifas.FormattingEnabled = true;
            lblRifas.ItemHeight = 15;
            lblRifas.Location = new Point(315, 236);
            lblRifas.Name = "lblRifas";
            lblRifas.Size = new Size(225, 154);
            lblRifas.TabIndex = 5;
            lblRifas.SelectedIndexChanged += lblRifas_SelectedIndexChanged;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnrevogar);
            groupBox2.Controls.Add(btnvendido);
            groupBox2.Controls.Add(lblstatus);
            groupBox2.Controls.Add(lblrifa);
            groupBox2.Location = new Point(546, 236);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(231, 155);
            groupBox2.TabIndex = 5;
            groupBox2.TabStop = false;
            groupBox2.Text = "Rifa";
            // 
            // btnrevogar
            // 
            btnrevogar.Location = new Point(69, 126);
            btnrevogar.Name = "btnrevogar";
            btnrevogar.Size = new Size(75, 23);
            btnrevogar.TabIndex = 5;
            btnrevogar.Text = "Revogar";
            btnrevogar.UseVisualStyleBackColor = true;
            // 
            // btnvendido
            // 
            btnvendido.Location = new Point(150, 126);
            btnvendido.Name = "btnvendido";
            btnvendido.Size = new Size(75, 23);
            btnvendido.TabIndex = 4;
            btnvendido.Text = "Vendido";
            btnvendido.UseVisualStyleBackColor = true;
            btnvendido.Click += btnvendido_Click;
            // 
            // lblstatus
            // 
            lblstatus.AutoSize = true;
            lblstatus.Location = new Point(22, 56);
            lblstatus.Name = "lblstatus";
            lblstatus.Size = new Size(36, 15);
            lblstatus.TabIndex = 3;
            lblstatus.Text = "Email";
            // 
            // lblrifa
            // 
            lblrifa.AutoSize = true;
            lblrifa.Location = new Point(22, 30);
            lblrifa.Name = "lblrifa";
            lblrifa.Size = new Size(46, 15);
            lblrifa.TabIndex = 0;
            lblrifa.Text = "Código";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox2);
            Controls.Add(lblRifas);
            Controls.Add(img);
            Controls.Add(button1);
            Controls.Add(groupBox1);
            Controls.Add(lbPendings);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)img).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ListBox lbPendings;
        private GroupBox groupBox1;
        private Button button1;
        private Label lblNome;
        private Label lblphone;
        private Label lblemail;
        private Label lbldata;
        private Label lbltotal;
        private PictureBox img;
        private ListBox lblRifas;
        private GroupBox groupBox2;
        private Button btnrevogar;
        private Button btnvendido;
        private Label lblstatus;
        private Label lblrifa;
    }
}
