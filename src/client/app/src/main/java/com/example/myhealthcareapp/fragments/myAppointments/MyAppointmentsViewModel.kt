package com.example.myhealthcareapp.fragments.myAppointments

import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.myhealthcareapp.data.repository.ContentRepository
import com.example.myhealthcareapp.model.response.ClientAppointment
import kotlinx.coroutines.launch

class MyAppointmentsViewModel(private val repository: ContentRepository) : ViewModel() {

    var uiState: MutableLiveData<UiState> = MutableLiveData(UiState.Normal)

    fun getAppointments(id: Int) {
        viewModelScope.launch {
            when (val result = repository.getClientAppointments(id = id)) {
                null -> uiState.value = UiState.Error
                else -> uiState.value = UiState.WithAppointments(appointments = result)
            }
        }
    }

    sealed class UiState {
        object Normal: UiState()
        object Error: UiState()
        data class WithAppointments(val appointments: List<ClientAppointment>): UiState()
    }
}