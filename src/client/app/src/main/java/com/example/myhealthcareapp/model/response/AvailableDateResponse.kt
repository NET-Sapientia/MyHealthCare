package com.example.myhealthcareapp.model.response

import com.google.gson.annotations.SerializedName

data class AvailableDateResponse (
    @SerializedName("data")
    val data : List<AvailableDate>
    )