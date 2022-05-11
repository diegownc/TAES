using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Diagnostics;
using System.Text;
using Mono.Data.Sqlite;
using System.Data;

public class LoginJuego1 : MonoBehaviour
{
    Thread leerJugadores;

    private string dbName = "URI=file:Taes.db";
    private ArrayList listaJugadores;
    private int NumMAXJugadores = 2;
    private string juegoSeleccionado = "juego1"; //la app m�vil espera "juego1" o "juego2" o "juego3" para saber que controles mostrar

    // Start is called before the first frame update
    void Start()
    {
        listaJugadores = new ArrayList();
        CreateDB();
        leerJugadores = new Thread(new ThreadStart(leerUsuarios));
        leerJugadores.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CreateDB()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS usuarios (nombre VARCHAR(45), ip VARCHAR(45), puerto int)";
                command.ExecuteNonQuery();
                command.CommandText = "DELETE FROM juegoseleccionado";
                command.ExecuteNonQuery();
                /*
                command.CommandText = "CREATE TABLE IF NOT EXISTS juegoseleccionado (nombre VARCHAR(45), numJugadores int, empezar int)";
                command.ExecuteNonQuery();

                //Borro partidas anteriores
                command.CommandText = "DELETE FROM juegoseleccionado";
                command.ExecuteNonQuery();

                //Empezamos una nueva partida
                command.CommandText = "INSERT INTO juegoseleccionado (nombre, numJugadores, empezar) values ('" + juegoSeleccionado + "', " + NumMAXJugadores + ", 0)";
                command.ExecuteNonQuery();*/
            }
            connection.Close();
        }
    }


    public void leerUsuarios()
    {
        IDataReader reader;
        IDataReader reader2;

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                //Me espero a que el servidor (server.py) este listo para atender a los usuarios
                /*while (true)
                {
                    command.CommandText = "select * from juegoseleccionado";
                    using (reader = command.ExecuteReader())
                    {
                        if (reader["empezar"].ToString() == "1")
                        {
                            UnityEngine.Debug.Log("Empezar� a leer los usuarios");

                            reader.Close();
                            reader.Dispose();
                            command.CommandText = "DELETE FROM juegoseleccionado";
                            command.ExecuteNonQuery();
                            break;
                        }
                    }

                    //Me espero 5 segundos hasta hacer la pr�xima select
                    Thread.Sleep(5000);
                }*/

                while (true)
                {
                    command.CommandText = "select * from usuarios";
                    using (reader2 = command.ExecuteReader())
                    {
                        while (reader2.Read())
                        {
                            if (!listaJugadores.Contains(reader2["ip"].ToString()))
                            { //Si no esta dentro de la lista...
                                listaJugadores.Add(reader2["ip"].ToString());

                            }
                        }
                    }

                    //Me espero 5 segundos hasta hacer la pr�xima select...
                    Thread.Sleep(5000);
                    UnityEngine.Debug.Log(listaJugadores.Count);

                    //Si ya hemos llegado al limite de jugadores nos salimos
                    if (listaJugadores.Count == NumMAXJugadores)
                        break;

                }

                reader2.Close();
                reader2.Dispose();
            }

            connection.Close();
        }
    }
}