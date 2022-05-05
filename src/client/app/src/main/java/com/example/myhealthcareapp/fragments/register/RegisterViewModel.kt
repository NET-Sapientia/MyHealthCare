package com.example.myhealthcareapp.fragments.register

import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.myhealthcareapp.data.repository.AuthenticationRepository
import kotlinx.coroutines.launch

class RegisterViewModel(private val repository: AuthenticationRepository) : ViewModel() {

    var uiState: MutableLiveData<UiState> = MutableLiveData(UiState.Normal)

    fun register(address: String, name: String, email: String, password: String) {
        viewModelScope.launch {
            when (val result = repository.registerClient(
                address = address,
                name = name,
                email = email,
                password = password
            )) {
                null -> uiState.value = UiState.Error
                else -> {
                    uiState.value = UiState.SignUpSuccess
                    // cache user
                }
            }
        }
    }

    sealed class UiState {
        object Normal: UiState()
        object Error: UiState()
        object SignUpSuccess: UiState()
    }
}