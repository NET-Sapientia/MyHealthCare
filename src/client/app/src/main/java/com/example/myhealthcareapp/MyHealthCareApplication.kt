package com.example.myhealthcareapp

import android.app.Application
import com.example.myhealthcareapp.cache.SharedPreferencesManager
import org.koin.android.ext.koin.androidContext
import org.koin.core.context.GlobalContext.startKoin
import org.koin.dsl.module

class MyHealthCareApplication : Application() {
    override fun onCreate() {
        super.onCreate()

        startKoin {
            androidContext(this@MyHealthCareApplication)
            modules(myHealthCareModule + initSharedPreferences())
        }

    }

    private fun initSharedPreferences() = module {
        single { SharedPreferencesManager(context = this@MyHealthCareApplication) }
    }
}