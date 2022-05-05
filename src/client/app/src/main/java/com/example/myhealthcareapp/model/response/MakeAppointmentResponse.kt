package com.example.myhealthcareapp.model.response

import com.google.gson.annotations.SerializedName

data class MakeAppointmentResponse (
    @SerializedName("Result")
    val result: String
    )