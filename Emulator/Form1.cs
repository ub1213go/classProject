using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emulator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region 宣告變數
        CStock A;

        string ID = "2324";
        int Money = 0;
        int price = 23500;
        int piece = 1;
        double tax = 0.003;
        double fee = 0.001425 * 0.28;
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;

            A = new CStock() {
                ID = ID,
                fee = fee,
                tax = tax
            };

            #region 顯示
            tBID.Text = ID;
            tBPrice.Text = (Convert.ToDouble(price) / 1000).ToString();
            tBPiece.Text = piece.ToString();
            tBMoney.Text = Money.ToString();
            tBFee.Text = A.fee.ToString("f4");
            tBTax.Text = A.tax.ToString();
            #endregion

        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            piece = Convert.ToInt32(tBPiece.Text);
            price = Convert.ToInt32(Convert.ToDouble(tBPrice.Text) * 1000);
            Money += A.addStock(price, piece);
            A.listBoxRefrash(listBox1);
            tBMoney.Text = Money.ToString();

            textBox1.Text += $"{DateTime.Now.ToString("HH:mm:ss")} -> 買入 價格{(Convert.ToDouble(price) / 1000)}, 數量{piece}";
            textBox1.Text += Environment.NewLine;
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            piece = Convert.ToInt32(tBPiece.Text);
            price = Convert.ToInt32(Convert.ToDouble(tBPrice.Text) * 1000);
            Money += A.addStock(price, (-1) * piece);
            A.listBoxRefrash(listBox1);
            tBMoney.Text = Money.ToString();

            textBox1.Text += $"{DateTime.Now.ToString("HH: mm: ss")} -> 賣出 價格{(Convert.ToDouble(price) / 1000)}, 數量{piece}";
            textBox1.Text += Environment.NewLine;
        }

        private void btnPriceUp_Click(object sender, EventArgs e)
        {
            if (price < 50000)
                price += 50;
            else if (price < 100000)
                price += 100;
            else
                price += 500;
            tBPrice.Text = (Convert.ToDouble(price) / 1000).ToString();
        }

        private void btnPriceDown_Click(object sender, EventArgs e)
        {
            if (price < 50000)
                price -= 50;
            else if (price < 100000)
                price -= 100;
            else
                price -= 500;
            tBPrice.Text = (Convert.ToDouble(price) / 1000).ToString();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    label1.Focus();
                    btnPriceUp_Click(this, new EventArgs());
                    break;
                case Keys.S:
                    label1.Focus();
                    btnPriceDown_Click(this, new EventArgs());
                    break;
                case Keys.A:
                    label1.Focus();
                    btnSale_Click(this, new EventArgs());
                    break;
                case Keys.D:
                    label1.Focus();
                    btnBuy_Click(this, new EventArgs());
                    break;
                default:
                    break;
            }
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            label1.Focus();
        }

        private void tBPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                price = Convert.ToInt32(Convert.ToDouble(tBPrice.Text) * 1000);
                label1.Focus();
            }
        }

        private void tBPiece_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                price = Convert.ToInt32(Convert.ToDouble(tBPrice.Text) * 1000);
                label1.Focus();
            }
        }
    }
    enum EStatus{
        None = 0,
        Buy = 1,
        Sale = 2,
        backBuy = 3,
        backSale = 4
    }
    /// <summary>
    /// 倉庫
    /// <para>存放各股票ID的價格張數</para>
    /// </summary>
    public class CStock
    {
        private double _tax = 0.003;
        private double _fee = 0.001425;
        private int money = 0;
        private int cost = 0;
        private string _ID = null;
        private Dictionary<int, int> _ST;
        private Dictionary<string, Dictionary<int, int>> _STid;

        public double tax
        {
            get
            {
                return _tax;
            }
            set
            {
                _tax = value;
            }
        }
        public double fee
        {
            get
            {
                return _fee;
            }
            set
            {
                _fee = value;
            }
        }
        public string ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if(_STid.ContainsKey(value))
                    _ST = _STid[value];
                else
                {
                    _STid.Add(value, new Dictionary<int, int>());
                    _ST = _STid[value];
                }
                _ID = value;
            }
        }
        
        public CStock()
        {
            _STid = new Dictionary<string, Dictionary<int, int>>();
        }
        public int addStock(int price, int piece)
        {
            if (piece == 0) throw new Exception("數量不能為零");
            cost = 0;

            // 花費計算處理
            if (piece > 0 & _ST.Any())
            {
                if (_ST.Values.Min() > 0) // 買 (股倉正數 負數
                {
                    if (_ST.ContainsKey(price))
                        _ST[price] += piece;
                    else
                        _ST.Add(price, piece);
                    cost = price * Math.Abs(piece) + feeCal(price, piece);
                    cost = (-1) * cost;
                }
                else // 回補買 (股倉負數 正數
                {
                    if(_ST.Values.Sum() < piece)
                    {
                        int temp = 0;
                        foreach (var item in _ST)
                        {
                            for (int i = 0; true ; i++)
                            {
                                _ST[item.Key] += 1;
                                temp++;
                                if (_ST[item.Key] == 0 | temp == piece) break;
                            }
                            if (temp == piece) break;
                        }
                        cost = price * Math.Abs(temp) - feeCal(price, temp) - taxCal(price, temp);

                        foreach (var item in _ST)
                        {
                            if (item.Value == 0)
                                _ST.Remove(item.Key);
                        }

                        if (temp != piece)
                        {
                            _ST.Add(price, piece - temp);
                            cost -= price * Math.Abs(piece - temp) - feeCal(price, piece - temp) - taxCal(price, piece - temp);

                        }
                    }
                    else
                    {
                        int temp = 0;
                        foreach (var item in _ST)
                        {
                            for (int i = 0; true; i++)
                            {
                                _ST[item.Key] += 1;
                                temp++;
                                if (_ST[item.Key] == 0 | temp == piece) break;
                            }
                            if (temp == piece) break;
                        }
                        cost = price * Math.Abs(temp) - feeCal(price, temp) - taxCal(price, temp);
                    }
                }
            }
            else if(piece < 0 & _ST.Any())
            {
                if (_ST.Values.Min() > 0) // 回補賣 (股倉正數 正數
                {
                    if(_ST.Values.Sum() < Math.Abs(piece))
                    {
                        int temp = 0;
                        foreach (var item in _ST)
                        {
                            for (int i = 0; true; i++)
                            {
                                _ST[item.Key] -= 1;
                                temp++;
                                if (_ST[item.Key] == 0 | temp == Math.Abs(piece)) break;
                            }
                            if (temp == Math.Abs(piece)) break;
                        }

                        cost = price * Math.Abs(temp) - feeCal(price, temp) - taxCal(price, temp);

                        foreach (var item in _ST)
                        {
                            if (item.Value == 0)
                                _ST.Remove(item.Key);
                        }

                        if (temp != Math.Abs(piece))
                        {
                            _ST.Add(price, piece + temp);
                            cost -= price * Math.Abs(piece + temp) - feeCal(price, piece + temp) - taxCal(price, piece + temp);

                        }
                    }
                    else
                    {
                        int temp = 0;
                        foreach (var item in _ST)
                        {
                            for (int i = 0; true; i++)
                            {
                                _ST[item.Key] -= 1;
                                temp++;
                                if (_ST[item.Key] == 0 | temp == Math.Abs(piece)) break;
                            }
                            if (temp == Math.Abs(piece)) break;
                        }

                        cost = price * Math.Abs(temp) - feeCal(price, temp) - taxCal(price, temp);

                    }
                }
                else // 賣 (股倉負數 負數
                {
                    if (_ST.ContainsKey(price))
                        _ST[price] += piece;
                    else
                        _ST.Add(price, piece);
                    cost = price * Math.Abs(piece) + feeCal(price, piece);
                    cost = (-1) * cost;
                }
               
            }
            else
            {
                _ST.Add(price, piece);
                cost = price * Math.Abs(piece) + feeCal(price, piece);
                cost = (-1) * cost;
            }

            foreach (var item in _ST)
            {
                if (item.Value == 0)
                    _ST.Remove(item.Key);
            }

            return cost;
        }
        public void listBoxRefrash(ListBox A)
        {
            A.Items.Clear();
            if(_ST != null)
            {
                foreach (var item in _ST)
                {
                    A.Items.Add(item.ToString());
                }
            }
        }

        private int feeCal(in int price, in int piece)
        {
            return (Convert.ToInt32(Math.Round(_fee * price * piece, MidpointRounding.AwayFromZero)) < 20 )?20 : Convert.ToInt32(Math.Round(_fee * price * piece, MidpointRounding.AwayFromZero));
        }
        private int taxCal(in int price, in int piece)
        {
            return Convert.ToInt32(Math.Round(_tax * price * piece / 2));
        }
        
    }
}
