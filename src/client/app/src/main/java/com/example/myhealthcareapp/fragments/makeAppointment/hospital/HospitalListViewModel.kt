package com.example.myhealthcareapp.fragments.makeAppointment.hospital

import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.myhealthcareapp.data.repository.ContentRepository
import com.example.myhealthcareapp.model.response.HospitalData
import kotlinx.coroutines.launch

class HospitalListViewModel(private val repository: ContentRepository) : ViewModel() {

    var uiState: MutableLiveData<UiState> = MutableLiveData(UiState.Normal)

    init {
        getHospitals()
    }

    fun getHospitals() {
        viewModelScope.launch {
            when (val result = repository.getHospitals()) {
                null -> uiState.value = UiState.Error
                else -> uiState.value = UiState.WithHospitals(hospitals = result)
            }
        }
    }

    sealed class UiState {
        object Normal: UiState()
        object Error: UiState()
        data class WithHospitals(val hospitals: List<HospitalData>): UiState()
    }
}