import socket
import sys 
import sqlite3

from time import sleep

contadorUsuarios = 0
juego = "juegox"
numMaxJugadores = 0

try:
    mi_conexion = sqlite3.connect("..\Taes.db")
    cursor=mi_conexion.cursor()

    #Vacio la tabla usuarios ya que es una nueva partida...
    cursor.execute("DELETE from usuarios")
    mi_conexion.commit()
    
    #Se queda en bucle infinito hasta que haya alguna fila en esta tabla
    while True:
        cursor.execute("select * from juegoseleccionado")
        rowJuego = cursor.fetchall()
        if len(rowJuego) > 0:
            juego = rowJuego[0][0]
            numMaxJugadores = int(rowJuego[0][1])

            cursor.execute("update juegoseleccionado set empezar=1")
            mi_conexion.commit()
            break

        #Me espero 5 segundos hasta hacer la próxima select
        sleep(5)

    #Sockets
    #hostname = socket.gethostname()
    #HOST = socket.gethostbyname(hostname)

    s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    s.connect(("8.8.8.8", 80))
    HOST = s.getsockname()[0]
    s.close()

    PORT = 8050

    print("Empiezo a escuchar clientes de la app móvil...")
    print("IP: " + HOST)
    print("PUERTO: " + str(PORT))
    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
        s.bind((HOST, PORT))
        s.listen(20)

        while True:
            try:
                conn, addr = s.accept()

                with conn:
                    while True:
                        data = conn.recv(1024)
                        if not data:
                            break
                        cursor.execute("select * from juegoseleccionado")
                        rowJuego = cursor.fetchall()
                        juego = rowJuego[0][0]
                        numMaxJugadores = int(rowJuego[0][1])

                        #Compruebo si el usuario que ha entrado ya esta en la base de datos...
                        cursor.execute("select * from usuarios where ip='"+ addr[0] + "'")
                        rowUsuario = cursor.fetchall()
                        usuarioExiste = len(rowUsuario) > 0

                        #Si el usuario es nuevo....
                        if not usuarioExiste:
                            cursor.execute("Select * from usuarios")
                            rows = cursor.fetchall()
                            contadorUsuarios = len(rows)

                            if contadorUsuarios >= numMaxJugadores: #No insertamos un nuevo usuario...
                                conn.sendall(b"Failed: max users.") #Le enviamos la respuesta al cliente    
                                break

                            puertoUno = False
                            puertoDos = False
                            puertoTres = False
                            puertoCuatro = False
                            puertoCinco = False
                            puertoSeis = False

                            #Voy comprobando por cada usuario que puerto esta libre
                            for row in rows:
                                if row.__contains__(8051):
                                    puertoUno = True
                                elif row.__contains__(8052):
                                    puertoDos = True
                                elif row.__contains__(8053):
                                    puertoTres = True
                                elif row.__contains__(8054):
                                    puertoCuatro = True
                                elif row.__contains__(8055):
                                    puertoCinco = True
                                elif row.__contains__(8056):
                                    puertoSeis = True

                            puertoUsuario = 1111
                            if not puertoUno:
                                puertoUsuario = 8051
                            elif not puertoDos:
                                puertoUsuario = 8052
                            elif not puertoTres:
                                puertoUsuario = 8053
                            elif not puertoCuatro:
                                puertoUsuario = 8054
                            elif not puertoCinco:
                                puertoUsuario = 8055
                            elif not puertoSeis:
                                puertoUsuario = 8056
                            
                            print(str(data))
                            
                            #Quito el b''
                            nombreUser = str(data)[2:len(str(data))-1] 
                            cursor.execute("INSERT INTO usuarios (nombre, ip, puerto) VALUES ('" + nombreUser + "','" + addr[0] + "'," + str(puertoUsuario) + ");")                      
                            mi_conexion.commit()

                        else: #Como el usuario ya existe, solo le reenvio el puerto que tenía asignado...
                            #Quito el b''
                            nombreUser = str(data)[2:len(str(data))-1] 
                            puertoUsuario = int(rowUsuario[0][2])
                            cursor.execute("UPDATE usuarios set nombre='" + nombreUser + "' where ip = '" + addr[0] + "'")
                            mi_conexion.commit()

                        #Enviamos al usuario el juego seleccionado y el puerto que tiene asignado..
                        cadena = juego + ":" + str(puertoUsuario)
                        conn.sendall(cadena.encode("utf-8"))
                        print("Ok")
                        break
            except Exception as e:
                s.close()
                print(e)
                cursor.close()
                mi_conexion.close()
                break

except Exception as ex:
    mi_conexion.close()
    print(ex)
