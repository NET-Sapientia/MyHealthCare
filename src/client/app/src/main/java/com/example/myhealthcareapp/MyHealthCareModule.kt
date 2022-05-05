package com.example.myhealthcareapp

import com.example.myhealthcareapp.data.v1.MyHealthCareInstance
import com.example.myhealthcareapp.data.v1.MyHealthCareRepository
import com.example.myhealthcareapp.data.v1.MyHealthCareViewModel
import com.example.myhealthcareapp.data.v2.MyHealthCareInstanceV2
import com.example.myhealthcareapp.data.repository.AuthenticationRepository
import com.example.myhealthcareapp.data.repository.ContentRepository
import com.example.myhealthcareapp.fragments.login.LoginViewModel
import com.example.myhealthcareapp.fragments.makeAppointment.department.MedicalDepartmentListViewModel
import com.example.myhealthcareapp.fragments.makeAppointment.hospital.HospitalListViewModel
import com.example.myhealthcareapp.fragments.makeAppointment.medic.BookAppointmentViewModel
import com.example.myhealthcareapp.fragments.register.RegisterViewModel
import org.koin.androidx.viewmodel.dsl.viewModel
import org.koin.dsl.module

val myHealthCareModule = module {
    //V1
    single { MyHealthCareInstance() }
    single { MyHealthCareRepository(get()) }
    viewModel { MyHealthCareViewModel(get()) }

    //V2
    single { MyHealthCareInstanceV2() }
    single { AuthenticationRepository(instance = get()) }
    single { ContentRepository(instance = get()) }

    viewModel { LoginViewModel(repository = get(), sharedPreferencesManager = get()) }
    viewModel { RegisterViewModel(repository = get(), sharedPreferencesManager = get()) }
    viewModel { HospitalListViewModel(repository = get()) }
    viewModel { MedicalDepartmentListViewModel(repository = get()) }
    viewModel { BookAppointmentViewModel(repository = get()) }
}