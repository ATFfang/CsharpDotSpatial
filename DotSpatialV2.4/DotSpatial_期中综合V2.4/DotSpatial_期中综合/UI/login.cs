using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DotSpatial_期中综合
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            string ID_password = string.Format($"{Application.StartupPath}\\data\\{"ID_PASSWORD.csv"}");
            Dictionary<string, string> ID_password_dic = new Dictionary<string, string>();
            //0.获取用户名和密码
            if (this.Local.Checked)
            {
                StreamReader sr = new StreamReader(ID_password);
                string line = sr.ReadLine();
                string[] strs = null;
                while ((line = sr.ReadLine()) != null)
                {
                    strs = line.Split(',');
                    ID_password_dic[strs[0]] = strs[1];
                }
            }
            

            //从数据库获取
            if(this.DataBase.Checked)
            {
                DataTable dt = DB.ConnectDB();

                foreach(DataRow r in dt.Rows)
                {
                    ID_password_dic[r[0].ToString()] = r[1].ToString();
                }
            }
            

            //1. 获取数据
            //从TextBox中获取用户输入信息
            string userName = this.txtUserName.Text;
            string userPassword = this.txtPassword.Text;

            //2. 验证数据
            // 验证用户输入是否为空，若为空，提示用户信息
            
            if (userName.Equals("") || userPassword.Equals(""))
            {
                MessageBox.Show("用户名或密码不能为空！");
            }
            // 若不为空，验证用户名和密码是否与数据库匹配
            // 这里只做字符串对比验证
            else
            {
                int iflogin = 0;
                foreach (KeyValuePair<string, string> kvl in ID_password_dic)
                {
                    if (userName.Equals(kvl.Key) && userPassword.Equals(kvl.Value))
                    {
                        MessageBox.Show("登录成功！");

                        this.DialogResult = DialogResult.OK;
                        this.Dispose();
                        this.Close();
                        iflogin = 1;
                    }
                }
                if(iflogin==0)
                {
                    MessageBox.Show("用户名或密码错误！");
                }
                //用户名和密码验证正确，提示成功，并执行跳转界面。
                
                //用户名和密码验证错误，提示错误。
            }
        }


    }
}
