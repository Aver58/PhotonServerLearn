using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace MyServer.DB
{

    public static class DBHelper
    {
        static MySqlConnection SQL;//SQL
        /// <summary>
        /// 与数据库建立连接
        /// </summary>
        public static void ConnectSql()
        {
            string connectStr = "server=127.0.0.1;port=3306;database=Aver3;user=root;password=op90--;";
            SQL = new MySqlConnection(connectStr);
            try
            {
                SQL.Open();

                //在这里执行其它操作

                //  Console.WriteLine("已经建立连接");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                SQL.Close();
            }
            //string str = "Server=127.0.0.1;User ID=root;Password=op90--;Database=aver3;CharSet=utf8;";
            //SQL = new MySqlConnection(str);//实例化链接
            //SQL.Open();//开启连接
        }
        /// <summary>
        /// 执行查询命令MySQLDataReader
        /// </summary>
        public static void dataReader()
        {
            string sql = "select * from users";

            MySqlCommand cmd = new MySqlCommand(sql, SQL);
            //cmd.ExecuteReader();//执行一些查询
            //cmd.ExecuteNonQuery();//插入 删除
            //cmd.ExecuteScalar();//执行一些查询，返回一个单个的值
            MySqlDataReader reader = cmd.ExecuteReader();
            //reader.Read();//读取下一页数据，如果读取成功，返回true，如果没有下一页了，读取失败的话，返回false

            while (reader.Read())
            {
                //Console.WriteLine(reader[0].ToString() + reader[1].ToString() + reader[2].ToString());
                //Console.WriteLine(reader.GetInt32(0) + " " + reader.GetString(1) + " " + reader.GetString(2));
                Console.WriteLine(reader.GetInt32("id") + " " + reader.GetString("username") + " " + reader.GetString("password"));
            }
        }
        /// <summary>
        /// 对数据进行插入Insert操作
        /// </summary>
        public static void Insert()
        {
            string sql = "insert into users(username,password,registerdate) values('csdFu','234','" + DateTime.Now + "')";

            MySqlCommand cmd = new MySqlCommand(sql, SQL);

            int result = cmd.ExecuteNonQuery();//返回值是数据库中受影响的数据的行数
        }
        /// <summary>
        /// 对数据进行删除Delete操作
        /// </summary>
        public static void Delete()
        {
            string sql = "delete from users where id = 4";

            MySqlCommand cmd = new MySqlCommand(sql, SQL);

            int result = cmd.ExecuteNonQuery();//返回值是数据库中受影响的数据的行数
        }
        /// <summary>
        /// 对数据进行Update操作
        /// </summary>
        public static void Update()
        {
            string sql = "update users set username='sdfsedfwer',password='23242342432' where id = 4";

            MySqlCommand cmd = new MySqlCommand(sql, SQL);

            int result = cmd.ExecuteNonQuery();//返回值是数据库中受影响的数据的行数
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        static bool VerifyUser(string username, string password)
        {
            string connectStr = "server=127.0.0.1;port=3306;database=mygamedb;user=root;password=root;";
            MySqlConnection conn = new MySqlConnection(connectStr);//并没有去跟数据库建立连接
            try
            {
                conn.Open();
                //string sql = "select * from users where username = '"+username+"' and                 password='"+password+"'";
                //我们自己按照查询条件去组拼sql，当参数多时容易出错。
                string sql = "select * from users where username =@para1 and password = @para2 ";

                MySqlCommand cmd = new MySqlCommand(sql, SQL);
                cmd.Parameters.AddWithValue("para1", username);
                cmd.Parameters.AddWithValue("para2", password);

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                conn.Close();
            }
            return false;
        }
    }
}
