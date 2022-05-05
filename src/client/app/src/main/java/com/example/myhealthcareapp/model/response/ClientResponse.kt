package com.example.myhealthcareapp.model.response

import com.example.myhealthcareapp.model.user.Client
import com.google.gson.annotations.SerializedName

data class ClientResponse (
    @SerializedName("data")
    val data : Client
    )