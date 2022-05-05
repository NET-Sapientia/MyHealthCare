package com.example.myhealthcareapp.data.v2

import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

class MyHealthCareInstanceV2 {

    private val retrofit by lazy {
        Retrofit.Builder().baseUrl(MyHealthCareInstanceV2.BASE_URL).addConverterFactory(GsonConverterFactory.create()).build()
    }

    val api : MyHealthCareApiV2 by lazy {
        retrofit.create(MyHealthCareApiV2::class.java)
    }

    companion object {
        private const val BASE_URL = "https://c6eb-92-84-24-20.ngrok.io"
    }
}