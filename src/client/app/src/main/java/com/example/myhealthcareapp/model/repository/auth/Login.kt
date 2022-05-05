package com.example.myhealthcareapp.model.repository.auth

import com.google.gson.annotations.SerializedName

data class Login(
    val code : Int,
    val error : String,
    val result : LoginResult?
)