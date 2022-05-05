package com.example.myhealthcareapp

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.MenuItem
import android.view.View
import android.widget.SearchView
import androidx.fragment.app.Fragment
import com.example.myhealthcareapp.cache.SharedPreferencesManager
import com.example.myhealthcareapp.data.v1.MyHealthCareInstance
import com.example.myhealthcareapp.data.v1.MyHealthCareRepository
import com.example.myhealthcareapp.data.v1.MyHealthCareViewModel
import com.example.myhealthcareapp.fragments.login.LoginFragment
import com.example.myhealthcareapp.fragments.makeAppointment.HospitalListFragment
import com.example.myhealthcareapp.fragments.myAppointments.MyAppointmentsFragment
import com.example.myhealthcareapp.fragments.profile.ProfileFragment
import com.google.firebase.auth.FirebaseAuth
import com.google.firebase.firestore.FirebaseFirestore
import com.google.firebase.firestore.ktx.firestore
import com.google.firebase.ktx.Firebase
import kotlinx.android.synthetic.main.activity_main.*
import org.koin.android.ext.android.inject

class MainActivity : AppCompatActivity() {
    lateinit var searchView : SearchView
    lateinit var searchIcon: MenuItem
    lateinit var profileIcon: MenuItem
    lateinit var viewModel: MyHealthCareViewModel

    private val sharedPreferencesManager: SharedPreferencesManager by inject()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        viewModel = MyHealthCareViewModel(MyHealthCareRepository(MyHealthCareInstance()))
        bottomNavigation.visibility = View.GONE
        searchView = findViewById(R.id.search_button)
        searchIcon = topAppBar.menu.findItem(R.id.search)
        profileIcon = topAppBar.menu.findItem(R.id.profile)

        initBottomNavigation()
        topAppBar.visibility = View.GONE
        if (sharedPreferencesManager.getUserId() <= 0){
            replaceFragment(LoginFragment(), R.id.fragment_container)
        }
        else{
            replaceFragment(HospitalListFragment(), R.id.fragment_container)
        }
    }

    fun replaceFragment(fragment: Fragment, containerId: Int, addToBackStack:Boolean = false, withAnimation:Boolean = false){
        val transaction = supportFragmentManager.beginTransaction()
        transaction.replace(containerId, fragment)
        if (addToBackStack) {
            transaction.addToBackStack(null)
        }
        transaction.commit()
    }

    private fun initBottomNavigation(){
        bottomNavigation.setOnItemSelectedListener {item ->
            val fragment = supportFragmentManager.findFragmentById(R.id.fragment_container)
            when(item.itemId) {
                R.id.make_appointment -> {
                    if(fragment !is HospitalListFragment)
                        replaceFragment(HospitalListFragment(), R.id.fragment_container)
                    true
                }
                R.id.my_appointments -> {
                    if(fragment !is MyAppointmentsFragment)
                        replaceFragment(MyAppointmentsFragment(), R.id.fragment_container)
                    true
                }
                else -> false
            }
        }
    }

    fun setProfileIconListener(){
        profileIcon.setOnMenuItemClickListener {
            replaceFragment(ProfileFragment(), R.id.fragment_container, addToBackStack = true)
            true
        }
    }
}