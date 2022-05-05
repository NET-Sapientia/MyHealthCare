package com.example.myhealthcareapp.model.response

import com.google.gson.annotations.SerializedName

data class ClientAppointmentResponse(
    @SerializedName("code")
    val code : Int?,
    @SerializedName("error")
    val error : String?,
    @SerializedName("result")
    val result : List<ClientAppointmentResultResponse>?,
)

data class ClientAppointmentResultResponse (
    @SerializedName("id")
    val id : Int?,
    @SerializedName("departmentName")
    val departmentName : String?,
    @SerializedName("medicName")
    val medicName : String?,
    @SerializedName("startDate")
    val startDate : String?,
    @SerializedName("endDate")
    val endDates : String?,
    @SerializedName("notes")
    val notes : String?
    )

fun ClientAppointmentResultResponse.toModel() = when {
    id == null || departmentName == null || medicName == null || startDate == null || endDates == null || notes == null -> null
    else -> ClientAppointment(
       id = id,
       departmentName = departmentName,
       medicName = medicName,
       startDate = startDate,
       endDates = endDates,
       notes = notes
    )
}

data class ClientAppointment(
    val id : Int,
    val departmentName : String,
    val medicName : String,
    val startDate : String,
    val endDates : String,
    val notes : String
)