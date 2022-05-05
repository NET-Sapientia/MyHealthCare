package com.example.myhealthcareapp.cache

import com.example.myhealthcareapp.model.response.Medic
import com.example.myhealthcareapp.model.user.Client

object Cache {
    private lateinit var client: Client
    private lateinit var medic: Medic

    fun setClient(currentClient: Client){
        client = currentClient
    }

    fun getClient() = client

    fun setMedic(currentMedic: Medic){
        medic = currentMedic
    }

    fun getMedic() = medic
}