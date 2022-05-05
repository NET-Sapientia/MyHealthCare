package com.example.myhealthcareapp.data.repository

import com.example.myhealthcareapp.data.v2.MyHealthCareInstanceV2
import com.example.myhealthcareapp.model.response.*

class ContentRepository(private val instance: MyHealthCareInstanceV2) {

    suspend fun getHospitals(): List<HospitalData>? = instance.api.getHospitals().body()?.result?.mapNotNull { it.toModel() }

    suspend fun getMedicalDepartments(id: Int): List<MedicalDepartment>? = instance.api.getHospitalDepartments(hospitalId = id).body()?.result?.mapNotNull { it.toModel() }

    suspend fun getMedics(id: Int): List<Medic>? = instance.api.getMedics(departmentId = id).body()?.result?.mapNotNull { it.toModel() }

    suspend fun makeAppointment(appointment: MakeAppointment) = instance.api.makeAppointment(makeAppointment = appointment).body()?.error

    suspend fun getClientAppointments(id: Int) = instance.api.getClientAppointments(clientId = id).body()?.result?.mapNotNull { it.toModel() }
}