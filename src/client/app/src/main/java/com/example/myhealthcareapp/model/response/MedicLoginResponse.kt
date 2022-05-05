package com.example.myhealthcareapp.model.response

import com.google.gson.annotations.SerializedName

data class MedicLoginResponse (
    @SerializedName("data")
    val data: Medic
    )