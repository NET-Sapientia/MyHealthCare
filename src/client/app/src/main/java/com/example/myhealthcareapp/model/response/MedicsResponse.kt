package com.example.myhealthcareapp.model.response

import com.google.gson.annotations.SerializedName

data class MedicsResponse (
    @SerializedName("code")
    val code : Int?,
    @SerializedName("error")
    val error : String?,
    @SerializedName("result")
    val result : List<MedicDataResponse>
)

data class MedicDataResponse(
    @SerializedName("id")
    val id: Int?,
    @SerializedName("name")
    val name: String?
)

fun MedicDataResponse.toModel() = when {
    id == null || name == null -> null
    else -> Medic(
        id = id,
        name = name
    )
}