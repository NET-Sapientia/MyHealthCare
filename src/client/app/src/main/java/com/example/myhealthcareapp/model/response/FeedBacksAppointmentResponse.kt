package com.example.myhealthcareapp.model.response

import com.google.gson.annotations.SerializedName

data class FeedBacksAppointmentResponse (
    @SerializedName("data")
    val data: List<FeedBackAppointmentResponse>
)