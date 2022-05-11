using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;

public class PruebaBDD : MonoBehaviour
{
    private string dbName = "URI=file:Taes.db";
    // Start is called before the first frame update
    void Start()
    {
        IDataReader reader;
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "select nombre from usuarios where puerto=8075";
                using (reader = command.ExecuteReader())
                {
                    Debug.Log(reader["nombre"].ToString());
                }
            }
            connection.Close();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
