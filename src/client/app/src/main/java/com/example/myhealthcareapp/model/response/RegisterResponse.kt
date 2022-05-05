package com.example.myhealthcareapp.model.response

import com.example.myhealthcareapp.model.repository.auth.LoginResult
import com.example.myhealthcareapp.model.repository.auth.Register
import com.example.myhealthcareapp.model.repository.auth.RegisterResult
import com.google.gson.annotations.SerializedName

data class RegisterResponse (
    @SerializedName("code") val code: Int?,
    @SerializedName("error") val error: String?,
    @SerializedName("result") val result: RegisterResultResponse,
)

data class RegisterResultResponse(
    @SerializedName("id") val id : Int?,
    @SerializedName("name") val name : String?,
    @SerializedName("email") val email : String?,
    @SerializedName("address") val address : String?
)

fun RegisterResponse.toModel() = when {
    code == null || error == null -> null
    else -> Register(
        code = code,
        error = error,
        result = result.toModel()
    )
}

fun RegisterResultResponse.toModel() = when {
    id == null || name == null || address == null || email == null -> null
    else -> RegisterResult(
        id = id,
        name = name,
        email = email,
        address = address
    )
}