package com.example.myhealthcareapp.model.response

import com.google.gson.annotations.SerializedName

data class MedicalDepartmentResponse (
    @SerializedName("data")
    val data : List<MedicalDepartment>
)