import sqlite3
import sys 
from time import sleep
from random import randint

try:
    mi_conexion = sqlite3.connect(".\Taes.db")
    cursor = mi_conexion.cursor()

    cursor.execute("select * from juegoseleccionado")
    rowUsuario = cursor.fetchall()
    mi_conexion.commit()

    for row in rowUsuario:
        print(row)

except Exception as ex:
    mi_conexion.close()
    print(ex) 