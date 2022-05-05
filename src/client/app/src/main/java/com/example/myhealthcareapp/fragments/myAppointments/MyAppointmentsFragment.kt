package com.example.myhealthcareapp.fragments.myAppointments

import android.os.Bundle
import android.os.SharedMemory
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import com.example.myhealthcareapp.MainActivity
import com.example.myhealthcareapp.R
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.example.myhealthcareapp.adapters.MyAppointmentsAdapter
import com.example.myhealthcareapp.cache.SharedPreferencesManager
import kotlinx.android.synthetic.main.activity_main.*
import com.example.myhealthcareapp.fragments.BaseFragment
import com.example.myhealthcareapp.model.response.ClientAppointment
import com.example.myhealthcareapp.util.OnItemClickListener
import com.google.android.material.dialog.MaterialAlertDialogBuilder
import org.koin.android.ext.android.inject
import org.koin.androidx.viewmodel.ext.android.viewModel

class MyAppointmentsFragment : BaseFragment(), OnItemClickListener {
    private lateinit var appointments: MutableList<ClientAppointment>
    private lateinit var adapter : MyAppointmentsAdapter
    private val viewModel by viewModel<MyAppointmentsViewModel>()
    private val sharedPreferencesManager: SharedPreferencesManager by inject()

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        val view = inflater.inflate(R.layout.fragment_my_appointments, container, false)

        viewModel.uiState.observe(viewLifecycleOwner) { state ->
            when (state) {
                is MyAppointmentsViewModel.UiState.WithAppointments -> {
                    appointments = state.appointments.toMutableList()
                    println("Appointments: ${appointments.toString()}")
                    println("userId: ${sharedPreferencesManager.getUserId()}")
                    setupUI(view = view)
                }
                is MyAppointmentsViewModel.UiState.Error -> {
                    Toast.makeText(requireActivity(), "Unable to get my appointments", Toast.LENGTH_LONG).show()
                }
                else -> Unit
            }
        }
        viewModel.getAppointments(id = sharedPreferencesManager.getUserId())

        return view
    }

    private fun setupUI(view : View){
        (mActivity as MainActivity).topAppBar.title = (mActivity).getString(R.string.my_appointments)
        (mActivity as MainActivity).bottomNavigation.visibility = View.VISIBLE
        (mActivity as MainActivity).searchIcon.isVisible = false
        (mActivity as MainActivity).profileIcon.isVisible = true

        val recyclerView = view.findViewById<RecyclerView>(R.id.recycler_view)
        adapter = MyAppointmentsAdapter(appointments, this)
        recyclerView.adapter = adapter
        recyclerView.layoutManager = LinearLayoutManager(context)
        recyclerView.setHasFixedSize(true)
    }

    override fun onItemClick(position: Int) {
        val singleAppointment = appointments[position]

        val summary = arrayOf(
            "Department: " + singleAppointment.departmentName,
            "Medic: " + singleAppointment.medicName,
            "Date & Time: " + singleAppointment.startDate + ", " + singleAppointment.endDates,
        )

        MaterialAlertDialogBuilder(requireContext())
            .setTitle(resources.getString(R.string.summary))
            .setItems(summary) {_, _ ->}
            .show()
    }
}