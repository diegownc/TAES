using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;

public class InsertarJugadores : MonoBehaviour
{
    //Thread PonerNombres;

    private int maximoJugadores;
    public Text nombreDelJuego;
    public Text jugador1;
    public Text jugador2;
    public Text jugador3;
    public Text jugador4;
    public Text jugador5;
    public Text jugador6;
    private string dbName = "URI=file:Taes.db";

    private void nombreDeLosJuegos()
    {
        if (nombreDelJuego.text == "Extreme Football")
        {
            maximoJugadores = 2;
            jugador2.text = "";
            jugador4.text = "";
            jugador5.text = "";
            jugador6.text = "";
        }
        else
            maximoJugadores = 6;
    }

    public void CrearJugadores()
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
                    if (jugador1.text == "Esperando Jugador..." || jugador1.text == reader["nombre"].ToString())
                        jugador1.text = reader["nombre"].ToString();
                    else if ((jugador2.text == "Esperando Jugador..." || jugador2.text == reader["nombre"].ToString()) 
                        && maximoJugadores > 2)
                        jugador2.text = reader["nombre"].ToString();
                    else if (jugador3.text == "Esperando Jugador..." || jugador3.text == reader["nombre"].ToString())
                        jugador3.text = reader["nombre"].ToString();
                    else if ((jugador4.text == "Esperando Jugador..." || jugador4.text == reader["nombre"].ToString())
                        && maximoJugadores > 2)
                        jugador4.text = reader["nombre"].ToString();
                    else if ((jugador5.text == "Esperando Jugador..." || jugador5.text == reader["nombre"].ToString())
                        && maximoJugadores > 2)
                        jugador5.text = reader["nombre"].ToString();
                    else if ((jugador6.text == "Esperando Jugador..." || jugador6.text == reader["nombre"].ToString())
                        && maximoJugadores > 2)
                        jugador6.text = reader["nombre"].ToString();
                }
            }

            reader.Close();

            connection.Close();
        }
    }

    void Start()
    {
        nombreDeLosJuegos();
        InvokeRepeating("CrearJugadores", 0, 1);
    }

    private void Update()
    {
        //CrearJugadores();
    }
}
