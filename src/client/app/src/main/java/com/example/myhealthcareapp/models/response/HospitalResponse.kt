package com.example.myhealthcareapp.models.response

import com.google.gson.annotations.SerializedName

data class HospitalResponse(
    @SerializedName("data")
    val data: List<Hospital>
    )