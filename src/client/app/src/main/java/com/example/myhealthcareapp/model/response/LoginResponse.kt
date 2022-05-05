package com.example.myhealthcareapp.model.response

import com.example.myhealthcareapp.model.repository.auth.Login
import com.example.myhealthcareapp.model.repository.auth.LoginResult
import com.google.gson.annotations.SerializedName

data class LoginResponse(
    @SerializedName("code") val code : Int?,
    @SerializedName("error") val error : String?,
    @SerializedName("result") val result : LoginResultResponse
)

data class LoginResultResponse(
    @SerializedName("id") val id : Int?,
    @SerializedName("name") val name : String?,
    @SerializedName("email") val email : String?,
    @SerializedName("address") val address : String?,
    @SerializedName("token") val token : String?,
)

fun LoginResponse.toModel() = when {
    code == null || error == null -> null
    else -> Login(
        code = code,
        error = error,
        result = result.toModel()
    )
}

fun LoginResultResponse.toModel() = when {
    id == null || name == null || address == null || token == null || email == null -> null
    else -> LoginResult(
        id = id,
        name = name,
        email = email,
        address = address,
        token = token
    )
}