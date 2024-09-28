package com.sena.libreriaapi





import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.*
import androidx.fragment.app.Fragment
import okhttp3.*
import okhttp3.MediaType.Companion.toMediaType
import okhttp3.RequestBody.Companion.toRequestBody
import org.json.JSONArray
import org.json.JSONObject
import java.io.IOException

class CreateFarmFragment : Fragment() {

    private lateinit var etName: EditText
    private lateinit var etDescription: EditText
    private lateinit var etSize: EditText
    private lateinit var etCoordinate: EditText
    private lateinit var spinnerPersona: Spinner
    private lateinit var spinnerCity: Spinner
    private lateinit var btnCreateFarm: Button

    private val client = OkHttpClient()

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        val view = inflater.inflate(R.layout.fragment_create_farm, container, false)

        // Inicializa los campos
        etName = view.findViewById(R.id.et_name)
        etDescription = view.findViewById(R.id.et_description)
        etSize = view.findViewById(R.id.et_size)
        etCoordinate = view.findViewById(R.id.et_coordinate)
        spinnerPersona = view.findViewById(R.id.spinner_persona)
        spinnerCity = view.findViewById(R.id.spinner_ciudad)
        btnCreateFarm = view.findViewById(R.id.btn_create_farm)

                // Carga de datos en spinners
                loadPersonas()

                loadCities()

                                // Acción del botón
                                btnCreateFarm.setOnClickListener {
                                    createFarm()
                                }

        return view
    }

    private fun loadPersonas() {
        val request = Request.Builder()
            .url("http://192.168.1.83:9191/api/Person") // Cambia la URL si es diferente
            .build()

        client.newCall(request).enqueue(object : Callback {
            override fun onFailure(call: Call, e: IOException) {
                activity?.runOnUiThread {
                    Toast.makeText(context, "Error al cargar personas", Toast.LENGTH_SHORT).show()
                }
            }

            override fun onResponse(call: Call, response: Response) {
                response.body?.let { responseBody ->
                    val personasJson = JSONArray(responseBody.string())
                    val personas = mutableListOf<String>()
                    for (i in 0 until personasJson.length()) {
                        val persona = personasJson.getJSONObject(i)
                        personas.add(persona.getString("firstName")) // Ajusta el campo según tu JSON
                    }
                    activity?.runOnUiThread {
                        val adapter = ArrayAdapter(
                            requireContext(),
                            android.R.layout.simple_spinner_item,
                            personas
                        )
                        spinnerPersona.adapter = adapter
                    }
                }
            }
        })
    }

    private fun loadCities() {
        val request = Request.Builder()
            .url("http://192.168.1.83:9191/api/City") // Cambia la URL si es diferente
            .build()

        client.newCall(request).enqueue(object : Callback {
            override fun onFailure(call: Call, e: IOException) {
                activity?.runOnUiThread {
                    Toast.makeText(context, "Error al cargar ciudades", Toast.LENGTH_SHORT).show()
                }
            }

            override fun onResponse(call: Call, response: Response) {
                response.body?.let { responseBody ->
                    val citiesJson = JSONArray(responseBody.string())
                    val cities = mutableListOf<String>()
                    for (i in 0 until citiesJson.length()) {
                        val city = citiesJson.getJSONObject(i)
                        cities.add(city.getString("name")) // Ajusta el campo según tu JSON
                    }
                    activity?.runOnUiThread {
                        val adapter = ArrayAdapter(
                            requireContext(),
                            android.R.layout.simple_spinner_item,
                            cities
                        )
                        spinnerCity.adapter = adapter
                    }
                }
            }
        })
    }

    private fun createFarm() {
        val name = etName.text.toString().trim()
        val description = etDescription.text.toString().trim()
        val size = etSize.text.toString().trim()
        val coordinate = etCoordinate.text.toString().trim()
        val persona = spinnerPersona.selectedItem.toString() // Obtén el ID real de la persona
        val city = spinnerCity.selectedItem.toString() // Obtén el ID real de la ciudad

        // Validaciones simples
        if (name.isEmpty() || description.isEmpty() || size.isEmpty() || coordinate.isEmpty()) {
            Toast.makeText(context, "Por favor complete todos los campos", Toast.LENGTH_SHORT).show()
            return
        }

        val jsonObject = JSONObject().apply {
            put("name", name)
            put("description", description)
            put("sizeMeter", size)
            put("coordinate", coordinate)
            put("personaId", persona) // Asegúrate de usar el ID correcto
            put("cityId", city) // Asegúrate de usar el ID correcto
            put("state", true) // Ajusta según tu lógica
        }

        val jsonMediaType = "application/json".toMediaType() // Utiliza la extensión adecuada
        val requestBody = jsonObject.toString().toRequestBody(jsonMediaType)

        val request = Request.Builder()
            .url("http://192.168.1.83:9191/api/farm") // Cambia la URL si es diferente
            .post(requestBody)
            .build()

        client.newCall(request).enqueue(object : Callback {
            override fun onFailure(call: Call, e: IOException) {
                activity?.runOnUiThread {
                    Toast.makeText(context, "Error al crear la finca", Toast.LENGTH_SHORT).show()
                }
            }

            override fun onResponse(call: Call, response: Response) {
                activity?.runOnUiThread {
                    if (response.isSuccessful) {
                        Toast.makeText(context, "Finca creada exitosamente", Toast.LENGTH_SHORT).show()
                    } else {
                        Toast.makeText(context, "Error al crear la finca", Toast.LENGTH_SHORT).show()
                    }
                }
            }
        })
    }
}
