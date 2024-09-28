package com.sena.libreriaapi

import android.app.DatePickerDialog
import android.content.Intent
import android.os.Bundle
import android.view.View
import android.widget.*
import androidx.appcompat.app.AppCompatActivity
import com.android.volley.Request
import com.android.volley.Response
import com.android.volley.toolbox.JsonArrayRequest
import com.android.volley.toolbox.JsonObjectRequest
import com.android.volley.toolbox.Volley
import com.sena.libreriaapi.Entity.City
import com.sena.libreriaapi.adapter.CityAdapter
import com.sena.libreriaapi.config.Url
import org.json.JSONArray
import org.json.JSONObject
import java.util.*

class RegistroActivity : AppCompatActivity() {

    private lateinit var citySpinner: Spinner
    private lateinit var genderSpinner: Spinner
    private lateinit var typeDocumentSpinner: Spinner
    private lateinit var etDateOfBirth: EditText
    private lateinit var etNumberDocument: EditText
    private lateinit var cities: List<City>
    private lateinit var successMessage: TextView
    private lateinit var createUserButton: Button

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_registro)

        // Inicializar campos
        etDateOfBirth = findViewById(R.id.etDateOfBirth)
        etNumberDocument = findViewById(R.id.etNumberDocument)
        genderSpinner = findViewById(R.id.spinnerGender)
        typeDocumentSpinner = findViewById(R.id.spinnerTypeDocument)
        citySpinner = findViewById(R.id.citySpinner)
        successMessage = findViewById(R.id.tvSuccessMessage)
        createUserButton = findViewById(R.id.btnCreateUser)

        // Cargar opciones en Spinner de Género
        val genderOptions = arrayOf("Masculino", "Femenino")
        genderSpinner.adapter = ArrayAdapter(this, android.R.layout.simple_spinner_dropdown_item, genderOptions)

        // Cargar opciones en Spinner de Tipo de Documento
        val typeDocumentOptions = arrayOf("Cédula de Ciudadanía", "Tarjeta de Identidad", "Cédula de Extranjería", "Pasaporte")
        typeDocumentSpinner.adapter = ArrayAdapter(this, android.R.layout.simple_spinner_dropdown_item, typeDocumentOptions)

        // Selección de fecha para el campo Fecha de Nacimiento
        etDateOfBirth.setOnClickListener {
            showDatePickerDialog()
        }

        loadCities()

        // Acción del botón de registro
        findViewById<Button>(R.id.btnRegister).setOnClickListener {
            registerPerson()
        }

        // Acción del botón para crear usuario después del registro
        createUserButton.setOnClickListener {
            // Iniciar la actividad para crear usuario
            val intent = Intent(this, CreateUserActivity::class.java)
            startActivity(intent)
        }
    }

    private fun showDatePickerDialog() {
        val calendar = Calendar.getInstance()
        val year = calendar.get(Calendar.YEAR)
        val month = calendar.get(Calendar.MONTH)
        val day = calendar.get(Calendar.DAY_OF_MONTH)

        val datePickerDialog = DatePickerDialog(this, { _, selectedYear, selectedMonth, selectedDay ->
            // Formato AAAA-MM-DD
            val formattedDate = "$selectedYear-${String.format("%02d", selectedMonth + 1)}-${String.format("%02d", selectedDay)}"
            etDateOfBirth.setText(formattedDate)
        }, year, month, day)

        datePickerDialog.show()
    }

    private fun loadCities() {
        val url = Url.CITY_URL
        val requestQueue = Volley.newRequestQueue(this)

        val jsonArrayRequest = JsonArrayRequest(
            Request.Method.GET, url, null,
            Response.Listener { response ->
                cities = parseCities(response)
                val adapter = CityAdapter(this, cities)
                citySpinner.adapter = adapter
            },
            Response.ErrorListener { error ->
                Toast.makeText(this, "Error al cargar ciudades", Toast.LENGTH_SHORT).show()
            }
        )

        requestQueue.add(jsonArrayRequest)
    }

    private fun parseCities(response: JSONArray): List<City> {
        val cityList = mutableListOf<City>()
        for (i in 0 until response.length()) {
            val cityObject = response.getJSONObject(i)
            val city = City(
                cityObject.getString("name"),
                cityObject.getInt("id")
            )
            cityList.add(city)
        }
        return cityList
    }

    private fun registerPerson() {
        val url = Url.PERSON_URL
        val requestQueue = Volley.newRequestQueue(this)

        // Obtener valores de los campos
        val firstName = findViewById<EditText>(R.id.etFirstName).text.toString()
        val secondName = findViewById<EditText>(R.id.etSecondName).text.toString()
        val firstLastName = findViewById<EditText>(R.id.etFirstLastName).text.toString()
        val secondLastName = findViewById<EditText>(R.id.etSecondLastName).text.toString()
        val email = findViewById<EditText>(R.id.etEmail).text.toString()
        val dateOfBirth = etDateOfBirth.text.toString()
        val gender = genderSpinner.selectedItem.toString()
        val typeDocument = typeDocumentSpinner.selectedItem.toString()
        val numberDocument = etNumberDocument.text.toString()
        val cityId = (citySpinner.selectedItem as City).id

        if (firstName.isEmpty() || email.isEmpty() || dateOfBirth.isEmpty() || gender.isEmpty() || typeDocument.isEmpty() || numberDocument.isEmpty()) {
            Toast.makeText(this, "Por favor, completa todos los campos requeridos.", Toast.LENGTH_SHORT).show()
            return
        }

        val personObject = JSONObject().apply {
            put("firstName", firstName)
            put("secondName", secondName)
            put("firstLastName", firstLastName)
            put("secondLastName", secondLastName)
            put("email", email)
            put("dateOfBirth", dateOfBirth)
            put("gender", gender)
            put("typeDocument", typeDocument)
            put("numberDocument", numberDocument)
            put("cityId", cityId)
            put("state", true)
        }

        val jsonObjectRequest = JsonObjectRequest(
            Request.Method.POST, url, personObject,
            Response.Listener { response ->
                successMessage.text = "¡Registro exitoso!"
                successMessage.visibility = View.VISIBLE
                createUserButton.visibility = View.VISIBLE
                Toast.makeText(this, "Persona registrada con éxito.", Toast.LENGTH_SHORT).show()
            },
            Response.ErrorListener { error ->
                error.printStackTrace()
                Toast.makeText(this, "Error al registrar persona: ${error.message}", Toast.LENGTH_SHORT).show()
            }
        )

        requestQueue.add(jsonObjectRequest)
    }
}
