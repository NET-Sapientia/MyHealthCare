package com.example.myhealthcareapp.fragments.makeAppointment.department

import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.myhealthcareapp.data.repository.ContentRepository
import com.example.myhealthcareapp.model.response.HospitalData
import com.example.myhealthcareapp.model.response.MedicalDepartment
import kotlinx.coroutines.launch

class MedicalDepartmentListViewModel(private val repository: ContentRepository) : ViewModel() {

    var uiState: MutableLiveData<UiState> = MutableLiveData(UiState.Normal)

    fun getDepartments(id: Int) {
        viewModelScope.launch {
            when (val result = repository.getMedicalDepartments(id = id)) {
                null -> uiState.value = UiState.Error
                else -> uiState.value = UiState.WithDepartments(departments = result)
            }
        }
    }

    sealed class UiState {
        object Normal: UiState()
        object Error: UiState()
        data class WithDepartments(val departments: List<MedicalDepartment>): UiState()
    }
}