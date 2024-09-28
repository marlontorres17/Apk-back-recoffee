package com.sena.libreriaapi.config

object Config {
    object urls {
        const val LOGIN_URL = "http://192.168.1.83:9191/api/Login/login"

        const val BASE_URL = "http://10.192.88.44:9191/api/"
        const val PERSON_URL = BASE_URL + "Person"
        const val CITY_URL = BASE_URL + "City"
    }
}