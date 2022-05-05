package com.example.myhealthcareapp.model.response

import com.google.gson.annotations.SerializedName

data class ClientAppointments (
    @SerializedName("data")
    val data : List<ClientAppointmentResponse>
    )