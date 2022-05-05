package com.example.myhealthcareapp.data.repository

import com.example.myhealthcareapp.data.v2.MyHealthCareInstanceV2
import com.example.myhealthcareapp.model.response.HospitalData
import com.example.myhealthcareapp.model.response.toModel

class ContentRepository(private val instance: MyHealthCareInstanceV2) {

    suspend fun getHospitals(): List<HospitalData>? = instance.api.getHospitals().body()?.result?.mapNotNull { it.toModel() }
}