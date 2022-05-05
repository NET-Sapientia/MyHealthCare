package com.example.myhealthcareapp.model.response

import com.google.gson.annotations.SerializedName

data class HospitalResponse(
    @SerializedName("code")
    val code: Int?,
    @SerializedName("error")
    val error: String?,
    @SerializedName("result")
    val result: List<HospitalDataResponse>?
)