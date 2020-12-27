 using UnityEngine;
using System;
using System.Data;
using System.Collections;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

public class ServerSql
{
    /// <summary> 
    /// 工具类对象
    /// </summary>
    private static SqlAccess sqlAce;
    

    static MySqlConnection con;
    void Start()
    {
        
       
       
        
    }

    public static bool login(string name, string password)
    {
        Debug.Log(string.Format("{0}", name));
        bool a = false;
        sqlAce = new SqlAccess();
        con = SqlAccess.con;
        string sql = (string.Format("select * from user where name='{0}'and password='{1}'", name, password));
        Dictionary<int, List<string>> dic = sqlAce.QueryInfo(sql, con);
        if (dic.Count != 0)
        {
            a = true;
        }
        //这里可以加判断，判断是否有信息
        for (int i = 0; i < dic.Count; i++)
        {
            Debug.Log(string.Format("name：{0} password：{1} mail：{2} time：{3}", dic[i][0], dic[i][1], dic[i][2], dic[i][3]));
        }
        sqlAce.CloseMySQL();
        return a;
    }
    
    public static bool register(string name,string password,string mail,string time)
    {
        sqlAce = new SqlAccess();
        con = SqlAccess.con;
        int a=0;
        string sql1 = (string.Format("insert into user(name,password,mail,time) values('{0}','{1}','{2}','{3}')",name,password,mail,time));
        
        string[] str = new string[1] { sql1 };
        for (int i = 0; i < str.Length; i++)
        {
             a = 0;
            a = sqlAce.InsertInfo(str[i], con);
            Debug.Log(string.Format("{0}",a));
        }
        
        sqlAce.CloseMySQL();
        sqlAce = new SqlAccess();
        con = SqlAccess.con;
        string sql = (string.Format("insert into userinformation(name,totalkill,totalmiss,totalplayhour,totalenergyspend,totalGPA) values('{0}',0,0,0.0,0,0.0)", name));
        string[] str1 = new string[1] { sql };
        for (int i = 0; i < str1.Length; i++)
        {
            
            sqlAce.InsertInfo(str1[i], con);
           
        }
        sqlAce.CloseMySQL();
        sqlAce = new SqlAccess();
        con = SqlAccess.con;
        string sqlwelcome = (string.Format("select name,now() from user"));
        Dictionary<int, List<string>> dicc = sqlAce.QueryInfo(sqlwelcome, con);
        string anothertime = dicc[0][1];
        sqlAce.CloseMySQL();
        string content = "Welcome to FFS!";
        sqlAce = new SqlAccess();
        con = SqlAccess.con;
        string ssql = (string.Format("insert into email(name,time,content) values('{0}','{1}','{2}')", name, anothertime, content));
        string[] ssstr = new string[1] { ssql };
        for (int i = 0; i < ssstr.Length; i++)
        {

            if (a == 1)
            {

                sqlAce.InsertInfo(ssstr[i], con);
            }
            

        }
        sqlAce.CloseMySQL();
        if (a == 1) {
            return true;
        }
        
            return false;
        
    }
    public static Dictionary<int, List<string>> addeverygame(string name,int gamenumber,int kill,int miss, double playhour, int energyspend, double gpa) {
        sqlAce = new SqlAccess();
        con = SqlAccess.con;
        string sql2 = (string.Format("insert into gamestate(name,gamenumber,killnumber,missnumber,playhour,energyspend,finalGPA) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", name,gamenumber,kill,miss,playhour,energyspend,gpa));
        
        string[] str = new string[1] { sql2 };
        for (int i = 0; i < str.Length; i++)
        {
            
            sqlAce.InsertInfo(str[i], con);
            
        }
        sqlAce.CloseMySQL();
        ServerSql.Calculate(name);
        Dictionary<int, List<string>> dic1 = ServerSql.concreteachievements(name);
        List<string> one = new List<string>();
        Dictionary<int, List<string>> dic4 = new Dictionary<int, List<string>>();
        List<string> two = new List<string>();


        ServerSql.achievementsnumber(name);
        Dictionary<int, List<string>> dic2 = ServerSql.concreteachievements(name);
        Dictionary<int, List<string>> dic3 = new Dictionary<int, List<string>>();
        
        for (int i = 0; i < 14; i++) {
            if (dic1.Count != 0)
            {
                if (dic1[0][i] == "null")
                {
                    if (dic2[0][i] != "null")
                    {
                        one.Add(dic2[0][i]);

                    }
                }

                if (i == 11 | i == 12 | i == 13) {
                    if (dic1[0][i] != "null")
                    {
                        if (dic2[0][i] == "null")
                        {
                            two.Add(dic1[0][i]);

                        }
                    }
                }
            }
            else {
                if (dic2[0][i] != "null")
                {
                    one.Add(dic2[0][i]);

                }
            }
        }
        dic3.Add(0, one);
        dic4.Add(0, two);
        
        sqlAce = new SqlAccess();
        con = SqlAccess.con;
        string sqll = (string.Format("select now() from achievements"));
        Dictionary<int, List<string>> dicc = sqlAce.QueryInfo(sqll, con);
        string time = dicc[0][0];
        sqlAce.CloseMySQL();
        

            for (int i = 0; i < dic3[0].Count; i++) {
            string content = "";
            string congrats = "";

            congrats = (string.Format("Congratulations! You have one new achievement! "));

            content = content + congrats;
            content = content + Environment.NewLine;
            content = content + dic3[0][i];
            sqlAce = new SqlAccess();
            con = SqlAccess.con;
            string ssql = (string.Format("insert into email(name,time,content) values('{0}','{1}','{2}')", name, time, content));
            string[] ssstr = new string[1] { ssql };
            for (int j = 0; j < ssstr.Length; j++)
            {
                if (dic3[0].Count != 0)
                {
                    sqlAce.InsertInfo(ssstr[j], con);
                }

            }
            sqlAce.CloseMySQL();
        }
        for (int i = 0; i < dic4[0].Count; i++)
        {
            string content = "";
            string pity = "";

            pity = (string.Format("What a pity it is! You lost one achievement: "));

            content = content + pity;
            content = content + Environment.NewLine;
            content = content + dic4[0][i];
            sqlAce = new SqlAccess();
            con = SqlAccess.con;
            string ssql = (string.Format("insert into email(name,time,content) values('{0}','{1}','{2}')", name, time, content));
            string[] ssstr = new string[1] { ssql };
            for (int j = 0; j < ssstr.Length; j++)
            {
                if (dic4[0].Count != 0)
                {
                    sqlAce.InsertInfo(ssstr[j], con);
                }

            }
            sqlAce.CloseMySQL();
        }







        return dic3;
    }
    public static Dictionary<int, List<string>> showallemails(string name) {
        sqlAce = new SqlAccess();
        con = SqlAccess.con;
        string sql = (string.Format("select time,content from email where name ='{0}'",name));
        Dictionary<int, List<string>> dic = sqlAce.QueryInfo(sql, con);
        sqlAce.CloseMySQL();
        return dic;
    }
    public static bool judge(string name) {
        
        bool a = false;
        sqlAce = new SqlAccess();
        con = SqlAccess.con;
        string sql = (string.Format("select * from userinformation where name='{0}'", name));
        Dictionary<int, List<string>> dic = sqlAce.QueryInfo(sql, con);
        if (dic.Count != 0)
        {
            a = true;
        }
        
        sqlAce.CloseMySQL();
        return a;
    }
    public static Dictionary<int, List<string>> email(string name) {
        sqlAce = new SqlAccess();
        con = SqlAccess.con;
        string sql = (string.Format("select mail from user where name ='{0}'",name));
        Dictionary<int, List<string>> dic = sqlAce.QueryInfo(sql, con);
        sqlAce.CloseMySQL();
        return dic;
    }
    public static Dictionary<int, List<string>> Calculate(string name) {
        
        
        sqlAce = new SqlAccess();
        con = SqlAccess.con;
        string sql3 = (string.Format("select name,sum(killnumber),sum(missnumber),sum(playhour),sum(energyspend),round(avg(finalGPA),2) from gamestate group by name having name='{0}' ", name));
        Dictionary<int, List<string>> dic = sqlAce.QueryInfo(sql3, con);
        sqlAce.CloseMySQL();
        
        
        if (dic.Count == 0)
        {
            sqlAce = new SqlAccess();
            con = SqlAccess.con;
            string sqlm = (string.Format("select * from userinformation where name ='{0}'",name));
            Dictionary<int, List<string>> newdic = sqlAce.QueryInfo(sqlm, con);
            sqlAce.CloseMySQL();
            return newdic;
        }else
        {
            

            int a = int.Parse(dic[0][1]);
            int b = int.Parse(dic[0][2]);
            double c = double.Parse(dic[0][3]);
            int d = int.Parse(dic[0][4]);
            double e = double.Parse(dic[0][5]);
            //    string ss = "";
            if (!judge(name))
            {
                sqlAce = new SqlAccess();
                con = SqlAccess.con;
                string sql4 = (string.Format("insert into userinformation(name,totalkill,totalmiss,totalplayhour,totalenergyspend,totalGPA) values('{0}',{1},{2},{3},{4},{5})", dic[0][0], a, b, c, d, e));
                //  string sql5 = (string.Format("update userinformation set totalkill = {0} , totalmiss = {1} ,totalplayhour={2},totalenergyspend={3},totalGPA={4} where name ='{5}'",a,b,c,d,e,dic[0][0]));
                // if (!judge(name)) {

                //  }
                //  if (judge(name)) {
                //     ss = sql5;
                // }

                string[] str = new string[1] { sql4 };
                for (int i = 0; i < str.Length; i++)
                {
                    //   if (!judge(name))
                    //   {
                    //   Debug.Log(string.Format("{0}", ss));
                    sqlAce.InsertInfo(str[i], con);
                    //  }
                    //  if (judge(name)) {
                    //     sqlAce.UpdateInfo(str[i], con);
                    // }
                }
                sqlAce.CloseMySQL();

            }
            else
            {
                sqlAce = new SqlAccess();
                con = SqlAccess.con;

                string sql4 = (string.Format("update userinformation set totalkill = {0} , totalmiss = {1} ,totalplayhour={2},totalenergyspend={3},totalGPA={4} where name ='{5}'", a, b, c, d, e, dic[0][0]));


                string[] str = new string[1] { sql4 };
                for (int i = 0; i < str.Length; i++)
                {

                    sqlAce.UpdateInfo(str[i], con);

                }
                sqlAce.CloseMySQL();
            }
        }
            return dic;
        
        }
    public static bool judge2(string name) {
        bool a = false;
        sqlAce = new SqlAccess();
        con = SqlAccess.con;
        string sql = (string.Format("select * from achievements where name='{0}'", name));
        Dictionary<int, List<string>> dic = sqlAce.QueryInfo(sql, con);
        if (dic.Count != 0)
        {
            a = true;
        }

        sqlAce.CloseMySQL();
        return a;
    }
    public static int achievementsnumber(string name) {
        sqlAce = new SqlAccess();
        con = SqlAccess.con;
        string sql5 = (string.Format("select totalkill,totalmiss,totalplayhour,totalenergyspend,totalGPA from userinformation where name ='{0}'",name));
        Dictionary<int, List<string>> dic = sqlAce.QueryInfo(sql5, con);
        sqlAce.CloseMySQL();
        int a = int.Parse(dic[0][0]);
        int b = int.Parse(dic[0][1]);
        double c = double.Parse(dic[0][2]);
        int d = int.Parse(dic[0][3]);
        double e = double.Parse(dic[0][4]);
        string a1 = "null";
        string b1 = "null";
        string c1 = "null";
        string d1 = "null";
        string e1 = "null";
        string f1 = "null";
        string g1 = "null";
        string h1 = "null";
        string i1 = "null";
        string j1 = "null";
        string k1 = "null";
        string l1 = "null";
        string m1 = "null";
        string n1 = "null";
        int totalachievement = 0;
        if (a >= 100) {
            d1 = "Kill number reaches 100!";
            totalachievement += 1;
        }
        if (a >= 1) {
            a1 = "First blood!";
            totalachievement += 1;
        }
        if (a >= 10)
        {
            b1 = "Kill number reaches 10!";
            totalachievement += 1;
        }
        if (a >= 50)
        {
            c1 = "Kill number reaches 50!";
            totalachievement += 1;
        }
        if (a >= 200)
        {
            e1 = "Kill number reaches 200!";
            totalachievement += 1;
        }
        if (c >= 0.2)
        {
            f1 = "Play for more than 0.2 hours in total!";
            totalachievement += 1;
        }
        if (c >= 0.6)
        {
            g1 = "Play for more than 0.6 hours in total!";
            totalachievement += 1;
        }
        if (c >= 1) {
            h1 = "Play for more than 1 hour in total!";
            totalachievement += 1;
        }
        if (d >= 1000)
        {
            i1 = "Spend more than 1000 energy!";
            totalachievement += 1;
        }
        if (d >= 5000)
        {
            j1 = "Spend more than 5000 energy!";
            totalachievement += 1;
        }
        if (d >= 10000) {
            k1 = "Spend more than 10000 energy!";
            totalachievement += 1;
        }
        if (e >= 3.0)
        {
            l1 = "GPA is more than 3.0!";
            totalachievement += 1;
        }
        if (e >= 3.5)
        {
            m1 = "GPA is more than 3.5!";
            totalachievement += 1;
        }
        if (e>=3.9) {
            n1 = "GPA is more than 3.9!";
            totalachievement += 1;
        }
        if (!judge2(name))
        {
            sqlAce = new SqlAccess();
            con = SqlAccess.con;
            string sql6 = (string.Format("insert into achievements(name,one,two,three,four,five,six,seven,eight,nine,ten,eleven,twelve,thirteen,fourteen) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')", name, a1, b1, c1, d1,e1,f1,g1,h1,i1,j1,k1,l1,m1,n1));
            string[] str = new string[1] { sql6 };
            for (int i = 0; i < str.Length; i++)
            {

                sqlAce.InsertInfo(str[i], con);

            }

            sqlAce.CloseMySQL();
        }
        else {
            sqlAce = new SqlAccess();
            con = SqlAccess.con;
            string sql6 = (string.Format("update achievements set one = '{0}' ,two='{1}',three='{2}',four='{3}',five='{4}',six='{5}',seven='{6}',eight='{7}',nine='{8}',ten='{9}',eleven='{10}',twelve='{11}',thirteen='{12}',fourteen='{13}' where name ='{14}'", a1,b1,c1,d1,e1,f1,g1,h1,i1,j1,k1,l1,m1,n1,name));
            string[] str = new string[1] { sql6 };
            for (int i = 0; i < str.Length; i++)
            {

                sqlAce.UpdateInfo(str[i], con);

            }

            sqlAce.CloseMySQL();
        }
            return totalachievement;
        
    }
    public static Dictionary<int, List<string>> concreteachievements(string name) {
        sqlAce = new SqlAccess();
        con = SqlAccess.con;
        string sql7 = (string.Format("select one,two,three,four,five,six,seven,eight,nine,ten,eleven,twelve,thirteen,fourteen from achievements where name='{0}' ", name));
        Dictionary<int, List<string>> dic = sqlAce.QueryInfo(sql7, con);
        

        sqlAce.CloseMySQL();
        return dic;
    }
    public static Dictionary<int, List<string>> Rank1() {
        sqlAce = new SqlAccess();
        con = SqlAccess.con;
        string sql8 = (string.Format("select name,round(avg(finalGPA),2) as c from gamestate group by name,gamenumber having gamenumber =1 order by c desc limit 10"));
        Dictionary<int, List<string>> dic = sqlAce.QueryInfo(sql8, con);
        sqlAce.CloseMySQL();
        return dic;
    }
    public static Dictionary<int, List<string>> myRank1(string name)
    {
        Dictionary<int, List<string>> finaldic = new Dictionary<int, List<string>>();
        sqlAce = new SqlAccess();
        con = SqlAccess.con;
        string sql9 = (string.Format("select round(avg(finalGPA),2) as c from gamestate group by name,gamenumber having gamenumber =1 and name='{0}' ", name));
        Dictionary<int, List<string>> dic = sqlAce.QueryInfo(sql9, con);

        sqlAce.CloseMySQL();
        if (dic.Count != 0)
        {
            string mygpa = dic[0][0];
            double mine = double.Parse(mygpa);
            sqlAce = new SqlAccess();
            con = SqlAccess.con;
            string sqln = (string.Format("select count(*) from(select name,round(avg(finalGPA),2) as c from gamestate group by name,gamenumber having gamenumber =1)x where c>{0}", mine));
            Dictionary<int, List<string>> dicc = sqlAce.QueryInfo(sqln, con);

            sqlAce.CloseMySQL();
            string before = dicc[0][0];
            int k = int.Parse(before);
            k = k + 1;
            List<string> final = new List<string>();
            string kstr = k.ToString();
            final.Add(kstr);
            final.Add(mygpa);

            finaldic.Add(0, final);
        }
        if (dic.Count == 0)
        {
            Dictionary<int, List<string>> dic1 = new Dictionary<int, List<string>>();
            List<string> one = new List<string>();
            one.Add("---");
            one.Add("0.00");
            dic1.Add(0, one);
            return dic1;
        }
        return finaldic;
    }
    public static Dictionary<int, List<string>> Rank2()
    {
        sqlAce = new SqlAccess();
        con = SqlAccess.con;
        string sql10 = (string.Format("select name,round(avg(finalGPA),2) as c from gamestate group by name,gamenumber having gamenumber =2 order by c desc limit 10"));
        Dictionary<int, List<string>> dic = sqlAce.QueryInfo(sql10, con);
        sqlAce.CloseMySQL();
        return dic;
    }
    public static Dictionary<int, List<string>> myRank2(string name)
    {
        Dictionary<int, List<string>> finaldic = new Dictionary<int, List<string>>();
        sqlAce = new SqlAccess();
        con = SqlAccess.con;
        string sql9 = (string.Format("select round(avg(finalGPA),2) as c from gamestate group by name,gamenumber having gamenumber =2 and name='{0}' ", name));
        Dictionary<int, List<string>> dic = sqlAce.QueryInfo(sql9, con);

        sqlAce.CloseMySQL();
        if (dic.Count != 0)
        {
            string mygpa = dic[0][0];
            double mine = double.Parse(mygpa);
            sqlAce = new SqlAccess();
            con = SqlAccess.con;
            string sqln = (string.Format("select count(*) from(select name,round(avg(finalGPA),2) as c from gamestate group by name,gamenumber having gamenumber =2)x where c>{0}", mine));
            Dictionary<int, List<string>> dicc = sqlAce.QueryInfo(sqln, con);

            sqlAce.CloseMySQL();
            string before = dicc[0][0];
            int k = int.Parse(before);
            k = k + 1;
            List<string> final = new List<string>();
            string kstr = k.ToString();
            final.Add(kstr);
            final.Add(mygpa);

            finaldic.Add(0, final);
        }
        if (dic.Count == 0)
        {
            Dictionary<int, List<string>> dic1 = new Dictionary<int, List<string>>();
            List<string> one = new List<string>();
            one.Add("---");
            one.Add("0.00");
            dic1.Add(0, one);
            return dic1;
        }
        return finaldic;
    }
    public static Dictionary<int, List<string>> Rank3()
    {
        sqlAce = new SqlAccess();
        con = SqlAccess.con;
        string sql12 = (string.Format("select name,round(avg(finalGPA),2) as c from gamestate group by name,gamenumber having gamenumber =3 order by c desc limit 10"));
        Dictionary<int, List<string>> dic = sqlAce.QueryInfo(sql12, con);
        sqlAce.CloseMySQL();
        return dic;
    }
    public static Dictionary<int, List<string>> myRank3(string name)
    {
        Dictionary<int, List<string>> finaldic = new Dictionary<int, List<string>>();
        sqlAce = new SqlAccess();
        con = SqlAccess.con;
        string sql9 = (string.Format("select round(avg(finalGPA),2) as c from gamestate group by name,gamenumber having gamenumber =3 and name='{0}' ", name));
        Dictionary<int, List<string>> dic = sqlAce.QueryInfo(sql9, con);

        sqlAce.CloseMySQL();
        if (dic.Count != 0)
        {
            string mygpa = dic[0][0];
            double mine = double.Parse(mygpa);
            sqlAce = new SqlAccess();
            con = SqlAccess.con;
            string sqln = (string.Format("select count(*) from(select name,round(avg(finalGPA),2) as c from gamestate group by name,gamenumber having gamenumber =3)x where c>{0}", mine));
            Dictionary<int, List<string>> dicc = sqlAce.QueryInfo(sqln, con);

            sqlAce.CloseMySQL();
            string before = dicc[0][0];
            int k = int.Parse(before);
            k = k + 1;
            List<string> final = new List<string>();
            string kstr = k.ToString();
            final.Add(kstr);
            final.Add(mygpa);

            finaldic.Add(0, final);
        }
        if (dic.Count == 0)
        {
            Dictionary<int, List<string>> dic1 = new Dictionary<int, List<string>>();
            List<string> one = new List<string>();
            one.Add("---");
            one.Add("0.00");
            dic1.Add(0, one);
            return dic1;
        }
        return finaldic;
    }


}