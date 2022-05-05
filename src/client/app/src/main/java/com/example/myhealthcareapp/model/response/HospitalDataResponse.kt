package com.example.myhealthcareapp.model.response
import com.google.gson.annotations.SerializedName

data class HospitalDataResponse (
    @SerializedName("id")
    val id : Int?,
    @SerializedName("name")
    val name : String?,
    @SerializedName("contact")
    val contact : String?,
    @SerializedName("address")
    val address : String?
)

data class HospitalData(
    val hospitalId : Int,
    val hospitalName : String,
    val hospitalPhoneNumber : String,
    val hospitalAddress : String
)

fun HospitalDataResponse.toModel() = when {
    id == null || name == null || contact == null || address == null -> null
    else -> HospitalData(
        hospitalId = id,
        hospitalName = name,
        hospitalPhoneNumber = contact,
        hospitalAddress = address
    )
}