package com.example.myhealthcareapp.data.v2

import com.example.myhealthcareapp.model.request.LoginRequest
import com.example.myhealthcareapp.model.request.RegisterRequest
import com.example.myhealthcareapp.model.response.HospitalResponse
import com.example.myhealthcareapp.model.response.LoginResponse
import com.example.myhealthcareapp.model.response.RegisterResponse
import retrofit2.http.Body
import retrofit2.http.POST
import retrofit2.Response
import retrofit2.http.GET

interface MyHealthCareApiV2 {
    @POST("/api/Clients/Login")
    suspend fun loginClient(@Body loginRequest: LoginRequest) : Response<LoginResponse>

    @POST("/api/Clients/Register")
    suspend fun registerClient(@Body registerRequest: RegisterRequest) : Response<RegisterResponse>

    @GET("/api/Hospitals/Hospitals")
    suspend fun getHospitals() : Response<HospitalResponse>
}