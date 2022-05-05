package com.example.myhealthcareapp.model.response

import com.google.gson.annotations.SerializedName

data class MedicsResponse (
    @SerializedName("data")
    val data : List<Medic>
)