package com.example.myhealthcareapp.model.response

import com.google.gson.annotations.SerializedName

data class MakeAppointmentResponse (
    @SerializedName("code") val code: Int?,
    @SerializedName("error") val error: String?
)