package com.example.myhealthcareapp.data.repository

import com.example.myhealthcareapp.data.v2.MyHealthCareInstanceV2
import com.example.myhealthcareapp.model.repository.auth.Login
import com.example.myhealthcareapp.model.repository.auth.Register
import com.example.myhealthcareapp.model.request.LoginRequest
import com.example.myhealthcareapp.model.request.RegisterRequest
import com.example.myhealthcareapp.model.response.toModel
import java.lang.IllegalStateException

class AuthenticationRepository(private val instance: MyHealthCareInstanceV2) {

    suspend fun loginClient(email: String, password: String): Login? = instance.api.loginClient(
        loginRequest = LoginRequest(email = email, password = password)
    ).body()?.toModel()

    suspend fun registerClient(
        address: String,
        name: String,
        email: String,
        password: String
    ): Register? = instance.api.registerClient(
        registerRequest = RegisterRequest(
            address = address,
            name = name,
            email = email,
            password = password
        )
    ).body()?.toModel()
}