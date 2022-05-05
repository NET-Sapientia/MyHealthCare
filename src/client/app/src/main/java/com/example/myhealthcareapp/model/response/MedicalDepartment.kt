package com.example.myhealthcareapp.model.response

import com.google.gson.annotations.SerializedName

data class MedicalDepartment(
    @SerializedName("id")
    val id: Int,
    @SerializedName("name")
    val name: String,
    @SerializedName("contact")
    val contact: String
)