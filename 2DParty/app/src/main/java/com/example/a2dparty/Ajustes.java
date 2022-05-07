package com.example.a2dparty;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

import com.google.android.material.textfield.TextInputEditText;

public class Ajustes extends AppCompatActivity {
    Button btn;
    TextInputEditText inputText;
    TextInputEditText puerto;
    String pantalla;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_ajustes);
        btn = (Button) findViewById(R.id.back);
        inputText = (TextInputEditText) findViewById(R.id.inputText);
        puerto = (TextInputEditText) findViewById(R.id.puerto);

        try{
            String ipActivity1 = getIntent().getStringExtra("ip");
            if(!ipActivity1.isEmpty()){ //Si no está vacio....
                inputText.setText(ipActivity1);
            }

            int puertoActivity1 = getIntent().getIntExtra("puerto", -1);
            if(puertoActivity1 != -1){
                puerto.setText(Integer.toString(puertoActivity1));
            }

            pantalla = getIntent().getStringExtra("pantalla");

        }catch(Exception e){
            //DE MOMENTO NO HAGO NADA
        }


        btn.setOnClickListener(new View.OnClickListener(){
            public void onClick(View v){
                Intent guardar;
                if (pantalla.equals("MandoUno"))
                    guardar = new Intent(getApplicationContext(), PrimerMando.class);
                else if (pantalla.equals("MandoDos"))
                    guardar = new Intent(getApplicationContext(), SegundoMando.class);
                else
                    guardar = new Intent(getApplicationContext(), MainActivity.class);
                String ipSinEspacios =  inputText.getText().toString().replaceAll("\\s+","");;
                guardar.putExtra("ip", ipSinEspacios);
                guardar.putExtra("puerto", Integer.parseInt(puerto.getText().toString()));
                startActivity(guardar);
            }
        });
    }
}