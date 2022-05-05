package com.example.myhealthcareapp.model.response

import com.google.gson.annotations.SerializedName

data class MakeAppointment (
    @SerializedName("clientId")
    val clientId : Int,
    @SerializedName("departmentId")
    val departmentId : Int,
    @SerializedName("medicId")
    val medicId : Int,
    @SerializedName("startDate")
    val startDate : String,
    @SerializedName("endDate")
    val endDate : String,
    @SerializedName("notes")
    val notes : String
)
