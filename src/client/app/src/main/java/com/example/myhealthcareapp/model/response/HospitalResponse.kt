package com.example.myhealthcareapp.model.response

import com.google.gson.annotations.SerializedName

data class HospitalResponse(
    @SerializedName("data")
    val data: List<Hospital>
    )