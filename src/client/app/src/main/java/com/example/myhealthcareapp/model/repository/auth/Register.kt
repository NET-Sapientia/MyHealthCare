package com.example.myhealthcareapp.model.repository.auth

data class Register(
    val code: Int,
    val error: String,
    val result: RegisterResult?
)