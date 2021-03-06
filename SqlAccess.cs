using UnityEngine;
using System.Collections;
using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

public class SqlAccess
{
	public static MySqlConnection mySqlConnection;
	//数据库名称
	public static string database = "test";
	//数据库IP
	private static string host = "121.196.45.45";
	//用户名
	private static string username = "checker";
	//用户密码
	private static string password = "123456";

	public static string sql = string.Format("database={0};server={1};user={2};password={3};port={4}",
	database, host, username, password, "3306");

	public static MySqlConnection con;
	private MySqlCommand com;

    #region BaseOperation

    /// <summary>
    /// 构造方法开启数据库
    /// </summary>
    public SqlAccess()
    {
        con = new MySqlConnection(sql);
        
        OpenMySQL(con);
    }
    /// <summary>
    /// 启动数据库
    /// </summary>    
    /// <param name="con"></param>
    public void OpenMySQL(MySqlConnection con)
    {
        con.Open();
        
    }
    /// <summary>
    /// 创建表
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="con"></param>
    public void CreateTable(string sql, MySqlConnection con)
    {
        MySqlCommand com = new MySqlCommand(sql, con);
        int res = com.ExecuteNonQuery();
    }
    /// <summary>
    /// 插入数据
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="con"></param>
    public int InsertInfo(string sql, MySqlConnection con)
    {
      try
        {
            MySqlCommand com = new MySqlCommand(sql, con);
            int res = com.ExecuteNonQuery();
            return res;
        }
        catch (MySqlException e)
        {
            Debug.Log(string.Format("1"));
            return 0;
        }
    }
    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="con"></param>
    public int DeleteInfo(string sql, MySqlConnection con)
    {
        MySqlCommand com = new MySqlCommand(sql, con);
        int res = com.ExecuteNonQuery();
        return res;
    }
    /// <summary>
    /// 修改数据
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="con"></param>
    public int UpdateInfo(string sql, MySqlConnection con)
    {
        MySqlCommand com = new MySqlCommand(sql, con);
        int res = com.ExecuteNonQuery();
        return res;
    }
    /// <summary>
    /// 查询数据
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="con"></param>
    public Dictionary<int, List<string>> QueryInfo(string sql, MySqlConnection con)
    {
        Dictionary<int, List<string>> dic = new Dictionary<int, List<string>>();
        try
        {
            int indexDic = 0;
            int indexList = 0;
            
            MySqlCommand com = new MySqlCommand(sql, con);
            MySqlDataReader reader = com.ExecuteReader();
            while (true)
            {
                if (reader.Read())
                {
                    List<string> list = new List<string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        list.Add(reader[indexList].ToString());
                        indexList++;
                    }
                    dic.Add(indexDic, list);
                    indexDic++;
                    indexList = 0;
                }
                else
                {
                    break;
                }
            }
        }
        catch (KeyNotFoundException e)
        {

            Dictionary<int, List<string>> dic1 = new Dictionary<int, List<string>>();
            List<string> list1 = new List<string>();
            dic1.Add(0, list1);
            return dic1;
        }
        return dic;
    }
    /// <summary>
    /// 关闭数据库
    /// </summary>
    public void CloseMySQL()
    {
        (new MySqlConnection(sql)).Close();
       
    }
    #endregion

}

