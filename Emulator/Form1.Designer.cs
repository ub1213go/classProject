
namespace Emulator
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
            this.tBID = new System.Windows.Forms.TextBox();
            this.tBPrice = new System.Windows.Forms.TextBox();
            this.tBPiece = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSale = new System.Windows.Forms.Button();
            this.btnBuy = new System.Windows.Forms.Button();
            this.tBTax = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tBFee = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tBCostFee = new System.Windows.Forms.TextBox();
            this.tBCostTax = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tBinID = new System.Windows.Forms.TextBox();
            this.tBCostAvg = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tBinStock = new System.Windows.Forms.TextBox();
            this.btnPriceUp = new System.Windows.Forms.Button();
            this.btnPriceDown = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.tBTT = new System.Windows.Forms.TextBox();
            this.tBMoney = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tBID
            // 
            this.tBID.Location = new System.Drawing.Point(75, 12);
            this.tBID.Name = "tBID";
            this.tBID.Size = new System.Drawing.Size(100, 23);
            this.tBID.TabIndex = 0;
            // 
            // tBPrice
            // 
            this.tBPrice.Location = new System.Drawing.Point(75, 41);
            this.tBPrice.Name = "tBPrice";
            this.tBPrice.Size = new System.Drawing.Size(100, 23);
            this.tBPrice.TabIndex = 1;
            this.tBPrice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tBPrice_KeyDown);
            // 
            // tBPiece
            // 
            this.tBPiece.Location = new System.Drawing.Point(75, 70);
            this.tBPiece.Name = "tBPiece";
            this.tBPiece.Size = new System.Drawing.Size(100, 23);
            this.tBPiece.TabIndex = 2;
            this.tBPiece.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tBPiece_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "價格";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "張數";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 379);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 15);
            this.label6.TabIndex = 8;
            // 
            // btnSale
            // 
            this.btnSale.Location = new System.Drawing.Point(12, 166);
            this.btnSale.Name = "btnSale";
            this.btnSale.Size = new System.Drawing.Size(61, 29);
            this.btnSale.TabIndex = 4;
            this.btnSale.TabStop = false;
            this.btnSale.Text = "賣";
            this.btnSale.UseVisualStyleBackColor = true;
            this.btnSale.Click += new System.EventHandler(this.btnSale_Click);
            // 
            // btnBuy
            // 
            this.btnBuy.Location = new System.Drawing.Point(151, 166);
            this.btnBuy.Name = "btnBuy";
            this.btnBuy.Size = new System.Drawing.Size(61, 29);
            this.btnBuy.TabIndex = 5;
            this.btnBuy.TabStop = false;
            this.btnBuy.Text = "買";
            this.btnBuy.UseVisualStyleBackColor = true;
            this.btnBuy.Click += new System.EventHandler(this.btnBuy_Click);
            // 
            // tBTax
            // 
            this.tBTax.Location = new System.Drawing.Point(67, 46);
            this.tBTax.Name = "tBTax";
            this.tBTax.Size = new System.Drawing.Size(96, 23);
            this.tBTax.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 49);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 15);
            this.label9.TabIndex = 16;
            this.label9.Text = "稅率";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 15);
            this.label10.TabIndex = 18;
            this.label10.Text = "手續費率";
            // 
            // tBFee
            // 
            this.tBFee.Location = new System.Drawing.Point(67, 20);
            this.tBFee.Name = "tBFee";
            this.tBFee.Size = new System.Drawing.Size(96, 23);
            this.tBFee.TabIndex = 20;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(169, 18);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(74, 29);
            this.button3.TabIndex = 22;
            this.button3.Text = "更改費率";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(231, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(202, 184);
            this.listBox1.TabIndex = 30;
            // 
            // tBCostFee
            // 
            this.tBCostFee.Location = new System.Drawing.Point(288, 16);
            this.tBCostFee.Name = "tBCostFee";
            this.tBCostFee.Size = new System.Drawing.Size(86, 23);
            this.tBCostFee.TabIndex = 13;
            // 
            // tBCostTax
            // 
            this.tBCostTax.Location = new System.Drawing.Point(288, 42);
            this.tBCostTax.Name = "tBCostTax";
            this.tBCostTax.Size = new System.Drawing.Size(86, 23);
            this.tBCostTax.TabIndex = 14;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(288, 68);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(86, 23);
            this.textBox8.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(205, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 15);
            this.label5.TabIndex = 7;
            this.label5.Text = "回補證交稅";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(205, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "回補手續費";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(205, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 15);
            this.label7.TabIndex = 14;
            this.label7.Text = "未實現損益";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.tBTax);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.tBFee);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Location = new System.Drawing.Point(12, 322);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(259, 76);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "費率";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.tBinID);
            this.groupBox2.Controls.Add(this.tBCostAvg);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.tBinStock);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.tBCostTax);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.tBCostFee);
            this.groupBox2.Controls.Add(this.textBox8);
            this.groupBox2.Location = new System.Drawing.Point(12, 216);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(421, 100);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "庫存即時損益";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(31, 24);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(19, 15);
            this.label14.TabIndex = 32;
            this.label14.Text = "ID";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(17, 73);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(55, 15);
            this.label12.TabIndex = 18;
            this.label12.Text = "平均價格";
            // 
            // tBinID
            // 
            this.tBinID.Location = new System.Drawing.Point(87, 16);
            this.tBinID.Name = "tBinID";
            this.tBinID.Size = new System.Drawing.Size(86, 23);
            this.tBinID.TabIndex = 10;
            // 
            // tBCostAvg
            // 
            this.tBCostAvg.Location = new System.Drawing.Point(87, 69);
            this.tBCostAvg.Name = "tBCostAvg";
            this.tBCostAvg.Size = new System.Drawing.Size(86, 23);
            this.tBCostAvg.TabIndex = 12;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 47);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 15);
            this.label11.TabIndex = 16;
            this.label11.Text = "庫存張數";
            // 
            // tBinStock
            // 
            this.tBinStock.Location = new System.Drawing.Point(87, 43);
            this.tBinStock.Name = "tBinStock";
            this.tBinStock.Size = new System.Drawing.Size(86, 23);
            this.tBinStock.TabIndex = 11;
            // 
            // btnPriceUp
            // 
            this.btnPriceUp.Location = new System.Drawing.Point(99, 154);
            this.btnPriceUp.Name = "btnPriceUp";
            this.btnPriceUp.Size = new System.Drawing.Size(29, 23);
            this.btnPriceUp.TabIndex = 6;
            this.btnPriceUp.TabStop = false;
            this.btnPriceUp.Text = "^";
            this.btnPriceUp.UseMnemonic = false;
            this.btnPriceUp.UseVisualStyleBackColor = true;
            this.btnPriceUp.Click += new System.EventHandler(this.btnPriceUp_Click);
            // 
            // btnPriceDown
            // 
            this.btnPriceDown.Location = new System.Drawing.Point(99, 183);
            this.btnPriceDown.Name = "btnPriceDown";
            this.btnPriceDown.Size = new System.Drawing.Size(29, 23);
            this.btnPriceDown.TabIndex = 7;
            this.btnPriceDown.TabStop = false;
            this.btnPriceDown.Text = "v";
            this.btnPriceDown.UseVisualStyleBackColor = true;
            this.btnPriceDown.Click += new System.EventHandler(this.btnPriceDown_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 124);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 15);
            this.label8.TabIndex = 25;
            this.label8.Text = "損益";
            // 
            // tBTT
            // 
            this.tBTT.Location = new System.Drawing.Point(75, 120);
            this.tBTT.Name = "tBTT";
            this.tBTT.Size = new System.Drawing.Size(100, 23);
            this.tBTT.TabIndex = 3;
            // 
            // tBMoney
            // 
            this.tBMoney.Location = new System.Drawing.Point(277, 347);
            this.tBMoney.Name = "tBMoney";
            this.tBMoney.Size = new System.Drawing.Size(156, 23);
            this.tBMoney.TabIndex = 50;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(277, 329);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(31, 15);
            this.label13.TabIndex = 19;
            this.label13.Text = "現金";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(439, 11);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(253, 386);
            this.textBox1.TabIndex = 51;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 410);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.tBMoney);
            this.Controls.Add(this.tBTT);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnPriceDown);
            this.Controls.Add(this.btnPriceUp);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btnBuy);
            this.Controls.Add(this.btnSale);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tBPiece);
            this.Controls.Add(this.tBPrice);
            this.Controls.Add(this.tBID);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tBID;
        private System.Windows.Forms.TextBox tBPrice;
        private System.Windows.Forms.TextBox tBPiece;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSale;
        private System.Windows.Forms.Button btnBuy;
        private System.Windows.Forms.TextBox tBTax;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tBFee;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox tBCostFee;
        private System.Windows.Forms.TextBox tBCostTax;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnPriceUp;
        private System.Windows.Forms.Button btnPriceDown;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tBTT;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tBCostAvg;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tBinStock;
        private System.Windows.Forms.TextBox tBMoney;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tBinID;
        private System.Windows.Forms.TextBox textBox1;
    }
}

