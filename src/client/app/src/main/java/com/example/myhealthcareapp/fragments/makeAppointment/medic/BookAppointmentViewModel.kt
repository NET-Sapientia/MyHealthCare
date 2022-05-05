package com.example.myhealthcareapp.fragments.makeAppointment.medic

import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.myhealthcareapp.data.repository.ContentRepository
import com.example.myhealthcareapp.model.response.HospitalData
import com.example.myhealthcareapp.model.response.MakeAppointment
import com.example.myhealthcareapp.model.response.Medic
import kotlinx.coroutines.launch

class BookAppointmentViewModel(private val repository: ContentRepository) : ViewModel() {

    var uiState: MutableLiveData<UiState> = MutableLiveData(UiState.Normal)

    fun getMedics(id: Int) {
        viewModelScope.launch {
            when (val result = repository.getMedics(id = id)) {
                null -> uiState.value = UiState.Error
                else -> uiState.value = UiState.WithMedics(medics = result)
            }
        }
    }

    fun makeAppointment(appointment: MakeAppointment) {
        viewModelScope.launch {
            when(val result = repository.makeAppointment(appointment = appointment)) {
                null -> uiState.value = UiState.Error
                else -> uiState.value = UiState.AppointmentMade
            }
        }
    }

    sealed class UiState {
        object Normal: UiState()
        object Error: UiState()
        data class WithMedics(val medics: List<Medic>): UiState()
        object AppointmentMade: UiState()
    }
}