package com.example.myhealthcareapp.data.v2

import com.example.myhealthcareapp.model.request.LoginRequest
import com.example.myhealthcareapp.model.request.RegisterRequest
import com.example.myhealthcareapp.model.response.*
import retrofit2.http.Body
import retrofit2.http.POST
import retrofit2.Response
import retrofit2.http.GET
import retrofit2.http.Path

interface MyHealthCareApiV2 {
    @POST("/api/Clients/Login")
    suspend fun loginClient(@Body loginRequest: LoginRequest) : Response<LoginResponse>

    @POST("/api/Clients/Register")
    suspend fun registerClient(@Body registerRequest: RegisterRequest) : Response<RegisterResponse>

    @GET("/api/Hospitals/Hospitals")
    suspend fun getHospitals() : Response<HospitalResponse>

    @GET("/api/Hospitals/HospitalsDepartments/{hospitalId}")
    suspend fun getHospitalDepartments(@Path("hospitalId") hospitalId: Int) : Response<MedicalDepartmentResponse>

    @GET("/api/Departments/{departmentId}")
    suspend fun getMedics(@Path("departmentId") departmentId: Int) : Response<MedicsResponse>

    @POST("/api/Appointments")
    suspend fun makeAppointment(@Body makeAppointment: MakeAppointment): Response<MakeAppointmentResponse>

    @GET("/api/Appointments/ClientAppointments/{clientId}")
    suspend fun getClientAppointments(@Path("clientId") clientId: Int): Response<ClientAppointmentResponse>
}