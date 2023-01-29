using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//References
using Mono.Data.Sqlite;
using System;
using System.Data;
using System.IO;
using UnityEngine.UI;

public class Umity : MonoBehaviour
{
    private string conn, sqlQuery;
    IDbConnection dbconn;
    IDbCommand dbcmd;
    private IDataReader reader;
    public Text t_name, t_Address, t_id;
    public Text data_staff;
    public Text ninios;

    string DatabaseName = "Employers.s3db";

    void Start()
    {

        string filepath = Application.dataPath + "/Plugins/" + DatabaseName;

        //open db connection
        conn = "URI=file:" + filepath;

        Debug.Log("Stablishing connection to: " + conn);
        dbconn = new SqliteConnection(conn);
        dbconn.Open();
        data_staff.text = "";
        ninios.text = "";
        //reader_function();
        reader_function2();
    }
    //Insert
    public void insert_button()
    {
        //insert_function(t_name.text, t_Address.text);
        Debug.Log("hol");
        insert_function2(t_name.text, t_Address.text);
    }
    //Search 
    public void Search_button()
    {
        data_staff.text = "";
        Search_function(t_id.text);

    }

    //Found to Update 
    public void F_to_update_button()
    {
        data_staff.text = "";
        F_to_update_function(t_id.text);

    }
    //Update
    public void Update_button()
    {
        update_function(t_id.text, t_name.text, t_Address.text);

    }

    //Delete
    public void Delete_button()
    {
        data_staff.text = "";
        Delete_function(t_id.text);

    }

    //Insert To Database
    private void insert_function(string name, string Address)
    {
        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open(); //Open connection to the database.
            dbcmd = dbconn.CreateCommand();
            sqlQuery = string.Format("insert into Staff (name, Address) values (\"{0}\",\"{1}\")", name, Address);// table name
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbconn.Close();
        }
        data_staff.text = "";
        Debug.Log("Insert Done  ");

        reader_function();
    }

    private void insert_function2(string name, string tablet)
    {
        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open(); //Open connection to the database.
            dbcmd = dbconn.CreateCommand();
            sqlQuery = string.Format("insert into Kids (name, " +"Tablet, Points) values (\"{0}\",\"{1}\",\"{2}\")", name,tablet,0);// table name
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbconn.Close();
        }
        ninios.text = "";
        Debug.Log("Insert Done  ");

        reader_function2();
    }

    //Read All Data For To Database
    private void reader_function()
    {
        Debug.Log("leer1");
         int idreaders;
        string Namereaders, Addressreaders;
        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open(); //Open connection to the database.
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT  Id, Name, address " + "FROM Staff";// table name
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {

                idreaders = reader.GetInt32(0);
                Namereaders = reader.GetString(1);
                Addressreaders = reader.GetString(2);
                //tabletreaders = reader.GetInt32(3);

                data_staff.text += idreaders+Namereaders + Addressreaders + "\n";
                Debug.Log("Value="+idreaders + " name =" + Namereaders + "Address=" + Addressreaders+" ");
            }
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            //       dbconn = null;

        }
    }
    private void reader_function2()
    {
        Debug.Log("leer2");
        int idreaders, pointreaders;
        string Namereaders, Surnamereaders;
        using (dbconn = new SqliteConnection(conn))
        {
            Debug.Log("conecttt");
            dbconn.Open(); //Open connection to the database.
            IDbCommand dbcmd = dbconn.CreateCommand();
            Debug.Log("conectinnng");
            string sqlQuery = "SELECT  Id, Name, Points " + "FROM Kids";// table name
            Debug.Log("tableee");
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            Debug.Log("read");
            while (reader.Read())
            {
                Debug.Log("1entra ");
                idreaders = reader.GetInt32(0);
                Debug.Log("2 "+ idreaders);
                Namereaders = reader.GetString(1);
                Debug.Log("3 "+ Namereaders);
                
                //Surnamereaders = reader.GetString(2);
                //Debug.Log("4 " + Surnamereaders);
                //ninios.text += idreaders + Namereaders + Surnamereaders+ "\n";
                pointreaders = reader.GetInt32(2);
                Debug.Log("4 " + pointreaders);
                ninios.text += idreaders + Namereaders +" "  + pointreaders+"\n";
                Debug.Log("Value=" + idreaders + " name =" + Namereaders + "Address="  + "Tablet="+ pointreaders);
            }
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            //       dbconn = null;

        }
    }
    //Search on Database by ID
    private void Search_function(string Search_by_id)
    {
        using (dbconn = new SqliteConnection(conn))
        {
            string Name_readers_Search, Address_readers_Search;
            dbconn.Open(); //Open connection to the database.
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT name,address " + "FROM Staff where id =" + Search_by_id;// table name
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                //  string id = reader.GetString(0);
                Name_readers_Search = reader.GetString(0);
                Address_readers_Search = reader.GetString(1);
                data_staff.text += Name_readers_Search + " - " + Address_readers_Search + "\n";

                Debug.Log(" name =" + Name_readers_Search + "Address=" + Address_readers_Search);

            }
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();


        }

    }


    //Search on Database by ID
    private void F_to_update_function(string Search_by_id)
    {
        using (dbconn = new SqliteConnection(conn))
        {
            string Name_readers_Search, Address_readers_Search;
            dbconn.Open(); //Open connection to the database.
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT name,address " + "FROM Staff where id =" + Search_by_id;// table name
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {

                Name_readers_Search = reader.GetString(0);
                Address_readers_Search = reader.GetString(1);
                t_name.text = Name_readers_Search;
                t_Address.text = Address_readers_Search;

            }
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();


        }

    }
    //Update on  Database 
    private void update_function(string update_id, string update_name, string update_address)
    {
        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open(); //Open connection to the database.
            dbcmd = dbconn.CreateCommand();
            sqlQuery = string.Format("UPDATE Staff set name = @name ,address = @address where ID = @id ");

            SqliteParameter P_update_name = new SqliteParameter("@name", update_name);
            SqliteParameter P_update_address = new SqliteParameter("@address", update_address);
            SqliteParameter P_update_id = new SqliteParameter("@id", update_id);

            dbcmd.Parameters.Add(P_update_name);
            dbcmd.Parameters.Add(P_update_address);
            dbcmd.Parameters.Add(P_update_id);

            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbconn.Close();
            Search_function(t_id.text);
        }

        // SceneManager.LoadScene("home");
    }



    //Delete
    private void Delete_function(string Delete_by_id)
    {
        using (dbconn = new SqliteConnection(conn))
        {

            dbconn.Open(); //Open connection to the database.
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "DELETE FROM Staff where id =" + Delete_by_id;// table name
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();


            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            //data_staff.text = Delete_by_id + " Delete  Done ";

        }
        reader_function();
    }
    // Update is called once per frame
    void Update()
    {

    }
}