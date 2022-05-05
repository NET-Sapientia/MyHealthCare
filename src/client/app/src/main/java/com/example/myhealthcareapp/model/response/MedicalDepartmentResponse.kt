package com.example.myhealthcareapp.model.response

import com.google.gson.annotations.SerializedName

data class MedicalDepartmentResponse (
    @SerializedName("code")
    val code: Int?,
    @SerializedName("error")
    val error: String?,
    @SerializedName("result")
    val result: List<MedicalDepartmentDataResponse>?
)

data class MedicalDepartmentDataResponse(
    @SerializedName("id")
    val id: Int?,
    @SerializedName("name")
    val name: String?,
    @SerializedName("contact")
    val contact: String?
)

fun MedicalDepartmentDataResponse.toModel() = when {
    id == null || name == null || contact == null -> null
    else -> MedicalDepartment(
        id = id,
        name = name,
        contact = contact
    )
}