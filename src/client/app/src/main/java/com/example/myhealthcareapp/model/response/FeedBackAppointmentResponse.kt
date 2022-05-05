package com.example.myhealthcareapp.model.response

import com.google.gson.annotations.SerializedName

data class FeedBackAppointmentResponse (
    @SerializedName("id")
    val id: Int,
    @SerializedName("appointment")
    val appointment: ClientAppointmentResponse,
    @SerializedName("message")
    val message: String,
    @SerializedName("billing")
    val billing: Int
    )