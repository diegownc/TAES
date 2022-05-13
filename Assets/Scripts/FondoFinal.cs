using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using Mono.Data.Sqlite;
using System.Data;

public class FondoFinal : MonoBehaviour
{
    public SpriteRenderer spriteredererFondo;
    public Text Winer;

    private string dbName = "URI=file:Taes.db";

    void Start()
    {
        spriteredererFondo = GetComponent<SpriteRenderer>();
        Winer.enabled = false;
    }

    void Update()
    {
        if (TimeController.finPartido)
        {
            spriteredererFondo.sortingOrder = 0;
            Winer.enabled = true;

            if (GoalPlayer1.score1 > GoalPlayer2.score2)
            {
               IDataReader reader;
               using (var connection = new SqliteConnection(dbName))
               {
                   connection.Open();
                   using (var command = connection.CreateCommand())
                   {
                       command.CommandText = "select nombre from usuarios where puerto=8052";
                       using (reader = command.ExecuteReader())
                       {
                           Winer.text = "GANADOR " + reader["nombre"].ToString();
                       }
                   }
                   connection.Close();
               }

            }
            else if (GoalPlayer1.score1 < GoalPlayer2.score2)
            {
                IDataReader reader;
                using (var connection = new SqliteConnection(dbName))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "select nombre from usuarios where puerto=8051";
                        using (reader = command.ExecuteReader())
                        {
                            Winer.text = "GANADOR " + reader["nombre"].ToString();
                        }
                    }
                    connection.Close();
                }
            }
            else
            {
                //Empate
                Winer.text = "EMPATE";
            }
        }
    }
}
