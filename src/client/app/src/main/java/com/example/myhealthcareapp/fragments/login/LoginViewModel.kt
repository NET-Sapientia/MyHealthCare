package com.example.myhealthcareapp.fragments.login

import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.myhealthcareapp.data.repository.AuthenticationRepository
import com.example.myhealthcareapp.model.repository.auth.Login
import com.example.myhealthcareapp.model.response.MedicLoginResponse
import kotlinx.coroutines.launch
import retrofit2.Response

class LoginViewModel(private val repository: AuthenticationRepository) : ViewModel() {

    var uiState: MutableLiveData<UiState> = MutableLiveData(UiState.Normal)

    fun loginClient(email: String, password: String) {
        viewModelScope.launch {
            when (val result = repository.loginClient(email = email, password = password)) {
                null -> uiState.value = UiState.Error
                else -> {
                   uiState.value =  UiState.LoginSuccess
                    // cache user
                }
            }
        }
    }

    sealed class UiState {
        object Normal: UiState()
        object Error: UiState()
        object LoginSuccess: UiState()
    }
}