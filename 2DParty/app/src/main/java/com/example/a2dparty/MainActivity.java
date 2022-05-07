package com.example.a2dparty;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.content.res.Configuration;
import android.os.AsyncTask;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.Toast;

import com.google.android.material.textfield.TextInputEditText;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.Socket;

public class MainActivity extends AppCompatActivity {
    private String ipAddress;
    private int puerto = 8050;

    Button entrar;
    TextInputEditText nombreUser;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        //Paso de parámetros entre activitys
        recibirParametros();

        entrar = (Button) findViewById(R.id.entrar);
        entrar.setOnClickListener(new View.OnClickListener(){
            public void onClick(View v){
                if(nombreUser.getText().toString().isEmpty()){
                    Toast.makeText(getApplicationContext(), "Por favor, escriba su nombre antes de entrar", Toast.LENGTH_SHORT).show();
                }else {
                    //SolicitudEntrar request = new SolicitudEntrar();
                    //request.execute();

                    Intent guardar = new Intent(getApplicationContext(), PrimerMando.class);
                    guardar.putExtra("ip", ipAddress);
                    guardar.putExtra("puerto", 8040);
                    startActivity(guardar);

                }
            }
        });

        nombreUser = (TextInputEditText) findViewById(R.id.nombre);
    }

    public void setIpAddress(String ip){
        if(ip.isEmpty()){
            ipAddress = "172.20.10.7";
        }else{
            ipAddress = ip;
        }
    }

    public void setPuerto(int puerto){
        if(puerto == -1){
            this.puerto = 8050;
        }else{
            this.puerto = puerto;
        }
    }

    public void recibirParametros(){
        try {
            setIpAddress(getIntent().getStringExtra("ip"));
        }catch(Exception e){//EN CASO DE QUE NO RECIBAMOS NINGÚN PARAMETRO
            setIpAddress("");
        }

        try {
            setPuerto(getIntent().getIntExtra("puerto", -1));
        }catch(Exception e){//EN CASO DE QUE NO RECIBAMOS NINGÚN PARAMETRO
            setPuerto(-1);
        }
    }

    //Metodo para mostrar y ocultar menu
    public boolean onCreateOptionsMenu(Menu menu){
        getMenuInflater().inflate(R.menu.overflow, menu);
        return true;
    }

    //Metodo para asignar las funciones correspondientes a las opciones
    public boolean onOptionsItemSelected(MenuItem item){
        int id = item.getItemId();
        if(id == R.id.item1){
            Intent siguiente = new Intent(this, Ajustes.class);
            siguiente.putExtra("ip", ipAddress);
            siguiente.putExtra("puerto", puerto);
            siguiente.putExtra("pantalla", "Login");
            startActivity(siguiente);
        }

        return super.onOptionsItemSelected(item);
    }


    class SolicitudEntrar extends AsyncTask<Void, Void, Void> {
        Socket mysocket;
        PrintWriter pw;
        BufferedReader in;
        protected Void doInBackground(Void...params) {
            try {
                mysocket = new Socket(ipAddress, puerto);
                pw = new PrintWriter(mysocket.getOutputStream());
                in = new BufferedReader(new InputStreamReader(mysocket.getInputStream()));
                System.out.println("LLEGO");
                pw.write(nombreUser.getText().toString());
                pw.flush();
                String userInput = null;
                userInput = in.readLine();
                System.out.println("RECIBIDO: " + userInput);
                String parts [] = userInput.split(":");

                Intent guardar;
                if(parts[0].equals("juego1"))
                    guardar = new Intent(getApplicationContext(), PrimerMando.class);
                else if(parts[0].equals("juego2"))
                    guardar = new Intent(getApplicationContext(), SegundoMando.class);
                else
                    guardar = new Intent(getApplicationContext(), PrimerMando.class);

                guardar.putExtra("ip", ipAddress);
                guardar.putExtra("puerto", parts[1]);
                startActivity(guardar);

                pw.close();
                in.close();
                mysocket.close();

            } catch (Exception e) {
                System.out.println("ERROR" + e.getMessage());
                try{
                    if(mysocket != null)
                        mysocket.close();

                    if(in != null)
                        in.close();

                    if(pw != null)
                        pw.close();
                }catch(Exception ex){
                    System.out.println("ERROR CERRANDO:" + ex.getMessage());
                }
            }
            return null;
        }
    }

    /*
    public void onConfigurationChanged(Configuration newConfig) {
        super.onConfigurationChanged(newConfig);
        Toast.makeText(this, "TEST", Toast.LENGTH_SHORT).show();
    }
    */
}