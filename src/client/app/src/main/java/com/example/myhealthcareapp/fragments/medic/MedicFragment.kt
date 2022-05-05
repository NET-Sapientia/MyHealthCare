package com.example.myhealthcareapp.fragments.medic

import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.example.myhealthcareapp.MainActivity
import com.example.myhealthcareapp.R
import com.example.myhealthcareapp.adapters.MedicAppointmentAdapter
import com.example.myhealthcareapp.data.v1.MyHealthCareViewModel
import com.example.myhealthcareapp.fragments.BaseFragment
import com.example.myhealthcareapp.model.response.ClientAppointment
import com.example.myhealthcareapp.model.response.ClientAppointmentResponse
import kotlinx.android.synthetic.main.activity_main.*
import org.koin.androidx.viewmodel.ext.android.sharedViewModel

class MedicFragment : BaseFragment(){
    private lateinit var appointments: MutableList<ClientAppointment>
    private lateinit var adapter: MedicAppointmentAdapter
    private lateinit var recyclerView: RecyclerView
    private val viewModel by sharedViewModel<MyHealthCareViewModel>()

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        val view = inflater.inflate(R.layout.fragment_medic, container, false)

        recyclerView = view.findViewById(R.id.recycler_view)

//        viewModel.medicAppointments.observe(viewLifecycleOwner, { response ->
//            if(response.isSuccessful){
//                appointments = response.body()?.data as MutableList
//                initUI()
//            }
//            Log.d("medic", response.message().toString())
//        })

        viewModel.getMedicAppointment(1)

        return view
    }

    private fun initUI(){
        adapter = MedicAppointmentAdapter(appointments)
        recyclerView.adapter = adapter
        recyclerView.layoutManager = LinearLayoutManager(context)
        recyclerView.setHasFixedSize(true)

        (mActivity as MainActivity).topAppBar.title = (mActivity).getString(R.string.upcoming_appointments)
        (mActivity as MainActivity).bottomNavigation.visibility = View.GONE
        (mActivity as MainActivity).topAppBar.visibility = View.VISIBLE
        (mActivity as MainActivity).searchIcon.isVisible = false
        (mActivity as MainActivity).profileIcon.isVisible = true
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
        (mActivity as MainActivity).profileIcon.setOnMenuItemClickListener {
            (mActivity as MainActivity).replaceFragment(MedicProfileFragment(), R.id.fragment_container, addToBackStack = true)
            true
        }
    }
}