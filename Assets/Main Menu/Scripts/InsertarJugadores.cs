using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System;
using System.Threading;

public class InsertarJugadores : MonoBehaviour
{
    private Thread PonerNombres;

    private int maximoJugadores;
    public Text jugador1;
    public Text jugador2;
    public Text jugador3;
    public Text jugador4;
    public Text jugador5;
    public Text jugador6;
    private string dbName = "URI=file:Taes.db";

    void Start()
    {
        PonerNombres = new Thread(new ThreadStart(CrearJugadores));
        PonerNombres.Start();       
    }

    private void Update()
    {
    }

    public void CrearJugadores()
    {
        int contador = 0;

        while (true)
        {
            using (var connection = new SqliteConnection(dbName))
            {
                connection.Open();

                string command = "SELECT * FROM usuarios";
                using var cmd = new SqliteCommand(command, connection);

                IDataReader reader;

                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (jugador1.text == "Esperando Jugador...")
                            jugador1.text = reader["nombre"].ToString();
                        else if(jugador2.text == "Esperando Jugador...")
                            jugador2.text = reader["nombre"].ToString();
                        else if (jugador3.text == "Esperando Jugador...")
                            jugador3.text = reader["nombre"].ToString();
                        else if (jugador4.text == "Esperando Jugador...")
                            jugador4.text = reader["nombre"].ToString();
                        else if (jugador5.text == "Esperando Jugador...")
                            jugador5.text = reader["nombre"].ToString();
                        else if (jugador6.text == "Esperando Jugador...")
                            jugador6.text = reader["nombre"].ToString();
                    }
                }
                reader.Close();
                connection.Close();

                Thread.Sleep(2000);
            }
        }
    }
}
