package com.example.myhealthcareapp.model.request

import com.google.gson.annotations.SerializedName

data class RegisterRequest(
    @SerializedName("address") val address : String,
    @SerializedName("email") val email : String,
    @SerializedName("name") val name : String,
    @SerializedName("password") val password : String,
)