package com.sena.libreriaapi

import android.os.Bundle
import android.view.MenuItem
import android.widget.PopupMenu
import androidx.appcompat.app.AppCompatActivity
import androidx.navigation.findNavController
import com.google.android.material.bottomnavigation.BottomNavigationView

class InicioActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_inicio)

        val navController = findNavController(R.id.fragmentContainer)
        val bottomNavigationView = findViewById<BottomNavigationView>(R.id.bottomNavigationView)

        bottomNavigationView.setOnNavigationItemSelectedListener { item ->
            when (item.itemId) {
                R.id.homeFragment -> {
                    navController.navigate(R.id.homeFragment)
                    true
                }
                R.id.gestionFragment -> {
                    showGestionMenu(item)
                    true
                }
                R.id.reportesFragment -> {
                    navController.navigate(R.id.reportesFragment)
                    true
                }
                else -> false
            }
        }
    }

    private fun showGestionMenu(menuItem: MenuItem) {
        // Crear el PopupMenu
        val popupMenu = PopupMenu(this, findViewById(menuItem.itemId))
        popupMenu.menuInflater.inflate(R.menu.gestion_submenu, popupMenu.menu)

        popupMenu.setOnMenuItemClickListener { subItem ->
            val navController = findNavController(R.id.fragmentContainer)
            when (subItem.itemId) {
                R.id.crearFinca -> {
                    navController.navigate(R.id.crearFincaFragment)
                    true
                }
                R.id.verFinca -> {
                    navController.navigate(R.id.verFincaFragment)
                    true
                }
                R.id.recolecciones -> {
                    navController.navigate(R.id.recoleccionesFragment)
                    true
                }
                else -> false
            }
        }
        popupMenu.show()
    }
}
