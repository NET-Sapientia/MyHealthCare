package com.example.myhealthcareapp.cache

import android.content.Context
import android.content.SharedPreferences

class SharedPreferencesManager(context: Context) {

    private val sharedPref = context.getSharedPreferences("SharedPref", Context.MODE_PRIVATE)

    fun saveUser(id: Int, name: String, email: String, address: String) {
        with (sharedPref.edit()) {
            putInt(USER_ID, id)
            putString(USER_NAME, name)
            putString(USER_EMAIL, email)
            putString(USER_ADDRESS, address)
            apply()
        }
    }

    fun deleteUser() {
        sharedPref.edit().clear().clear().apply()
    }

    fun getUserId() = sharedPref.getInt(USER_ID, -1)

    fun getUser() = User(
        id = getUserId(),
        name = sharedPref.getString(USER_NAME, "") ?: "",
        email = sharedPref.getString(USER_EMAIL, "") ?: "",
        address = sharedPref.getString(USER_ADDRESS, "") ?: "",
    )

    companion object {
        private const val USER_ID = "UserId"
        private const val USER_NAME = "UserName"
        private const val USER_EMAIL = "UserEmail"
        private const val USER_ADDRESS = "UserAddress"
    }
}

data class User(
    val id: Int,
    val name: String,
    val address: String,
    val email: String
)