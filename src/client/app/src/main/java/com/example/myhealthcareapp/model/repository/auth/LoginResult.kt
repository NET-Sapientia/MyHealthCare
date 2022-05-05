package com.example.myhealthcareapp.model.repository.auth

data class LoginResult(
    val id: Int,
    val name: String,
    val email: String,
    val address: String,
    val token: String
)