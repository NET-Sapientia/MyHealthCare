package com.example.myhealthcareapp.fragments.login

import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.myhealthcareapp.cache.SharedPreferencesManager
import com.example.myhealthcareapp.data.repository.AuthenticationRepository
import kotlinx.coroutines.launch

class LoginViewModel(
    private val repository: AuthenticationRepository,
    private val sharedPreferencesManager: SharedPreferencesManager
) : ViewModel() {

    var uiState: MutableLiveData<UiState> = MutableLiveData(UiState.Normal)

    fun loginClient(email: String, password: String) {
        viewModelScope.launch {
            when (val result = repository.loginClient(email = email, password = password)) {
                null -> uiState.value = UiState.Error
                else -> {
                    uiState.value =  UiState.LoginSuccess
                    sharedPreferencesManager.saveUser(
                        id = result.result!!.id,
                        name = result.result.name,
                        email = result.result.email,
                        address = result.result.address
                    )
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